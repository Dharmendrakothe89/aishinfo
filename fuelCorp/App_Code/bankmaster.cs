using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;

/// <summary>
/// Summary description for bankmaster
/// </summary>
public class bankmaster
{
	public bankmaster()
	{
	}

    Handler objHandler = new Handler();
    Handler objhandler = new Handler();
    string xmlpath;
    public bankmaster(string xmlpathh)
    {
        xmlpath = xmlpathh;
    }

    #region Private Members
    private int _srno = 0;
    private int _ledgerid = 0;
    private string _accountname = string.Empty;
    private string _accountno = string.Empty;
    private string _bankname = string.Empty;
    private string _bankbranchname = string.Empty;
    private string _micrcode = string.Empty;
    private string _ifsccode = string.Empty;
    private string _cityname = string.Empty;
    private string _statename = string.Empty;
    private string _address = string.Empty;
    private int _branchid = 0;
    private int _status = 0;
    #endregion

    #region Properties
    public int bankmaster_SRNO
    {
        get { return _srno; }
        set { _srno = value; }
    }
    public int bankmaster_LEDGERID
    {
        get { return _ledgerid; }
        set { _ledgerid = value; }
    }
    public string bankmaster_ACCOUNTNAME
    {
        get { return _accountname; }
        set { _accountname = value; }
    }
    public string bankmaster_ACCOUNTNO
    {
        get { return _accountno; }
        set { _accountno = value; }
    }
    public string bankmaster_BANKNAME
    {
        get { return _bankname; }
        set { _bankname = value; }
    }
    public string bankmaster_BANKBRANCHNAME
    {
        get { return _bankbranchname; }
        set { _bankbranchname = value; }
    }
    public string bankmaster_MICRCODE
    {
        get { return _micrcode; }
        set { _micrcode = value; }
    }
    public string bankmaster_IFSCCODE
    {
        get { return _ifsccode; }
        set { _ifsccode = value; }
    }
    public string bankmaster_CITYNAME
    {
        get { return _cityname; }
        set { _cityname = value; }
    }
    public string bankmaster_STATENAME
    {
        get { return _statename; }
        set { _statename = value; }
    }
    public string bankmaster_ADDRESS
    {
        get { return _address; }
        set { _address = value; }
    }
    public int bankmaster_BRANCHID
    {
        get { return _branchid; }
        set { _branchid = value; }
    }
    public int bankmaster_STATUS
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