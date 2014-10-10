using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class partylist : System.Web.UI.Page
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
                if (Request.QueryString["ID"].ToString() == "1")
                {
                    MessageBox("Party Details Updated Successsfully");
                }
                else if (Request.QueryString["ID"].ToString() == "2")
                {
                    MessageBox("Party Created Successsfully");
                }
                
            }
            DataTable dtparty = FillPartyList();
            if (dtparty.Rows.Count > 0)
            {
                ViewState["lookup"] = dtparty;
                gvpartylist.DataSource = (DataTable)ViewState["lookup"];
                gvpartylist.DataBind();
            }
            else
            {
                gvpartylist.DataSource = null;
                gvpartylist.DataBind();
            }
        }
    }
    private DataTable FillPartyList()
    {
        string sqlpartylist = "SELECT PM.SRNO AS PARTYID,PM.PARTYNAME,PM.PARTYCODE,PM.PANNO,PM.WEBSITE,PM.PHONENO,PM.EMAIL,PAD.CITYNAME,PAD.STATENAME FROM partymaster PM " +
                            " INNER JOIN partyaddressmaster PAD ON PM.SRNO=PAD.PARTYID AND PAD.ADDRESSTYPE=1 WHERE PAD.STATUS=0 GROUP BY PM.SRNO,PM.PARTYNAME,PM.PARTYCODE,PM.PANNO,PM.WEBSITE,PM.PHONENO,PM.EMAIL,PAD.CITYNAME,PAD.STATENAME";
        Handler hdnpartylist = new Handler();
        DataTable dtpartylist = hdnpartylist.GetTable(sqlpartylist);
        return dtpartylist;
    }
    protected void gvlookup_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvpartylist.PageIndex = e.NewPageIndex;
        gvpartylist.DataSource = (DataTable)ViewState["lookup"];
        gvpartylist.DataBind();
    }
    protected void lnkdetails_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        Response.Redirect("partydetails.aspx?PARTYID=" + lnk.CommandArgument.ToString().Trim());
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