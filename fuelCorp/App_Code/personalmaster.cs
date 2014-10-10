using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
/// <summary>
/// Summary description for personalmaster
/// </summary>
public class personalmaster
{
    Handler objHandler = new Handler();
    Handler objhandler = new Handler();
    string xmlpath;
	public personalmaster()
	{
	}

    public personalmaster(string xmlpathh)
    {
        xmlpath = xmlpathh;
    }

    #region Private members
    private int _srno = 0;
    private string _personname = string.Empty;
    private string _designation = string.Empty;
    private string _department = string.Empty;
    private string _phoneno =string.Empty;
    private string _mobile = string.Empty;
    private string _fax = string.Empty;
    private string _emailid = string.Empty;
    private int _personrelationid = 0;
    private string _persontype = string.Empty;
    

    #endregion
    #region Properties
    public int personalmaster_SRNO
    {
        get { return _srno; }
        set { _srno = value; }
    }
    public string personalmaster_PERSONNAME
    {
        get { return _personname; }
        set { _personname = value; }
    }
    public string personalmaster_DESIGNATION
    {
        get { return _designation; }
        set { _designation = value; }

     }
    public string personalmaster_DEPARTMENT
    {
        get { return _department; }
        set { _department = value; }
    }
    public string personalmaster_PHONENO
    {
        get { return _phoneno; }
        set { _phoneno = value; }
    }
    public string personalmaster_MOBILE
    {
        get { return _mobile; }
        set { _mobile = value; }
    }
    public string personalmaster_FAX
    {
        get { return _fax; }
        set { _fax = value; }
    }
    public string personalmaster_EMAILID
    {
        get { return _emailid; }
        set { _emailid = value; }


    }
    public int personalmaster_PERSONRELATIONID
    {
        get { return _personrelationid; }
        set { _personrelationid = value; }
    }
    public string personalmaster_PERSONTYPE
    {
        get { return _persontype; }
        set { _persontype = value; }
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