using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Xml;
using System.Data.Odbc;
using System.Reflection;
using System.Collections;

/// <summary>
/// Class Handler implements IHandler interface
/// </summary>
public class Handler : IHandler
{
    public Handler()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    DataSet ds = new DataSet();
    DataRelation drel = null;
    DataTable dtSelect = new DataTable();
    private string column_nodes = string.Empty;
    private string _condition = string.Empty;

    public string strinsert_values = string.Empty;
    public string strinsert_names = string.Empty;
    private string SQL = "INSERT INTO";
    private string SQLUpdate = "UPDATE";
    private string SQLSelect = "SELECT ";
    private string strPrimaryKey = string.Empty;
    private string strPrimaryKeyValue = string.Empty;
    private string strSelectColumns = string.Empty;
    private string joinTable = string.Empty;
    DataSet dsSelect = new DataSet();
    public string connection = ConfigurationManager.ConnectionStrings["db_ConnectionString"].ToString();

    #region Properties

    public string Condition
    {
        get { return _condition; }
        set { _condition = value; }
    }

    #endregion


    #region IHandler Members

    public bool Insert(bool flag, string tableName, object objClass, string pathXML)
    {
        try
        {
            string col_name = string.Empty;
            string col_datatype = string.Empty;
            string col_isnull = string.Empty;
            string col_text = string.Empty;
            object o = null;


            ds.ReadXml(pathXML);

            drel = new DataRelation("tableRelation", ds.Tables["table"].Columns["table_Id"], ds.Tables["column"].Columns["table_Id"]);

            for (int i = 0; i < drel.ParentTable.Rows.Count; i++)
            {
                if (tableName == ds.Tables["table"].Rows[i]["name"].ToString())
                {
                    strPrimaryKey = ds.Tables["table"].Rows[i]["pk"].ToString();

                    for (int j = 0; j < drel.ChildTable.Rows.Count; j++)
                    {
                        if (drel.ParentTable.Rows[i]["table_Id"].ToString() == drel.ChildTable.Rows[j]["table_Id"].ToString())
                        {
                            col_name += "," + drel.ChildTable.Rows[j]["name"].ToString();
                            col_text += "," + drel.ChildTable.Rows[j]["text"].ToString();
                            col_datatype += "," + drel.ChildTable.Rows[j]["datatype"].ToString();
                            col_isnull += "," + drel.ChildTable.Rows[j]["isnull"].ToString();
                        }
                    }
                }
            }


            col_name = col_name.Substring(1, col_name.Length - 1);
            string[] colname = col_name.Split(',');

            col_datatype = col_datatype.Substring(1, col_datatype.Length - 1);
            string[] coldatatype = col_datatype.Split(',');

            col_text = col_text.Substring(1, col_text.Length - 1);
            string[] coltext = col_text.Split(',');

            col_isnull = col_isnull.Substring(1, col_isnull.Length - 1);
            string[] colisnull = col_isnull.Split(',');



            int ic = 0;
            foreach (var strproperty in objClass.GetType().GetProperties())
            {
                if (strproperty.CanRead)
                {
                    o = strproperty.GetValue(objClass, null);

                    if (flag == false)
                    {
                        if (o == null)
                        {
                            o = "-1";
                        }
                        else if (o.ToString() == "False")
                        {
                            o = "-1";

                        }
                    }

                    string prop_type = o.GetType().ToString();

                    if (o.ToString() != string.Empty && o.ToString() != null && o.ToString() != "-1")
                    {
                        if (strPrimaryKey == strproperty.Name)
                        {
                            strPrimaryKeyValue = o.ToString();
                        }

                        string[] strPropertyName = strproperty.ToString().Split(' ');
                        string PropertyName = strPropertyName[1].ToString().Replace('_', '.').ToString();

                        if (colname[ic].Contains('$'))
                        {
                            string temp = colname[ic].ToString();
                            temp = temp.Substring(temp.IndexOf('(') + 1, temp.IndexOf(" s") - temp.IndexOf('(') - 1);
                            string[] arrtemp = temp.Split('.');

                            colname[ic] = arrtemp[0] + "." + arrtemp[1].ToString();

                        }

                        if (colname[ic] == PropertyName)
                        {
                            strinsert_names += "," + PropertyName;


                            if (prop_type == coldatatype[ic].ToString())
                            {
                                if (coldatatype[ic] == "System.String")
                                {
                                   
                                    //strinsert_values += "~" + "'" + strproperty.GetValue(objClass, null).ToString().ToUpper() + "'";
                                    string Colmnvalue = strproperty.GetValue(objClass, null).ToString().ToUpper();
                                    Colmnvalue = Colmnvalue.Replace("'", "\\'");
                                    strinsert_values += "~" + "'" + Colmnvalue + "'";

                                }
                                else if (coldatatype[ic] == "System.DateTime")
                                {
                                    DateTime DateTimee = (DateTime)strproperty.GetValue(objClass, null);
                                    string Date = DateTimee.ToString("yyyy-MM-dd");
                                    strinsert_values += "~" + "'" + Date + "'";
                                }
                                else if (coldatatype[ic] == "System.Double" || coldatatype[ic] == "System.Float" || coldatatype[ic] == "System.Int32" || coldatatype[ic] == "System.Boolean")
                                {
                                    strinsert_values += "~" + strproperty.GetValue(objClass, null).ToString();
                                }
                            }
                        }


                    }
                    else
                    {
                        ///checking if not is not allowed
                        ////check in xml file(column:isnull) if property value is null;
                        if (colisnull[ic] == "False")
                        {
                            ////null not allowed in this column.
                            if (flag != false)
                                return false;
                        }

                    }
                }

                ic++;

            }

            strinsert_names = strinsert_names.Substring(1, strinsert_names.Length - 1);
            strinsert_values = strinsert_values.Substring(1, strinsert_values.Length - 1);

            if (CreateSQL(flag, tableName))
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        catch (Exception ex)
        {
            int Lineno = Convert.ToInt32(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(":line") + 5));
            string ExcType = ex.GetType().Name;
            string strErrorText = string.Empty;
            if (flag == true)
                strErrorText = "Insert fail in table " + tableName;
            else
                strErrorText = "Update fail in table " + tableName;
            //"Page:Handler.cs" + Environment.NewLine + "Method:Insert" + Environment.NewLine + "Line No:" + Lineno + Environment.NewLine +
            string strStackTrace = "ExeptionType: " + ExcType + "\r\n Message: " + ex.Message + Environment.NewLine;
            if (HttpContext.Current != null)
            {
                strStackTrace += "\r\n URL: " + HttpContext.Current.Request.Url +
                                 "StackTrace: " + ex.StackTrace;
            }
            else
            {
                strStackTrace += "StackTrace: " + ex.StackTrace;
            }
            if (HttpContext.Current != null && HttpContext.Current.Session["userID"] != null)
            {
                strStackTrace = strStackTrace + "\r\n User ID : " + HttpContext.Current.Session["userID"].ToString();
            }
            int rowsAffects = General.InsertTrackOfError(strErrorText, strStackTrace);

        }

        return false;
    }

