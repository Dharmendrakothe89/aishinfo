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

public partial class createvoucher : System.Web.UI.Page
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
        string sql = "SELECT RTRIM(PM.FIRSTNAME)+'('+ RTRIM(PM.BRANCHNAME)+')' AS LEDGER,PR.RELATIONSHIPID,SRNO FROM personaltable PM INNER JOIN personalrelation PR ON PM.RELATIONSHIPID=PR.RELATIONSHIPID" +
                   " WHERE PM.FIRSTNAME='CASH ACCOUNT' AND PM.BRANCHID=" + Session["branchid"].ToString();
        Handler hdn = new Handler();
        DataTable dt = hdn.GetTable(sql);
        if (dt.Rows.Count > 0)
        {
            txtledgername.Text = dt.Rows[0]["LEDGER"].ToString();
            hdnfirstledger.Value = dt.Rows[0]["RELATIONSHIPID"].ToString();
            ViewState["led1"] = General.Parse<int>(dt.Rows[0]["SRNO"].ToString());
            ViewState["trans1"] = dt.Rows[0]["RELATIONSHIPID"].ToString();
        }
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


    //protected void txtsecondledger_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (txtsecondledger.Text != string.Empty)
    //        {
    //            Handler objh = new Handler();
    //            DataTable dt = new DataTable();
    //            string rel = string.Empty;
    //            if (txtsecondledger.Text.Contains('~'))
    //            {
    //                rel = txtsecondledger.Text.Substring(txtsecondledger.Text.LastIndexOf('~') + 1).Trim();
                   
    //            }
    //            if (rel != string.Empty)
    //            {

    //                string SQL = "SELECT PR.SRNO,P.RELATIONSHIPID,ASSOSIATEDFEILD,P.BRANCHNAME,AG.GROUPNAME"+
    //                             " FROM personaltable P  INNER JOIN personalrelation PR ON P.RELATIONSHIPID = PR.RELATIONSHIPID "+
    //                             " INNER JOIN ACCOUNTGROUP AG ON AG.GROUPID = PR.GROUPID WHERE PR.RELATIONSHIPID =" + rel;

    //                dt = objh.GetTable(SQL);
    //                hdnSRNO_REL2.Value = dt.Rows[0]["RELATIONSHIPID"].ToString();
    //                ViewState["trans2"] = dt.Rows[0]["RELATIONSHIPID"].ToString();
    //                ViewState["led2"] =General.Parse<int>( dt.Rows[0]["SRNO"].ToString());
    //                if (dt.Rows.Count > 0)
    //                {
    //                    ddlfeild.DataSource = dt;
    //                    ddlfeild.DataTextField="ASSOSIATEDFEILD";
    //                    ddlfeild.DataValueField="RELATIONSHIPID";
    //                    ddlfeild.DataBind();
    //                    ddlfeild.Items.Insert(0, "-- Feild --");
    //                    ddlfeild.Enabled = true;
    //                }
    //                else
    //                {
    //                    ddlfeild.DataSource = null;
    //                    ddlfeild.DataBind();
    //                    ddlfeild.SelectedIndex = 0;
    //                    ddlfeild.Enabled = false;
    //                }
    //            }
    //        }
    //        else
    //        {
    //            ddlfeild.DataSource = null;
    //            ddlfeild.DataBind();

    //            ddlfeild.SelectedIndex = 0;
    //            ddlfeild.Enabled = false;
    //        }
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg1", "PerformPostBack();", true);
            
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}


    ////--------AutoComplete Service Method-------------
    //[System.Web.Services.WebMethod]
    //[System.Web.Script.Services.ScriptMethod]
    //public static System.Collections.ArrayList GetLedgerList(string prefixText, int count, string contextKey)
    //{
    //    try
    //    {
    //        System.Collections.ArrayList result = new System.Collections.ArrayList();
    //        string SQL = string.Empty;
    //        if (contextKey != null && contextKey.Trim().ToString() != string.Empty )
    //        {

    //            SQL = "SELECT p.RELATIONSHIPID,ASSOSIATEDFEILD,P.FIRSTNAME+' '+CASE WHEN P.MIDDLENAME != NULL THEN P.MIDDLENAME ELSE '' END +' '+CASE WHEN P.LASTNAME != NULL THEN P.LASTNAME ELSE '' END AS NAME FROM personaltable P" +
    //               " left outer JOIN personalrelation PR ON P.RELATIONSHIPID = PR.RELATIONSHIPID" +
    //               " WHERE p.FIRSTNAME <> 'OPENING ACCOUNT' AND (PR.ASSOCIATEBRANCH = " + contextKey + " OR P.BRANCHID = " + contextKey + " ) AND ((upper(CONCAT(REPLACE(p.FIRSTNAME,' ',''),' ',p.MIDDLENAME ,' ',p.LASTNAME))" +
    //               " LIKE upper('" + prefixText + "%') OR  upper(REPLACE(p.FIRSTNAME,' ','')+' '+p.MIDDLENAME+' '+p.LASTNAME)" +
    //               " LIKE upper('%" + prefixText + "') OR  upper(p.FIRSTNAME+' '+p.MIDDLENAME +' '+p.LASTNAME)" +
    //               " LIKE upper('%" + prefixText + "%')) OR upper(CONCAT(REPLACE(p.FIRSTNAME,' ',''),' ',REPLACE(p.LASTNAME,' ',''))) LIKE upper('%" + prefixText + "%')"+
    //               " OR REPLACE(ASSOSIATEDFEILD,' ','') LIKE UPPER('%" + prefixText + "%') OR UPPER(ASSOSIATEDFEILD) LIKE UPPER('%" + prefixText + "%'))";
    //        }
    //        else
    //        {

    //            SQL = "SELECT P.branchid,P.branchname,P.RELATIONSHIPID,ASSOSIATEDFEILD,P.FIRSTNAME+' '+CASE WHEN P.MIDDLENAME != NULL THEN P.MIDDLENAME ELSE '' END +' '+CASE WHEN P.LASTNAME != NULL THEN P.LASTNAME ELSE '' END AS NAME FROM personaltable P" +
    //               " left outer JOIN personalrelation PR ON P.RELATIONSHIPID = PR.RELATIONSHIPID" +
    //               " WHERE p.FIRSTNAME NOT LIKE 'OPENING ACCOUNT%' AND "+
    //               " upper(REPLACE(p.FIRSTNAME,' ','')+' '+p.MIDDLENAME+' '+p.LASTNAME) LIKE upper('" + prefixText + "%') OR"+
    //               " upper(REPLACE(p.FIRSTNAME,' ','')+' '+p.MIDDLENAME+' '+p.LASTNAME) LIKE upper('%" + prefixText + "') OR" +
    //               " upper(REPLACE(p.FIRSTNAME,' ','')+' '+p.MIDDLENAME+' '+p.LASTNAME) LIKE upper('%" + prefixText + "&') OR" +
    //               " REPLACE(ASSOSIATEDFEILD,' ','') LIKE UPPER('%" + prefixText + "%') OR UPPER(ASSOSIATEDFEILD) LIKE UPPER('%" + prefixText + "%')";
    //        }


    //        Handler objhandler = new Handler();
    //        DataTable dt = new DataTable();
    //        dt = objhandler.GetTable(SQL);

    //        for (int i = 0; i < dt.Rows.Count; i++)
    //        {
    //            try
    //            {
    //                if (contextKey != null && contextKey != string.Empty)
    //                {
    //                    result.Add(dt.Rows[i]["NAME"].ToString() + "~" + dt.Rows[i]["RELATIONSHIPID"].ToString());
    //                }
    //                else
    //                {
    //                    result.Add(dt.Rows[i]["NAME"].ToString() +"~" + dt.Rows[i]["RELATIONSHIPID"].ToString());
    //                }

    //            }
    //            catch (Exception ex)
    //            {
    //                throw ex;
    //            }
    //        }



    //        return result;

    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

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
            string sql="SELECT (RTRIM(PT.FIRSTNAME)+' '+CASE WHEN PT.LASTNAME IS NULL THEN '' ELSE RTRIM(PT.LASTNAME) END +'~'+ CAST(RTRIM(PT.RELATIONSHIPID) AS VARCHAR(10))  ) AS NAME FROM PERSONALTABLE PT"+
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
    protected void btnsubmit_Click(object sender, EventArgs e)
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
        if (ViewState["led1"].ToString() != string.Empty && ViewState["led2"].ToString() != string.Empty)
        {
        transactiontable trans = new transactiontable(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
        trans.transactiontable_SRNO = -1;
        trans.transactiontable_LEDGER1 = General.Parse<int>(ViewState["led1"].ToString());
        trans.transactiontable_RELATIONID1 = General.Parse<int>(ViewState["trans1"].ToString()); 
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
        trans.transactiontable_AMOUNT=General.Parse<double>(txtamount.Text);
        trans.transactiontable_NARRATION = txtnarration.Text;
        trans.transactiontable_TRANSDATE = DateTime.Today.ToString("dd/MM/YYYY");
        trans.transactiontable_TRANSACTIONTYPE = General.Parse<int>(ddlvouchertype.SelectedValue);
        trans.transactiontable_VOUCHERTYPE = ddlvouchertype.SelectedItem.Text;
        trans.transactiontable_BRANCHID = General.Parse<int>(Session["branchid"].ToString()); 
        trans.transactiontable_STATUS=0;

        if (trans.Insert(true, "transactiontable"))
                {
                    string sqltransaction = "SELECT MAX(TT.SRNO) AS SRNO FROM transactiontable TT WHERE LEDGER1=" + General.Parse<int>(ViewState["led1"].ToString()) +" AND TT.LEDGER2=" + General.Parse<int>(ViewState["led2"].ToString()) +" AND STATUS=0";
                    Handler hdntransaction = new Handler();
                    DataTable dttransaction = hdntransaction.GetTable(sqltransaction);
                    for(int i=0;i<2;i++)
                    {
                        TransactionDetailsC objtransactiondetails = new TransactionDetailsC(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
                        if (i == 0)
                        {
                            objtransactiondetails.transactiondetails_LTRNTYPE = trans.transactiontable_LTRNTYPE1.ToString();
                            objtransactiondetails.transactiondetails_LEDGERID = General.Parse<int>(ViewState["led1"].ToString());
                            objtransactiondetails.transactiondetails_RELATIONID = General.Parse<int>(ViewState["trans1"].ToString());
                        }
                        else
                        {
                            objtransactiondetails.transactiondetails_LTRNTYPE = trans.transactiontable_LTRNTYPE2.ToString();
                            objtransactiondetails.transactiondetails_LEDGERID = General.Parse<int>(ViewState["led2"].ToString());
                            objtransactiondetails.transactiondetails_RELATIONID =  General.Parse<int>(ViewState["trans2"].ToString());
                        }
                        
                        objtransactiondetails.transactiondetails_SRNO = -1;
                        objtransactiondetails.transactiondetails_STATUS = 0;
                       
                        objtransactiondetails.transactiondetails_TRANSDATE = DateTime.Today.ToString("dd/MM/YYYY");
                        objtransactiondetails.transactiondetails_NARRATION = txtnarration.Text;
                        objtransactiondetails.transactiondetails_VOUCHERTYPE = ddlvouchertype.SelectedItem.Text;
                        objtransactiondetails.transactiondetails_BRANCHID =  General.Parse<int>(Session["branchid"].ToString()); //1;
                        objtransactiondetails.transactiondetails_AMOUNT = General.Parse<double>(txtamount.Text); //General.Parse<double>(txtamt.Text.Trim().ToString());
                        objtransactiondetails.transactiondetails_ASSOCIATELEDGER = General.Parse<int>(dttransaction.Rows[0]["SRNO"].ToString());
                        if (objtransactiondetails.Insert(true, "transactiondetails"))
                        {
                        }
                    }
                    MessageBox("Voucher Added Successfully");
                }
    }
        txtsecondledger.Text = string.Empty;
        txtamount.Text = string.Empty;
        txtamtword.Text = string.Empty;
        txtnarration.Text = string.Empty;
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
