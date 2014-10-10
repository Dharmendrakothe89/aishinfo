using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class addvehicle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("default.aspx");
            }
            FillVehicle();
            FillTransporter();
            FillCapacity();
        }
    }
    private void FillVehicle()
    {
        string sql = "SELECT VM.VEHICLEID,VM.VEHICLENAME,VM.VEHICLENO,VM.CAPACITY,TM.TRANSPORTERNAME,CASE WHEN VM.STATUS=0 THEN 'WORKING' ELSE 'NOT-WORKING' END AS STATUS" +
                   " FROM VEHICLEMASTER VM INNER JOIN TRANSPORTERMASTER TM ON  VM.TRANSPORTERID=TM.SRNO";
        Handler hdn = new Handler();
        DataTable dt = hdn.GetTable(sql);
        ViewState["list"] = dt;
        if (dt.Rows.Count > 0)
        {
            gvvehiclelist.DataSource = (DataTable)ViewState["list"];
            gvvehiclelist.DataBind();
        }
        else
        {
            gvvehiclelist.DataSource = null;
            gvvehiclelist.DataBind();
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
        ddlcapacity.DataValueField = "SRNO";
        ddlcapacity.DataBind();
        
    }
    protected void gvlookup_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvvehiclelist.PageIndex = e.NewPageIndex;
        gvvehiclelist.DataSource = (DataTable)ViewState["list"];
        gvvehiclelist.DataBind();
    }
    protected void lnkedit_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        Response.Redirect("vehicledetails.aspx?VEHICLEID=" + lnk.CommandArgument.ToString().Trim());
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
         Handler hdnpartycode=new Handler();
         DataTable dtpartycode = hdnpartycode.GetTable("SELECT VEHICLEID FROM VEHICLEMASTER VM WHERE TRANSPORTERID=" + ddltransporter.SelectedValue.ToString().Trim() + " AND VEHICLENO='" + txtvehicleno.Text.Trim().ToString() + "'");
         if (dtpartycode.Rows.Count == 0)
         {
             vehiclemaster objvehiclemaster = new vehiclemaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
             objvehiclemaster.vehiclemaster_VEHICLEID = -1;
             objvehiclemaster.vehiclemaster_TRANSPORTERID = General.Parse<int>(ddltransporter.SelectedValue.ToString().Trim());
             objvehiclemaster.vehiclemaster_STATUS = 0;
             objvehiclemaster.vehiclemaster_CAPACITY = ddlcapacity.SelectedItem.Text.Trim();
             objvehiclemaster.vehiclemaster_VEHICLENO = txtvehicleno.Text.Trim().ToString();
             objvehiclemaster.vehiclemaster_VEHICLENAME = txtvehiclename.Text.Trim().ToString();
             if (objvehiclemaster.Insert(true, "vehiclemaster"))
             {
                 MessageBox("Vehicle Added Successfully");
                 ddltransporter.SelectedIndex = 0;
                 ddlcapacity.SelectedIndex = 0;
                 txtvehicleno.Text = string.Empty;
                 txtvehiclename.Text = string.Empty;
                 FillVehicle();

             }
         }
         else
         {
             MessageBox("Vehicle Already Present");
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