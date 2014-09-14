using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for reportingmaster
/// </summary>
public class reportingmaster
{
	public reportingmaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
         Handler objHandler = new Handler();
    Handler objhandler = new Handler();
    string xmlpath;
    public reportingmaster(string xmlpathh)
    {
        xmlpath = xmlpathh;
    }

    #region Private Members
    private int _srno = 0;
    private int _relationshipid = 0;
    private string _date = string.Empty;
    private string _name = string.Empty;
    private string _contactno = string.Empty;
    private string _activity = string.Empty;
    private string _result = string.Empty;
    private string _remark = string.Empty;
    private int _status = 0;
    #endregion

    #region Properties
    public int reportingmaster_SRNO
    {
        get { return _srno; }
        set { _srno = value; }
    }
     public int reportingmaster_RELATIONSHIPID
    {
        get { return _relationshipid; }
        set { _relationshipid = value; }
    }
    public string reportingmaster_DATE
    {
        get { return _date; }
        set { _date = value; }
    }
    public string reportingmaster_NAME
    {
        get { return _name; }
        set { _name = value; }
    }
    public string reportingmaster_CONTACTNO
    {
        get { return _contactno; }
        set { _contactno = value; }
    }
    public string reportingmaster_ACTIVITY
    {
        get { return _activity; }
        set { _activity = value; }
    }
    public string reportingmaster_RESULT
    {
        get { return _result; }
        set { _result = value; }
    }
    public string reportingmaster_REMARK
    {
        get { return _remark; }
        set { _remark = value; }
    }
    public int reportingmaster_STATUS
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