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
    string relationshipid = string.Empty;
    string downline = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            //if (Session["downline"] == null)
            {
                string sql = "SELECT UT.SRNO AS RELATIONSHIPID,RT.SRNO,RT.NAME AS USERNAME,CONCAT(RT.NAME,'-',RT.SEMICODE) AS NAME FROM REGISTRATIONTABLE RT INNER JOIN USERTABLE UT ON RT.SRNO=UT.RELATIONSHIPID WHERE RT.SPONSORID=" + Session["relationshipid"].ToString();
                Handler hdn = new Handler();
                DataTable dt = hdn.GetTable(sql);
                relationshipid = Session["relationshipid"].ToString();
                downline = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //if (i > 0)
                    {
                        if (downline != string.Empty)
                        {
                            downline += "," + dt.Rows[i]["RELATIONSHIPID"].ToString();
                        } 
                        else
                        {
                            downline = dt.Rows[i]["RELATIONSHIPID"].ToString();
                        }
                    }
                    string sql1 = "SELECT UT.SRNO AS RELATIONSHIPID,RT.SRNO,RT.NAME AS USERNAME,CONCAT(RT.NAME,'-',RT.SEMICODE) AS NAME FROM REGISTRATIONTABLE RT INNER JOIN USERTABLE UT ON RT.SRNO=UT.RELATIONSHIPID WHERE RT.SPONSORID=" + dt.Rows[i]["SRNO"].ToString().Trim();
                    Handler hdn1 = new Handler();
                    DataTable dt1 = hdn1.GetTable(sql1);
                    dt.Merge(dt1);
                }
                ViewState["relationshipid"] = relationshipid;
                ViewState["downline"] = downline;
                Session["downline"] = dt;

            }

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
        string sql = "SELECT DATE,NAME,CONTACTNO,ACTIVITY,RESULT,REMARK FROM REPORTINGMASTER RM WHERE STATUS=0 ";
        if(ddltype.SelectedValue.ToString()  == "1")
        {
            sql += "AND RELATIONSHIPID=" + ViewState["relationshipid"].ToString();
        }
        else if (ddltype.SelectedValue.ToString() == "2" && ViewState["downline"].ToString() != string.Empty)
        {
            sql += "AND RELATIONSHIPID IN ( " + ViewState["downline"].ToString() + ")";
        }
        else
        {
            if (ViewState["downline"].ToString() != string.Empty)
            {
                sql += "AND RELATIONSHIPID IN (" + (ViewState["relationshipid"].ToString() + "," + ViewState["downline"].ToString()) + ")";
            }
            else
            {
                sql += "AND RELATIONSHIPID=" + ViewState["relationshipid"].ToString();
            }
        }
        
        if (Request.Form[txtfromdate.UniqueID].ToString() != string.Empty && Request.Form[txttodate.UniqueID].ToString() != string.Empty)
        {
            sql+= " AND str_to_date(RM.DATE,'%d/%m/%Y') BETWEEN  str_to_date('"+Request.Form[txtfromdate.UniqueID]+"','%d/%m/%Y') AND str_to_date('"+Request.Form[txttodate.UniqueID]+"','%d/%m/%Y')";
            // sql += " AND RM.DATE BETWEEN '" + Request.Form[txtfromdate.UniqueID] + "' AND '" + Request.Form[txttodate.UniqueID] + "'";
            //sql += " AND RM.DATE BETWEEN '" + txtfromdate.Text.Trim() + "' AND '" + txttodate.Text.Trim() + "'";
        }
        if (txtname.Text != string.Empty)
        {
            sql += " AND RM.NAME LIKE '" + txtname.Text.Trim() + "%'";
        }
        if (ddltype.SelectedValue.ToString() == "2" && ViewState["downline"].ToString() == string.Empty)
        {
         
            ViewState["data"] = null;
            grvExcelData.DataSource = null;
            grvExcelData.DataBind();
        }
        else
        {
            Handler hdn = new Handler();
            DataTable dt = hdn.GetTable(sql);
            ViewState["data"] = dt;
            grvExcelData.DataSource = (DataTable)ViewState["data"];
            grvExcelData.DataBind();
        }
        
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