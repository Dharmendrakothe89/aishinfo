using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class createbranch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("default.aspx");
            }
           FillState();
        }
    }
    private void FillState()
    {
        string sqlstate = "SELECT RTRIM(STATENAME) AS STATENAME,RTRIM(STATEID) AS STATEID FROM STATEMASTER SM WHERE SM.STATUS=0 ORDER BY STATENAME";
        Handler hdnstate = new Handler();
        DataTable dtstate = hdnstate.GetTable(sqlstate);

        ddlstate.DataSource = dtstate;
        ddlstate.DataTextField = "STATENAME";
        ddlstate.DataValueField = "STATEID";
        ddlstate.DataBind();

        DataTable dtcity = FillCity(dtstate.Rows[0]["STATEID"].ToString().Trim());
        ddlcity.Enabled = true;
        ddlcity.DataSource = dtcity;
        ddlcity.DataTextField = "CITYNAME";
        ddlcity.DataValueField = "CITYID";
        ddlcity.DataBind();
    }
    private DataTable FillCity(string stateid)
    {
        string sqlcity = "SELECT RTRIM(CITYNAME) AS CITYNAME,RTRIM(CITYID) AS CITYID FROM CITYMASTER CM WHERE CM.STATUS=0 AND CM.STATEID=" + stateid + " ORDER BY CM.CITYNAME";
        Handler hdncity = new Handler();
        DataTable dtcity = hdncity.GetTable(sqlcity);
        return dtcity;
    }
    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {
            DataTable dtcity = FillCity(ddlstate.SelectedValue.ToString());
            ddlcity.Enabled = true;
            ddlcity.DataSource = dtcity;
            ddlcity.DataTextField = "CITYNAME";
            ddlcity.DataValueField = "CITYID";
            ddlcity.DataBind();
   }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        branchmaster objbranch = new branchmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
        objbranch.branchmaster_BRANCHID = -1;
        objbranch.branchmaster_BRANCHNAME=txtbranchname.Text.Trim().ToString();
        objbranch.branchmaster_CITYID = General.Parse<int>(ddlcity.SelectedValue.ToString());
        objbranch.branchmaster_CITYNAME=ddlcity.SelectedItem.Text.ToString().Trim();
        objbranch.branchmaster_STATEID = General.Parse<int>(ddlstate.SelectedValue.ToString());
        objbranch.branchmaster_STATENAME = ddlstate.SelectedItem.Text.ToString().Trim();
        objbranch.branchmaster_STATUS=0;
        objbranch.branchmaster_ADDRESS=txtaddress.Text.Trim().ToString();
        objbranch.branchmaster_CMPID = General.Parse<int>(Session["cmpid"].ToString());
        if (objbranch.Insert(true, "branchmaster"))
        {
            Handler hd = new Handler();
         int branchid= Convert.ToInt32(  hd.GetTable("select MAX(branchid) from branchmaster").Rows[0][0]);
            //call procedurre
            SqlConnection conn = new
                SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString());
            conn.Open();

            
            SqlCommand cmd = new SqlCommand(
                "sp_createbranchledger", conn);

           
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.Add(
                new SqlParameter("@branch_id", branchid));


            SqlDataReader rdr = cmd.ExecuteReader();
            
            Response.Redirect("branchlist.aspx?id=1");
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