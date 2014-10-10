using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

public class domaster
{
	public domaster()
	{
	}
    Handler objHandler = new Handler();
    Handler objhandler = new Handler();
    string xmlpath;
    public domaster(string xmlpathh)
    {
        xmlpath = xmlpathh;
    }
    #region  private members
    private int _doid = 0;
    private string _dofor = string.Empty;
    private int _partyid = 0;
    private string _dono = string.Empty;
    private string _dodate = string.Empty;
    private int _collieryid = 0;
    private int _monthid = 0;
    private string _month = string.Empty;
    private string _destination = string.Empty;
    private string _date = string.Empty;
    private int _cmpid = 0;
    private int _status = 0;
    #endregion

    #region properties
    public int domaster_DOID
    {
        get { return _doid; }
        set { _doid = value; }
    }
    public string domaster_DOFOR
    {
        get { return _dofor; }
        set { _dofor = value; }
    }
    public int domaster_PARTYID
    {
        get { return _partyid; }
        set { _partyid = value; }
    }
    public string domaster_DONO
    {
        get { return _dono; }
        set { _dono = value; }
    }
    public string domaster_DODATE
    {
        get { return _dodate; }
        set { _dodate = value; }
    }
    public int domaster_COLLIERYID
    {
        get { return _collieryid; }
        set { _collieryid = value; }
    }
    public int domaster_MONTHID
    {
        get { return _monthid; }
        set { _monthid = value; }
    }
    public string domaster_MONTH
    {
        get { return _month; }
        set { _month = value; }
    }
    public string domaster_DESTINATION
    {
        get { return _destination; }
        set { _destination = value; }
    }
    public int domaster_CMPID
    {
        get { return _cmpid; }
        set { _cmpid = value; }
    }
    public int domaster_STATUS
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

    #endregion Handler objHandler = new Handler();
   
}