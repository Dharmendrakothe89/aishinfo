using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class depotdetails : System.Web.UI.Page
{
    string editbtn = "1";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("default.aspx");
            }
            ViewState["postback"] = "1";
            if (Request.QueryString["DEPOTID"] != null)
            {
                FillPersonData();
                ViewState["DEPOTID"] = Request.QueryString["DEPOTID"].ToString();
                FillData(ViewState["DEPOTID"].ToString().Trim());
                FillState();
                
                DisableControl();
            }
        }
        editbtn = ViewState["postback"].ToString();
    }
    private void FillData(string depotid)
    {
        string sql = "SELECT * FROM GODOWNMASTER GM WHERE GM.SRNO=" + depotid;
        Handler hdn = new Handler();
        DataTable dt = hdn.GetTable(sql);
        if (dt.Rows.Count > 0)
        {
            FillState();
            txtgodownname.Text = dt.Rows[0]["GODOWNNAME"].ToString().Trim();
            txtphone.Text = dt.Rows[0]["PHONE"].ToString().Trim();
            txtfax.Text = dt.Rows[0]["FAX"].ToString().Trim();
            txtemail.Text = dt.Rows[0]["EMAIL"].ToString().Trim();
            txtaddress.Text = dt.Rows[0]["ADDRESS"].ToString().Trim();
            txtpin.Text = dt.Rows[0]["PIN"].ToString().Trim();
            ddlstate.SelectedValue = dt.Rows[0]["STATEID"].ToString().Trim();
            DataTable dtcity= FillCity(dt.Rows[0]["STATEID"].ToString().Trim());
            ddlcity.SelectedValue = dt.Rows[0]["CITYID"].ToString().Trim();
            ddlcity.DataSource = dtcity;
            ddlcity.DataTextField = "CITYNAME";
            ddlcity.DataValueField = "CITYID";
            ddlcity.DataBind();
            ddlcity.Items.Insert(0, "--City--");
            ddlcity.SelectedValue = dt.Rows[0]["CITYID"].ToString().Trim();
            if (dt.Rows[0]["STATUS"].ToString().Trim() == "True")
            {
                ddlstatus.SelectedValue = "1";
            }
            else
            {
                ddlstatus.SelectedValue = "0";
            }
            string sqlperson = "SELECT * FROM PERSONALMASTER PM WHERE PM.PERSONTYPE='GODOWN' AND PM.PERSONRELATIONID=" + depotid;
            Handler hdnperson = new Handler();
            DataTable dtperson = hdnperson.GetTable(sqlperson);
            ViewState["personrecord"] = dtperson;
            memberrepeater.DataSource = dtperson;
            memberrepeater.DataBind();
        }
    }
    private void DisableControl()
    {
        txtgodownname.Enabled = false;
        txtphone.Enabled = false;
        txtfax.Enabled = false;
        txtemail.Enabled = false;
        txtaddress.Enabled = false;
        ddlstate.Enabled = false;
        ddlcity.Enabled = false;
        txtpin.Enabled = false;

    }
    private void FillState()
    {
        string sqlstate = "SELECT STATENAME,STATEID FROM STATEMASTER SM WHERE SM.STATUS=0 ORDER BY STATENAME";
        Handler hdnstate = new Handler();
        DataTable dtstate = hdnstate.GetTable(sqlstate);

        ddlstate.DataSource = dtstate;
        ddlstate.DataTextField = "STATENAME";
        ddlstate.DataValueField = "STATEID";

        ddlstate.DataBind();
        ddlstate.Items.Insert(0, "--State--");

    }
    private DataTable FillCity(string stateid)
    {
        string sqlcity = "SELECT CITYNAME,CITYID FROM CITYMASTER CM WHERE CM.STATUS=0 AND CM.STATEID=" + stateid + " ORDER BY CM.CITYNAME";
        Handler hdncity = new Handler();
        DataTable dtcity = hdncity.GetTable(sqlcity);
        return dtcity;
    }
    private void FillPersonData()
    {
        string sqldepartment = "SELECT RTRIM(LM.NAME) AS NAME,RTRIM(LM.SRNO) AS SRNO FROM LOOKUPMASTER LM INNER JOIN LOOKUPHEADINGMASTER LHM ON LHM.SRNO=LM.HEADID WHERE LM.STATUS=0 AND LHM.STATUS=0 AND LHM.HEAD='DEPARTMENT' ORDER BY LM.NAME";
        Handler hdndepartment = new Handler();
        DataTable dtdepartment = hdndepartment.GetTable(sqldepartment);
        ViewState["department"] = dtdepartment;

        string sqldesignation = "SELECT RTRIM(LM.NAME) AS NAME,RTRIM(LM.SRNO) AS SRNO FROM LOOKUPMASTER LM INNER JOIN LOOKUPHEADINGMASTER LHM ON LHM.SRNO=LM.HEADID WHERE LM.STATUS=0 AND LHM.STATUS=0 AND LHM.HEAD='DESIGNATION' ORDER BY LM.NAME";
        Handler hdndesignation = new Handler();
        DataTable dtdesignation = hdndesignation.GetTable(sqldesignation);
        ViewState["designation"] = dtdesignation;


    }
    protected void memberrepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DropDownList ddldepartment = (DropDownList)e.Item.FindControl("ddldepartment");
            DropDownList ddldesignation = (DropDownList)e.Item.FindControl("ddldesignation");
            TextBox personname = (TextBox)e.Item.FindControl("txtpersonname");
            TextBox email = (TextBox)e.Item.FindControl("txtperemailid");
            TextBox txtperphone = (TextBox)e.Item.FindControl("txtperphone");
            TextBox txtpermobile = (TextBox)e.Item.FindControl("txtpermobile");
            ddldepartment.DataSource = (DataTable)ViewState["department"];
            ddldepartment.DataTextField = "NAME";
            ddldepartment.DataValueField = "NAME";
            ddldepartment.DataBind();
            string department = ((System.Data.DataRowView)(e.Item.DataItem)).Row.ItemArray[2].ToString().Trim();
            ddldepartment.SelectedValue = department;


            ddldesignation.DataSource = (DataTable)ViewState["designation"];
            ddldesignation.DataTextField = "NAME";
            ddldesignation.DataValueField = "NAME";
            ddldesignation.DataBind();
            string designation = ((System.Data.DataRowView)(e.Item.DataItem)).Row.ItemArray[2].ToString().Trim();
            ddldesignation.SelectedValue = designation;
            if (editbtn == "UPDATE")
            {
                ddldepartment.Enabled = true;
                ddldesignation.Enabled = true;
                personname.Enabled = true;
                email.Enabled = true;
                txtperphone.Enabled = true;
                txtpermobile.Enabled = true;
            }
            else
            {
                ddldepartment.Enabled = false;
                ddldesignation.Enabled = false;
                personname.Enabled = false;
                email.Enabled = false;
                txtperphone.Enabled = false;
                txtpermobile.Enabled = false;
            }
            

            

        }
    }
    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlstate.SelectedIndex > 0)
        {
            DataTable dtcity = FillCity(ddlstate.SelectedValue.ToString());
            ddlcity.Enabled = true;
            ddlcity.DataSource = dtcity;
            ddlcity.DataTextField = "CITYNAME";
            ddlcity.DataValueField = "CITYID";
            ddlcity.DataBind();
            ddlcity.Items.Insert(0, "--City--");
        }
        else
        {
            ddlcity.SelectedIndex = 0;
            ddlcity.Enabled = false;
        }
    }
    protected void btnedit_Click(object sender, EventArgs e)
    {
        ViewState["postback"] = "UPDATE";
        editbtn = ViewState["postback"].ToString();
        txtgodownname.Enabled = true;
        txtphone.Enabled = true;
        txtfax.Enabled = true;
        txtemail.Enabled = true;
        txtaddress.Enabled = true;
        ddlstate.Enabled = true;
        ddlcity.Enabled = true;
        txtpin.Enabled = true;
        ddlstatus.Enabled = true;
        btnupdate.Visible = true;
        traddmore.Style.Add("display", "block");
        memberrepeater.DataSource = (DataTable)ViewState["personrecord"];
        memberrepeater.DataBind();
    }
    protected void lnkaddmember_Click(object sender, EventArgs e)
    {
        DataTable dt1 = GetFillPreviousData();

        ViewState["member"] = dt1;

        DataTable dt = GetFillDataTable(1);
        if (ViewState["member"] != null)
        {
            ((DataTable)ViewState["member"]).Merge(dt);

        }
        else
        {
            ViewState["member"] = dt;
        }
        memberrepeater.DataSource = (DataTable)ViewState["member"];
        memberrepeater.DataBind();
    }
    private DataTable GetFillPreviousData()
    {
        DataTable dtData = new DataTable();
        dtData.Columns.Add("SRNO");
        dtData.Columns.Add("PERSONNAME");
        dtData.Columns.Add("DESIGNATION");
        dtData.Columns.Add("DEPARTMENT");
        dtData.Columns.Add("EMIALID");
        dtData.Columns.Add("PHONE");
        dtData.Columns.Add("MOBILE");

        for (int i = 0; i < memberrepeater.Items.Count; i++)
        {

            TextBox name = (TextBox)memberrepeater.Items[i].FindControl("txtpersonname");
            TextBox phone = (TextBox)memberrepeater.Items[i].FindControl("txtperphone");
            TextBox email = (TextBox)memberrepeater.Items[i].FindControl("txtperemailid");
            TextBox mobile = (TextBox)memberrepeater.Items[i].FindControl("txtpermobile");
            DropDownList ddldepartment = (DropDownList)memberrepeater.Items[i].FindControl("ddldepartment");
            DropDownList ddldesignation = (DropDownList)memberrepeater.Items[i].FindControl("ddldesignation");

            int count = dtData.Rows.Count;
            dtData.Rows.Add(1);
            dtData.Rows[count]["SRNO"] = (count + 1).ToString();
            dtData.Rows[count]["PERSONNAME"] = name.Text.ToString();

            dtData.Rows[count]["DEPARTMENT"] = ddldepartment.SelectedValue.Trim().ToString();
            dtData.Rows[count]["DESIGNATION"] = ddldesignation.SelectedValue.Trim().ToString();

            dtData.Rows[count]["EMIALID"] = email.Text.ToString();
            dtData.Rows[count]["PHONE"] = phone.Text.ToString();
            dtData.Rows[count]["MOBILE"] = mobile.Text.ToString();

        }

        return dtData;
    }
    private DataTable GetFillDataTable(int value)
    {
        DataTable dtData = new DataTable();
        dtData.Columns.Add("SRNO");
        dtData.Columns.Add("PERSONNAME");
        dtData.Columns.Add("DESIGNATION");
        dtData.Columns.Add("DEPARTMENT");
        dtData.Columns.Add("EMAILID");
        dtData.Columns.Add("PHONENO");
        dtData.Columns.Add("MOBILE");
        for (int i = 0; i < value; i++)
        {
            DataRow dtrow = dtData.NewRow();
            dtData.Rows.Add(dtrow);
            dtData.Rows[i]["SRNO"] = i + 1;

        }

        return dtData;
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        godownmaster objgodown = new godownmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
        objgodown.godownmaster_SRNO = -1;
        objgodown.godownmaster_GODOWNNAME = txtgodownname.Text.ToString().Trim();
        objgodown.godownmaster_STATEID = General.Parse<int>(ddlstate.SelectedValue.ToString());
        objgodown.godownmaster_STATENAME = ddlstate.SelectedItem.Text.Trim().ToString();
        objgodown.godownmaster_CITYID = General.Parse<int>(ddlcity.SelectedValue.ToString());
        objgodown.godownmaster_CITYNAME = ddlcity.SelectedItem.Text.Trim().ToString();
        objgodown.godownmaster_PHONE = txtphone.Text.ToString().Trim();
        objgodown.godownmaster_FAX = txtfax.Text.ToString().Trim();
        objgodown.godownmaster_EMAIL = txtemail.Text.ToString().Trim();
        objgodown.godownmaster_ADDRESS = txtaddress.Text.ToString().Trim();
        objgodown.godownmaster_PIN = txtpin.Text.ToString().Trim();
        objgodown.godownmaster_STATUS = General.Parse<int>(ddlstatus.SelectedValue.Trim().ToString());
        string condition = "SRNO=" + ViewState["DEPOTID"].ToString().Trim();
        if (objgodown.Insert(false, "godownmaster", condition))
        {
            if (memberrepeater.Items.Count > 0)
            {
                int membercount = ((DataTable)ViewState["personrecord"]).Rows.Count;
                for (int i = 0; i < memberrepeater.Items.Count; i++)
                {

                    TextBox name = (TextBox)memberrepeater.Items[i].FindControl("txtpersonname");
                    DropDownList department = (DropDownList)memberrepeater.Items[i].FindControl("ddldepartment");
                    DropDownList designation = (DropDownList)memberrepeater.Items[i].FindControl("ddldesignation");
                    TextBox phone = (TextBox)memberrepeater.Items[i].FindControl("txtperphone");
                    TextBox email = (TextBox)memberrepeater.Items[i].FindControl("txtperemailid");
                    TextBox mobile = (TextBox)memberrepeater.Items[i].FindControl("txtpermobile");
                    if (i < membercount)
                    {
                        personalmaster objpersonal = new personalmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                        objpersonal.personalmaster_SRNO = -1;
                        objpersonal.personalmaster_PERSONNAME = name.Text.ToString().Trim();
                        objpersonal.personalmaster_DEPARTMENT = department.SelectedItem.Text.Trim().ToString();
                        objpersonal.personalmaster_DESIGNATION = designation.SelectedItem.Text.Trim().ToString();
                        objpersonal.personalmaster_EMAILID = email.Text.ToString().Trim();
                        objpersonal.personalmaster_PHONENO = phone.Text.ToString().Trim();
                        objpersonal.personalmaster_MOBILE = mobile.Text.ToString().Trim();
                        objpersonal.personalmaster_PERSONRELATIONID = -1;
                        string condition1 = "SRNO=" + ViewState["DEPOTID"].ToString().Trim();
                        if (objpersonal.Insert(false, "personalmaster", condition1))
                        {
                        }
                    }
                    else
                    {
                        personalmaster objpersonal = new personalmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                        objpersonal.personalmaster_SRNO = -1;
                        objpersonal.personalmaster_PERSONNAME = name.Text.ToString().Trim();
                        objpersonal.personalmaster_DEPARTMENT = department.SelectedItem.Text.Trim().ToString();
                        objpersonal.personalmaster_DESIGNATION = designation.SelectedItem.Text.Trim().ToString();
                        objpersonal.personalmaster_EMAILID = email.Text.ToString().Trim();
                        objpersonal.personalmaster_PHONENO = phone.Text.ToString().Trim();
                        objpersonal.personalmaster_MOBILE = mobile.Text.ToString().Trim();
                        objpersonal.personalmaster_PERSONRELATIONID = General.Parse<int>(ViewState["DEPOTID"].ToString().Trim());
                        objpersonal.personalmaster_PERSONTYPE = "GODOWN";
                        if (objpersonal.Insert(true, "personalmaster"))
                        {
                        }
                    }

                }
                Response.Redirect("godownlist.aspx");
            }
        }
    }
}