    public bool Insert2(bool flag, string tableName, object objClass, string pathXML)
    {
        try
        {
            string col_name = string.Empty;
            string col_datatype = string.Empty;
            string col_isnull = string.Empty;
            string col_text = string.Empty;
            object o = null;


            ds.ReadXml(pathXML);

            drel = new DataRelation("tableRelation", ds.Tables["table"].Columns["table_Id"], ds.Tables["column"].Columns["table_Id"]);

            for (int i = 0; i < drel.ParentTable.Rows.Count; i++)
            {
                if (tableName == ds.Tables["table"].Rows[i]["name"].ToString())
                {
                    strPrimaryKey = ds.Tables["table"].Rows[i]["pk"].ToString();

                    for (int j = 0; j < drel.ChildTable.Rows.Count; j++)
                    {
                        if (drel.ParentTable.Rows[i]["table_Id"].ToString() == drel.ChildTable.Rows[j]["table_Id"].ToString())
                        {
                            col_name += "," + drel.ChildTable.Rows[j]["name"].ToString();
                            col_text += "," + drel.ChildTable.Rows[j]["text"].ToString();
                            col_datatype += "," + drel.ChildTable.Rows[j]["datatype"].ToString();
                            col_isnull += "," + drel.ChildTable.Rows[j]["isnull"].ToString();
                        }
                    }
                }
            }


            col_name = col_name.Substring(1, col_name.Length - 1);
            string[] colname = col_name.Split(',');

            col_datatype = col_datatype.Substring(1, col_datatype.Length - 1);
            string[] coldatatype = col_datatype.Split(',');

            col_text = col_text.Substring(1, col_text.Length - 1);
            string[] coltext = col_text.Split(',');

            col_isnull = col_isnull.Substring(1, col_isnull.Length - 1);
            string[] colisnull = col_isnull.Split(',');



            int ic = 0;
            foreach (var strproperty in objClass.GetType().GetProperties())
            {
                if (strproperty.CanRead)
                {
                    o = strproperty.GetValue(objClass, null);



                    string prop_type = o.GetType().ToString();

                    if (o.ToString() != string.Empty && o.ToString() != null && o.ToString() != "-1")
                    {
                        if (strPrimaryKey == strproperty.Name)
                        {
                            strPrimaryKeyValue = o.ToString();
                        }

                        string[] strPropertyName = strproperty.ToString().Split(' ');
                        string PropertyName = strPropertyName[1].ToString().Replace('_', '.').ToString();

                        if (colname[ic].Contains('$'))
                        {
                            string temp = colname[ic].ToString();
                            temp = temp.Substring(temp.IndexOf('(') + 1, temp.IndexOf(" s") - temp.IndexOf('(') - 1);
                            string[] arrtemp = temp.Split('.');

                            colname[ic] = arrtemp[0] + "." + arrtemp[1].ToString();

                        }

                        if (colname[ic] == PropertyName)
                        {
                            strinsert_names += "," + PropertyName;


                            if (prop_type == coldatatype[ic].ToString())
                            {
                                if (coldatatype[ic] == "System.String")
                                {
                                    //strinsert_values += "~" + "'" + strproperty.GetValue(objClass, null).ToString() + "'";
                                    string Colmnvalue = strproperty.GetValue(objClass, null).ToString();
                                    Colmnvalue = Colmnvalue.Replace("'", "\\'");
                                    strinsert_values += "~" + "'" + Colmnvalue + "'";
                                }
                                else if (coldatatype[ic] == "System.DateTime")
                                {
                                    DateTime DateTimee = (DateTime)strproperty.GetValue(objClass, null);
                                    string Date = DateTimee.ToString("yyyy-MM-dd");
                                    strinsert_values += "~" + "'" + Date + "'";
                                }
                                else if (coldatatype[ic] == "System.Double" || coldatatype[ic] == "System.Float" || coldatatype[ic] == "System.Int32" || coldatatype[ic] == "System.Boolean")
                                {
                                    strinsert_values += "~" + strproperty.GetValue(objClass, null).ToString();
                                }
                            }
                        }


                    }
                    else
                    {
                        ///checking if not is not allowed
                        ////check in xml file(column:isnull) if property value is null;
                        if (colisnull[ic] == "False")
                        {
                            ////null not allowed in this column.
                            if (flag != false)
                                return false;
                        }

                    }
                }

                ic++;

            }

            strinsert_names = strinsert_names.Substring(1, strinsert_names.Length - 1);
            strinsert_values = strinsert_values.Substring(1, strinsert_values.Length - 1);

            if (CreateSQL(flag, tableName))
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        catch (Exception ex)
        {
            string ExcType = ex.GetType().Name;
            string strErrorText = string.Empty;
            if (flag == true)
                strErrorText = "Insert fail in table " + tableName;
            else
                strErrorText = "Update fail in table " + tableName;
            //"Page:Handler.cs" + Environment.NewLine + "Method:Insert" + Environment.NewLine + "Line No:" + Lineno + Environment.NewLine +
            string strStackTrace = "ExeptionType: " + ExcType + "\r\n Message: " + ex.Message + Environment.NewLine +
                                   "\r\n URL: " + HttpContext.Current.Request.Url +
                                   "StackTrace: " + ex.StackTrace;
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["userID"] != null)
            {
                strStackTrace = strStackTrace + "\r\n User ID : " + HttpContext.Current.Session["userID"].ToString();
            }
            
            int rowsAffects = General.InsertTrackOfError(strErrorText, strStackTrace);

        }

        return false;
    }

