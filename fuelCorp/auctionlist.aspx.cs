using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class auctionlist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("default.aspx");
            }
            DataTable dtcontract = FillContractList();
            ViewState["list"] = dtcontract;
            if (dtcontract.Rows.Count > 0)
            {
                gvcontractlist.DataSource = (DataTable)ViewState["list"];
                gvcontractlist.DataBind();
            }
            else
            {
                gvcontractlist.DataSource = null;
                gvcontractlist.DataBind();
            }
        }
    }
    private DataTable FillContractList()
    {
        string sqlpartylist = "SELECT AM.AUCTIONID,AM.AUCTIONNAME,AM.DESCRIPTION,AM.AUCTIONDATE,AM.STARTDATE,AM.ENDDATE,CASE WHEN STATUS=0 THEN 'ACTIVE' WHEN AM.STATUS=1 THEN 'COMPLETE' ELSE 'CANCEL' END AS STATUS " +
                              " FROM AUCTIONMASTER AM WHERE AM.CMPID=" + Session["cmpid"].ToString();
        Handler hdnpartylist = new Handler();
        DataTable dtpartylist = hdnpartylist.GetTable(sqlpartylist);
        return dtpartylist;
    }
    protected void gvlookup_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvcontractlist.PageIndex = e.NewPageIndex;
        gvcontractlist.DataSource = (DataTable)ViewState["list"];
        gvcontractlist.DataBind();
    }
    protected void lnkdetails_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        Response.Redirect("auctiondetails.aspx?AUCTIONID=" + lnk.CommandArgument.ToString().Trim());
    }
}