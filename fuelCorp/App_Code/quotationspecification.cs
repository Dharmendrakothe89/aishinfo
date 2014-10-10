using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for quotationspecification
/// </summary>
public class quotationspecification
{
	public quotationspecification()
	{
		//
		// TODO: Add constructor logic here
		//
	}
     Handler objHandler = new Handler();
    Handler objhandler = new Handler();
    string xmlpath;
    public quotationspecification(string xmlpathh)
    {
        xmlpath = xmlpathh;
    }

    #region private members
    private int _srno = 0;
    private int _quotationid = 0;
    private string _coaltype = string.Empty;
    private int _coaltypeid = 0;
    private string _coalsource = string.Empty;
    private int _coalsourceid = 0;
    private double _coalquantity = 0;
    private double _coalrate = 0;
    private double _coalsizemin = 0;
    private double _coalsizemax = 0;
    private double _gcv = 0;
    private double _gcverror = 0;
    private double _moisture = 0;
    private double _moistureerror = 0;
    #endregion



    #region Properties
    public int quotationspecification_SRNO
    {
        get { return _srno; }
        set{  _srno=value;}
    }


     public int quotationspecification_QUOTATIONID
    {
        get { return _quotationid; }
        set{  _quotationid=value;}
    }

     public string quotationspecification_COALTYPE
    {
        get { return _coaltype; }
        set{  _coaltype=value;}
    }

     public int quotationspecification_COALTYPEID
    {
        get { return _coaltypeid; }
        set{ _coaltypeid =value;}
    }

     public string  quotationspecification_COALSOURCE
    {
        get { return _coalsource; }
        set{  _coalsource=value;}

    }
     public int quotationspecification_COALSOURCEID
    {
        get { return _coalsourceid; }
        set{  _coalsourceid=value;}
    }
     public double quotationspecification_COALQUANTITY
    {
        get { return _coalquantity; }
        set{ _coalquantity =value;}
    }
     public double quotationspecification_COALRATE
    {
        get { return _coalrate; }
        set{ _coalrate =value;}
    }
     public double quotationspecification_COALSIZEMIN
    {
        get { return _coalsizemin; }
        set{ _coalsizemin =value;}
    }

     public double quotationspecification_COALSIZEMAX
     {
         get { return _coalsizemax; }
         set { _coalsizemax = value; }
     }
     public double quotationspecification_GCV
     {
         get { return _gcv; }
         set { _gcv = value; }
     }
     public double quotationspecification_GCVERROR
     {
         get { return _gcverror; }
         set { _gcverror = value; }
     }

     public double quotationspecification_MOISTURE
     {
         get { return _moisture; }
         set { _moisture = value; }
     }
     public double quotationspecification_MOISTUREERROR
     {
         get { return _moistureerror; }
         set { _moistureerror = value; }
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