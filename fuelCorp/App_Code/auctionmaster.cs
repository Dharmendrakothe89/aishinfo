using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for auctionmaster
/// </summary>
public class auctionmaster
{
	public auctionmaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
     Handler objHandler = new Handler();
    Handler objhandler = new Handler();
    string xmlpath;
    public auctionmaster(string xmlpathh)
    {
        xmlpath = xmlpathh;
    }

    #region Private Members
    private int _auctionid = 0;
    private int _cmpid = 0;
    private string _auctionname = string.Empty;
    private string _description = string.Empty;
    private string _auctiondate = string.Empty;
    private string _startdate = string.Empty;
    private string _enddate = string.Empty;
    private int _status = 0;
    #endregion

    #region Properties
    public int auctionmaster_AUCTIONID
    {
        get { return _auctionid; }
        set { _auctionid = value; }
    }
    public int auctionmaster_CMPID
    {
        get { return _cmpid; }
        set { _cmpid = value; }
    }
    public string auctionmaster_AUCTIONNAME
    {
        get { return _auctionname; }
        set { _auctionname = value; }
    }

    public string auctionmaster_DESCRIPTION
    {
        get { return _description; }
        set { _description = value; }
    }
    public string auctionmaster_AUCTIONDATE
    {
        get { return _auctiondate; }
        set { _auctiondate = value; }
    }
    public string auctionmaster_STARTDATE
    {
        get { return _startdate; }
        set { _startdate = value; }
    }
    public string auctionmaster_ENDDATE
    {
        get { return _enddate; }
        set { _enddate = value; }
    }
    public int auctionmaster_STATUS
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