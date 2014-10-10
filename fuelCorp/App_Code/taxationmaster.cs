using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for taxationmaster
/// </summary>
public class taxationmaster
{
	public taxationmaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    Handler objHandler = new Handler();
    Handler objhandler = new Handler();
    string xmlpath;
    public taxationmaster(string xmlpathh)
    {
        xmlpath = xmlpathh;
    }
    #region privatemembers
    private int _srno = 0;
    private string _taxpartytype = string.Empty;
    private int _taxpartyid = 0;
    private int _taxid = 0;
    private string _taxname = string.Empty;
    private double _taxvalue = 0;
    private string _taxunit = string.Empty;
    private int _status = 0;

    #endregion


    #region properties
    public int taxationmaster_SRNO
    {
        get { return _srno; }
        set { _srno = value; }
    }

    public string taxationmaster_TAXPARTYTYPE
    {
        get { return _taxpartytype; }
        set { _taxpartytype = value; }
    }

    public int taxationmaster_TAXPARTYID
    {
        get { return _taxpartyid; }
        set { _taxpartyid = value; }
    }
    public int taxationmaster_TAXID
    {
        get { return _taxid; }
        set { _taxid = value; }
    }
    public string  taxationmaster_TAXNAME
    {
        get { return _taxname; }
        set { _taxname = value; }
    }
    public double taxationmaster_TAXVALUE
    {
        get { return _taxvalue; }
        set { _taxvalue = value; }
    }
    public string taxationmaster_TAXUNIT
    {
        get { return _taxunit; }
        set { _taxunit = value; }
    }
    public int taxationmaster_STATUS
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