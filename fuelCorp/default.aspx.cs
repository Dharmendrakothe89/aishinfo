using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class loginform : System.Web.UI.Page
{
    Handler hd = new Handler();
    protected void Page_Load(object sender, EventArgs e)
    {
       // if (!IsPostBack)
        {
            if (Request.Params.Get("__EVENTTARGET") == "userid" || Request.Params.Get("__EVENTTARGET") == "txtPassword")
            {
                btnlogin.Enabled = false;
                if (txtUserid.Text != string.Empty && txtPassword.Text != string.Empty)
                {
                    ddlcompany.Focus();

                    string sql = "select SRNO from usertable where userid= '" + txtUserid.Text.Trim().ToString() + "' and password= '" + txtPassword.Text.Trim().ToString() + "'";
                    DataTable dtuser = GetData(sql);
                    if (dtuser.Rows.Count > 0)
                    {
                        Session["userid"] = dtuser.Rows[0][0].ToString();
                        FillCompany();
                        txtPassword.Attributes["value"] = txtPassword.Text;
                        btnlogin.Enabled = true;
                    }
                    else
                    {
                        Session["userid"] = string.Empty;
                        MessageBox("Please Enter Valid Credential");
                    }
                }
                else if (txtUserid.Text != string.Empty)
                {
                    txtUserid.Text = "";
                    txtPassword.Text = "";
                    Session["userid"] = string.Empty;
                    MessageBox("Please Enter User ID");
                }
                else if (txtPassword.Text != string.Empty)
                {
                    txtUserid.Text = "";
                    txtPassword.Text = "";
                    Session["userid"] = string.Empty;
                    MessageBox("Please Enter Password");
                }
            }
        }
    }
    public void MessageBox(string msg)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "msg1", "alert('" + msg + "');", true);

    }
    public DataTable GetData(string query)
    {
        Handler hdn = new Handler();
        DataTable dt = hdn.GetTable(query);
        return dt;
    }
    private void FillCompany()
    {
        if (Session["userid"] != null && Session["userid"].ToString() != string.Empty)
        {
            Handler company = new Handler();
            string sqlcmp = "SELECT CM.CMPID,CM.CMPNAME FROM PERMISSIONMASTER PM INNER JOIN COMPANYMASTER CM ON CM.CMPID=PM.CMPID"+
                            " WHERE CM.STATUS=0 AND PM.STATUS=0 AND PM.USERID="+ Session["userid"].ToString()+" GROUP BY CM.CMPID,CM.CMPNAME";
            DataTable dtcompany = company.GetTable(sqlcmp);
            if (dtcompany.Rows.Count > 0)
            {
                ddlcompany.DataSource = dtcompany;
                ddlcompany.DataTextField = "CMPNAME";
                ddlcompany.DataValueField = "CMPID";
                ddlcompany.DataBind();
               
                ddlcompany.Enabled = true;
            }
        }
        else
        {
            ddlcompany.DataSource = null;
            ddlcompany.DataBind();
            ddlcompany.Items.Insert(0, new ListItem("--Select Company--", "--Select Company--"));
            ddlcompany.Enabled = false;
        }
    }
    protected void btnlogin_Click(object sender, EventArgs e)
    {
        string sql = "select SRNO ,designation from usertable where userid= '" + txtUserid.Text.Trim().ToString() + "' and password= '" + txtPassword.Text.Trim().ToString() + "'";
        DataTable dtlogin = hd.GetTable(sql);
        if (dtlogin.Rows.Count > 0)
        {

            Session["designation"] = dtlogin.Rows[0]["designation"].ToString();


            int login = General.Parse<int>(dtlogin.Rows[0][0].ToString());
            if (login != 0)
            {
                Session["userid"] = login;
                Handler hdn = new Handler();
                DataTable dt = hdn.GetTable("SELECT CMPID FROM PERMISSIONMASTER PM WHERE STATUS=0 AND PREFFERED=1 AND BRANCHID IS NULL AND PM.USERID=" + Session["userid"].ToString());
                Session["cmpid"] = dt.Rows[0][0].ToString();
                Handler hdn1 = new Handler();
                DataTable dt1 = hdn1.GetTable("SELECT PM.BRANCHID,BM.BRANCHNAME FROM PERMISSIONMASTER PM INNER JOIN BRANCHMASTER BM ON PM.BRANCHID=BM.BRANCHID WHERE PM.STATUS=0 AND PM.PREFFERED=1 AND PM.CMPID IS NULL AND PM.USERID=" + Session["userid"].ToString());
                Session["branchid"] = dt1.Rows[0][0].ToString();
                Session["branchname"] = dt1.Rows[0][1].ToString();
                Response.Redirect("dashboard.aspx");
            }
        }
    }
}