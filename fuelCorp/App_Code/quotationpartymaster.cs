using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Summary description for quotationpartymaster
/// </summary>
public class quotationpartymaster
{
	public quotationpartymaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
          Handler objHandler = new Handler();
    Handler objhandler = new Handler();
    string xmlpath;
    public quotationpartymaster(string xmlpathh)
    {
        xmlpath = xmlpathh;
    }
    #region privatemembers
    //TRANSPORTATIONCOST
    private int _srno = 0;
    private string _partyname = string.Empty;
    private string _partycode = string.Empty;
    private string _address = string.Empty;
    private string _contactno = string.Empty;
    #endregion

    
    #region Properties
    public int quotationpartymaster_SRNO
    {
        get { return _srno; }
        set { _srno = value; }
    }
    public string quotationpartymaster_PARTYNAME
    {
        get { return _partyname; }
        set { _partyname = value; }
    }
    public string quotationpartymaster_PARTYCODE
    {
        get { return _partycode; }
        set { _partycode = value; }
    }
    public string quotationpartymaster_ADDRESS
    {
        get { return _address; }
        set { _address = value; }
    }
    public string quotationpartymaster_CONTACTNO
    {
        get { return _contactno; }
        set { _contactno = value; }
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