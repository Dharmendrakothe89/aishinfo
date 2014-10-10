using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;

/// <summary>
/// Summary description for partytaxmaster
/// </summary>
public class partytaxmaster
{
    Handler objHandler = new Handler();
    Handler objhandler = new Handler();
    string xmlpath;
	public partytaxmaster()
	{
	}

    public partytaxmaster(string xmlpathh)
    {
        xmlpath = xmlpathh;
    }

    #region Private members
    private int _srno = 0;
    private int _partyid = 0;
    private int _taxid = 0;
    private string _taxname = string.Empty;
    private double _taxvalue = 0;
    private string _taxunit = string.Empty;
    private int _status = 0;
  
    #endregion
    #region Properties
    public int partytaxmaster_SRNO
    {
        get { return _srno; }
        set { _srno = value; }
    }
    public int partytaxmaster_PARTYID
    {
        get { return _partyid; }
        set { _partyid = value; }
    }
    public int partytaxmaster_TAXID
    {
        get { return _taxid; }
        set { _taxid = value; }

     }
    public string partytaxmaster_TAXNAME
    {
        get { return _taxname; }
        set { _taxname = value; }
    }
    public double partytaxmaster_TAXVALUE
    {
        get { return _taxvalue; }
        set { _taxvalue = value; }
    }
    public string partytaxmaster_TAXUNIT
    {
        get { return _taxunit; }
        set { _taxunit = value; }
    }
    public int partytaxmaster_STATUS
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