﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for usertable
/// </summary>
public class usertable
{
    public usertable()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    Handler objHandler = new Handler();
    Handler objhandler = new Handler();
    string xmlpath;
    public usertable(string xmlpathh)
    {
        xmlpath = xmlpathh;
    }

    #region Private Members
    private int _srno = 0;
    private string _name = string.Empty;
    private string _role = string.Empty;
    private string _userid = string.Empty;
    private string _password = string.Empty;
    private int _relationshipid = 0;
    private int _status = 0;
    #endregion

    #region Properties
    public int usertable_SRNO
    {
        get { return _srno; }
        set { _srno = value; }
    }
    public string usertable_NAME
    {
        get { return _name; }
        set { _name = value; }
    }
    public string usertable_ROLE
    {
        get { return _role; }
        set { _role = value; }
    }
    public string usertable_USERID
    {
        get { return _userid; }
        set { _userid = value; }
    }
    public string usertable_PASSWORD
    {
        get { return _password; }
        set { _password = value; }
    }
    public int usertable_RELATIONSHIPID
    {
        get { return _relationshipid; }
        set { _relationshipid = value; }
    }
    public int usertable_STATUS
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