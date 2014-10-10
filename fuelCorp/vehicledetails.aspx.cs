using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class vehicledetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("default.aspx");
            }
            FillTransporter();
            FillCapacity();
            if (Request.QueryString["VEHICLEID"] != null)
            {
                ViewState["VEHICLEID"] = Request.QueryString["VEHICLEID"].ToString();
            }
            
            FillVehicle(ViewState["VEHICLEID"].ToString());
        }
    }
    private void FillVehicle(string vehicleid)
    {
        string sql = "SELECT VM.TRANSPORTERID,VM.VEHICLENAME,VM.VEHICLENO,VM.CAPACITY,VM.STATUS FROM VEHICLEMASTER VM WHERE VEHICLEID=" + vehicleid;
        Handler hdn = new Handler();
        DataTable dt = hdn.GetTable(sql);
        if (dt.Rows.Count > 0)
        {
            txtvehiclename.Text = dt.Rows[0]["VEHICLENAME"].ToString();
            txtvehicleno.Text = dt.Rows[0]["VEHICLENO"].ToString();
            ddltransporter.SelectedValue = dt.Rows[0]["TRANSPORTERID"].ToString();
            ddlcapacity.SelectedValue = dt.Rows[0]["CAPACITY"].ToString();
            if (dt.Rows[0]["STATUS"].ToString().Trim() == "True")
            {
                ddlstatus.SelectedValue = "1";
            }
            else
            {
                ddlstatus.SelectedValue = "0";
            }
        }
      
    }
    private void FillTransporter()
    {
        string sql = "SELECT TM.SRNO, TM.TRANSPORTERNAME FROM  transportermaster TM WHERE TM.STATUS=0";
        Handler hdn = new Handler();
        DataTable dt = hdn.GetTable(sql);
        ddltransporter.DataSource = dt;
        ddltransporter.DataTextField = "TRANSPORTERNAME";
        ddltransporter.DataValueField = "SRNO";
        ddltransporter.DataBind();
       
    }

    private void FillCapacity()
    {
        string sqldepartment = "SELECT LM.NAME,LM.SRNO FROM LOOKUPMASTER LM INNER JOIN LOOKUPHEADINGMASTER LHM ON LHM.SRNO=LM.HEADID WHERE LM.STATUS=0 AND LHM.STATUS=0 AND LHM.HEAD='VEHICLE CAPACITY' ORDER BY LM.NAME";
        Handler hdndepartment = new Handler();
        DataTable dtdepartment = hdndepartment.GetTable(sqldepartment);
        ddlcapacity.DataSource = dtdepartment;
        ddlcapacity.DataTextField = "NAME";
        ddlcapacity.DataValueField = "NAME";
        ddlcapacity.DataBind();
       
    }
    protected void btnedit_Click(object sender, EventArgs e)
    {
        ddltransporter.Enabled = true;
        txtvehiclename.Enabled = true;
        txtvehicleno.Enabled = true;
        ddlcapacity.Enabled = true;
        btnsubmit.Visible = true;
        ddlstatus.Enabled = true;
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        vehiclemaster objvehiclemaster = new vehiclemaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
        objvehiclemaster.vehiclemaster_VEHICLEID = -1;
        objvehiclemaster.vehiclemaster_TRANSPORTERID = General.Parse<int>(ddltransporter.SelectedValue.ToString().Trim());
        objvehiclemaster.vehiclemaster_STATUS = General.Parse<int>(ddlstatus.SelectedValue.ToString().Trim());
        objvehiclemaster.vehiclemaster_CAPACITY = ddlcapacity.SelectedItem.Text.Trim();
        objvehiclemaster.vehiclemaster_VEHICLENO = txtvehicleno.Text.Trim().ToString();
        objvehiclemaster.vehiclemaster_VEHICLENAME = txtvehiclename.Text.Trim().ToString();
        string condition = "VEHICLEID=" + ViewState["VEHICLEID"].ToString();
        if (objvehiclemaster.Insert(false, "vehiclemaster", condition))
        {
            MessageBox("Vehicle Updated Successfully");
            Response.Redirect("addvehicle.aspx");
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