using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class godownlist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("default.aspx");
            }
            DataTable dtgown = FillGodownList();
            if (dtgown.Rows.Count > 0)
            {
                gvgodownlist.DataSource = dtgown;
                gvgodownlist.DataBind();
            }
            else
            {
                gvgodownlist.DataSource = null;
                gvgodownlist.DataBind();
            }
        }
        // SELECT TM.TRANSPORTERNAME,TM.TRANSPORTERCODE,TM.TRANTYPE,TM.MOBILENO,TM.EMAILID FROM transportermaster TM WHERE TM.STATUS=0;
    }
    private DataTable FillGodownList()
    {
        string sqlpartylist = "SELECT GM.SRNO AS GODOWNID,GM.GODOWNNAME,GM.CITYNAME,GM.STATENAME,GM.PHONE,GM.EMAIL,GM.ADDRESS FROM godownmaster GM WHERE STATUS=0";
        Handler hdnpartylist = new Handler();
        DataTable dtpartylist = hdnpartylist.GetTable(sqlpartylist);
        return dtpartylist;
    }
    protected void lnkdetails_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        Response.Redirect("depotdetails.aspx?DEPOTID=" + lnk.CommandArgument.ToString().Trim());

    }
}