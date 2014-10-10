using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class createdeliveryorder : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("default.aspx");
            }
            FillData();
            FillMonth();
            FillPaymentData();
            DataTable dt = GetFillDataTable(1);
            ViewState["payment"] = dt;
            paymentrepeater.DataSource = (DataTable)ViewState["payment"];
            paymentrepeater.DataBind();
        }
        if (Request.Params.Get("__EVENTTARGET") == "coal")
        {
            
        }
    }
  
    private void FillData()
    {
        string sqlcolliery = "SELECT CM.SRNO AS COLLIERYID,CM.COLLIERYNAME FROM COLLIERYMASTER CM WHERE STATUS=0";
        Handler hdncolliery = new Handler();
        DataTable dtcolliery = hdncolliery.GetTable(sqlcolliery);
        ddlcolliery.DataSource = dtcolliery;
        ddlcolliery.DataTextField = "COLLIERYNAME";
        ddlcolliery.DataValueField = "COLLIERYID";
        ddlcolliery.DataBind();
        ddlcolliery.Items.Insert(0, "--Select--");

        string sqldepartment = "SELECT LM.PARTYNAME,LM.SRNO FROM PARTYMASTER LM ORDER BY LM.PARTYNAME";
        Handler hdndepartment = new Handler();
        DataTable dtdepartment = hdndepartment.GetTable(sqldepartment);
        ddlparty.DataSource = dtdepartment;
        ddlparty.DataTextField = "PARTYNAME";
        ddlparty.DataValueField = "SRNO";
        ddlparty.DataBind();
        ddlparty.Items.Insert(0, "-- Party --");

        string sqlauction = "SELECT AUCTIONID,AUCTIONNAME FROM AUCTIONMASTER WHERE STATUS=0";
        Handler hdnauction = new Handler();
        DataTable dtauction = hdnauction.GetTable(sqlauction);
        ddlauction.DataSource = dtauction;
        ddlauction.DataTextField = "AUCTIONNAME";
        ddlauction.DataValueField = "AUCTIONID";
        ddlauction.DataBind();
        ddlauction.Items.Insert(0, "-- Select Auction --");

        string sqlcoaltype = "SELECT SRNO,COALTYPE FROM COALMASTER CM WHERE STATUS=0 AND COALTYPEID IS NULL";
        Handler hdncoaltype = new Handler();
        DataTable dtcoaltype = hdncoaltype.GetTable(sqlcoaltype);
        ddlcoaltype.DataSource = dtcoaltype;
        ddlcoaltype.DataTextField = "COALTYPE";
        ddlcoaltype.DataValueField = "SRNO";
        ddlcoaltype.DataBind();
        

        string sqlterms = "SELECT TM.SRNO,TM.TERMS FROM TERMSCONDITION TM WHERE TM.STATUS=0 AND (CATEGORY='DO' OR CATEGORY='ALL')";
        Handler hdnterms = new Handler();
        DataTable dtterms = hdnterms.GetTable(sqlterms);
        chktermslist.DataSource = dtterms;
        chktermslist.DataTextField = "TERMS";
        chktermslist.DataValueField = "SRNO";
        chktermslist.DataBind();
    }
    private void FillMonth()
    {
        string sqlcolliery = "SELECT LM.NAME,LM.SRNO FROM LOOKUPMASTER LM INNER JOIN LOOKUPHEADINGMASTER LHM ON LM.HEADID=LHM.SRNO" +
                             " WHERE LHM.STATUS=0 AND LM.STATUS=0 AND LHM.HEAD='SUPPLY MONTH'";
        Handler hdncolliery = new Handler();
        DataTable dtcolliery = hdncolliery.GetTable(sqlcolliery);
        ddlmonth.DataSource = dtcolliery;
        ddlmonth.DataTextField = "NAME";
        ddlmonth.DataValueField = "SRNO";
        ddlmonth.DataBind();
        ddlmonth.Items.Insert(0, "--Select--");
    }
   
    protected void ddldotype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldotype.SelectedIndex > 0)
        {
            if (ddldotype.SelectedValue == "AUCTION")
            {
                trlinkedcustomer.Style.Add("display", "none");
                trauction.Style.Add("display", "block");
            }
            else if (ddldotype.SelectedValue == "LINKEDCUSTOMER")
            {
                trauction.Style.Add("display", "none");
                trlinkedcustomer.Style.Add("display", "block");
            }
        }
        else
        {
            trlinkedcustomer.Style.Add("display","none");
            trauction.Style.Add("display", "none");
        }
    }
    protected void ddlcolliery_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcolliery.SelectedIndex > 0)
        {
            FillCollieryCode(ddlcolliery.SelectedValue.ToString().Trim());
        }
        else
        {
            txtcollierycode.Text = string.Empty;
            
        }
    }
    private void FillCollieryCode(string collieryid)
    {
        string sqlcolliery = "SELECT CM.COLLIERYCODE FROM COLLIERYMASTER CM WHERE CM.STATUS=0 AND CM.SRNO=" + collieryid;
        Handler hdncolliery = new Handler();
        DataTable dtcolliery = hdncolliery.GetTable(sqlcolliery);
        if (dtcolliery.Rows.Count > 0)
        {
            txtcollierycode.Text = dtcolliery.Rows[0]["COLLIERYCODE"].ToString().Trim();
            
        }
        else
        {
            txtcollierycode.Text = string.Empty;
            
        }
    }
    private void FillPaymentData()
    {
        string sqldepartment = "SELECT LM.NAME,LM.SRNO FROM LOOKUPMASTER LM INNER JOIN LOOKUPHEADINGMASTER LHM ON LHM.SRNO=LM.HEADID WHERE LM.STATUS=0 AND LHM.STATUS=0 AND RTRIM(LHM.HEAD)='PAYMENT TYPE' ORDER BY LM.NAME";
        Handler hdndepartment = new Handler();
        DataTable dtdepartment = hdndepartment.GetTable(sqldepartment);
        ViewState["paymenttype"] = dtdepartment;

        string sqldesignation = "SELECT LM.NAME,LM.SRNO FROM LOOKUPMASTER LM INNER JOIN LOOKUPHEADINGMASTER LHM ON LHM.SRNO=LM.HEADID WHERE LM.STATUS=0 AND LHM.STATUS=0 AND RTRIM(LHM.HEAD)='BANK' ORDER BY LM.NAME";
        Handler hdndesignation = new Handler();
        DataTable dtdesignation = hdndesignation.GetTable(sqldesignation);
        ViewState["bank"] = dtdesignation;


    }
    protected void ddlparty_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlparty.SelectedIndex > 0)
        {
            string sqldepartment = "SELECT PM.PARTYNAME,PM.PARTYCODE,PAM.ADDRESS FROM PARTYMASTER PM INNER JOIN PARTYADDRESSMASTER PAM ON PM.SRNO=PAM.PARTYID" +
                                   " WHERE PAM.ADDRESSTYPE=1 AND PM.SRNO=" + ddlparty.SelectedValue;
            Handler hdndepartment = new Handler();
            DataTable dtdepartment = hdndepartment.GetTable(sqldepartment);
           
            if (dtdepartment.Rows.Count > 0)
            {
                txtcode.Text = dtdepartment.Rows[0]["PARTYCODE"].ToString();
                txtaddress.Text = dtdepartment.Rows[0]["ADDRESS"].ToString();
            }
        }
       
    }
    protected void ddlauction_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlauction.SelectedIndex > 0)
        {
            string sql = "SELECT AUCTIONDATE FROM AUCTIONMASTER WHERE STATUS=0 AND AUCTIONID=" + ddlauction.SelectedValue.Trim().ToString();
            Handler hdn = new Handler();
            DataTable dt = hdn.GetTable(sql);
            if (dt.Rows.Count > 0)
            {
                txtauctiondate.Text = dt.Rows[0]["AUCTIONDATE"].ToString().Trim();
            }
            else
            {
                txtauctiondate.Text = string.Empty;
            }
            
        }
        else
        {
            txtauctiondate.Text = string.Empty;
        }
    }
    protected void ddlcoaltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcoaltype.SelectedIndex > 0)
        {
            string sqlcoaltype = "SELECT GRADE,SRNO FROM COALMASTER CM WHERE STATUS=0 AND COALTYPEID=" + ddlcoaltype.SelectedValue.Trim().ToString();
            
            Handler hdncoaltype = new Handler();
            DataTable dtcoaltype = hdncoaltype.GetTable(sqlcoaltype);
            ddlcoalgrade.DataSource = dtcoaltype;
            ddlcoalgrade.DataTextField = "GRADE";
            ddlcoalgrade.DataValueField = "SRNO";
            ddlcoalgrade.DataBind();
            ddlcoalgrade.Items.Insert(0, "-- Select Coal --");
            ddlcoalgrade.Enabled = true;
        }
        else
        {
            ddlcoalgrade.Enabled = false;
        }
    }
    protected void lnkaddmember_Click(object sender, EventArgs e)
    {
        DataTable dt1 = GetFillPreviousData();
        ViewState["payment"] = dt1;
        DataTable dt = GetFillDataTable(1);

        if (ViewState["payment"] != null)
        {
            ((DataTable)ViewState["payment"]).Merge(dt);

        }
        else
        {
            ViewState["payment"] = dt;
        }
        paymentrepeater.DataSource = (DataTable)ViewState["payment"];
        paymentrepeater.DataBind();
    }
    private DataTable GetFillPreviousData()
    {
        DataTable dtData = new DataTable();
        dtData.Columns.Add("SRNO");
        dtData.Columns.Add("PMTTYPE");
        dtData.Columns.Add("PMTNO");
        dtData.Columns.Add("PMTDATE");
        dtData.Columns.Add("BANKNAME");
        dtData.Columns.Add("PMTAMOUNT");

        for (int i = 0; i < paymentrepeater.Items.Count; i++)
        {
            DropDownList ddlpaymenttype = (DropDownList)paymentrepeater.Items[i].FindControl("ddlpaymenttype");
            TextBox txtpmtno = (TextBox)paymentrepeater.Items[i].FindControl("txtpmtno");

            TextBox txtpmtdate = (TextBox)paymentrepeater.Items[i].FindControl("txtpmtdate");
            DropDownList ddlbank = (DropDownList)paymentrepeater.Items[i].FindControl("ddlbank");
            TextBox txtamount = (TextBox)paymentrepeater.Items[i].FindControl("txtamount");
            
            int count = dtData.Rows.Count;
            dtData.Rows.Add(1);
            dtData.Rows[count]["SRNO"] = (count + 1).ToString();
            dtData.Rows[count]["PMTTYPE"] = ddlpaymenttype.SelectedItem.Text.ToString();

            dtData.Rows[count]["PMTNO"] = txtpmtno.Text.ToString();
            dtData.Rows[count]["PMTDATE"] = txtpmtdate.Text.ToString();
            dtData.Rows[count]["BANKNAME"] = ddlbank.SelectedItem.Text.ToString();
            dtData.Rows[count]["PMTAMOUNT"] = txtamount.Text.ToString();

        }

        return dtData;
    }
    protected void paymentrepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DropDownList ddlpaymenttype = (DropDownList)e.Item.FindControl("ddlpaymenttype");
            DropDownList ddlbank = (DropDownList)e.Item.FindControl("ddlbank");

            ddlpaymenttype.DataSource = (DataTable)ViewState["paymenttype"];
            ddlpaymenttype.DataTextField = "NAME";
            ddlpaymenttype.DataValueField = "SRNO";
            ddlpaymenttype.DataBind();
            //ddlpaymenttype.Items.Insert(0, "-- Payment Type --");


            ddlbank.DataSource = (DataTable)ViewState["bank"];
            ddlbank.DataTextField = "NAME";
            ddlbank.DataValueField = "SRNO";
            ddlbank.DataBind();
            //ddlbank.Items.Insert(0, "-- Bank --");

        }
    }
    private DataTable GetFillDataTable(int value)
    {
        DataTable dtData = new DataTable();
        dtData.Columns.Add("SRNO");
        dtData.Columns.Add("PMTTYPE");
        dtData.Columns.Add("PMTNO");
        dtData.Columns.Add("PMTDATE");
        dtData.Columns.Add("BANKNAME");
        dtData.Columns.Add("PMTAMOUNT");
        
        for (int i = 0; i < value; i++)
        {
            DataRow dtrow = dtData.NewRow();
            dtData.Rows.Add(dtrow);
            dtData.Rows[i]["SRNO"] = i + 1;

        }

        return dtData;
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        domaster objdomaster = new domaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
        objdomaster.domaster_DOID = -1;
        objdomaster.domaster_DOFOR = ddldotype.SelectedValue.Trim().ToString();
        objdomaster.domaster_COLLIERYID = General.Parse<int>(ddlcolliery.SelectedValue.Trim().ToString());
        objdomaster.domaster_DONO = txtdono.Text.Trim();
        objdomaster.domaster_CMPID = General.Parse<int>(Session["cmpid"].ToString().Trim());
        objdomaster.domaster_DODATE = txtdodate.Text.Trim();
        if (ddldotype.SelectedValue.Trim().ToString() == "AUCTION")
        {
            objdomaster.domaster_PARTYID = General.Parse<int>(ddlauction.SelectedValue.Trim().ToString());
        }
        else
        {
            objdomaster.domaster_PARTYID = General.Parse<int>(ddlparty.SelectedValue.Trim().ToString());
        }
        objdomaster.domaster_DESTINATION = txtdestination.Text.Trim().ToString();
        objdomaster.domaster_MONTHID=General.Parse<int>(ddlmonth.SelectedValue.Trim().ToString());
        objdomaster.domaster_MONTH=ddlmonth.SelectedItem.Text.Trim().ToString();
        objdomaster.domaster_STATUS = 0;
        if (objdomaster.Insert(true, "domaster"))
        {
            string sqlmax = "SELECT MAX(DOID) AS DOID FROM DOMASTER DM WHERE DM.DONO='" + txtdono.Text.Trim().ToString() + "' AND DM.STATUS=0";
            Handler hdnmax = new Handler();
            DataTable dtmax = hdnmax.GetTable(sqlmax);
            if (dtmax.Rows.Count > 0)
            {
                dodetailmaster objdodetailmaster = new dodetailmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                objdodetailmaster.dodetailmaster_SRNO = -1;
                objdodetailmaster.dodetailmaster_COALID=General.Parse<int>(ddlcoaltype.SelectedValue.ToString().Trim());
                objdodetailmaster.dodetailmaster_GRADE = ddlcoalgrade.SelectedItem.Text.Trim().ToString();
                objdodetailmaster.dodetailmaster_DOID = General.Parse<int>(dtmax.Rows[0][0].ToString().Trim());
                objdodetailmaster.dodetailmaster_QUANTITY = General.Parse<double>(txtquantity.Text.ToString().Trim());

                objdodetailmaster.dodetailmaster_BASICCHARGES = General.Parse<double>(txtbasiccharges.Text.ToString().Trim());
                objdodetailmaster.dodetailmaster_SIZINGCHARGES = General.Parse<double>(txtsizingcharges.Text.ToString().Trim());
                objdodetailmaster.dodetailmaster_STC = General.Parse<double>(txtstc.Text.ToString().Trim());
                objdodetailmaster.dodetailmaster_SED = General.Parse<double>(txtsed.Text.ToString().Trim());
                objdodetailmaster.dodetailmaster_ROYALTY = General.Parse<double>(txtroyalty.Text.ToString().Trim());
                objdodetailmaster.dodetailmaster_CENTALEXCISE = General.Parse<double>(txtexcise.Text.ToString().Trim());
                objdodetailmaster.dodetailmaster_CLEANENERGYNESS = General.Parse<double>(txtcleanenergyness.Text.ToString().Trim());
                objdodetailmaster.dodetailmaster_MPRDTAX = General.Parse<double>(txtmpgatsava.Text.ToString().Trim());
                objdodetailmaster.dodetailmaster_TRANSITTAX = General.Parse<double>(txttransit.Text.ToString().Trim());
                objdodetailmaster.dodetailmaster_ENTRYFEE = General.Parse<double>(txtentryfee.Text.ToString().Trim());
                objdodetailmaster.dodetailmaster_VATCST = General.Parse<double>(txtvatcst.Text.ToString().Trim());
                objdodetailmaster.dodetailmaster_TCS = General.Parse<double>(txttcs.Text.ToString().Trim());
                objdodetailmaster.dodetailmaster_TOTALAMOUNT = General.Parse<double>(txttotal.Text.ToString().Trim());
                objdodetailmaster.dodetailmaster_STATUS = 0;
                if (objdodetailmaster.Insert(true, "dodetailmaster"))
                {
                }

                for (int i = 0; i < paymentrepeater.Items.Count; i++)
                {

                    TextBox txtpmtno = (TextBox)paymentrepeater.Items[i].FindControl("txtpmtno");
                    DropDownList ddlpaymenttype = (DropDownList)paymentrepeater.Items[i].FindControl("ddlpaymenttype");
                    DropDownList ddlbank = (DropDownList)paymentrepeater.Items[i].FindControl("ddlbank");
                    TextBox txtpmtdate = (TextBox)paymentrepeater.Items[i].FindControl("txtpmtdate");
                    TextBox txtamount = (TextBox)paymentrepeater.Items[i].FindControl("txtamount");


                    paymentmaster objpayment = new paymentmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                    objpayment.paymentmaster_SRNO = -1;
                    objpayment.paymentmaster_PAYMENTTYPE = ddlpaymenttype.SelectedItem.Text.Trim().ToString();
                    objpayment.paymentmaster_PAYMENTNO = General.Parse<int>(txtpmtno.Text.Trim());
                    objpayment.paymentmaster_PAYMENTDATE = txtpmtdate.Text.Trim().ToString();
                    objpayment.paymentmaster_RELATIONTYPE = "DO";
                    objpayment.paymentmaster_RELATIONID = General.Parse<int>(dtmax.Rows[0][0].ToString());
                    objpayment.paymentmaster_BANKNAME = ddlbank.SelectedItem.Text.Trim().ToString();
                    objpayment.paymentmaster_AMOUNT= General.Parse<double>(txtamount.Text.Trim());
                    objpayment.paymentmaster_STATUS = 0;
                    if (objpayment.Insert(true, "paymentmaster"))
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
                        objterms.termstable_PARTYTYPE = "DO";
                        objterms.termstable_PARTYID = General.Parse<int>(dtmax.Rows[0][0].ToString());
                        objterms.termstable_TERMSID = General.Parse<int>(aListItem.Value.ToString().Trim());
                        objterms.termstable_TERMS = aListItem.Text.Trim().ToString();
                        objterms.termstable_TERMSVALUE = -1;
                        if (objterms.Insert(true, "termstable"))
                        {
                        }
                    }
                }
            }
            Response.Redirect("dolist.aspx");
        }
    }
}