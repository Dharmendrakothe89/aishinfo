using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Summary description for collierydetailmaster
/// </summary>
public class collierydetailmaster
{
	public collierydetailmaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    
    Handler objHandler = new Handler();
    Handler objhandler = new Handler();
    string xmlpath;
    public collierydetailmaster(string xmlpathh)
    {
        xmlpath = xmlpathh;
    }

    #region Private members
    private int _srno = 0;
    private int _collieryid = 0;
    private int _coalid = 0;
    private string _grade = string.Empty;
    private double _notifiedprice = 0;
    private double _commbenifitcharges =0;
    private double _crushingcharges = 0;
    private double _stc = 0;
    private double _sed = 0;
    private double _cec = 0;
    private double _royalty = 0;
    private double _mprdtax = 0;
    private double _transittax = 0;
    private double _entryfee = 0;
    private int _status = 0;
    #endregion
    #region Properties
    public int collierydetailmaster_SRNO
    {
        get { return _srno; }
        set { _srno = value; }
    }
    public int collierydetailmaster_COALID
    {
        get { return _coalid; }
        set { _coalid = value; }
    }
    public int collierydetailmaster_COLLIERYID
    {
        get { return _collieryid; }
        set { _collieryid = value; }
    }
    public string collierydetailmaster_GRADE
    {
        get { return _grade; }
        set { _grade = value; }

     }
    public double collierydetailmaster_NOTIFIEDPRICE
    {
        get { return _notifiedprice; }
        set { _notifiedprice = value; }
    }
    public double collierydetailmaster_COMMBENIFITCHARGES
    {
        get { return _commbenifitcharges; }
        set { _commbenifitcharges = value; }
    }
    public double collierydetailmaster_CRUSHINGCHARGES
    {
        get { return _crushingcharges; }
        set { _crushingcharges = value; }
    }
    public double collierydetailmaster_STC
    {
        get { return _stc; }
        set { _stc = value; }
    }
    public double collierydetailmaster_SED
    {
        get { return _sed; }
        set { _sed = value; }
    }
    public double collierydetailmaster_CEC
    {
        get { return _cec; }
        set { _cec = value; }
    }
    public double collierydetailmaster_ROYALTY
    {
        get { return _royalty; }
        set { _royalty = value; }
    }
    public double collierydetailmaster_MPRDTAX
    {
        get { return _mprdtax; }
        set { _mprdtax = value; }
    }
    public double collierydetailmaster_TRANSITTAX
    {
        get { return _transittax; }
        set { _transittax = value; }
    }
    public double collierydetailmaster_ENTRYFEE
    {
        get { return _entryfee; }
        set { _entryfee = value; }
    }

    public int collierydetailmaster_STATUS
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