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

/// <summary>
/// Summary description for Transaction
/// </summary>
public class transactiontable
{
    Handler objHandler = new Handler();
    string xmlpath;
    public transactiontable()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public transactiontable(string xmlpathh)
    {
        xmlpath = xmlpathh;
    }


    #region Private Members
    private int _srno = 0;
    private int _ledger1 = 0;
    private int _relationid1 = 0;
    private string _ltrntype1 = string.Empty;
    private double _amount = 0;
    private int _ledger2 = 0;
    private int _relationid2 = 0;
    private string _ltrntype2 = string.Empty;
    private string _narration = string.Empty;
    private string _transdate = string.Empty;
    private int _transactiontype = 0;
    private string _vouchertype = string.Empty;
    private string _voucherno = string.Empty;
    private int _branchid = 0;
    private int _status = 0;
    #endregion


    #region Properties
    public int transactiontable_SRNO
    {
        get { return _srno; }
        set { _srno = value; }
    }
    public int transactiontable_LEDGER1
    {
        get { return _ledger1; }
        set { _ledger1 = value; }
    }
    public int transactiontable_RELATIONID1
    {
        get { return _relationid1; }
        set { _relationid1 = value; }
    }
    public string transactiontable_LTRNTYPE1
    {
        get { return _ltrntype1; }
        set { _ltrntype1 = value; }
    }
    public double transactiontable_AMOUNT
    {
        get { return _amount; }
        set { _amount = value; }
    }
    public int transactiontable_LEDGER2
    {
        get { return _ledger2; }
        set { _ledger2 = value; }
    }
    public int transactiontable_RELATIONID2
    {
        get { return _relationid2; }
        set { _relationid2 = value; }
    }
    public string transactiontable_LTRNTYPE2
    {
        get { return _ltrntype2; }
        set { _ltrntype2 = value; }
    }
    public string transactiontable_NARRATION
    {
        get { return _narration; }
        set { _narration = value; }
    }
    public string transactiontable_TRANSDATE
    {
        get { return _transdate; }
        set { _transdate = value; }
    }
    public int transactiontable_TRANSACTIONTYPE
    {
        get { return _transactiontype; }
        set { _transactiontype = value; }
    }
    public string transactiontable_VOUCHERTYPE
    {
        get { return _vouchertype; }
        set { _vouchertype = value; }
    }

    public string transactiontable_VOUCHERNO
    {
        get { return _voucherno; }
        set { _voucherno = value; }
    }
    public int transactiontable_BRANCHID
    {
        get { return _branchid; }
        set { _branchid = value; }
    }
    public int transactiontable_STATUS
    {
        get { return _status; }
        set { _status = value; }
    }
    #endregion

    #region IDataBase Members

    public DataTable Select(string tableName)
    {
        return null;
    }

    public DataTable Select(System.Collections.ArrayList arrcolumns, string tableName, string condition)
    {
        try
        {
            return objHandler.GetTable(arrcolumns, tableName, condition);
        }
        catch
        {
            throw;
        }

    }

    public DataTable Select(System.Collections.ArrayList columns, System.Collections.ArrayList tables, string conditon, string joinType, System.Collections.ArrayList OnCondition)
    {
        return null;
    }

    public bool Insert(bool flag, string tableName)
    {
        try
        {
            return objHandler.Insert(flag, tableName, this, xmlpath);
        }
        catch
        {
            throw;
        }
    }


    public bool Insert(bool flag, string tableName, string condition)
    {
        try
        {
            objHandler.Condition = condition;
            return objHandler.Insert(flag, tableName, this, xmlpath);
        }
        catch
        {
            throw;
        }
    }

    public void Update()
    {
        throw new NotImplementedException();
    }

    public void Delete()
    {
        throw new NotImplementedException();
    }

    public bool CreateSQL(bool flag, string tableName)
    {
        return false;
    }

    #endregion
}
