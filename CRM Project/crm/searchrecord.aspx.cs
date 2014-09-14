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

public partial class searchrecord : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string id = Request.QueryString["upload"];
            if (id != null)
            {
                MessageBox("Record Uploaded Successfully");
            }

            if (Session["userid"] == null)
            {
                Response.Redirect("register.aspx?login=1");
            }
            txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            ViewState["data"] = FillData(Session["userid"].ToString());
            grvExcelData.DataSource = (DataTable)ViewState["data"];
            grvExcelData.DataBind();
        }
        else
        {
            txtfromdate.Text =  Request.Form[txtfromdate.UniqueID];
            txttodate.Text =  Request.Form[txttodate.UniqueID];
        }
    }
    private DataTable FillData(string id)
    {
        string sql = "SELECT DATE,NAME,CONTACTNO,ACTIVITY,RESULT,REMARK FROM REPORTINGMASTER RM WHERE STATUS=0 AND RELATIONSHIPID=" + id.ToString();
        Handler hdn = new Handler();
        DataTable dt = hdn.GetTable(sql);
        return dt;
    }
    protected void gvlookup_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvExcelData.PageIndex = e.NewPageIndex;
        grvExcelData.DataSource = (DataTable)ViewState["data"];
        grvExcelData.DataBind();
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        string sql = "SELECT DATE,NAME,CONTACTNO,ACTIVITY,RESULT,REMARK FROM REPORTINGMASTER RM WHERE STATUS=0 AND RELATIONSHIPID=" + Session["userid"].ToString()+"";
        if (Request.Form[txtfromdate.UniqueID].ToString() != string.Empty && Request.Form[txttodate.UniqueID].ToString() != string.Empty)
        {
            sql += " AND RM.DATE BETWEEN '" + Request.Form[txtfromdate.UniqueID] + "' AND '" + Request.Form[txttodate.UniqueID] + "'";
            //sql += " AND RM.DATE BETWEEN '" + txtfromdate.Text.Trim() + "' AND '" + txttodate.Text.Trim() + "'";
        }
        if (txtname.Text != string.Empty)
        {
            sql += " AND RM.NAME LIKE '" + txtname.Text.Trim() + "%'";
        }
        Handler hdn = new Handler();
        DataTable dt = hdn.GetTable(sql);
        ViewState["data"] = dt;
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