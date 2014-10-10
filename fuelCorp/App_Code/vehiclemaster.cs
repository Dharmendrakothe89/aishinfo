using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for vehiclemaster
/// </summary>
public class vehiclemaster
{
     Handler objHandler = new Handler();
    Handler objhandler = new Handler();
    string xmlpath;
	public vehiclemaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    
    public vehiclemaster(string xmlpathh)
    {
        xmlpath = xmlpathh;
    }

    #region Private members
    private int _vehicleid = 0;
    private int _transporterid = 0;
    private string _vehiclename = string.Empty;
    private string _vehicleno = string.Empty;
    private string _capacity = string.Empty;
    private int _status = 0;
    #endregion
    
    #region Properties
    public int vehiclemaster_VEHICLEID
    {
        get { return _vehicleid; }
        set { _vehicleid = value; }
    }

    public int vehiclemaster_TRANSPORTERID
    {
        get { return _transporterid; }
        set { _transporterid = value; }
    }
    public string vehiclemaster_VEHICLENAME
    {
        get { return _vehiclename; }
        set { _vehiclename = value; }
    }
    public string vehiclemaster_VEHICLENO
    {
        get { return _vehicleno; }
        set { _vehicleno = value; }
    }
    public string vehiclemaster_CAPACITY
    {
        get { return _capacity; }
        set { _capacity = value; }


    }
    public int vehiclemaster_STATUS
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