using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for lookupmaster
/// </summary>
public class lookupmaster
{
	public lookupmaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
     Handler objHandler = new Handler();
    Handler objhandler = new Handler();
    string xmlpath;
    public lookupmaster(string xmlpathh)
    {
        xmlpath = xmlpathh;
    }
    //[SRNO]
    //  ,[NAME]
    //  ,[VALUE]
    //  ,[HEADID]
    //  ,[STATUS]
    #region Private members
    private int _srno = 0;
    private string _name = string.Empty;
    private double  _value = 0;
    private int _headid = 0;
    private int _status = 0;

    #endregion

    #region Properties

    public int lookupmaster_SRNO
    {
        get { return _srno; }
        set { _srno = value; }
    }

    public string lookupmaster_NAME
    {
        get{return _name;}
        set{_name=value;}

    }
    public double  lookupmaster_VALUE
    {
        get { return _value; }
        set { _value = value; }
    }

        public int lookupmaster_HEADID
        {
            get{return _headid;}
            set{_headid=value;}

        }

        public int lookupmaster_STATUS
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