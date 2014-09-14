using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;
using System.Security.Cryptography;
public partial class superadminmemberlist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dtcontract = FillMemberList();
            ViewState["list"] = dtcontract;
            if (dtcontract.Rows.Count > 0)
            {
                gvmemberlist.DataSource = (DataTable)ViewState["list"];
                gvmemberlist.DataBind();
            }
            else
            {
                gvmemberlist.DataSource = null;
                gvmemberlist.DataBind();
            }
        }
    }
    private DataTable FillMemberList()
    {
        string sqlpartylist = "SELECT RT.SRNO,RT.NAME,RT.SPONSORNAME,RT.SPONSORSEMICODE,RT.SEMICODE,RT.EMAILID,RT.PHONENO," +
                               " CASE WHEN RT.STATUS=0 THEN 'ACTIVE' ELSE 'DE-ACTIVE' END AS STATUS FROM REGISTRATIONTABLE RT WHERE STATUS=1";
        Handler hdnpartylist = new Handler();
        DataTable dtpartylist = hdnpartylist.GetTable(sqlpartylist);
        return dtpartylist;
    }
    protected void gvlookup_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvmemberlist.PageIndex = e.NewPageIndex;
        gvmemberlist.DataSource = (DataTable)ViewState["list"];
        gvmemberlist.DataBind();
    }

    protected void lnkdetails_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        ViewState["registrationid"] = lnk.CommandArgument.ToString();
        string sqlpartylist = "SELECT RT.NAME,RT.EMAILID FROM REGISTRATIONTABLE RT WHERE STATUS=1 AND SRNO=" + ViewState["registrationid"].ToString();
        Handler hdnpartylist = new Handler();
        DataTable dtpartylist = hdnpartylist.GetTable(sqlpartylist);
        if (dtpartylist.Rows.Count > 0)
        {
            txtmemname.Text = dtpartylist.Rows[0]["NAME"].ToString();
            txtmememail.Text = dtpartylist.Rows[0]["EMAILID"].ToString();
        }
        else
        {
            txtmemname.Text = string.Empty;
            txtmememail.Text = string.Empty;
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (txtpassword.Text.Trim() != string.Empty)
        {
            registrationtable obj = new registrationtable(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
            obj.registrationtable_SRNO = -1;
            obj.registrationtable_SPONSORID = -1;
            obj.registrationtable_STATUS = 2;
            string condition = "SRNO=" + ViewState["registrationid"].ToString();
            if (obj.Insert(false, "registrationtable", condition))
            {
                usertable obj1 = new usertable(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                obj1.usertable_SRNO = -1;
                obj1.usertable_STATUS = 2;
                obj1.usertable_NAME = txtmemname.Text;
                obj1.usertable_USERID = txtmememail.Text;
                obj1.usertable_PASSWORD = txtpassword.Text;
                obj1.usertable_ROLE = "USER";
                obj1.usertable_RELATIONSHIPID = General.Parse<int>(ViewState["registrationid"].ToString());
                if (obj1.Insert(true, "usertable"))
                {
                    string encryptedid = EncryptionMethod(txtmememail.Text);
                    string encryptedpassword = EncryptionMethod(txtpassword.Text);
                    string x = HttpContext.Current.Request.Url.ToString();
                    string[] s = { "superadminmemberlist.aspx" };
                    string[] spath = x.Split(s, StringSplitOptions.None);
                    string link = "http://crmproject.aishinfotech.net/activatelink.aspx?id=" + txtmememail.Text.Trim().ToLower() + "&psd=" + txtpassword.Text.Trim();
                    MessageBox("Mail Sent To User For Activation");
                    string body = "<html xmlns='http://www.w3.org/1999/xhtml'> <head runat='server'> <title>Registration</title> </head> " +
                        " <body> <form id='form1' runat='server'> <div style='  border: 4px solid #537DA3; height:544px; width:600px; background-color:#fff; left:42%; top:0; margin-left:-200px; z-index:99999; border-radius:10px; position:fixed;'>" +
                        " <img src='Images/logo.png' alt='' style='margin-left:3%; padding-top:5px;'  /> <div style='font-size:18px; font-family:Arial Balck; font-weight:bold; margin-left:22px; color:#93c220;'>Welcome To Marketing Project</div><br />" +
                        " <p style='margin-left:24px; line-height:22px;'>Hi " + txtmemname.Text + "   <br /> Welcome to<a href=''>Marketing Project</a><strong>Get Started!</strong> </p>" +
                        " <strong style='margin-left:24px;'>Your Login Details are :</strong> <p style='margin-left:24px;'><strong>UserName : </strong> &nbsp;&nbsp;" + txtmememail.Text.ToLower() + "<br />" +
                        " <strong>Password :</strong>&nbsp;&nbsp;&nbsp;" + txtpassword.Text + " </p> <p style='color:#b9b7b7; font-size:13px;margin-left:24px;'> If you forget your username/password in future, you can use the <a href='http://crmproject.aishinfotech.net/register.aspx'>forgot your password</a> link on<br /> <a href='http://crmproject.aishinfotech.net'> Marketing Project </a></p>" +
                        " <p style='margin-left:24px;'><strong>Activate Account</strong></p> <p style='color:#b9b7b7; font-size:13px;margin-left:24px; line-height:0px;'> Please visit below url </p><p style='margin-left:24px;line-height:17px;'><br/><a href='" + link + "'>" + link + "</a><br/></p>" +
                        " </form></body> </html>";
                    try
                    {
                        General.SendMail("", "sumitrk2002@gmail.com", "9850386144k", "smtp.gmail.com", 587, "Registration", body, txtmememail.Text, "", "");
                        //General.SendMail("", "info.aishinfotech@gmail.com", "aish@123", "smtp.gmail.com", 587, "registration", body, txtmememail.Text, "", "");
                    }
                    catch (Exception ex)
                    {
                    }

                    
                }
            }
        }
        else
        {
            MessageBox("Please Generate Password");
        }
    }
    protected void lnkpassword_Click(object sender, EventArgs e)
    {
        string password = GeneratePassword();
        txtpassword.Text = password;
    }
    public string GeneratePassword()
    {

        char[] chars = new char[62];
        chars =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
        byte[] data = new byte[1];
        RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
        crypto.GetNonZeroBytes(data);
        data = new byte[8];
        crypto.GetNonZeroBytes(data);
        System.Text.StringBuilder result = new System.Text.StringBuilder(8);
        foreach (byte b in data)
        {
            result.Append(chars[b % (chars.Length)]);
        }
        return result.ToString();
    }
    public string EncryptionMethod(string role)
    {
        RijndaelManaged RijndaelCipher = new RijndaelManaged();
        string encryptionkey = "delicate";
        byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(role);
        byte[] Salt = System.Text.Encoding.ASCII.GetBytes(encryptionkey.Length.ToString());
        PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(encryptionkey, Salt);
        ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
        System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
        CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);
        cryptoStream.Write(PlainText, 0, PlainText.Length);
        cryptoStream.FlushFinalBlock();
        byte[] CipherBytes = memoryStream.ToArray();
        memoryStream.Close();
        cryptoStream.Close();
        string EncryptedData = Convert.ToBase64String(CipherBytes);
        return EncryptedData;
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