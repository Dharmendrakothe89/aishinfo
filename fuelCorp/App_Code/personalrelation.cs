using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
/// <summary>
/// Summary description for personalrelation
/// </summary>
public class personalrelation
{
	public personalrelation()
	{
		//
		// TODO: Add constructor logic here
		//
	}

       Handler objHandler = new Handler();
    Handler objhandler = new Handler();
    string xmlpath;
    public personalrelation(string xmlpathh)
    {
        xmlpath = xmlpathh;
    }

    #region Private Members
    private int _srno = 0;
    private int _relationshipid = 0;
    private string _assosiatedfield = string.Empty;
    private int _groupid = 0;
    private int _assosiatedbranch = 0;
    private int _status = 0;
    #endregion

    #region Properties
    public int personalrelation_SRNO
    {
        get { return _srno; }
        set { _srno = value; }
    }

    public int personalrelation_RELATIONSHIPID
    {
        get { return _relationshipid; }
        set { _relationshipid = value; }
    }

    public string personalrelation_ASSOSIATEDFEILD
    {
        get { return _assosiatedfield; }
        set { _assosiatedfield = value; }
    }

   public int personalrelation_GROUPID
    {
        get { return _groupid; }
        set { _groupid = value; }
    }

   public int personalrelation_ASSOSIATEDBRANCH
    {
        get { return _assosiatedbranch; }
        set { _assosiatedbranch = value; }
    }
    
    public int personalrelation_STATUS
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