using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class dodetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("default.aspx");
            }
            ViewState["postback"] = "1";
            ViewState["postback"] = "normal";
            FillPreviousData();
            if (Request.QueryString["DOID"] != null)
            {
                ViewState["DOID"] = Request.QueryString["DOID"].ToString();
                FillData(ViewState["DOID"].ToString().Trim());
            }
        }
    }
    public void FillPreviousData()
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
        ddlcoaltype.Items.Insert(0, "-- Select Coal --");

        string sqlcolliery2 = "SELECT LM.NAME,LM.SRNO FROM LOOKUPMASTER LM INNER JOIN LOOKUPHEADINGMASTER LHM ON LM.HEADID=LHM.SRNO" +
                             " WHERE LHM.STATUS=0 AND LM.STATUS=0 AND LHM.HEAD='SUPPLY MONTH'";
        Handler hdncolliery2 = new Handler();
        DataTable dtcolliery2 = hdncolliery.GetTable(sqlcolliery2);
        ddlmonth.DataSource = dtcolliery2;
        ddlmonth.DataTextField = "NAME";
        ddlmonth.DataValueField = "SRNO";
        ddlmonth.DataBind();
        ddlmonth.Items.Insert(0, "--Select--");
    }
    public void FillData(string doid)
    {
        string sqlcolliery = "SELECT * FROM DOMASTER DM WHERE DOID=" + doid;
        Handler hdncolliery = new Handler();

        DataTable dtcolliery = hdncolliery.GetTable(sqlcolliery);
        if (dtcolliery.Rows.Count > 0)
        {
            ddldotype.SelectedValue = dtcolliery.Rows[0]["DOFOR"].ToString().Trim();
            ddlcolliery.SelectedValue = dtcolliery.Rows[0]["COLLIERYID"].ToString().Trim();
            ddlmonth.SelectedValue = dtcolliery.Rows[0]["MONTHID"].ToString().Trim();
            txtdestination.Text = dtcolliery.Rows[0]["DESTINATION"].ToString().Trim();
            string sqlcollierycode = "SELECT CM.COLLIERYCODE FROM COLLIERYMASTER CM WHERE CM.SRNO=" + dtcolliery.Rows[0]["COLLIERYID"].ToString().Trim();
            Handler hdncollierycode = new Handler();
            DataTable dtcollierycode = hdncolliery.GetTable(sqlcollierycode);
            txtcollierycode.Text = dtcollierycode.Rows[0][0].ToString().Trim();

            txtdono.Text = dtcolliery.Rows[0]["DONO"].ToString().Trim();
            txtdodate.Text = dtcolliery.Rows[0]["DODATE"].ToString().Trim();
            ddlauction.SelectedValue = dtcolliery.Rows[0]["PARTYID"].ToString().Trim();
            if (dtcolliery.Rows[0]["DOFOR"].ToString().Trim() == "AUCTION")
            {
                trlinkedcustomer.Style.Add("display", "none");
                trauction.Style.Add("display", "block");

                string sqlparty = "SELECT AUCTIONDATE FROM AUCTIONMASTER AM WHERE AM.AUCTIONID=" + dtcolliery.Rows[0]["PARTYID"].ToString().Trim();
                Handler hdnparty = new Handler();
                DataTable dtparty = hdnparty.GetTable(sqlparty);
                txtauctiondate.Text = dtparty.Rows[0]["AUCTIONDATE"].ToString().Trim();
                
            }
            else
            {
                trlinkedcustomer.Style.Add("display", "block");
                trauction.Style.Add("display", "none");

                string sqlparty = "SELECT PM.PARTYCODE,PAM.ADDRESS FROM PARTYMASTER PM INNER JOIN PARTYADDRESSMASTER PAM ON PM.SRNO=PAM.PARTYID" +
                                " WHERE PM.SRNO=" + dtcolliery.Rows[0]["PARTYID"].ToString().Trim();
                Handler hdnparty = new Handler();
                DataTable dtparty = hdnparty.GetTable(sqlparty);
                txtcode.Text = dtparty.Rows[0]["PARTYCODE"].ToString().Trim();
                txtaddress.Text = dtparty.Rows[0]["ADDRESS"].ToString().Trim();
            }

            string sqldodetails = "SELECT * FROM DODETAILMASTER DD WHERE STATUS=0 AND DOID=" + dtcolliery.Rows[0]["DOID"].ToString().Trim();
            Handler hdndetails = new Handler();
            DataTable dtdodetails = hdndetails.GetTable(sqldodetails);
            if (dtdodetails.Rows.Count > 0)
            {
                ddlcoaltype.SelectedValue = dtdodetails.Rows[0]["COALID"].ToString().Trim();

                string sqlcoaltype = "SELECT GRADE,SRNO FROM COALMASTER CM WHERE STATUS=0 AND COALTYPEID=" + ddlcoaltype.SelectedValue.Trim().ToString();
                Handler hdncoaltype = new Handler();
                DataTable dtcoaltype = hdncoaltype.GetTable(sqlcoaltype);
                ddlcoalgrade.DataSource = dtcoaltype;
                ddlcoalgrade.DataTextField = "GRADE";
                ddlcoalgrade.DataValueField = "SRNO";
                ddlcoalgrade.DataBind();
                ddlcoalgrade.Items.Insert(0, "-- Select Coal --");
                ddlcoaltype.SelectedItem.Text = dtdodetails.Rows[0]["GRADE"].ToString().Trim();

                txtquantity.Text = dtdodetails.Rows[0]["QUANTITY"].ToString().Trim();
                txtbasiccharges.Text = dtdodetails.Rows[0]["QUANTITY"].ToString().Trim();
                txtsizingcharges.Text = dtdodetails.Rows[0]["QUANTITY"].ToString().Trim();
                txtstc.Text = dtdodetails.Rows[0]["QUANTITY"].ToString().Trim();
                txtroyalty.Text = dtdodetails.Rows[0]["QUANTITY"].ToString().Trim();
                txtsed.Text = dtdodetails.Rows[0]["QUANTITY"].ToString().Trim();
                txtexcise.Text = dtdodetails.Rows[0]["QUANTITY"].ToString().Trim();
                txtcleanenergyness.Text = dtdodetails.Rows[0]["QUANTITY"].ToString().Trim();
                txtmpgatsava.Text = dtdodetails.Rows[0]["QUANTITY"].ToString().Trim();
                txttransit.Text = dtdodetails.Rows[0]["QUANTITY"].ToString().Trim();
                txtentryfee.Text = dtdodetails.Rows[0]["QUANTITY"].ToString().Trim();
                txttcs.Text = dtdodetails.Rows[0]["QUANTITY"].ToString().Trim();
                txtvatcst.Text = dtdodetails.Rows[0]["QUANTITY"].ToString().Trim();
                txttotal.Text = dtdodetails.Rows[0]["QUANTITY"].ToString().Trim();

            }

            string sql2 = "SELECT * FROM PAYMENTMASTER T WHERE RELATIONTYPE='DO' AND RELATIONID=" + doid.ToString().Trim();
            Handler hdn2 = new Handler();
            DataTable dt2 = hdn2.GetTable(sql2);
            if (dt2.Rows.Count > 0)
            {
                paymentrepeater.DataSource = dt2;
                paymentrepeater.DataBind();
                
            }
            string sql1 = "SELECT T.TERMSID,T.TERMS FROM TERMSTABLE T WHERE T.STATUS=0 AND T.PARTYTYPE='DO' AND T.PARTYID=" + doid.ToString().Trim();
            Handler hdn1 = new Handler();
            DataTable dt1 = hdn1.GetTable(sql1);
            if (dt1.Rows.Count > 0)
            {
                chktermslist.DataSource = dt1;
                chktermslist.DataTextField = "TERMS";
                chktermslist.DataValueField = "TERMSID";
                chktermslist.DataBind();
                
            }
        }

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
            trlinkedcustomer.Style.Add("display", "none");
            trauction.Style.Add("display", "none");
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
           
            ddlbank.DataSource = (DataTable)ViewState["bank"];
            ddlbank.DataTextField = "NAME";
            ddlbank.DataValueField = "SRNO";
            ddlbank.DataBind();
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