﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.Odbc;
using System.IO;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
       
        }
    }

    protected void btnsign_Click(object sender, EventArgs e)
    {
        Handler hd = new Handler();
        string sql = "select SRNO,NAME ,ROLE,RELATIONSHIPID from usertable where STATUS=0 and userid= '" + txtusername.Text.Trim().ToString() + "' and password= '" + txtpassword.Text.Trim().ToString() + "'";
        DataTable dtlogin = hd.GetTable(sql);
        if (dtlogin.Rows.Count > 0 && dtlogin.Rows[0]["SRNO"].ToString().Trim() != string.Empty)
        {
            Session["userid"] = dtlogin.Rows[0]["SRNO"].ToString();
            Session["username"] = dtlogin.Rows[0]["NAME"].ToString();
            Session["designation"] = dtlogin.Rows[0]["ROLE"].ToString();
            Session["relationshipid"] = dtlogin.Rows[0]["RELATIONSHIPID"].ToString();
            Response.Redirect("userdashboard.aspx");
        }
        else
        {
            MessageBox("Invalid Login Details");
        }
    }

    protected void btnforget_Click(object sender, EventArgs e)
    {
        if (txtforgetuserid.Text.Trim() != string.Empty || txtforgetemailid.Text.Trim() != string.Empty)
        {
            string body = string.Empty;
            string sql = "SELECT * FROM USERTABLE UT WHERE STATUS=0";
            if (txtforgetuserid.Text.Trim() != string.Empty)
            {
                sql += " AND USERID='" + txtforgetuserid.Text.Trim() + "'";
            }
            else if (txtforgetemailid.Text.Trim() != string.Empty)
            {
                sql += " AND USERID='" + txtforgetemailid.Text.Trim() + "'";
            }
            Handler hdn = new Handler();
            DataTable dt = hdn.GetTable(sql);
            if (dt.Rows.Count > 0)
            {
                body = "<html xmlns='http://www.w3.org/1999/xhtml'> <head runat='server'> <title>Registration</title> </head> " +
                " <body> <form id='form1' runat='server'> <div style='  border: 4px solid #537DA3; height:544px; width:600px; background-color:#fff; left:42%; top:0; margin-left:-200px; z-index:99999; border-radius:10px; position:fixed;'>" +
                " <img src='Images/logo.png' alt='' style='margin-left:3%; padding-top:5px;'  /> <div style='font-size:18px; font-family:Arial Balck; font-weight:bold; margin-left:22px; color:#93c220;'>Welcome To Marketing Project</div><br />" +
                " <p style='margin-left:24px; line-height:22px;'>Hi " + dt.Rows[0]["NAME"].ToString() + "   <br /> Welcome to<a href=''>Marketing Project</a><strong>Get Started!</strong> </p>" +
                " <strong style='margin-left:24px;'>Your Login Details are :</strong> <p style='margin-left:24px;'><strong>UserName : </strong> &nbsp;&nbsp;" + dt.Rows[0]["USERID"].ToString() + "<br />" +
                " <strong>Password :</strong>&nbsp;&nbsp;&nbsp;" + dt.Rows[0]["PASSWORD"].ToString() + " </p>" +
                " </form></body> </html>";

                string x = HttpContext.Current.Request.Url.ToString();
                string[] s = { "Default.aspx" };
                string[] spath = x.Split(s, StringSplitOptions.None);
                MessageBox("Check Your Mail For Account Details. Thank You");
                try
                {
                    General.SendMail("", "sumitrk2002@gmail.com", "9850386144k", "smtp.gmail.com", 587, "Login Recovery", body, txtforgetemailid.Text, "", "");
                }
                catch (Exception ex)
                {
                    MessageBox("Mail Not Send Try Again");
                }
            }
            else
            {
                MessageBox("Provide Proper Details");
            }

        }
        else
        {
            MessageBox("Please Provide User ID or Email ID");
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