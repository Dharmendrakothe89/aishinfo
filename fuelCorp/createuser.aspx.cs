using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Data;

public partial class createuser : System.Web.UI.Page
{
    Handler hd = new Handler();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("default.aspx");
            }
            FillPersonData();
            FillUser();
        }
    }
    private void FillUser()
    {
        string sql = "SELECT UM.SRNO,UM.NAME AS USERNAME,UM.DEPARTMENT,DESIGNATION,USERID,UM.PASSWORD,CASE WHEN STATUS=0 THEN 'ACTIVE' ELSE 'NON-WORKING' END AS STATUS FROM USERTABLE UM";
        Handler hdn = new Handler();
        DataTable dt = hdn.GetTable(sql);
        if (dt.Rows.Count > 0)
        {
            gvuserlist.DataSource = dt;
            gvuserlist.DataBind();
        }
        else
        {
            gvuserlist.DataSource = null;
            gvuserlist.DataBind();
        }
    }
    private void FillPersonData()
    {
        string sqldepartment = "SELECT RTRIM(LM.NAME) AS NAME,RTRIM(LM.SRNO) AS SRNO FROM LOOKUPMASTER LM INNER JOIN LOOKUPHEADINGMASTER LHM ON LHM.SRNO=LM.HEADID WHERE LM.STATUS=0 AND LHM.STATUS=0 AND LHM.HEAD='DEPARTMENT' ORDER BY LM.NAME";
        Handler hdndepartment = new Handler();
        DataTable dtdepartment = hdndepartment.GetTable(sqldepartment);
        ddldepartment.DataSource = dtdepartment;
        ddldepartment.DataTextField = "name";
        ddldepartment.DataValueField = "srno";
        ddldepartment.DataBind();

        ddleditdepartment.DataSource = dtdepartment;
        ddleditdepartment.DataTextField = "name";
        ddleditdepartment.DataValueField = "srno";
        ddleditdepartment.DataBind();
        

        string sqldesignation = "SELECT RTRIM(LM.NAME) AS NAME,RTRIM(LM.SRNO) AS SRNO FROM LOOKUPMASTER LM INNER JOIN LOOKUPHEADINGMASTER LHM ON LHM.SRNO=LM.HEADID WHERE LM.STATUS=0 AND LHM.STATUS=0 AND LHM.HEAD='DESIGNATION' ORDER BY LM.NAME";
        Handler hdndesignation = new Handler();
        DataTable dtdesignation = hdndesignation.GetTable(sqldesignation);
        ddldesignation.DataSource = dtdesignation;
        ddldesignation.DataTextField = "name";
        ddldesignation.DataValueField = "srno";
        ddldesignation.DataBind();
        
        ddleditdesignation.DataSource = dtdesignation;
        ddleditdesignation.DataTextField = "name";
        ddleditdesignation.DataValueField = "srno";
        ddleditdesignation.DataBind();
        
        

        string cmp = "SELECT CMPNAME,CMPID FROM COMPANYMASTER CM WHERE STATUS=0 ORDER BY CMPNAME";
        Handler hdncmp = new Handler();
        DataTable dtcmp = hdncmp.GetTable(cmp);
        chkcmplist.DataSource = dtcmp;
        chkcmplist.DataTextField = "CMPNAME";
        chkcmplist.DataValueField = "CMPID";
        chkcmplist.DataBind();

    }

    protected void chkcmplist_SelectedIndexChanged(object sender, EventArgs e)
    {
        string cmpname = string.Empty;
        foreach (ListItem aListItem in chkcmplist.Items)
        {
            if (aListItem.Selected)
            {
                if (cmpname == string.Empty)
                {
                    cmpname = aListItem.Value;
                }
                else
                {
                    cmpname += "," + aListItem.Value;
                }
            }
        }
        if (cmpname.Trim().ToString() != string.Empty)
        {
            chkbranchlist.Items.Clear();
            chkbranchlist.Enabled = true;
            string sql = "SELECT BRANCHNAME,BRANCHID FROM BRANCHMASTER BM WHERE STATUS=0 AND CMPID IN (" + cmpname + ")";
            Handler hdn = new Handler();
            DataTable dt = hdn.GetTable(sql);
            chkbranchlist.DataSource = dt;
            chkbranchlist.DataTextField = "BRANCHNAME";
            chkbranchlist.DataValueField = "BRANCHID";
            chkbranchlist.DataBind();
            chkcmplist.Items.Clear();
        }
        else
        {
            chkcmplist.Items.Clear();
            chkbranchlist.Items.Clear();
            chkbranchlist.DataSource = null;
            chkbranchlist.DataBind();
            chkbranchlist.Enabled = false;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int cmpcount = 0;
        int branchcount = 0;
        foreach (ListItem aListItem in chkcmplist.Items)
        {
            if (aListItem.Selected)
            {
                cmpcount = 1;
                foreach (ListItem aListItem1 in chkbranchlist.Items)
                {
                    if (aListItem1.Selected)
                    {
                        branchcount = 1;
                        break;
                    }
                }
                break;
            }
        }
        if (cmpcount > 0 && branchcount > 0)
        {
            usertable user = new usertable(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
            user.usertable_SRNO = -1;
            user.usertable_NAME = txtname.Text;
            user.usertable_DESIGNATION = ddldesignation.SelectedItem.Text.Trim().ToString();
            user.usertable_DEPARTMENT = ddldepartment.SelectedItem.Text.Trim().ToString();
            if (rdfemale.Checked)
            {
                user.usertable_GENDER = "FEMALE";
            }
            else
            {
                user.usertable_GENDER = "MALE";
            }
            user.usertable_EMAIL = txtemail.Text;
            user.usertable_PHONE = txtphone.Text;
            user.usertable_USERID = txtuserid.Text;
            user.usertable_PASSWORD = GenerateRandomCode();
            user.usertable_STATUS = 0;
            if (user.Insert(true, "usertable"))
            {
                int i = 0;
                foreach (ListItem aListItem in chkcmplist.Items)
                {
                    if (aListItem.Selected)
                    {
                        permissionmaster permission = new permissionmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                        permission.permissionmaster_SRNO = -1;
                        permission.permissionmaster_USERID = General.Parse<int>(Session["userid"].ToString().Trim());
                        permission.permissionmaster_CMPID = General.Parse<int>(aListItem.Value.ToString().Trim());
                        if (i == 0)
                        {
                            permission.permissionmaster_PREFFERED = 1;
                        }
                        else
                        {
                            permission.permissionmaster_PREFFERED = 0;
                        }
                        permission.permissionmaster_BRANCHID = -1;
                        permission.permissionmaster_STATUS = 0;
                        if (permission.Insert(true, "permissionmaster"))
                        {
                        }
                    }
                }
                i = 0;
                foreach (ListItem aListItem in chkbranchlist.Items)
                {
                    if (aListItem.Selected)
                    {
                        permissionmaster permission = new permissionmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                        permission.permissionmaster_SRNO = -1;
                        permission.permissionmaster_USERID = General.Parse<int>(Session["userid"].ToString().Trim());
                        permission.permissionmaster_BRANCHID = General.Parse<int>(aListItem.Value.ToString().Trim());
                        if (i == 0)
                        {
                            permission.permissionmaster_PREFFERED = 1;
                        }
                        else
                        {
                            permission.permissionmaster_PREFFERED = 0;
                        }
                        permission.permissionmaster_CMPID = -1;
                        permission.permissionmaster_STATUS = 0;
                        if (permission.Insert(true, "permissionmaster"))
                        {
                        }
                    }
                }
            }
            
            MessageBox("User Created Successfully");
            FillUser();
            
        }
    }
    #region Random Code Generation Logic
    public string GenerateRandomCode()
    {

        char[] chars = new char[62];
        chars =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
        byte[] data = new byte[1];
        RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
        crypto.GetNonZeroBytes(data);
        data = new byte[5];
        crypto.GetNonZeroBytes(data);
        System.Text.StringBuilder result = new System.Text.StringBuilder(5);
        foreach (byte b in data)
        {
            result.Append(chars[b % (chars.Length)]);
        }
        return result.ToString();
    }
    #endregion

    protected void lnkdetails_Click(object sender, EventArgs e)
    {

        LinkButton lnk = (LinkButton)sender;
        ViewState["userid"] = lnk.CommandArgument.ToString();
        string sql = "SELECT * FROM USERTABLE UT WHERE UT.SRNO=" + lnk.CommandArgument.Trim().ToString();
        Handler hdnstate = new Handler();
        DataTable dtstate = hdnstate.GetTable(sql);
        txteditname.Text = dtstate.Rows[0]["NAME"].ToString().Trim();
        txteditphone.Text = dtstate.Rows[0]["PHONE"].ToString().Trim();
        txteditemail.Text = dtstate.Rows[0]["EMAIL"].ToString().Trim();
        txtedituserid.Text = dtstate.Rows[0]["USERID"].ToString().Trim();
        txteditpassword.Text = dtstate.Rows[0]["PASSWORD"].ToString().Trim();
        ddleditdesignation.Items.FindByText(dtstate.Rows[0]["DESIGNATION"].ToString().Trim()).Selected = true;
        ddleditdepartment.Items.FindByText(dtstate.Rows[0]["DEPARTMENT"].ToString().Trim()).Selected = true;
        if (dtstate.Rows[0]["STATUS"].ToString().Trim() == "True")
        {
            ddlstatus.SelectedValue = "1";
        }
        else
        {
            ddlstatus.SelectedValue = "0";
        }
        if (dtstate.Rows[0]["GENDER"].ToString().Trim() == "MALE")
        {
            rdeditmale.Checked = true;
        }
        else
        {
            rdeditfemale.Checked = true;
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "edituser", "EditUser();", true);
    }
    protected void btnedit_Click(object sender, EventArgs e)
    {
        usertable user = new usertable(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
        user.usertable_SRNO = -1;
        user.usertable_NAME = txteditname.Text.Trim().ToString();
        user.usertable_DESIGNATION = ddleditdesignation.SelectedItem.Text.Trim().ToString();
        user.usertable_DEPARTMENT = ddleditdepartment.SelectedItem.Text.Trim().ToString();
        if (rdeditfemale.Checked)
        {
            user.usertable_GENDER = "FEMALE";
        }
        else
        {
            user.usertable_GENDER = "MALE";
        }

        user.usertable_EMAIL = txteditemail.Text.Trim().ToString();
        user.usertable_PHONE = txteditphone.Text.Trim().ToString();
        user.usertable_USERID = txtedituserid.Text.Trim().ToString();
        user.usertable_PASSWORD = txteditpassword.Text.Trim().ToString();
        user.usertable_STATUS = General.Parse<int>(ddlstatus.SelectedValue.Trim().ToString());
        string condition = "SRNO=" + ViewState["userid"].ToString();
        if (user.Insert(false, "usertable", condition))
        {
            MessageBox("User Updated Successfully");
            FillUser();
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "viewuser", "ShowUser();", true);
    }
    public void MessageBox(string msg)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmasg", "alert('" + msg + "');", true);
        }
        catch
        {
            throw;
        }
    }
}