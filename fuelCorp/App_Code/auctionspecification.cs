using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Summary description for auctionspecification
/// </summary>
public class auctionspecification
{
	public auctionspecification()
	{
	}
    Handler objHandler = new Handler();
    Handler objhandler = new Handler();
    string xmlpath;
    public auctionspecification(string xmlpathh)
    {
        xmlpath = xmlpathh;
    }

    #region Private Members
    private int _srno = 0;
    private string _contracttype = string.Empty;
    private int _contractid = 0;
    private string _coaltype = string.Empty;
    private string _grade = string.Empty;    
    private double rate = 0;
    private double quantity = 0;
    private string _quantityunit = string.Empty;
    private int _status = 0;
    #endregion

    #region Properties
    public int auctionspecification_SRNO
    {
        get { return _srno; }
        set { _srno = value; }
    }
    public string auctionspecification_CONTRACTTYPE
    {
        get { return _contracttype; }
        set { _contracttype = value; }
    }
    public int auctionspecification_CONTRACTID
    {
        get { return _contractid; }
        set { _contractid = value; }
    }
    public string auctionspecification_COALTYPE
    {
        get { return _coaltype; }
        set { _coaltype = value; }
    }
    public string auctionspecification_GRADE
    {
        get { return _grade; }
        set { _grade = value; }
    }
    public double auctionspecification_RATE
    {
        get { return rate; }
        set { rate = value; }
    }
    public double auctionspecification_QUANTITY
    {
        get { return quantity; }
        set { quantity = value; }
    }
    public string auctionspecification_QUANTITYUNIT
    {
        get { return _quantityunit; }
        set { _quantityunit = value; }
    }
    public int auctionspecification_STATUS
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
            return objhandler.GetTable(arrcolumns, tableName, condition);
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
            return objhandler.Insert(flag, tableName, this, xmlpath);

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
            objhandler.Condition = condition;
            return objhandler.Insert(flag, tableName, this, xmlpath);
        }
        catch
        {
            throw;
        }
    }
    public void Update()
    {

    }

    public void Delete()
    {

    }

    public bool CreateSQL(bool flag, string tableName)
    {
        return false;
    }

    #endregion
}