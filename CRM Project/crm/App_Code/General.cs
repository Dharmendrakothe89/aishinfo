using System;
using System.Data;
using System.Configuration;
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

using System.Collections;
using System.Text;
using System.Xml;
using System.IO;
using System.Globalization;

using System.Net;
using System.Net.Mail;

using System.Net.Configuration;

/// <summary>
/// Summary description for General
/// </summary>
public class General
{
    public General()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parameter">Pass String Vaiable To convert into T</param>
    /// <returns></returns>
    /// 
    public static T Parse<T>(string parameter)
    {
        if (parameter != string.Empty)
            return (T)System.Convert.ChangeType(parameter, Type.GetTypeCode(typeof(T)));
        else
            return (T)System.Convert.ChangeType(0, Type.GetTypeCode(typeof(T)));
    }
   

    //public static int get(params object[] p)
    //{
    //    int sum = 0;
    //    foreach (object o in p)
    //    {
    //        sum += Parse<int>(o.ToString());
    //    }
    //    return sum;
    //}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="FromFormat">dd/MM/yyyy</param>
    /// <param name="ToFormat">yyyy-MM-dd</param>
    /// <returns></returns>
    public static string ConvertDateTime(string value, string FromFormat, string ToFormat)
    {
        try
        {
            DateTime dtout;
            if (DateTime.TryParseExact(value, FromFormat, null, System.Globalization.DateTimeStyles.None, out dtout))
            {
                return dtout.ToString(ToFormat);
            }

            return string.Empty;
        }
        catch (Exception ex)
        {
            throw ex;
            return string.Empty;
        }
    }

