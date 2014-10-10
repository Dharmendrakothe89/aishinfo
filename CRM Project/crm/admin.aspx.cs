using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;
using System.Security.Cryptography;

public partial class admin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnlogin_Click(object sender, EventArgs e)
    {
        Handler hd = new Handler();
        string sql = "select SRNO,NAME ,ROLE from usertable where STATUS=0 AND (ROLE='ADMIN' OR ROLE='SUPERADMIN') and userid= '" + txtemail.Text.Trim().ToString() + "' and password= '" + txtpassword.Text.Trim().ToString() + "'";
        DataTable dtlogin = hd.GetTable(sql);
        if (dtlogin.Rows.Count > 0 && dtlogin.Rows[0]["SRNO"].ToString().Trim() != string.Empty)
        {
            Session["userid"] = dtlogin.Rows[0]["SRNO"].ToString();
            Session["username"] = dtlogin.Rows[0]["NAME"].ToString();
            Session["designation"] = dtlogin.Rows[0]["ROLE"].ToString();
            Response.Redirect("dashboard.aspx");
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