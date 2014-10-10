using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class createtranspoter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("default.aspx");
            }
            DataTable dt = GetFillDataTable(1);
            ViewState["member"] = dt;
            memberrepeater.DataSource = (DataTable)ViewState["member"];
            memberrepeater.DataBind();
        }
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

        dtData.Columns.Add("EMIALID");
        dtData.Columns.Add("PHONE");
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
        string sqlcheck = "SELECT RTRIM(SRNO) AS TRANSPORTERNO FROM TRANSPORTERMASTER TM WHERE TRANSPORTERCODE='" + txttransportercode.Text.Trim().ToString() + "'";
        Handler hdncheck = new Handler();
        DataTable dtcheck = hdncheck.GetTable(sqlcheck);
        if (dtcheck.Rows.Count == 0)
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
            if (txtemail.Text.Trim().ToString() != string.Empty)
            {
                objtransporter.transportermaster_EMAILID = txtemail.Text.Trim().ToString();
            }
            objtransporter.transportermaster_PANNO = txtpano.Text.Trim().ToString();
            objtransporter.transportermaster_SERVICETAXNO = txtservicetaxno.Text.Trim().ToString();
            objtransporter.transportermaster_ADDRESS = txtaddress.Text.Trim().ToString();
            objtransporter.transportermaster_STATUS = 0;
            if (objtransporter.Insert(true, "transportermaster"))
            {
                string sqlmax = "SELECT MAX(SRNO) AS SRNO FROM TRANSPORTERMASTER TM WHERE STATUS=0 AND TRANSPORTERNAME='" + txttransportername.Text.Trim().ToString() + "' AND TRANSPORTERCODE='" + txttransportercode.Text.Trim().ToString() + "'";
                Handler hdnmax = new Handler();
                DataTable dtmax = hdnmax.GetTable(sqlmax);

                if (rdserviceapplicable.Checked == true)
                {
                    string sqltax = "SELECT SRNO,TAXNAME,TAXUNIT,TAXVALUE FROM TAXMASTER TM WHERE TM.STATUS=0 AND TM.TAXNAME='SERVICE TAX'";
                    Handler hdntax = new Handler();
                    DataTable dttax = hdntax.GetTable(sqltax);
                    taxationmaster objtax = new taxationmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                    objtax.taxationmaster_SRNO = -1;
                    objtax.taxationmaster_TAXPARTYTYPE = "TRANSPORTER";
                    objtax.taxationmaster_TAXPARTYID = General.Parse<int>(dtmax.Rows[0][0].ToString());
                    objtax.taxationmaster_TAXID = General.Parse<int>(dttax.Rows[0]["SRNO"].ToString());
                    objtax.taxationmaster_TAXNAME = dttax.Rows[0]["TAXNAME"].ToString().Trim();
                    objtax.taxationmaster_TAXVALUE = General.Parse<double>(dttax.Rows[0]["TAXVALUE"].ToString().Trim());
                    objtax.taxationmaster_TAXUNIT = dttax.Rows[0]["TAXUNIT"].ToString().Trim();
                    objtax.taxationmaster_STATUS = 0;
                    if (objtax.Insert(true, "taxationmaster"))
                    {
                    }
                }
                if (dtmax.Rows.Count > 0 && memberrepeater.Items.Count > 0)
                {
                    for (int i = 0; i < memberrepeater.Items.Count; i++)
                    {

                        TextBox name = (TextBox)memberrepeater.Items[i].FindControl("txtpersonname");

                        TextBox phone = (TextBox)memberrepeater.Items[i].FindControl("txtperphone");
                        TextBox email = (TextBox)memberrepeater.Items[i].FindControl("txtperemailid");
                        TextBox mobile = (TextBox)memberrepeater.Items[i].FindControl("txtpermobile");

                        personalmaster objpersonal = new personalmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                        objpersonal.personalmaster_SRNO = -1;
                        objpersonal.personalmaster_PERSONNAME = name.Text.ToString().Trim();
                        if (email.Text.ToString().Trim() != string.Empty)
                        {
                            objpersonal.personalmaster_EMAILID = email.Text.ToString().Trim();
                        }
                        objpersonal.personalmaster_PHONENO = phone.Text.ToString().Trim();
                        objpersonal.personalmaster_MOBILE = mobile.Text.ToString().Trim();
                        objpersonal.personalmaster_PERSONRELATIONID = General.Parse<int>(dtmax.Rows[0][0].ToString());
                        objpersonal.personalmaster_PERSONTYPE = "TRANSPORTER";
                        if (objpersonal.Insert(true, "personalmaster"))
                        {
                        }

                    }
                }
                personaltable objpersonal1 = new personaltable(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                objpersonal1.personaltable_RELATIONSHIPID = -1;
                objpersonal1.personaltable_FIRSTNAME = txttransportername.Text.Trim().ToString();
                objpersonal1.personaltable_BRANCHID = General.Parse<int>(Session["branchid"].ToString());
                Handler branch = new Handler();
                DataTable dtbranch = branch.GetTable("select BRANCHNAME from branchmaster where branchid=" + Session["branchid"].ToString());
                objpersonal1.personaltable_BRANCHNAME = dtbranch.Rows[0][0].ToString().Trim();
                if (objpersonal1.Insert(true, "personaltable"))
                {
                    string sql = "SELECT MAX(PR.RELATIONSHIPID) AS RELATIONSHIPID FROM personaltable PR WHERE PR.BRANCHID=1 AND PR.FIRSTNAME='" + txttransportername.Text.Trim().ToString() + "'";
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
                MessageBox("Transporter Added Successfully");
                Response.Redirect("transporterlist.aspx?ID=2");
                //Response.Redirect("transporterdetails.aspx?ID=1&TRANSPORTERID=" + dtmax.Rows[0][0].ToString());
            }
        }
        else
        {
            MessageBox("Transporter Code Already  Exist");
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