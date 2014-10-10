using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
public partial class quotationdetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("default.aspx");
            }
            if (Request.QueryString["QUOTATIONID"] != null)
            {
                FillPersonData();
                FillParty();
                ViewState["QUOTATIONID"] = Request.QueryString["QUOTATIONID"].ToString();
                FillQuotationData(ViewState["QUOTATIONID"].ToString());
                DisableControl();
            }
        }
    }
    private void DisableControl()
    {
        txtdate.Enabled = false;
        txtexpirydate.Enabled = false;
        txtrefno.Enabled = false;
        ddlparty.Enabled = false;
        ddlcoalsource.Enabled = false;
        txtcode.Enabled = false;
        txtaddress.Enabled = false;
        ddlcontactperson.Enabled = false;
        txtnewpartyname.Enabled = false;
        txtnewpartycode.Enabled = false;
        txtnewpartyaddress.Enabled = false;
        txtnewpartyno.Enabled = false;
        txtpersonname.Enabled = false;
        ddldesignation.Enabled = false;
        txtemailid.Enabled = false;
        txtpersonno.Enabled = false;
        txtcoalquantity.Enabled = false;
        txtcoalrate.Enabled = false;

        ddlcoaltype.Enabled = false;
        ddlcoalgrade.Enabled = false;
        txtcoalsizemin.Enabled = false;
        txtcoalsizemax.Enabled = false;
        txtgcv.Enabled = false;
        txtgcverror.Enabled = false;
        txtmoisture.Enabled = false;
        txtmoistureerror.Enabled = false;
        ddlcoaltax.Enabled = false;
        btntaxsubmit.Enabled = false;
        txtfinalcoalcost.Enabled = false;
        txtfinaltax.Enabled = false;
        txttransportationcost.Enabled = false;
        txtgrandtotal.Enabled = false;
        chktermslist.Enabled = false;
        btnsubmit.Visible= false;
        
    }
    private void EnableControl()
    {
        txtdate.Enabled = true;
        txtexpirydate.Enabled = true;
        txtrefno.Enabled = true;
        ddlcoalsource.Enabled = true;
        txtcode.Enabled = true;
        txtaddress.Enabled = true;
        ddlcontactperson.Enabled = true;
        txtnewpartyname.Enabled = true;
        txtnewpartycode.Enabled = true;
        txtnewpartyaddress.Enabled = true;
        txtnewpartyno.Enabled = true;
        txtpersonname.Enabled = true;
        ddldesignation.Enabled = true;
        txtemailid.Enabled = true;
        txtpersonno.Enabled = true;
        txtcoalquantity.Enabled = true;
        txtcoalrate.Enabled = true;

        ddlcoaltype.Enabled = true;
        ddlcoalgrade.Enabled = true;
        txtcoalsizemin.Enabled = true;
        txtcoalsizemax.Enabled = true;
        txtgcv.Enabled = true;
        txtgcverror.Enabled = true;
        txtmoisture.Enabled = true;
        txtmoistureerror.Enabled = true;
        ddlcoaltax.Enabled = true;
        btntaxsubmit.Enabled = true;

        //txtfinalcoalcost.Enabled = true;
        //txtfinaltax.Enabled = true;
        txttransportationcost.Enabled = true;
        //txtgrandtotal.Enabled = true;
        chktermslist.Enabled = true;
        btnsubmit.Visible = true;

    }
    private void FillQuotationData(string quotationid)
    {
        string sql = "SELECT * FROM QUOTATIONMASTER QM WHERE QM.QUOTATIONID=" + quotationid;
        Handler hdn = new Handler();
        DataTable dt = hdn.GetTable(sql);

        string sql1 = "SELECT * FROM QUOTATIONSPECIFICATION QM WHERE QM.QUOTATIONID=" + quotationid;
        Handler hdn1 = new Handler();
        DataTable dt1 = hdn.GetTable(sql1);

        txtdate.Text = dt.Rows[0]["DATE"].ToString().Trim();
        txtexpirydate.Text = dt.Rows[0]["EXPIRYDATE"].ToString().Trim();
        txtrefno.Text = dt.Rows[0]["REFNO"].ToString().Trim();
        ddlcoalsource.SelectedValue = dt1.Rows[0]["COALSOURCEID"].ToString().Trim();
        ddlparty.SelectedValue = dt.Rows[0]["PARTYID"].ToString().Trim();
        FillPartydetails(dt.Rows[0]["PARTYID"].ToString().Trim());
        ddlcontactperson.SelectedValue = dt.Rows[0]["CONTACTPERSON"].ToString().Trim();

        txtcoalquantity.Text = dt1.Rows[0]["COALQUANTITY"].ToString().Trim();
        txtcoalrate.Text = dt1.Rows[0]["COALRATE"].ToString().Trim();

        string sqlgrade = "SELECT CM.SRNO FROM COALMASTER CM WHERE CM.SRNO=(SELECT COALTYPEID FROM COALMASTER CM1 WHERE CM1.SRNO=" + dt1.Rows[0]["COALTYPEID"].ToString().Trim() + ")";
        Handler hdngrade = new Handler();
        DataTable dtgrade = hdngrade.GetTable(sqlgrade);
        ddlcoaltype.SelectedValue = dtgrade.Rows[0]["SRNO"].ToString().Trim();
        FillCoalGrade(dtgrade.Rows[0]["SRNO"].ToString().Trim());
        ddlcoalgrade.SelectedValue = dt1.Rows[0]["COALTYPEID"].ToString().Trim();

        txtcoalsizemin.Text = dt1.Rows[0]["COALSIZEMIN"].ToString().Trim();
        txtcoalsizemax.Text = dt1.Rows[0]["COALSIZEMAX"].ToString().Trim();
        txtgcv.Text = dt1.Rows[0]["GCV"].ToString().Trim();
        txtgcverror.Text = dt1.Rows[0]["GCVERROR"].ToString().Trim();
        txtmoisture.Text = dt1.Rows[0]["MOISTURE"].ToString().Trim();
        txtmoistureerror.Text = dt1.Rows[0]["MOISTUREERROR"].ToString().Trim();

        txtfinalcoalcost.Text = dt.Rows[0]["COALCOST"].ToString().Trim();
        txtfinaltax.Text = dt.Rows[0]["TAXCOST"].ToString().Trim();
        txttransportationcost.Text = dt.Rows[0]["TRANSPORTATIONCOST"].ToString().Trim();
        txtgrandtotal.Text = dt.Rows[0]["TOTALCOST"].ToString().Trim();

        string sqltax = "SELECT * FROM taxationmaster TM WHERE TAXPARTYTYPE='QUOTATION' AND TAXPARTYID=" + quotationid;
        Handler hdntax = new Handler();
        DataTable dttax = hdntax.GetTable(sqltax);
        if (dttax.Rows.Count > 0)
        {
            Session["tax"] = dttax;
            gvtax.DataSource = dttax;
            gvtax.DataBind();
        }
        else
        {
            gvtax.DataSource = null;
            gvtax.DataBind();
        }

        string sql2 = "SELECT T.TERMSID,T.TERMS FROM TERMSTABLE T WHERE T.STATUS=0 AND T.PARTYTYPE='QUOTATION' AND T.PARTYID=" + quotationid.ToString().Trim();
        Handler hdn2 = new Handler();
        DataTable dt2 = hdn2.GetTable(sql2);
        if (dt1.Rows.Count > 0)
        {
            chktermslist.DataSource = dt2;
            chktermslist.DataTextField = "TERMS";
            chktermslist.DataValueField = "TERMSID";
            chktermslist.DataBind();

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
        FillPartydetails(dtdepartment.Rows[0]["SRNO"].ToString().Trim());
    }
    private void FillPartydetails(string partyid)
    {
        string sqldepartment = "SELECT PM.PARTYNAME,PM.PARTYCODE,PAM.ADDRESS FROM PARTYMASTER PM INNER JOIN PARTYADDRESSMASTER PAM ON PM.SRNO=PAM.PARTYID" +
                               " WHERE PAM.ADDRESSTYPE=1 AND PM.SRNO=" + partyid;
        Handler hdndepartment = new Handler();
        DataTable dtdepartment = hdndepartment.GetTable(sqldepartment);
        ddlcontactperson.Enabled = true;
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
    private void FillPersonData()
    {


        string sqldesignation = "SELECT LM.NAME,LM.SRNO FROM LOOKUPMASTER LM INNER JOIN LOOKUPHEADINGMASTER LHM ON LHM.SRNO=LM.HEADID WHERE LM.STATUS=0 AND LHM.STATUS=0 AND LHM.HEAD='DESIGNATION' ORDER BY LM.NAME";
        Handler hdndesignation = new Handler();
        DataTable dtdesignation = hdndesignation.GetTable(sqldesignation);
        ddldesignation.DataSource = dtdesignation;
        ddldesignation.DataTextField = "NAME";
        ddldesignation.DataValueField = "SRNO";
        ddldesignation.DataBind();


        string sqlcoalsource = "SELECT LM.NAME,LM.SRNO FROM LOOKUPMASTER LM INNER JOIN LOOKUPHEADINGMASTER LHM ON LHM.SRNO=LM.HEADID WHERE LM.STATUS=0 AND LHM.STATUS=0 AND LHM.HEAD='COAL SOURCE' ORDER BY LM.NAME";
        Handler hdncoalsource = new Handler();
        DataTable dtcoalsource = hdncoalsource.GetTable(sqlcoalsource);
        ddlcoalsource.DataSource = dtcoalsource;
        ddlcoalsource.DataTextField = "NAME";
        ddlcoalsource.DataValueField = "SRNO";
        ddlcoalsource.DataBind();


        string sqlcoaltype = "SELECT CT.COALTYPE,CT.SRNO FROM COALMASTER CT WHERE CT.STATUS=0 AND COALTYPEID IS NULL ORDER BY CT.COALTYPE";
        Handler hdncoaltype = new Handler();
        DataTable dtcoaltype = hdncoaltype.GetTable(sqlcoaltype);
        ddlcoaltype.DataSource = dtcoaltype;
        ddlcoaltype.DataTextField = "COALTYPE";
        ddlcoaltype.DataValueField = "SRNO";
        ddlcoaltype.DataBind();
        FillCoalGrade(dtcoaltype.Rows[0]["SRNO"].ToString().Trim());

        string sqltax = "SELECT TM.TAXVALUE AS TAXID, TM.TAXNAME FROM TAXMASTER TM WHERE TM.STATUS=0 ORDER BY TM.TAXNAME";
        Handler hdntax = new Handler();
        DataTable dttax = hdntax.GetTable(sqltax);
        ddlcoaltax.DataSource = dttax;
        ddlcoaltax.DataTextField = "TAXNAME";
        ddlcoaltax.DataValueField = "TAXID";
        ddlcoaltax.DataBind();
        ddlcoaltax.Items.Insert(0, "-- Tax --");


        
    }
    protected void ddlcoaltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCoalGrade(ddlcoaltype.SelectedValue.ToString().Trim());

    }
    public void FillCoalGrade(string coalid)
    {
        string sql1 = "SELECT SRNO,GRADE FROM COALMASTER CM WHERE COALTYPEID IS NOT NULL AND COALTYPEID=" + coalid.ToString().Trim();
        Handler hdn1 = new Handler();
        DataTable dt1 = hdn1.GetTable(sql1);
        ddlcoalgrade.DataSource = dt1;
        ddlcoalgrade.DataTextField = "GRADE";
        ddlcoalgrade.DataValueField = "SRNO";
        ddlcoalgrade.DataBind();
    }
    protected void ddlparty_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillPartydetails(ddlparty.SelectedValue);
    }
    protected void btnedit_Click(object sender, EventArgs e)
    {
        EnableControl();
        string sqlterms = "SELECT TM.SRNO,TM.TERMS FROM TERMSCONDITION TM WHERE TM.STATUS=0 AND (CATEGORY='QUOTATION' OR CATEGORY='ALL')";
        Handler hdnterms = new Handler();
        DataTable dtterms = hdnterms.GetTable(sqlterms);
        chktermslist.DataSource = dtterms;
        chktermslist.DataTextField = "TERMS";
        chktermslist.DataValueField = "SRNO";
        chktermslist.DataBind();
        trtax1.Style.Add("display", "block");
        trtax2.Style.Add("display", "block");
    }
    
    protected void btntaxsubmit_Click(object sender, EventArgs e)
    {
        int match = 0;
        DataTable dt = new DataTable();
        if (Session["tax"] != null)
        {
            dt = (DataTable)Session["tax"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["TAXNAME"].ToString().Trim() == ddlcoaltax.SelectedItem.Text.ToString().Trim() && dt.Rows[i]["TAXVALUE"].ToString().Trim() == ddlcoaltax.SelectedValue.ToString().Trim())
                {
                    match = 1;
                    break;
                }
            }
        }
        else
        {
            dt.Columns.Add("TAXNAME");
            dt.Columns.Add("TAXVALUE");
        }
        if (match == 0)
        {
            DataRow dr = dt.NewRow();
            dr["TAXNAME"] = ddlcoaltax.SelectedItem.Text.ToString().Trim();
            dr["TAXVALUE"] = ddlcoaltax.SelectedValue.ToString().Trim();
            dt.Rows.Add(dr);

            double amount = ((General.Parse<double>(txtfinalcoalcost.Text.Trim())) * (General.Parse<double>(ddlcoaltax.SelectedValue.ToString().Trim()))) / 100;
            if (txtfinaltax.Text == string.Empty)
            {
                txtfinaltax.Text = amount.ToString();
            }
            else
            {
                txtfinaltax.Text = ((General.Parse<double>(txtfinaltax.Text)) + amount).ToString();
            }

        }
        Session["tax"] = dt;
        gvtax.DataSource = (DataTable)Session["tax"];
        gvtax.DataBind();
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {

        string partyid = string.Empty;
        string contactperson = string.Empty;
        partyid = ddlparty.SelectedValue.ToString();
        quotationmaster objquotation = new quotationmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
        objquotation.quotationmaster_QUOTATIONID = -1;
        objquotation.quotationmaster_PARTYID = General.Parse<int>(partyid);
        objquotation.quotationmaster_PARTYTYPE = 1;
        objquotation.quotationmaster_CONTACTPERSON = General.Parse<int>(contactperson);
        objquotation.quotationmaster_DATE = txtdate.Text.Trim().ToString();
        objquotation.quotationmaster_EXPIRYDATE = txtexpirydate.Text.Trim().ToString();
        if (txtrefno.Text.Trim().ToString() != string.Empty)
        {
            objquotation.quotationmaster_REFNO = txtrefno.Text.Trim().ToString();
        }
        objquotation.quotationmaster_COALCOST = General.Parse<double>(txtfinalcoalcost.Text.Trim().ToString());
        objquotation.quotationmaster_TAXCOST = General.Parse<double>(txtfinaltax.Text.Trim().ToString());
        objquotation.quotationmaster_TRANSPORTATIONCOST = General.Parse<double>(txttransportationcost.Text.Trim().ToString());
        objquotation.quotationmaster_TOTALCOST = General.Parse<double>(txtfinalcoalcost.Text.Trim().ToString());
        objquotation.quotationmaster_STATUS = 0;
        string condition = "QUOTATIONID=" + ViewState["QUOTATIONID"].ToString();
        if (objquotation.Insert(false, "quotationmaster", condition))
        {
            quotationspecification objspecification = new quotationspecification(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
            objspecification.quotationspecification_SRNO = -1;
            objspecification.quotationspecification_QUOTATIONID = -1;
            objspecification.quotationspecification_COALTYPE = ddlcoalgrade.SelectedItem.Text;
            objspecification.quotationspecification_COALTYPEID = General.Parse<int>(ddlcoalgrade.SelectedValue.ToString());
            objspecification.quotationspecification_COALSOURCE = ddlcoalsource.SelectedItem.Text;
            objspecification.quotationspecification_COALSOURCEID = General.Parse<int>(ddlcoalsource.SelectedValue.ToString());
            objspecification.quotationspecification_COALQUANTITY = General.Parse<double>(txtcoalquantity.Text.Trim().ToString());
            objspecification.quotationspecification_COALRATE = General.Parse<double>(txtcoalrate.Text.Trim().ToString());
            objspecification.quotationspecification_COALSIZEMIN = General.Parse<double>(txtcoalsizemin.Text.Trim().ToString());
            objspecification.quotationspecification_COALSIZEMAX = General.Parse<double>(txtcoalsizemax.Text.Trim().ToString());
            objspecification.quotationspecification_GCV = General.Parse<double>(txtgcv.Text.Trim().ToString());
            objspecification.quotationspecification_GCVERROR = General.Parse<double>(txtgcv.Text.Trim().ToString());
            objspecification.quotationspecification_MOISTURE = General.Parse<double>(txtmoisture.Text.Trim().ToString());
            objspecification.quotationspecification_MOISTUREERROR = General.Parse<double>(txtmoistureerror.Text.Trim().ToString());
            if (objspecification.Insert(false, "quotationspecification", condition))
            {
            }
            SqlConnection Connection = new SqlConnection("Data Source=50.28.62.129,1433;Network Library=DBMSSOCN;Initial Catalog=db_fuel;User ID=fuel;Password= lSa2@11h");
            string qry = "delete from taxationmaster where TAXPARTYTYPE='QUOTATION' AND TAXPARTYID=" + ViewState["QUOTATIONID"].ToString();

            Connection.Open();
            SqlCommand com = new SqlCommand(qry, Connection);
            com.ExecuteNonQuery();
            Connection.Close();

            for (int t = 0; t < ((DataTable)Session["tax"]).Rows.Count; t++)
            {
                string sqltax = "SELECT SRNO,TAXNAME,TAXUNIT,TAXVALUE FROM TAXMASTER TM WHERE TM.STATUS=0 AND TM.TAXNAME='" + ((DataTable)Session["tax"]).Rows[t]["TAXNAME"].ToString().Trim() + "' AND TM.TAXVALUE=" + ((DataTable)Session["tax"]).Rows[t]["TAXVALUE"].ToString().Trim();
                Handler hdntax = new Handler();
                DataTable dttax = hdntax.GetTable(sqltax);
                taxationmaster objtax = new taxationmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                objtax.taxationmaster_SRNO = -1;
                objtax.taxationmaster_TAXPARTYTYPE = "QUOTATION";
                objtax.taxationmaster_TAXPARTYID = General.Parse<int>(ViewState["QUOTATIONID"].ToString());
                objtax.taxationmaster_TAXID = General.Parse<int>(dttax.Rows[0]["SRNO"].ToString());
                objtax.taxationmaster_TAXNAME = dttax.Rows[0]["TAXNAME"].ToString().Trim();
                objtax.taxationmaster_TAXVALUE = General.Parse<double>(dttax.Rows[0]["TAXVALUE"].ToString().Trim());
                objtax.taxationmaster_TAXUNIT = dttax.Rows[0]["TAXUNIT"].ToString().Trim();
                objtax.taxationmaster_STATUS = 0;
                if (objtax.Insert(true, "taxationmaster"))
                {
                }
            }

            string qry1 = "delete from termstable where partytype='QUOTATION' AND PARTYID=" + ViewState["QUOTATIONID"].ToString();

            Connection.Open();
            SqlCommand com1 = new SqlCommand(qry1, Connection);
            com1.ExecuteNonQuery();
            Connection.Close();
            foreach (ListItem aListItem in chktermslist.Items)
            {
                if (aListItem.Selected)
                {
                    termstable objterms = new termstable(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                    objterms.termstable_SRNO = -1;
                    objterms.termstable_STATUS = 0;
                    objterms.termstable_PARTYTYPE = "QUOTATION";
                    objterms.termstable_PARTYID = General.Parse<int>(ViewState["QUOTATIONID"].ToString());
                    objterms.termstable_TERMSID = General.Parse<int>(aListItem.Value.ToString().Trim());
                    objterms.termstable_TERMS = aListItem.Text.Trim().ToString();
                    objterms.termstable_TERMSVALUE = -1;
                    if (objterms.Insert(true, "termstable"))
                    {
                    }
                }
            }


        }
        Response.Redirect("Quotationlist.aspx?ID=2");
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