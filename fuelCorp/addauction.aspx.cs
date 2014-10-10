using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class addauction : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("default.aspx");
            }
            DataTable dt = GetFillDataTable(1);
            ViewState["coaltype"] = dt;
            FillPersonData();
            coaltyperepeater.DataSource = (DataTable)ViewState["coaltype"];
            coaltyperepeater.DataBind();
        }
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
    protected void coaltyperepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DropDownList ddldepartment = (DropDownList)e.Item.FindControl("ddlcoaltype");
            DropDownList ddldesignation = (DropDownList)e.Item.FindControl("ddlcoalgrade");

            ddldepartment.DataSource = (DataTable)ViewState["coal"];
            ddldepartment.DataTextField = "COALTYPE";
            ddldepartment.DataValueField = "SRNO";
            ddldepartment.DataBind();
            ddldepartment.Items.Insert(0, "-- Coal Type --");


            ddldesignation.DataSource = (DataTable)ViewState["grade"];
            ddldesignation.DataTextField = "COALTYPE";
            ddldesignation.DataValueField = "SRNO";
            ddldesignation.DataBind();
            ddldesignation.Items.Insert(0, "-- Coal Grade --");

        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        auctionmaster objauctionmaster = new auctionmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
        objauctionmaster.auctionmaster_AUCTIONID = -1;
        objauctionmaster.auctionmaster_AUCTIONNAME = txtauctionname.Text.Trim().ToString();
        objauctionmaster.auctionmaster_CMPID = General.Parse<int>(Session["cmpid"].ToString());
        objauctionmaster.auctionmaster_AUCTIONDATE = txtauctiondate.Text.Trim().ToString();
        objauctionmaster.auctionmaster_STARTDATE = txtstartdate.Text.Trim().ToString();
        objauctionmaster.auctionmaster_ENDDATE = txtenddate.Text.Trim().ToString();
        objauctionmaster.auctionmaster_DESCRIPTION = txtdescription.Text.Trim().ToString();
        objauctionmaster.auctionmaster_STATUS = 0;
        if (objauctionmaster.Insert(true, "auctionmaster"))
        {
            string sqlmax = "SELECT MAX(AM.AUCTIONID) FROM AUCTIONMASTER AM WHERE AM.STATUS=0 AND AM.AUCTIONNAME='"+txtauctionname.Text.Trim().ToString()+"' AND"+
                            " AM.AUCTIONDATE='" + txtauctiondate.Text.Trim().ToString() + "' AND AM.CMPID=" + Session["cmpid"].ToString();
            Handler hdnmax = new Handler();
            DataTable dtmax = hdnmax.GetTable(sqlmax);
            if (dtmax.Rows.Count > 0 && coaltyperepeater.Items.Count > 0)
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
                    objspecification.auctionspecification_CONTRACTTYPE = "AUCTION";
                    objspecification.auctionspecification_CONTRACTID = General.Parse<int>(dtmax.Rows[0][0].ToString());
                    objspecification.auctionspecification_COALTYPE = ddlcoaltype.SelectedItem.Text.Trim();
                    objspecification.auctionspecification_GRADE = ddlcoalgrade.SelectedItem.Text.Trim();
                    objspecification.auctionspecification_QUANTITY = General.Parse<double>(txtquantity.Text.Trim().ToString());
                    objspecification.auctionspecification_RATE = General.Parse<double>(txtcoalrate.Text.Trim().ToString());
                    objspecification.auctionspecification_QUANTITYUNIT = ddlquantityunit.SelectedValue.ToString();
                    objspecification.auctionspecification_STATUS = 0;
                    if (objspecification.Insert(true, "auctionspecification"))
                    {
                    }


                }
            }
            Response.Redirect("auctiondetails.aspx?AUCTIONID=" + dtmax.Rows[0][0].ToString().Trim());
        }
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
}