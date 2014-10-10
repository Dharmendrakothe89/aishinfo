using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Summary description for citymaster
/// </summary>
public class citymaster
{
	public citymaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
     Handler objHandler = new Handler();
    Handler objhandler = new Handler();
    string xmlpath;
    public citymaster(string xmlpathh)
    {
        xmlpath = xmlpathh;
    }

    #region Private Members
    private int _cityid = 0;
    private string _cityname = string.Empty;
    private int _stateid = -1;
    private int _status = 0;
    #endregion

    #region Properties
    public int citymaster_CITYID
    {
        get { return _cityid; }
        set { _cityid = value; }
    }
    public string citymaster_CITYNAME
    {
        get { return _cityname; }
        set { _cityname = value; }
    }
    public int citymaster_STATEID
    {
        get { return _stateid; }
        set { _stateid = value; }
    }

    public int citymaster_STATUS
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