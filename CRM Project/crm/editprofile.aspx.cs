using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class editprofile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("register.aspx?login=1");
            }
            else
            {
                FillData(Session["userid"].ToString());
            }

        }
    }
    private void FillData(string id)
    {
        string sql = "SELECT RT.NAME,RT.SPONSORSEMICODE,RT.SPONSORNAME,RT.SEMICODE,RT.EMAILID,RT.PHONENO" +
                   " FROM USERTABLE UT INNER JOIN REGISTRATIONTABLE RT ON UT.RELATIONSHIPID=RT.SRNO" +
                   " WHERE UT.STATUS=0 AND UT.SRNO=" + id;
        Handler hdn = new Handler();
        DataTable dt = hdn.GetTable(sql);
        if (dt.Rows.Count > 0)
        {
            txtname.Text = dt.Rows[0]["NAME"].ToString();
            txtsponsorid.Text = dt.Rows[0]["SPONSORSEMICODE"].ToString();
            txtsponsorname.Text = dt.Rows[0]["SPONSORNAME"].ToString();
            txtsemid.Text = dt.Rows[0]["SEMICODE"].ToString();
            txtemail.Text = dt.Rows[0]["EMAILID"].ToString();
            txtmobileno.Text = dt.Rows[0]["PHONENO"].ToString();
        }
    }
    protected void btnaccount_Click(object sender, EventArgs e)
    {
        string sponsorid = string.Empty;
        Handler hdn = new Handler();
        DataTable dt = hdn.GetTable("SELECT SRNO,NAME FROM REGISTRATIONTABLE RT WHERE SPONSORSEMICODE='" + txtsponsorid.Text.Trim() + "'");
        if (dt.Rows.Count > 0 && dt.Rows[0]["SRNO"].ToString() != string.Empty)
        {
            registrationtable obj = new registrationtable(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
            obj.registrationtable_SRNO = -1;
            obj.registrationtable_NAME = txtname.Text.Trim();
            obj.registrationtable_SEMICODE = txtsemid.Text.Trim();
            obj.registrationtable_PHONENO = txtmobileno.Text.Trim();
            obj.registrationtable_EMAILID = txtemail.Text.Trim();
            if (dt.Rows.Count > 0 && dt.Rows[0]["SRNO"].ToString() != string.Empty)
            {
                obj.registrationtable_SPONSORID = General.Parse<int>(dt.Rows[0]["SRNO"].ToString());
                obj.registrationtable_SPONSORNAME = dt.Rows[0]["NAME"].ToString();
                obj.registrationtable_SPONSORSEMICODE = txtsponsorid.Text.Trim();
            }
            else
            {
                obj.registrationtable_SPONSORID = -1;
            }
            
            obj.registrationtable_STATUS = -1;
            string condition = "SRNO=" + Session["userid"].ToString();
            if (obj.Insert(false, "registrationtable", condition))
            {
                MessageBox("Profile Updated Successfully");
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