using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Master : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["username"] != null)
            {
                lblname.Text = Session["username"].ToString();
            }
        }
    }

    protected void lnklogout_Click(object sender, EventArgs e)
    {
        Session.Remove("designation");
        Session.Remove("userid");
        Session.Remove("username");
        Response.Redirect("Default.aspx");
    }
}
