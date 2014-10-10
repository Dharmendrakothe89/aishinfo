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

public partial class UserControls_ScrollDataFetchControl : System.Web.UI.UserControl
{

    public DataTable InputData
    {
        get;
        set;
    }
    public string DisplayColumn
    {
        get;
        set;
    }
    public string FDate
    {
        get;
        set;
    }
    public string TDate
    {
        get;
        set;
    }
    public string DisplayPrint
    {
        get;
        set;
    }
    public string DisplayChangeLedger
    {
        get;
        set;
    }
    public string DisplayChart
    {
        get;
        set;
    }
    public string DisplayDateRange
    {
        get;
        set;
    }
    public string DisplayMonthVeiw
    {
        get;
        set;
    }
    public string GetLinkName
    {
        get;
        set;
    }
    public string GetTotalRecordCount
    {
        get;
        set;
    }
    public bool IsOld
    {
        get;
        set;
    }
    public event GridViewRowEventHandler ControlDataBound;
    public event GridViewCommandEventHandler ControlCommandEvent;
    public event EventHandler ClickLedgerButton;
    public event EventHandler ClickDateButton;
    public event EventHandler ClickMonthVeiwButton;
    public event EventHandler ClickColumnChange;
    public event EventHandler ClickFeildChange;
    public event EventHandler ClickVoucher;
    public event EventHandler ClickPrintButton;
    DataTable dsProducts1 = null;
    DataTable PartialDataset = null;
    string monthdate = string.Empty;
    string Fdate = string.Empty;
    string Tdate = string.Empty;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ddffvv", "ClickToFix();", true);
        if (!String.IsNullOrEmpty(FDate))
        {
            FromDateLabel.Text = FDate;
            ToDateLabel.Text = TDate;
        }
       
