using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;

/// <summary>
/// Summary description for partymaster
/// </summary>
public class partymaster
{
	public partymaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    Handler objHandler = new Handler();
    Handler objhandler = new Handler();
    string xmlpath;
    public partymaster(string xmlpathh)
    {
        xmlpath = xmlpathh;
    }

    #region Private members
    private int _srno = 0;
    private string _partyname = string.Empty;
    private string _partycode = string.Empty;
    private string _partytype = string.Empty;
    private int _partytypecode =0;
    private int _partyaddon = 0;
    private string _website = string.Empty;
    private string _panno = string.Empty;
    private string _phoneno = string.Empty;
    private string _email = string.Empty;
    private string _fax = string.Empty;

    private string _cstno = string.Empty;
    private string _vatno = string.Empty;
    private string _servicetaxno = string.Empty;
    private string _exciseno = string.Empty;
    private string _exciserange = string.Empty;
    private string _excisedivision = string.Empty;
    private string _excisecollectrate = string.Empty;
    #endregion
    #region Properties
    public int partymaster_SRNO
    {
        get { return _srno; }
        set { _srno = value; }
    }
    public string partymaster_PARTYNAME
    {
        get { return _partyname; }
        set { _partyname = value; }
    }
    public string partymaster_PARTYCODE
    {
        get { return _partycode; }
        set { _partycode = value; }

     }
    public string partymaster_PARTYTYPE
    {
        get { return _partytype; }
        set { _partytype = value; }
    }
    public int partymaster_PARTYTYPECODE
    {
        get { return _partytypecode; }
        set { _partytypecode = value; }
    }
    public int partymaster_PARTYADDON
    {
        get { return _partyaddon; }
        set { _partyaddon = value; }
    }
    public string partymaster_WEBSITE
    {
        get { return _website; }
        set { _website = value; }
    }
    public string partymaster_PANNO
    {
        get { return _panno; }
        set { _panno = value; }
    }
    public string partymaster_PHONENO
    {
        get { return _phoneno; }
        set { _phoneno = value; }
    }
    public string partymaster_EMAIL
    {
        get { return _email; }
        set { _email = value; }
    }
    public string partymaster_FAX
    {
        get { return _fax; }
        set { _fax = value; }
    }
    public string partymaster_CSTNO
    {
        get { return _cstno; }
        set { _cstno = value; }
    }
    public string partymaster_VATNO
    {
        get { return _vatno; }
        set { _vatno = value; }
    }
    public string partymaster_SERVICETAXNO
    {
        get { return _servicetaxno; }
        set { _servicetaxno = value; }
    }

    public string partymaster_EXCISENO
    {
        get { return _exciseno; }
        set { _exciseno = value; }
    }
    public string partymaster_EXCISERANGE
    {
        get { return _exciserange; }
        set { _exciserange = value; }
    }
    public string partymaster_EXCISEDIVISION
    {
        get { return _excisedivision; }
        set { _excisedivision = value; }
    }
    public string partymaster_EXCISECOLLECTRATE
    {
        get { return _excisecollectrate; }
        set { _excisecollectrate = value; }
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
