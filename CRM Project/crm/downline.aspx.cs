using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.Odbc;
using System.IO;
using System.Security.Permissions;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Collections.Specialized;
using System.Net;
using System.Globalization;

public partial class downline : System.Web.UI.Page
{
    DataTable GridData = new DataTable();
    string relationshipIDs = string.Empty;
    int strDownlineCount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            downlinetree.Nodes.Clear();
            DrawTree();
        }
    }
    public void DrawTree()
    {

        downlinetree.Nodes.Clear();
        if (downlinetree.Nodes.Count <= 0)
        {
            DataTable dtDownline = new DataTable();
            TreeNode node1 = new TreeNode();
            string sql = "SELECT RT.SRNO,RT.NAME FROM REGISTRATIONTABLE RT INNER JOIN USERTABLE UT ON UT.RELATIONSHIPID=RT.SRNO WHERE UT.RELATIONSHIPID=" + Session["relationshipid"].ToString();
            Handler hdn = new Handler();
            dtDownline = hdn.GetTable(sql);
            node1.Text = dtDownline.Rows[0]["NAME"].ToString();
            node1.Value = dtDownline.Rows[0]["SRNO"].ToString();
            downlinetree.Nodes.Add(node1);
            DataTable dt = FillGridData(dtDownline.Rows[0]["SRNO"].ToString());
            int i = dt.Rows.Count;
                for (int j = 0; j < i; j++)
                {
                    String name = dt.Rows[j]["NAME"].ToString();
                    String code = dt.Rows[j]["SRNO"].ToString();

                    relationshipIDs += "," + code;

                    TreeNode node = new TreeNode();

                        node.Text = name;
                        node.Value = code;
                        downlinetree.Nodes[0].ChildNodes.Add(node);


                }
                AppendInfraAdvisor();
                downlinetree.ExpandAll();

                ViewState["relations"] = relationshipIDs;

                //LbDownCount.Text = strDownlineCount.ToString();
            }
        }
    private void AppendInfraAdvisor()
    {
        try
        {
            int i = downlinetree.Nodes.Count;
            int j = downlinetree.Nodes[0].ChildNodes.Count;
            for (int z = 0; z < j; z++)
            {
                String code = downlinetree.Nodes[0].ChildNodes[z].Value;
                String name = downlinetree.Nodes[0].ChildNodes[z].ValuePath;
                AddAdvisorChild(code, name);
            }
        }
        catch
        {
            throw;
        }
    }
    private void AddAdvisorChild(String s, String path)
    {
        try
        {
            DataTable dt = new DataTable();

            
            {
                dt = GetDownline(s);
            }

            int k = dt.Rows.Count;
            for (int r = 0; r < k; r++)
            {
                String name = dt.Rows[r]["NAME"].ToString();
                String val = dt.Rows[r]["SRNO"].ToString();

                relationshipIDs += "," + val;

                TreeNode node = new TreeNode();

                
                    node.Text = name;
                    node.Value = val;

                


                TreeNode nd = downlinetree.FindNode(path);
                nd.ChildNodes.Add(node);
                strDownlineCount = strDownlineCount + 1;
                String nextpath = path + "/" + val;
                AddAdvisorChild(val, nextpath);
            }

        }
        catch
        {
            throw;
        }
    }
       

    private DataTable FillGridData(String code)
    {
        try
        {
            GridData.Clear();
            DataTable dtchild = new DataTable();
            {
                
                dtchild = GetDownline(code);
            }
            if (GridData.Rows.Count <= 0)
            {
                GridData = dtchild.Clone();
            }
            foreach (DataRow dr in dtchild.Rows)
            {
                GridData.ImportRow(dr);
            }

            strDownlineCount = GridData.Rows.Count;
            return GridData;
        }
        catch
        {
            throw;
        }
    }
    private DataTable GetDownline(string code)
    {
        string sql1 = "SELECT SRNO,CONCAT(NAME,'-',SEMICODE) AS NAME FROM REGISTRATIONTABLE RT WHERE SPONSORID=" + code;
        Handler hdn1 = new Handler();
        DataTable dt1 = hdn1.GetTable(sql1);
        return dt1;
    }
}