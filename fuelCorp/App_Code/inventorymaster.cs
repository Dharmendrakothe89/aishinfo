using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Summary description for inventorymaster
/// </summary>
public class inventorymaster
{
    public inventorymaster()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    Handler objHandler = new Handler();
    Handler objhandler = new Handler();
    string xmlpath;
    public inventorymaster(string xmlpathh)
    {
        xmlpath = xmlpathh;
    }
    #region  private members
    private int _srno = 0;
    private string _date = string.Empty;
    private int _cmpid = 0;
    private int _coalid = 0;
    private string _coaltype = string.Empty;
    private int _gradeid = 0;
    private string _grade = string.Empty;
    private double _quantity = 0;
    private string _type = string.Empty;
    private int _doid = 0;
    private int _partyid = 0;
    private int _transporterid = 0;
    private int _vehicleid = 0;
    private string _trnsactiontype = string.Empty;
    private int _depotid = 0;
    private string _destination = string.Empty;
    private int _status = 0;
    #endregion

    #region properties
    public int inventorymaster_SRNO
    {
        get { return _srno; }
        set { _srno = value; }
    }
    public string inventorymaster_DATE
    {
        get { return _date; }
        set { _date = value; }
    }
    public int inventorymaster_CMPID
    {
        get { return _cmpid; }
        set { _cmpid = value; }
    }
    public int inventorymaster_COALID
    {
        get { return _coalid; }
        set { _coalid = value; }
    }
    public string inventorymaster_COALTYPE
    {
        get { return _coaltype; }
        set { _coaltype = value; }
    }
    public int inventorymaster_GRADEID
    {
        get { return _gradeid; }
        set { _gradeid = value; }
    }


    public string inventorymaster_GRADE
    {
        get { return _grade; }
        set { _grade = value; }
    }
    public double inventorymaster_QUANTITY
    {
        get { return _quantity; }
        set { _quantity = value; }
    }
    public string inventorymaster_TYPE
    {
        get { return _type; }
        set { _type = value; }
    }
    public int inventorymaster_DOID
    {
        get { return _doid; }
        set { _doid = value; }
    }
    public int inventorymaster_PARTYID
    {
        get { return _partyid; }
        set { _partyid = value; }
    }
    public int inventorymaster_TRANSPORTERID
    {
        get { return _transporterid; }
        set { _transporterid = value; }
    }
    public int inventorymaster_VEHICLEID
    {
        get { return _vehicleid; }
        set { _vehicleid = value; }
    }
    public string inventorymaster_TRNASACTIONTYPE
    {
        get { return _trnsactiontype; }
        set { _trnsactiontype = value; }
    }
    public int inventorymaster_DEPOTID
    {
        get { return _depotid; }
        set { _depotid = value; }
    }
    public string inventorymaster_DESTINATION
    {
        get { return _destination; }
        set { _destination = value; }
    }
    public int inventorymaster_STATUS
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

    #endregion Handler objHandler = new Handler();

}