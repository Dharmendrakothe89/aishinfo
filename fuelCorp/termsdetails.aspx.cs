using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class termsdetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("default.aspx");
            }
            if (Request.QueryString["termid"] != null)
            {
                ViewState["termid"] = Request.QueryString["termid"].ToString();
            }

            FillData(ViewState["termid"].ToString());
            DisableControl();
        }
    }

    protected void FillData(string vehicleid)
    {
        Handler hd = new Handler();
        DataTable dtcategory = hd.GetTable("select RTRIM(name) AS NAME , RTRIM(value) AS VALUE from lookupmaster lm , lookupheadingmaster hm where head='TERMS AND CONDITION CATEGORY' and lm.headid=hm.srno ");
      
        ddleditcategory.DataSource = dtcategory;
        ddleditcategory.DataTextField = "NAME";
        ddleditcategory.DataValueField = "VALUE";
        ddleditcategory.DataBind();


        DataTable dtsubcategory = hd.GetTable("select RTRIM(name) AS NAME , RTRIM(value) AS VALUE from lookupmaster lm , lookupheadingmaster hm where head='TERMS AND CONDITION SUBCATEGORY' and lm.headid=hm.srno ");
        ddleditsubcategory.DataSource = dtsubcategory;
        ddleditsubcategory.DataTextField = "NAME";
        ddleditsubcategory.DataValueField = "VALUE";
        ddleditsubcategory.DataBind();
        ddleditsubcategory.Items.Insert(0, "--Select--");

        string sql = "SELECT * FROM TERMSCONDITION TM WHERE TM.SRNO=" + ViewState["termid"].ToString().Trim().ToString();
        Handler hdnstate = new Handler();
        DataTable dtstate = hdnstate.GetTable(sql);
        txteditterms.Text = dtstate.Rows[0]["TERMS"].ToString().Trim();
        txteditdescription.Text = dtstate.Rows[0]["DESCRIPTION"].ToString().Trim();
        ddleditcategory.Items.FindByText(dtstate.Rows[0]["CATEGORY"].ToString().Trim()).Selected = true;
        if (dtstate.Rows[0]["STATUS"].ToString().Trim() == "True")
        {
            ddlstatus.SelectedValue = "1";
        }
        else
        {
            ddlstatus.SelectedValue = "0";
        }
    }
    private void DisableControl()
    {
        ddleditcategory.Enabled = false;
        ddleditsubcategory.Enabled = false;
        txteditterms.Enabled = false;
        txteditdescription.Enabled = false;
        ddlstatus.Enabled = false;
        btnedit.Visible = false;
    }
    private void EnableControl()
    {
        //ddleditcategory.Enabled = true;
        //ddleditsubcategory.Enabled = true;
        txteditterms.Enabled = true;
        txteditdescription.Enabled = true;
        ddlstatus.Enabled = true;
        btnedit.Visible = true;
    }
    protected void btnedit_Click(object sender, EventArgs e)
    {
        EnableControl();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        termscondition terms = new termscondition(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
        terms.termscondition_TERMS = txteditterms.Text;
        terms.termscondition_SRNO = -1;
        terms.termscondition_TERMSVALUE = -1;
        if (txteditdescription.Text.Trim().ToString() != string.Empty)
        {
            terms.termscondition_DESCRIPTION = txteditdescription.Text;
        }
        terms.termscondition_STATUS = General.Parse<int>(ddlstatus.SelectedValue.ToString().Trim().ToString());
        string condition = "SRNO=" + ViewState["termid"].ToString();
        if (terms.Insert(false, "termscondition", condition))
        {
            Response.Redirect("termsconditions.aspx?id=1");
        }
    }
}