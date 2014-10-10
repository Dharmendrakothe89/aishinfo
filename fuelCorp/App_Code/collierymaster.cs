using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Summary description for collierymaster
/// </summary>
public class collierymaster
{
    public collierymaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    Handler objHandler = new Handler();
    Handler objhandler = new Handler();
    string xmlpath;
    public collierymaster(string xmlpathh)
    {
        xmlpath = xmlpathh;
    }

    #region Private members
    private int _srno = 0;
    private string _collieryname = string.Empty;
    private string _collierycode = string.Empty;
    private string _collieryregion = string.Empty;
    private int _cmpid = 0;
    private int _status =0;
    
    #endregion
    #region Properties
    public int collierymaster_SRNO
    {
        get { return _srno; }
        set { _srno = value; }
    }
    public string collierymaster_COLLIERYNAME
    {
        get { return _collieryname; }
        set { _collieryname = value; }
    }
    public string collierymaster_COLLIERYCODE
    {
        get { return _collierycode; }
        set { _collierycode = value; }

     }
    public string collierymaster_COLLIERYREGION
    {
        get { return _collieryregion; }
        set { _collieryregion = value; }
    }
    public int collierymaster_CMPID
    {
        get { return _cmpid; }
        set { _cmpid = value; }
    }
    public int collierymaster_STATUS
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