using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
/// <summary>
/// Summary description for branchmaster
/// </summary>
public class branchmaster
{
	public branchmaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}

      Handler objHandler = new Handler();
    Handler objhandler = new Handler();
    string xmlpath;
    public branchmaster(string xmlpathh)
    {
        xmlpath = xmlpathh;
    }

    #region Private members
    private int _branchid = 0;
    private string _branchname = string.Empty;
    private int _cmpid = 0;
    private string _address = string.Empty;
    private int _stateid =0;
    private string _statename = string.Empty;
    private int _cityid = 0;
    private string _cityname = string.Empty;
    private int _status = 0;
   
    #endregion
    #region Properties
    public int branchmaster_BRANCHID
    {
        get { return _branchid; }
        set { _branchid = value; }
    }
    public string branchmaster_BRANCHNAME
    {
        get { return _branchname; }
        set { _branchname = value; }
    }
    public int branchmaster_CMPID
    {
        get { return _cmpid; }
        set { _cmpid = value; }

     }
    public string branchmaster_ADDRESS
    {
        get { return _address; }
        set { _address = value; }
    }
    public int branchmaster_STATEID
    {
        get { return _stateid; }
        set { _stateid = value; }
    }
    public string branchmaster_STATENAME
    {
        get { return _statename; }
        set { _statename = value; }
    }
    public int branchmaster_CITYID
    {
        get { return _cityid; }
        set { _cityid = value; }
    }
    public string branchmaster_CITYNAME
    {
        get { return _cityname; }
        set { _cityname = value; }
    }

    public int branchmaster_STATUS
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

    }

    public void Delete()
    {

    }

    public bool CreateSQL(bool flag, string tableName)
    {
        return false;
    }

    #endregion





    #region IDataBase Members


    public DataTable Select(ArrayList arrcolumns, string tableName, string condition)
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




    #endregion



    #region IDataBase Members


    public DataTable Select(ArrayList columns, ArrayList tables, string conditon, string joinType, ArrayList OnCondition)
    {
        return null;
    }

    #endregion
}