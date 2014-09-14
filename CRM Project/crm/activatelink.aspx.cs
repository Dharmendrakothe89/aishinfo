using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Data;
public partial class activatelink : System.Web.UI.Page
{
    string ID, password;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        string id = Request.QueryString["id"];
        string psw = Request.QueryString["psd"];
        NameValueCollection n = Request.QueryString;
        if (id != null)
        {
            lblactivate.Visible = true;
            lblnotactivate.Visible = false;
            // btnactivate.Text = n.GetKey(0);
            ID = id;

            password = psw;

            updateLogin();
        }
        else
        {
            lblactivate.Visible = false;
            lblnotactivate.Visible = true;
        }
    }
    protected void updateLogin()
    {
        string sql = "SELECT RELATIONSHIPID FROM USERTABLE UT WHERE UT.USERID='" + ID + "' AND PASSWORD='" + password + "' AND STATUS=2";
        Handler hdn = new Handler();
        DataTable dt = hdn.GetTable(sql);
        if (dt.Rows.Count > 0)
        {
            usertable obj1 = new usertable(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
            obj1.usertable_SRNO = -1;
            obj1.usertable_RELATIONSHIPID = -1;
            obj1.usertable_STATUS = 0;
            string condition = "USERID = '" + ID + "' AND PASSWORD = '" + password + "'";
            if (obj1.Insert(false, "usertable", condition))
            {
            }

            registrationtable obj = new registrationtable(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
            obj.registrationtable_SRNO = -1;
            obj.registrationtable_SPONSORID = -1;
            obj.registrationtable_STATUS = 0;
            string condition1 = "SRNO =" + dt.Rows[0][0].ToString(); ;
            if (obj.Insert(false, "registrationtable", condition1))
            {
            }
        }

    }
    protected void btnsign_Click(object sender, EventArgs e)
    {
        Handler hd = new Handler();
        string sql = "select SRNO,NAME ,ROLE from usertable where STATUS=0 and userid= '" + txtusername.Text.Trim().ToString() + "' and password= '" + txtpassword.Text.Trim().ToString() + "'";
        DataTable dtlogin = hd.GetTable(sql);
        if (dtlogin.Rows.Count > 0 && dtlogin.Rows[0]["SRNO"].ToString().Trim() != string.Empty)
        {
            Session["userid"] = dtlogin.Rows[0]["SRNO"].ToString();
            Session["designation"] = dtlogin.Rows[0]["ROLE"].ToString();
            Session["username"] = dtlogin.Rows[0]["NAME"].ToString();
            MessageBox("Login Successfully");
            Response.Redirect("userdashboard.aspx");
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
    #region Decryption

    protected string Decryptionmethod(string TextToBeDecrypted)
    {


        string DecryptedData;
        TextToBeDecrypted = TextToBeDecrypted.Replace("%", "&");
        System.Security.Cryptography.RijndaelManaged RijndaelCipher = new RijndaelManaged();
        string Password = "delicate";
        byte[] EncryptedData = Convert.FromBase64String(TextToBeDecrypted);
        byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());
        PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);
        ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
        MemoryStream memoryStream = new MemoryStream(EncryptedData);
        CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);
        byte[] PlainText = new byte[EncryptedData.Length];
        int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);
        memoryStream.Close();
        cryptoStream.Close();
        DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);
        return DecryptedData;
        //rolehidden.Value = DecryptedData;
        //permissionhidden.Value = Session["access"].ToString();


    }


    #endregion
}