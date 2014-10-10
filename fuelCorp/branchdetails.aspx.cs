using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class branchdetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("default.aspx");
            }
            if (Request.QueryString["BRANCHID"] != null)
            {
                ViewState["BRANCHID"] = Request.QueryString["BRANCHID"].ToString();
                FillData(ViewState["BRANCHID"].ToString().Trim());

            }
        }
    }
    private void FillData(string branchid)
    {
        string sqlcolliery = "SELECT * FROM BRANCHMASTER BM WHERE BM.BRANCHID=" + branchid;
        Handler hdncolliery = new Handler();

        DataTable dtcolliery = hdncolliery.GetTable(sqlcolliery);
        if (dtcolliery.Rows.Count > 0)
        {
            FillState();
            txtbranchname.Text = dtcolliery.Rows[0]["BRANCHNAME"].ToString();
            txtaddress.Text = dtcolliery.Rows[0]["ADDRESS"].ToString();
            ddlstate.SelectedValue = dtcolliery.Rows[0]["STATEID"].ToString().Trim();
            DataTable dtcity= FillCity(dtcolliery.Rows[0]["STATEID"].ToString().Trim());
            ddlcity.DataSource = dtcity;
            ddlcity.DataTextField = "CITYNAME";
            ddlcity.DataValueField = "CITYID";
            ddlcity.DataBind();
            

            ddlcity.SelectedValue = dtcolliery.Rows[0]["CITYID"].ToString().Trim();
            ddlstatus.SelectedValue = dtcolliery.Rows[0]["STATUS"].ToString().Trim();
        }

    }
    private void FillState()
    {
        string sqlstate = "SELECT RTRIM(STATENAME) AS STATENAME,RTRIM(STATEID) AS STATEID FROM STATEMASTER SM WHERE SM.STATUS=0 ORDER BY STATENAME";
        Handler hdnstate = new Handler();
        DataTable dtstate = hdnstate.GetTable(sqlstate);

        ddlstate.DataSource = dtstate;
        ddlstate.DataTextField = "STATENAME";
        ddlstate.DataValueField = "STATEID";

        ddlstate.DataBind();
        

    }
    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlstate.SelectedIndex > 0)
        {
            DataTable dtcity = FillCity(ddlstate.SelectedValue.ToString());
            ddlcity.Enabled = true;
            ddlcity.DataSource = dtcity;
            ddlcity.DataTextField = "CITYNAME";
            ddlcity.DataValueField = "CITYID";
            ddlcity.DataBind();
        
        }
        else
        {
            ddlcity.SelectedIndex = 0;
            ddlcity.Enabled = false;
        }
    }
    private DataTable FillCity(string stateid)
    {
        string sqlcity = "SELECT RTRIM(CITYNAME) AS CITYNAME,RTRIM(CITYID) AS CITYID FROM CITYMASTER CM WHERE CM.STATUS=0 AND CM.STATEID=" + stateid + " ORDER BY CM.CITYNAME";
        Handler hdncity = new Handler();
        DataTable dtcity = hdncity.GetTable(sqlcity);
        return dtcity;
    }
    protected void btnedit_Click(object sender, EventArgs e)
    {
        btnsubmit.Visible = true;
        ddlstate.Enabled = true;
        txtaddress.Enabled = true;
        ddlstatus.Enabled = true;
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        branchmaster objbranch = new branchmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
        objbranch.branchmaster_BRANCHID = -1;
        objbranch.branchmaster_CITYID = General.Parse<int>(ddlcity.SelectedValue.ToString());
        objbranch.branchmaster_CITYNAME=ddlcity.SelectedItem.Text.ToString().Trim();
        objbranch.branchmaster_STATEID = General.Parse<int>(ddlstate.SelectedValue.ToString());
        objbranch.branchmaster_STATENAME = ddlstate.SelectedItem.Text.ToString().Trim();
        objbranch.branchmaster_STATUS = General.Parse<int>(ddlstatus.SelectedValue.ToString());
        objbranch.branchmaster_ADDRESS=txtaddress.Text.Trim().ToString();
        objbranch.branchmaster_CMPID = -1;
        string condition = "BRANCHID=" + ViewState["BRANCHID"].ToString().Trim();
        if (objbranch.Insert(false, "branchmaster", condition))
        {
            
            Response.Redirect("branchlist.aspx?id=2");
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