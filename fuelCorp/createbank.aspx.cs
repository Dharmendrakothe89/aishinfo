using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class createbank : System.Web.UI.Page
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
            FillBank();
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

        DataTable dtcity = FillCity(dtstate.Rows[0]["STATEID"].ToString().Trim());
        ddlcity.Enabled = true;
        ddlcity.DataSource = dtcity;
        ddlcity.DataTextField = "CITYNAME";
        ddlcity.DataValueField = "CITYID";
        ddlcity.DataBind();
       
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
      
    }
    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {
      
            DataTable dtcity = FillCity(ddlstate.SelectedValue.ToString());
            ddlcity.Enabled = true;
            ddlcity.DataSource = dtcity;
            ddlcity.DataTextField = "CITYNAME";
            ddlcity.DataValueField = "CITYID";
            ddlcity.DataBind();
            
      
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        
        bankmaster objbank = new bankmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
        objbank.bankmaster_SRNO = -1;
        objbank.bankmaster_LEDGERID = -1;
        objbank.bankmaster_ACCOUNTNAME=txtaccountname.Text.Trim().ToString();
        objbank.bankmaster_ACCOUNTNO = txtactno.Text.Trim().ToString();
        objbank.bankmaster_CITYNAME=ddlcity.SelectedItem.Text;
        objbank.bankmaster_STATENAME=ddlstate.SelectedItem.Text;
        objbank.bankmaster_BANKBRANCHNAME = txtbranchname.Text.Trim().ToString();
        objbank.bankmaster_BANKNAME = ddlbank.SelectedItem.Text;
        objbank.bankmaster_MICRCODE = txtmicrcode.Text.Trim().ToString();
        objbank.bankmaster_IFSCCODE = txtifsccode.Text.Trim().ToString();
        objbank.bankmaster_ADDRESS = txtaddress.Text.Trim().ToString();
        objbank.bankmaster_BRANCHID = General.Parse<int>(Session["cmpid"].ToString());
        objbank.bankmaster_STATUS=0;
        if (objbank.Insert(true, "bankmaster"))
        {
            string sqlmax = "SELECT MAX(SRNO) AS SRNO FROM BANKMASTER BM WHERE STATUS=0 AND ACCOUNTNAME='" + txtaccountname.Text.Trim().ToString()+"'";
            Handler hdnmax = new Handler();
            DataTable dtmax = hdnmax.GetTable(sqlmax);

            string sqlrel = "SELECT RELATIONSHIPID FROM PERSONALTABLE PT WHERE PT.FIRSTNAME='BANK ACCOUNT' AND BRANCHID=" + Session["branchid"].ToString();
            Handler hdnrel = new Handler();
            DataTable dtrel = hdnrel.GetTable(sqlrel);
            if (dtrel.Rows.Count > 0)
            {
                Handler hdngroup = new Handler();
                DataTable dtgroup = hdngroup.GetTable("SELECT GROUPID FROM ACCOUNTGROUP AG WHERE GROUPNAME='BANK ACCOUNT' AND BRANCHID=0");
                personalrelation objpersonalrelation = new personalrelation(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                objpersonalrelation.personalrelation_SRNO = -1;
                objpersonalrelation.personalrelation_RELATIONSHIPID = General.Parse<int>(dtrel.Rows[0]["RELATIONSHIPID"].ToString());
                objpersonalrelation.personalrelation_ASSOSIATEDBRANCH = General.Parse<int>(Session["branchid"].ToString());
                objpersonalrelation.personalrelation_ASSOSIATEDFEILD = ddlbank.SelectedItem.Text;
                objpersonalrelation.personalrelation_GROUPID = General.Parse<int>(dtgroup.Rows[0]["GROUPID"].ToString());
                objpersonalrelation.personalrelation_STATUS = 0;
                if (objpersonalrelation.Insert(true, "personalrelation"))
                {
                    Handler hdnledger = new Handler();
                    string sqlban="SELECT MAX(PR.SRNO) AS LEDGERID FROM PERSONALRELATION PR WHERE PR.GROUPID=" + dtgroup.Rows[0]["GROUPID"].ToString().Trim() + " AND PR.ASSOSIATEDFEILD='" + ddlbank.SelectedItem.Text.Trim() + "' AND PR.RELATIONSHIPID=" + dtrel.Rows[0]["RELATIONSHIPID"].ToString().Trim();
                    DataTable dtledger = hdnledger.GetTable(sqlban);
                    
                    bankmaster objbank1 = new bankmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                    objbank1.bankmaster_SRNO = -1;
                    objbank1.bankmaster_LEDGERID = General.Parse<int>(dtledger.Rows[0]["LEDGERID"].ToString());
                    objbank1.bankmaster_BRANCHID = -1;
                    objbank1.bankmaster_STATUS = -1;
                    string condition = "BRANCHID=" + Session["branchid"].ToString() + " AND BANKNAME='" + ddlbank.SelectedItem.Text.Trim() + "' AND ACCOUNTNO='" + txtactno.Text.Trim().ToString()+"'";
                    if (objbank1.Insert(false, "bankmaster", condition))
                    {
                    }
                }
            }
            //MessageBox("Bank Created Successfully");
            //Response.Redirect("bankaccountdetails.aspx?ID=1&BANKID=" + dtmax.Rows[0][0].ToString().Trim());
            Response.Redirect("bankaccountlist.aspx?ID=1");
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