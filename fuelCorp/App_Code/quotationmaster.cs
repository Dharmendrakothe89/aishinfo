using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for quotationmaster
/// </summary>
public class quotationmaster
{
    public quotationmaster()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    Handler objHandler = new Handler();
    Handler objhandler = new Handler();
    string xmlpath;
    public quotationmaster(string xmlpathh)
    {
        xmlpath = xmlpathh;
    }
    #region privatemembers
    //TRANSPORTATIONCOST
    private int _quotationid = 0;
    private int _partytype = 0;
    private int _partyid = 0;
    private string _date = string.Empty;
    private string _refno = string.Empty;
    private double _coalcost = 0;
    private double _taxcost = 0;
    private double _transportationcost = 0;
    private double _totalcost = 0;
    private int _status = 0;
    private string _expirydate = string.Empty;
    private int _contactperson = 0;
    #endregion


    #region Properties
    public int quotationmaster_QUOTATIONID
    {
        get { return _quotationid; }
        set { _quotationid = value; }
    }
    public int quotationmaster_PARTYTYPE
    {
        get { return _partytype; }
        set { _partytype = value; }
    }
    public int quotationmaster_PARTYID
    {
        get { return _partyid; }
        set { _partyid = value; }
    }

    public string quotationmaster_DATE
    {
        get { return _date; }
        set { _date = value; }
    }


    public string quotationmaster_REFNO
    {
        get { return _refno; }
        set { _refno = value; }
    }

    public double quotationmaster_COALCOST
    {
        get { return _coalcost; }
        set { _coalcost = value; }
    }
    public double quotationmaster_TAXCOST
    {
        get { return _taxcost; }
        set { _taxcost = value; }
    }
    public double quotationmaster_TRANSPORTATIONCOST
    {
        get { return _transportationcost; }
        set { _transportationcost = value; }
    }

    public double quotationmaster_TOTALCOST
    {
        get { return _totalcost; }
        set { _totalcost = value; }
    }

    public int quotationmaster_STATUS
    {
        get { return _status; }
        set { _status = value; }
    }
    public string quotationmaster_EXPIRYDATE
    {
        get { return _expirydate; }
        set { _expirydate = value; }
    }
    public int quotationmaster_CONTACTPERSON
    {
        get { return _contactperson; }
        set { _contactperson = value; }
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
    //public bool Insert2(bool flag, string tableName, string condition)
    //{
    //    objhandler.Condition = condition;
    //    return objhandler.Insert2(flag, tableName, this, xmlpath);
    //}
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