using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Configuration;
public partial class uploadexcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("register.aspx?login=1");
            }
            txtdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            grvExcelData.DataSource = null;
            grvExcelData.DataBind();
        }
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        ;
        if (fileuploadExcel.HasFile)
        {
            string FileName = Path.GetFileName(fileuploadExcel.PostedFile.FileName);
            string Extension = Path.GetExtension(fileuploadExcel.PostedFile.FileName);
            string FolderPath = System.Configuration.ConfigurationManager.AppSettings["FolderPath"];
            lock (this)
            {
                FileInfo TheFileInfo = new FileInfo("reporting.xlsx");
                if (TheFileInfo.Exists)
                {
                    File.Delete("reporting.xlsx");
                }
                fileuploadExcel.SaveAs(Server.MapPath("reporting.xlsx"));

            }

            //string FilePath = Server.MapPath(FolderPath + FileName);
            string FilePath = Server.MapPath(FolderPath + "reporting.xlsx");
            DataSet ds = ImportExcelXLS(FilePath, true);
            DataTable dt = ds.Tables[0];
            if (dt.Columns.Count == 6 && dt.Columns[1].ColumnName.ToUpper() == "NAME OF PROSPECT" && dt.Columns[2].ColumnName.ToUpper() == "CONTACT NUMBER" && dt.Columns[3].ColumnName.ToUpper() == "ACTIVITY" && dt.Columns[4].ColumnName.ToUpper() == "RESULT" && dt.Columns[5].ColumnName.ToUpper() == "REMARKS")
            {
                dt.Columns[0].ColumnName = "SRNO";
                dt.Columns[1].ColumnName = "NAME";
                dt.Columns[2].ColumnName = "CONTACTNO";
                dt.Columns[3].ColumnName = "ACTIVITY";
                dt.Columns[4].ColumnName = "RESULT";
                dt.Columns[5].ColumnName = "REMARK";
                ViewState["data"] = (DataTable)ds.Tables[0];
                
            }
            else
            {
                MessageBox("Please Select Proper File");
                ViewState["data"] = null;
               
            }
            grvExcelData.DataSource = (DataTable)ViewState["data"];
            grvExcelData.DataBind();
        }
        else
        {
            MessageBox("Please Select File");
        }
        btnImport.Enabled = true;
    }
    public static DataSet ImportExcelXLS(string FileName, bool hasHeaders)
    {

        string HDR = hasHeaders ? "Yes" : "No";
        string strConn;
        if (FileName.Substring(FileName.LastIndexOf('.')).ToLower() == ".xlsx")
            strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=1\"";
        else
            strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FileName + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=1\"";

        DataSet output = new DataSet();

        using (OleDbConnection conn = new OleDbConnection(strConn))
        {
            conn.Open();

            DataTable schemaTable = conn.GetOleDbSchemaTable(
                OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

            foreach (DataRow schemaRow in schemaTable.Rows)
            {
                string sheet = schemaRow["TABLE_NAME"].ToString();

                if (!sheet.EndsWith("_"))
                {
                    try
                    {
                        OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + sheet + "]", conn);
                        cmd.CommandType = CommandType.Text;

                        DataTable outputTable = new DataTable(sheet);
                        output.Tables.Add(outputTable);
                        new OleDbDataAdapter(cmd).Fill(outputTable);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message + string.Format("Sheet:{0}.File:F{1}", sheet, FileName), ex);
                    }
                }
            }
        }
        return output;
    }
    protected void gvlookup_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvExcelData.PageIndex = e.NewPageIndex;
        grvExcelData.DataSource = (DataTable)ViewState["data"];
        grvExcelData.DataBind();
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (ViewState["data"] != null)
        {
            if (((DataTable)ViewState["data"]).Rows.Count > 0)
            {
                DataTable dt = (DataTable)ViewState["data"];
                ViewState["data"] = null;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0].ToString().Trim() != string.Empty)
                    {
                        reportingmaster obj = new reportingmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                        obj.reportingmaster_SRNO = -1;
                        obj.reportingmaster_DATE = txtdate.Text.Trim();
                        obj.reportingmaster_NAME = dt.Rows[i][1].ToString();
                        obj.reportingmaster_CONTACTNO = dt.Rows[i][2].ToString();
                        obj.reportingmaster_ACTIVITY = dt.Rows[i][3].ToString();
                        obj.reportingmaster_RESULT = dt.Rows[i][4].ToString();
                        obj.reportingmaster_REMARK = dt.Rows[i][5].ToString();
                        obj.reportingmaster_RELATIONSHIPID = General.Parse<int>(Session["userid"].ToString());
                        obj.reportingmaster_STATUS = 0;
                        if (obj.Insert(true, "reportingmaster"))
                        {
                            dt.Rows[i].Delete();

                        }
                    }
                }
                Response.Redirect("searchrecord.aspx?upload=1");
            }
        }
        else
        {
            MessageBox("Please Upload Record");
        }
        btnsubmit.Enabled = true;
    }
    protected void btnaddrecord_Click(object sender, EventArgs e)
    {
        DataTable dt1 = new DataTable();
        if (ViewState["data"] == null)
        {
            dt1.Columns.Add("SRNO");
            dt1.Columns.Add("NAME");
            dt1.Columns.Add("CONTACTNO");
            dt1.Columns.Add("ACTIVITY");
            dt1.Columns.Add("RESULT");
            dt1.Columns.Add("REMARK");
          
        }
        else
        {
            dt1 = (DataTable)ViewState["data"];
            ViewState["data"] = null;
        }
        int count = dt1.Rows.Count;
        dt1.Rows.Add();
        dt1.Rows[count]["SRNO"] = (count + 1).ToString();
        dt1.Rows[count]["NAME"] = txtprospect.Text.Trim().ToString();
        dt1.Rows[count]["CONTACTNO"] = txtcontactno.Text.Trim().ToString();
        dt1.Rows[count]["ACTIVITY"] = txtactivity.Text.Trim().ToString();
        dt1.Rows[count]["RESULT"] = txtresult.Text.Trim().ToString();
        dt1.Rows[count]["REMARK"] = txtremark.Text.Trim().ToString();

        ViewState["data"] = dt1;
        grvExcelData.DataSource = (DataTable)ViewState["data"];
        grvExcelData.DataBind();
    }
    public void MessageBox(string msg)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + msg + "');", true);
        }
        catch
        {
            throw;
        }
    }
}