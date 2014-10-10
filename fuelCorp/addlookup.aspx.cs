using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class addlookup : System.Web.UI.Page
{
    Handler hdn = new Handler();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("default.aspx");
            }
            FillCategory();
            gettermscondition();
        }
    }
    private void FillCategory()
    {
        string sql = "SELECT SRNO,HEAD FROM lookupheadingmaster hm WHERE hm.STATUS=0";
        
        DataTable dtcategory = hdn.GetTable(sql);
        ddlcategory.DataSource = dtcategory;
        ddlcategory.DataTextField = "HEAD";
        ddlcategory.DataValueField = "SRNO";
        ddlcategory.DataBind();

        ddleditcategory.DataSource = dtcategory;
        ddleditcategory.DataTextField = "HEAD";
        ddleditcategory.DataValueField = "SRNO";
        ddleditcategory.DataBind();
       
    }
    public void gettermscondition()
    {
        gvlookup.DataSource = null;
        gvlookup.DataBind();
        string sql = "SELECT LM.SRNO,LM.NAME,LM.VALUE,LHM.HEAD AS CATEGORY,CASE WHEN LM.STATUS=0 THEN 'ACTIVE' ELSE 'DE-ACTIVE' END AS STATUS FROM LOOKUPMASTER LM"+
                     " INNER JOIN LOOKUPHEADINGMASTER LHM ON LHM.SRNO=LM.HEADID ";
        DataTable dt = hdn.GetTable(sql);
        ViewState["lookup"] = dt;
        if (dt.Rows.Count > 0)
        {
            gvlookup.DataSource = (DataTable)ViewState["lookup"];
            gvlookup.DataBind();
        }
        else
        {
            gvlookup.DataSource = null;
            gvlookup.DataBind();
        }

    }
    protected void gvlookup_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlookup.PageIndex = e.NewPageIndex;
        gvlookup.DataSource = (DataTable)ViewState["lookup"];
        gvlookup.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        lookupmaster lookup = new lookupmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
        lookup.lookupmaster_SRNO = -1;
        lookup.lookupmaster_NAME = txtname.Text;
        lookup.lookupmaster_HEADID = Convert.ToInt32(ddlcategory.SelectedValue);
        lookup.lookupmaster_VALUE = Convert.ToDouble(txtvalue.Text);
        lookup.lookupmaster_STATUS = 0;
        if (lookup.Insert(true, "lookupmaster"))
        {
            MessageBox("lookup Added Successfully");
            gettermscondition();
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg4e3", "showlist();", true);
    }
    protected void lnkdetails_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        ViewState["lookupid"] = lnk.CommandArgument.ToString();
        string sql = "SELECT * FROM LOOKUPMASTER UT WHERE UT.SRNO=" + lnk.CommandArgument.Trim().ToString();
        Handler hdnstate = new Handler();
        DataTable dtstate = hdnstate.GetTable(sql);
        txteditname.Text = dtstate.Rows[0]["NAME"].ToString().Trim();
        txteditvalue.Text = dtstate.Rows[0]["VALUE"].ToString().Trim();
        ddleditcategory.SelectedValue = dtstate.Rows[0]["HEADID"].ToString().Trim();
        //ddleditcategory.Items.FindByText(dtstate.Rows[0]["CATEGORY"].ToString().Trim()).Selected = true;
        //ddleditsubcategory.Items.FindByText(dtstate.Rows[0]["SUBCATEGORY"].ToString().Trim()).Selected = true;
        if (dtstate.Rows[0]["STATUS"].ToString().Trim() == "True")
        {
            ddlstatus.SelectedValue = "1";
        }
        else
        {
            ddlstatus.SelectedValue = "0";
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msge", "editdiv();", true);
    }
    protected void btnedit_Click(object sender, EventArgs e)
    {
        lookupmaster lookup = new lookupmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
        lookup.lookupmaster_SRNO = -1;
        lookup.lookupmaster_NAME = txteditname.Text.Trim();
        lookup.lookupmaster_HEADID = General.Parse<int>(ddleditcategory.SelectedValue.Trim().ToString());
        lookup.lookupmaster_VALUE = General.Parse<double>(txteditvalue.Text.Trim());
        lookup.lookupmaster_STATUS = General.Parse<int>(ddlstatus.SelectedValue.Trim().ToString());
        string condition = "SRNO=" + ViewState["lookupid"].ToString();
        if (lookup.Insert(false, "lookupmaster", condition))
        {
            MessageBox("lookup Updated Successfully");
            gettermscondition();
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msge3", "showlist();", true);
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