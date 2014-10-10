using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class bankaccountdetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("default.aspx");
            }
            if (Request.QueryString["BANKID"] != null)
            {
                ViewState["BANKID"] = Request.QueryString["BANKID"].ToString();
                FillState();
                FillBank();
                FillData(ViewState["BANKID"].ToString().Trim());
                
            }
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
        ddlstate.Items.Insert(0, "--State--");

    }
    private DataTable FillCity(string stateid)
    {
        string sqlcity = "SELECT RTRIM(CITYNAME) AS CITYNAME,RTRIM(CITYID) AS CITYID FROM CITYMASTER CM WHERE CM.STATUS=0 AND CM.STATEID=" + stateid + " ORDER BY CM.CITYNAME";
        Handler hdncity = new Handler();
        DataTable dtcity = hdncity.GetTable(sqlcity);
        return dtcity;
    }
    private void FillBank()
    {
        string sqlbank = "SELECT RTRIM(LM.NAME) AS BANKNAME,RTRIM(LM.SRNO) AS BANKID FROM lookupmaster LM INNER JOIN lookupheadingmaster LHM ON LHM.SRNO=LM.HEADID WHERE LHM.HEAD='BANK' ORDER BY LM.NAME";
        Handler hdnbank = new Handler();
        DataTable dtbank = hdnbank.GetTable(sqlbank);
        ddlbank.DataSource = dtbank;
        ddlbank.DataTextField = "BANKNAME";
        ddlbank.DataValueField = "BANKID";
        ddlbank.DataBind();
        ddlbank.Items.Insert(0, "--Bank--");
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
            ddlcity.Items.Insert(0, "--City--");
        }
        else
        {
            ddlcity.Enabled = false;
        }
    }

    private void FillData(string bankid)
    {
        string sqlcolliery = "select * from bankmaster where srno=" + bankid;
        Handler hdncolliery = new Handler();

        DataTable dtcolliery = hdncolliery.GetTable(sqlcolliery);
        if (dtcolliery.Rows.Count > 0)
        {
            txtaccountname.Text = dtcolliery.Rows[0]["ACCOUNTNAME"].ToString().Trim();
            txtactno.Text = dtcolliery.Rows[0]["ACCOUNTNO"].ToString().Trim();
            txtbranchname.Text = dtcolliery.Rows[0]["BANKBRANCHNAME"].ToString().Trim();
            txtifsccode.Text = dtcolliery.Rows[0]["IFSCCODE"].ToString().Trim();
            txtmicrcode.Text = dtcolliery.Rows[0]["MICRCODE"].ToString().Trim();
            txtaddress.Text = dtcolliery.Rows[0]["ADDRESS"].ToString().Trim();

            //ddlbank.SelectedItem.Text = dtcolliery.Rows[0]["BANKNAME"].ToString().Trim();
            ddlbank.Items.FindByText(dtcolliery.Rows[0]["BANKNAME"].ToString().Trim()).Selected = true;
            ddlstate.Items.FindByText(dtcolliery.Rows[0]["STATENAME"].ToString().Trim()).Selected = true;
            DataTable dtcity = FillCity(ddlstate.SelectedValue.ToString().Trim());
            ddlcity.Enabled = true;
            ddlcity.DataSource = dtcity;
            ddlcity.DataTextField = "CITYNAME";
            ddlcity.DataValueField = "CITYID";
            ddlcity.DataBind();
            ddlcity.Items.Insert(0, "--City--");
            if (dtcolliery.Rows[0]["STATUS"].ToString().Trim() == "True")
            {
                ddlstatus.SelectedValue = "1";
            }
            else
            {
                ddlstatus.SelectedValue = "0";
            }
            //ddlcity.SelectedItem.Text = dtcolliery.Rows[0]["CITYNAME"].ToString().Trim();
            ddlcity.Items.FindByText(dtcolliery.Rows[0]["CITYNAME"].ToString().Trim()).Selected = true;
            ddlcity.Enabled = false;
        }

    }
    protected void btnedit_Click(object sender, EventArgs e)
    {
        btnsubmit.Visible = true;
        txtaccountname.Enabled = true;
        txtactno.Enabled = true;
        ddlstate.Enabled = true;
        ddlstatus.Enabled = true;
        ddlbank.Enabled = true;
        txtbranchname.Enabled = true;
        txtifsccode.Enabled = true;
        txtaddress.Enabled = true;
        txtmicrcode.Enabled = true;

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {

        bankmaster objbank = new bankmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
        objbank.bankmaster_SRNO = -1;
        objbank.bankmaster_LEDGERID = -1;
        objbank.bankmaster_ACCOUNTNAME = txtaccountname.Text.Trim().ToString();
        objbank.bankmaster_ACCOUNTNO = txtactno.Text.Trim().ToString();
        objbank.bankmaster_CITYNAME = ddlcity.SelectedItem.Text;
        objbank.bankmaster_STATENAME = ddlstate.SelectedItem.Text;
        objbank.bankmaster_BANKBRANCHNAME = txtbranchname.Text.Trim().ToString();
        objbank.bankmaster_BANKNAME = ddlbank.SelectedItem.Text;
        objbank.bankmaster_MICRCODE = txtmicrcode.Text.Trim().ToString();
        objbank.bankmaster_IFSCCODE = txtifsccode.Text.Trim().ToString();
        objbank.bankmaster_ADDRESS = txtaddress.Text.Trim().ToString();
        objbank.bankmaster_BRANCHID = General.Parse<int>(Session["cmpid"].ToString());
        objbank.bankmaster_STATUS = General.Parse<int>(ddlstatus.SelectedValue.Trim().ToString());
        string condition = "SRNO=" + ViewState["BANKID"].ToString().Trim();
        if (objbank.Insert(false, "bankmaster", condition))
        {
            
            Response.Redirect("bankaccountlist.aspx?ID=2");
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