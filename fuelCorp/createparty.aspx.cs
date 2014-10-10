using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class createparty : System.Web.UI.Page
{
    int samecity = 0;
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
        
        ddlworkstate.DataSource = dtstate;
        ddlworkstate.DataTextField = "STATENAME";
        ddlworkstate.DataValueField = "STATEID";
        ddlworkstate.DataBind();

        DataTable dtcity = FillCity(dtstate.Rows[0]["STATEID"].ToString());
        ddlcity.Enabled = true;
        ddlcity.DataSource = dtcity;
        ddlcity.DataTextField = "CITYNAME";
        ddlcity.DataValueField = "CITYID";
        ddlcity.DataBind();

        ddlworkcity.Enabled = true;
        ddlworkcity.DataSource = dtcity;
        ddlworkcity.DataTextField = "CITYNAME";
        ddlworkcity.DataValueField = "CITYID";
        ddlworkcity.DataBind();
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
        ddlcity.Enabled = true;
        ddlcity.DataSource = dtcity;
        ddlcity.DataTextField = "CITYNAME";
        ddlcity.DataValueField = "CITYID";
        ddlcity.DataBind();
        if (ddlworkstate.SelectedIndex == 0)
        {
            ddlworkstate.SelectedValue = ddlstate.SelectedValue.ToString();
            ddlworkcity.Enabled = true;
            ddlworkcity.DataSource = dtcity;
            ddlworkcity.DataTextField = "CITYNAME";
            ddlworkcity.DataValueField = "CITYID";
            ddlworkcity.DataBind();
       }
    }
    protected void ddlworkstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtcity = FillCity(ddlworkstate.SelectedValue.ToString());
        ddlworkcity.Enabled = true;
        ddlworkcity.DataSource = dtcity;
        ddlworkcity.DataTextField = "CITYNAME";
        ddlworkcity.DataValueField = "CITYID";
        ddlworkcity.DataBind();
    }
    protected void ddlcity_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlworkcity.SelectedIndex = General.Parse<int>(ddlcity.SelectedIndex.ToString());
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

    protected void lnkremove_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        string srno = lnk.CommandArgument.ToString().Trim();
        DataTable dt = (DataTable)ViewState["member"];
        ViewState["member"] = null;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["SRNO"].ToString().Trim() == srno)
            {
                dt.Rows.RemoveAt(i);
            }
        }
        ViewState["member"] = dt;
        if (dt.Rows.Count > 0)
        {
            memberrepeater.DataSource = (DataTable)ViewState["member"];
            memberrepeater.DataBind();
        }
        else
        {
            DataTable dt1 = GetFillDataTable(1);
            ViewState["member"] = dt1;
            memberrepeater.DataSource = (DataTable)ViewState["member"];
            memberrepeater.DataBind();
        }
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
            DropDownList department = (DropDownList)memberrepeater.Items[i].FindControl("ddldepartment");
            DropDownList designation = (DropDownList)memberrepeater.Items[i].FindControl("ddldesignation");
            TextBox phone = (TextBox)memberrepeater.Items[i].FindControl("txtperphone");
            TextBox email = (TextBox)memberrepeater.Items[i].FindControl("txtperemailid");
            TextBox mobile = (TextBox)memberrepeater.Items[i].FindControl("txtpermobile");

            int count = dtData.Rows.Count;
            dtData.Rows.Add(1);
            dtData.Rows[count]["SRNO"] = (count + 1).ToString();
            dtData.Rows[count]["PERSONNAME"] = name.Text.ToString();
            dtData.Rows[count]["DESIGNATION"] = designation.SelectedItem.Text.ToString();
            dtData.Rows[count]["DEPARTMENT"] = department.SelectedItem.Text.ToString();

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
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        Handler hdnpartycode=new Handler();
        DataTable dtpartycode=hdnpartycode.GetTable("SELECT SRNO FROM PARTYMASTER PM WHERE PARTYCODE='"+txtpartycode.Text.Trim().ToString()+"'");
        if (dtpartycode.Rows.Count == 0)
        {
            DataTable dtmax = new DataTable();
            partymaster objpartymaster = new partymaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
            objpartymaster.partymaster_SRNO = -1;
            objpartymaster.partymaster_PARTYNAME = txtpartyname.Text.Trim().ToString();
            objpartymaster.partymaster_PARTYCODE = txtpartycode.Text.Trim().ToString();
            objpartymaster.partymaster_PARTYTYPE = ddlpartytype.SelectedItem.Text.ToString();
            objpartymaster.partymaster_PARTYTYPECODE = General.Parse<int>(ddlpartytype.SelectedValue.ToString());
            if (rdaddonyes.Checked == true)
            {
                objpartymaster.partymaster_PARTYADDON = 1;
            }
            else
            {
                objpartymaster.partymaster_PARTYADDON = 0;
            }
            objpartymaster.partymaster_PHONENO = txtphone.Text.Trim().ToString();
            if (txtfax.Text.Trim().ToString() != string.Empty)
            {
                objpartymaster.partymaster_FAX = txtfax.Text.Trim().ToString();
            }
            if (txtemail.Text.Trim().ToString() != string.Empty)
            {
                objpartymaster.partymaster_EMAIL = txtemail.Text.Trim().ToString();
            }
            if (txtwebsite.Text.Trim().ToString() != string.Empty)
            {
                objpartymaster.partymaster_WEBSITE = txtwebsite.Text.Trim().ToString();
            }
            if (txtpanno.Text.Trim().ToString() != string.Empty)
            {
                objpartymaster.partymaster_PANNO = txtpanno.Text.Trim().ToString();
            }
            if (txtcstno.Text.Trim().ToString() != string.Empty)
            {
                objpartymaster.partymaster_CSTNO = txtcstno.Text.Trim().ToString();
            }
            if (txtvatno.Text.Trim().ToString() != string.Empty)
            {
                objpartymaster.partymaster_VATNO = txtvatno.Text.Trim().ToString();
            }
            if (txtservicetaxno.Text.Trim().ToString() != string.Empty)
            {
                objpartymaster.partymaster_SERVICETAXNO = txtservicetaxno.Text.Trim().ToString();
            }
            if (txtexciseno.Text.Trim().ToString() != string.Empty)
            {
                objpartymaster.partymaster_EXCISENO = txtexciseno.Text.Trim().ToString();
            }
            if (txtexciserange.Text.Trim().ToString() != string.Empty)
            {
                objpartymaster.partymaster_EXCISERANGE = txtexciserange.Text.Trim().ToString();
            }
            if (txtexcisedivision.Text.Trim().ToString() != string.Empty)
            {
                objpartymaster.partymaster_EXCISEDIVISION = txtexcisedivision.Text.Trim().ToString();
            }
            if (txtexcisecollectrate.Text.Trim().ToString() != string.Empty)
            {
                objpartymaster.partymaster_EXCISECOLLECTRATE = txtexcisecollectrate.Text.Trim().ToString();
            }
            if (objpartymaster.Insert(true, "partymaster"))
            {
                string sqlmax = "SELECT MAX(SRNO) AS SRNO FROM partymaster WHERE PARTYNAME='" + txtpartyname.Text.Trim().ToString() + "' AND PARTYCODE='" + txtpartycode.Text.Trim().ToString() + "' ";
                Handler hdnmax = new Handler();
                dtmax = hdnmax.GetTable(sqlmax);
                if (dtmax.Rows.Count > 0)
                {
                    personaltable objpersonal1 = new personaltable(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                    objpersonal1.personaltable_RELATIONSHIPID = -1;
                    objpersonal1.personaltable_FIRSTNAME = txtpartyname.Text.Trim().ToString();
                    objpersonal1.personaltable_BRANCHID = General.Parse<int>(Session["branchid"].ToString());
                    Handler branch = new Handler();
                    DataTable dtbranch = branch.GetTable("select BRANCHNAME from branchmaster where branchid=" + Session["branchid"].ToString());
                    objpersonal1.personaltable_BRANCHNAME = dtbranch.Rows[0][0].ToString().Trim();
                    if (objpersonal1.Insert(true, "personaltable"))
                    {
                        string sql = "SELECT MAX(PR.RELATIONSHIPID) AS RELATIONSHIPID FROM personaltable PR WHERE PR.BRANCHID=1 AND PR.FIRSTNAME='" + txtpartyname.Text.Trim().ToString() + "'";
                        Handler hdn = new Handler();
                        DataTable dt = hdn.GetTable(sql);
                        personalrelation objpersonalrelation = new personalrelation(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                        objpersonalrelation.personalrelation_SRNO = -1;
                        objpersonalrelation.personalrelation_RELATIONSHIPID = General.Parse<int>(dt.Rows[0][0].ToString());
                        objpersonalrelation.personalrelation_ASSOSIATEDFEILD = "MAIN ACCOOUNT";
                        objpersonalrelation.personalrelation_ASSOSIATEDBRANCH = General.Parse<int>(Session["branchid"].ToString());
                        objpersonalrelation.personalrelation_GROUPID = 100;
                        objpersonalrelation.personalrelation_STATUS = 0;
                        if (objpersonalrelation.Insert(true, "personalrelation"))
                        {
                        }
                    }

                    partyaddressmaster objpartyaddressmaster = new partyaddressmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                    objpartyaddressmaster.partyaddressmaster_SRNO = -1;
                    objpartyaddressmaster.partyaddressmaster_PARTYID = General.Parse<int>(dtmax.Rows[0][0].ToString());
                    objpartyaddressmaster.partyaddressmaster_ADDRESSTYPE = 1;
                    objpartyaddressmaster.partyaddressmaster_ADDRESS = txtaddress.Text.Trim().ToString();
                    objpartyaddressmaster.partyaddressmaster_CITYID = General.Parse<int>(ddlcity.SelectedValue.ToString()); ;
                    objpartyaddressmaster.partyaddressmaster_CITYNAME = ddlcity.SelectedItem.Text.Trim().ToString(); ;
                    objpartyaddressmaster.partyaddressmaster_STATEID = General.Parse<int>(ddlstate.SelectedValue.ToString()); ;
                    objpartyaddressmaster.partyaddressmaster_STATENAME = ddlstate.SelectedItem.Text.Trim().ToString(); ;
                    objpartyaddressmaster.partyaddressmaster_PINCODE = txtpin.Text.Trim().ToString(); ;
                    objpartyaddressmaster.partyaddressmaster_STATUS = 0;
                    if (objpartyaddressmaster.Insert(true, "partyaddressmaster"))
                    {
                    }
                    partyaddressmaster objpartyworkaddressmaster = new partyaddressmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                    objpartyworkaddressmaster.partyaddressmaster_SRNO = -1;
                    objpartyworkaddressmaster.partyaddressmaster_PARTYID = General.Parse<int>(dtmax.Rows[0][0].ToString());
                    objpartyworkaddressmaster.partyaddressmaster_ADDRESSTYPE = 2;
                    objpartyworkaddressmaster.partyaddressmaster_ADDRESS = txtworkaddress.Text.Trim().ToString();
                    objpartyworkaddressmaster.partyaddressmaster_CITYID = General.Parse<int>(ddlworkcity.SelectedValue.ToString()); ;
                    objpartyworkaddressmaster.partyaddressmaster_CITYNAME = ddlworkcity.SelectedItem.Text.Trim().ToString(); ;
                    objpartyworkaddressmaster.partyaddressmaster_STATEID = General.Parse<int>(ddlworkstate.SelectedValue.ToString()); ;
                    objpartyworkaddressmaster.partyaddressmaster_STATENAME = ddlworkstate.SelectedItem.Text.Trim().ToString(); ;
                    objpartyworkaddressmaster.partyaddressmaster_PINCODE = txtworkpin.Text.Trim().ToString(); ;
                    objpartyworkaddressmaster.partyaddressmaster_STATUS = 0;
                    if (objpartyworkaddressmaster.Insert(true, "partyaddressmaster"))
                    {
                    }


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
                            objpersonal.personalmaster_PERSONTYPE = "PARTY";
                            if (objpersonal.Insert(true, "personalmaster"))
                            {
                            }


                        }
                    }
                    string sqltax = "SELECT SRNO AS TAXID,TAXNAME,TAXVALUE,TAXUNIT FROM taxmaster WHERE STATUS=0";
                    Handler hdntax = new Handler();
                    DataTable dttax = hdntax.GetTable(sqltax);

                    for (int k = 0; k < dttax.Rows.Count; k++)
                    {
                        if (dttax.Rows[k]["TAXNAME"].ToString().Trim() == "VAT" && rdvat.Checked == true)
                        {
                            partytaxmaster objpartytax = new partytaxmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                            objpartytax.partytaxmaster_SRNO = -1;
                            objpartytax.partytaxmaster_PARTYID = General.Parse<int>(dtmax.Rows[0][0].ToString());
                            objpartytax.partytaxmaster_TAXID = General.Parse<int>(dttax.Rows[k]["TAXID"].ToString());
                            objpartytax.partytaxmaster_TAXNAME = dttax.Rows[k]["TAXNAME"].ToString();
                            objpartytax.partytaxmaster_TAXVALUE = General.Parse<double>(dttax.Rows[k]["TAXVALUE"].ToString());
                            objpartytax.partytaxmaster_TAXUNIT = dttax.Rows[k]["TAXUNIT"].ToString();
                            if (objpartytax.Insert(true, "partytaxmaster"))
                            {
                            }
                        }
                        if (dttax.Rows[k]["TAXNAME"].ToString().Trim() == "CST" && rdexcise.Checked == true)
                        {
                            partytaxmaster objpartytax = new partytaxmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                            objpartytax.partytaxmaster_SRNO = -1;
                            objpartytax.partytaxmaster_PARTYID = General.Parse<int>(dtmax.Rows[0][0].ToString());
                            objpartytax.partytaxmaster_TAXID = General.Parse<int>(dttax.Rows[k]["TAXID"].ToString());
                            objpartytax.partytaxmaster_TAXNAME = dttax.Rows[k]["TAXNAME"].ToString();
                            objpartytax.partytaxmaster_TAXVALUE = General.Parse<double>(dttax.Rows[k]["TAXVALUE"].ToString());
                            objpartytax.partytaxmaster_TAXUNIT = dttax.Rows[k]["TAXUNIT"].ToString();
                            if (objpartytax.Insert(true, "partytaxmaster"))
                            {
                            }
                        }
                        if (dttax.Rows[k]["TAXNAME"].ToString().Trim() == "SERVICE TAX" && rdserviceapplicable.Checked == true)
                        {
                            partytaxmaster objpartytax = new partytaxmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                            objpartytax.partytaxmaster_SRNO = -1;
                            objpartytax.partytaxmaster_PARTYID = General.Parse<int>(dtmax.Rows[0][0].ToString());
                            objpartytax.partytaxmaster_TAXID = General.Parse<int>(dttax.Rows[k]["TAXID"].ToString());
                            objpartytax.partytaxmaster_TAXNAME = dttax.Rows[k]["TAXNAME"].ToString();
                            objpartytax.partytaxmaster_TAXVALUE = General.Parse<double>(dttax.Rows[k]["TAXVALUE"].ToString());
                            objpartytax.partytaxmaster_TAXUNIT = dttax.Rows[k]["TAXUNIT"].ToString();
                            if (objpartytax.Insert(true, "partytaxmaster"))
                            {
                            }
                        }
                        if (dttax.Rows[k]["TAXNAME"].ToString().Trim() == "EXCISE" && rdexciseapplicable.Checked == true)
                        {
                            partytaxmaster objpartytax = new partytaxmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                            objpartytax.partytaxmaster_SRNO = -1;
                            objpartytax.partytaxmaster_PARTYID = General.Parse<int>(dtmax.Rows[0][0].ToString());
                            objpartytax.partytaxmaster_TAXID = General.Parse<int>(dttax.Rows[k]["TAXID"].ToString());
                            objpartytax.partytaxmaster_TAXNAME = dttax.Rows[k]["TAXNAME"].ToString();
                            objpartytax.partytaxmaster_TAXVALUE = General.Parse<double>(dttax.Rows[k]["TAXVALUE"].ToString());
                            objpartytax.partytaxmaster_TAXUNIT = dttax.Rows[k]["TAXUNIT"].ToString();
                            if (objpartytax.Insert(true, "partytaxmaster"))
                            {
                            }
                        }
                    }
                }
                Response.Redirect("partylist.aspx?ID=2");

            }
            else
            {
                MessageBox("Party Code Already Present");
            }
       
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