        if (FromDateLabel.Text != string.Empty && ToDateLabel.Text != string.Empty)
        {
            if (ViewState["ControlStoredate"] != null)
            {
                string testdate = ViewState["ControlStoredate"].ToString();
                FromDateLabel.Text = testdate.Split('$')[0].ToString();
                ToDateLabel.Text = testdate.Split('$')[1].ToString();
                
            }
            Fdate = FromDateLabel.Text;
            Tdate = ToDateLabel.Text;
            monthdate = Fdate + " $" + Tdate;
            Fdate = Fdate.Trim();
            Tdate = Tdate.Trim();
            string Fromdate = ConvertDateTime(Fdate);
            string Todate = ConvertDateTime(Tdate);
            Table DynamicTable = new Table();
            DateTime dtfromdate = DateTime.ParseExact(Fromdate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            DateTime dtTodate = DateTime.ParseExact(Todate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            DateTime newdate = dtfromdate.AddYears(1);
            string s = newdate.Year.ToString() + "-03-31";
            newdate = DateTime.ParseExact(s, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            if (newdate < dtTodate)
            {
                dtTodate = newdate;
            }
            for (int i = 0; i < 12; i++)
            {
                if (dtTodate >= dtfromdate)
                {
                    TableRow tr = new TableRow();
                    string monthName = GetMonthName(dtfromdate.Month, false, null) + "-" + dtfromdate.Year;
                    for (int j = 0; j < 1; j++)
                    {
                        TableCell tc = new TableCell();
                        LinkButton lnkMonthYear = new LinkButton();
                        lnkMonthYear.ID = "lnk_" + i;
                        lnkMonthYear.Click += new EventHandler(lnkMonthYear_Click);
                        lnkMonthYear.Text = monthName;
                        tc.Controls.Add(lnkMonthYear);
                        tr.Controls.Add(tc);
                    }
                    DynamicTable.Rows.Add(tr);
                }
                dtfromdate = dtfromdate.AddMonths(1);
            }
            DivContainer.Controls.Add(DynamicTable);
           
        }

        if (Session["Record"] != null)
        {
            if (IsOld == false)
                InputData = (DataTable)Session["Record"];
            string remainfromdate=FromDateLabel.Text;
            string remaintodate=ToDateLabel.Text;
            ControlDataBind();
            //labelRecordFetch.Text = "0";
            if(FromDateLabel.Text == string.Empty || ToDateLabel.Text == string.Empty)
            {
                FromDateLabel.Text = remainfromdate;
                ToDateLabel.Text = remaintodate;
            }
            
        }
    }
    public void ControlDataBind()
    {
        ChangeLedgerButton.Style.Add("display", DisplayChangeLedger);
        //ChartButton.Style.Add("display", DisplayChart);
        MonthVeiwButton.Style.Add("display", DisplayMonthVeiw);
        ChangeRangeButton.Style.Add("display", DisplayDateRange);
        PrintButton.Style.Add("display", DisplayPrint);
        btnpostback.Style.Add("display", "block");
        Session["value"] = DisplayColumn;
        lblTotalLedgers.Text = string.Empty;
        labelRecordFetch.Text = string.Empty;
        if (FDate != string.Empty || Fdate != null)
        {
            FromDateLabel.Text = FDate;
            ToDateLabel.Text = TDate;
        }
         
        if (InputData != null && InputData.Rows.Count > 0)
        {

            if (InputData != null && IsOld != true)
            {
                Session["Record"] = InputData;

            }

            DataTable PartialDataset = InputData.Clone();
            if (InputData.Rows.Count > 10)
            {
                if (Session["MaintainPreviousData"] != null)
                {
                    PartialDataset.Merge((DataTable)Session["MaintainPreviousData"]);
                }
                else
                {
                    for (int i = 0; i < 10; i++)
                    {
                        PartialDataset.Rows.Add(InputData.Rows[i].ItemArray);
                    }
                }

            }
            else
            {
                for (int i = 0; i < InputData.Rows.Count; i++)
                {
                    PartialDataset.Rows.Add(InputData.Rows[i].ItemArray);
                }

            }
            
            //ViewState["PoductID"] = PartialDataset.Rows[PartialDataset.Rows.Count - 1][0].ToString();

            UserControlGridVeiw.Columns.Clear();


            BindDataGrid(PartialDataset);
            if (GetTotalRecordCount != null)
            {
                lblTotalLedgers.Text = GetTotalRecordCount.ToString();
                ViewState["RecordCount"] = GetTotalRecordCount.ToString();
                
            }
            else if (ViewState["RecordCount"] != null)
            {
                lblTotalLedgers.Text = ViewState["RecordCount"].ToString();
               
            }
            else
            {
                lblTotalLedgers.Text = PartialDataset.Rows.Count.ToString();
            }

            if (Session["MaintainPreviousData"] != null && IsOld == true)
            {
                UserControlGridVeiw.Visible = true;
                UserControlGridVeiw.DataSource = (DataTable)Session["MaintainPreviousData"];
                UserControlGridVeiw.DataBind();
            }
            else
            {
                UserControlGridVeiw.DataSource = PartialDataset;
                UserControlGridVeiw.DataBind();
            }


            Session["MaintainPreviousData"] = PartialDataset;

            int recordcount = UserControlGridVeiw.Rows.Count;
            labelRecordFetch.Text = recordcount.ToString();
        }
        else
        {
            UserControlGridVeiw.DataSource = null;
            UserControlGridVeiw.DataBind();

        }
        if (lblTotalLedgers.Text != string.Empty && labelRecordFetch.Text != string.Empty)
        {

            if (General.Parse<int>(lblTotalLedgers.Text) == General.Parse<int>(labelRecordFetch.Text))
            {
                btnpostback.Style.Add("display", "none");
            }
        }
        lblTotalCredit.Text = string.Empty;
        lblTotalDebit.Text = string.Empty;
        lblClosingTotalCredit.Text = string.Empty;
        lblClosingTotalDebit.Text = string.Empty;
        GetCreditDebit();
    }
    public void BindDataGrid(DataTable PartialDataset)
    {
        try
        {
            if (!PartialDataset.Columns.Contains("PRIMARY"))
            {
                PartialDataset.Columns.Add("PRIMARY");
                PartialDataset.Columns["PRIMARY"].SetOrdinal(0);
            }

            for (int row = 0; row < PartialDataset.Rows.Count; row++)
            {
                PartialDataset.Rows[row]["PRIMARY"] = PartialDataset.Rows[row]["RELATIONID"].ToString();
            }

            for (int i = 0; i < PartialDataset.Columns.Count; i++)
            {
                if (i == 1)
                {
                    BoundField bfcol1 = new BoundField();
                    bfcol1.DataField = PartialDataset.Columns["Date"].ColumnName;
                    bfcol1.HeaderText = "DATE";
                    bfcol1.ItemStyle.Width = Unit.Pixel(100);
                    UserControlGridVeiw.Columns.Add(bfcol1);

                }
                if (i == 9)
                {
                    BoundField bfcol9 = new BoundField();
                    if (PartialDataset.Columns.Contains("BALANCE"))
                    {
                        bfcol9.DataField = PartialDataset.Columns["BALANCE"].ColumnName;
                    }
                    else if (PartialDataset.Columns.Contains("Credit"))
                    {
                        bfcol9.DataField = PartialDataset.Columns["Credit"].ColumnName;
                    }
                    bfcol9.HeaderText = "BALANCE";
                    bfcol9.ItemStyle.Width = Unit.Pixel(100);
                    UserControlGridVeiw.Columns.Add(bfcol9);
                }
                if (i == 3)
                {
                    BoundField bfcol2 = new BoundField();
                    bfcol2.DataField = PartialDataset.Columns["NARRATION"].ColumnName;
                    bfcol2.HeaderText = "NARRATION";
                    bfcol2.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                    bfcol2.ItemStyle.Width = Unit.Pixel(250);
                    UserControlGridVeiw.Columns.Add(bfcol2);

                }
                if (i == 5)
                {
                    BoundField bfcol6 = new BoundField();
                    bfcol6.DataField = PartialDataset.Columns["LTRANTYPE"].ColumnName;
                    bfcol6.HeaderText = "TRANSACTION TYPE";
                    bfcol6.HeaderStyle.CssClass = "hideen";
                    bfcol6.ItemStyle.CssClass = "hideen";
                    bfcol6.HeaderStyle.Width = Unit.Pixel(5);
                    bfcol6.ItemStyle.Width = Unit.Pixel(5);
                    UserControlGridVeiw.Columns.Add(bfcol6);

                }
                if (i == 6)
                {
                    BoundField bfcol3 = new BoundField();
                    bfcol3.DataField = PartialDataset.Columns["VOUCHERTYPE"].ColumnName;
                    bfcol3.HeaderText = "VOUCHER TYPE";
                    bfcol3.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                    UserControlGridVeiw.Columns.Add(bfcol3);

                }
                if (i == 4)
                {
                    BoundField bfcol4 = new BoundField();
                    bfcol4.DataField = PartialDataset.Columns["VchNo"].ColumnName;
                    bfcol4.HeaderText = "VOUCHER NO";
                    bfcol4.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                    UserControlGridVeiw.Columns.Add(bfcol4);

                }

                if (i == 2)
                {
                    Label lbl = new Label();
                    LinkButton linkbtn = new LinkButton();
                    TemplateField tempobj = new TemplateField();
                    /************************MAHENDRA*****************************/
                    //tempobj.HeaderTemplate = new ScrollDataGrid(lbl, PartialDataset.Columns["particular"].ColumnName, i, ListItemType.Header);
                    /**************Ashish******/
                    tempobj.ItemTemplate = new ScrollDataGrid(lbl, linkbtn, PartialDataset.Columns["particular"].ColumnName, i, ListItemType.Item);
                    tempobj.HeaderText = "PARTICULAR";
                    tempobj.ItemStyle.Width = Unit.Pixel(250);
                    tempobj.HeaderStyle.Width = Unit.Pixel(250);
                    tempobj.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                    tempobj.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
                    UserControlGridVeiw.Columns.Add(tempobj);

                }
                if (i == 0)
                {
                    Label lbl = new Label();
                    TemplateField tempobj = new TemplateField();
                    tempobj.ItemTemplate = new ScrollDataGrid(lbl, PartialDataset.Columns["PRIMARY"].ColumnName, i, ListItemType.Item);
                    UserControlGridVeiw.Columns.Add(tempobj);

                }
                if (i == 7)
                {

                    Label lbl = new Label();
                    TemplateField tempobjcredit = new TemplateField();
                    tempobjcredit.HeaderTemplate = new ScrollDataGrid(lbl, PartialDataset.Columns["Credit"].ColumnName, i, ListItemType.Header);
                    tempobjcredit.ItemTemplate = new ScrollDataGrid(lbl, PartialDataset.Columns["Credit"].ColumnName, i, ListItemType.Item);
                    UserControlGridVeiw.Columns.Add(tempobjcredit);

                }
                if (i == 8)
                {

                    Label lbl = new Label();
                    TemplateField tempobjcredit = new TemplateField();
                    tempobjcredit.HeaderTemplate = new ScrollDataGrid(lbl, "Debit", i, ListItemType.Header);
                    tempobjcredit.ItemTemplate = new ScrollDataGrid(lbl, PartialDataset.Columns["Credit"].ColumnName, i, ListItemType.Item);
                    UserControlGridVeiw.Columns.Add(tempobjcredit);

                }

            }


        }
        catch 
        {
            throw;

        }

    }
    protected void UserControlGridVeiw_OnBound(object sender, GridViewRowEventArgs e)
    {
        if (ControlDataBound != null)
        {
            ControlDataBound(this, e);
        }


    }
    protected void ChangeLedgerButton_Click(object sender, EventArgs e)
    {
        Session.Remove("MaintainPreviousData");
        if (ClickLedgerButton != null)
        {
            ClickLedgerButton(this, e);
        }
    }
    protected void UserControlGridVeiw_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (ControlCommandEvent != null)
        {
            ControlCommandEvent(this, e);
        }
    }
    protected void btnpostback_Click(object sender, EventArgs e)//this button is visisble = false
    {
        /*IsOld = true;
        if (Session["Record"] != null)
        {
            InputData = (DataTable)Session["Record"];
            DataTable PartialDataset = ((DataTable)Session["MaintainPreviousData"]).Clone();
            if (Session["MaintainPreviousData"] != null && ((DataTable)Session["MaintainPreviousData"]).Rows.Count > 0)
            {
                PartialDataset.Merge(((DataTable)Session["MaintainPreviousData"]));
            }

            //for (int i = 0; i < InputData.Rows.Count; i++)
            //{
            //    if (ViewState["PoductID"] != null && ViewState["PoductID"].ToString() != string.Empty)
            //    {
            //        if (int.Parse(InputData.Rows[i][0].ToString()) > int.Parse(ViewState["PoductID"].ToString()))
            //        {
            //            // InputData.Rows[i].ItemArray
            //            PartialDataset.Rows.Add(InputData.Rows[i]["RELATIONID"], InputData.Rows[i]["associateledger"], InputData.Rows[i]["Date"], InputData.Rows[i]["Credit"], InputData.Rows[i]["NARRATION"], InputData.Rows[i]["VchNo"], InputData.Rows[i]["VOUCHERTYPE"], InputData.Rows[i]["LTRANTYPE"], InputData.Rows[i]["particular"], InputData.Rows[i]["RELATIONID"]);
            //        }
            //    }
            //}
            if (PartialDataset.Rows.Count != InputData.Rows.Count)
            {
                int count = InputData.Rows.Count - PartialDataset.Rows.Count;
                if (count > 10)
                {
                    count = 10;
                }
                count = PartialDataset.Rows.Count + count;
                
                for (int i = PartialDataset.Rows.Count; i < count; i++)
                {
                    int h = 0;
                    int column = InputData.Columns.Count;
                    PartialDataset.Rows.Add();
                    for (int j = 0; j < column+1; j++)
                    {
                        if (j == 0)
                        {
                            PartialDataset.Rows[i][j] = InputData.Rows[i][column - 1];
                        }
                        else if(h == column)
                        {
                            PartialDataset.Rows[i][h] = InputData.Rows[i][h - 1];
                            
                        }
                        else
                        {
                            PartialDataset.Rows[i][j] = InputData.Rows[i][h];
                            h++;
                        }
                    }
                    //PartialDataset.Rows.Add(InputData.Rows[i]["RELATIONID"], InputData.Rows[i]["particular"], InputData.Rows[i]["associateledger"], InputData.Rows[i]["SRNO"], InputData.Rows[i]["NARRATION"], InputData.Rows[i]["VOUCHERTYPE"], InputData.Rows[i]["STATUS"], InputData.Rows[i]["Date"], InputData.Rows[i]["LTRANTYPE"], InputData.Rows[i]["VchNo"], InputData.Rows[i]["CREDIT"], InputData.Rows[i]["DEBIT"], "", "", "", "", "", "", "", "", InputData.Rows[i]["relationid1"], InputData.Rows[i]["RELATIONID"]);
                }
            }

            if (PartialDataset.Rows.Count > 0)
            {
                ViewState["PoductID"] = PartialDataset.Rows[PartialDataset.Rows.Count - 1][0].ToString();

                UserControlGridVeiw.DataSource = PartialDataset;
                UserControlGridVeiw.DataBind();

                Session["MaintainPreviousData"] = PartialDataset;

                GetCreditDebit();
            }
            else
            {

                ScriptManager.RegisterClientScriptBlock(btnpostback, btnpostback.GetType(), "AppendScript", "BindNewData()", true);
            }
            //ScriptManager.RegisterClientScriptBlock(btnpostback, btnpostback.GetType(), "AppendScript2", "AppendData()", true);

            ScriptManager.RegisterClientScriptBlock(btnpostback, btnpostback.GetType(), "setscroll", "ccc()", true);

            int recordcount = UserControlGridVeiw.Rows.Count;
            labelRecordFetch.Text = recordcount.ToString();
        }*/
        ScriptManager.RegisterClientScriptBlock(btnpostback, btnpostback.GetType(), "setscroll", "ccc()", true);
    }
    protected void ChangeRangeButton_Click(object sender, EventArgs e)
    {
        ViewState["ControlStoredate"] = null;
        if (ClickDateButton != null)
        {
            ClickDateButton(this, e);
        }
    }
    protected void MonthVeiwButton_Click(object sender, EventArgs e)
    {
        if (ViewState["ControlStoredate"] == null)
        {
            ViewState["ControlStoredate"] = monthdate;
        }
        else
        {
            monthdate = ViewState["ControlStoredate"].ToString();
        }
        if (monthdate != string.Empty)
        {
            FromDateLabel.Text = monthdate.Split('$')[0].ToString();
            ToDateLabel.Text = monthdate.Split('$')[1].ToString();
        }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ChangeDateRanger", "revealModal1('LedgerGridVeiw_divmonthveiw');", true);
        /*Button btn = (Button)sender;
         string Fromdate = ConvertDateTime(FromDateLabel.Text);
         string Todate = ConvertDateTime(ToDateLabel.Text);
         Table DynamicTable = new Table();
         DateTime dtfromdate = DateTime.ParseExact(Fromdate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
         DateTime dtTodate = DateTime.ParseExact(Todate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
         DateTime newdate = dtfromdate.AddYears(1);
         string s = newdate.Year.ToString() + "-03-31";
         newdate = DateTime.ParseExact(s, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
         if (newdate < dtTodate)
         {
             dtTodate = newdate;
         }
         // dtTodate.g
         for (int i = 0; i < 12; i++)
         {
             if (dtTodate >= dtfromdate)
             {
                 TableRow tr = new TableRow();

                 string monthName = GetMonthName(dtfromdate.Month, false, null) + "-" + dtfromdate.Year;
                 for (int j = 0; j < 1; j++)
                 {

                     TableCell tc = new TableCell();
                     //LinkButton lnkMonthYear = new LinkButton();
                     //lnkMonthYear.ID = "lnk_" + i;
                     //lnkMonthYear.Click += lnkMonthYear_Click;
                     ////lnkMonthYear.Attributes.Add("runat", "server");
                     //lnkMonthYear.Attributes.Add("onclick", "alert('1');");
                     //lnkMonthYear.Text = monthName;

                     //tc.Controls.Add(lnkMonthYear);
                     Button testbtn = new Button();
                     testbtn.ID = "testbtn" + i;
                     testbtn.Click += new EventHandler(testbtn_Click);
                     testbtn.Attributes.Add("onclick", "alert('1');");
                     testbtn.Text = monthName;
                     tc.Controls.Add(testbtn);
                     tr.Controls.Add(tc);

                 }



                 DynamicTable.Rows.Add(tr);
             }
             dtfromdate = dtfromdate.AddMonths(1);
         }
         DivContainer.Controls.Add(DynamicTable);


         ScriptManager.RegisterClientScriptBlock(btn, btn.GetType(), "ChangeDateRange", "revealModal1('LedgerGridVeiw_DivContainer');", true);*/
        //if (ClickMonthVeiwButton != null)
        //{
        //    ClickMonthVeiwButton(this, e);
        //}

    }
    public static string GetMonthName(int month, bool abbreviate, IFormatProvider provider)
    {
        System.Globalization.DateTimeFormatInfo info = System.Globalization.DateTimeFormatInfo.GetInstance(provider);
        if (abbreviate) return info.GetAbbreviatedMonthName(month);
        return info.GetMonthName(month);
    }
    public string ConvertDateTime(string value)
    {
        try
        {
            DateTime dtout;
            if (DateTime.TryParseExact(value, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dtout))
            {
                return dtout.ToString("yyyy-MM-dd");
            }
            else if (DateTime.TryParseExact(value, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out dtout))
            {
                return dtout.ToString("yyyy-MM-dd");
            }

            return string.Empty;
        }
        catch (Exception ex)
        {
            return string.Empty;
            throw ex;
        }
    }
    protected void lnkMonthYear_Click(object sender, EventArgs e)
    {

        LinkButton llbtn = (LinkButton)sender;
        GetLinkName = llbtn.Text;
        ScriptManager.RegisterClientScriptBlock(llbtn, llbtn.GetType(), "ChangeMonth", "hideModal1('LedgerGridVeiw_divmonthveiw');", true);
        if (ClickMonthVeiwButton != null)
        {
            ClickMonthVeiwButton(this, e);
        }

    }
    public static Control GetPostBackControl(Page page)
    {
        Control control = null;

        string ctrlname = page.Request.Params.Get("__EVENTTARGET");
        if (ctrlname != null && ctrlname != string.Empty)
        {
            control = page.FindControl(ctrlname);
        }
        else
        {
            foreach (string ctl in page.Request.Form)
            {
                Control c = page.FindControl(ctl);
                if (c is System.Web.UI.WebControls.Button)
                {
                    control = c;
                    break;
                }
            }
        }
        return control;
    }
    protected void ChangeColumnButton_Click(object sender, EventArgs e)
    {
        ViewState["columnchangedate"] = Fdate + "$" + Tdate;
        if (ClickColumnChange != null)
        {
            ClickColumnChange(this, e);
        }

    }
    protected void ChangeFeildButton_click(object sender, EventArgs e)
    {
        if (ClickFeildChange != null)
        {
            ClickFeildChange(this, e);
        }
    }
    protected void BtnVoucher_click(object sender, EventArgs e)
    {
        if (ClickVoucher != null)
        {
            ClickVoucher(this, e);
        }
    }
    public void GetCreditDebit()
    {
        double debitamount = 0.0;
        double creditamount = 0.0;
        try
        {



            for (int i = 0; i < UserControlGridVeiw.Rows.Count; i++)
            {
                //if (i > 0)
                {
                    Label lbl1 = (Label)UserControlGridVeiw.Rows[i].Cells[7].FindControl("lblCredit");

                    Label lbl2 = (Label)UserControlGridVeiw.Rows[i].Cells[6].FindControl("lblDebit");

                    if (lbl1.Text.Trim() != string.Empty)
                    {
                        creditamount += Convert.ToDouble(lbl1.Text.Trim());
                    }
                    if (lbl2.Text.Trim() != string.Empty)
                    {
                        debitamount += Convert.ToDouble(lbl2.Text.Trim());
                    }
                }

            }
            lblTotalCredit.Text = creditamount.ToString();
            lblTotalDebit.Text = debitamount.ToString();
            if (creditamount > debitamount)
            {
                double value = creditamount - debitamount;

                lblClosingTotalCredit.Text = value.ToString() +" CR";
            }
            else if (debitamount > creditamount)
            {
                double value = debitamount - creditamount;
                //lblTotalDebit.Text = value.ToString();
                lblClosingTotalDebit.Text = value.ToString() + " DR";
            }
            else
            {
                lblClosingTotalCredit.Text = "0";
                lblClosingTotalDebit.Text = "0";
            }

        }
        catch (Exception ex)
        {
            throw ex;

        }
    }
    protected void PrintButton_Click(object sender, EventArgs e)
    {
        if (ClickPrintButton != null)
        {
            ClickPrintButton(this, e);
        }
    }
}

