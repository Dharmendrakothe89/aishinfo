using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Odbc;
using System.Reflection;
using System.Collections;
using System.Data.SqlClient;

public partial class collierydetails : System.Web.UI.Page
{
    string editbtn = "1";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("default.aspx");
            }
            ViewState["postback"] = "1";
            if (Request.QueryString["COLLIERYID"] != null)
            {
                ViewState["COLLIERYID"] = Request.QueryString["COLLIERYID"].ToString();
                FillData(ViewState["COLLIERYID"].ToString().Trim());
                DisableControl();
            }
            if (Request.QueryString["ID"] != null)
            {
                if (Request.QueryString["ID"].ToString() == "1")
                {
                    MessageBox("Colliery Added Successfully");
                }
                else if (Request.QueryString["ID"].ToString() == "2")
                {
                    MessageBox("Colliery Updated Successfully");
                }
            }
        }
        editbtn = ViewState["postback"].ToString();
    }

    private void DisableControl()
    {
        txtcollieryname.Enabled = false;
        txtcollierycode.Enabled = false;
        rdnagpur.Enabled = false;
        rdwani.Enabled = false;
    }
    private void FillData(string collieryid)
    {
        string sqlcolliery = "SELECT * FROM collierymaster CM WHERE CM.SRNO=" + collieryid;
        Handler hdncolliery = new Handler();
        
        DataTable dtcolliery = hdncolliery.GetTable(sqlcolliery);
        if (dtcolliery.Rows.Count > 0)
        {
            txtcollieryname.Text =dtcolliery.Rows[0]["COLLIERYNAME"].ToString().Trim();
            txtcollierycode.Text = dtcolliery.Rows[0]["COLLIERYCODE"].ToString().Trim();
            if (dtcolliery.Rows[0]["COLLIERYREGION"].ToString() == "NAGPUR DEPOT")
            {
                rdnagpur.Checked = true;
            }
            else if (dtcolliery.Rows[0]["COLLIERYREGION"].ToString() == "WANI DEPOT")
            {
                rdwani.Checked = true;
            }
        }

        string sqldetail = "SELECT * FROM collierydetailmaster CDM INNER JOIN COALMASTER CM ON CM.SRNO=CDM.COALID WHERE CM.STATUS=0 AND CDM.COLLIERYID=" + collieryid;
        Handler hdndetails = new Handler();
        DataTable dtsql = hdndetails.GetTable(sqldetail);
        ViewState["data"] = dtsql;
        coaltyperepeater.DataSource = dtsql;
        coaltyperepeater.DataBind();
    }
    protected void coaltyperepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            CheckBox chkcoaltype = (CheckBox)e.Item.FindControl("chkcoaltype");

            TextBox txtgrade = (TextBox)e.Item.FindControl("txtgrade");
            TextBox txtcommbenifitchrg = (TextBox)e.Item.FindControl("txtcommbenifitchrg");
            TextBox txtnotifiedprice = (TextBox)e.Item.FindControl("txtnotifiedprice");
            TextBox txtcrushingcharges = (TextBox)e.Item.FindControl("txtcrushingcharges");
            TextBox txtstc = (TextBox)e.Item.FindControl("txtstc");

            TextBox txtsed = (TextBox)e.Item.FindControl("txtsed");
            TextBox txtcec = (TextBox)e.Item.FindControl("txtcec");
            TextBox txtroyalty = (TextBox)e.Item.FindControl("txtroyalty");
            TextBox txtmprdtax = (TextBox)e.Item.FindControl("txtmprdtax");
            TextBox txttransitfee = (TextBox)e.Item.FindControl("txttransitfee");
            TextBox txtentryfee = (TextBox)e.Item.FindControl("txtentryfee");

            if (editbtn == "UPDATE")
            {
                chkcoaltype.Enabled = true;
                txtnotifiedprice.Enabled = true;
                txtgrade.Enabled = true;
                txtcommbenifitchrg.Enabled = true;
                txtcrushingcharges.Enabled = true;
                txtstc.Enabled = true;
                txtsed.Enabled = true;
                txtcec.Enabled = true;
                txtroyalty.Enabled = true;
                txtmprdtax.Enabled = true;
                txttransitfee.Enabled = true;
                txtentryfee.Enabled = true;
            }
            else
            {
                chkcoaltype.Checked = true;
                chkcoaltype.Enabled = false;
                txtnotifiedprice.Enabled = false;
                txtgrade.Enabled = false;
                txtcommbenifitchrg.Enabled = false;
                txtcrushingcharges.Enabled = false;
                txtstc.Enabled = false;
                txtsed.Enabled = false;
                txtcec.Enabled = false;
                txtroyalty.Enabled = false;
                txtmprdtax.Enabled = false;
                txttransitfee.Enabled = false;
                txtentryfee.Enabled = false;
                
            }
            
        }
    }
    
    protected void btnedit_Click(object sender, EventArgs e)
    {
        ViewState["postback"] = "UPDATE";
        editbtn = ViewState["postback"].ToString();
        txtcollieryname.Enabled = true;
        txtcollierycode.Enabled = true;
        rdnagpur.Enabled = true;
        rdwani.Enabled = true;
        ddlstatus.Enabled = true;
        btnupdate.Visible = true;

        string sql = "SELECT SRNO AS COALID,COALTYPE FROM coalmaster CM WHERE CM.STATUS=0 AND COALTYPEID IS NULL AND SRNO NOT IN (SELECT COALID FROM collierydetailmaster CDM WHERE CDM.STATUS=0 AND CDM.COLLIERYID=" + ViewState["COLLIERYID"].ToString().Trim() + ")";
        Handler hdncoaltype = new Handler();
        DataTable dtData = hdncoaltype.GetTable(sql);
        //dtData.Columns.Add("COALTYPE");
        dtData.Columns.Add("GRADE");
        dtData.Columns.Add("NOTIFIEDPRICE");
        dtData.Columns.Add("COMMBENIFITCHARGES");
        dtData.Columns.Add("CRUSHINGCHARGES");
        dtData.Columns.Add("STC");
        dtData.Columns.Add("SED");
        dtData.Columns.Add("CEC");
        dtData.Columns.Add("ROYALTY");
        dtData.Columns.Add("MPRDTAX");
        dtData.Columns.Add("TRANSITTAX");
        dtData.Columns.Add("ENTRYFEE");

        if (ViewState["data"] != null)
        {
            DataTable temp = (DataTable)ViewState["data"];
            //temp.Columns.Add("COALTYPE");
            int count = temp.Rows.Count;
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                temp.Rows.Add(1);
                temp.Rows[count]["COALID"] = dtData.Rows[i]["COALID"].ToString().Trim();
                temp.Rows[count]["COALTYPE"] = dtData.Rows[i]["COALTYPE"].ToString().Trim();
                count++;
            }
            

        }
        else
        {
            ViewState["data"] = dtData;
        }
        coaltyperepeater.DataSource = (DataTable)ViewState["data"];
        coaltyperepeater.DataBind();
    }
    
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        collierymaster objcolliery = new collierymaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
        objcolliery.collierymaster_SRNO = -1;
        objcolliery.collierymaster_COLLIERYNAME = txtcollieryname.Text.Trim().ToString();
        objcolliery.collierymaster_COLLIERYCODE = txtcollierycode.Text.Trim().ToString();
        objcolliery.collierymaster_CMPID = General.Parse<int>(Session["cmpid"].ToString());
        if (rdnagpur.Checked == true)
        {
            objcolliery.collierymaster_COLLIERYREGION = rdnagpur.Text.ToString();
        }
        else if (rdwani.Checked == true)
        {
            objcolliery.collierymaster_COLLIERYREGION = rdwani.Text.ToString();
        }
        objcolliery.collierymaster_STATUS = General.Parse<int>(ddlstatus.SelectedValue.ToString().Trim());
        string condition = "SRNO=" + ViewState["COLLIERYID"].ToString().Trim();
        if (objcolliery.Insert(false, "collierymaster", condition)) 
        {
             if (coaltyperepeater.Items.Count > 0)
            {

                SqlConnection Connection = new SqlConnection("Data Source=50.28.62.129,1433;Network Library=DBMSSOCN;Initial Catalog=db_fuel;User ID=fuel;Password= lSa2@11h");
                string qry = "delete from collierydetailmaster where collieryid=" + ViewState["COLLIERYID"].ToString();
                
                Connection.Open();
                SqlCommand com = new SqlCommand(qry, Connection);
                com.ExecuteNonQuery();
                Connection.Close();
                for (int i = 0; i < coaltyperepeater.Items.Count; i++)
                {

                    CheckBox chkcoaltype = (CheckBox)coaltyperepeater.Items[i].FindControl("chkcoaltype");
                    if (chkcoaltype.Checked == true)
                    {
                        TextBox grade = (TextBox)coaltyperepeater.Items[i].FindControl("txtgrade");
                        TextBox notifiedfrice = (TextBox)coaltyperepeater.Items[i].FindControl("txtnotifiedprice");
                        TextBox commbenifitchg = (TextBox)coaltyperepeater.Items[i].FindControl("txtcommbenifitchrg");
                        TextBox crushing = (TextBox)coaltyperepeater.Items[i].FindControl("txtcrushingcharges");
                        TextBox stc = (TextBox)coaltyperepeater.Items[i].FindControl("txtstc");
                        TextBox sed = (TextBox)coaltyperepeater.Items[i].FindControl("txtsed");
                        TextBox cec = (TextBox)coaltyperepeater.Items[i].FindControl("txtcec");
                        TextBox royalty = (TextBox)coaltyperepeater.Items[i].FindControl("txtroyalty");
                        TextBox mprdtax = (TextBox)coaltyperepeater.Items[i].FindControl("txtmprdtax");
                        TextBox transit = (TextBox)coaltyperepeater.Items[i].FindControl("txttransitfee");
                        TextBox entry = (TextBox)coaltyperepeater.Items[i].FindControl("txtentryfee");

                        collierydetailmaster objdetails = new collierydetailmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                        objdetails.collierydetailmaster_SRNO = -1;
                        objdetails.collierydetailmaster_COALID = General.Parse<int>(chkcoaltype.CssClass.ToString().Trim());
                        objdetails.collierydetailmaster_COLLIERYID = General.Parse<int>(ViewState["COLLIERYID"].ToString().Trim());
                        objdetails.collierydetailmaster_GRADE = grade.Text.Trim().ToString();
                        objdetails.collierydetailmaster_NOTIFIEDPRICE = General.Parse<double>(notifiedfrice.Text.Trim().ToString());
                        objdetails.collierydetailmaster_COMMBENIFITCHARGES = General.Parse<double>(commbenifitchg.Text.Trim().ToString());
                        objdetails.collierydetailmaster_CRUSHINGCHARGES = General.Parse<double>(crushing.Text.Trim().ToString());
                        objdetails.collierydetailmaster_STC = General.Parse<double>(stc.Text.Trim().ToString());
                        objdetails.collierydetailmaster_SED = General.Parse<double>(sed.Text.Trim().ToString());
                        objdetails.collierydetailmaster_CEC = General.Parse<double>(cec.Text.Trim().ToString());
                        objdetails.collierydetailmaster_ROYALTY = General.Parse<double>(royalty.Text.Trim().ToString());
                        objdetails.collierydetailmaster_MPRDTAX = General.Parse<double>(mprdtax.Text.Trim().ToString());
                        objdetails.collierydetailmaster_TRANSITTAX = General.Parse<double>(transit.Text.Trim().ToString());
                        objdetails.collierydetailmaster_ENTRYFEE = General.Parse<double>(entry.Text.Trim().ToString());
                        objdetails.collierydetailmaster_STATUS = 0;
                        if (objdetails.Insert(true, "collierydetailmaster"))
                        {
                        }
                    }


                }
                Response.Redirect("collierylist.aspx?ID=1");
            }
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