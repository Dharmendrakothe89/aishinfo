﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Summary description for coalmaster
/// </summary>
public class coalmaster
{
	public coalmaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
      Handler objHandler = new Handler();
    Handler objhandler = new Handler();
    string xmlpath;
    public coalmaster(string xmlpathh)
    {
        xmlpath = xmlpathh;
    }

    #region Private Members
    private int _srno = 0;
    private string _coaltype = string.Empty;
    private string _grade = string.Empty;
    private int _coaltypeid = -1;
    private int _status = 0;
    #endregion

    #region Properties
    public int coalmaster_SRNO
    {
        get { return _srno; }
        set { _srno = value; }
    }
    public string coalmaster_COALTYPE
    {
        get { return _coaltype; }
        set { _coaltype = value; }
    }
    public string coalmaster_GRADE
    {
        get { return _grade; }
        set { _grade = value; }
    }
    public int coalmaster_COALTYPEID
    {
        get { return _coaltypeid; }
        set { _coaltypeid = value; }
    }
    public int coalmaster_STATUS
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