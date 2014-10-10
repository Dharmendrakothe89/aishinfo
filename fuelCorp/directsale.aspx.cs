using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class directsale : System.Web.UI.Page
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
        string sqldepartment = "SELECT SRNO,TRANSPORTERNAME FROM TRANSPORTERMASTER TM WHERE STATUS=0";
        Handler hdndepartment = new Handler();
        DataTable dtdepartment = hdndepartment.GetTable(sqldepartment);
        ddltransporter.DataSource = dtdepartment;
        ddltransporter.DataTextField = "TRANSPORTERNAME";
        ddltransporter.DataValueField = "SRNO";
        ddltransporter.DataBind();
        ddltransporter.Items.Insert(0, "-- Transporter --");

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

        string sqldesignation = "SELECT LM.NAME,LM.SRNO FROM LOOKUPMASTER LM INNER JOIN LOOKUPHEADINGMASTER LHM ON LHM.SRNO=LM.HEADID WHERE LM.STATUS=0 AND LHM.STATUS=0 AND LHM.HEAD='DESIGNATION' ORDER BY LM.NAME";
        Handler hdndesignation = new Handler();
        DataTable dtdesignation = hdndesignation.GetTable(sqldesignation);
        ddldesignation.DataSource = dtdesignation;
        ddldesignation.DataTextField = "NAME";
        ddldesignation.DataValueField = "SRNO";
        ddldesignation.DataBind();
        ddldesignation.Items.Insert(0, "-- Designation --");

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
        string sqlcoaltype = "SELECT VEHICLEID,VEHICLENAME+'( '+VEHICLENO+ ')' AS VEHICLENAME FROM VEHICLEMASTER VM WHERE STATUS=0 AND TRANSPORTERID=" + vehicleid;
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
        objmaster.inventorymaster_DOID = -1;
        objmaster.inventorymaster_GRADEID = General.Parse<int>(ddlcoalgrade.SelectedValue.Trim().ToString());
        objmaster.inventorymaster_GRADE = ddlcoalgrade.SelectedItem.Text.Trim().ToString();
        objmaster.inventorymaster_QUANTITY = General.Parse<double>(txtquantity.Text.Trim().ToString());
        objmaster.inventorymaster_TRNASACTIONTYPE = "OUTWARD";
        objmaster.inventorymaster_TRANSPORTERID = General.Parse<int>(ddltransporter.SelectedValue.Trim().ToString());
        objmaster.inventorymaster_VEHICLEID = General.Parse<int>(ddlvehicle.SelectedValue.Trim().ToString());
        objmaster.inventorymaster_DESTINATION = txtdestination.Text.Trim();
        objmaster.inventorymaster_TYPE = "DIRECTCUSTOMER";
        objmaster.inventorymaster_STATUS = 0;
        objmaster.inventorymaster_PARTYID = -1;
        if (objmaster.Insert(true, "inventorymaster"))
        {
            string sqlmax = "SELECT MAX(SRNO) AS SRNO FROM INVENTORYMASTER IM WHERE CMPID=" + Session["cmpid"].ToString() + " AND COALID=" + ddlcoaltype.SelectedValue.Trim().ToString() + " AND GRADEID=" + ddlcoalgrade.SelectedValue.Trim().ToString() + " AND QUANTITY='" + txtquantity.Text.Trim().ToString() + "'";
            Handler hdnmax = new Handler();
            DataTable dtmax = hdnmax.GetTable(sqlmax);

            personalmaster objpersonal = new personalmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
            objpersonal.personalmaster_SRNO = -1;
            objpersonal.personalmaster_PERSONNAME = txtpersonname.Text.ToString().Trim();
            objpersonal.personalmaster_DESIGNATION = ddldesignation.SelectedItem.Text.Trim().ToString();
            objpersonal.personalmaster_EMAILID = txtemailid.Text.ToString().Trim();
            objpersonal.personalmaster_MOBILE = txtpersonno.Text.ToString().Trim();
            objpersonal.personalmaster_PERSONRELATIONID = General.Parse<int>(dtmax.Rows[0][0].ToString());
            objpersonal.personalmaster_PERSONTYPE = "SALE";
            if (objpersonal.Insert(true, "personalmaster"))
            {
                string sqlmax1 = "SELECT MAX(SRNO) AS SRNO FROM PERSONALMASTER GM WHERE PERSONTYPE='SALE' AND PERSONRELATIONID='" + dtmax.Rows[0][0].ToString() + "'";
                Handler hdnmax1 = new Handler();
                DataTable dtmax1 = hdnmax1.GetTable(sqlmax1);

                inventorymaster objmaster1 = new inventorymaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                objmaster1.inventorymaster_SRNO = -1;
                objmaster1.inventorymaster_CMPID = -1;
                objmaster1.inventorymaster_COALID = -1;
                
                
                objmaster1.inventorymaster_DEPOTID = -1;
                objmaster1.inventorymaster_DOID = -1;
                objmaster1.inventorymaster_GRADEID = -1;
                objmaster1.inventorymaster_QUANTITY = -1;
                
                objmaster1.inventorymaster_TRANSPORTERID = -1;
                objmaster1.inventorymaster_VEHICLEID = -1;
                
                
                objmaster1.inventorymaster_STATUS = -1;
                objmaster1.inventorymaster_PARTYID = General.Parse<int>(dtmax.Rows[0][0].ToString().Trim());
                string condition = "SRNO=" + dtmax.Rows[0][0].ToString();
                if (objmaster1.Insert(false, "inventorymaster", condition))
                {
                }
            }
        }
        

        ClearControls();
        MessageBox("Sales Successfully");
       
    }
    private void ClearControls()
    {
        ddldepot.SelectedIndex = 0;
        txtdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        ddlcoaltype.SelectedIndex = 0;
        txtquantity.Text = string.Empty;
        ddltransporter.SelectedIndex = 0;
        ddlvehicle.DataSource = null;
        ddlvehicle.DataBind();
        ddlvehicle.Enabled = false;
        ddlcoalgrade.DataSource = null;
        ddlcoalgrade.DataBind();
        ddlcoalgrade.Enabled = false;
        ddldesignation.SelectedIndex = 0;
        txtnewpartyname.Text = string.Empty;
        txtnewpartycode.Text = string.Empty;
        txtnewpartyaddress.Text = string.Empty;
        txtnewpartyno.Text = string.Empty;
        txtpersonname.Text = string.Empty;
        txtemailid.Text = string.Empty;
        txtpersonno.Text = string.Empty;
        txtdestination.Text = string.Empty;
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