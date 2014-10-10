using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Summary description for paymentmaster
/// </summary>
public class paymentmaster
{
	public paymentmaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    Handler objHandler = new Handler();
    Handler objhandler = new Handler();
    string xmlpath;
    public paymentmaster(string xmlpathh)
    {
        xmlpath = xmlpathh;
    }


    #region  private members
    private int _srno = 0;
    private string _relationtype = string.Empty;
    private int _relationid = 0;
    private string _paymenttype = string.Empty;
    private int _paymentno = 0;
    private string _paymentdate = string.Empty;
    private string _bankname = string.Empty;
    private double _amount = 0;
    private int _status = 0;
    #endregion

    #region properties
    public int paymentmaster_SRNO
    {
        get { return _srno; }
        set { _srno = value; }
    }
    public string paymentmaster_RELATIONTYPE
    {
        get { return _relationtype; }
        set { _relationtype = value; }
    }
    public int paymentmaster_RELATIONID
    {
        get { return _relationid; }
        set { _relationid = value; }
    }
    public string paymentmaster_PAYMENTTYPE
    {
        get { return _paymenttype; }
        set { _paymenttype = value; }
    }
    public int paymentmaster_PAYMENTNO
    {
        get { return _paymentno; }
        set { _paymentno = value; }
    }
    public string paymentmaster_PAYMENTDATE
    {
        get { return _paymentdate; }
        set { _paymentdate = value; }
    }
    public string paymentmaster_BANKNAME
    {
        get { return _bankname; }
        set { _bankname = value; }
    }
    public double paymentmaster_AMOUNT
    {
        get { return _amount; }
        set { _amount = value; }
    }
    public int paymentmaster_STATUS
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