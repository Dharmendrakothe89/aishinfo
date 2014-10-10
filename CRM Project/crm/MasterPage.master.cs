using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            lnklogin.Visible = false;
            lnklogout.Visible = true;
            lblwelcome.Visible = true;
            lbluser.Visible = true;
            lnkregister.Visible = true;
            lnkdownline.Visible = true;
            lnkdashboard.Visible = true;
            lnkupload.Visible = true;
            lnksearch.Visible = true;
            lnkedit.Visible = true;
            
            lbluser.Text = Session["username"].ToString();
        }
        else
        {
            lnklogin.Visible = true;
            lnklogout.Visible = false;
            lblwelcome.Visible = false;
            lnkregister.Visible = false;
            lnkdownline.Visible = false;
            lnkdashboard.Visible = false;
            lnkupload.Visible = false;
            lnksearch.Visible = false;
            lnkedit.Visible = false;

            lbluser.Visible = false;
        }
    }
    protected void lnkregister_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        if (lnk.CommandName == "DASHBOARD")
        {
            Response.Redirect("userdashboard.aspx");
        }
        else if (lnk.CommandName == "UPLOAD")
        {
            Response.Redirect("uploadexcel.aspx");
        }
        else if (lnk.CommandName == "SEARCH")
        {
            Response.Redirect("searchrecord.aspx");
        }
        else if (lnk.CommandName == "EDIT")
        {
            Response.Redirect("editprofile.aspx");
        }
        else if (lnk.CommandName == "REGISTER")
        {
            Response.Redirect("register.aspx");
        }
        else if (lnk.CommandName == "DOWNLINE")
        {
            Response.Redirect("downline.aspx");
        }

    }
    protected void lnklogout_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        if (lnk.CommandName == "LOGOUT")
        {
            Session.Remove("userid");
            Session.Remove("username");
            Session.Remove("designation");
            lnklogin.Visible = true;
            lnklogout.Visible = false;
            lnkdashboard.Visible = false;
            lnkupload.Visible = false;
            lnksearch.Visible = false;
            lnkregister.Visible = false;
            lnkedit.Visible = false;
            Response.Redirect("login.aspx");

        }
        else if (lnk.CommandName == "LOGIN")
        {
            lnklogin.Visible = false;
            lnklogout.Visible = true;

            Response.Redirect("login.aspx");
        }
    }
}
