using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class changespreference : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("default.aspx");
            }
            FillData();
        }
    }
    private void FillData()
    {
        if (Session["userid"] != null)
        {
            string sqlcmp = "SELECT CM.CMPID,CM.CMPNAME AS COMPANYNAME,CM.REGISTRATIONDATE,CM.ABBRIVATION ,CASE WHEN PM.PREFFERED=0 THEN 'NO' ELSE 'YES' END AS PREFFERENCE" +
                          " FROM PERMISSIONMASTER PM INNER JOIN COMPANYMASTER CM ON CM.CMPID=PM.CMPID" +
                          " WHERE  PM.STATUS=0 AND CM.STATUS=0 AND PM.USERID=" + Session["userid"].ToString() + " GROUP BY CM.CMPID,CM.CMPNAME,CM.REGISTRATIONDATE,CM.ABBRIVATION,PM.PREFFERED";
            Handler hdncmp = new Handler();
            DataTable dtcmp = hdncmp.GetTable(sqlcmp);
            ViewState["companylist"] = dtcmp;
            gvcompanylist.DataSource = (DataTable)ViewState["companylist"];
            gvcompanylist.DataBind();

            int selectedindex = 0;
            string cmpid = string.Empty;
            for (int i = 0; i < dtcmp.Rows.Count; i++)
            {
                if (dtcmp.Rows[i]["PREFFERENCE"].ToString() == "YES")
                {
                    selectedindex = i;
                    cmpid = dtcmp.Rows[i]["CMPID"].ToString();
                    break;
                }
            }
            rdcmplist.DataSource = dtcmp;
            rdcmplist.DataTextField = "COMPANYNAME";
            rdcmplist.DataValueField = "CMPID";
            rdcmplist.DataBind();
            rdcmplist.SelectedIndex = selectedindex;

            string sqlbranch = "SELECT BM.BRANCHID,BM.BRANCHNAME,BM.CITYNAME,CM.CMPID,CM.CMPNAME,CASE WHEN PM.PREFFERED=0 THEN 'NO' ELSE 'YES' END AS PREFFERENCE FROM PERMISSIONMASTER PM" +
                             " INNER JOIN BRANCHMASTER BM ON BM.BRANCHID=PM.BRANCHID INNER JOIN COMPANYMASTER CM ON CM.CMPID=BM.CMPID" +
                             " WHERE  PM.STATUS=0 AND BM.STATUS=0 AND PM.USERID=" + Session["userid"].ToString() + "GROUP BY BM.BRANCHID,BM.BRANCHNAME,BM.CITYNAME,CM.CMPID,CM.CMPNAME,PM.PREFFERED ";
            Handler hdnbranch = new Handler();
            DataTable dtbranch = hdnbranch.GetTable(sqlbranch);
            ViewState["branchlist"] = dtbranch;
            gvbranchlist.DataSource = (DataTable)ViewState["branchlist"];
            gvbranchlist.DataBind();

            DataTable dtbranchnew = dtbranch.Clone();
            int branchselectindex = 0;
            for (int i = 0; i < dtbranch.Rows.Count; i++)
            {
                if (dtbranch.Rows[i]["CMPID"].ToString() == cmpid.ToString())
                {
                    var newDataRow = dtbranchnew.NewRow();
                    newDataRow.ItemArray = dtbranch.Rows[i].ItemArray;
                    dtbranchnew.Rows.Add(newDataRow);
                    if (dtbranch.Rows[i]["PREFFERENCE"].ToString() == "YES")
                    {
                        branchselectindex = dtbranch.Rows.Count - 1;
                    }
                }

            }
            rdbranchlist.DataSource = dtbranchnew;
            rdbranchlist.DataTextField = "BRANCHNAME";
            rdbranchlist.DataValueField = "BRANCHID";
            rdbranchlist.DataBind();
            rdbranchlist.SelectedIndex = branchselectindex;
        }
    }
    protected void gvcompanylist_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvcompanylist.PageIndex = e.NewPageIndex;
        gvcompanylist.DataSource = (DataTable)ViewState["companylist"];
        gvcompanylist.DataBind();
    }
    protected void gvbranchlist_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvbranchlist.PageIndex = e.NewPageIndex;
        gvbranchlist.DataSource = (DataTable)ViewState["branchlist"];
        gvbranchlist.DataBind();
    }

    protected void rdcmplist_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sql = "SELECT BM.BRANCHNAME,BM.BRANCHID,CM.CMPID FROM PERMISSIONMASTER UT INNER JOIN COMPANYMASTER CM ON UT.CMPID=CM.CMPID" +
                   " INNER JOIN BRANCHMASTER BM ON BM.CMPID=CM.CMPID WHERE UT.USERID=1 AND UT.CMPID=" + rdcmplist.SelectedValue.ToString().Trim() + " GROUP BY BM.BRANCHID, BM.BRANCHNAME,CM.CMPID";
        Handler hdn = new Handler();
        DataTable dt = hdn.GetTable(sql);
        rdbranchlist.Items.Clear();
        if (dt.Rows.Count > 0)
        {
            rdcmplist.Items.Clear();
            rdbranchlist.DataSource = dt;
            rdbranchlist.DataTextField = "BRANCHNAME";
            rdbranchlist.DataValueField = "BRANCHID";
            rdbranchlist.DataBind();
            rdbranchlist.SelectedIndex = 0;
        }
        else
        {
            rdcmplist.Items.Clear();
            rdbranchlist.DataSource = null;
            rdbranchlist.DataBind();
        }
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        permissionmaster permission = new permissionmaster(HttpContext.Current.Server.MapPath("~/XML/database.xml"));
        permission.permissionmaster_SRNO = -1;
        permission.permissionmaster_USERID  =-1;
        foreach (ListItem aListItem in rdcmplist.Items)
        {
            if (aListItem.Selected)
            {
                permission.permissionmaster_SRNO=-1;
                permission.permissionmaster_USERID=-1;
                permission.permissionmaster_CMPID = General.Parse<int>(aListItem.Value.ToString().Trim());
                permission.permissionmaster_PREFFERED = 1;
                permission.permissionmaster_BRANCHID = -1;
                permission.permissionmaster_STATUS =-1;
                string condition = "BRANCHID = NULL AND PREFFERED =1 AND STATUS=0 AND USERID ="+Session["userid"].ToString()+" AND CMPID=" + aListItem.Value.ToString().Trim();
                if (permission.Insert(false, "permissionmaster",condition))
                {
                }
            }

        }
    }
}