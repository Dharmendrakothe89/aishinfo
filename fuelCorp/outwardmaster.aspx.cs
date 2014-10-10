using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class outwardmaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("default.aspx");
            }
            FillData();
            txtdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
    }

    private void FillData()
    {
        string sqlcolliery = "SELECT LM.PARTYNAME,LM.SRNO FROM PARTYMASTER LM ORDER BY LM.PARTYNAME";
        Handler hdncolliery = new Handler();
        DataTable dtcolliery = hdncolliery.GetTable(sqlcolliery);
        ddlparty.DataSource = dtcolliery;
        ddlparty.DataTextField = "PARTYNAME";
        ddlparty.DataValueField = "SRNO";
        ddlparty.DataBind();
        ddlparty.Items.Insert(0, "--Select--");

        string sqldepartment = "SELECT SRNO,TRANSPORTERNAME FROM TRANSPORTERMASTER TM WHERE STATUS=0";
        Handler hdndepartment = new Handler();
        DataTable dtdepartment = hdndepartment.GetTable(sqldepartment);
        ddltransporter.DataSource = dtdepartment;
        ddltransporter.DataTextField = "TRANSPORTERNAME";
        ddltransporter.DataValueField = "SRNO";
        ddltransporter.DataBind();
        ddltransporter.Items.Insert(0, "-- Party --");

        string sqlcoaltype = "SELECT SRNO,COALTYPE FROM COALMASTER CM WHERE STATUS=0 AND COALTYPEID IS NULL";
        Handler hdncoaltype = new Handler();
        DataTable dtcoaltype = hdncoaltype.GetTable(sqlcoaltype);
        ddlcoaltype.DataSource = dtcoaltype;
        ddlcoaltype.DataTextField = "COALTYPE";
        ddlcoaltype.DataValueField = "SRNO";
        ddlcoaltype.DataBind();
        ddlcoaltype.Items.Insert(0, "-- Select Coal --");

        string sqlgodown = "SELECT SRNO,GODOWNNAME FROM GODOWNMASTER GM WHERE STATUS=0";
        Handler hdn = new Handler();
        DataTable dtgodown = hdn.GetTable(sqlgodown);
        ddldepot.DataSource = dtgodown;
        ddldepot.DataTextField = "GODOWNNAME";
        ddldepot.DataValueField = "SRNO";
        ddldepot.DataBind();
        ddldepot.Items.Insert(0, "-- Select Depot --");
    }

    protected void ddlcoaltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcoaltype.SelectedIndex > 0)
        {
            DataTable dt = FillCoalGrade(ddlcoaltype.SelectedValue.Trim().ToString());
            if (dt.Rows.Count > 0)
            {
                ddlcoalgrade.DataSource = dt;
                ddlcoalgrade.DataTextField = "GRADE";
                ddlcoalgrade.DataValueField = "SRNO";
                ddlcoalgrade.DataBind();
                ddlcoalgrade.Items.Insert(0, "-- Select Grade --");
                ddlcoalgrade.Enabled = true;
            }
            else
            {
                ddlcoalgrade.Enabled = false;
            }
        }
        else
        {
            ddlcoalgrade.Enabled = false;
        }

    }
    private DataTable FillCoalGrade(string coalid)
    {
        string sqlcoaltype = "SELECT SRNO,GRADE FROM COALMASTER CM WHERE GRADE IS NOT NULL AND  STATUS=0 AND COALTYPEID=" + coalid;
        Handler hdncoaltype = new Handler();
        DataTable dtcoaltype = hdncoaltype.GetTable(sqlcoaltype);
        return dtcoaltype;
    }
    protected void ddltransporter_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltransporter.SelectedIndex > 0)
        {
            DataTable dt = FillVehicle(ddltransporter.SelectedValue.Trim().ToString());
            if (dt.Rows.Count > 0)
            {
                ddlvehicle.DataSource = dt;
                ddlvehicle.DataTextField = "VEHICLENAME";
                ddlvehicle.DataValueField = "VEHICLEID";
                ddlvehicle.DataBind();
                ddlvehicle.Items.Insert(0, "-- Select Vehicle --");
                ddlvehicle.Enabled = true;
            }
            else
            {
                MessageBox("There is no vehicle present");
                ddlvehicle.Enabled = false;
            }
        }
        else
        {
            ddlvehicle.Enabled = false;
            
        }
    }
    private DataTable FillVehicle(string vehicleid)
    {
        string sqlcoaltype = "SELECT VEHICLEID,VEHICLENAME+VEHICLENO AS VEHICLENAME FROM VEHICLEMASTER VM WHERE STATUS=0 AND TRANSPORTERID=" + vehicleid;
        Handler hdncoaltype = new Handler();
        DataTable dtcoaltype = hdncoaltype.GetTable(sqlcoaltype);
        return dtcoaltype;
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        inventorymaster objmaster = new inventorymaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
        objmaster.inventorymaster_SRNO = -1;
        objmaster.inventorymaster_CMPID = General.Parse<int>(Session["cmpid"].ToString());
        objmaster.inventorymaster_COALID = General.Parse<int>(ddlcoaltype.SelectedValue.Trim().ToString());
        objmaster.inventorymaster_COALTYPE = ddlcoaltype.SelectedItem.Text.Trim().ToString();
        objmaster.inventorymaster_DATE = txtdate.Text.Trim().ToString();
        objmaster.inventorymaster_DEPOTID = General.Parse<int>(ddldepot.SelectedValue.Trim().ToString());
        objmaster.inventorymaster_PARTYID = General.Parse<int>(ddlparty.SelectedValue.Trim().ToString());
        objmaster.inventorymaster_GRADE = ddlcoalgrade.SelectedItem.Text.Trim().ToString();
        objmaster.inventorymaster_GRADEID = General.Parse<int>(ddlcoalgrade.SelectedValue.Trim().ToString());
        objmaster.inventorymaster_QUANTITY = General.Parse<double>(txtquantity.Text.Trim().ToString());
        objmaster.inventorymaster_TRNASACTIONTYPE = "OUTWARD";
        objmaster.inventorymaster_TRANSPORTERID = General.Parse<int>(ddltransporter.SelectedValue.Trim().ToString());
        objmaster.inventorymaster_VEHICLEID = General.Parse<int>(ddlvehicle.SelectedValue.Trim().ToString());
        //objmaster.inventorymaster_VEHICLEID = 1;
        objmaster.inventorymaster_DESTINATION = txtdestination.Text.Trim().ToString();
        objmaster.inventorymaster_TYPE = "LINKEDCUSTOMER";
        objmaster.inventorymaster_STATUS = 0;
        objmaster.inventorymaster_DOID = -1;
        if (objmaster.Insert(true, "inventorymaster"))
        {
            ClearControls();
            MessageBox("Coal Outward successfully");
        }
    }
    private void ClearControls()
    {
        ddldepot.SelectedIndex = 0;
        txtdate.Text = DateTime.Now.ToString("dd/MM/yyyy");

        ddlparty.SelectedIndex = 0;
        ddlcoaltype.SelectedIndex = 0;
        txtquantity.Text = string.Empty;
        ddltransporter.SelectedIndex = 0;

        ddlvehicle.Items.Clear();
        ddlvehicle.DataSource = null;
        ddlvehicle.DataBind();
        ddlvehicle.Enabled = false;

        ddlcoalgrade.DataSource = null;
        ddlcoalgrade.DataBind();
        ddlcoalgrade.Enabled = false;
       
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