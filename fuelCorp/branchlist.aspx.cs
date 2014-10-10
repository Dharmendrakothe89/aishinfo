using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class branchlist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("default.aspx");
            }
            string id = Request.QueryString["id"];
            if (id != null)
            {

                if (Request.QueryString["id"].ToString() == "1")
                {
                    MessageBox("Branch Created Successsfully");
                }
                else if (Request.QueryString["id"].ToString() == "2")
                {
                    MessageBox("Branch Details Updated Successfully");
                }


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
        string sqlpartylist = "SELECT BRANCHID,BRANCHNAME,STATENAME,CITYNAME,ADDRESS,CASE WHEN STATUS=0 THEN 'ACTIVE' ELSE 'NON-ACTIVE' END AS STATUS FROM BRANCHMASTER BM WHERE BM.CMPID=" + Session["cmpid"].ToString();
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
        Response.Redirect("branchdetails.aspx?BRANCHID=" + lnk.CommandArgument.ToString().Trim());
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