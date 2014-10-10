using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class taxentry : System.Web.UI.Page
{
    Handler hd = new Handler();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("default.aspx");
            }
            gettaxdetails();
        }
    }
    public void gettaxdetails()
    {
        DataTable dt = new DataTable();
        Handler hdntax = new Handler();
        dt = hdntax.GetTable("SELECT SRNO,TAXNAME,TAXVALUE,TAXUNIT,CASE WHEN STATUS=0 THEN 'ACTIVE' ELSE 'DE-ACTIVE' END AS STATUS FROM TAXMASTER");
        ViewState["list"] = dt;
        if (dt.Rows.Count > 0)
        {
            gvtax.DataSource = (DataTable)ViewState["list"];
            gvtax.DataBind();
        }
        else
        {
            gvtax.DataSource = null;
            gvtax.DataBind();
        }


    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        taxmaster tax = new taxmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
        tax.taxmaster_SRNO = -1;
        tax.taxmaster_TAXNAME = txttaxname.Text;
        tax.taxmaster_TAXVALUE = Convert.ToDouble(txttaxvalue.Text);
        tax.taxmaster_TAXUNIT = ddlunit.SelectedValue.ToString();
        tax.taxmaster_STATUS = 0;
        if (tax.Insert(true, "taxmaster"))
        {
            MessageBox("Tax Entry Added Successfully");
            gettaxdetails();
            
        }
    }
    protected void gvlookup_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvtax.PageIndex = e.NewPageIndex;
        gvtax.DataSource = (DataTable)ViewState["list"];
        gvtax.DataBind();
    }
    protected void lnkdetails_Click(object sender, EventArgs e)
    {
        LinkButton lnk=(LinkButton)sender;
        DataTable dt = new DataTable();
        Handler hdntax = new Handler();
        dt = hdntax.GetTable("SELECT SRNO,TAXNAME,TAXVALUE,TAXUNIT,CASE WHEN STATUS=0 THEN 'ACTIVE' ELSE 'DE-ACTIVE' END AS STATUS FROM TAXMASTER TM WHERE SRNO=" + lnk.CommandArgument.ToString().Trim());
        txteditname.Text = dt.Rows[0]["TAXNAME"].ToString().Trim();
        txteditvalue.Text = dt.Rows[0]["TAXVALUE"].ToString().Trim();
        ddleditunit.SelectedValue = dt.Rows[0]["TAXUNIT"].ToString().Trim();
        ViewState["tax"] = dt.Rows[0]["SRNO"].ToString().Trim();
        if (dt.Rows[0]["STATUS"].ToString().Trim() == "True")
        {
            ddlstatus.SelectedValue = "1";
        }
        else
        {
            ddlstatus.SelectedValue = "0";
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Edit", "EditState();", true);
    }
    protected void btnedit_Click(object sender, EventArgs e)
    {
        taxmaster tax = new taxmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
        tax.taxmaster_SRNO = -1;
        tax.taxmaster_TAXNAME = txteditname.Text.Trim().ToString();
        tax.taxmaster_TAXVALUE = Convert.ToDouble(txteditvalue.Text.Trim().ToString());
        tax.taxmaster_TAXUNIT = ddleditunit.SelectedValue.ToString();
        tax.taxmaster_STATUS = General.Parse<int>(ddlstatus.SelectedValue.ToString()); 
        string condition = "SRNO=" + ViewState["tax"].ToString();
        if (tax.Insert(false, "taxmaster", condition))
        {
            MessageBox("Tax Entry Updated Successfully");
            gettaxdetails();
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Edit", "FillState();", true);
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