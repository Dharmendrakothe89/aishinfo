using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class outwardlist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("default.aspx");
            }
            txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            FillData();
        }
    }

    private void FillData()
    {
        string sqlgodown = "SELECT SRNO,GODOWNNAME FROM GODOWNMASTER GM WHERE STATUS=0";
        Handler hdn = new Handler();
        DataTable dtgodown = hdn.GetTable(sqlgodown);
        ddldepot.DataSource = dtgodown;
        ddldepot.DataTextField = "GODOWNNAME";
        ddldepot.DataValueField = "SRNO";
        ddldepot.DataBind();

        string sqlcoaltype = "SELECT SRNO AS COALID, COALTYPE FROM COALMASTER CM WHERE STATUS=0 AND COALTYPEID IS NULL";
        Handler hdncoaltype = new Handler();
        DataTable dtcoaltype = hdncoaltype.GetTable(sqlcoaltype);
        ViewState["coaltype"] = dtcoaltype;

        string sqlgrade = "SELECT SRNO AS GRADEID, GRADE,COALTYPEID FROM COALMASTER CM WHERE STATUS=0 AND COALTYPEID IS NOT NULL";
        Handler hdngrade = new Handler();
        DataTable dtgrade = hdngrade.GetTable(sqlgrade);
        ViewState["grade"] = dtgrade;

        DataTable dtdata = FillGride();

        ViewState["stock"] = dtdata;
        gvcoaldetailsstock.DataSource = (DataTable)ViewState["stock"];
        gvcoaldetailsstock.DataBind();
    }
    private DataTable FillGride()
    {
        string sql = "SELECT IM.SRNO,IM.DATE,IM.SRNO,IM.COALTYPE,IM.GRADE,IM.QUANTITY,TM.TRANSPORTERNAME,VM.VEHICLENAME + '( ' + VM.VEHICLENO+' )' AS VEHICLENAME" +
                     " FROM INVENTORYMASTER IM INNER JOIN TRANSPORTERMASTER TM ON TM.SRNO=IM.TRANSPORTERID INNER JOIN VEHICLEMASTER VM ON VM.VEHICLEID=IM.VEHICLEID" +
                     " WHERE IM.STATUS=0 AND IM.TRNASACTIONTYPE='OUTWARD' AND IM.CMPID=" + Session["cmpid"].ToString() + " AND IM.DEPOTID=" + ddldepot.SelectedValue.Trim().ToString() + " AND" +
                     " convert(datetime, IM.DATE, 103) >= '" + txtfromdate.Text + "' AND convert(datetime, IM.DATE, 103) <='" + txttodate.Text + "'";
        Handler hdn = new Handler();
        DataTable dt = hdn.GetTable(sql);

        return dt;

    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        DataTable dtdata = FillGride();
        ViewState["stock"] = dtdata;
        gvcoaldetailsstock.DataSource = (DataTable)ViewState["stock"];
        gvcoaldetailsstock.DataBind();
    }

    protected void gvcoaldetailsstock_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvcoaldetailsstock.PageIndex = e.NewPageIndex;
            gvcoaldetailsstock.DataSource = (DataTable)ViewState["stock"];
            gvcoaldetailsstock.DataBind();
        }
        catch (Exception ex)
        {

        }
    }
}