using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class createcolliery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("default.aspx");
            }
            DataTable dt= GetFillDataTable();
            coaltyperepeater.DataSource = dt;
            coaltyperepeater.DataBind();
        }
    }
    private DataTable GetFillDataTable()
    {
        string sql = "SELECT SRNO AS COALID,COALTYPE FROM coalmaster CM WHERE CM.STATUS=0 AND COALTYPEID IS NULL";
        Handler hdncoaltype = new Handler();
        DataTable dtData = hdncoaltype.GetTable(sql);
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
        
        return dtData;
    }
    protected void alpharepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
       
    }

    protected void coaltyperepeater_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        RadioButton AlphaTextBox = (RadioButton)e.Item.FindControl("chkcoaltype");
    }
    
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
         Handler hdnpartycode=new Handler();
         DataTable dtpartycode = hdnpartycode.GetTable("SELECT SRNO FROM COLLIERYMASTER PM WHERE STATUS=0 AND COLLIERYCODE='" + txtcollierycode.Text.Trim().ToString() + "'");
        if (dtpartycode.Rows.Count == 0)
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
            objcolliery.collierymaster_STATUS = 0;
            if (objcolliery.Insert(true, "collierymaster"))
            {
                string sqlmax = "SELECT MAX(CM.SRNO) FROM collierymaster CM WHERE STATUS=0 AND COLLIERYNAME='" + txtcollieryname.Text.ToString().Trim() + "'";
                Handler hdnmax = new Handler();
                DataTable dtmax = hdnmax.GetTable(sqlmax);
                if (dtmax.Rows.Count > 0 && coaltyperepeater.Items.Count > 0)
                {
                    for (int i = 0; i < coaltyperepeater.Items.Count; i++)
                    {

                        CheckBox chkcoaltype = (CheckBox)coaltyperepeater.Items[i].FindControl("chkcoaltype");
                        if (chkcoaltype.Checked == true)
                        {
                            TextBox grade = (TextBox)coaltyperepeater.Items[i].FindControl("txtgrade");
                            TextBox notifiedfrice = (TextBox)coaltyperepeater.Items[i].FindControl("txtnotifiedprice");
                            TextBox commbenifitchg = (TextBox)coaltyperepeater.Items[i].FindControl("txtcommbenifitchrg");
                            TextBox crushing = (TextBox)coaltyperepeater.Items[i].FindControl("txtcrushingchg");
                            TextBox stc = (TextBox)coaltyperepeater.Items[i].FindControl("txtstc");
                            TextBox sed = (TextBox)coaltyperepeater.Items[i].FindControl("txtsed");
                            TextBox cec = (TextBox)coaltyperepeater.Items[i].FindControl("txtroyalty");
                            TextBox royalty = (TextBox)coaltyperepeater.Items[i].FindControl("txtcec");
                            TextBox mprdtax = (TextBox)coaltyperepeater.Items[i].FindControl("txtmprdtax");
                            TextBox transit = (TextBox)coaltyperepeater.Items[i].FindControl("txttransitfee");
                            TextBox entry = (TextBox)coaltyperepeater.Items[i].FindControl("txtentryfee");

                            collierydetailmaster objdetails = new collierydetailmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                            objdetails.collierydetailmaster_SRNO = -1;
                            objdetails.collierydetailmaster_COALID = General.Parse<int>(chkcoaltype.CssClass.ToString().Trim());
                            objdetails.collierydetailmaster_COLLIERYID = General.Parse<int>(dtmax.Rows[0][0].ToString());
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
                    //Response.Redirect("collierydetails.aspx?ID=1&COLLIERYID=" + dtmax.Rows[0][0].ToString());
                    Response.Redirect("collierylist.aspx?ID=2");
                }
            }
        }
        else
        {
            MessageBox("Colliery Code Already Present");
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