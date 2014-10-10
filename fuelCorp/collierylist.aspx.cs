using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class collierylist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("default.aspx");
            }
            DataTable dtcolliery = FillCoolieryList();
            ViewState["list"] = dtcolliery;
            if (dtcolliery.Rows.Count > 0)
            {
                gvcollierylist.DataSource = (DataTable)ViewState["list"];
                gvcollierylist.DataBind();
            }
            else
            {
                gvcollierylist.DataSource = null;
                gvcollierylist.DataBind();
            }
            if (Request.QueryString["ID"] != null)
            {
                if (Request.QueryString["ID"].ToString() == "1")
                {
                    MessageBox("Colliery Updated Successfully");
                }
                else if (Request.QueryString["ID"].ToString() == "2")
                {
                    MessageBox("Colliery Created Successfully");
                }
            }
        }

    }
    private DataTable FillCoolieryList()
    {
        string sqlpartylist = "SELECT CM.SRNO AS COLLIERYID,CM.COLLIERYNAME,CM.COLLIERYCODE,CM.COLLIERYREGION,CASE WHEN STATUS=0 THEN 'ACTIVE' ELSE 'DE-ACTIVE' END AS STATUS FROM collierymaster CM";
        Handler hdnpartylist = new Handler();
        DataTable dtpartylist = hdnpartylist.GetTable(sqlpartylist);
        return dtpartylist;
    }
    protected void gvlookup_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvcollierylist.PageIndex = e.NewPageIndex;
        gvcollierylist.DataSource = (DataTable)ViewState["list"];
        gvcollierylist.DataBind();
    }
    protected void lnkdetails_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        Response.Redirect("collierydetails.aspx?COLLIERYID=" + lnk.CommandArgument.ToString().Trim());
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