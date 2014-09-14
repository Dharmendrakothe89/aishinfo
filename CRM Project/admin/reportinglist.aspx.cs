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
            DataTable dtcontract = FillMemberList(txtfromdate.Text.Trim(), txttodate.Text.Trim());
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
    private DataTable FillMemberList(string fromdate,string todate)
    {
        string sqlpartylist = "SELECT * FROM REPORTINGMASTER RM WHERE STATUS=0 AND RELATIONSHIPID=" + Session["userid"].ToString() + " AND DATE BETWEEN '" + fromdate + "' AND '" + todate + "'";
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
        DataTable dtcontract = FillMemberList(txtfromdate.Text.Trim(), txttodate.Text.Trim());
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