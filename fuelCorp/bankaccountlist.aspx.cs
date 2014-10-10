using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class bankaccountlist : System.Web.UI.Page
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
                    MessageBox("Bank Account Created Successfully");
                }
                else if (Request.QueryString["ID"].ToString() == "2")
                {
                    MessageBox("Bank Account Updated Successfully");
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
        string sqlpartylist = " SELECT SRNO,ACCOUNTNAME,ACCOUNTNO,BANKNAME,BANKBRANCHNAME,IFSCCODE,ADDRESS,"+
                              " CASE WHEN STATUS=0 THEN 'ACTIVE' ELSE 'DEACTIVE' END AS STATUS FROM BANKMASTER BM"+
                              " WHERE BM.BRANCHID IN( SELECT B.BRANCHID FROM BRANCHMASTER B WHERE B.CMPID="+Session["cmpid"].ToString()+")" ;
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
        Response.Redirect("bankaccountdetails.aspx?BANKID=" + lnk.CommandArgument.ToString().Trim());
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