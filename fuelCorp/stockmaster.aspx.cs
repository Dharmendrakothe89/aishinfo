using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class stockmaster : System.Web.UI.Page
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
            txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");

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
        DataTable dtdata=FillGride();
        ViewState["stock"]=dtdata;
        gvstock.DataSource = (DataTable)ViewState["stock"];
        gvstock.DataBind();
    }
    private DataTable FillGride()
    {
        DataTable dtcoal=(DataTable)ViewState["coaltype"];
        DataTable dtgrade=(DataTable)ViewState["grade"];
        DataTable dt = new DataTable();
        dt.Columns.Add("COALID");
        dt.Columns.Add("COALTYPE");
        dt.Columns.Add("GRADEID");
        dt.Columns.Add("GRADE");
        dt.Columns.Add("OPENING");
        dt.Columns.Add("INWARD");
        dt.Columns.Add("OUTWARD");
        dt.Columns.Add("BALANCE");
        
        for (int i = 0; i < dtcoal.Rows.Count; i++)
        {
            for (int j = 0; j < dtgrade.Rows.Count; j++)
            {
                if (dtcoal.Rows[i]["COALID"].ToString() == dtgrade.Rows[j]["COALTYPEID"].ToString())
                {
                    int count = dt.Rows.Count;
                    dt.Rows.Add(1);
                    dt.Rows[count]["COALID"] = dtcoal.Rows[i]["COALID"].ToString();
                    dt.Rows[count]["COALTYPE"] = dtcoal.Rows[i]["COALTYPE"].ToString();
                    dt.Rows[count]["GRADEID"] = dtgrade.Rows[j]["GRADEID"].ToString();
                    dt.Rows[count]["GRADE"] = dtgrade.Rows[j]["GRADE"].ToString();
                    DataTable dtopeningstock = GetCoalOpeningStock(ddldepot.SelectedValue.Trim().ToString(), dtcoal.Rows[i]["COALID"].ToString().Trim(), dtgrade.Rows[j]["GRADEID"].ToString(), txtfromdate.Text.Trim());
                    if (dtopeningstock.Rows.Count > 0 && dtopeningstock.Rows[0][0].ToString().Trim() != string.Empty)
                    {
                        dt.Rows[count]["OPENING"] = dtopeningstock.Rows[0]["STOCK"].ToString();
                    }
                    else
                    {
                        dt.Rows[count]["OPENING"] = "0";
                    }

                    DataTable dtstock = GetCoalStock(ddldepot.SelectedValue.Trim().ToString(), dtcoal.Rows[i]["COALID"].ToString().Trim(), dtgrade.Rows[j]["GRADEID"].ToString(), txtfromdate.Text.Trim(), txttodate.Text.Trim());
                    if (dtstock.Rows.Count > 0 && dtstock.Rows[0]["INWARD"].ToString().Trim() != string.Empty)
                    {
                        dt.Rows[count]["INWARD"] = dtstock.Rows[0]["INWARD"].ToString();
                        
                    }
                    else
                    {
                        dt.Rows[count]["INWARD"] = "0";
                        
                    }
                    if (dtstock.Rows.Count > 0 && dtstock.Rows[0]["OUTWARD"].ToString().Trim() != string.Empty)
                    {
                        
                        dt.Rows[count]["OUTWARD"] = dtstock.Rows[0]["OUTWARD"].ToString();
                    }
                    else
                    {
                        
                        dt.Rows[count]["OUTWARD"] = "0";
                    }
                    dt.Rows[count]["BALANCE"] = (((General.Parse<double>(dt.Rows[count]["INWARD"].ToString())) - (General.Parse<double>(dt.Rows[count]["OUTWARD"].ToString()))) + (General.Parse<double>(dt.Rows[count]["OPENING"].ToString()))).ToString();
                }
            }
        }
        return dt;
        
    }
    private DataTable GetCoalStock( string depotid, string coalid, string gradeid, string fromdate, string todate)
    {
        string sql = "SELECT SUM(CASE WHEN TRNASACTIONTYPE='INWARD' THEN IM.QUANTITY ELSE 0 END) AS INWARD,SUM(CASE WHEN TRNASACTIONTYPE='OUTWARD' THEN IM.QUANTITY ELSE 0 END) AS OUTWARD  FROM INVENTORYMASTER IM" +
                           " WHERE IM.STATUS=0 AND IM.CMPID="+Session["cmpid"].ToString()+" AND IM.DEPOTID=" + depotid.Trim().ToString() + " AND IM.COALID=" + coalid.ToString() + " AND IM.GRADEID=" + gradeid.ToString() + " AND " +
                           " convert(datetime, IM.DATE, 103) >= '" + txtfromdate.Text + "' AND convert(datetime, IM.DATE, 103) <='" + txttodate.Text + "'";
                Handler hdn = new Handler();
                DataTable dtstock = hdn.GetTable(sql);
                return dtstock;
    }
    private DataTable GetCoalOpeningStock(string depotid, string coalid, string gradeid, string fromdate)
    {
        string sql = "SELECT SUM(CASE WHEN TRNASACTIONTYPE='INWARD' THEN IM.QUANTITY ELSE 0 END) - SUM(CASE WHEN TRNASACTIONTYPE='OUTWARD' THEN IM.QUANTITY ELSE 0 END) AS STOCK  FROM INVENTORYMASTER IM" +
                           " WHERE IM.STATUS=0 AND IM.CMPID=" + Session["cmpid"].ToString() + " AND IM.DEPOTID=" + depotid.Trim().ToString() + " AND IM.COALID=" + coalid.ToString() + " AND IM.GRADEID=" + gradeid.ToString() + " AND " +
                           " convert(datetime, IM.DATE, 103) <'" + fromdate + "'";
        Handler hdn = new Handler();
        DataTable dtstock = hdn.GetTable(sql);
        return dtstock;
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        DataTable dtdata = FillGride();
        ViewState["stock"] = dtdata;
        gvstock.DataSource = (DataTable)ViewState["stock"];
        gvstock.DataBind();
    }
    protected void gvstock_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvstock.PageIndex = e.NewPageIndex;
            gvstock.DataSource = (DataTable)ViewState["stock"];
            gvstock.DataBind();
        }
        catch (Exception ex)
        {
           
        }
    }
    protected void lnkselect_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        string[] id = lnk.CommandArgument.Split('-');
        string sql = "SELECT IM.SRNO,IM.DATE,IM.SRNO,IM.COALTYPE,IM.GRADE,CASE WHEN IM.TRNASACTIONTYPE='INWARD' THEN IM.QUANTITY ELSE 0 END AS INWARD," +
                    " CASE WHEN IM.TRNASACTIONTYPE='OUTWARD' THEN IM.QUANTITY ELSE 0 END AS OUTWARD,TM.TRANSPORTERNAME,VM.VEHICLENAME + '( ' + VM.VEHICLENO+' )' AS VEHICLENAME"+
                    " FROM INVENTORYMASTER IM INNER JOIN TRANSPORTERMASTER TM ON TM.SRNO=IM.TRANSPORTERID INNER JOIN VEHICLEMASTER VM ON VM.VEHICLEID=IM.VEHICLEID"+
                    " WHERE IM.STATUS=0 AND IM.CMPID=" + Session["cmpid"].ToString() + " AND IM.DEPOTID=" + ddldepot.SelectedValue.Trim().ToString() + " AND IM.COALID=1 AND IM.GRADEID=4 AND" +
                    " convert(datetime, IM.DATE, 103) >= '" + txtfromdate.Text + "' AND convert(datetime, IM.DATE, 103) <= '" + txttodate.Text + "'";
        Handler hdn = new Handler();
        DataTable dt = hdn.GetTable(sql);
        gvstock.Visible = false;
        gvcoaldetailsstock.Visible = true;
        ViewState["inventorydetails"] = dt;
        gvcoaldetailsstock.DataSource = (DataTable)ViewState["inventorydetails"];
        gvcoaldetailsstock.DataBind();

        btnback.Visible = true;
        btnsearch.Visible = false;
    }
    protected void gvcoaldetailsstock_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvcoaldetailsstock.PageIndex = e.NewPageIndex;
            gvcoaldetailsstock.DataSource = (DataTable)ViewState["inventorydetails"];
            gvcoaldetailsstock.DataBind();
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        gvstock.Visible = true;
        gvcoaldetailsstock.Visible = false;
        btnback.Visible = false;
        btnsearch.Visible = true;
    }
}