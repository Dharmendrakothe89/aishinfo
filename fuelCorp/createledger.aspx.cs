using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class createledger : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("default.aspx");
            }
            FillGroup();
        }
    }

    private void FillGroup()
    {
        string sqlgroup = "SELECT GROUPNAME,GROUPID FROM accountgroup AG WHERE ASSOGROUPID=0 AND BRANCHID=0";
        Handler hdngroup = new Handler();
        DataTable dtgroup = hdngroup.GetTable(sqlgroup);
        ddlgroup.DataSource = dtgroup;
        ddlgroup.DataTextField = "GROUPNAME";
        ddlgroup.DataValueField = "GROUPID";
        ddlgroup.DataBind();

        DataTable dtcity = FillSubgroup(dtgroup.Rows[0]["GROUPID"].ToString().Trim());
        ddlsubgroup.Enabled = true;
        ddlsubgroup.DataSource = dtcity;
        ddlsubgroup.DataTextField = "GROUPNAME";
        ddlsubgroup.DataValueField = "GROUPID";

        ddlsubgroup.DataBind();
        ListItem li = new ListItem();
        li.Text = ddlgroup.SelectedItem.Text.ToString();
        li.Value = ddlgroup.SelectedValue.ToString();
        ddlsubgroup.Items.Insert(0, li);

    }
    private DataTable FillSubgroup(string groupid)
    {
        string sql = "SELECT GROUPNAME,GROUPID FROM accountgroup AG WHERE ASSOGROUPID=" + groupid + " AND BRANCHID=0";
        Handler hdn = new Handler();
        DataTable dt = hdn.GetTable(sql);
        return dt;
    }

    protected void ddlgroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlgroup.SelectedIndex > 0)
        {
            DataTable dtcity = FillSubgroup(ddlgroup.SelectedValue.ToString());
            ddlsubgroup.Enabled = true;
            ddlsubgroup.DataSource = dtcity;
            ddlsubgroup.DataTextField = "GROUPNAME";
            ddlsubgroup.DataValueField = "GROUPID";

            ddlsubgroup.DataBind();
            ListItem li = new ListItem();
            li.Text = ddlgroup.SelectedItem.Text.ToString();
            li.Value = ddlgroup.SelectedValue.ToString();
            ddlsubgroup.Items.Insert(0, li);
        }
        else
        {
            ddlsubgroup.Enabled = false;
        }
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        int check = 0;
        personaltable objpersonal = new personaltable(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
        objpersonal.personaltable_RELATIONSHIPID = -1;
        objpersonal.personaltable_FIRSTNAME = txtledgername.Text.Trim().ToString();
        objpersonal.personaltable_BRANCHID = General.Parse<int>(Session["branchid"].ToString());
        objpersonal.personaltable_BRANCHNAME = Session["branchname"].ToString();
        if (objpersonal.Insert(true, "personaltable"))
        {
            string sql = "SELECT MAX(PR.RELATIONSHIPID) AS RELATIONSHIPID FROM personaltable PR WHERE PR.BRANCHID=1 AND PR.FIRSTNAME='" + txtledgername.Text.Trim().ToString() + "'";
            Handler hdn = new Handler();
            DataTable dt = hdn.GetTable(sql);
            personalrelation objpersonalrelation = new personalrelation(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
            objpersonalrelation.personalrelation_SRNO = -1;
            objpersonalrelation.personalrelation_RELATIONSHIPID = General.Parse<int>(dt.Rows[0][0].ToString());
            objpersonalrelation.personalrelation_ASSOSIATEDFEILD = txtfeild.Text.Trim().ToString();
            objpersonalrelation.personalrelation_ASSOSIATEDBRANCH = General.Parse<int>(Session["branchid"].ToString());
            objpersonalrelation.personalrelation_GROUPID = General.Parse<int>(ddlsubgroup.SelectedValue.ToString());
            objpersonalrelation.personalrelation_STATUS = 0;
            check = 1;
            if (objpersonalrelation.Insert(true, "personalrelation"))
            {
                string sqlperrelation = "SELECT MAX(PR.SRNO) AS SRNO FROM personalrelation PR WHERE PR.STATUS=0 AND PR.RELATIONSHIPID=" + dt.Rows[0][0].ToString() + " AND PR.ASSOSIATEDBRANCH=1";
                Handler hdn1 = new Handler();
                DataTable dt1 = hdn1.GetTable(sqlperrelation);

                Handler objopening = new Handler();
                DataTable dtopening = objopening.GetTable("SELECT SRNO,RELATIONSHIPID FROM PERSONALRELATION WHERE ASSOSIATEDBRANCH =1 and ASSOSIATEDFEILD = 'OPENING ACCOUNT'");

                transactiontable objtransaction = new transactiontable(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                objtransaction.transactiontable_SRNO = -1;
                objtransaction.transactiontable_LEDGER1 = General.Parse<int>(dt1.Rows[0]["SRNO"].ToString());
                objtransaction.transactiontable_RELATIONID1 = General.Parse<int>(dt.Rows[0]["RELATIONSHIPID"].ToString());

                objtransaction.transactiontable_LEDGER2 = General.Parse<int>(dtopening.Rows[0]["SRNO"].ToString());
                objtransaction.transactiontable_RELATIONID2 = General.Parse<int>(dtopening.Rows[0]["RELATIONSHIPID"].ToString());

                if (rdcredit.Checked)
                {
                    objtransaction.transactiontable_LTRNTYPE1 = "CR";
                    objtransaction.transactiontable_LTRNTYPE2 = "DR";
                }
                else
                {
                    objtransaction.transactiontable_LTRNTYPE1 = "DR";
                    objtransaction.transactiontable_LTRNTYPE2 = "CR";
                }
                objtransaction.transactiontable_AMOUNT = General.Parse<double>(txtamt.Text.Trim().ToString());
                objtransaction.transactiontable_BRANCHID = General.Parse<int>(Session["branchid"].ToString());
                objtransaction.transactiontable_TRANSDATE = DateTime.Today.ToString("dd/MM/YYYY");
                objtransaction.transactiontable_VOUCHERNO = "1";
                objtransaction.transactiontable_VOUCHERTYPE = "OPENING AMOUNT";
                objtransaction.transactiontable_TRANSACTIONTYPE = 7; //type for opening amount 
                objtransaction.transactiontable_NARRATION = "OPENING AMOUNT";
                objtransaction.transactiontable_STATUS = 0;
                if (objtransaction.Insert(true, "transactiontable"))
                {
                    string sqltransaction = "SELECT MAX(TT.SRNO) AS SRNO FROM transactiontable TT WHERE LEDGER1=" + dt1.Rows[0]["SRNO"].ToString() + " AND TT.LEDGER2=" + dtopening.Rows[0]["SRNO"].ToString() + " AND STATUS=0";
                    Handler hdntransaction = new Handler();
                    DataTable dttransaction = hdntransaction.GetTable(sqltransaction);
                    for(int i=0;i<2;i++)
                    {
                        TransactionDetailsC objtransactiondetails = new TransactionDetailsC(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                        if (i == 0)
                        {
                            objtransactiondetails.transactiondetails_LTRNTYPE = objtransaction.transactiontable_LTRNTYPE1.ToString();
                            objtransactiondetails.transactiondetails_LEDGERID = General.Parse<int>(dt1.Rows[0]["SRNO"].ToString());
                            objtransactiondetails.transactiondetails_RELATIONID = General.Parse<int>(dt.Rows[0]["RELATIONSHIPID"].ToString());
                        }
                        else
                        {
                            objtransactiondetails.transactiondetails_LTRNTYPE = objtransaction.transactiontable_LTRNTYPE2.ToString();
                            objtransactiondetails.transactiondetails_LEDGERID = General.Parse<int>(dtopening.Rows[0]["SRNO"].ToString());
                            objtransactiondetails.transactiondetails_RELATIONID = General.Parse<int>(dtopening.Rows[0]["RELATIONSHIPID"].ToString());
                        }
                        
                        objtransactiondetails.transactiondetails_SRNO = -1;
                        objtransactiondetails.transactiondetails_STATUS = 0;
                      
                        objtransactiondetails.transactiondetails_TRANSDATE = DateTime.Today.ToString("dd/MM/YYYY");
                        objtransactiondetails.transactiondetails_NARRATION = "OPENING AMOUNT";
                        objtransactiondetails.transactiondetails_VOUCHERTYPE = "OPENING AMOUNT";
                        objtransactiondetails.transactiondetails_BRANCHID = General.Parse<int>(Session["branchid"].ToString()); //1;
                        objtransactiondetails.transactiondetails_AMOUNT = General.Parse<double>(txtamt.Text.Trim().ToString());
                        objtransactiondetails.transactiondetails_ASSOCIATELEDGER = General.Parse<int>(dttransaction.Rows[0]["SRNO"].ToString());
                        if (objtransactiondetails.Insert(true, "transactiondetails"))
                        {
                        }
                    }
                    //objtransactiondetails.
                }
            }
        }
        if (check == 1)
        {
            Response.Redirect("ledgerdetails.aspx?id=1");
        }
        else
        {
            MessageBox("Please Enter Proper Value");
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