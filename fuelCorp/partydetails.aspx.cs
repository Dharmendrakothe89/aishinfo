using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Odbc;
using System.Reflection;
using System.Collections;
using System.Data.SqlClient;

public partial class partydetails : System.Web.UI.Page
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
            ViewState["postback"] = "normal";
            if (Request.QueryString["PARTYID"] != null)
            {
                ViewState["PARTYID"] = Request.QueryString["PARTYID"].ToString();
                FillState();
                FillPersonData();
                FillData(ViewState["PARTYID"].ToString().Trim());
                DisablePageControl();
                if (Request.QueryString["ID"] != null)
                {
                    if (Request.QueryString["ID"].ToString() == "1")
                    {
                        MessageBox("Party Created Successfully");
                    }

                }
            }
        }
        editbtn = ViewState["postback"].ToString();
    }
    private void DisablePageControl()
    {
        txtpartyname.Enabled = false;
        txtpartycode.Enabled = false;
        ddlpartytype.Enabled = false;
        txtphone.Enabled = false;
        txtfax.Enabled = false;
        txtemail.Enabled = false;
        txtwebsite.Enabled = false;
        txtpanno.Enabled = false;
        txtcstno.Enabled = false;
        txtvatno.Enabled = false;
        txtservicetaxno.Enabled = false;
        txtexciseno.Enabled = false;
        txtexciserange.Enabled = false;
        txtexcisedivision.Enabled = false;
        rdaddonyes.Enabled = false;
        rdaddonno.Enabled = false;
        txtaddress.Enabled = false;
        txtexcisecollectrate.Enabled = false;
        txtcstno.Enabled = false;
        ddlstate.Enabled = false;
        ddlcity.Enabled = false;
        txtpin.Enabled = false;
        txtworkaddress.Enabled = false;
        ddlworkstate.Enabled = false;
        ddlworkcity.Enabled = false;
        txtworkpin.Enabled = false;

        rdvat.Enabled = false;
        rdexcise.Enabled = false;
        rdserviceapplicable.Enabled = false;
        rdservicenotapplicable.Enabled = false;
        rdexciseapplicable.Enabled = false;
        rdexcisenotapplicable.Enabled = false;
        btnupdate.Visible = false;

    }
    private void EnablePageControl()
    {
        //txtpartyname.Enabled = true;
        //txtpartycode.Enabled = true;
        ddlpartytype.Enabled = true;
        txtphone.Enabled = true;
        txtfax.Enabled = true;
        txtemail.Enabled = true;
        txtwebsite.Enabled = true;
        txtpanno.Enabled = true;
        txtcstno.Enabled = true;
        txtvatno.Enabled = true;
        txtservicetaxno.Enabled = true;
        txtexciseno.Enabled = true;
        txtexciserange.Enabled = true;
        txtexcisedivision.Enabled = true;
        rdaddonyes.Enabled = true;
        rdaddonno.Enabled = true;
        txtaddress.Enabled = true;

        txtcstno.Enabled = true;
        ddlstate.Enabled = true;
        ddlcity.Enabled = true;
        txtpin.Enabled = true;
        txtworkaddress.Enabled = true;
        ddlworkstate.Enabled = true;
        ddlworkcity.Enabled = true;
        txtworkpin.Enabled = true;
        txtexcisecollectrate.Enabled = true;
        rdvat.Enabled = true;
        rdexcise.Enabled = true;
        rdserviceapplicable.Enabled = true;
        rdservicenotapplicable.Enabled = true;
        rdexciseapplicable.Enabled = true;
        rdexcisenotapplicable.Enabled = true;
        btnupdate.Visible = true;
    }
    private void FillData(string partyid)
    {
        string sqlpartymaster = "SELECT * FROM PARTYMASTER PM WHERE SRNO=" + partyid;
        Handler hdnpartymaster = new Handler();
        DataTable dtpartymaster = hdnpartymaster.GetTable(sqlpartymaster);

        txtpartyname.Text = dtpartymaster.Rows[0]["PARTYNAME"].ToString();
        txtpartycode.Text = dtpartymaster.Rows[0]["PARTYCODE"].ToString();
        ddlpartytype.SelectedValue = dtpartymaster.Rows[0]["PARTYTYPECODE"].ToString();
        txtphone.Text = dtpartymaster.Rows[0]["PHONENO"].ToString();
        txtfax.Text = dtpartymaster.Rows[0]["FAX"].ToString();
        txtemail.Text = dtpartymaster.Rows[0]["EMAIL"].ToString();
        txtwebsite.Text = dtpartymaster.Rows[0]["WEBSITE"].ToString();
        txtpanno.Text = dtpartymaster.Rows[0]["PANNO"].ToString();
        txtcstno.Text = dtpartymaster.Rows[0]["CSTNO"].ToString();
        txtvatno.Text = dtpartymaster.Rows[0]["VATNO"].ToString();
        txtservicetaxno.Text = dtpartymaster.Rows[0]["SERVICETAXNO"].ToString();
        txtexciseno.Text = dtpartymaster.Rows[0]["EXCISENO"].ToString();
        txtexciserange.Text = dtpartymaster.Rows[0]["EXCISERANGE"].ToString();
        txtexcisedivision.Text = dtpartymaster.Rows[0]["EXCISEDIVISION"].ToString();
        txtexcisecollectrate.Text = dtpartymaster.Rows[0]["EXCISECOLLECTRATE"].ToString();
        if (dtpartymaster.Rows[0]["PARTYADDON"].ToString() == "True")
        {
            rdaddonyes.Checked = true;
        }
        else
        {
            rdaddonno.Checked = true;
        }

        string sqladdress = "SELECT * FROM PARTYADDRESSMASTER PAM WHERE PAM.STATUS=0 AND PAM.PARTYID=" + partyid;
        Handler hdnaddres = new Handler();
        DataTable dtaddress = hdnaddres.GetTable(sqladdress);
        if (dtaddress.Rows.Count > 0)
        {
            for (int i = 0; i < dtaddress.Rows.Count; i++)
            {
                if (i < 2)
                {
                    if (dtaddress.Rows[i]["ADDRESSTYPE"].ToString().Trim() == "1")
                    {
                        txtaddress.Text = dtaddress.Rows[i]["ADDRESS"].ToString().Trim();
                        ddlstate.SelectedValue = dtaddress.Rows[i]["STATEID"].ToString().Trim();
                        BindCity(dtaddress.Rows[i]["STATEID"].ToString().Trim());
                        ddlcity.SelectedValue = dtaddress.Rows[i]["CITYID"].ToString().Trim();
                        txtpin.Text = dtaddress.Rows[i]["PINCODE"].ToString().Trim();
                    }
                    else if (dtaddress.Rows[i]["ADDRESSTYPE"].ToString().Trim() == "2")
                    {
                        txtworkaddress.Text = dtaddress.Rows[i]["ADDRESS"].ToString().Trim();
                        ddlworkstate.SelectedValue = dtaddress.Rows[i]["STATEID"].ToString().Trim();
                        BindWorkCity(dtaddress.Rows[i]["STATEID"].ToString().Trim());
                        ddlworkcity.SelectedValue = dtaddress.Rows[i]["CITYID"].ToString().Trim();
                        txtworkpin.Text = dtaddress.Rows[i]["PINCODE"].ToString().Trim();
                    }
                }
            }
        }

        string sqltax = "SELECT * FROM PARTYTAXMASTER PTM WHERE PTM.STATUS=0 AND PTM.PARTYID=" + partyid;
        Handler hdntax = new Handler();
        DataTable dttax = hdntax.GetTable(sqltax);
        if (dttax.Rows.Count > 0)
        {
            for (int i = 0; i < dttax.Rows.Count; i++)
            {
                if (dttax.Rows[i]["TAXNAME"].ToString().Trim() == "VAT")
                {
                    rdvat.Checked = true;
                }
                else if (dttax.Rows[i]["TAXNAME"].ToString().Trim() == "CST")
                {
                    rdexcise.Checked = true;
                }
                
                if (dttax.Rows[i]["TAXNAME"].ToString().Trim() == "SERVICE TAX")
                {
                    rdserviceapplicable.Checked = true;
                }
                else
                {
                    rdservicenotapplicable.Checked = true;
                }

                if (dttax.Rows[i]["TAXNAME"].ToString().Trim() == "EXCISE")
                {
                    rdexciseapplicable.Checked = true;
                }
                else
                {
                    rdexcisenotapplicable.Checked = true;
                }
            }
        }
        else
        {
            rdservicenotapplicable.Checked = true;
        }
        string sqlperson = "SELECT * FROM PERSONALMASTER PM WHERE PM.PERSONTYPE='PARTY' AND PM.PERSONRELATIONID=" + partyid;
        Handler hdnperson = new Handler();
        DataTable dtperson = hdnperson.GetTable(sqlperson);
        ViewState["personaldata"] = dtperson;
        memberrepeater.DataSource = (DataTable)ViewState["personaldata"];
        memberrepeater.DataBind();
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
        ddlworkstate.DataSource = dtstate;
        ddlworkstate.DataTextField = "STATENAME";
        ddlworkstate.DataValueField = "STATEID";

        ddlworkstate.DataBind();
        ddlworkstate.Items.Insert(0, "--State--");

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
            TextBox personame = (TextBox)e.Item.FindControl("txtpersonname");
            TextBox email = (TextBox)e.Item.FindControl("txtperemailid");
            TextBox phone = (TextBox)e.Item.FindControl("txtperphone");
            TextBox mobile = (TextBox)e.Item.FindControl("txtpermobile");
            if (editbtn == "UPDATE")
            {
                ddldepartment.Enabled = true;
                ddldesignation.Enabled = true;
                personame.Enabled = true;
                email.Enabled = true;
                phone.Enabled = true;
                mobile.Enabled = true;
                
                rowaddmore.Style.Add("display", "Block");
            }
            else
            {
                ddldepartment.Enabled = false;
                ddldesignation.Enabled = false;
                personame.Enabled = false;
                email.Enabled = false;
                phone.Enabled = false;
                mobile.Enabled = false;
                
                rowaddmore.Style.Add("display", "none");
            }
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
    private DataTable FillCity(string stateid)
    {
        string sqlcity = "SELECT CITYNAME,CITYID FROM CITYMASTER CM WHERE CM.STATUS=0 AND CM.STATEID=" + stateid + " ORDER BY CM.CITYNAME";
        Handler hdncity = new Handler();
        DataTable dtcity = hdncity.GetTable(sqlcity);
        return dtcity;
    }
    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlstate.SelectedIndex > 0)
        {
            BindCity(ddlstate.SelectedValue.ToString());
            ddlcity.Enabled = true;
        }
        else
        {
            ddlcity.Enabled = false;
        }
    }
    private void BindCity(string stateid)
    {
        DataTable dtcity = FillCity(stateid);
        ddlcity.DataSource = dtcity;
        ddlcity.DataTextField = "CITYNAME";
        ddlcity.DataValueField = "CITYID";
        ddlcity.DataBind();
        ddlcity.Items.Insert(0, "--City--");
    }
    protected void ddlworkstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlworkstate.SelectedIndex > 0)
        {
            BindWorkCity(ddlworkstate.SelectedValue.ToString());
            ddlworkcity.Enabled = true;
        }
        else
        {
            ddlworkcity.Enabled = false;
        }
    }
    private void BindWorkCity(string stateid)
    {
        DataTable dtcity = FillCity(stateid);

        ddlworkcity.DataSource = dtcity;
        ddlworkcity.DataTextField = "CITYNAME";
        ddlworkcity.DataValueField = "CITYID";

        ddlworkcity.DataBind();
        ddlworkcity.Items.Insert(0, "--City--");
    }

    private void ChangeControlStatus(bool status)
    {

        foreach (Control c in Page.Controls)
            foreach (Control ctrl in c.Controls)

                if (ctrl is TextBox)

                    ((TextBox)ctrl).Enabled = status;

                else if (ctrl is Button)

                    ((Button)ctrl).Enabled = status;

                else if (ctrl is RadioButton)

                    ((RadioButton)ctrl).Enabled = status;

                else if (ctrl is ImageButton)

                    ((ImageButton)ctrl).Enabled = status;

                else if (ctrl is CheckBox)

                    ((CheckBox)ctrl).Enabled = status;

                else if (ctrl is DropDownList)

                    ((DropDownList)ctrl).Enabled = status;

                else if (ctrl is HyperLink)

                    ((HyperLink)ctrl).Enabled = status;



    }
    protected void btnedit_Click(object sender, EventArgs e)
    {
        ViewState["postback"] = "UPDATE";
        editbtn = ViewState["postback"].ToString().Trim();
        EnablePageControl();
        memberrepeater.DataSource = (DataTable)ViewState["personaldata"];
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
        dtData.Columns.Add("EMAILID");
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

            dtData.Rows[count]["EMAILID"] = email.Text.ToString();
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
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        DataTable dtmax = new DataTable();
        partymaster objpartymaster = new partymaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
        objpartymaster.partymaster_SRNO = -1;
        //objpartymaster.partymaster_PARTYNAME = txtpartyname.Text.Trim().ToString();
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
        objpartymaster.partymaster_EMAIL = txtemail.Text.Trim().ToString();
        if (txtwebsite.Text.Trim().ToString() != string.Empty)
        {
            objpartymaster.partymaster_WEBSITE = txtwebsite.Text.Trim().ToString();
        }
        objpartymaster.partymaster_PANNO = txtpanno.Text.Trim().ToString();
        objpartymaster.partymaster_CSTNO = txtcstno.Text.Trim().ToString();
        objpartymaster.partymaster_VATNO = txtvatno.Text.Trim().ToString();
        objpartymaster.partymaster_SERVICETAXNO = txtservicetaxno.Text.Trim().ToString();
        objpartymaster.partymaster_EXCISENO = txtexciseno.Text.Trim().ToString();
        objpartymaster.partymaster_EXCISERANGE = txtexciserange.Text.Trim().ToString();
        objpartymaster.partymaster_EXCISEDIVISION = txtexcisedivision.Text.Trim().ToString();
        objpartymaster.partymaster_EXCISECOLLECTRATE = txtexcisecollectrate.Text.Trim().ToString();
        string condition = "SRNO=" + ViewState["PARTYID"].ToString().Trim();
        if (objpartymaster.Insert(false, "partymaster", condition))
        {

            {
                partyaddressmaster objpartyaddressmaster = new partyaddressmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                objpartyaddressmaster.partyaddressmaster_SRNO = -1;
                objpartyaddressmaster.partyaddressmaster_PARTYID = -1;
                objpartyaddressmaster.partyaddressmaster_ADDRESSTYPE = -1;
                objpartyaddressmaster.partyaddressmaster_ADDRESS = txtaddress.Text.Trim().ToString();
                objpartyaddressmaster.partyaddressmaster_CITYID = General.Parse<int>(ddlcity.SelectedValue.ToString()); ;
                objpartyaddressmaster.partyaddressmaster_CITYNAME = ddlcity.SelectedItem.Text.Trim().ToString(); ;
                objpartyaddressmaster.partyaddressmaster_STATEID = General.Parse<int>(ddlstate.SelectedValue.ToString()); ;
                objpartyaddressmaster.partyaddressmaster_STATENAME = ddlstate.SelectedItem.Text.Trim().ToString(); ;
                objpartyaddressmaster.partyaddressmaster_PINCODE = txtpin.Text.Trim().ToString(); ;
                objpartyaddressmaster.partyaddressmaster_STATUS = 0;
                string condition1 = "PARTYID=" + ViewState["PARTYID"].ToString().Trim() + " AND ADDRESSTYPE=1";
                if (objpartyaddressmaster.Insert(false, "partyaddressmaster", condition1))
                {
                }
                partyaddressmaster objpartyworkaddressmaster = new partyaddressmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                objpartyworkaddressmaster.partyaddressmaster_SRNO = -1;
                objpartyworkaddressmaster.partyaddressmaster_PARTYID = -1;
                objpartyworkaddressmaster.partyaddressmaster_ADDRESSTYPE = -1;
                objpartyworkaddressmaster.partyaddressmaster_ADDRESS = txtworkaddress.Text.Trim().ToString();
                objpartyworkaddressmaster.partyaddressmaster_CITYID = General.Parse<int>(ddlworkcity.SelectedValue.ToString()); ;
                objpartyworkaddressmaster.partyaddressmaster_CITYNAME = ddlworkcity.SelectedItem.Text.Trim().ToString(); ;
                objpartyworkaddressmaster.partyaddressmaster_STATEID = General.Parse<int>(ddlworkstate.SelectedValue.ToString()); ;
                objpartyworkaddressmaster.partyaddressmaster_STATENAME = ddlworkstate.SelectedItem.Text.Trim().ToString(); ;
                objpartyworkaddressmaster.partyaddressmaster_PINCODE = txtworkpin.Text.Trim().ToString(); ;
                objpartyworkaddressmaster.partyaddressmaster_STATUS = 0;
                string condition2 = "PARTYID=" + ViewState["PARTYID"].ToString().Trim() + " AND ADDRESSTYPE=2";
                if (objpartyworkaddressmaster.Insert(false, "partyaddressmaster", condition2))
                {
                }


                if (memberrepeater.Items.Count > 0)
                {


                    int membercount = ((DataTable)ViewState["personaldata"]).Rows.Count;
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
                            objpersonal.personalmaster_EMAILID = email.Text.ToString().Trim();
                            objpersonal.personalmaster_PHONENO = phone.Text.ToString().Trim();
                            objpersonal.personalmaster_MOBILE = mobile.Text.ToString().Trim();
                            objpersonal.personalmaster_PERSONRELATIONID = -1;
                            string taxcondition = "SRNO=" + ((DataTable)ViewState["personaldata"]).Rows[i]["SRNO"].ToString().Trim();
                            if (objpersonal.Insert(false, "personalmaster", taxcondition))
                            {
                            }
                        }
                        else
                        {
                            personalmaster objpersonal = new personalmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                            objpersonal.personalmaster_SRNO = -1;
                            objpersonal.personalmaster_PERSONNAME = name.Text.ToString().Trim();
                            objpersonal.personalmaster_EMAILID = email.Text.ToString().Trim();
                            objpersonal.personalmaster_PHONENO = phone.Text.ToString().Trim();
                            objpersonal.personalmaster_MOBILE = mobile.Text.ToString().Trim();
                            objpersonal.personalmaster_PERSONRELATIONID = General.Parse<int>(ViewState["PARTYID"].ToString().Trim());
                            objpersonal.personalmaster_PERSONTYPE = "PARTY";
                            if (objpersonal.Insert(true, "personalmaster"))
                            {
                            }
                        }

                    }


                }
                string sqltax = "SELECT SRNO AS TAXID,TAXNAME,TAXVALUE,TAXUNIT FROM taxmaster WHERE STATUS=0";
                Handler hdntax = new Handler();
                DataTable dttax = hdntax.GetTable(sqltax);

                SqlConnection Connection = new SqlConnection("Data Source=50.28.62.129,1433;Network Library=DBMSSOCN;Initial Catalog=db_fuel;User ID=fuel;Password= lSa2@11h");
                string qry = "delete from partytaxmaster where partyid=" + ViewState["PARTYID"].ToString();

                Connection.Open();
                SqlCommand com = new SqlCommand(qry, Connection);
                com.ExecuteNonQuery();
                Connection.Close();

                for (int k = 0; k < dttax.Rows.Count; k++)
                {
                    if (dttax.Rows[k]["TAXNAME"].ToString().Trim() == "VAT" && rdvat.Checked == true)
                    {
                        partytaxmaster objpartytax = new partytaxmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                        objpartytax.partytaxmaster_SRNO = -1;
                        objpartytax.partytaxmaster_PARTYID = General.Parse<int>(ViewState["PARTYID"].ToString());
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
                        objpartytax.partytaxmaster_PARTYID = General.Parse<int>(ViewState["PARTYID"].ToString());
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
                        objpartytax.partytaxmaster_PARTYID = General.Parse<int>(ViewState["PARTYID"].ToString());
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
                        objpartytax.partytaxmaster_PARTYID = General.Parse<int>(ViewState["PARTYID"].ToString());
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
        }
        MessageBox("Party Updated Successfully");
        Response.Redirect("partylist.aspx?id=1");
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