using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Summary description for contractmaster
/// </summary>
public class contractmaster
{
	public contractmaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    Handler objHandler = new Handler();
    Handler objhandler = new Handler();
    string xmlpath;
    public contractmaster(string xmlpathh)
    {
        xmlpath = xmlpathh;
    }
   
    #region  private members
    private int _contractid = 0;
    private string _contractwith = string.Empty;
    private int _contracttypeid = 0;
    private string _contracttype = string.Empty;
    private string _contractdate = string.Empty;
    private string _refno = string.Empty;
    private string _refdate = string.Empty;
    private string _startdate = string.Empty;
    private string _enddate = string.Empty;
    private int _partyid = 0;
    private int _partycontractpersonid = 0;
    private double _quantity = 0;
    private string _quantityper = string.Empty;
    private double _rate = 0;
    private double _servicecharge = 0;
    private int _cmpid = 0;
    private int _branchid = 0;
    private int _status = 0;
    #endregion

    #region properties
    public int contractmaster_CONTRACTID
    {
        get { return _contractid; }
        set { _contractid = value; }
    }
    public string contractmaster_CONTRACTWITH
    {
        get { return _contractwith; }
        set { _contractwith = value; }
    }
    public int contractmaster_CONTRACTTYPEID
    {
        get { return _contracttypeid; }
        set { _contracttypeid = value; }
    }
    public string contractmaster_CONTRACTTYPE
    {
        get { return _contracttype; }
        set { _contracttype = value; }
    }
    public string contractmaster_CONTRACTDATE
    {
        get { return _contractdate; }
        set { _contractdate = value; }
    }
    public string contractmaster_REFNO
    {
        get { return _refno; }
        set { _refno = value; }
    }
    public string contractmaster_REFDATE
    {
        get { return _refdate; }
        set { _refdate = value; }
    }
    public string contractmaster_STARTDATE
    {
        get { return _startdate; }
        set { _startdate = value; }
    }
    public string contractmaster_ENDDATE
    {
        get { return _enddate; }
        set { _enddate = value; }
    }
    public int contractmaster_PARTYID
    {
        get { return _partyid; }
        set { _partyid = value; }
    }
    public int contractmaster_PARTYCONTACTPERSONID
    {
        get { return _partycontractpersonid; }
        set { _partycontractpersonid = value; }
    }
    public double contractmaster_QUANTITY
    {
        get { return _quantity; }
        set { _quantity = value; }
    }
    public string contractmaster_QUANTITYPER
    {
        get { return _quantityper; }
        set { _quantityper = value; }
    }
    public double contractmaster_RATE
    {
        get { return _rate; }
        set { _rate = value; }
    }
    public double contractmaster_SERVICECHARGE
    {
        get { return _servicecharge; }
        set { _servicecharge = value; }
    }
    public int contractmaster_CMPID
    {
        get { return _cmpid; }
        set { _cmpid = value; }
    }
    public int contractmaster_BRANCHID
    {
        get { return _branchid; }
        set { _branchid = value; }
    }
    public int contractmaster_STATUS
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