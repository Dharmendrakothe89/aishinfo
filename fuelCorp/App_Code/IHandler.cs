using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for IHandler
/// </summary>
public interface IHandler
{
    bool Insert(bool flag, string tableName, object objClass, string pathXML);
    bool CreateSQL(bool flag, string tableName);
}
