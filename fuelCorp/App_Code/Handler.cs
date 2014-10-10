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
using System.Data.SqlClient;

/// <summary>
/// Summary description for Handler
/// </summary>
public class Handler
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
    public string connection = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();

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


        }

        return false;
    }

    public bool CreateSQL(bool flag, string tableName)
    {
        int i = 0;
        try
        {
            DataTable dtprevious = new DataTable();
            DataTable dtLatest = new DataTable();
            string strinsertSSQL = string.Empty;

            string SqlStmt = string.Empty;
            if (flag)
            {


                if (tableName != null)
                {
                    strinsert_values = strinsert_values.Replace('~', ',');
                    strinsert_values = strinsert_values.Replace('$', '~');
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
                            //UserLoginHistoryDetails objhistorydetails = new UserLoginHistoryDetails(HttpContext.Current.Server.MapPath("~/XML/1.xml"));
                            //objhistorydetails.userloginhistorydetails_ULID = General.Parse<int>(HttpContext.Current.Session["UserDetailID"].ToString());
                            //objhistorydetails.userloginhistorydetails_ULDID = -1;
                            //objhistorydetails.userloginhistorydetails_QUERY = SQLStatment;
                            //objhistorydetails.userloginhistorydetails_TABLENAME = tableName;
                            //objhistorydetails.userloginhistorydetails_PREVIOUSVALUE = previoushistoryvalue;
                            //objhistorydetails.userloginhistorydetails_NEWVALUE = newhistoryvalue;
                            //objhistorydetails.userloginhistorydetails_PRIMARYKEY = General.Parse<int>(HttpContext.Current.Session["UpdatePrimaryKey"].ToString());
                            //objhistorydetails.userloginhistorydetails_QUERYTYPE = "UPDATE";
                            //objhistorydetails.userloginhistorydetails_MENUID = HttpContext.Current.Session["UpdateTabID"].ToString();
                            //objhistorydetails.userloginhistorydetails_OPERATIONDATE = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                            //string SQLInsert = "INSERT INTO userloginhistorydetails(ULID, QUERY, QUERYTYPE, OPERATIONDATE, TABLENAME,PRIMARYKEY, PREVIOUSVALUE, NEWVALUE) VALUES (" + objhistorydetails.userloginhistorydetails_ULID + ",\"" + objhistorydetails.userloginhistorydetails_QUERY + "\",\'" + objhistorydetails.userloginhistorydetails_QUERYTYPE + "\',\'" + objhistorydetails.userloginhistorydetails_OPERATIONDATE + "\')";
                            // string SQLInsert = "INSERT INTO userloginhistorydetails(ULID, QUERY, QUERYTYPE, OPERATIONDATE, TABLENAME,PRIMARYKEY, PREVIOUSVALUE, NEWVALUE, MENUID) VALUES (" + objhistorydetails.userloginhistorydetails_ULID + ",\"" + objhistorydetails.userloginhistorydetails_QUERY + "\",\'" + objhistorydetails.userloginhistorydetails_QUERYTYPE + "\',\'" + objhistorydetails.userloginhistorydetails_OPERATIONDATE + "\',\'" + objhistorydetails.userloginhistorydetails_TABLENAME + "\',\'" + objhistorydetails.userloginhistorydetails_PRIMARYKEY + "\',\'" + objhistorydetails.userloginhistorydetails_PREVIOUSVALUE + "\',\'" + objhistorydetails.userloginhistorydetails_NEWVALUE + "\',\'" + objhistorydetails.userloginhistorydetails_MENUID + "\')";
                            //if (InsertData(SQLInsert) > 0)
                            //{
                            //}
                        }
                    }
                }
                else if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session["UserDetailID"] != null && flag == true)
                {

                    //UserLoginHistoryDetails objhistorydetails = new UserLoginHistoryDetails(HttpContext.Current.Server.MapPath("~/XML/1.xml"));
                    //objhistorydetails.userloginhistorydetails_ULID = General.Parse<int>(HttpContext.Current.Session["UserDetailID"].ToString());
                    //objhistorydetails.userloginhistorydetails_ULDID = -1;
                    //objhistorydetails.userloginhistorydetails_QUERY = strinsertSSQL;
                    //objhistorydetails.userloginhistorydetails_QUERYTYPE = "INSERT";
                    //objhistorydetails.userloginhistorydetails_OPERATIONDATE = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    //string SQLInsert = "INSERT INTO userloginhistorydetails(ULID, QUERY, QUERYTYPE, OPERATIONDATE) VALUES (" + objhistorydetails.userloginhistorydetails_ULID + ",\"" + objhistorydetails.userloginhistorydetails_QUERY + "\",\'" + objhistorydetails.userloginhistorydetails_QUERYTYPE + "\',\'" + objhistorydetails.userloginhistorydetails_OPERATIONDATE + "\')";
                    //if (InsertData(SQLInsert) > 0)
                    //{
                    //}
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
            SqlDataAdapter sqla = new SqlDataAdapter(SQLSelect, connection);
            sqla.Fill(dtSelect);
            
            return dtSelect;
        }
        catch (Exception ex)
        {

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

            SqlDataAdapter sqla = new SqlDataAdapter(SQLSelect, connection);
            sqla.Fill(dtSelect);
            
            return dtSelect;
        }
        catch (Exception ex)
        {

        }

        return dtSelect;
    }


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


            SqlDataAdapter sqla = new SqlDataAdapter(SQLSelect, connection);
            sqla.Fill(dtSelect);
            
            return dtSelect;
        }
        catch (Exception ex)
        {

        }

        return dtSelect;
    }


    public DataTable GetTable(string SQLStmtWithJoin)
    {
        //dtSelect.Columns.Clear();
        //dtSelect.Rows.Clear();
        DataTable dtSelect = new DataTable();
        SqlDataAdapter sqla = new SqlDataAdapter(SQLStmtWithJoin, connection);
        
       
        if (SQLStmtWithJoin != string.Empty)
            
            sqla.Fill(dtSelect);
        return dtSelect;
    }

    public DataSet GetDataSet(string SQLStmtWithJoin)
    {

        
        SqlDataAdapter sqla = new SqlDataAdapter(SQLStmtWithJoin, connection);
        if (SQLStmtWithJoin != string.Empty)
           
            sqla.Fill(dtSelect);
        return dsSelect;
    }

    public int InsertData(string SQLStmtWithJoin)
    {
        int i = 0;
        try
        {
          
            SqlConnection sqlconnection = new SqlConnection(connection);
          
            if (sqlconnection.State == ConnectionState.Closed)
            {
                sqlconnection.Open();
            }
           
            SqlCommand cmd = new SqlCommand(SQLStmtWithJoin, sqlconnection);
            i = cmd.ExecuteNonQuery();

            sqlconnection.Close();
        }
        catch (Exception ex)
        {

        }
        return i;
    }

    public bool UpdateExplicit(string SQLUpdate)
    { SqlConnection sqlconnection = new SqlConnection(connection);
       
        using (SqlConnection conn = new SqlConnection(connection))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(SQLUpdate, conn);
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
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = new SqlCommand(SqlStmt, conn);
            conn.Open();
            result = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {

        }
        return result;

    }

    public string ExecuteScalerQuery(string SqlStmt)
    {
        string Result = string.Empty;
        SqlConnection sqlcon = new SqlConnection(connection);
        SqlCommand cmd = new SqlCommand(SqlStmt, sqlcon);
        
        try
        {
            sqlcon.Open();
            object obj = cmd.ExecuteScalar();
            Result = obj.ToString();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            if (sqlcon.State == ConnectionState.Open)
            {
                sqlcon.Close();
            }

            cmd.Dispose();
        }
        return Result;
    }
    #endregion
}