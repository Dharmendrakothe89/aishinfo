using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class memberlist : System.Web.UI.Page
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
                               " CASE WHEN RT.STATUS=0 THEN 'ACTIVE' ELSE 'DE-ACTIVE' END AS STATUS FROM REGISTRATIONTABLE RT WHERE STATUS=0";
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