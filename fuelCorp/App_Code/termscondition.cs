using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for termscondition
/// </summary>
public class termscondition
{
	public termscondition()
	{
		//
		// TODO: Add constructor logic here
		//
	}
     Handler objHandler = new Handler();
    Handler objhandler = new Handler();
    string xmlpath;
    public termscondition(string xmlpathh)
    {
        xmlpath = xmlpathh;
    }
   
    #region  private members
    private int _srno = 0;
    private string _category = string.Empty;
    private string _subcategory = string.Empty;
    private string _terms = string.Empty;
    private int _termsvalue = 0;
    private string _description = string.Empty;
    private int _status = 0;
    #endregion

    #region properties
    public int termscondition_SRNO
    {
        get { return _srno; }
        set { _srno = value; }
    }
    
    public string termscondition_CATEGORY
    {
        get { return _category; }
        set { _category = value; }
    }
     public string termscondition_SUBCATEGORY
    {
        get { return _subcategory; }
        set { _subcategory = value; }
    }
    public string termscondition_TERMS
    {
        get { return _terms; }
        set { _terms = value; }
    }
    public int termscondition_TERMSVALUE
    {
        get { return _termsvalue; }
        set { _termsvalue = value; }

    }

    public string termscondition_DESCRIPTION
    {
        get { return _description; }
        set { _description = value; }
    }

    public int termscondition_STATUS
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