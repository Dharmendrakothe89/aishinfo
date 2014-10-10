using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for statemaster
/// </summary>
public class statemaster
{
	public statemaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    Handler objHandler = new Handler();
    Handler objhandler = new Handler();
    string xmlpath;
    public statemaster(string xmlpathh)
    {
        xmlpath = xmlpathh;
    }

    #region Private Members
    private int _stateid = 0;
    private string _statename = string.Empty;
    private int _unionteritary = -1;
    private int _status = 0;
    #endregion

    #region Properties
    public int statemaster_STATEID
    {
        get { return _stateid; }
        set { _stateid = value; }
    }
    public string statemaster_STATENAME
    {
        get { return _statename; }
        set { _statename = value; }
    }
    public int statemaster_UNIONTERITARY
    {
        get { return _unionteritary; }
        set { _unionteritary = value; }
    }

    public int statemaster_STATUS
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