    public bool CreateSQL(bool flag, string tableName)
    {
        int i = 0;
        try
        {
            //OdbcConnection conn = new OdbcConnection(connection);
            //OdbcCommand cmd = null;
            DataTable dtprevious = new DataTable();
            DataTable dtLatest = new DataTable();
            string strinsertSSQL = string.Empty;

            string SqlStmt = string.Empty;
            if (flag)
            {


                if (tableName != null)
                {
                    strinsert_values = strinsert_values.Replace('~', ',');
                    SQL += " " + tableName + "(" + strinsert_names + ") VALUES(" + strinsert_values + ")";
                }
                strinsertSSQL = SQL;
                //cmd = new OdbcCommand(SQL, conn);
                SqlStmt = SQL;

            }
            else
            {
                string SQL = "SELECT " + strinsert_names + " FROM " + tableName + " WHERE " + Condition;
                dtprevious = GetTable(SQL);

                if (tableName != null)
                {
                    string[] UpdateColums = strinsert_names.Split(',');
                    string[] UpdateValues = strinsert_values.Split('~');
                    int j = 0;
                    string strupdate = string.Empty;

                    foreach (string column in UpdateColums)
                    {
                        strupdate += column + "=" + UpdateValues[j].ToString() + ", ";
                        j++;
                    }
                    SQLUpdate += " " + tableName + "  SET  " + strupdate + "";
                    int index = SQLUpdate.LastIndexOf(",");
                    SQLUpdate = SQLUpdate.Remove(index);
                    //SQLUpdate += "WHERE " + strPrimaryKey + " = '" + strPrimaryKeyValue + "'";
                    SQLUpdate += " WHERE " + Condition;
                }

                //cmd = new OdbcCommand(SQLUpdate, conn);
                SqlStmt = SQLUpdate;

            }

            //conn.Open();
            //int i = cmd.ExecuteNonQuery();
            i = ExecuteSqlQuery(SqlStmt);
            if (i != 0)
            {
                string previoushistoryvalue = string.Empty;
                string newhistoryvalue = string.Empty;
                if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session["UserDetailID"] != null && flag == false)
                {
                    string SQL = "SELECT " + strinsert_names + " FROM " + tableName + " WHERE " + Condition;
                    dtLatest = GetTable(SQL);
                    string SQLStatment = "UPDATE " + tableName + " SET ";
                    if (dtLatest.Rows.Count > 0)
                    {
                        for (int c = 0; c < dtLatest.Columns.Count; c++)
                        {

                            if (dtLatest.Rows[0][c].ToString() != dtprevious.Rows[0][c].ToString())
                            {
                                SQLStatment += dtLatest.Columns[c].ColumnName + " = " + dtLatest.Rows[0][c].ToString() + ",";
                                newhistoryvalue += dtLatest.Columns[c].ColumnName + " = " + dtLatest.Rows[0][c].ToString() + "~";
                                previoushistoryvalue += dtprevious.Columns[c].ColumnName + " = " + dtprevious.Rows[0][c].ToString() + "~";
                            }

                        }
                    }
                    else
                    {
                    }
                    if (previoushistoryvalue.Contains('~'))
                    {
                        previoushistoryvalue = previoushistoryvalue.Remove(previoushistoryvalue.LastIndexOf('~'));
                    }
                    if (newhistoryvalue.Contains('~'))
                    {
                        newhistoryvalue = newhistoryvalue.Remove(newhistoryvalue.LastIndexOf('~'));
                    }
                    if (SQLStatment != string.Empty && SQLStatment.Contains(','))
                    {
                        int index = SQLStatment.LastIndexOf(",");
                        SQLStatment = SQLStatment.Remove(index);
                        SQLStatment += " WHERE " + Condition;
                    }
                    //CHECK PRIMARY KEY
                    string primarykeyhistory = string.Empty;
                    if (Condition != string.Empty)
                    {
                        string primarystring = Condition.ToUpper();
                        string[] primary = Condition.Split(new string[] { "AND" }, StringSplitOptions.None);
                        if (primary.Length > 1)
                        {
                            string[] key = { "RELATIONSHIPID", "PROJECTID", "PRODUCTID" };
                            for (int m = 0; m < primary.Length; m++)
                            {
                                string pri = primary[m].ToString().Split('=')[0].ToString();
                                for (int n = 0; n < key.Length; n++)
                                {
                                    if (pri.Trim() == key[n].ToString().Trim())
                                    {
                                        primarykeyhistory = primary[m].ToString().Split('=')[1].ToString();
                                        break;
                                    }
                                }

                            }
                        }
                        else if (primary.Length == 1)
                        {
                            if (primary[0].ToString().Contains('='))
                            {
                                primarykeyhistory = primary[0].ToString().Split('=')[1].ToString();
                            }
                            else
                            {
                                if (primary[0].ToString().Contains('('))
                                {
                                     primarykeyhistory = primary[0].ToString().Split('(')[1].ToString().Split(')')[0].ToString();
                                }
                            }
                        }
                    }
                    //Inserting UserLoginDetails*********
                    if (!SQLStatment.EndsWith(" SET "))
                    {
                        if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session["UpdatePrimaryKey"] != null && HttpContext.Current.Session["UpdatePrimaryKey"].ToString() != "" && HttpContext.Current.Session["UpdateTabID"] != null && HttpContext.Current.Session["UpdateTabID"].ToString() != "")
                        {
                          
                        }
                    }
                }
                else if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session["UserDetailID"] != null && flag == true)
                {

                   
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            //int Lineno = General.Parse<int>(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(":line") + 5));
            string ExcType = ex.GetType().Name;
            string strErrorText = "fail to add userloginhistorydetails for table " + tableName;
            string strStackTrace = string.Empty;
            //if (Lineno == 428)
                //strStackTrace = "Sql Statement: " + SQL+ Environment.NewLine;

            //"Page:Handler.cs" + Environment.NewLine + "Method:Insert" + Environment.NewLine + "Line No:" + Lineno + Environment.NewLine +
            strStackTrace += "ExeptionType: " + ExcType +"\r\n Message: " + ex.Message +
                            "\r\n StackTrace : " + ex.StackTrace;


            if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session["userID"] != null && HttpContext.Current.Request != null)
            {
                strStackTrace = strStackTrace +  "\r\n URL: " + HttpContext.Current.Request.Url + 
                                "\r\n User ID : " + HttpContext.Current.Session["userID"].ToString();
            }
            
