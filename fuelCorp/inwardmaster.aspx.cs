using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class inwardmaster : System.Web.UI.Page
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
        string sqlcolliery = "SELECT DM.DOID,DM.DONO FROM DOMASTER DM WHERE DM.STATUS=0";
        Handler hdncolliery = new Handler();
        DataTable dtcolliery = hdncolliery.GetTable(sqlcolliery);
        ddldo.DataSource = dtcolliery;
        ddldo.DataTextField = "DONO";
        ddldo.DataValueField = "DOID";
        ddldo.DataBind();
        ddldo.Items.Insert(0, "--Select--");

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

    }

    protected void ddldo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldo.SelectedIndex > 0)
        {
            DataTable dt = FillDoDate(ddldo.SelectedValue.Trim().ToString());
            if (dt.Rows.Count > 0)
            {
                txtdodate.Text = dt.Rows[0][0].ToString().Trim();
                ViewState["TYPE"] = dt.Rows[0][1].ToString().Trim();
                ddlcoaltype.SelectedValue = dt.Rows[0]["COALID"].ToString().Trim();
                DataTable dt1 = FillCoalGrade(dt.Rows[0]["COALID"].ToString().Trim());
                if (dt1.Rows.Count > 0)
                {
                    ddlcoalgrade.DataSource = dt1;
                    ddlcoalgrade.DataTextField = "GRADE";
                    ddlcoalgrade.DataValueField = "SRNO";
                    ddlcoalgrade.DataBind();
                    ddlcoalgrade.Items.Insert(0, "-- Select Grade --");
                    ddlcoalgrade.Enabled = true;
                    ddlcoaltype.Enabled = true;
                    //ddlcoalgrade.Items.FindByText(dt.Rows[0]["GRADE"].ToString().Trim()).Selected = true;
                    //ddlcoalgrade.Enabled = false;
                    ddlcoaltype.Enabled = false;
                }
            }
        }
        else
        {
            txtdodate.Text = string.Empty;
        }

    }
    private DataTable FillDoDate(string doid)
    {
        string sqlcolliery = "SELECT DM.DODATE,DM.DOFOR,DD.COALID,RTRIM(DD.GRADE) AS GRADE FROM DOMASTER DM INNER JOIN DODETAILMASTER DD ON DM.DOID=DD.DOID WHERE DM.STATUS=0 AND DM.DOID=" + doid.Trim().ToString();
        Handler hdncolliery = new Handler();
        DataTable dtcolliery = hdncolliery.GetTable(sqlcolliery);
        return dtcolliery;
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
        objmaster.inventorymaster_DOID = General.Parse<int>(ddldo.SelectedValue.Trim().ToString());
        objmaster.inventorymaster_GRADEID = General.Parse<int>(ddlcoalgrade.SelectedValue.Trim().ToString());
        objmaster.inventorymaster_GRADE = ddlcoalgrade.SelectedItem.Text.Trim().ToString();
        objmaster.inventorymaster_QUANTITY = General.Parse<double>(txtquantity.Text.Trim().ToString());
        objmaster.inventorymaster_TRNASACTIONTYPE = "INWARD";
        objmaster.inventorymaster_TRANSPORTERID = General.Parse<int>(ddltransporter.SelectedValue.Trim().ToString());
        objmaster.inventorymaster_VEHICLEID = General.Parse<int>(ddlvehicle.SelectedValue.Trim().ToString());
        objmaster.inventorymaster_DESTINATION = "DEPOT";
        objmaster.inventorymaster_TYPE = ViewState["TYPE"].ToString().Trim();
        objmaster.inventorymaster_STATUS = 0;
        objmaster.inventorymaster_PARTYID = -1;
        if (objmaster.Insert(true, "inventorymaster"))
        {
            ClearControls();
            MessageBox("Coal Inward successfully");
        }
    }
    private void ClearControls()
    {
        ddldepot.SelectedIndex = 0;
        txtdate.Text = DateTime.Now.ToString("dd/MM/yyyy");

        ddldo.SelectedIndex = 0;
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
        txtdodate.Text = string.Empty;
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