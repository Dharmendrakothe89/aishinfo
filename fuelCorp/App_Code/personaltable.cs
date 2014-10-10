using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
/// <summary>
/// Summary description for personaltable
/// </summary>
public class personaltable
{
	public personaltable()
	{
	}
    
    Handler objHandler = new Handler();
    Handler objhandler = new Handler();
    string xmlpath;
    public personaltable(string xmlpathh)
    {
        xmlpath = xmlpathh;
    }

    #region Private Members

    private int _relationshipid = 0;
    private string _initial = string.Empty;
    private string _firstName = string.Empty;
    private string _middleName = string.Empty;
    private string _lastName = string.Empty;
    private string _minitial = string.Empty;
    private string _mfirstName = string.Empty;
    private string _mmiddleName = string.Empty;
    private string _mlastName = string.Empty;
    private string _branchname = string.Empty;
    private int _branchid = 0;
    private string _gender = string.Empty;
    private string _martialstatus = string.Empty;
    private string _dob = string.Empty;
    private string _pan = string.Empty;
    #endregion


    #region Properties
    public int personaltable_RELATIONSHIPID
    {
        get { return _relationshipid; }
        set { _relationshipid = value; }
    }
    public string personaltable_INITIAL
    {
        get { return _initial; }
        set { _initial = value; }
    }
    public string personaltable_FIRSTNAME
    {
        get { return _firstName; }
        set { _firstName = value; }
    }
    public string personaltable_MIDDLENAME
    {
        get { return _middleName; }
        set { _middleName = value; }
    }
    public string personaltable_LASTNAME
    {
        get { return _lastName; }
        set { _lastName = value; }
    }
    public string personaltable_MINITIAL
    {
        get { return _minitial; }
        set { _minitial = value; }
    }
    public string personaltable_MFIRSTNAME
    {
        get { return _mfirstName; }
        set { _mfirstName = value; }
    }
    public string personaltable_MMIDDLENAME
    {
        get { return _mmiddleName; }
        set { _mmiddleName = value; }
    }
    public string personaltable_MLASTNAME
    {
        get { return _mlastName; }
        set { _mlastName = value; }
    }
    public string personaltable_BRANCHNAME
    {
        get { return _branchname; }
        set { _branchname = value; }
    }
    public int personaltable_BRANCHID
    {
        get { return _branchid; }
        set { _branchid = value; }
    }
    public string personaltable_GENDER
    {
        get { return _gender; }
        set { _gender = value; }
    }
    public string personaltable_MARITALSTATUS
    {
        get { return _martialstatus; }
        set { _martialstatus = value; }
    }
    public string personaltable_DOB
    {
        get { return _dob; }
        set { _dob = value; }
    }
    public string personaltable_PAN
    {
        get { return _pan; }
        set { _pan = value; }
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