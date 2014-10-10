using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class masterpage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Session["userid"] != null && Session["userid"].ToString() != string.Empty)
            {
                ShowMenu();
                string sql = "SELECT NAME FROM usertable UM WHERE SRNO=" + Session["userid"].ToString();
                Handler hdn = new Handler();
                DataTable dt = hdn.GetTable(sql);
                lblusername.Text = dt.Rows[0]["NAME"].ToString().Trim();
            }
            else
            {
                Response.Redirect("default.aspx");
            }
        }
    }
    private void ShowMenu()
    {
        Handler hd = new Handler();
        string rights = " select mt.menuid,mt.menuname,mt.menutype ,mt.mainmenu from menumaster mt  inner join  userrights ur on " +
            "ur.menuid=mt.menuid inner join userrolemaster  urm on urm.srno=ur.roleid " +
            "where urm.userdesignation='" + Session["designation"].ToString() + "' and mt.status=0 and menutype='menu' order by mt.menutype";
        DataTable dtrights = hd.GetTable(rights);


        string subrights = " select mt.menuid,mt.menuname,mt.menutype ,mt.mainmenu,mm.menuname as mainmaenuname" +
       " from menumaster mt  inner join  userrights ur on ur.menuid=mt.menuid inner join userrolemaster  urm on urm.srno=ur.roleid " +
       " inner join menumaster mm on mt.mainmenu=mm.menuid where urm.userdesignation='" + Session["designation"].ToString() + "' and mt.status=0 order by mt.menutype";

        DataTable dtsubrights = hd.GetTable(subrights);
        string main = string.Empty;
        string menu = string.Empty;
        rights = string.Empty;
        foreach (DataRow dr in dtrights.Rows)
        {
            rights += dr["menuname"] + ",";

        }

        foreach (DataRow dr in dtsubrights.Rows)
        {
            menu += dr["menuname"] + ",";
            main += dr["mainmaenuname"] + ",";

        }
        hdnrights.Value = rights;
        hdnmenu.Value = menu;
        hdnmainmenu.Value = main;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ScriptRegistration", "hidediv();", true);
    }
    protected void lnklogout_Click(object sender, EventArgs e)
    {
        Session.Remove("userid");
        Session.Remove("branchid");
        Session.Remove("cmpid");
        Response.Redirect("default.aspx");

    }
}
