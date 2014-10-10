using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class reportinglist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            if (Session["userid"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                DataTable dtcontract = FillMemberList(txtfromdate.Text.Trim(), txttodate.Text.Trim(), txtname.Text.Trim());
                ViewState["list"] = dtcontract;
                if (dtcontract.Rows.Count > 0)
                {
                    gvmemberlist.DataSource = (DataTable)ViewState["list"];
                    gvmemberlist.DataBind();
                }
                else
                {
                    gvmemberlist.DataSource = null;
                    gvmemberlist.DataBind();
                }
            }
        }
    }
    private DataTable FillMemberList(string fromdate,string todate,string name)
    {
        
        string sqlpartylist = "SELECT * FROM REPORTINGMASTER RM WHERE STATUS=0 " ;
        if (fromdate != string.Empty && todate != string.Empty)
        {
            sqlpartylist += " AND DATE BETWEEN '" + fromdate + "' AND '" + todate + "'";
        }
        if (name != string.Empty)
        {
            sqlpartylist += " AND RM.NAME LIKE '" + name.ToString().Trim() + "%'";
        }
        Handler hdnpartylist = new Handler();
        DataTable dtpartylist = hdnpartylist.GetTable(sqlpartylist);
        return dtpartylist;
    }
    protected void gvlookup_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvmemberlist.PageIndex = e.NewPageIndex;
        gvmemberlist.DataSource = (DataTable)ViewState["list"];
        gvmemberlist.DataBind();
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        DataTable dtcontract = FillMemberList(txtfromdate.Text.Trim(), txttodate.Text.Trim(), txtname.Text.Trim());
        ViewState["list"] = dtcontract;
        if (dtcontract.Rows.Count > 0)
        {
            gvmemberlist.DataSource = (DataTable)ViewState["list"];
            gvmemberlist.DataBind();
        }
        else
        {
            gvmemberlist.DataSource = null;
            gvmemberlist.DataBind();
        }
    }

    protected void lnkdetails_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        ViewState["srno"] = lnk.CommandArgument;
        string sql = "SELECT * FROM REPORTINGMASTER RM WHERE SRNO=" + ViewState["srno"].ToString();
        Handler hdn = new Handler();
        DataTable dt = hdn.GetTable(sql);
        txtprospect.Text = dt.Rows[0]["NAME"].ToString();
        txtcontactno.Text = dt.Rows[0]["CONTACTNO"].ToString();
        txtactivity.Text = dt.Rows[0]["ACTIVITY"].ToString();
        txtresult.Text = dt.Rows[0]["RESULT"].ToString();
        txtremark.Text = dt.Rows[0]["REMARK"].ToString();
        
    }
    protected void lnkdelete_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        reportingmaster obj = new reportingmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
        obj.reportingmaster_SRNO = -1;
        obj.reportingmaster_RELATIONSHIPID = -1;
        obj.reportingmaster_STATUS = 1;
        string condition = "SRNO=" + lnk.CommandArgument.ToString();
        if (obj.Insert(false, "reportingmaster", condition))
        {
            DataTable dtcontract = FillMemberList(txtfromdate.Text.Trim(), txttodate.Text.Trim(), txtname.Text.Trim());
            ViewState["list"] = dtcontract;
            if (dtcontract.Rows.Count > 0)
            {
                gvmemberlist.DataSource = (DataTable)ViewState["list"];
                gvmemberlist.DataBind();
            }
            else
            {
                gvmemberlist.DataSource = null;
                gvmemberlist.DataBind();
            }
        }
    }
    protected void btnaddrecord_Click(object sender, EventArgs e)
    {
       reportingmaster obj = new reportingmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
        obj.reportingmaster_SRNO = -1;
        obj.reportingmaster_NAME = txtprospect.Text.Trim();
        obj.reportingmaster_CONTACTNO = txtcontactno.Text.Trim();
        obj.reportingmaster_ACTIVITY = txtactivity.Text.Trim();
        obj.reportingmaster_RESULT = txtresult.Text.Trim();
        obj.reportingmaster_REMARK = txtremark.Text.Trim();
        obj.reportingmaster_RELATIONSHIPID = -1;
        obj.reportingmaster_STATUS = -1;
        string condition = "SRNO=" + ViewState["srno"].ToString();
        if (obj.Insert(false, "reportingmaster", condition))
        {
            DataTable dtcontract = FillMemberList(txtfromdate.Text.Trim(), txttodate.Text.Trim(), txtname.Text.Trim());
            ViewState["list"] = dtcontract;
            if (dtcontract.Rows.Count > 0)
            {
                gvmemberlist.DataSource = (DataTable)ViewState["list"];
                gvmemberlist.DataBind();
            }
            else
            {
                gvmemberlist.DataSource = null;
                gvmemberlist.DataBind();
            }
        }
    }
}