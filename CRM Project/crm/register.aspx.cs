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

public partial class register : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if(Session["downline"] == null)
            {
                string sql = "SELECT SRNO,NAME AS USERNAME,CONCAT(NAME,'-',SEMICODE) AS NAME FROM REGISTRATIONTABLE RT WHERE SRNO=" + Session["relationshipid"].ToString();
                Handler hdn = new Handler();
                DataTable dt = hdn.GetTable(sql);
                
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string sql1 = "SELECT SRNO,NAME AS USERNAME,CONCAT(NAME,'-',SEMICODE) AS NAME FROM REGISTRATIONTABLE RT WHERE SPONSORID=" + dt.Rows[i]["SRNO"].ToString().Trim();
                    Handler hdn1 = new Handler();
                    DataTable dt1 = hdn1.GetTable(sql1);
                    dt.Merge(dt1);
                }
                Session["downline"] = dt;
              
            }
            
        }
    }

    protected void btnaccount_Click(object sender, EventArgs e)
    {
        string sponsorid = string.Empty;
        Handler hdn = new Handler();
        DataTable dt = hdn.GetTable("SELECT SRNO,NAME FROM REGISTRATIONTABLE RT WHERE STATUS=0 AND SEMICODE='" + txtsponsorid.Text.Trim() + "'");

        if (dt.Rows.Count == 1  && dt.Rows[0]["SRNO"].ToString() != string.Empty)
        {
            registrationtable obj = new registrationtable(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
            obj.registrationtable_SRNO = -1;
            obj.registrationtable_NAME = txtname.Text.Trim();
            obj.registrationtable_SEMICODE = txtsemid.Text.Trim();
            obj.registrationtable_PHONENO = txtmobileno.Text.Trim();
            obj.registrationtable_EMAILID = txtemail.Text.Trim();
            if (dt.Rows.Count > 0 && dt.Rows[0]["SRNO"].ToString() != string.Empty)
            {
                obj.registrationtable_SPONSORID = General.Parse<int>(dt.Rows[0]["SRNO"].ToString());
            }
            else
            {
                obj.registrationtable_SPONSORID = -1;
            }
            obj.registrationtable_SPONSORNAME = txtsponsorname.Text.Trim();
            obj.registrationtable_SPONSORSEMICODE = txtsponsorid.Text.Trim();
            obj.registrationtable_STATUS = 1;
            if (obj.Insert(true, "registrationtable"))
            {
                txtname.Text = string.Empty;
                txtsemid.Text = string.Empty;
                txtmobileno.Text = string.Empty;
                txtemail.Text = string.Empty;
                txtsponsorid.Text = string.Empty;
                txtsponsorname.Text = string.Empty;

                MessageBox("You will recieve an E-mail shortly after Admin Approval. Thank You");
            }
            else
            {
                MessageBox("Please provide proper details");
            }
        }
        else
        {
            MessageBox("Please Enter Proper SAMI Id");
        }

    }

    protected void txtsponsorname_TextChanged(object sender, EventArgs e)
    {
        if (txtsponsorname.Text != string.Empty)
        {
            string rel = txtsponsorname.Text.Substring(txtsponsorname.Text.LastIndexOf(',') + 1);
            txtsponsorid.Text = rel;
        }
    }

    [System.Web.Script.Services.ScriptMethod]
    [System.Web.Services.WebMethod(EnableSession = true)]
    public static ArrayList GetAutoCompleteData(string username)
    {
        ArrayList result = new ArrayList();
        if (HttpContext.Current.Session["downline"] != null)
        {
            DataTable dtSelect = new DataTable();
            dtSelect = (DataTable) HttpContext.Current.Session["downline"];
            List<string> list =  (from a in dtSelect.AsEnumerable().Where(a => a["NAME"].ToString().StartsWith(username)) select a.Field<string>("NAME")).ToList();
            //List<string> list = (from row in dtSelect.AsEnumerable().Where(row=>row["NAME"].ToString().StartsWith(username))).ToList(); //select row.Field<string>("NAME")).ToList();

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