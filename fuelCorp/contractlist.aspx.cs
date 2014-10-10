using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class contractlist : System.Web.UI.Page
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
                gvcontractlist.DataSource = dtcontract;
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
        string sqlpartylist = "SELECT CM.CONTRACTID,PM.PARTYNAME,CM.CONTRACTTYPE, CM.STARTDATE,CM.ENDDATE,CAST(CM.QUANTITY AS VARCHAR(20)) +' '+CM.QUANTITYPER AS QUANTITY," +
                              " CM.RATE,CM.SERVICECHARGE,CASE WHEN CM.STATUS='0' THEN 'ACTIVE' ELSE 'COMPLETE' END AS STATUS FROM CONTRACTMASTER CM " +
                              " INNER JOIN PARTYMASTER PM ON PM.SRNO=CM.PARTYID ";
        Handler hdnpartylist = new Handler();
        DataTable dtpartylist = hdnpartylist.GetTable(sqlpartylist);
        return dtpartylist;
    }

    protected void lnkdetails_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        Response.Redirect("contractdetails.aspx?CONTRACTID=" + lnk.CommandArgument.ToString().Trim());
    }
}