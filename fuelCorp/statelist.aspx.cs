using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class statelist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("default.aspx");
            }
            FillState();
        }
    }
    private void FillState()
    {
        string sql = "SELECT SM.STATEID,SM.STATENAME,CASE WHEN SM.UNIONTERITARY=0 THEN 'STATE' ELSE 'UNION TERITARY' END AS TYPE," +
                   " CASE WHEN SM.STATUS=0 THEN 'ACTIVE' ELSE 'DE-ACTIVE' END AS STATUS FROM STATEMASTER SM ORDER BY SM.STATENAME";
        Handler hdnstate = new Handler();
        DataTable dtstate = hdnstate.GetTable(sql);
        ViewState["list"] = dtstate;
        gvstatelist.DataSource = (DataTable)ViewState["list"];
        gvstatelist.DataBind();
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (txtname.Text.ToString().Trim() != string.Empty)
        {
            statemaster state = new statemaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
            state.statemaster_STATEID = -1;
            state.statemaster_STATENAME = txtname.Text.ToString().Trim();
            state.statemaster_STATUS = 0;
            state.statemaster_UNIONTERITARY = General.Parse<int>(ddlunionteritary.SelectedValue.ToString());
            if (state.Insert(true, "statemaster"))
            {
                MessageBox("State Added Successfully");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg2", "FillState();", true);
                FillState();
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg23", "AddVehicle();", true);
        }
    }
    protected void lnkedit_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        ViewState["stateid"] = lnk.CommandArgument.ToString();
        string sql = "SELECT SM.STATEID,SM.STATENAME,SM.UNIONTERITARY,SM.STATUS FROM STATEMASTER SM WHERE SM.STATEID=" + lnk.CommandArgument.Trim().ToString();
        Handler hdnstate = new Handler();
        DataTable dtstate = hdnstate.GetTable(sql);
        txteditname.Text = dtstate.Rows[0]["STATENAME"].ToString().Trim();
        if (dtstate.Rows[0]["UNIONTERITARY"].ToString().Trim() == "True")
        {
            ddledittype.SelectedValue = "1";
        }
        else
        {
            ddledittype.SelectedValue = "0";
        }
        if (dtstate.Rows[0]["STATUS"].ToString().Trim() == "True")
        {
            ddlstatus.SelectedValue = "1";
        }
        else
        {
            ddlstatus.SelectedValue = "0";
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "EditState();", true);
    }
    protected void gvlookup_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvstatelist.PageIndex = e.NewPageIndex;
        gvstatelist.DataSource = (DataTable)ViewState["list"];
        gvstatelist.DataBind();
    }
    protected void btnedit_Click(object sender, EventArgs e)
    {
        if (txteditname.Text.ToString().Trim() != string.Empty)
        {
            statemaster state = new statemaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
            state.statemaster_STATEID = -1;
            state.statemaster_STATENAME = txteditname.Text.ToString().Trim();
            state.statemaster_STATUS = General.Parse<int>(ddlstatus.SelectedValue.ToString());
            state.statemaster_UNIONTERITARY = General.Parse<int>(ddledittype.SelectedValue.ToString());
            string condition = "STATEID=" + ViewState["stateid"].ToString();
            if (state.Insert(false, "statemaster", condition))
            {
                MessageBox("State Updated Successfully");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg2d", "FillState();", true);
                FillState();
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg23", "EditState();", true);
        }
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