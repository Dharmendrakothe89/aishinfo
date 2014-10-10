using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class coaltypemaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("default.aspx");
            }
            FillList();
        }
    }
    public void FillList()
    {
        string sql = "SELECT SRNO,COALTYPE,CASE WHEN STATUS=0 THEN 'ACTIVE' ELSE 'DIACTIVE' END AS STATUS FROM COALMASTER CM WHERE COALTYPEID IS NULL";
        Handler hdn = new Handler();
        DataTable dt = hdn.GetTable(sql);
        ViewState["coaltype"] = dt;
        if (dt.Rows.Count > 0)
        {
            gvcoaltype.DataSource = (DataTable)ViewState["coaltype"];
            gvcoaltype.DataBind();
        }
        else
        {
            gvcoaltype.DataSource = null;
            gvcoaltype.DataBind();
        }

        string sql1 = "SELECT SRNO,COALTYPE,GRADE,CASE WHEN STATUS=0 THEN 'ACTIVE' ELSE 'DIACTIVE' END AS STATUS FROM COALMASTER CM" +
                      " WHERE COALTYPEID IS NOT NULL";
        Handler hdn1 = new Handler();
        DataTable dt1 = hdn1.GetTable(sql1);
        ViewState["grade"] = dt1;
        if (dt1.Rows.Count > 0)
        {
            gvcoalgrade.DataSource = (DataTable)ViewState["grade"];
            gvcoalgrade.DataBind();
        }
        else
        {
            gvcoalgrade.DataSource = null;
            gvcoalgrade.DataBind();
        }
        string sql2 = "SELECT SRNO,COALTYPE FROM COALMASTER CM WHERE STATUS=0 AND COALTYPEID IS NULL GROUP BY SRNO,COALTYPE";
        Handler hdn2 = new Handler();
        DataTable dt2 = hdn2.GetTable(sql2);
        
        ddlcoaltype.DataSource = dt2;
        ddlcoaltype.DataTextField = "COALTYPE";
        ddlcoaltype.DataValueField = "SRNO";
        ddlcoaltype.DataBind();
        ddleditcoaltype.DataSource = dt2;
        ddleditcoaltype.DataTextField = "COALTYPE";
        ddleditcoaltype.DataValueField = "SRNO";
        ddleditcoaltype.DataBind();
    }
    protected void gvcoaltype_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvcoaltype.PageIndex = e.NewPageIndex;
        gvcoaltype.DataSource = (DataTable)ViewState["coaltype"];
        gvcoaltype.DataBind();
    }
    protected void gvcoalgrade_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvcoalgrade.PageIndex = e.NewPageIndex;
        gvcoalgrade.DataSource = (DataTable)ViewState["grade"];
        gvcoalgrade.DataBind();
    }
    protected void btnsubmitcoaltype_Click(object sender, EventArgs e)
    {
        coalmaster coalmaster = new coalmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
        coalmaster.coalmaster_SRNO = -1;
        coalmaster.coalmaster_COALTYPEID = -1;
        coalmaster.coalmaster_COALTYPE = txtname.Text.Trim().ToString();
        coalmaster.coalmaster_STATUS = 0;
        if (coalmaster.Insert(true, "coalmaster"))
        {
            MessageBox("Caol Type Added Successfully");
            FillList();

        }
       ScriptManager.RegisterStartupScript(this, this.GetType(), "msgadds", "showmaster();", true);
    }
    protected void btnsubmitgrade_Click(object sender, EventArgs e)
    {
        coalmaster coalmaster = new coalmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
        coalmaster.coalmaster_SRNO = -1;
        coalmaster.coalmaster_COALTYPE = ddlcoaltype.SelectedItem.Text.Trim().ToString();
        coalmaster.coalmaster_COALTYPEID = General.Parse<int>(ddlcoaltype.SelectedValue.Trim().ToString());
        coalmaster.coalmaster_GRADE = txtgrade.Text.Trim().ToString();
        coalmaster.coalmaster_STATUS = 0;
        if (coalmaster.Insert(true, "coalmaster"))
        {
            MessageBox("Caol Grade Added Successfully");
            FillList();
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msgadds", "showmaster();", true);
    }

    protected void lnkedit_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        ViewState["coalid"] = lnk.CommandArgument.ToString();
        string sql = "SELECT * FROM COALMASTER UT WHERE UT.SRNO=" + lnk.CommandArgument.Trim().ToString();
        Handler hdnstate = new Handler();
        DataTable dtstate = hdnstate.GetTable(sql);
        txteditcoaltype.Text = dtstate.Rows[0]["COALTYPE"].ToString().Trim();
        if (dtstate.Rows[0]["STATUS"].ToString().Trim() == "True")
        {
            ddlstatus.SelectedValue = "1";
        }
        else
        {
            ddlstatus.SelectedValue = "0";
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "msgas", "showeditcoaldiv();", true);
    }
    protected void lnkgradeedit_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        ViewState["gradeid"] = lnk.CommandArgument.ToString();
        string sql = "SELECT * FROM COALMASTER UT WHERE UT.SRNO=" + lnk.CommandArgument.Trim().ToString();
        Handler hdnstate = new Handler();
        DataTable dtstate = hdnstate.GetTable(sql);
        ddleditcoaltype.SelectedValue = dtstate.Rows[0]["COALTYPEID"].ToString().Trim();
        txteditgrade.Text = dtstate.Rows[0]["GRADE"].ToString().Trim();
        if (dtstate.Rows[0]["STATUS"].ToString().Trim() == "True")
        {
            ddlgradestatus.SelectedValue = "1";
        }
        else
        {
            ddlgradestatus.SelectedValue = "0";
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "editgrade", "showeditgradediv();", true);
    }

    protected void txtbtnupdatecoal_Click(object sender, EventArgs e)
    {
        coalmaster coalmaster = new coalmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
        coalmaster.coalmaster_SRNO = -1;
        coalmaster.coalmaster_COALTYPEID = -1;
        coalmaster.coalmaster_COALTYPE = txteditcoaltype.Text.Trim().ToString();
        coalmaster.coalmaster_STATUS = General.Parse<int>(ddlstatus.SelectedValue.Trim().ToString());
        string condition = "SRNO=" + ViewState["coalid"].ToString();
        if (coalmaster.Insert(false, "coalmaster", condition))
        {
            MessageBox("Caol Type Updated Successfully");
            FillList();
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg4", "showmaster();", true);
    }
    protected void btnupdategrade_Click(object sender, EventArgs e)
    {
        coalmaster coalmaster = new coalmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
        coalmaster.coalmaster_SRNO = -1;
        coalmaster.coalmaster_COALTYPE = ddleditcoaltype.SelectedItem.Text.Trim().ToString();
        coalmaster.coalmaster_COALTYPEID = General.Parse<int>(ddleditcoaltype.SelectedValue.Trim().ToString());
        coalmaster.coalmaster_GRADE = txteditgrade.Text.Trim().ToString();
        coalmaster.coalmaster_STATUS = General.Parse<int>(ddlgradestatus.SelectedValue.Trim().ToString());
        string condition = "SRNO=" + ViewState["gradeid"].ToString();
        if (coalmaster.Insert(false, "coalmaster", condition))
        {
            MessageBox("Caol Grade Updated Successfully");
            FillList();

        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msdsdssg4", "showmaster();", true);
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