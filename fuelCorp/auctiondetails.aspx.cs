using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class auctiondetails : System.Web.UI.Page
{
    string editbtn = "1";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("default.aspx");
            }
            FillPersonData();
            ViewState["postback"] = "1";
            if (Request.QueryString["AUCTIONID"] != null)
            {
                ViewState["AUCTIONID"] = Request.QueryString["AUCTIONID"].ToString();
                FillData(ViewState["AUCTIONID"].ToString().Trim());
               
            }
        }
        editbtn = ViewState["postback"].ToString();
    }
    private void FillData(string auctionid)
    {
        string sqlcolliery = "SELECT AM.AUCTIONID,AM.AUCTIONNAME,AM.DESCRIPTION,AM.AUCTIONDATE,AM.STARTDATE,AM.ENDDATE,CASE WHEN STATUS=0 THEN 'ACTIVE' WHEN AM.STATUS=1 THEN 'COMPLETE' ELSE 'CANCEL' END AS STATUS " +
                             " FROM AUCTIONMASTER AM WHERE AM.AUCTIONID=" + auctionid;
        Handler hdncolliery = new Handler();

        DataTable dtcolliery = hdncolliery.GetTable(sqlcolliery);
        if (dtcolliery.Rows.Count > 0)
        {
            txtauctionname.Text = dtcolliery.Rows[0]["AUCTIONNAME"].ToString();
            txtauctiondate.Text = dtcolliery.Rows[0]["AUCTIONDATE"].ToString();
            txtdescription.Text = dtcolliery.Rows[0]["DESCRIPTION"].ToString();
            txtstartdate.Text = dtcolliery.Rows[0]["STARTDATE"].ToString();
            txtenddate.Text = dtcolliery.Rows[0]["ENDDATE"].ToString();
        }

        string sqldetail = "select * from auctionspecification where status=0 and contractid=" + auctionid;
        Handler hdndetails = new Handler();
        DataTable dtsql = hdndetails.GetTable(sqldetail);
        ViewState["coalrecord"] = dtsql;
        coaltyperepeater.DataSource = dtsql;
        coaltyperepeater.DataBind();
    }
    private void FillPersonData()
    {
        string sqldepartment = "SELECT SRNO,COALTYPE FROM COALMASTER CM WHERE STATUS=0 AND COALTYPEID IS NULL ORDER BY COALTYPE";
        Handler hdndepartment = new Handler();
        DataTable dtdepartment = hdndepartment.GetTable(sqldepartment);
        ViewState["coal"] = dtdepartment;

        string sqldesignation = "SELECT SRNO,COALTYPE FROM COALMASTER CM WHERE STATUS=0 AND COALTYPEID IS NOT NULL ORDER BY COALTYPE";
        Handler hdndesignation = new Handler();
        DataTable dtdesignation = hdndesignation.GetTable(sqldesignation);
        ViewState["grade"] = dtdesignation;


    }
    protected void coaltyperepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            CheckBox chkcoaltype = (CheckBox)e.Item.FindControl("COALTYPE");
            DropDownList ddlcoaltype = (DropDownList)e.Item.FindControl("ddlcoaltype");
            DropDownList ddlcoalgrade = (DropDownList)e.Item.FindControl("ddlcoalgrade");
            TextBox txtcoalrate = (TextBox)e.Item.FindControl("txtcoalrate");
            TextBox txtquantity = (TextBox)e.Item.FindControl("txtquantity");
            DropDownList ddlquantityunit = (DropDownList)e.Item.FindControl("ddlquantityunit");

            ddlcoaltype.DataSource = (DataTable)ViewState["coal"];
            ddlcoaltype.DataTextField = "COALTYPE";
            ddlcoaltype.DataValueField = "SRNO";
            ddlcoaltype.DataBind();
            if (((System.Data.DataRowView)(e.Item.DataItem)).Row.ItemArray.Length >= 3 && ((System.Data.DataRowView)(e.Item.DataItem)).Row.ItemArray[3].ToString().Trim() != string.Empty)
            {
                string department = ((System.Data.DataRowView)(e.Item.DataItem)).Row.ItemArray[3].ToString().Trim();
                ddlcoaltype.SelectedValue = department;
            }

            ddlcoalgrade.DataSource = (DataTable)ViewState["grade"];
            ddlcoalgrade.DataTextField = "COALTYPE";
            ddlcoalgrade.DataValueField = "SRNO";
            ddlcoalgrade.DataBind();
            if (((System.Data.DataRowView)(e.Item.DataItem)).Row.ItemArray.Length >= 4 && ((System.Data.DataRowView)(e.Item.DataItem)).Row.ItemArray[4].ToString().Trim() != string.Empty)
            {
                string designation = ((System.Data.DataRowView)(e.Item.DataItem)).Row.ItemArray[4].ToString().Trim();
                ddlcoalgrade.SelectedValue = designation;
            }
            if (((System.Data.DataRowView)(e.Item.DataItem)).Row.ItemArray.Length >= 7 && ((System.Data.DataRowView)(e.Item.DataItem)).Row.ItemArray[7].ToString().Trim() != string.Empty)
            {
                string unit = ((System.Data.DataRowView)(e.Item.DataItem)).Row.ItemArray[7].ToString().Trim();
                ddlquantityunit.SelectedValue = unit;
            }
            if (editbtn == "UPDATE")
            {
                ddlcoaltype.Enabled = true;
                ddlcoalgrade.Enabled = true;
                txtcoalrate.Enabled = true;
                txtquantity.Enabled = true;
                ddlquantityunit.Enabled = true;
                
            }
            else
            {
                ddlcoaltype.Enabled = false;
                ddlcoalgrade.Enabled = false;
                txtcoalrate.Enabled = false;
                txtquantity.Enabled = false;
                ddlquantityunit.Enabled = false;
            }
          
        }
    }
    protected void btnedit_Click(object sender, EventArgs e)
    {
        ViewState["postback"] = "UPDATE";
        editbtn = ViewState["postback"].ToString();
        txtauctionname.Enabled = true;
        txtauctiondate.Enabled = true;
        txtdescription.Enabled = true;
        txtstartdate.Enabled = true;
        txtenddate.Enabled = true;
        ddlstatus.Enabled = true;
        btnupdate.Visible = true;
        traddmore.Style.Add("display", "block");
        coaltyperepeater.DataSource = (DataTable)ViewState["coalrecord"];
        coaltyperepeater.DataBind();
    }
    protected void lnkaddmember_Click(object sender, EventArgs e)
    {
        DataTable dt1 = GetFillPreviousData();
        ViewState["coaltype"] = dt1;
        DataTable dt = GetFillDataTable(1);
        if (ViewState["coaltype"] != null)
        {
            ((DataTable)ViewState["coaltype"]).Merge(dt);

        }
        else
        {
            ViewState["coaltype"] = dt;
        }
        coaltyperepeater.DataSource = (DataTable)ViewState["coaltype"];
        coaltyperepeater.DataBind();
    }
    private DataTable GetFillPreviousData()
    {
        DataTable dtData = new DataTable();
        dtData.Columns.Add("SRNO");
        dtData.Columns.Add("COALTYPE");
        dtData.Columns.Add("GRADE");
        dtData.Columns.Add("RATE");
        dtData.Columns.Add("QUANTITY");
        dtData.Columns.Add("QUANTITYUNIT");

        for (int i = 0; i < coaltyperepeater.Items.Count; i++)
        {
            TextBox txtquantity = (TextBox)coaltyperepeater.Items[i].FindControl("txtquantity");
            TextBox txtcoalrate = (TextBox)coaltyperepeater.Items[i].FindControl("txtcoalrate");
            DropDownList ddlcoaltype = (DropDownList)coaltyperepeater.Items[i].FindControl("ddlcoaltype");
            DropDownList ddlcoalgrade = (DropDownList)coaltyperepeater.Items[i].FindControl("ddlcoalgrade");
            DropDownList ddlquantityunit = (DropDownList)coaltyperepeater.Items[i].FindControl("ddlquantityunit");
            int count = dtData.Rows.Count;
            dtData.Rows.Add(1);
            dtData.Rows[count]["SRNO"] = (count + 1).ToString();
            dtData.Rows[count]["COALTYPE"] = ddlcoaltype.SelectedItem.Text.ToString();

            dtData.Rows[count]["GRADE"] = ddlcoalgrade.SelectedItem.Text.Trim().ToString();
            dtData.Rows[count]["QUANTITY"] = txtquantity.Text.Trim().ToString();

            dtData.Rows[count]["RATE"] = txtcoalrate.Text.Trim().ToString();
            dtData.Rows[count]["QUANTITYUNIT"] = ddlquantityunit.SelectedItem.Text.ToString();
        }

        return dtData;
    }
    private DataTable GetFillDataTable(int value)
    {
        DataTable dtData = new DataTable();
        dtData.Columns.Add("SRNO");
        dtData.Columns.Add("COALTYPE");
        dtData.Columns.Add("GRADE");
        dtData.Columns.Add("RATE");
        dtData.Columns.Add("QUANTITY");
        dtData.Columns.Add("QUANTITYUNIT");
        for (int i = 0; i < value; i++)
        {
            DataRow dtrow = dtData.NewRow();
            dtData.Rows.Add(dtrow);
            dtData.Rows[i]["SRNO"] = i + 1;

        }
        return dtData;
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        auctionmaster objauctionmaster = new auctionmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
        objauctionmaster.auctionmaster_AUCTIONID = -1;
        objauctionmaster.auctionmaster_AUCTIONNAME = txtauctionname.Text.Trim().ToString();
        objauctionmaster.auctionmaster_CMPID = -1;
        objauctionmaster.auctionmaster_AUCTIONDATE = txtauctiondate.Text.Trim().ToString();
        objauctionmaster.auctionmaster_STARTDATE = txtstartdate.Text.Trim().ToString();
        objauctionmaster.auctionmaster_ENDDATE = txtenddate.Text.Trim().ToString();
        objauctionmaster.auctionmaster_DESCRIPTION = txtdescription.Text.Trim().ToString();
        objauctionmaster.auctionmaster_STATUS =General.Parse<int>(ddlstatus.SelectedValue.Trim().ToString());
        string condition = "AUCTIONID=" + ViewState["AUCTIONID"].ToString().Trim();
        if (objauctionmaster.Insert(false, "auctionmaster", condition))
        {
            if (coaltyperepeater.Items.Count > 0)
            {
                for (int i = 0; i < coaltyperepeater.Items.Count; i++)
                {
                    DropDownList ddlcoaltype = (DropDownList)coaltyperepeater.Items[i].FindControl("ddlcoaltype");
                    DropDownList ddlcoalgrade = (DropDownList)coaltyperepeater.Items[i].FindControl("ddlcoalgrade");
                    DropDownList ddlquantityunit = (DropDownList)coaltyperepeater.Items[i].FindControl("ddlquantityunit");
                    TextBox txtcoalrate = (TextBox)coaltyperepeater.Items[i].FindControl("txtcoalrate");
                    TextBox txtquantity = (TextBox)coaltyperepeater.Items[i].FindControl("txtquantity");


                    auctionspecification objspecification = new auctionspecification(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                    objspecification.auctionspecification_SRNO = -1;
                    objspecification.auctionspecification_CONTRACTID = -1;
                    objspecification.auctionspecification_COALTYPE = ddlcoaltype.SelectedItem.Text.Trim();
                    objspecification.auctionspecification_GRADE = ddlcoalgrade.SelectedItem.Text.Trim();
                    objspecification.auctionspecification_QUANTITY = General.Parse<double>(txtquantity.Text.Trim().ToString());
                    objspecification.auctionspecification_RATE = General.Parse<double>(txtcoalrate.Text.Trim().ToString());
                    objspecification.auctionspecification_QUANTITYUNIT = ddlquantityunit.SelectedValue.ToString();
                    objspecification.auctionspecification_STATUS = 0;
                    string condition1 = "CONTRACTTYPE='AUCTION' AND CONTRACTID=" + ViewState["AUCTIONID"].ToString().Trim();
                    if (objspecification.Insert(false, "auctionspecification", condition1))
                    {
                    }


                }
            }
        }
    }

}