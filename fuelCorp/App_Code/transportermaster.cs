using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Summary description for transportermaster
/// </summary>
public class transportermaster
{
    Handler objHandler = new Handler();
    Handler objhandler = new Handler();
    string xmlpath;
	public transportermaster()
	{
	}

    public transportermaster(string xmlpathh)
    {
        xmlpath = xmlpathh;
    }

    #region Private members
    private int _srno = 0;
    private string _transportername = string.Empty;
    private string _transportercode = string.Empty;
    private string _trantype = string.Empty;
    private int _trantypecode = 0;
    private string _address = string.Empty;
    private string _mobileno = string.Empty;
    private string _fax = string.Empty;
    private string _emailid = string.Empty;

    private string _panno = string.Empty;
    private string _servicetaxno = string.Empty;
    private int _status = 0;
    #endregion
    
    #region Properties
    public int transportermaster_SRNO
    {
        get { return _srno; }
        set { _srno = value; }
    }
    public string transportermaster_TRANSPORTERNAME
    {
        get { return _transportername; }
        set { _transportername = value; }
    }
    public string transportermaster_TRANSPORTERCODE
    {
        get { return _transportercode; }
        set { _transportercode = value; }

     }
    public string transportermaster_TRANTYPE
    {
        get { return _trantype; }
        set { _trantype = value; }
    }
    public int transportermaster_TRANTYPECODE
    {
        get { return _trantypecode; }
        set { _trantypecode = value; }
    }
    public string transportermaster_ADDRESS
    {
        get { return _address; }
        set { _address = value; }
    }
    public string transportermaster_MOBILENO
    {
        get { return _mobileno; }
        set { _mobileno = value; }
    }
    public string transportermaster_FAX
    {
        get { return _fax; }
        set { _fax = value; }


    }
    public string transportermaster_EMAILID
    {
        get { return _emailid; }
        set { _emailid = value; }
    }
    public string transportermaster_PANNO
    {
        get { return _panno; }
        set { _panno = value; }
    }
    public string transportermaster_SERVICETAXNO
    {
        get { return _servicetaxno; }
        set { _servicetaxno = value; }
    }
    public int transportermaster_STATUS
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