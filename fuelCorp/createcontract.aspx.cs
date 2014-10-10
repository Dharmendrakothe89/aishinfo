using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class createcontract : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("default.aspx");
            }
            //txtchargservice.Enabled = true;
            FillParty();
            FillData();
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
    protected void ContractType_CheckedChanged(object sender, EventArgs e)
    {
        if (rdservicecontract.Checked == true)
        {
            txtrate.Enabled = false;
            txtchargservice.Enabled = true;
        }
        else if (rdtransportcontract.Checked == true)
        {
            txtrate.Enabled = true;
            txtchargservice.Enabled = false;
        }
    }
    private void FillData()
    {
        string sqlterms = "SELECT TM.SRNO AS TERMID,TM.SUBCATEGORY,CASE WHEN  TM.DESCRIPTION IS NULL THEN TM.TERMS ELSE"+
                          " TM.TERMS +'( '+TM.DESCRIPTION+' )' END AS TERMS FROM TERMSCONDITION TM WHERE TM.STATUS=0 AND"+
                          " TM.CATEGORY='CONTRACT'";
 
        Handler hdnterms = new Handler();
        DataTable dtterms = hdnterms.GetTable(sqlterms);
        if (dtterms.Rows.Count > 0)
        {
            string filterExp = "SUBCATEGORY = 'QUALITY GUARANTEE'";
            string sortExp = "SUBCATEGORY";
            DataRow[] drarray;
            drarray = dtterms.Select(filterExp, sortExp, DataViewRowState.CurrentRows);
            DataTable dt1 = drarray.CopyToDataTable();
            chkqualityguarantity.DataSource = dt1;
            chkqualityguarantity.DataTextField = "TERMS";
            chkqualityguarantity.DataValueField = "TERMID";
            chkqualityguarantity.DataBind();

            filterExp = "SUBCATEGORY = 'ANALYSIS OF QUALITY'";
            drarray = dtterms.Select(filterExp, sortExp, DataViewRowState.CurrentRows);
            dt1 = drarray.CopyToDataTable();
            chkqualityanalysis.DataSource = dt1;
            chkqualityanalysis.DataTextField = "TERMS";
            chkqualityanalysis.DataValueField = "TERMID";
            chkqualityanalysis.DataBind();

            filterExp = "SUBCATEGORY = 'PENALTY IN ASH'";
            drarray = dtterms.Select(filterExp, sortExp, DataViewRowState.CurrentRows);
            dt1 = drarray.CopyToDataTable();
            chkashpenalty.DataSource = dt1;
            chkashpenalty.DataTextField = "TERMS";
            chkashpenalty.DataValueField = "TERMID";
            chkashpenalty.DataBind();

            filterExp = "SUBCATEGORY = 'LOWER BTU'";
            drarray = dtterms.Select(filterExp, sortExp, DataViewRowState.CurrentRows);
            dt1 = drarray.CopyToDataTable();
            chklowerbtu.DataSource = dt1;
            chklowerbtu.DataTextField = "TERMS";
            chklowerbtu.DataValueField = "TERMID";
            chklowerbtu.DataBind();

            filterExp = "SUBCATEGORY = 'OTHER TERMS'";
            drarray = dtterms.Select(filterExp, sortExp, DataViewRowState.CurrentRows);
            dt1 = drarray.CopyToDataTable();
            chkotherterms.DataSource = dt1;
            chkotherterms.DataTextField = "TERMS";
            chkotherterms.DataValueField = "TERMID";
            chkotherterms.DataBind();

            filterExp = "SUBCATEGORY = 'PAYMENT TERMS'";
            drarray = dtterms.Select(filterExp, sortExp, DataViewRowState.CurrentRows);
            dt1 = drarray.CopyToDataTable();
            chkpaymentterms.DataSource = dt1;
            chkpaymentterms.DataTextField = "TERMS";
            chkpaymentterms.DataValueField = "TERMID";
            chkpaymentterms.DataBind();
        }
       
    }
    protected void ddlparty_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlparty.SelectedIndex > 0)
        {
            string sqldepartment = "SELECT PM.PARTYNAME,PM.PARTYCODE,PAM.ADDRESS FROM PARTYMASTER PM INNER JOIN PARTYADDRESSMASTER PAM ON PM.SRNO=PAM.PARTYID" +
                                   " WHERE PAM.ADDRESSTYPE=1 AND PM.SRNO=" + ddlparty.SelectedValue;
            Handler hdndepartment = new Handler();
            DataTable dtdepartment = hdndepartment.GetTable(sqldepartment);
            ddlcontactperson.Enabled = true;
            if (dtdepartment.Rows.Count > 0)
            {
                txtcode.Text = dtdepartment.Rows[0]["PARTYCODE"].ToString();
                txtaddress.Text = dtdepartment.Rows[0]["ADDRESS"].ToString();
            }

            string sqlperson = "SELECT PM.SRNO,PM.PERSONNAME FROM PERSONALMASTER PM WHERE PM.PERSONTYPE='PARTY' AND PM.PERSONRELATIONID=" + ddlparty.SelectedValue;
            Handler hdnperson = new Handler();
            DataTable dtperson = hdnperson.GetTable(sqlperson);

            ddlcontactperson.DataSource = dtperson;
            ddlcontactperson.DataTextField = "PERSONNAME";
            ddlcontactperson.DataValueField = "SRNO";
            ddlcontactperson.DataBind();
            
        }
        else
        {
            ddlcontactperson.DataSource = null;
            ddlcontactperson.DataBind();
            ddlcontactperson.Enabled = false;

        }
    }

    protected void btnsubmit1_Click(object sender, EventArgs e)
    {
        contractmaster contract = new contractmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
        contract.contractmaster_CONTRACTID = -1;
        contract.contractmaster_CONTRACTWITH = "CUSTOMER";
        if(rdservicecontract.Checked == true)
        {
            contract.contractmaster_CONTRACTTYPE = "SERVICECONTRACT";
            contract.contractmaster_CONTRACTTYPEID = 1;
            contract.contractmaster_SERVICECHARGE = General.Parse<double>(txtchargservice.Text.Trim().ToString());
        }
        else if(rdtransportcontract.Checked == true)
        {
            contract.contractmaster_CONTRACTTYPE = "TRANSPORTCONTRACT";
            contract.contractmaster_CONTRACTTYPEID = 2;
            contract.contractmaster_RATE = General.Parse<double>(txtrate.Text.Trim().ToString());
        }
        contract.contractmaster_CONTRACTDATE = txtdate.Text.Trim().ToString();
        contract.contractmaster_REFNO = txtrefno.Text.Trim().ToString();
        contract.contractmaster_REFDATE = txtrefdate.Text.Trim().ToString();
        contract.contractmaster_STARTDATE = txtcontractdate.Text.Trim().ToString();
        contract.contractmaster_ENDDATE = txtexpirydate.Text.Trim().ToString();
        contract.contractmaster_QUANTITY = General.Parse<double>(txtquantity.Text.Trim().ToString());
        contract.contractmaster_QUANTITYPER = ddlquantitytype.SelectedItem.Text.ToString();
        contract.contractmaster_PARTYID = General.Parse<int>(ddlparty.SelectedValue.Trim().ToString());
        contract.contractmaster_PARTYCONTACTPERSONID = General.Parse<int>(ddlcontactperson.SelectedValue.Trim().ToString());
        contract.contractmaster_CMPID = General.Parse<int>(Session["cmpid"].ToString());
        contract.contractmaster_BRANCHID = General.Parse<int>(Session["branchid"].ToString());
        contract.contractmaster_STATUS = 0;
        if (contract.Insert(true, "contractmaster"))
        {
            string sql = "SELECT MAX(CONTRACTID) FROM CONTRACTMASTER CM WHERE CM.STATUS=0 AND" +
                       " CM.PARTYID=" + ddlparty.SelectedValue.Trim().ToString() + " AND CM.PARTYCONTACTPERSONID=" + ddlcontactperson.SelectedValue.Trim().ToString();
            Handler hdn = new Handler();
            DataTable dt = hdn.GetTable(sql);
            ViewState["contractid"] = dt.Rows[0][0].ToString().Trim();

            string sqltax = "SELECT SRNO,TAXNAME,TAXUNIT,TAXVALUE FROM TAXMASTER TM WHERE TM.STATUS=0 AND TM.TAXNAME='SERVICE TAX'"; ;
            Handler hdntax = new Handler();
            DataTable dttax = hdntax.GetTable(sqltax);
            taxationmaster objtax = new taxationmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
            objtax.taxationmaster_SRNO = -1;
            objtax.taxationmaster_TAXPARTYTYPE = "CONTRACT";
            objtax.taxationmaster_TAXPARTYID = General.Parse<int>(ViewState["contractid"].ToString());
            objtax.taxationmaster_TAXID = General.Parse<int>(dttax.Rows[0]["SRNO"].ToString());
            objtax.taxationmaster_TAXNAME = dttax.Rows[0]["TAXNAME"].ToString().Trim();
            objtax.taxationmaster_TAXVALUE = General.Parse<double>(dttax.Rows[0]["TAXVALUE"].ToString().Trim());
            objtax.taxationmaster_TAXUNIT = dttax.Rows[0]["TAXUNIT"].ToString().Trim();
            objtax.taxationmaster_STATUS = 0;
            if (objtax.Insert(true, "taxationmaster"))
            {
            }
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg1", "ShowDiv2();", true);
    }
    protected void btnsubmit2_Click(object sender, EventArgs e)
    {
        contractdetailmaster contractdetail = new contractdetailmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
        contractdetail.contractdetailmaster_SRNO = -1;
        contractdetail.contractdetailmaster_CONTRACTID = General.Parse<int>(ViewState["contractid"].ToString());
        if (txtworkdetails.Text.Trim().ToString() != string.Empty)
        {
            contractdetail.contractdetailmaster_WORKDETAILS = txtworkdetails.Text.Trim().ToString();
        }
        if (txtdeliverydetails.Text.Trim().ToString() != string.Empty)
        {
            contractdetail.contractdetailmaster_DELIVERYDETAILS = txtdeliverydetails.Text.Trim().ToString();
        }
        contractdetail.contractdetailmaster_STATUS = 0;
        if (contractdetail.Insert(true, "contractdetailmaster"))
        {
        }

        foreach (ListItem aListItem in chkqualityguarantity.Items)
        {
            if (aListItem.Selected)
            {
                termstable objterms = new termstable(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                objterms.termstable_SRNO = -1;
                objterms.termstable_STATUS = 0;
                objterms.termstable_PARTYTYPE = "CONTRACT";
                objterms.termstable_PARTYID = General.Parse<int>(ViewState["contractid"].ToString());
                objterms.termstable_TERMSID = General.Parse<int>(aListItem.Value.ToString().Trim());
                objterms.termstable_TERMS = aListItem.Text.Trim().ToString();
                objterms.termstable_TERMSVALUE = -1;
                if (objterms.Insert(true, "termstable"))
                {
                }
            }
        }
        foreach (ListItem aListItem in chkqualityanalysis.Items)
        {
            if (aListItem.Selected)
            {
                termstable objterms = new termstable(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                objterms.termstable_SRNO = -1;
                objterms.termstable_STATUS = 0;
                objterms.termstable_PARTYTYPE = "CONTRACT";
                objterms.termstable_PARTYID = General.Parse<int>(ViewState["contractid"].ToString());
                objterms.termstable_TERMSID = General.Parse<int>(aListItem.Value.ToString().Trim());
                objterms.termstable_TERMS = aListItem.Text.Trim().ToString();
                objterms.termstable_TERMSVALUE = -1;
                if (objterms.Insert(true, "termstable"))
                {
                }
            }
        }
        foreach (ListItem aListItem in chkpaymentterms.Items)
        {
            if (aListItem.Selected)
            {
                termstable objterms = new termstable(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                objterms.termstable_SRNO = -1;
                objterms.termstable_STATUS = 0;
                objterms.termstable_PARTYTYPE = "CONTRACT";
                objterms.termstable_PARTYID = General.Parse<int>(ViewState["contractid"].ToString());
                objterms.termstable_TERMSID = General.Parse<int>(aListItem.Value.ToString().Trim());
                objterms.termstable_TERMS = aListItem.Text.Trim().ToString();
                objterms.termstable_TERMSVALUE = -1;
                if (objterms.Insert(true, "termstable"))
                {
                }
            }
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg1", "ShowDiv3();", true);
    }
    protected void btnsubmit3_Click(object sender, EventArgs e)
    {

        foreach (ListItem aListItem in chklowerbtu.Items)
        {
            if (aListItem.Selected)
            {
                termstable objterms = new termstable(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                objterms.termstable_SRNO = -1;
                objterms.termstable_STATUS = 0;
                objterms.termstable_PARTYTYPE = "CONTRACT";
                objterms.termstable_PARTYID = General.Parse<int>(ViewState["contractid"].ToString());
                objterms.termstable_TERMSID = General.Parse<int>(aListItem.Value.ToString().Trim());
                objterms.termstable_TERMS = aListItem.Text.Trim().ToString();
                objterms.termstable_TERMSVALUE = -1;
                if (objterms.Insert(true, "termstable"))
                {
                }
            }
        }
        foreach (ListItem aListItem in chkashpenalty.Items)
        {
            if (aListItem.Selected)
            {
                termstable objterms = new termstable(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                objterms.termstable_SRNO = -1;
                objterms.termstable_STATUS = 0;
                objterms.termstable_PARTYTYPE = "CONTRACT";
                objterms.termstable_PARTYID = General.Parse<int>(ViewState["contractid"].ToString());
                objterms.termstable_TERMSID = General.Parse<int>(aListItem.Value.ToString().Trim());
                objterms.termstable_TERMS = aListItem.Text.Trim().ToString();
                objterms.termstable_TERMSVALUE = -1;
                if (objterms.Insert(true, "termstable"))
                {
                }
            }
        }
        contractdetailmaster contractdetail = new contractdetailmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
        contractdetail.contractdetailmaster_SRNO = -1;
        contractdetail.contractdetailmaster_CONTRACTID = -1;
        if (txtdeleysupply.Text.Trim().ToString() != string.Empty)
        {
            contractdetail.contractdetailmaster_SUPPLYDELEYDETAILS = txtdeleysupply.Text.Trim().ToString();
        }
        if (txtexcesssupply.Text.Trim().ToString() != string.Empty)
        {
            contractdetail.contractdetailmaster_EXCESSSUPPLY = txtexcesssupply.Text.Trim().ToString();
        }
        if (txtweighment.Text.Trim().ToString() != string.Empty)
        {
            contractdetail.contractdetailmaster_WEIGHTMENT = txtweighment.Text.Trim().ToString();
        }
        if (txtsecuritydeposit.Text.Trim().ToString() != string.Empty)
        {
            contractdetail.contractdetailmaster_SECURITYDEPOSIT = txtsecuritydeposit.Text.Trim().ToString();
        }
        contractdetail.contractdetailmaster_STATUS = -1;
        string condition = "CONTRACTID=" + ViewState["contractid"].ToString() +" AND STATUS=0";
        if (contractdetail.Insert(false, "contractdetailmaster",condition))
        {
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg1", "ShowDiv4();", true);
    }
    protected void btnsubmit4_Click(object sender, EventArgs e)
    {
        contractdetailmaster contractdetail = new contractdetailmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
        contractdetail.contractdetailmaster_SRNO = -1;
        contractdetail.contractdetailmaster_CONTRACTID = -1;
        if (txtjurisdiction.Text.Trim().ToString() != string.Empty)
        {
            contractdetail.contractdetailmaster_JURISDICTION = txtjurisdiction.Text.Trim().ToString();
        }
        if (txtcompliancelaw.Text.Trim().ToString() != string.Empty)
        {
            contractdetail.contractdetailmaster_COMPLIANCELAW = txtcompliancelaw.Text.Trim().ToString();
        }
        if (txtconfidential.Text.Trim().ToString() != string.Empty)
        {
            contractdetail.contractdetailmaster_CONFIDENTIALITY = txtconfidential.Text.Trim().ToString();
        }
        if (txtindenmnity.Text.Trim().ToString() != string.Empty)
        {
            contractdetail.contractdetailmaster_INDEMNITY = txtindenmnity.Text.Trim().ToString();
        }
        contractdetail.contractdetailmaster_STATUS = -1;
        string condition = "CONTRACTID=" + ViewState["contractid"].ToString() + " AND STATUS=0";
        if (contractdetail.Insert(false, "contractdetailmaster", condition))
        {
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg1", "ShowDiv5();", true);
    }
    protected void btnsubmit5_Click(object sender, EventArgs e)
    {
        contractdetailmaster contractdetail = new contractdetailmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
        contractdetail.contractdetailmaster_SRNO = -1;
        contractdetail.contractdetailmaster_CONTRACTID = -1;
        if (txtarbitration.Text.Trim().ToString() != string.Empty)
        {
            contractdetail.contractdetailmaster_ARBITRATION = txtarbitration.Text.Trim().ToString();
        }
        if (txttermination.Text.Trim().ToString() != string.Empty)
        {
            contractdetail.contractdetailmaster_TERMINATION = txttermination.Text.Trim().ToString();
        }
        if (txtforcemajeure.Text.Trim().ToString() != string.Empty)
        {
            contractdetail.contractdetailmaster_FORCEMAJEURE = txtforcemajeure.Text.Trim().ToString();
        }
        contractdetail.contractdetailmaster_STATUS = -1;
        string condition = "CONTRACTID=" + ViewState["contractid"].ToString() + " AND STATUS=0";
        if (contractdetail.Insert(false, "contractdetailmaster", condition))
        {
        }
        foreach (ListItem aListItem in chkotherterms.Items)
        {
            if (aListItem.Selected)
            {
                termstable objterms = new termstable(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                objterms.termstable_SRNO = -1;
                objterms.termstable_STATUS = 0;
                objterms.termstable_PARTYTYPE = "CONTRACT";
                objterms.termstable_PARTYID = General.Parse<int>(ViewState["contractid"].ToString());
                objterms.termstable_TERMSID = General.Parse<int>(aListItem.Value.ToString().Trim());
                objterms.termstable_TERMS = aListItem.Text.Trim().ToString();
                objterms.termstable_TERMSVALUE = -1;
                if (objterms.Insert(true, "termstable"))
                {
                }
            }
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg1", "ShowDiv6();", true);
    }
    protected void btnsubmit6_Click(object sender, EventArgs e)
    {
        contractdetailmaster contractdetail = new contractdetailmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
        contractdetail.contractdetailmaster_SRNO = -1;
        contractdetail.contractdetailmaster_CONTRACTID = -1;
        if (txtpartyvalue.Text.Trim().ToString() != string.Empty)
        {
            contractdetail.contractdetailmaster_PARTYVALUE = txtpartyvalue.Text.Trim().ToString();
        }
        
        contractdetail.contractdetailmaster_STATUS = -1;
        string condition = "CONTRACTID=" + ViewState["contractid"].ToString() + " AND STATUS=0";
        if (contractdetail.Insert(false, "contractdetailmaster", condition))
        {
        }
        Response.Redirect("contractlist.aspx");
     //   ScriptManager.RegisterStartupScript(this, this.GetType(), "msg1", "ShowDiv6();", true);
    }

  
}