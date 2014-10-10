using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class transporterdetails : System.Web.UI.Page
{
    string editbtn="1";
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
            if (Request.QueryString["TRANSPORTERID"] != null)
            {
                ViewState["TRANSPORTERID"] = Request.QueryString["TRANSPORTERID"].ToString();
                FillData(ViewState["TRANSPORTERID"].ToString().Trim());

                if (Request.QueryString["ID"] != null)
                {
                    if (Request.QueryString["ID"].ToString() == "1")
                    {
                        MessageBox("Transporter Created Successfully");
                    }
                }

            }
        }
        editbtn = ViewState["postback"].ToString();
    }
    private void FillData(string transporterid)
    {
        string sqlcolliery = "SELECT * FROM TRANSPORTERMASTER TM WHERE TM.SRNO=" + transporterid;
        Handler hdncolliery = new Handler();

        DataTable dtcolliery = hdncolliery.GetTable(sqlcolliery);
        if (dtcolliery.Rows.Count > 0)
        {
            txttransportername.Text = dtcolliery.Rows[0]["TRANSPORTERNAME"].ToString().Trim();
            txttransportercode.Text = dtcolliery.Rows[0]["TRANSPORTERCODE"].ToString().Trim();
            if (dtcolliery.Rows[0]["TRANTYPE"].ToString() == "LOCAL")
            {
                rdlocal.Checked = true;
            }
            else if (dtcolliery.Rows[0]["TRANTYPE"].ToString() == "REMOTE")
            {
                rdremote.Checked = true;
            }
            txtaddress.Text = dtcolliery.Rows[0]["ADDRESS"].ToString().Trim();
            txtphone.Text = dtcolliery.Rows[0]["MOBILENO"].ToString().Trim();
            txtfax.Text = dtcolliery.Rows[0]["FAX"].ToString().Trim();
            txtemail.Text = dtcolliery.Rows[0]["EMAILID"].ToString().Trim();
            txtpanno.Text = dtcolliery.Rows[0]["PANNO"].ToString().Trim();
            txtservicetaxno.Text = dtcolliery.Rows[0]["SERVICETAXNO"].ToString().Trim();
            if (dtcolliery.Rows[0]["STATUS"].ToString().Trim() == "True")
            {
                ddlstatus.SelectedValue = "1";
            }
            else
            {
                ddlstatus.SelectedValue = "0";
            }
        }
        string sqltax = "SELECT * FROM TAXATIONMASTER TM WHERE STATUS=0 AND TM.TAXPARTYTYPE='TRANSPORTER' AND TAXPARTYID=" + transporterid;
        Handler hdntax = new Handler();
        DataTable dttax = hdntax.GetTable(sqltax);
        if (dttax.Rows.Count > 0)
        {
            rdserviceapplicable.Checked = true;
        }
        else
        {
            rdservicenotapplicable.Checked = true;
        }

        string sqlperson = "SELECT * FROM PERSONALMASTER PM WHERE PM.PERSONTYPE='TRANSPORTER' AND PM.PERSONRELATIONID=" + transporterid;
        Handler hdnperson = new Handler();
        DataTable dtperson = hdnperson.GetTable(sqlperson);
        ViewState["person"] = dtperson;
        ViewState["personrecord"] = dtperson;
        memberrepeater.DataSource = dtperson;
        memberrepeater.DataBind();
    }
    protected void memberrepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (editbtn == "UPDATE")
            {
                TextBox txtpersonname = (TextBox)e.Item.FindControl("txtpersonname");
                TextBox txtperemailid = (TextBox)e.Item.FindControl("txtperemailid");
                TextBox txtperphone = (TextBox)e.Item.FindControl("txtperphone");
                TextBox txtpermobile = (TextBox)e.Item.FindControl("txtpermobile");
                txtpersonname.Enabled = true;
                txtperemailid.Enabled = true;
                txtperphone.Enabled = true;
                txtpermobile.Enabled = true;
            }
        }
    }
    protected void btnedit_Click(object sender, EventArgs e)
    {
        ViewState["postback"] = "UPDATE";
        editbtn = ViewState["postback"].ToString().Trim();
        tradd.Style.Add("display", "block");
        //txttransportercode.Enabled = true;
        rdlocal.Enabled = true;
        rdremote.Enabled = true;
        txtaddress.Enabled = true;
        txtphone.Enabled = true;
        txtfax.Enabled = true;
        txtemail.Enabled = true;
        rdserviceapplicable.Enabled = true;
        rdservicenotapplicable.Enabled = true;
        ddlstatus.Enabled = true;
        btnupdate.Visible = true;
        txtpanno.Enabled = true;
        txtservicetaxno.Enabled = true;
        memberrepeater.DataSource = (DataTable)ViewState["personrecord"];
        memberrepeater.DataBind();

    }
    protected void lnkaddmember_Click(object sender, EventArgs e)
    {
        DataTable dt1 = GetFillPreviousData();

        ViewState["person"] = dt1;

        DataTable dt = GetFillDataTable(1);
        if (ViewState["person"] != null)
        {
            ((DataTable)ViewState["person"]).Merge(dt);

        }
        else
        {
            ViewState["person"] = dt;
        }
        memberrepeater.DataSource = (DataTable)ViewState["person"];
        memberrepeater.DataBind();
    }
    private DataTable GetFillPreviousData()
    {
        DataTable dtData = new DataTable();
        dtData.Columns.Add("SRNO");
        dtData.Columns.Add("PERSONNAME");

        dtData.Columns.Add("EMIALID");
        dtData.Columns.Add("PHONENO");
        dtData.Columns.Add("MOBILE");
        for (int i = 0; i < memberrepeater.Items.Count; i++)
        {

            TextBox name = (TextBox)memberrepeater.Items[i].FindControl("txtpersonname");

            TextBox phone = (TextBox)memberrepeater.Items[i].FindControl("txtperphone");
            TextBox email = (TextBox)memberrepeater.Items[i].FindControl("txtperemailid");
            TextBox mobile = (TextBox)memberrepeater.Items[i].FindControl("txtpermobile");
            int count = dtData.Rows.Count;
            dtData.Rows.Add(1);
            dtData.Rows[count]["SRNO"] = (count + 1).ToString();
            dtData.Rows[count]["PERSONNAME"] = name.Text.ToString();

            dtData.Rows[count]["EMIALID"] = email.Text.ToString();
            dtData.Rows[count]["PHONENO"] = phone.Text.ToString();
            dtData.Rows[count]["MOBILE"] = mobile.Text.ToString();

        }

        return dtData;
    }
    private DataTable GetFillDataTable(int value)
    {
        DataTable dtData = new DataTable();
        dtData.Columns.Add("SRNO");
        dtData.Columns.Add("PERSONNAME");

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
        transportermaster objtransporter = new transportermaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
        objtransporter.transportermaster_SRNO = -1;
        objtransporter.transportermaster_TRANSPORTERNAME = txttransportername.Text.Trim().ToString();
        objtransporter.transportermaster_TRANSPORTERCODE = txttransportercode.Text.Trim().ToString();
        if (rdlocal.Checked == true)
        {
            objtransporter.transportermaster_TRANTYPE = "LOCAL";
            objtransporter.transportermaster_TRANTYPECODE = 1;
        }
        else
        {
            objtransporter.transportermaster_TRANTYPE = "REMOTE";
            objtransporter.transportermaster_TRANTYPECODE = 2;
        }
        objtransporter.transportermaster_MOBILENO = txtphone.Text.Trim().ToString();
        objtransporter.transportermaster_FAX = txtfax.Text.Trim().ToString();
        objtransporter.transportermaster_EMAILID = txtemail.Text.Trim().ToString();
        objtransporter.transportermaster_PANNO = txtpanno.Text.Trim().ToString();
        objtransporter.transportermaster_SERVICETAXNO = txtservicetaxno.Text.Trim().ToString();
        objtransporter.transportermaster_ADDRESS = txtaddress.Text.Trim().ToString();
        objtransporter.transportermaster_STATUS = General.Parse<int>(ddlstatus.SelectedValue.Trim().ToString());
        string condition = "SRNO=" + ViewState["TRANSPORTERID"].ToString().Trim();
        if (objtransporter.Insert(false, "transportermaster", condition))
        {
            string sqltax = "SELECT SRNO,TAXNAME,TAXUNIT,TAXVALUE FROM TAXMASTER TM WHERE TM.STATUS=0 AND TM.TAXNAME='SERVICE TAX'";
            Handler hdntax = new Handler();
            DataTable dttax = hdntax.GetTable(sqltax);

            string sqlchecktax = "SELECT SRNO,STATUS FROM TAXATIONMASTER TM WHERE TAXPARTYTYPE='TRANSPORTER' AND TAXPARTYID="+ViewState["TRANSPORTERID"].ToString().Trim()+" AND TAXID="+dttax.Rows[0]["SRNO"].ToString();
            Handler hdntax1 = new Handler();
            DataTable dtchecktax = hdntax1.GetTable(sqlchecktax);
            if(dtchecktax.Rows.Count > 0 && dtchecktax.Rows[0][0].ToString() != string.Empty)
            {
                if (rdserviceapplicable.Checked == true && dtchecktax.Rows[0][1].ToString() == "1")
                {
                    taxationmaster objtax = new taxationmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                    objtax.taxationmaster_SRNO = -1;
                    objtax.taxationmaster_TAXPARTYID =-1;
                    objtax.taxationmaster_TAXID =-1;
                    objtax.taxationmaster_TAXVALUE = -1;
                    objtax.taxationmaster_STATUS = 0;
                    string taxcondition="SRNO="+dtchecktax.Rows[0][0].ToString();
                    if (objtax.Insert(false, "taxationmaster",taxcondition))
                    {
                    }
                }
                else if (rdservicenotapplicable.Checked == true && dtchecktax.Rows[0][1].ToString() == "0")
                {
                    taxationmaster objtax = new taxationmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                    objtax.taxationmaster_SRNO = -1;
                    objtax.taxationmaster_TAXPARTYID =-1;
                    objtax.taxationmaster_TAXID =-1;
                    objtax.taxationmaster_TAXVALUE = -1;
                    objtax.taxationmaster_STATUS =1;
                    string taxcondition="SRNO="+dtchecktax.Rows[0][0].ToString();
                    if (objtax.Insert(false, "taxationmaster",taxcondition))
                    {
                    }
                }
            }
           
            if (memberrepeater.Items.Count > 0)
            {
                int membercount = ((DataTable)ViewState["personrecord"]).Rows.Count;
                for (int i = 0; i < memberrepeater.Items.Count; i++)
                {

                    TextBox name = (TextBox)memberrepeater.Items[i].FindControl("txtpersonname");

                    TextBox phone = (TextBox)memberrepeater.Items[i].FindControl("txtperphone");
                    TextBox email = (TextBox)memberrepeater.Items[i].FindControl("txtperemailid");
                    TextBox mobile = (TextBox)memberrepeater.Items[i].FindControl("txtpermobile");
                    if(i<membercount)
                    {
                        personalmaster objpersonal = new personalmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                        objpersonal.personalmaster_SRNO = -1;
                        objpersonal.personalmaster_PERSONNAME = name.Text.ToString().Trim();
                        objpersonal.personalmaster_EMAILID = email.Text.ToString().Trim();
                        objpersonal.personalmaster_PHONENO = phone.Text.ToString().Trim();
                        objpersonal.personalmaster_MOBILE = mobile.Text.ToString().Trim();
                        objpersonal.personalmaster_PERSONRELATIONID =-1;
                        string taxcondition = "SRNO=" + ((DataTable)ViewState["personrecord"]).Rows[i]["SRNO"].ToString().Trim();
                        if (objpersonal.Insert(false, "personalmaster",taxcondition))
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
                        objpersonal.personalmaster_PERSONRELATIONID = General.Parse<int>(ViewState["TRANSPORTERID"].ToString().Trim());
                        objpersonal.personalmaster_PERSONTYPE = "TRANSPORTER";
                         if (objpersonal.Insert(true, "personalmaster"))
                        {
                        }
                    }

                }
            }
            MessageBox("Transporter Details Updated Successfully");
            Response.Redirect("transporterlist.aspx?ID=1");
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