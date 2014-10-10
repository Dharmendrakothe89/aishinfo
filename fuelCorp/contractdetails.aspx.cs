using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class contractdetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("default.aspx");
            }
            DisableControl();
            if (Request.QueryString["CONTRACTID"] != null)
            {
                FillParty();
                ViewState["CONTRACTID"] = Request.QueryString["CONTRACTID"].ToString();
                FillData(ViewState["CONTRACTID"].ToString().Trim());
            }

        }
    }

    private void FillParty()
    {
        string sqldepartment = "SELECT LM.PARTYNAME,LM.SRNO FROM PARTYMASTER LM ORDER BY LM.PARTYNAME";
        Handler hdndepartment = new Handler();
        DataTable dtdepartment = hdndepartment.GetTable(sqldepartment);
        ddlparty.DataSource = dtdepartment;
        ddlparty.DataTextField = "PARTYNAME";
        ddlparty.DataValueField = "SRNO";
        ddlparty.DataBind();
        ddlparty.Items.Insert(0, "-- Party --");
    }
    private void FillData(string contractid)
    {
        string sqldepartment = "SELECT * FROM CONTRACTMASTER CM WHERE CONTRACTID=" + contractid;
        Handler hdndepartment = new Handler();
        DataTable dtcontract = hdndepartment.GetTable(sqldepartment);
        if (dtcontract.Rows[0]["CONTRACTTYPEID"].ToString() == "1")
        {
            rdservicecontract.Checked = true;
        }
        else
        {
            rdtransportcontract.Checked = true;
        }
        ddlparty.SelectedValue = dtcontract.Rows[0]["PARTYID"].ToString();
        FillPartyDetails(contractid.Trim().ToString());
        ddlcontactperson.SelectedValue = dtcontract.Rows[0]["PARTYCONTACTPERSONID"].ToString();
        txtdate.Text = dtcontract.Rows[0]["CONTRACTDATE"].ToString();
        txtrefno.Text = dtcontract.Rows[0]["REFNO"].ToString();
        txtrefdate.Text = dtcontract.Rows[0]["REFDATE"].ToString();
        txtcontractdate.Text = dtcontract.Rows[0]["STARTDATE"].ToString();
        txtexpirydate.Text = dtcontract.Rows[0]["ENDDATE"].ToString();
        txtquantity.Text = dtcontract.Rows[0]["QUANTITY"].ToString();
        ddlquantitytype.SelectedValue = dtcontract.Rows[0]["QUANTITYPER"].ToString();
        txtrate.Text = dtcontract.Rows[0]["RATE"].ToString();
        txtchargservice.Text = dtcontract.Rows[0]["SERVICECHARGE"].ToString();
        if (dtcontract.Rows[0]["CONTRACTTYPEID"].ToString() == "1")
        {
            rdservicetaxapplicable.Checked = true;
        }
        else
        {
            rdservicetaxnotapplicable.Checked = true;
        }

    }
    private void DisableControl()
    {
        rdservicecontract.Enabled = false;
        rdtransportcontract.Enabled = false;
        txtdate.Enabled = false;
        txtrefno.Enabled = false;
        txtrefdate.Enabled = false;
        txtcode.Enabled = false;
        ddlcontactperson.Enabled = false;
        txtaddress.Enabled = false;
        txtcontractdate.Enabled = false;
        txtexpirydate.Enabled = false;
        txtquantity.Enabled = false;
        ddlquantitytype.Enabled = false;
        txtrate.Enabled = false;
        txtchargservice.Enabled = false;
        rdservicetaxapplicable.Enabled = false;
        rdservicetaxnotapplicable.Enabled = false;
        ddlparty.Enabled = false;
        ddlcontactperson.Enabled = false;
    }
    protected void ddlparty_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlparty.SelectedIndex > 0)
        {
            FillPartyDetails(ddlparty.SelectedValue.Trim().ToString());

        }
        else
        {
            ddlcontactperson.DataSource = null;
            ddlcontactperson.DataBind();
            ddlcontactperson.Enabled = false;

        }
    }
    private void FillPartyDetails(string partyid)
    {
        string sqldepartment = "SELECT PM.PARTYNAME,PM.PARTYCODE,PAM.ADDRESS FROM PARTYMASTER PM INNER JOIN PARTYADDRESSMASTER PAM ON PM.SRNO=PAM.PARTYID" +
                               " WHERE PAM.ADDRESSTYPE=1 AND PM.SRNO=" + partyid;
        Handler hdndepartment = new Handler();
        DataTable dtdepartment = hdndepartment.GetTable(sqldepartment);
        //ddlcontactperson.Enabled = true;
        if (dtdepartment.Rows.Count > 0)
        {
            txtcode.Text = dtdepartment.Rows[0]["PARTYCODE"].ToString();
            txtaddress.Text = dtdepartment.Rows[0]["ADDRESS"].ToString();
        }

        string sqlperson = "SELECT PM.SRNO,PM.PERSONNAME FROM PERSONALMASTER PM WHERE PM.PERSONTYPE='PARTY' AND PM.PERSONRELATIONID=" + partyid;
        Handler hdnperson = new Handler();
        DataTable dtperson = hdnperson.GetTable(sqlperson);

        ddlcontactperson.DataSource = dtperson;
        ddlcontactperson.DataTextField = "PERSONNAME";
        ddlcontactperson.DataValueField = "SRNO";
        
        ddlcontactperson.DataBind();
    }
    protected void btnsubmit1_Click(object sender, EventArgs e)
    {
        txtworkdetails.Enabled = false;
        txtdeliverydetails.Enabled = false;
        chkqualityguarantity.Enabled = false;
        chkqualityanalysis.Enabled = false;
        chkpaymentterms.Enabled = false;
        string sql = "SELECT * FROM CONTRACTDETAILMASTER CM WHERE STATUS=0 AND CONTRACTID=" + ViewState["CONTRACTID"].ToString().Trim();
        Handler hdn = new Handler();
        DataTable dt = hdn.GetTable(sql);
        ViewState["contractdetails"] = dt;
        if (((DataTable)ViewState["contractdetails"]).Rows.Count > 0)
        {
            if (((DataTable)ViewState["contractdetails"]).Rows.Count > 0 && ((DataTable)ViewState["contractdetails"]).Rows[0]["WORKDETAILS"].ToString().Trim() != null)
            {
                txtworkdetails.Text = ((DataTable)ViewState["contractdetails"]).Rows[0]["WORKDETAILS"].ToString().Trim();
            }
            else
            {
                txtworkdetails.Text = "No work Details";
            }
            if (((DataTable)ViewState["contractdetails"]).Rows[0]["DELIVERYDETAILS"].ToString().Trim() != null)
            {
                txtdeliverydetails.Text = ((DataTable)ViewState["contractdetails"]).Rows[0]["DELIVERYDETAILS"].ToString().Trim();
            }
            else
            {
                txtdeliverydetails.Text = "No Delivery Details";
            }
            string sql1 = "SELECT T.TERMSID,T.TERMS FROM TERMSTABLE T INNER JOIN TERMSCONDITION TC ON TC.SRNO=T.TERMSID" +
                                      " WHERE T.STATUS=0 AND T.PARTYTYPE='CONTRACT' AND TC.SUBCATEGORY='QUALITY GUARANTEE' AND T.PARTYID=" + ViewState["CONTRACTID"].ToString().Trim();
            Handler hdn1 = new Handler();
            DataTable dt1 = hdn1.GetTable(sql1);
            if (dt1.Rows.Count > 0)
            {
                chkqualityguarantity.DataSource = dt1;
                chkqualityguarantity.DataTextField = "TERMS";
                chkqualityguarantity.DataValueField = "TERMSID";
                chkqualityguarantity.DataBind();
                lblqualityguarantity.Visible = true;
                chkqualityanalysis.Visible = false;
            }
            else
            {
                chkqualityguarantity.DataSource = null;
                chkqualityguarantity.DataBind();
                chkqualityguarantity.Visible = false;
                lblqualityguarantity.Visible = true;
            }
            string sql2 = "SELECT T.TERMSID,T.TERMS FROM TERMSTABLE T INNER JOIN TERMSCONDITION TC ON TC.SRNO=T.TERMSID" +
                                      " WHERE T.STATUS=0 AND T.PARTYTYPE='CONTRACT' AND TC.SUBCATEGORY='ANALYSIS OF QUALITY' AND T.PARTYID=" + ViewState["CONTRACTID"].ToString().Trim();
            Handler hdn2 = new Handler();
            DataTable dt2 = hdn2.GetTable(sql2);
            if (dt2.Rows.Count > 0)
            {
                chkqualityanalysis.DataSource = dt2;
                chkqualityanalysis.DataTextField = "TERMS";
                chkqualityanalysis.DataValueField = "TERMSID";
                chkqualityanalysis.DataBind();
                chkqualityanalysis.Visible = true;
                lblanalysisquality.Visible = false;
            }
            else
            {
                chkqualityanalysis.DataSource = null;
                chkqualityanalysis.DataBind();
                chkqualityanalysis.Visible = false;
                lblanalysisquality.Visible = true;
            }
            string sql3 = "SELECT T.TERMSID,T.TERMS FROM TERMSTABLE T INNER JOIN TERMSCONDITION TC ON TC.SRNO=T.TERMSID" +
                                      " WHERE T.STATUS=0 AND T.PARTYTYPE='CONTRACT' AND TC.SUBCATEGORY='PAYMENT TERMS' AND T.PARTYID=" + ViewState["CONTRACTID"].ToString().Trim();
            Handler hdn3 = new Handler();
            DataTable dt3 = hdn3.GetTable(sql3);
            if (dt3.Rows.Count > 0)
            {
                chkpaymentterms.DataSource = dt3;
                chkpaymentterms.DataTextField = "TERMS";
                chkpaymentterms.DataValueField = "TERMSID";
                chkpaymentterms.DataBind();
                chkpaymentterms.Visible = true;
                lblpaymentterms.Visible = false;
            }
            else
            {
                chkpaymentterms.DataSource = null;
                chkpaymentterms.DataBind();
                chkpaymentterms.Visible = false;
                lblpaymentterms.Visible = true;
            }
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg1", "ShowDiv2();", true);
    }
    protected void btnsubmit2_Click(object sender, EventArgs e)
    {
        txtsecuritydeposit.Enabled = false;
        txtweighment.Enabled = false;
        txtexcesssupply.Enabled = false;
        txtdeleysupply.Enabled = false;
        chkashpenalty.Enabled = false;
        chklowerbtu.Enabled = false;
        if (((DataTable)ViewState["contractdetails"]).Rows.Count > 0)
        {
            if (((DataTable)ViewState["contractdetails"]).Rows[0]["SUPPLYDELEYDETAILS"].ToString().Trim() != null)
            {
                txtdeleysupply.Text = ((DataTable)ViewState["contractdetails"]).Rows[0]["SUPPLYDELEYDETAILS"].ToString().Trim();
            }
            else
            {
                txtdeleysupply.Text = string.Empty;
            }
            if (((DataTable)ViewState["contractdetails"]).Rows[0]["WEIGHTMENT"].ToString().Trim() != null)
            {
                txtweighment.Text = ((DataTable)ViewState["contractdetails"]).Rows[0]["WEIGHTMENT"].ToString().Trim();
            }
            else
            {
                txtweighment.Text = string.Empty;
            }
            if (((DataTable)ViewState["contractdetails"]).Rows[0]["EXCESSSUPPLY"].ToString().Trim() != null)
            {
                txtexcesssupply.Text = ((DataTable)ViewState["contractdetails"]).Rows[0]["EXCESSSUPPLY"].ToString().Trim();
            }
            else
            {
                txtexcesssupply.Text = string.Empty;
            }
            if (((DataTable)ViewState["contractdetails"]).Rows[0]["SECURITYDEPOSIT"].ToString().Trim() != null)
            {
                txtsecuritydeposit.Text = ((DataTable)ViewState["contractdetails"]).Rows[0]["SECURITYDEPOSIT"].ToString().Trim();
            }
            else
            {
                txtsecuritydeposit.Text = string.Empty;
            }
            string sql3 = "SELECT T.TERMSID,T.TERMS FROM TERMSTABLE T INNER JOIN TERMSCONDITION TC ON TC.SRNO=T.TERMSID" +
                                      " WHERE T.STATUS=0 AND T.PARTYTYPE='CONTRACT' AND TC.SUBCATEGORY='LOWER BTU' AND T.PARTYID=" + ViewState["CONTRACTID"].ToString().Trim();
            Handler hdn3 = new Handler();
            DataTable dt3 = hdn3.GetTable(sql3);
            if (dt3.Rows.Count > 0)
            {
                chklowerbtu.DataSource = dt3;
                chklowerbtu.DataTextField = "TERMS";
                chklowerbtu.DataValueField = "TERMSID";
                chklowerbtu.DataBind();
                chklowerbtu.Visible = true;
                lbllowerbtu.Visible = false;
            }
            else
            {
                chklowerbtu.DataSource = null;
                chklowerbtu.DataBind();
                chklowerbtu.Visible = false;
                lbllowerbtu.Visible = true;
            }

            string sql4 = "SELECT T.TERMSID,T.TERMS FROM TERMSTABLE T INNER JOIN TERMSCONDITION TC ON TC.SRNO=T.TERMSID" +
                                      " WHERE T.STATUS=0 AND T.PARTYTYPE='CONTRACT' AND TC.SUBCATEGORY='LOWER BTU' AND T.PARTYID=" + ViewState["CONTRACTID"].ToString().Trim();
            Handler hdn4 = new Handler();
            DataTable dt4 = hdn4.GetTable(sql4);
            if (dt4.Rows.Count > 0)
            {
                chkashpenalty.DataSource = dt4;
                chkashpenalty.DataTextField = "TERMS";
                chkashpenalty.DataValueField = "TERMSID";
                chkashpenalty.DataBind();
                chkashpenalty.Visible = true;
                lblashpenalty.Visible = false;
            }
            else
            {
                chkashpenalty.DataSource = null;
                chkashpenalty.DataBind();
                chkashpenalty.Visible = false;
                lblashpenalty.Visible = true;
            }
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg1", "ShowDiv3();", true);
    }
    protected void btnsubmit3_Click(object sender, EventArgs e)
    {
        if (((DataTable)ViewState["contractdetails"]).Rows.Count > 0)
        {
            if (((DataTable)ViewState["contractdetails"]).Rows[0]["JURISDICTION"].ToString().Trim() != null)
            {
                txtjurisdiction.Text = ((DataTable)ViewState["contractdetails"]).Rows[0]["JURISDICTION"].ToString().Trim();
            }
            else
            {
                txtjurisdiction.Text = string.Empty;
            }
            if (((DataTable)ViewState["contractdetails"]).Rows[0]["COMPLIANCELAW"].ToString().Trim() != null)
            {
                txtcompliancelaw.Text = ((DataTable)ViewState["contractdetails"]).Rows[0]["COMPLIANCELAW"].ToString().Trim();
            }
            else
            {
                txtcompliancelaw.Text = string.Empty;
            }
            if (((DataTable)ViewState["contractdetails"]).Rows[0]["CONFIDENTIALITY"].ToString().Trim() != null)
            {
                txtconfidential.Text = ((DataTable)ViewState["contractdetails"]).Rows[0]["CONFIDENTIALITY"].ToString().Trim();
            }
            else
            {
                txtconfidential.Text = string.Empty;
            }
            if (((DataTable)ViewState["contractdetails"]).Rows[0]["INDEMNITY"].ToString().Trim() != null)
            {
                txtindenmnity.Text = ((DataTable)ViewState["contractdetails"]).Rows[0]["INDEMNITY"].ToString().Trim();
            }
            else
            {
                txtindenmnity.Text = string.Empty;
            }
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg1", "ShowDiv4();", true);
    }
    protected void btnsubmit4_Click(object sender, EventArgs e)
    {
        if (((DataTable)ViewState["contractdetails"]).Rows.Count > 0)
        {
            if (((DataTable)ViewState["contractdetails"]).Rows[0]["JURISDICTION"].ToString().Trim() != null)
            {
                txtarbitration.Text = ((DataTable)ViewState["contractdetails"]).Rows[0]["JURISDICTION"].ToString().Trim();
            }
            else
            {
                txtarbitration.Text = string.Empty;
            }
            if (((DataTable)ViewState["contractdetails"]).Rows[0]["COMPLIANCELAW"].ToString().Trim() != null)
            {
                txttermination.Text = ((DataTable)ViewState["contractdetails"]).Rows[0]["COMPLIANCELAW"].ToString().Trim();
            }
            else
            {
                txttermination.Text = string.Empty;
            }
            if (((DataTable)ViewState["contractdetails"]).Rows[0]["CONFIDENTIALITY"].ToString().Trim() != null)
            {
                txtforcemajeure.Text = ((DataTable)ViewState["contractdetails"]).Rows[0]["CONFIDENTIALITY"].ToString().Trim();
            }
            else
            {
                txtforcemajeure.Text = string.Empty;
            }

            string sql4 = "SELECT T.TERMSID,T.TERMS FROM TERMSTABLE T INNER JOIN TERMSCONDITION TC ON TC.SRNO=T.TERMSID" +
                                    " WHERE T.STATUS=0 AND T.PARTYTYPE='CONTRACT' AND TC.SUBCATEGORY='OTHER TERMS' AND T.PARTYID=" + ViewState["CONTRACTID"].ToString().Trim();
            Handler hdn4 = new Handler();
            DataTable dt4 = hdn4.GetTable(sql4);
            if (dt4.Rows.Count > 0)
            {
                chkotherterms.DataSource = dt4;
                chkotherterms.DataTextField = "TERMS";
                chkotherterms.DataValueField = "TERMSID";
                chkotherterms.DataBind();
                chkotherterms.Visible = true;
                lblotherterms.Visible = false;
            }
            else
            {
                chkotherterms.DataSource = null;
                chkotherterms.DataBind();
                chkotherterms.Visible = false;
                lblotherterms.Visible = true;
            }
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg1", "ShowDiv5();", true);
    }
    protected void btnsubmit5_Click(object sender, EventArgs e)
    {
        if (((DataTable)ViewState["contractdetails"]).Rows.Count > 0)
        {
            if (((DataTable)ViewState["contractdetails"]).Rows[0]["PARTYVALUE"].ToString().Trim() != null)
            {
                txtpartyvalue.Text = ((DataTable)ViewState["contractdetails"]).Rows[0]["PARTYVALUE"].ToString().Trim();
            }
            else
            {
                txtpartyvalue.Text = string.Empty;
            }
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg1", "ShowDiv6();", true);
    }
    protected void btnsubmit6_Click(object sender, EventArgs e)
    {
        Response.Redirect("contractlist.aspx");
    }
}