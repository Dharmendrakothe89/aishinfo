using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

public class godownmaster
{
    Handler objHandler = new Handler();
    Handler objhandler = new Handler();
    string xmlpath;
	public godownmaster()
	{
	}
    public godownmaster(string xmlpathh)
    {
        xmlpath = xmlpathh;
    }

    #region Private members
    private int _srno = 0;
    private string _godownname = string.Empty;
    private int _stateid = 0;
    private string _statename = string.Empty;
    private int _cityid = 0;
    private string _cityname = string.Empty;
    private string _phone = string.Empty;
    private string _fax = string.Empty;
    private string _email = string.Empty;
    private string _address = string.Empty;
    private string _pin = string.Empty;
    private int _status = 0;
    #endregion
    #region Properties
    public int godownmaster_SRNO
    {
        get { return _srno; }
        set { _srno = value; }
    }
    public string godownmaster_GODOWNNAME
    {
        get { return _godownname; }
        set { _godownname = value; }
    }
    public int godownmaster_STATEID
    {
        get { return _stateid; }
        set { _stateid = value; }

     }
    public string godownmaster_STATENAME
    {
        get { return _statename; }
        set { _statename = value; }
    }
    public int godownmaster_CITYID
    {
        get { return _cityid; }
        set { _cityid = value; }
    }
    public string godownmaster_CITYNAME
    {
        get { return _cityname; }
        set { _cityname = value; }
    }
    public string godownmaster_PHONE
    {
        get { return _phone; }
        set { _phone = value; }
    }
    public string godownmaster_FAX
    {
        get { return _fax; }
        set { _fax = value; }
    }
    public string godownmaster_EMAIL
    {
        get { return _email; }
        set { _email = value; }


    }
    public string godownmaster_ADDRESS
    {
        get { return _address; }
        set { _address = value; }
    }
    public string godownmaster_PIN
    {
        get { return _pin; }
        set { _pin = value; }
    }
    public int godownmaster_STATUS
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
}