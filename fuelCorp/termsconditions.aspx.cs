using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class termsconditions : System.Web.UI.Page
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
            string id = Request.QueryString["id"];
            if (id != null)
            {
                if (Request.QueryString["id"].ToString() == "1")
                {
                    MessageBox("Terms  Updated Successsfully");
                }
               

            }

            gettermscondition();
            getcategory();
        }
    }
    public void gettermscondition()
    {
        string sql = "select SRNO,CATEGORY,TERMS,DESCRIPTION,CASE WHEN SUBCATEGORY IS NULL THEN '' ELSE SUBCATEGORY END AS SUBCATEGORY from termscondition";
        DataTable dt = hd.GetTable(sql);
        ViewState["data"] = dt;
        if (dt.Rows.Count > 0)
        {
            ViewState["data"] = dt;
            gvtermscondition.DataSource = (DataTable)ViewState["data"];
            gvtermscondition.DataBind();
        }
        else
        {
            ViewState["data"] = null;
            gvtermscondition.DataSource = (DataTable)ViewState["data"];
            gvtermscondition.DataBind();
        }

    }

    public void getcategory()
    {
        DataTable dtcategory = hd.GetTable("select RTRIM(name) AS NAME , RTRIM(value) AS VALUE from lookupmaster lm , lookupheadingmaster hm where head='TERMS AND CONDITION CATEGORY' and lm.headid=hm.srno ");
        ddlcategory.DataSource = dtcategory;
        ddlcategory.DataTextField = "NAME";
        ddlcategory.DataValueField = "VALUE";
        ddlcategory.DataBind();
       
        DataTable dtsubcategory = hd.GetTable("select RTRIM(name) AS NAME , RTRIM(value) AS VALUE from lookupmaster lm , lookupheadingmaster hm where head='TERMS AND CONDITION SUBCATEGORY' and lm.headid=hm.srno ");
        ddlsubcategory.DataSource = dtsubcategory;
        ddlsubcategory.DataTextField = "NAME";
        ddlsubcategory.DataValueField = "VALUE";
        ddlsubcategory.DataBind();
        ddlsubcategory.Items.Insert(0, "--Select--");
       
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtterms.Text.Trim().ToString() != string.Empty)
        {
            termscondition terms = new termscondition(HttpContext.Current.Server.MapPath("~/XML/database.xml"));

            string sql = "select TERMSVALUE from termscondition  where CATEGORY = '" + ddlcategory.SelectedItem.Text.Trim().ToString() + "'";
            DataTable dtterms = hd.GetTable(sql);
            int tvalue = 0;
            if (dtterms.Rows.Count > 0)
            {
                tvalue = General.Parse<int>(dtterms.Rows[0][0].ToString().Trim());
            }
            else
            {
                tvalue = 1;
            }
            terms.termscondition_SRNO = -1;
            terms.termscondition_CATEGORY = ddlcategory.SelectedItem.Text.Trim();
            if (ddlsubcategory.SelectedIndex > 0)
            {
                terms.termscondition_SUBCATEGORY = ddlsubcategory.SelectedItem.Text.Trim();
            }
            terms.termscondition_TERMS = txtterms.Text;
            terms.termscondition_TERMSVALUE = tvalue + 1;
            if (txtdesc.Text.Trim().ToString() != string.Empty)
            {
                terms.termscondition_DESCRIPTION = txtdesc.Text;
            }
            terms.termscondition_STATUS = 0;
            if (terms.Insert(true, "termscondition"))
            {
                gettermscondition();
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msgdsa", "showdiv();", true);
        }
    }
    protected void lnkedit_Click(object sender, EventArgs e)
    {
        
        LinkButton lnk = (LinkButton)sender;
        ViewState["termid"] = lnk.CommandArgument.ToString();
        Response.Redirect("termsdetails.aspx?termid="+ViewState["termid"].ToString());
    }
  
    protected void gvtermscondition_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvtermscondition.PageIndex = e.NewPageIndex;
        gvtermscondition.DataSource = (DataTable)ViewState["data"];
        gvtermscondition.DataBind();
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