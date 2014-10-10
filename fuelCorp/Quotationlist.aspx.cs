using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Quotationlist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("default.aspx");
            }
            if (Request.QueryString["ID"] != null)
            {
                if (Request.QueryString["ID"].ToString() == "1")
                {
                    MessageBox("Quotation Created Successfully");
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
        string sqlpartylist = "SELECT RTRIM(QUOTATIONID) AS QUOTATIONID,RTRIM(DATE) AS DATE,RTRIM(EXPIRYDATE) AS EXPIRYDATE,RTRIM(REFNO) AS REFNO," +
                              " RTRIM(COALCOST) AS COALCOST,RTRIM(TAXCOST) AS TAXCOST,RTRIM(TRANSPORTATIONCOST) AS TRANSPORTATIONCOST," +
                              " RTRIM(TOTALCOST) AS TOTALCOST FROM QUOTATIONMASTER QM";
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
        Response.Redirect("quotationdetail.aspx?QUOTATIONID=" + lnk.CommandArgument.ToString().Trim());
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