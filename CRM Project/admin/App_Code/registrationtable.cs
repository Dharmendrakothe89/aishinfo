using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for registrationtable
/// </summary>
public class registrationtable
{
	public registrationtable()
	{
		//
		// TODO: Add constructor logic here
		//
	}
      Handler objHandler = new Handler();
    Handler objhandler = new Handler();
    string xmlpath;
    public registrationtable(string xmlpathh)
    {
        xmlpath = xmlpathh;
    }

    #region Private Members
    private int _srno = 0;
    private string _name = string.Empty;
    private int _sponsorid = 0;
    private string _sponsorsemicode = string.Empty;
    private string _sponsorname = string.Empty;
    private string _semicode = string.Empty;
    private string _emailid = string.Empty;
    private string _phoneno = string.Empty;
    private int _status = 0;
    #endregion

    #region Properties
    public int registrationtable_SRNO
    {
        get { return _srno; }
        set { _srno = value; }
    }
    public string registrationtable_NAME
    {
        get { return _name; }
        set { _name = value; }
    }
    public int registrationtable_SPONSORID
    {
        get { return _sponsorid; }
        set { _sponsorid = value; }
    }
    public string registrationtable_SPONSORSEMICODE
    {
        get { return _sponsorsemicode; }
        set { _sponsorsemicode = value; }
    }
    public string registrationtable_SPONSORNAME
    {
        get { return _sponsorname; }
        set { _sponsorname = value; }
    }
    public string registrationtable_SEMICODE
    {
        get { return _semicode; }
        set { _semicode = value; }
    }
    public string registrationtable_EMAILID
    {
        get { return _emailid; }
        set { _emailid = value; }
    }
    public string registrationtable_PHONENO
    {
        get { return _phoneno; }
        set { _phoneno = value; }
    }
    public int registrationtable_STATUS
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