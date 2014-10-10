using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class taxinvoice : System.Web.UI.Page
{
    DataTable dt = new DataTable();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        dt.Columns.Add("Srno", typeof(int));
        dt.Columns.Add("DesptDt", typeof(string));
        dt.Columns.Add("RecdDt", typeof(string));
        dt.Columns.Add("Biltno", typeof(int));
        dt.Columns.Add("truckno", typeof(string));
        dt.Columns.Add("DispatchWeigth", typeof(double));
        dt.Columns.Add("Rate", typeof(int));
        dt.Columns.Add("Amount", typeof(int));
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("default.aspx");
            }
           
        }
    }
    protected void lnkadd_Click(object sender, EventArgs e)
    {
        dt.Rows.Add();
       // dt.Rows[dt.Rows.Count - 1]["Srno"] = dt.Rows.Count;
        dt.Rows[dt.Rows.Count - 1]["DesptDt"] = txtdesptdt.Text;
        dt.Rows[dt.Rows.Count - 1]["RecdDt"] =txtrecddt.Text;
        dt.Rows[dt.Rows.Count - 1]["Biltno"] = Convert.ToInt32(txtbilty.Text);
        dt.Rows[dt.Rows.Count - 1]["truckno"] = ddltruckno.SelectedItem.Text;
        dt.Rows[dt.Rows.Count - 1]["DispatchWeigth"] =Convert.ToDouble( txtweight.Text);
        dt.Rows[dt.Rows.Count - 1]["Rate"] = Convert.ToInt32(txtrate.Text);
        dt.Rows[dt.Rows.Count - 1]["Amount"] = Convert.ToInt32(txtamount.Text);

        gvinvoicelist.DataSource = dt;
        gvinvoicelist.DataBind();
    }
    private DataTable GetFillDataTable(int value)
    {
        DataTable dtData = new DataTable();
        dtData.Columns.Add("SRNO");
        dtData.Columns.Add("PERSONNAME");
        dtData.Columns.Add("DESIGNATION");
        dtData.Columns.Add("DEPARTMENT");
        dtData.Columns.Add("EMIALID");
        dtData.Columns.Add("PHONE");
        dtData.Columns.Add("MOBILE");
        for (int i = 0; i < value; i++)
        {
            DataRow dtrow = dtData.NewRow();
            dtData.Rows.Add(dtrow);
            dtData.Rows[i]["SRNO"] = i + 1;

        }

        return dtData;
    }
}