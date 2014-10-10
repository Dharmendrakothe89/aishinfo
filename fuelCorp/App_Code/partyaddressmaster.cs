using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;

/// <summary>
/// Summary description for partyaddressmaster
/// </summary>
public class partyaddressmaster
{
    Handler objHandler = new Handler();
    Handler objhandler = new Handler();
    string xmlpath;
	public partyaddressmaster()
	{
	}
    public partyaddressmaster(string xmlpathh)
    {
        xmlpath = xmlpathh;
    }

    #region Private members
    private int _srno = 0;
    private int _partyid = 0;
    private int _addresstype = 0;
    private string _statename = string.Empty;
    private int _stateid = 0;
    private string _cityname = string.Empty;
    private int _cityid = 0;
    private string _pincode = string.Empty;
    private string _address = string.Empty;
    private int _status = 0;
    #endregion
    #region Properties
    public int partyaddressmaster_SRNO
    {
        get { return _srno; }
        set { _srno = value; }
    }
    public int partyaddressmaster_PARTYID
    {
        get { return _partyid; }
        set { _partyid = value; }
    }
    public int partyaddressmaster_ADDRESSTYPE
    {
        get { return _addresstype; }
        set { _addresstype = value; }

     }
    public string partyaddressmaster_STATENAME
    {
        get { return _statename; }
        set { _statename = value; }
    }
    public int partyaddressmaster_STATEID
    {
        get { return _stateid; }
        set { _stateid = value; }
    }
    public string partyaddressmaster_CITYNAME
    {
        get { return _cityname; }
        set { _cityname = value; }
    }
    public int partyaddressmaster_CITYID
    {
        get { return _cityid; }
        set { _cityid = value; }
    }
    public string partyaddressmaster_PINCODE
    {
        get { return _pincode; }
        set { _pincode = value; }


    }
    public string partyaddressmaster_ADDRESS
    {
        get { return _address; }
        set { _address = value; }
    }
    public int partyaddressmaster_STATUS
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