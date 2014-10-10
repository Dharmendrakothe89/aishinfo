using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for termstable
/// </summary>
public class termstable
{
	public termstable()
	{
		//
		// TODO: Add constructor logic here
		//
	}
     Handler objHandler = new Handler();
    Handler objhandler = new Handler();
    string xmlpath;
    public termstable(string xmlpathh)
    {
        xmlpath = xmlpathh;
    }


    #region Private members
    private int _srno = 0;
    private string _partytype = string.Empty;
    private int _partyid = 0;
    private int _termsid = 0;
    private string _terms = string.Empty;
    private double _termsvalue = 0;
    private int _status = 0;
    #endregion

    #region properties
    public int termstable_SRNO
    {
        get { return _srno; }
        set { _srno = value; }
    }
    public string termstable_PARTYTYPE
    {
        get { return _partytype; }
        set { _partytype = value; }

    }
    public int termstable_PARTYID
    {
        get { return _partyid; }
        set { _partyid = value; }
    }
    public int termstable_TERMSID
    {
        get { return _termsid; }
        set { _termsid = value; }
    }
    public string termstable_TERMS
    {
        get { return _terms; }
        set { _terms = value; }

    }
    public double termstable_TERMSVALUE
    {
        get { return _termsvalue; }
        set { _termsvalue = value; }
    }
    public int termstable_STATUS
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