    public static Control GetPostBackControl(Page page)
    {
        Control control = null;

        string ctrlname = page.Request.Params.Get("__EVENTTARGET");
        if (ctrlname != null && ctrlname != string.Empty)
        {
            control = page.FindControl(ctrlname);
        }
        else
        {
            foreach (string ctl in page.Request.Form)
            {
                Control c = page.FindControl(ctl);
                if (c is System.Web.UI.WebControls.Button)
                {
                    control = c;
                    break;
                }
            }
        }
        return control;
    }
    public static void ClearSessions()
    {

        try
        {
            //if (HttpContext.Current.Session["userID"] == null)
            if (HttpContext.Current.Session["userinfo"] == null || HttpContext.Current.Session["userID"] == null)
            {
               //HttpContext.Current.Response.Redirect("Default.aspx");
               //HttpContext.Current.Response.Redirect("~/Default.aspx");
                HttpContext.Current.Response.Redirect("~/RedirectPage.htm");
                // HttpContext.Current.Response.Redirect("javascript:parent.change_parent_url('Default.aspx');");
            }
            else
            {
                int TotalSession = HttpContext.Current.Session.Count;
                for (int sessionIndex = 0; sessionIndex < TotalSession; sessionIndex++)
                {
                    for (int IndexAfterRemoved = 0; IndexAfterRemoved < HttpContext.Current.Session.Count; IndexAfterRemoved++)
                    {
                        if (
                            HttpContext.Current.Session.Keys[IndexAfterRemoved] != "userID" 
                            && HttpContext.Current.Session.Keys[IndexAfterRemoved] != "UserDetailID" 
                            && HttpContext.Current.Session.Keys[IndexAfterRemoved] != "EmployeeBranchID" 
                            && HttpContext.Current.Session.Keys[IndexAfterRemoved] != "UID" 
                            && HttpContext.Current.Session.Keys[IndexAfterRemoved] != "g_BranchName" 
                            && HttpContext.Current.Session.Keys[IndexAfterRemoved] != "g_BranchID" 
                            && HttpContext.Current.Session.Keys[IndexAfterRemoved] != "g_City" 
                            && HttpContext.Current.Session.Keys[IndexAfterRemoved] != "g_companyid"
                            && HttpContext.Current.Session.Keys[IndexAfterRemoved] != "g_companyname"
                            && HttpContext.Current.Session.Keys[IndexAfterRemoved] != "userinfo"
                            )
                        {

                            HttpContext.Current.Session.Remove(HttpContext.Current.Session.Keys[IndexAfterRemoved]);
                            break;
                        }

                    }
                }

            }
        }
        catch(Exception ex)
        {
            int Lineno = Convert.ToInt32(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(":line") + 5));
            string ExcType = ex.GetType().Name;
            string strErrorText = ex.Message;

            //"Page:Handler.cs" + Environment.NewLine + "Method:Insert" + Environment.NewLine + "Line No:" + Lineno + Environment.NewLine +
            string strStackTrace = "";
            if (HttpContext.Current != null)
            {
                strStackTrace += "URL: " + HttpContext.Current.Request.Url;
            }
            strStackTrace += "\r\n Message: " + "\r\n ExeptionType: " + ExcType + " \r\n " + ex.Message + " \r\n On General.cs Line no:" + Lineno + Environment.NewLine + " StackTrace: " + ex.StackTrace;
            if (HttpContext.Current != null && HttpContext.Current.Session["userID"] != null)
            {
                strStackTrace = strStackTrace + "\r\n User ID : " + HttpContext.Current.Session["userID"].ToString();
            }
            int rowsAffects = General.InsertTrackOfError(strErrorText, strStackTrace);

        }




    }
    #region Common Method
    public static void MessageBox(Page UIpage, string Message)
    {
        //UIpage.RegisterStartupScript("Messageboxx", "<script>alert('text messagebox');</script>");
        //UIpage.ClientScript.RegisterStartupScript(UIpage.GetType(), "SS", "alert('text messagebox');", true);
        ScriptManager.RegisterStartupScript(UIpage, UIpage.GetType(), "msgbox", "alert('" + Message + "');", true);
    }
    public static void Hidedivs(Page UIpage,params string[] divIds)
    {
        int total = 1;
        foreach (string str in divIds)
        {
            ScriptManager.RegisterStartupScript(UIpage, UIpage.GetType(), "hide_" + str, "hideModal('" + str + "');", true);
            total++;
        }
    }
    public static void Showdivs(Page UIpage,params string[] divIds)
    {
        int total = 1;
        foreach (string str in divIds)
        {
            ScriptManager.RegisterStartupScript(UIpage, UIpage.GetType(), "show_" + str, "revealModal('" + str + "');", true);
            total++;
        }
    }
    public static void ExceptionforModelPopup(Exception ex, Page UIpage)
    {
        //ErrorLog err = new ErrorLog();
        string strStacktrace = ex.StackTrace.ToString();


        HttpContext ctx = HttpContext.Current;
        //int httpCode = ((HttpException)ex).GetHttpCode();


        // get the IP Address
        String strHostName = string.Empty;
        String ipAddress_s = string.Empty;
        strHostName = System.Net.Dns.GetHostName();

        System.Net.IPHostEntry ipEntry = System.Net.Dns.GetHostByName(strHostName);
        System.Net.IPAddress[] addr = ipEntry.AddressList;

        for (int i = 0; i < addr.Length; i++)
        {
            ipAddress_s += "IP Address {" + (i + 1) + "} " +
                                addr[i].ToString() + Environment.NewLine;
        }
        if (HttpContext.Current.Session != null && HttpContext.Current.Session["userID"] != null)
        {
            strStacktrace = strStacktrace + "User ID :" + HttpContext.Current.Session["userID"].ToString() + " ip address: " + ipAddress_s;
        }
        else
        {
            strStacktrace = strStacktrace + " ip address: " + ipAddress_s;
        }

        string strTryCatch = ex.Message.ToString();
        strTryCatch = strTryCatch.Replace("'", " ");
        strStacktrace = strStacktrace.Replace("'", " ");

        //err.CreateErrorLog(strTryCatch, strStacktrace);

        Handler objhandler = new Handler();
        string strDateNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        string SQLinsertQuery = "Insert into Logs (ERRORTEXT,STACKTRACE,DATETIME) values ('" + strTryCatch + "','" + strStacktrace + "','" + strDateNow + "')";
        objhandler.InsertData(SQLinsertQuery);

        System.Data.DataTable dt = objhandler.GetTable("SELECT MAX(SRNO) FROM logs where DATETIME= '" + strDateNow + "'");

        //UIpage.ClientScript.RegisterStartupScript(UIpage.GetType(), "alertmsg", "alert('Sorry for inconvenience, There was an error(" + dt.Rows[0][0].ToString()+ ")  while performing your request.');", true);
        ScriptManager.RegisterStartupScript(UIpage, UIpage.GetType(), "msgbox", "alert('Sorry for inconvenience, There was an error(" + dt.Rows[0][0].ToString() + ")  while performing your request.');", true);
    }

    #endregion


    /// <summary>
    ///  
    /// </summary>
    /// <param name="TableName">Name of the table in which insertion failed</param>
    /// <param name="operation">while inserting or updating or else other</param>
    public static void InsertTrackOfFail(string TableName,string operation)
    {
        try
        {
            Handler objerror = new Handler();
            int NoOfRowAffected = objerror.InsertData("INSERT INTO LOGS(ERRORTEXT, STACKTRACE, DATETIME) VALUES('" + operation + " IN TABLE NAMED " + TableName + " IS FAILED','NOT SPECIFIED','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "')");
        }
        catch
        {

            throw;
        }
    }
    public static int InsertTrackOfError(string strErrortext, string strStackttrace)
    {
        strErrortext = strErrortext.Replace("'", "\\'");
        strStackttrace = strStackttrace.Replace("'", "\\'");
        Handler objerror = new Handler();
        return objerror.InsertData("INSERT INTO LOGS(ERRORTEXT, STACKTRACE, DATETIME) VALUES('" + strErrortext + "','" + strStackttrace + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");
    }
    public static void AddToolTip(DropDownList lst)
    {
        foreach (ListItem curItem in lst.Items)
        {
            curItem.Attributes.Add("title", curItem.Text);
        }
    }
    public static string GetIP()
    {
        string strHostName = "";
        strHostName = System.Net.Dns.GetHostName();

        IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);

        IPAddress[] addr = ipEntry.AddressList;

        return addr[addr.Length - 1].ToString();

        //HttpRequest currentRequest = HttpContext.Current.Request;
        //string ipAddress = currentRequest.ServerVariables["HTTP_X_FORWARDED_FOR"];

        //if (ipAddress == null || ipAddress.ToLower() == "unknown")
        //    ipAddress = currentRequest.ServerVariables["REMOTE_ADDR"];

        //return ipAddress;

    }

    public static string GetConfigAppSettingValue(string Key)
    {
        string value = string.Empty;
        if (ConfigurationManager.AppSettings[Key] != null)
        {
            value = ConfigurationManager.AppSettings[Key].ToString();
        }
        return value;

    }

    #region Mail/SMS

    public static void SendMail(string smsemailid, string userName, string userPassword, string userHost, int userPort, string subject, string body, string toAddress, string cc, string bcc)
    {
        if (!String.IsNullOrEmpty(toAddress))
        {

            body = body + "<br/><br /><br /><br /><br /><div><b>This is the system generated e-mail.</b></div>";

            //SmtpSection smtpsetting = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            SmtpSection smtpsetting = new SmtpSection();
            smtpsetting.From = userName;
            smtpsetting.Network.UserName = userName;
            smtpsetting.Network.Password = userPassword;
            smtpsetting.Network.Port = userPort;
            smtpsetting.Network.Host = userHost;

            string fromAddress = smtpsetting.From;

            //MailMessage message = new MailMessage(fromAddress, toAddress);
            MailMessage message = new MailMessage();

            message.From = new MailAddress(fromAddress, "Enquiry");
            message.To.Add(toAddress);
            var smtp = new System.Net.Mail.SmtpClient();
            {

                smtp.Host = smtpsetting.Network.Host;
                smtp.Port = smtpsetting.Network.Port;
                smtp.EnableSsl = true;
                smtp.Timeout = 40000;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                //smtp.Credentials = new NetworkCredential(smtpsetting.Network.UserName, Crypto.Decrypt(smtpsetting.Network.Password, Crypto.Key));
                smtp.Credentials = new NetworkCredential(smtpsetting.Network.UserName, smtpsetting.Network.Password);
                message.Subject = subject;
                body = body.Replace("&NBSP;", "&nbsp;");
                message.Body = body;

                message.IsBodyHtml = true;
                if (!String.IsNullOrEmpty(cc))
                {
                    string[] arrCC = cc.Split(',');
                    for (int i = 0; i < arrCC.Length; i++)
                    {
                        message.CC.Add(new MailAddress(arrCC[i]));
                    }
                }
                if ((bcc != null) && (bcc != string.Empty))
                {
                    message.Bcc.Add(new MailAddress(bcc));
                }

            }

            string paths = HttpContext.Current.Request.PhysicalApplicationPath;

            paths += "/Uploads/Attachments/Mail/" + smsemailid;
            if (String.IsNullOrEmpty(smsemailid))
            {
                paths = string.Empty;
            }
            /*if (System.IO.File.Exists(paths))
            {
                Attachment itm = new Attachment(paths);
                message.Attachments.Add(itm);
            }*/

            if (paths != string.Empty && System.IO.Directory.Exists(paths))
            {
                string[] strFileName = System.IO.Directory.GetFiles(paths);
                if (strFileName.Length > 0)
                {

                    /*foreach (string str in strFileName)
                    {
                        Attachment  itm = new Attachment(str);
                        message.Attachments.Add(itm);

                    }*/
                    //Attachment  itm = new Attachment(strFileName[0]);
                    Attachment[] arritm = new Attachment[strFileName.Length];
                    for (int i = 0; i < strFileName.Length; i++)
                    {
                        //itm = new Attachment(strFileName[i]);
                        //itm.Name=strFileName[i]);
                        //message.Attachments.Add(itm);
                        //itm.Dispose();

                        arritm[i] = new Attachment(strFileName[i]);
                    }
                    foreach (Attachment Att in arritm)
                    {
                        message.Attachments.Add(Att);
                    }
                    smtp.Send(message);
                    //itm.Dispose();
                    for (int i = 0; i < arritm.Length; i++)
                    {

                        arritm[i].Dispose();
                    }
                }
                else
                    smtp.Send(message);

            }
            else
                smtp.Send(message);




            ///////////////Delete Folder////////////////////
            if (paths != string.Empty && System.IO.Directory.Exists(paths))
            {
                string[] strFileName = System.IO.Directory.GetFiles(paths);
                foreach (string str in strFileName)
                {
                    System.IO.File.Delete(str);
                }
                System.IO.Directory.Delete(paths);
            }
            /////////////////////////////////////////////////

        }
    }

    #endregion

    #region lookUp
    public static string GetStringFromlookup(string strValue,string strcolumnName)
    {
        string strStatus = "";
        DataSet ds = new DataSet();
        ds.ReadXml(HttpContext.Current.Server.MapPath("~/XML/lookup.xml"));

        DataRow[] arrDr = ds.Tables[1].Select("name='" + strcolumnName + "'");

        foreach (DataRow dr in arrDr)
        {
            if (dr["value"].ToString() == strValue)
            {
                strStatus = dr["text"].ToString();
            }

        }
        return strStatus;
    }
   
    /// <summary>
    /// Get Value from Lookup xml just pass table name and column name get value of that column 
    /// (if multiple column then return value in separated by comm)
    /// </summary>
    /// <param name="tableName">Table name field in xml</param>
    /// <param name="colmName">Name field in column </param>
    /// <param name="outputColName">Output field name inside Table in xml</param>
    /// <returns></returns>
    public static string GetLookupAllValues(string tableName, string colmName,string outputColName)
    {
        System.Data.DataSet dsLookUp1 = new System.Data.DataSet();
        string xmlPath = string.Empty;
        string cc = string.Empty;
        

        string strConn = ConfigurationManager.ConnectionStrings["db_phoenixConnectionString"].ToString();
        string[] arrCon = strConn.Split(';');
        if (arrCon[1].Contains("192.168.1.2"))
        {
            xmlPath = System.Web.Hosting.HostingEnvironment.MapPath("~/XML/lookup.xml");
        }
        else
        {
            xmlPath = System.Web.Hosting.HostingEnvironment.MapPath("~/XML/lookuplocal.xml");
        }
        dsLookUp1.ReadXml(xmlPath);
            DataRow[] arrMamgtTable = dsLookUp1.Tables[0].Select("name='" + tableName + "'");
            if (arrMamgtTable.Length > 0)
            {
                DataRow[] arrTableColm = dsLookUp1.Tables[1].Select("table_Id=" + arrMamgtTable[0]["table_Id"].ToString() + " AND name='" + colmName + "'");

                foreach (DataRow dr in arrTableColm)
                {
                    if (cc == string.Empty)
                        cc = dr[outputColName].ToString();
                    else
                        cc = cc + "," + dr[outputColName].ToString();
                }


            }
        
        /*
        else
        {
            DataRow[] arrMamgtTable = dsLookUp1.Tables[0].Select("name='itteamtesting'");
            if (arrMamgtTable.Length > 0)
            {
                DataRow[] arrTableColm = dsLookUp1.Tables[1].Select("table_Id=" + arrMamgtTable[0]["table_Id"].ToString() + " AND role='true'");

                foreach (DataRow dr in arrTableColm)
                {
                    if (cc == string.Empty)
                        cc = dr["value"].ToString();
                    else
                        cc = cc + "," + dr["value"].ToString();
                }


            }
        }*/
        return cc;

    }

    #endregion

    #region Taxes
    /// <summary>
    /// Get Field value from taxes.xml
    /// </summary>
    /// <returns></returns>
    public static string GetValuefromTaxesXML(string inputColName, string inputcolValue, string OutColName)
    {
        string ColmName = string.Empty;
        System.Data.DataSet dsLookUp1 = new System.Data.DataSet();
        string xmlPath = System.Web.Hosting.HostingEnvironment.MapPath("~/XML/taxes.xml");

        dsLookUp1.ReadXml(xmlPath);
        DataRow[] arrMamgtTable = dsLookUp1.Tables[0].Select("yr='2012-2013'");
        if (arrMamgtTable.Length > 0)
        {
            //DataRow[] arrTableColm = dsLookUp1.Tables[1].Select("tax_Id=" + arrMamgtTable[0]["tax_Id"].ToString() + " AND id='" + Taxid + "'");
            string condition = "tax_Id=" + arrMamgtTable[0]["tax_Id"].ToString();

            //if we want all value(of column) then inputCol and inputColValue is pass Empty
            if (!String.IsNullOrEmpty(inputColName) && !String.IsNullOrEmpty(inputColName))
                condition += " AND " + inputColName + "='" + inputcolValue + "'";

            //DataRow[] arrTableColm = dsLookUp1.Tables[1].Select("tax_Id=" + arrMamgtTable[0]["tax_Id"].ToString() + " AND " + inputColName + "='" + inputcolValue + "'");
            DataRow[] arrTableColm = dsLookUp1.Tables[1].Select(condition);

                foreach (DataRow dr in arrTableColm)
                {
                    if (String.IsNullOrEmpty(inputColName) && String.IsNullOrEmpty(inputColName) && OutColName == "abbr")
                    {
                        if (ColmName == string.Empty)
                            ColmName = dr[OutColName].ToString() + ": " + dr["name"].ToString();
                        else
                            ColmName = ColmName + ", " + dr[OutColName].ToString() + ": " + dr["name"].ToString();
                    }
                    else
                    {
                        if (ColmName == string.Empty)
                            ColmName = dr[OutColName].ToString();
                        else
                            ColmName = ColmName + "," + dr[OutColName].ToString();
                    }
                }
            
        }
        return ColmName;

    }

    /// <summary>
    /// Get Formulas from taxes.xml
    /// </summary>
    /// <returns></returns>
    public static string GetFormulafromTaxesXML(string Taxid,string TaxUnit,string TaxSequence)
    {
        string ColmName = string.Empty;
        System.Data.DataSet dsLookUp1 = new System.Data.DataSet();
        string xmlPath = System.Web.Hosting.HostingEnvironment.MapPath("~/XML/taxes.xml");

        dsLookUp1.ReadXml(xmlPath);
        DataRow[] arrMamgtTable = dsLookUp1.Tables[0].Select("yr='2012-2013'");
        if (arrMamgtTable.Length > 0)
        {
            //DataRow[] arrTableColm = dsLookUp1.Tables[1].Select("tax_Id=" + arrMamgtTable[0]["tax_Id"].ToString() + " AND id='" + Taxid + "'");
            string condition = "tax_Id=" + arrMamgtTable[0]["tax_Id"].ToString();
                condition += " AND id='" + Taxid + "'";

            //DataRow[] arrTableColm = dsLookUp1.Tables[1].Select("tax_Id=" + arrMamgtTable[0]["tax_Id"].ToString() + " AND " + inputColName + "='" + inputcolValue + "'");
            DataRow[] arrTableColm = dsLookUp1.Tables[1].Select(condition);

            if (!String.IsNullOrEmpty(TaxUnit))
            {
                string str = "column_Id=" + arrTableColm[0]["column_Id"].ToString() + " AND text='" + TaxUnit + "'" + " AND sequence='" + TaxSequence + "'";
                DataRow[] arrFormula = dsLookUp1.Tables[2].Select("column_Id=" + arrTableColm[0]["column_Id"].ToString() + " AND text='" + TaxUnit + "'" + " AND ( sequence='" + TaxSequence + "' OR sequence='')");
                foreach (DataRow dr in arrFormula)
                {
                    if (ColmName == string.Empty)
                        ColmName = dr["value"].ToString();
                    else
                        ColmName = ColmName + "," + dr["value"].ToString();

                }

            }
            
        }
        return ColmName;

    }

    #endregion

   public static DataTable GetXMLDataTable(string xmlPath,string tableName)
    {
        
        DataSet ds  = new DataSet();
        xmlPath = System.Web.Hosting.HostingEnvironment.MapPath("~/XML/lookup.xml");
        ds.ReadXml(xmlPath);
        DataTable dt = ds.Tables[1].Clone();
        DataRow[] arrMamgtTable = ds.Tables[0].Select("name='" + tableName + "'");
        string str = string.Empty;
        if (arrMamgtTable.Length > 0)
        {
            DataRow[] arrTableColm = ds.Tables[1].Select("table_Id=" + arrMamgtTable[0]["table_Id"].ToString());


            dt = arrTableColm.CopyToDataTable();
        }
        return dt;
       
    }


}
