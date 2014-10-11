using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class bankbook : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("default.aspx");
            }
            string today = DateTime.Now.ToString("dd/MM/yyyy");
            txtfromdate.Text = today;
            txttodate.Text = today;
            FillBank();
            FillData(today, today,"");
        }
    }
    private void FillBank()
    {
        string sql = "SELECT PR1.SRNO,PR1.ASSOSIATEDFEILD FROM PERSONALRELATION PR1 INNER JOIN PERSONALTABLE PT1 ON PT1.RELATIONSHIPID=PR1.RELATIONSHIPID " +
                   " WHERE PT1.FIRSTNAME='BANK ACCOUNT' AND PT1.BRANCHID=" + Session["branchid"].ToString() + " GROUP BY PR1.SRNO,PR1.ASSOSIATEDFEILD";
        Handler hdn = new Handler();
        DataTable dt = hdn.GetTable(sql);
        ddlbank.DataSource = dt;
        ddlbank.DataTextField = "ASSOSIATEDFEILD";
        ddlbank.DataValueField = "SRNO";
        ddlbank.DataBind();
        ListItem li=new ListItem();
        li.Text="All";
        li.Value="0";
        ddlbank.Items.Insert(0, li);

    }
    private void FillData(string fromdate, string todate,string bankledger)
    {
        string sqlbank = string.Empty;
        if (bankledger == "")
        {
            sqlbank = "SELECT SRNO FROM PERSONALRELATION PR1 INNER JOIN PERSONALTABLE PT1 ON PT1.RELATIONSHIPID=PR1.RELATIONSHIPID WHERE PT1.FIRSTNAME='BANK ACCOUNT' AND" +
                       " PT1.BRANCHID=" + Session["branchid"].ToString() + " ";
        }
        else
        {
            sqlbank = bankledger;
        }
        string sql = "SELECT PRB.ASSOSIATEDFEILD AS BANKLEDGER,PT.FIRSTNAME+' '+CASE WHEN PT.MIDDLENAME IS NULL THEN '' ELSE PT.MIDDLENAME END +' '+CASE WHEN PT.LASTNAME IS NULL THEN '' ELSE PT.LASTNAME END +'('+PR.ASSOSIATEDFEILD+')' AS LEDGER," +
                   " TT.AMOUNT,TT.VOUCHERTYPE,TT.TRANSDATE,TT.NARRATION,TT.LTRNTYPE2 AS LTRNTYPE FROM TRANSACTIONTABLE TT"+
                   " INNER JOIN PERSONALRELATION PRB ON PRB.SRNO=TT.LEDGER1 INNER JOIN PERSONALTABLE PTB ON PTB.RELATIONSHIPID=PRB.RELATIONSHIPID "+
                   " INNER JOIN PERSONALRELATION PR ON PR.SRNO=TT.LEDGER2" +
                   " INNER JOIN PERSONALTABLE PT ON PT.RELATIONSHIPID=PR.RELATIONSHIPID WHERE TT.BRANCHID =" + Session["branchid"].ToString() + " AND TT.STATUS=0 " +
                  " AND (convert(datetime, TT.TRANSDATE, 103) BETWEEN  convert(datetime, '" + fromdate.Trim().ToString() + "', 103) AND convert(datetime, '" + todate.Trim().ToString() + "', 103)) AND" +
                   " TT.LEDGER1 IN ("+sqlbank+" ) AND (TT.TRANSACTIONTYPE=3 OR TT.TRANSACTIONTYPE=4)";
        Handler hdn = new Handler();
        DataTable dt = hdn.GetTable(sql);
        string value = GetOpeningAmount(fromdate,bankledger);
        DataRow newrow = dt.NewRow();

        string[] columnarr = value.Split('~');
        dt.Rows.InsertAt(newrow, 0);
        dt.Rows[0]["LEDGER"] = "OPENING AMOUNT";
        dt.Rows[0]["AMOUNT"] = columnarr[0].ToString();
        dt.Rows[0]["LTRNTYPE"] = columnarr[1].ToString();

        DataTable dtcash = GetBalance(dt, "AMOUNT", "LTRNTYPE");
        ViewState["bankbook"] = dtcash;

        if (dt.Rows.Count > 0)
        {
            gvbankbook.DataSource = (DataTable)ViewState["bankbook"];
            gvbankbook.DataBind();
        }
        else
        {
            gvbankbook.DataSource = null;
            gvbankbook.DataBind();
        }
    }
    private string GetOpeningAmount(string fromdate, string bankledger)
    {
        double credit = 0;
        double debit = 0;
        double amount = 0;
        string ltrntype = "Cr";
        string sqlbank = string.Empty;
        if (bankledger == "")
        {
            sqlbank = "SELECT SRNO FROM PERSONALRELATION PR1 INNER JOIN PERSONALTABLE PT1 ON PT1.RELATIONSHIPID=PR1.RELATIONSHIPID WHERE PT1.FIRSTNAME='BANK ACCOUNT' AND" +
                          " PT1.BRANCHID=" + Session["branchid"].ToString();
        }
        else
        {
            sqlbank = bankledger;
        }

        string sqlopening1 = "SELECT CASE WHEN SUM(TT.AMOUNT)IS NULL THEN 0 ELSE SUM(TT.AMOUNT) END AS AMOUNT FROM TRANSACTIONTABLE TT" +
                          " INNER JOIN PERSONALRELATION PR ON PR.SRNO=TT.LEDGER2 INNER JOIN PERSONALTABLE PT ON PT.RELATIONSHIPID=PR.RELATIONSHIPID" +
                          " WHERE TT.BRANCHID =" + Session["branchid"].ToString() + " AND TT.STATUS=0 AND TT.TRANSDATE < '" + fromdate + "' AND" +
                          " TT.LEDGER1 IN ("+sqlbank+" ) AND TT.LTRNTYPE2='Cr'";

        Handler hdnopening1 = new Handler();
        DataTable dtopening1 = hdnopening1.GetTable(sqlopening1);
        if (dtopening1.Rows.Count > 0)
        {
            credit = General.Parse<double>(dtopening1.Rows[0][0].ToString());
        }

        string sqlopening2 = "SELECT CASE WHEN SUM(TT.AMOUNT)IS NULL THEN 0 ELSE SUM(TT.AMOUNT) END AS AMOUNT FROM TRANSACTIONTABLE TT" +
                           " INNER JOIN PERSONALRELATION PR ON PR.SRNO=TT.LEDGER2 INNER JOIN PERSONALTABLE PT ON PT.RELATIONSHIPID=PR.RELATIONSHIPID" +
                           " WHERE TT.BRANCHID =" + Session["branchid"].ToString() + " AND TT.STATUS=0 AND TT.TRANSDATE < '" + fromdate + "' AND" +
                           " TT.LEDGER1 IN (" + sqlbank + " ) AND TT.LTRNTYPE2='Dr'";
        Handler hdnopening2 = new Handler();
        DataTable dtopening2 = hdnopening2.GetTable(sqlopening2);
        if (dtopening2.Rows.Count > 0)
        {
            debit = General.Parse<double>(dtopening2.Rows[0][0].ToString());
        }
        if (credit > debit)
        {
            amount = credit - debit;
            ltrntype = "Cr";
        }
        else if (credit < debit)
        {
            amount = debit - credit;
            ltrntype = "Dr";
        }

        return (amount.ToString() + "~" + ltrntype);
    }
    private DataTable GetBalance(DataTable dtdata, string column, string ltypecolumn)
    {
        try
        {
            dtdata.Columns.Add("BALANCE");
            for (int i = 0; i < dtdata.Rows.Count; i++)
            {
                if (i == 0)
                {
                    dtdata.Rows[i]["BALANCE"] = dtdata.Rows[i][column].ToString() + " " + dtdata.Rows[i][ltypecolumn].ToString();
                }
                else
                {
                    string val = dtdata.Rows[i - 1]["BALANCE"].ToString().Split(' ')[0].ToString();
                    string ltype = dtdata.Rows[i - 1]["BALANCE"].ToString().Split(' ')[1].ToString();
                    if (ltype == dtdata.Rows[i][ltypecolumn].ToString())
                    {
                        dtdata.Rows[i]["BALANCE"] = General.Parse<double>(val) + General.Parse<double>(dtdata.Rows[i][column].ToString()) + " " + ltype;
                    }
                    else if (General.Parse<double>(dtdata.Rows[i][column].ToString()) > General.Parse<double>(val))
                    {
                        dtdata.Rows[i]["BALANCE"] = General.Parse<double>(dtdata.Rows[i][column].ToString()) - General.Parse<double>(val) + " " + dtdata.Rows[i][ltypecolumn].ToString();
                    }
                    else if (General.Parse<double>(dtdata.Rows[i][column].ToString()) < General.Parse<double>(val))
                    {
                        dtdata.Rows[i]["BALANCE"] = General.Parse<double>(val) - General.Parse<double>(dtdata.Rows[i][column].ToString()) + " " + ltype;
                    }
                    else
                    {
                        dtdata.Rows[i]["BALANCE"] = "0" + " " + ltype;
                    }
                }
            }
            return dtdata;
        }
        catch
        {
            throw;
        }
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        if (ddlbank.SelectedIndex > 0)
        {
            FillData(txtfromdate.Text.Trim().ToString(), txttodate.Text.Trim().ToString(), ddlbank.SelectedValue.ToString().Trim());
        }
        else
        {
            FillData(txtfromdate.Text.Trim().ToString(), txttodate.Text.Trim().ToString(), "");
        }
    }
    protected void gvbankbook_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvbankbook.PageIndex = e.NewPageIndex;
        gvbankbook.DataSource = (DataTable)ViewState["bankbook"];
        gvbankbook.DataBind();
    }
}