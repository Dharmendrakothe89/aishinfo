using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class userlist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            DataTable dtcontract = FillMemberList();
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
    private DataTable FillMemberList()
    {
        string sqlpartylist = "SELECT RT.SRNO,RT.NAME,RT.SPONSORNAME,RT.SPONSORSEMICODE,RT.SEMICODE,RT.EMAILID,RT.PHONENO," +
                               " CASE WHEN RT.STATUS=0 THEN 'ACTIVE' ELSE 'DE-ACTIVE' END AS STATUS,RT.STATUS AS ACTIVATE FROM REGISTRATIONTABLE RT WHERE RT.STATUS=0 OR RT.STATUS=1";
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
    protected void lnkdetails_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        if (lnk.CommandName == "0")
        {
            registrationtable obj = new registrationtable(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
            obj.registrationtable_SRNO = -1;
            obj.registrationtable_SPONSORID = -1;
            obj.registrationtable_STATUS = 1;
            string condition = "SRNO=" + lnk.CommandArgument.ToString();
            if (obj.Insert(false, "registrationtable", condition))
            {
            }
        }
        else if (lnk.CommandName == "1")
        {
            registrationtable obj = new registrationtable(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
            obj.registrationtable_SRNO = -1;
            obj.registrationtable_SPONSORID = -1;
            obj.registrationtable_STATUS = 0;
            string condition = "SRNO=" + lnk.CommandArgument.ToString();
            if (obj.Insert(false, "registrationtable", condition))
            {
            }
        }
        DataTable dtcontract = FillMemberList();
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
    protected void gvmemberlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {   
        DataRowView drview = e.Row.DataItem as DataRowView;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton active = (LinkButton)e.Row.FindControl("lnkactivate");
            LinkButton deactive = (LinkButton)e.Row.FindControl("lnkdeactivate");
            if (active.CommandName.ToString() == "0")
            {
                active.Visible = false;
                deactive.Visible = true;
            }
            else
            {
                active.Visible = true;
                deactive.Visible = false;
            }
            
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