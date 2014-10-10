using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;
using System.Data.Odbc;


/// <summary>
/// Summary description for ScrollDataGrid
/// </summary>
public class ScrollDataGrid : ITemplate
{
    Label lbl;
    LinkButton lkbtn;
    BoundField mainbf;
    string colname;
    ListItemType itemtype;
    int colno;
    string relationid;

    public ScrollDataGrid()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public ScrollDataGrid(Label lblcol, LinkButton lkbtncol, string colnameval, int colnoval, ListItemType itype)
    {
        lbl = lblcol;
        lkbtn = lkbtncol;
        colname = colnameval;
        colno = colnoval;
        itemtype = itype;
    }
    public ScrollDataGrid(Label lblcol, string colnameval, int colnoval, ListItemType itype)
    {
        lbl = lblcol;
        colname = colnameval;
        colno = colnoval;
        itemtype = itype;
    }
    //public ScrollDataGrid(Label lblcol, string colnameval, int colnoval)
    //{
    //    lbl = lblcol;
    //    colname = colnameval;
    //    colno = colnoval;

    //}

    public void InstantiateIn(System.Web.UI.Control container)
    {
        try
        {
            switch (itemtype)
            {
                case ListItemType.Header:
                    if (colno == 2)
                    {
                        Label lbl2 = new Label();
                        lbl2.Text = colname;
                        container.Controls.Add(lbl2);
                    }
                    if (colno == 7)
                    {
                        Label lblcredit = new Label();
                        lblcredit.Text = colname.ToUpper();
                        container.Controls.Add(lblcredit);
                    }
                    if (colno == 8)
                    {
                        Label lbldebit = new Label();
                        lbldebit.Text = colname.ToUpper();
                        container.Controls.Add(lbldebit);
                    }
                    break;
                case ListItemType.Item:
                    if (colno == 0)
                    {
                        Label lbl1 = new Label();
                        lbl1.DataBinding += new EventHandler(lbl1_DataBinding);
                        container.Controls.Add(lbl1);
                    }
                    if (colno == 2)
                    {

                        Label lbl2 = new Label();
                        lbl2.DataBinding += new EventHandler(lbl2_DataBinding);

                        LinkButton lnk = new LinkButton();
                        lnk.DataBinding += new EventHandler(lnk_DataBinding);

                        container.Controls.Add(lbl2);
                        container.Controls.Add(lnk);
                    }
                    if (colno == 7)
                    {
                        Label lblcredit = new Label();
                        lblcredit.DataBinding += new EventHandler(lblcredit_DataBinding);
                        container.Controls.Add(lblcredit);
                    }
                    if (colno == 8)
                    {
                        Label lbldebit = new Label();
                        lbldebit.DataBinding += new EventHandler(lbldebit_DataBinding);
                        container.Controls.Add(lbldebit);
                    }

                    break;
            }
        }
        catch
        {
            throw;
        }
    }

    protected void lblcredit_DataBinding(object sender, EventArgs e)
    {
        try
        {
            Label labelcredit = (Label)sender;

            GridViewRow containerr = (GridViewRow)labelcredit.NamingContainer;
            object dataValue = DataBinder.Eval(containerr.DataItem, colname);
            if (dataValue != DBNull.Value)
            {
                labelcredit.ID = "lblCredit";
                if (containerr.Cells[5].Text == "CR")
                    labelcredit.Text = dataValue.ToString();

            }
        }
        catch
        {
            throw;
        }
    }

    protected void lbldebit_DataBinding(object sender, EventArgs e)
    {
        try
        {
            Label labeldebit = (Label)sender;

            GridViewRow containerr = (GridViewRow)labeldebit.NamingContainer;
            object dataValue = DataBinder.Eval(containerr.DataItem, colname);
            if (dataValue != DBNull.Value)
            {
                labeldebit.ID = "lblDebit";
                if (containerr.Cells[5].Text == "DR")
                    labeldebit.Text = dataValue.ToString();

            }
        }
        catch
        {
            throw;
        }
    }

    protected void lbl2_DataBinding(object sender, EventArgs e)
    {
        try
        {
            Label label1 = (Label)sender;

            GridViewRow containerr = (GridViewRow)label1.NamingContainer;
            object dataValue = DataBinder.Eval(containerr.DataItem, colname);
            
            Label lbl = (Label)containerr.Cells[0].Controls[0];
            
            if (dataValue != DBNull.Value)
            {
                label1.ID = "lbl_" + dataValue.ToString();

                if ((lbl.ID != null && dataValue.ToString().StartsWith("By ")) || dataValue.ToString().StartsWith("Opening"))
                {
                    label1.Text = dataValue.ToString();
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void lbl1_DataBinding(object sender, EventArgs e)
    {
        try
        {
            Label label1 = (Label)sender;

            GridViewRow containerr = (GridViewRow)label1.NamingContainer;
            object dataValue = DataBinder.Eval(containerr.DataItem, colname);
            if (dataValue != DBNull.Value)
            {
                label1.ID = dataValue.ToString();

            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void lnk_DataBinding(object sender, EventArgs e)
    {
        try
        {
            LinkButton linke = (LinkButton)sender;
            GridViewRow containerr = (GridViewRow)linke.NamingContainer;
            object dataValue = DataBinder.Eval(containerr.DataItem, colname);
            if (dataValue != DBNull.Value)
            {
                Label lbl = (Label)containerr.Cells[0].Controls[0];
                linke.ID = "lnk_" + lbl.ID;
                linke.CommandName = "Ledger";
                linke.CommandArgument = lbl.ID + "-" + dataValue.ToString();

                //if (lbl.ID != null && !(dataValue.ToString().StartsWith("By ")))
                if (lbl.ID != null)
                {
                    linke.Text = dataValue.ToString();
                    
                    linke.Click += new EventHandler(linke_Click);
                }

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void linke_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkLedger = (LinkButton)sender;
            string strRelationid = linkLedger.ID;
            HttpContext.Current.Session["RELATIONID"] = strRelationid.Split('_')[1].ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}
