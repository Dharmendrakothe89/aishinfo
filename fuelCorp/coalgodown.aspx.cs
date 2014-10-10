using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class coalgodown : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("default.aspx");
            }
            FillPersonData();
            DataTable dt = GetFillDataTable(1);
            ViewState["member"] = dt;
            memberrepeater.DataSource = (DataTable)ViewState["member"];
            memberrepeater.DataBind();
            FillState();

        }
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
        DataTable dtcity = FillCity(dtstate.Rows[0]["STATEID"].ToString().Trim());

        ddlcity.DataSource = dtcity;
        ddlcity.DataTextField = "CITYNAME";
        ddlcity.DataValueField = "CITYID";
        ddlcity.DataBind();

    }
    protected void memberrepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DropDownList ddldepartment = (DropDownList)e.Item.FindControl("ddldepartment");
            DropDownList ddldesignation = (DropDownList)e.Item.FindControl("ddldesignation");

            ddldepartment.DataSource = (DataTable)ViewState["department"];
            ddldepartment.DataTextField = "NAME";
            ddldepartment.DataValueField = "SRNO";
            ddldepartment.DataBind();



            ddldesignation.DataSource = (DataTable)ViewState["designation"];
            ddldesignation.DataTextField = "NAME";
            ddldesignation.DataValueField = "SRNO";
            ddldesignation.DataBind();


        }
    }

    private void FillPersonData()
    {
        string sqldepartment = "SELECT LM.NAME,LM.SRNO FROM LOOKUPMASTER LM INNER JOIN LOOKUPHEADINGMASTER LHM ON LHM.SRNO=LM.HEADID WHERE LM.STATUS=0 AND LHM.STATUS=0 AND LHM.HEAD='DEPARTMENT' ORDER BY LM.NAME";
        Handler hdndepartment = new Handler();
        DataTable dtdepartment = hdndepartment.GetTable(sqldepartment);
        ViewState["department"] = dtdepartment;

        string sqldesignation = "SELECT LM.NAME,LM.SRNO FROM LOOKUPMASTER LM INNER JOIN LOOKUPHEADINGMASTER LHM ON LHM.SRNO=LM.HEADID WHERE LM.STATUS=0 AND LHM.STATUS=0 AND LHM.HEAD='DESIGNATION' ORDER BY LM.NAME";
        Handler hdndesignation = new Handler();
        DataTable dtdesignation = hdndesignation.GetTable(sqldesignation);
        ViewState["designation"] = dtdesignation;


    }
    private DataTable FillCity(string stateid)
    {
        string sqlcity = "SELECT CITYNAME,CITYID FROM CITYMASTER CM WHERE CM.STATUS=0 AND CM.STATEID=" + stateid + " ORDER BY CM.CITYNAME";
        Handler hdncity = new Handler();
        DataTable dtcity = hdncity.GetTable(sqlcity);
        return dtcity;
    }
    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {
            DataTable dtcity = FillCity(ddlstate.SelectedValue.ToString());
            
            ddlcity.DataSource = dtcity;
            ddlcity.DataTextField = "CITYNAME";
            ddlcity.DataValueField = "CITYID";
            ddlcity.DataBind();
         
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
        dtData.Columns.Add("EMIALID");
        dtData.Columns.Add("PHONE");
        dtData.Columns.Add("MOBILE");
        for (int i = 0; i < value; i++)
        {
            DataRow dtrow = dtData.NewRow();
            dtData.Rows.Add(dtrow);
            dtData.Rows[i]["SRNO"] = i + 1;

        }

        return dtData;
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
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
        objgodown.godownmaster_STATUS = 0;
        if (objgodown.Insert(true, "godownmaster"))
        {
            string sqlmax = "SELECT MAX(SRNO) AS SRNO FROM GODOWNMASTER GM WHERE STATUS=0 AND GODOWNNAME='" + txtgodownname.Text.ToString().Trim() + "' AND CITYNAME='" + ddlcity.SelectedItem.Text.ToString().Trim() + "'";
            Handler hdnmax = new Handler();
            DataTable dtmax = hdnmax.GetTable(sqlmax);
            if (dtmax.Rows.Count > 0 && memberrepeater.Items.Count > 0)
            {
                for (int i = 0; i < memberrepeater.Items.Count; i++)
                {

                    TextBox name = (TextBox)memberrepeater.Items[i].FindControl("txtpersonname");
                    DropDownList department = (DropDownList)memberrepeater.Items[i].FindControl("ddldepartment");
                    DropDownList designation = (DropDownList)memberrepeater.Items[i].FindControl("ddldesignation");
                    TextBox phone = (TextBox)memberrepeater.Items[i].FindControl("txtperphone");
                    TextBox email = (TextBox)memberrepeater.Items[i].FindControl("txtperemailid");
                    TextBox mobile = (TextBox)memberrepeater.Items[i].FindControl("txtpermobile");

                    personalmaster objpersonal = new personalmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                    objpersonal.personalmaster_SRNO = -1;
                    objpersonal.personalmaster_PERSONNAME = name.Text.ToString().Trim();
                    objpersonal.personalmaster_DEPARTMENT = department.SelectedItem.Text.Trim().ToString();
                    objpersonal.personalmaster_DESIGNATION = designation.SelectedItem.Text.Trim().ToString();
                    objpersonal.personalmaster_EMAILID = email.Text.ToString().Trim();
                    objpersonal.personalmaster_PHONENO = phone.Text.ToString().Trim();
                    objpersonal.personalmaster_MOBILE = mobile.Text.ToString().Trim();
                    objpersonal.personalmaster_PERSONRELATIONID = General.Parse<int>(dtmax.Rows[0][0].ToString());
                    objpersonal.personalmaster_PERSONTYPE = "GODOWN";
                    if (objpersonal.Insert(true, "personalmaster"))
                    {
                    }

                }
                Response.Redirect("depotdetails.aspx?DEPOTID=" + dtmax.Rows[0][0].ToString().Trim());
            }
        }


    }













}