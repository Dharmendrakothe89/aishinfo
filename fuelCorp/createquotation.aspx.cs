using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class createquotation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("default.aspx");
            }
            Session.Remove("tax");
            txtfinaltax.Text = "0";
            txtexpirydate.Text = "";
            txtdate.Text = "";
            FillPersonData();
            FillParty();
            gvtax.DataSource = null;
            gvtax.DataBind();
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


        string sqlterms = "SELECT TM.SRNO,TM.TERMS FROM TERMSCONDITION TM WHERE TM.STATUS=0 AND (CATEGORY='QUOTATION' OR CATEGORY='ALL')";
        Handler hdnterms = new Handler();
        DataTable dtterms = hdnterms.GetTable(sqlterms);
        chktermslist.DataSource = dtterms;
        chktermslist.DataTextField = "TERMS";
        chktermslist.DataValueField = "SRNO";
        chktermslist.DataBind();
    }
    protected void ddlcoaltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCoalGrade(ddlcoaltype.SelectedValue.ToString().Trim());

    }
    public void FillCoalGrade( string coalid)
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
            //ddlcontactperson.Items.Insert(0, "-- Person --");
        
        //else
        //{
        //   ddlcontactperson.SelectedIndex = 0;
        //   ddlcontactperson.Enabled = false;

        //}
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
    protected void btntaxsubmit_Click(object sender, EventArgs e)
    {
        int match=0;
        DataTable dt = new DataTable();
        if (Session["tax"] != null)
        {
            dt = (DataTable) Session["tax"];
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
        if(match == 0)
        {   
            DataRow dr = dt.NewRow();
            dr["TAXNAME"] = ddlcoaltax.SelectedItem.Text.ToString().Trim();
            dr["TAXVALUE"] = ddlcoaltax.SelectedValue.ToString().Trim();
            dt.Rows.Add(dr);

            double amount = ((General.Parse<double>(txtfinalcoalcost.Text.Trim())) * (General.Parse<double>(ddlcoaltax.SelectedValue.ToString().Trim()))) / 100;
            if (txtfinaltax.Text == string.Empty)
            {
                txtfinaltax.Text= amount.ToString();
            }
            else
            {
                txtfinaltax.Text = ((General.Parse<double>(txtfinaltax.Text)) + amount).ToString();
            }
            
        }
        Session["tax"] = dt;
        gvtax.DataSource =(DataTable) Session["tax"];
        gvtax.DataBind();
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        
        string partyid = string.Empty;
        string contactperson = string.Empty;
        partyid = ddlparty.SelectedValue.ToString();
        quotationmaster objquotation = new quotationmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
        objquotation.quotationmaster_QUOTATIONID = -1;
        objquotation.quotationmaster_PARTYID=General.Parse<int>(partyid);
         objquotation.quotationmaster_PARTYTYPE = 1;
        objquotation.quotationmaster_CONTACTPERSON=General.Parse<int>(contactperson);
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
        if (objquotation.Insert(true, "quotationmaster"))
        {
            string sqlmax = "SELECT MAX(QUOTATIONID) AS QUOTATIONID FROM QUOTATIONMASTER GM WHERE PARTYID=" + partyid;
            Handler hdnmax = new Handler();
            DataTable dtmax = hdnmax.GetTable(sqlmax);

            quotationspecification objspecification = new quotationspecification(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
            objspecification.quotationspecification_SRNO = -1;
            objspecification.quotationspecification_QUOTATIONID = General.Parse<int>(dtmax.Rows[0][0].ToString());
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
            if (objspecification.Insert(true, "quotationspecification"))
            {
            }
            for (int t = 0; t < ((DataTable)Session["tax"]).Rows.Count; t++)
            {
                string sqltax = "SELECT SRNO,TAXNAME,TAXUNIT,TAXVALUE FROM TAXMASTER TM WHERE TM.STATUS=0 AND TM.TAXNAME='" + ((DataTable)Session["tax"]).Rows[t]["TAXNAME"].ToString().Trim() + "' AND TM.TAXVALUE=" + ((DataTable)Session["tax"]).Rows[t]["TAXVALUE"].ToString().Trim();
                Handler hdntax = new Handler();
                DataTable dttax = hdntax.GetTable(sqltax);
                taxationmaster objtax = new taxationmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                objtax.taxationmaster_SRNO = -1;
                objtax.taxationmaster_TAXPARTYTYPE = "QUOTATION";
                objtax.taxationmaster_TAXPARTYID = General.Parse<int>(dtmax.Rows[0][0].ToString());
                objtax.taxationmaster_TAXID = General.Parse<int>(dttax.Rows[0]["SRNO"].ToString());
                objtax.taxationmaster_TAXNAME = dttax.Rows[0]["TAXNAME"].ToString().Trim();
                objtax.taxationmaster_TAXVALUE = General.Parse<double>(dttax.Rows[0]["TAXVALUE"].ToString().Trim());
                objtax.taxationmaster_TAXUNIT = dttax.Rows[0]["TAXUNIT"].ToString().Trim();
                objtax.taxationmaster_STATUS = 0;
                if (objtax.Insert(true, "taxationmaster"))
                {
                }
            }
            foreach (ListItem aListItem in chktermslist.Items)
            {
                if (aListItem.Selected)
                {
                    termstable objterms = new termstable(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                    objterms.termstable_SRNO = -1;
                    objterms.termstable_STATUS = 0;
                    objterms.termstable_PARTYTYPE = "QUOTATION";
                    objterms.termstable_PARTYID = General.Parse<int>(dtmax.Rows[0][0].ToString());
                    objterms.termstable_TERMSID = General.Parse<int>(aListItem.Value.ToString().Trim());
                    objterms.termstable_TERMS = aListItem.Text.Trim().ToString();
                    objterms.termstable_TERMSVALUE =-1;
                    if (objterms.Insert(true, "termstable"))
                    {
                    }
                }
            }
                     
           
        }
        Response.Redirect("Quotationlist.aspx?ID=1");
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