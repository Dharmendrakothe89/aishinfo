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
using CurrencyConverter;

public partial class ledgerdetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("default.aspx");
            }
            string id = Request.QueryString["id"];
            if (id != null)
            {
                if (Request.QueryString["ID"].ToString() == "1")
                {
                    MessageBox("Ledger Created Successsfully");
                }
               

            }
            GetLedger();
        }
    }
    private void GetLedger()
    {
        string sql = "SELECT PT.RELATIONSHIPID,(PT.FIRSTNAME+' '+CASE WHEN PT.LASTNAME IS NULL THEN ' ' ELSE PT.LASTNAME END ) AS NAME,PR.ASSOSIATEDFEILD,BRANCHNAME FROM PERSONALTABLE PT" +
                   " INNER JOIN PERSONALRELATION PR ON PR.RELATIONSHIPID=PT.RELATIONSHIPID" +
                   " WHERE PR.STATUS=0 AND PT.BRANCHID=" + Session["branchid"].ToString();
        Handler hdn = new Handler();
        DataTable dt = hdn.GetTable(sql);
        ViewState["ledgerdata"] = dt;
        gvledgerlist.DataSource = (DataTable)ViewState["ledgerdata"]; ;
        gvledgerlist.DataBind();
    }
    protected void gvlookup_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvledgerlist.PageIndex = e.NewPageIndex;
        gvledgerlist.DataSource = (DataTable)ViewState["ledgerdata"];
        gvledgerlist.DataBind();
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
    protected void lnkdetails_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        string relationshipid = lnk.CommandArgument.ToString().Trim();
        Response.Redirect("transactiondetails.aspx?RELATIONSHIPID="+relationshipid);
    }
    [System.Web.Script.Services.ScriptMethod]
    [System.Web.Services.WebMethod(EnableSession = true)]
    public static ArrayList GetAutoCompleteData(string username)
    {
        ArrayList result = new ArrayList();
        if (HttpContext.Current.Session["branchid"] != null)
        {
            DataTable dtSelect = new DataTable();
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

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (hdnsecondledger.Value != string.Empty)
        {
            string sql = "SELECT PT.RELATIONSHIPID,(PT.FIRSTNAME+' '+CASE WHEN PT.LASTNAME IS NULL THEN ' ' ELSE PT.LASTNAME END ) AS NAME,PR.ASSOSIATEDFEILD,BRANCHNAME FROM PERSONALTABLE PT" +
                  " INNER JOIN PERSONALRELATION PR ON PR.RELATIONSHIPID=PT.RELATIONSHIPID" +
                  " WHERE PR.STATUS=0 AND PT.BRANCHID=" + Session["branchid"].ToString() + " AND PT.RELATIONSHIPID=" + hdnsecondledger.Value.ToString().Trim();
            Handler hdn = new Handler();
            DataTable dt = hdn.GetTable(sql);
            ViewState["ledgerdata"] = dt;
            gvledgerlist.DataSource = (DataTable)ViewState["ledgerdata"]; ;
            gvledgerlist.DataBind();
        }
        else
        {
            string sql = "SELECT PT.RELATIONSHIPID,(PT.FIRSTNAME+' '+CASE WHEN PT.LASTNAME IS NULL THEN ' ' ELSE PT.LASTNAME END ) AS NAME,PR.ASSOSIATEDFEILD,BRANCHNAME FROM PERSONALTABLE PT" +
                 " INNER JOIN PERSONALRELATION PR ON PR.RELATIONSHIPID=PT.RELATIONSHIPID" +
                 " WHERE PR.STATUS=0 AND PT.BRANCHID=" + Session["branchid"].ToString();
            Handler hdn = new Handler();
            DataTable dt = hdn.GetTable(sql);
            ViewState["ledgerdata"] = dt;
            gvledgerlist.DataSource = (DataTable)ViewState["ledgerdata"]; ;
            gvledgerlist.DataBind();
        }
    }
}