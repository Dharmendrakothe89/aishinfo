using System;
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
    #region privatemembers

    private int _srno = 0;
    private string _name = string.Empty;
    private string _designation = string.Empty;
    private string _department = string.Empty;
    private string _gender = string.Empty;
    private string _phone = string.Empty;
    private string _email = string.Empty;
    private string _userid = string.Empty;
    private string _password = string.Empty;
    private int _status = 0;
    #endregion


    #region  Properties

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

    public string usertable_DESIGNATION
    {
        get { return _designation; }
        set { _designation = value; }

    }

    public string usertable_DEPARTMENT
    {
        get { return _department; }
        set { _department = value; }
    }
    public string usertable_GENDER
    {
        get { return _gender; }
        set { _gender = value; }

    }

    public string usertable_PHONE
    {
        get { return _phone; }
        set { _phone = value; }
    }


    public string usertable_EMAIL
    {
        get { return _email; }
        set { _email = value; }

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