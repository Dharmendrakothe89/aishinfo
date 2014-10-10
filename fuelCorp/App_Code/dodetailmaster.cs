using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Summary description for dodetailmaster
/// </summary>
public class dodetailmaster
{
    public dodetailmaster()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    Handler objHandler = new Handler();
    Handler objhandler = new Handler();
    string xmlpath;
    public dodetailmaster(string xmlpathh)
    {
        xmlpath = xmlpathh;
    }


    #region  private members
    private int _srno = 0;
    private int _coalid = 0;
    private int _doid = 0;
    private string _grade = string.Empty;
    private double _quantity = 0;
    private double _basiccharges = 0;
    private double _sizingcharges = 0;
    private double _benifitcharges = 0;
    private double _stc = 0;
    private double _sed = 0;
    
    private double _mprdtax = 0;
    private double _transittax = 0;
    private double _entryfee = 0;
    private double _royalty = 0;
    private double _centalexcise = 0;
    private double _cleanenergyness = 0;
    private double _vatcst = 0;
    private double _tcs = 0;
    private double _totalamount = 0;
    private int _status = 0;
    #endregion

    #region properties
    public int dodetailmaster_SRNO
    {
        get { return _srno; }
        set { _srno = value; }
    }
    public int dodetailmaster_COALID
    {
        get { return _coalid; }
        set { _coalid = value; }
    }
    public int dodetailmaster_DOID
    {
        get { return _doid; }
        set { _doid = value; }
    }
    public string dodetailmaster_GRADE
    {
        get { return _grade; }
        set { _grade = value; }
    }
    public double dodetailmaster_QUANTITY
    {
        get { return _quantity; }
        set { _quantity = value; }
    }
    public double dodetailmaster_BASICCHARGES
    {
        get { return _basiccharges; }
        set { _basiccharges = value; }
    }
    public double dodetailmaster_SIZINGCHARGES
    {
        get { return _sizingcharges; }
        set { _sizingcharges = value; }
    }
    public double dodetailmaster_BENIFITCHARGES
    {
        get { return _benifitcharges; }
        set { _benifitcharges = value; }
    }
    public double dodetailmaster_STC
    {
        get { return _stc; }
        set { _stc = value; }
    }
    public double dodetailmaster_SED
    {
        get { return _sed; }
        set { _sed = value; }
    }
    
    public double dodetailmaster_MPRDTAX
    {
        get { return _mprdtax; }
        set { _mprdtax = value; }
    }
    public double dodetailmaster_TRANSITTAX
    {
        get { return _transittax; }
        set { _transittax = value; }
    }
    public double dodetailmaster_ENTRYFEE
    {
        get { return _entryfee; }
        set { _entryfee = value; }
    }
    public double dodetailmaster_ROYALTY
    {
        get { return _royalty; }
        set { _royalty = value; }
    }
    public double dodetailmaster_CENTALEXCISE
    {
        get { return _centalexcise; }
        set { _centalexcise = value; }
    }
    public double dodetailmaster_CLEANENERGYNESS
    {
        get { return _cleanenergyness; }
        set { _cleanenergyness = value; }
    }
    public double dodetailmaster_VATCST
    {
        get { return _vatcst; }
        set { _vatcst = value; }
    }
    public double dodetailmaster_TCS
    {
        get { return _tcs; }
        set { _tcs = value; }
    }


     public double dodetailmaster_TOTALAMOUNT
    {
        get { return _totalamount; }
        set { _totalamount = value; }
    }
    public int dodetailmaster_STATUS
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