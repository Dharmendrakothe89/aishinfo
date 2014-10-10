using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for permissionmaster
/// </summary>
public class permissionmaster
{
	public permissionmaster()
	{
	
	}
    Handler objHandler = new Handler();
    Handler objhandler = new Handler();
    string xmlpath;
    public permissionmaster(string xmlpathh)
    {
        xmlpath = xmlpathh;
    }
    #region privatemembers

    private int _srno = 0;
    private int _userid = 0;
    private int _cmpid = 0;
    private int _branchid = 0;
    private int _preffred = 0;
    private int _status = 0;
    #endregion


    #region  Properties

    public int permissionmaster_SRNO
    {
        get { return _srno; }
        set { _srno = value; }
    }
    public int permissionmaster_USERID
    {
        get { return _userid; }
        set { _userid = value; }
    }
    public int permissionmaster_CMPID
    {
        get { return _cmpid; }
        set { _cmpid = value; }
    }
    public int permissionmaster_BRANCHID
    {
        get { return _branchid; }
        set { _branchid = value; }
    }
    public int permissionmaster_PREFFERED
    {
        get { return _preffred; }
        set { _preffred = value; }
    }
    public int permissionmaster_STATUS
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