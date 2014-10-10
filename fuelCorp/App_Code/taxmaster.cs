using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for taxmaster
/// </summary>
public class taxmaster
{
	public taxmaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}

     Handler objHandler = new Handler();
    Handler objhandler = new Handler();
    string xmlpath;
    public taxmaster(string xmlpathh)
    {
        xmlpath = xmlpathh;
    }
    #region privatemembers

    private int _srno = 0;
    private string _taxname = string.Empty;
    private double _taxvalue = 0;
    private string _taxunit = string.Empty;
    private int _status = 0;
    #endregion

    #region properties
    public int taxmaster_SRNO
    {
        get { return _srno; }
        set { _srno = value; }
    }

    public string taxmaster_TAXNAME
    {
        get { return _taxname; }
        set { _taxname = value; }
    }
    public double taxmaster_TAXVALUE
    {
        get { return _taxvalue; }
        set { _taxvalue = value; }
    }
    public string taxmaster_TAXUNIT
    {
        get { return _taxunit; }
        set { _taxunit = value; }
    }
    public int taxmaster_STATUS
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