            int rowsAffects = General.InsertTrackOfError(strErrorText, strStackTrace);
        }
        if (i != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public DataTable GetTable(string column, string tablee)
    {
        try
        {
            SQLSelect += " " + column + " FROM " + tablee;
            OdbcDataAdapter odbcda = new OdbcDataAdapter(SQLSelect, connection);
            odbcda.Fill(dtSelect);
            return dtSelect;
        }
        catch (Exception ex)
        {
            string ExcType = ex.GetType().Name;
            string strErrorText = "Select fail for table " + tablee;
            //"Page:Handler.cs" + Environment.NewLine + "Method:Insert" + Environment.NewLine + "Line No:" + Lineno + Environment.NewLine +
            string strStackTrace = "Sql Select Statement : "+ SQLSelect+"\r\n ExeptionType : " + ExcType + "\r\n Message : " + ex.Message + Environment.NewLine +
                                   "\r\n URL: " + HttpContext.Current.Request.Url +
                                   "\r\n StackTrace : " + ex.StackTrace;
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["userID"] != null)
            {
                strStackTrace = strStackTrace + "\r\n User ID : " + HttpContext.Current.Session["userID"].ToString();
            }
            
            General.InsertTrackOfError(strErrorText, strStackTrace);
        }

        return dtSelect;
    }


    public DataTable GetTable(ArrayList columns, string tablee, string conditon)
    {
        try
        {
            foreach (var column in columns)
            {
                strSelectColumns += "," + column;
            }
            strSelectColumns = strSelectColumns.Substring(1);
            if (conditon != string.Empty)
            {
                SQLSelect += " " + strSelectColumns + " FROM " + tablee + " WHERE " + conditon;
            }
            else
            {
                SQLSelect += " " + strSelectColumns + " FROM " + tablee;
            }

            OdbcDataAdapter odbcda = new OdbcDataAdapter(SQLSelect, connection);
            odbcda.Fill(dtSelect);
            return dtSelect;
        }
        catch (Exception ex)
        {
            string ExcType = ex.GetType().Name;
            string ErrorText = "Select fail for table " + tablee;
            //"Page:Handler.cs" + Environment.NewLine + "Method:Insert" + Environment.NewLine + "Line No:" + Lineno + Environment.NewLine +
            string Stacktrace = "Sql Select Statement : " + SQLSelect + "\r\n ExeptionType : " + ExcType + "\r\n Message : " + ex.Message + Environment.NewLine +
                                "\r\n URL: " + HttpContext.Current.Request.Url +
                                   "\r\n StackTrace : " + ex.StackTrace;

            if (HttpContext.Current.Session != null && HttpContext.Current.Session["userID"] != null)
            {
                Stacktrace = Stacktrace + "\r\n User ID: " + HttpContext.Current.Session["userID"].ToString();
            }
            General.InsertTrackOfError(ErrorText, Stacktrace);
        }

        return dtSelect;
    }

    //using join
    public DataTable GetTable(ArrayList columns, ArrayList tables, string conditon, string joinType, ArrayList OnCondition)
    {
        try
        {

            for (int even = 0; even < tables.Count; even++)
            {

                if (even == 0)
                {
                    joinTable += " " + tables[even].ToString() + " " + joinType;
                }
                else
                {
                    joinTable += " " + tables[even].ToString() + " " + OnCondition[even - 1].ToString() + " " + joinType;
                }

            }

            int index = joinTable.LastIndexOf(joinType);
            joinTable = joinTable.Remove(index, joinType.Length);

            foreach (var column in columns)
            {
                strSelectColumns += "," + column;
            }

            //joinTable = joinTable.Substring(joinType.Length + 1);

            strSelectColumns = strSelectColumns.Substring(1);

            if (conditon != string.Empty)
            {
                SQLSelect += " " + strSelectColumns + " FROM " + joinTable + " WHERE " + conditon;
            }
            //else
            //{
            //    SQLSelect += " " + strSelectColumns + " FROM " + tables;
            //}

            OdbcDataAdapter odbcda = new OdbcDataAdapter(SQLSelect, connection);
            odbcda.Fill(dtSelect);
            return dtSelect;
        }
        catch (Exception ex)
        {
            string ExcType = ex.GetType().Name;
            string strErrorText = "Select fail for tables " + joinTable;
            //"Page:Handler.cs" + Environment.NewLine + "Method:Insert" + Environment.NewLine + "Line No:" + Lineno + Environment.NewLine +
            string strStackTrace = "Sql Select Statement : " + SQLSelect + "\r\n ExeptionType : " + ExcType + "\r\n Message : " + ex.Message +
                                    "\r\n URL: " + HttpContext.Current.Request.Url +
                                   "\r\n StackTrace : " + ex.StackTrace;
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["userID"] != null)
            {
                strStackTrace = strStackTrace + "\r\n User ID : " + HttpContext.Current.Session["userID"].ToString();
            }
            General.InsertTrackOfError(strErrorText, strStackTrace);
        }

        return dtSelect;
    }


    public DataTable GetTable(string SQLStmtWithJoin)
    {
        //dtSelect.Columns.Clear();
        //dtSelect.Rows.Clear();
        DataTable dtSelect = new DataTable();
        OdbcDataAdapter odbcda = new OdbcDataAdapter(SQLStmtWithJoin, connection);
        if (SQLStmtWithJoin != string.Empty)
            odbcda.Fill(dtSelect);
        return dtSelect;
    }

    public DataSet GetDataSet(string SQLStmtWithJoin)
    {

        OdbcDataAdapter odbcda = new OdbcDataAdapter(SQLStmtWithJoin, connection);
        if (SQLStmtWithJoin != string.Empty)
            odbcda.Fill(dsSelect);
        return dsSelect;
    }

    public int InsertData(string SQLStmtWithJoin)
    {
        int i = 0;
        try
        {
            OdbcConnection odbcConnection = new OdbcConnection(connection);
            if (odbcConnection.State == ConnectionState.Closed)
            {
                odbcConnection.Open();
            }

            OdbcCommand odbccmd = new OdbcCommand(SQLStmtWithJoin, odbcConnection);
            i = odbccmd.ExecuteNonQuery();

            odbcConnection.Close();
        }
        catch (Exception ex)
        {
            string ExcType = ex.GetType().Name;
            string strErrorText = "Insert fail ";
            
            string strStackTrace = "Sql Statement: " + SQLStmtWithJoin + "\r\n ExeptionType: " + ExcType + "\r\n Message: " + ex.Message + Environment.NewLine +
                                    "\r\n URL: " + HttpContext.Current.Request.Url +
                                   "StackTrace: " + ex.StackTrace;
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["userID"] != null)
            {
                strStackTrace = strStackTrace + "\r\n User ID : " + HttpContext.Current.Session["userID"].ToString();
            }
            int rowsAffects = General.InsertTrackOfError(strErrorText, strStackTrace);
        }
        return i;
    }

    public bool UpdateExplicit(string SQLUpdate)
    {
        using (OdbcConnection conn = new OdbcConnection(connection))
        {
            conn.Open();
            OdbcCommand cmd = new OdbcCommand(SQLUpdate, conn);
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                return true;
            }

        }

        return false;
    }
    private int ExecuteSqlQuery(string SqlStmt)
    {
        int result = 0;
        try
        {
            OdbcConnection conn = new OdbcConnection(connection);
            OdbcCommand cmd = new OdbcCommand(SqlStmt, conn);
            conn.Open();
            result = cmd.ExecuteNonQuery();
            conn.Close();
        }
        catch (Exception ex)
        {
            string ExcType = ex.GetType().Name;
            string strErrorText = "Insert/Update fail in table ";
            string strStackTrace = "Sql Statement : " + SqlStmt +
                                    "\r\n URL: " + HttpContext.Current.Request.Url +
                                    "\r\n ExeptionType: " + ExcType + "\r\n Message: " + ex.Message +
                                   "\r\n StackTrace: " + ex.StackTrace;
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["userID"] != null)
            {
                strStackTrace = strStackTrace + "\r\n User ID: " + HttpContext.Current.Session["userID"].ToString();
            }
            
            int rowsAffects = General.InsertTrackOfError(strErrorText, strStackTrace);
        }
        return result;

    }

    //Sumit K 2013-06-06 To Get Scaler Value 
    public string ExecuteScalerQuery(string SqlStmt)
    {
        string Result = string.Empty;
        OdbcConnection odbcConn = new OdbcConnection(connection);
            OdbcCommand odbcCmd = new OdbcCommand(SqlStmt, odbcConn);
        try
        {
            odbcConn.Open();
            object obj = odbcCmd.ExecuteScalar();
            Result = obj.ToString();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            if (odbcConn.State == ConnectionState.Open)
            {
                odbcConn.Close();
            }
            
            odbcCmd.Dispose();
        }
        return Result;
    }
    #endregion



}

