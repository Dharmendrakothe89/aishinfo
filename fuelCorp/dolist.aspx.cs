using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class dolist : System.Web.UI.Page
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
            if (dtcontract.Rows.Count > 0)
            {
                gvdolist.DataSource = dtcontract;
                gvdolist.DataBind();
            }
            else
            {
                gvdolist.DataSource = null;
                gvdolist.DataBind();
            }
        }
    }
    private DataTable FillContractList()
    {
        string sqlpartylist = "SELECT DM.DOID,DM.DOFOR,DM.DONO,DM.DODATE,DM.MONTH,DM.DESTINATION,CASE WHEN DM.DOFOR='AUCTION' THEN AM.AUCTIONNAME ELSE PM.PARTYNAME END AS PARTY FROM DOMASTER DM"+
                              " LEFT OUTER JOIN PARTYMASTER PM ON PM.SRNO=DM.PARTYID LEFT OUTER JOIN AUCTIONMASTER AM ON AM.AUCTIONID=DM.PARTYID"+
                              " WHERE DM.CMPID=" + Session["cmpid"].ToString();
        Handler hdnpartylist = new Handler();
        DataTable dtpartylist = hdnpartylist.GetTable(sqlpartylist);
        return dtpartylist;
    }
    protected void lnkdetails_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        Response.Redirect("dodetails.aspx?DOID=" + lnk.CommandArgument.ToString().Trim());
    }
  
}