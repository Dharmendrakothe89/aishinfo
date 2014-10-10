using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class transporterlist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("default.aspx");
            }
            if (Request.QueryString["ID"] != null)
            {
                if (Request.QueryString["ID"].ToString() == "1")
                {
                    MessageBox("Transporter Updated Successfully");
                }
                else if (Request.QueryString["ID"].ToString() == "2")
                {
                    MessageBox("Transporter Created Successfully");
                }
            }
            DataTable dttransporter = FillTransporterList();
            ViewState["list"] = dttransporter;
            if (dttransporter.Rows.Count > 0)
            {
                gvtransporterlist.DataSource =  (DataTable)ViewState["list"];
                gvtransporterlist.DataBind();
            }
            else
            {
                gvtransporterlist.DataSource = null;
                gvtransporterlist.DataBind();
            }
        }
       
    }
    private DataTable FillTransporterList()
    {
        string sqlpartylist = "SELECT TM.SRNO AS TRANSPORTERID,TM.TRANSPORTERNAME,TM.TRANSPORTERCODE,TM.TRANTYPE,TM.MOBILENO,TM.EMAILID,TM.ADDRESS,CASE WHEN TM.STATUS=0 THEN 'ACTIVE' ELSE 'DE-ACTIVE' END AS STATUS FROM transportermaster TM WHERE TM.STATUS=0";
        Handler hdnpartylist = new Handler();
        DataTable dtpartylist = hdnpartylist.GetTable(sqlpartylist);
        return dtpartylist;
    }
    protected void gvlookup_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvtransporterlist.PageIndex = e.NewPageIndex;
        gvtransporterlist.DataSource = (DataTable)ViewState["list"];
        gvtransporterlist.DataBind();
    }
    protected void lnkdetails_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        Response.Redirect("transporterdetails.aspx?TRANSPORTERID=" + lnk.CommandArgument.ToString().Trim());
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