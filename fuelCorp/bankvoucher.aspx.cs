using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.Odbc;
using System.IO;
using System.Collections.Generic;

public partial class bankvoucher : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("default.aspx");
            }
            txtdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            FillLedger();
        }
    }
    private void FillLedger()
    {
        string sql = "SELECT CAST(PR.SRNO AS varchar(18)) + '$'+ CAST(PR.RELATIONSHIPID AS varchar(18)) AS SRNO_RELATION,PR.ASSOSIATEDFEILD FROM PERSONALTABLE PT"+
                     " INNER JOIN PERSONALRELATION PR ON PT.RELATIONSHIPID = PR.RELATIONSHIPID WHERE PT.FIRSTNAME='BANK ACCOUNT' AND PR.ASSOSIATEDFEILD <> 'CHEQUE IN HAND' AND PR.ASSOSIATEDBRANCH=" + Session["branchid"].ToString();
        Handler hdn = new Handler();
        DataTable dt = hdn.GetTable(sql);

        ddlbank.DataSource = dt;
        ddlbank.DataTextField = "ASSOSIATEDFEILD";
        ddlbank.DataValueField = "SRNO_RELATION";
        ddlbank.DataBind();
        ddlbank.Items.Insert(0, "-- Select Bank--");

    }
    protected void ddlvouchertype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlvouchertype.SelectedValue == "1")
        {
            ddlType.SelectedValue = "1";
        }
        else if (ddlvouchertype.SelectedValue == "2")
        {
            ddlType.SelectedValue = "2";
        }
    }

    protected void txtamount_TextChanged(object sender, EventArgs e)
    {
        if (this.txtamount.Text.Length > 0)
        {
            CurrencyConverter.Class1 cs = new CurrencyConverter.Class1();
            txtamtword.Text = cs.RupeesToWord(Convert.ToDouble(txtamount.Text)).ToString();

        }
    }
  
    [System.Web.Script.Services.ScriptMethod]
    [System.Web.Services.WebMethod(EnableSession = true)]
    public static ArrayList GetAutoCompleteData(string username)
    {
        ArrayList result = new ArrayList();
        if (HttpContext.Current.Session["branchid"] != null)
        {
            DataTable dtSelect = new DataTable();
            //dtSelect = (DataTable)HttpContext.Current.Session["downline"];
            //List<string> list = (from a in dtSelect.AsEnumerable().Where(a => a["NAME"].ToString().StartsWith(username)) select a.Field<string>("NAME")).ToList();
            ////List<string> list = (from row in dtSelect.AsEnumerable().Where(row=>row["NAME"].ToString().StartsWith(username))).ToList(); //select row.Field<string>("NAME")).ToList();
            string sql = "SELECT (RTRIM(PT.FIRSTNAME)+' '+CASE WHEN PT.LASTNAME IS NULL THEN '' ELSE RTRIM(PT.LASTNAME) END +'~'+ CAST(RTRIM(PT.RELATIONSHIPID) AS VARCHAR(10))  ) AS NAME FROM PERSONALTABLE PT" +
                       " INNER JOIN PERSONALRELATION PR ON PR.RELATIONSHIPID=PT.RELATIONSHIPID WHERE PR.STATUS=0 AND PT.BRANCHID=" + HttpContext.Current.Session["branchid"].ToString() + "" +
                       " AND PT.FIRSTNAME LIKE '" + username.ToUpper().Trim() + "%'";
            Handler hdn = new Handler();
            dtSelect = hdn.GetTable(sql);
            foreach (DataRow dr in dtSelect.Rows)
            {
                if (dr["NAME"].ToString().ToLower().StartsWith(username))
                {
                    result.Add(dr["NAME"]);
                }
            }
        }
        return result;
    }
    public void btnsubmit_Click(object sender, EventArgs e)
    {
        ViewState["trans2"] = string.Empty;
        ViewState["led2"] = string.Empty;
        string SQL = "SELECT PR.SRNO,P.RELATIONSHIPID,ASSOSIATEDFEILD,P.BRANCHNAME,AG.GROUPNAME" +
                     " FROM personaltable P  INNER JOIN personalrelation PR ON P.RELATIONSHIPID = PR.RELATIONSHIPID " +
                     " INNER JOIN ACCOUNTGROUP AG ON AG.GROUPID = PR.GROUPID WHERE PR.RELATIONSHIPID =" + hdnsecondledger.Value;
        Handler hdns = new Handler();
        DataTable dts = hdns.GetTable(SQL);
        if (dts.Rows.Count > 0)
        {
            ViewState["trans2"] = dts.Rows[0]["RELATIONSHIPID"].ToString();
            ViewState["led2"] = General.Parse<int>(dts.Rows[0]["SRNO"].ToString());
        }

        string[] ids;
        transactiontable trans = new transactiontable(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
        trans.transactiontable_SRNO = -1;
        string id=ddlbank.SelectedValue.Replace("$"," ");
     ids=  id.Split(' ');
        trans.transactiontable_LEDGER1 = General.Parse<int>(ids[0]);
        trans.transactiontable_RELATIONID1 = General.Parse<int>(ids[1]);
        trans.transactiontable_LEDGER2 = General.Parse<int>(ViewState["led2"].ToString());
        trans.transactiontable_RELATIONID2 = General.Parse<int>(ViewState["trans2"].ToString());
        if (ddlType.SelectedItem.Text == "credit")
        {
            trans.transactiontable_LTRNTYPE1 = "CR";
            trans.transactiontable_LTRNTYPE2 = "DR";
        }
        else
        {
            trans.transactiontable_LTRNTYPE1 = "DR";
            trans.transactiontable_LTRNTYPE2 = "CR";
        }
        trans.transactiontable_AMOUNT = General.Parse<double>(txtamount.Text);
        trans.transactiontable_NARRATION = txtnarration.Text;
        trans.transactiontable_TRANSDATE = txtdate.Text;
        trans.transactiontable_TRANSACTIONTYPE = General.Parse<int>(ddlvouchertype.SelectedValue);
        trans.transactiontable_VOUCHERTYPE = ddlvouchertype.SelectedItem.Text;
        trans.transactiontable_BRANCHID = General.Parse<int>(Session["branchid"].ToString()); 
        trans.transactiontable_STATUS = 0;

        if (trans.Insert(true, "transactiontable"))
        {
            string sqltransaction = "SELECT MAX(TT.SRNO) AS SRNO FROM transactiontable TT WHERE LEDGER1=" + ids[0] + " AND TT.LEDGER2=" + General.Parse<int>(ViewState["led2"].ToString()) + " AND STATUS=0";
            Handler hdntransaction = new Handler();
            DataTable dttransaction = hdntransaction.GetTable(sqltransaction);
            for (int i = 0; i < 2; i++)
            {
                transactiondetails objtransactiondetails = new transactiondetails(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                if (i == 0)
                {
                    objtransactiondetails.transactiondetails_LTRNTYPE = trans.transactiontable_LTRNTYPE1.ToString();
                    objtransactiondetails.transactiondetails_LEDGERID = General.Parse<int>( ids[0]);
                    objtransactiondetails.transactiondetails_RELATIONID =General.Parse<int>(  ids[1]);
                }
                else
                {
                    objtransactiondetails.transactiondetails_LTRNTYPE = trans.transactiontable_LTRNTYPE2.ToString();
                    objtransactiondetails.transactiondetails_LEDGERID = General.Parse<int>(ViewState["led2"].ToString());
                    objtransactiondetails.transactiondetails_RELATIONID = General.Parse<int>(ViewState["trans2"].ToString());
                }

                objtransactiondetails.transactiondetails_SRNO = -1;
                objtransactiondetails.transactiondetails_STATUS = 0;

                objtransactiondetails.transactiondetails_TRANSDATE = DateTime.Today.ToString("dd/MM/YYYY");
                objtransactiondetails.transactiondetails_NARRATION = txtnarration.Text;
                objtransactiondetails.transactiondetails_VOUCHERTYPE = ddlvouchertype.SelectedItem.Text;
                objtransactiondetails.transactiondetails_BRANCHID = General.Parse<int>(Session["branchid"].ToString()); //1;
                objtransactiondetails.transactiondetails_AMOUNT = General.Parse<double>(txtamount.Text); //General.Parse<double>(txtamt.Text.Trim().ToString());
                objtransactiondetails.transactiondetails_ASSOCIATELEDGER = General.Parse<int>(dttransaction.Rows[0]["SRNO"].ToString());
                if (objtransactiondetails.Insert(true, "transactiondetails"))
                {
                }
            }
            //objtransactiondetails.
        }

    }


}