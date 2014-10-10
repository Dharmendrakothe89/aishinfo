using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class citylist : System.Web.UI.Page
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
            FillCity();
        }
    }
    private void FillCity()
    {
        string sql = "SELECT RTRIM(CM.CITYID) AS CITYID,RTRIM(CM.CITYNAME) AS CITYNAME,RTRIM(SM.STATENAME) AS STATENAME,CASE WHEN SM.UNIONTERITARY=0 THEN 'STATE' ELSE 'UNION TERITARY' END AS TYPE," +
                     " CASE WHEN SM.STATUS=0 THEN 'ACTIVE' ELSE 'DE-ACTIVE' END AS STATUS FROM CITYMASTER CM" +
                     " INNER JOIN STATEMASTER SM ON SM.STATEID=CM.STATEID";
        Handler hdnstate = new Handler();
        DataTable dtstate = hdnstate.GetTable(sql);
        ViewState["list"] = dtstate;
        gvstatelist.DataSource = (DataTable)ViewState["list"];
        gvstatelist.DataBind();
    }
    private void FillState()
    {
        string sql = "SELECT RTRIM(SM.STATEID) AS STATEID,RTRIM(SM.STATENAME) AS STATENAME FROM STATEMASTER SM WHERE SM.STATUS=0 ORDER BY SM.STATENAME";
        Handler hdnstate = new Handler();
        DataTable dtstate = hdnstate.GetTable(sql);
        ddlstate.DataSource = dtstate;
        ddlstate.DataTextField = "STATENAME";
        ddlstate.DataValueField = "STATEID";
        ddlstate.DataBind();


        ddleditstate.DataSource = dtstate;
        ddleditstate.DataTextField = "STATENAME";
        ddleditstate.DataValueField = "STATEID";
        ddleditstate.DataBind();

    }
    protected void lnkedit_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        ViewState["cityid"] = lnk.CommandArgument.ToString();
        string sql = "SELECT SM.CITYID,SM.CITYNAME,SM.STATEID,SM.STATUS FROM CITYMASTER SM WHERE SM.CITYID=" + lnk.CommandArgument.Trim().ToString();
        Handler hdnstate = new Handler();
        DataTable dtstate = hdnstate.GetTable(sql);
        txteditcity.Text = dtstate.Rows[0]["CITYNAME"].ToString().Trim();
        ddleditstate.SelectedValue = dtstate.Rows[0]["STATEID"].ToString().Trim();
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
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (txtname.Text.Trim().ToString() != string.Empty)
        {
            citymaster state = new citymaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
            state.citymaster_CITYID = -1;
            state.citymaster_CITYNAME = txtname.Text.ToString().Trim();
            state.citymaster_STATUS = 0;
            state.citymaster_STATEID = General.Parse<int>(ddlstate.SelectedValue.ToString());
            if (state.Insert(true, "citymaster"))
            {
                MessageBox("City Added Successfully");
                FillCity();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "FillState();", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg23", "AddVehicle();", true);
        }
    }
    protected void btnedit_Click(object sender, EventArgs e)
    {
        if (txteditcity.Text.Trim().ToString() != string.Empty)
        {
            citymaster state = new citymaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
            state.citymaster_CITYID = -1;
            state.citymaster_CITYNAME = txteditcity.Text.ToString().Trim();
            state.citymaster_STATUS = General.Parse<int>(ddlstatus.SelectedValue.ToString());
            state.citymaster_STATEID = General.Parse<int>(ddleditstate.SelectedValue.ToString());
            string condition = "CITYID=" + ViewState["cityid"].ToString();
            if (state.Insert(false, "citymaster", condition))
            {
                MessageBox("City Updated Successfully");
                FillCity();
                divlist.Style.Add("display", "block");
                divedit.Style.Add("display", "none");
                divadd.Style.Add("display", "none");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "FillState();", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg2d", "EditState();", true);
        }
    }
    protected void gvlookup_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvstatelist.PageIndex = e.NewPageIndex;
        gvstatelist.DataSource = (DataTable)ViewState["list"];
        gvstatelist.DataBind();
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