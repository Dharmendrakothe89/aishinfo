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

public class transactiondetails
{
	public transactiondetails()
	{
		//
		// TODO: Add constructor logic here
		//
	}

     Handler objHandler = new Handler();
    string xmlpath;
    public transactiondetails(string xmlpathh)
    {
        xmlpath = xmlpathh;
    }
    #region Private Members

    private int _srno = 0;
    private int _ledgerid = 0;
    private int _relationid = 0;
    private string _ltrntype = string.Empty;
    private double _amount = 0;
    private string _narration = string.Empty;
    private string _transdate = string.Empty;
    private string _vouchertype = string.Empty;
    private int _branchid = 0;
    private int _associateledger = 0;
    private int _status = 0;
    #endregion


    #region Properties
    public int transactiondetails_SRNO
    {
        get { return _srno; }
        set { _srno = value; }
    }
    public int transactiondetails_LEDGERID
    {
        get { return _ledgerid; }
        set { _ledgerid = value; }
    }
    public int transactiondetails_RELATIONID
    {
        get { return _relationid; }
        set { _relationid = value; }
    }
    public string transactiondetails_LTRNTYPE
    {
        get { return _ltrntype; }
        set { _ltrntype = value; }
    }
    public double transactiondetails_AMOUNT
    {
        get { return _amount; }
        set { _amount = value; }
    }
  
    public string transactiondetails_NARRATION
    {
        get { return _narration; }
        set { _narration = value; }
    }
    public string transactiondetails_TRANSDATE
    {
        get { return _transdate; }
        set { _transdate = value; }
    }
    public string transactiondetails_VOUCHERTYPE
    {
        get { return _vouchertype; }
        set { _vouchertype = value; }
    }
    public int transactiondetails_BRANCHID
    {
        get { return _branchid; }
        set { _branchid = value; }
    }
    public int transactiondetails_ASSOCIATELEDGER
    {
        get { return _associateledger; }
        set { _associateledger = value; }
    }
    public int transactiondetails_STATUS
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