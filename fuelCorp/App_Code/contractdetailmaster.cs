using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Summary description for contractdetailmaster
/// </summary>
public class contractdetailmaster
{
    Handler objHandler = new Handler();
    Handler objhandler = new Handler();
    string xmlpath;
	public contractdetailmaster()
	{
		
	}
    public contractdetailmaster(string xmlpathh)
    {
        xmlpath = xmlpathh;
    }
   
    #region  private members
    private int _srno = 0;
    private int _contractid = 0;
    private string _workdetails = string.Empty;
    private string _deliverydetails = string.Empty;
    private string _supplydeleydetails = string.Empty;
    private string _excesssupply = string.Empty;
    private string _weightment = string.Empty;
    private string _securitydeposit = string.Empty;
    private string _jurisdiction = string.Empty;
    private string _compliancelaw = string.Empty;
    private string _confidentiality = string.Empty;
    private string _indemnity = string.Empty;
    private string _arbitration = string.Empty;
    private string _termination = string.Empty;
    private string _forcemajeure = string.Empty;
    private string _retentionmoney = string.Empty;
    private string _partyvalue = string.Empty;
    private int _status = 0;
    #endregion

    #region properties
    public int contractdetailmaster_SRNO
    {
        get { return _srno; }
        set { _srno = value; }
    }
    public int contractdetailmaster_CONTRACTID
    {
        get { return _contractid; }
        set { _contractid = value; }
    }
    public string contractdetailmaster_WORKDETAILS
    {
        get { return _workdetails; }
        set { _workdetails = value; }
    }
    public string contractdetailmaster_DELIVERYDETAILS
    {
        get { return _deliverydetails; }
        set { _deliverydetails = value; }
    }
    public string contractdetailmaster_SUPPLYDELEYDETAILS
    {
        get { return _supplydeleydetails; }
        set { _supplydeleydetails = value; }
    }
    public string contractdetailmaster_EXCESSSUPPLY
    {
        get { return _excesssupply; }
        set { _excesssupply = value; }
    }
    public string contractdetailmaster_WEIGHTMENT
    {
        get { return _weightment; }
        set { _weightment = value; }
    }
    public string contractdetailmaster_SECURITYDEPOSIT
    {
        get { return _securitydeposit; }
        set { _securitydeposit = value; }
    }
    public string contractdetailmaster_JURISDICTION
    {
        get { return _jurisdiction; }
        set { _jurisdiction = value; }
    }
    public string contractdetailmaster_COMPLIANCELAW
    {
        get { return _compliancelaw; }
        set { _compliancelaw = value; }
    }
    public string contractdetailmaster_CONFIDENTIALITY
    {
        get { return _confidentiality; }
        set { _confidentiality = value; }
    }
    public string contractdetailmaster_INDEMNITY
    {
        get { return _indemnity; }
        set { _indemnity = value; }
    }
    public string contractdetailmaster_ARBITRATION
    {
        get { return _arbitration; }
        set { _arbitration = value; }
    }
    public string contractdetailmaster_TERMINATION
    {
        get { return _termination; }
        set { _termination = value; }
    }
    public string contractdetailmaster_FORCEMAJEURE
    {
        get { return _forcemajeure; }
        set { _forcemajeure = value; }
    }
    public string contractdetailmaster_RETENTIONMONEY
    {
        get { return _retentionmoney; }
        set { _retentionmoney = value; }
    }
    public string contractdetailmaster_PARTYVALUE
    {
        get { return _partyvalue; }
        set { _partyvalue = value; }
    }
    public int contractdetailmaster_STATUS
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