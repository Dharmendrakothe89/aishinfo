using System;
using System.Data;
using System.Configuration;
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

using System.Collections;
using System.Text;
using System.Xml;
using System.IO;
using System.Globalization;

using System.Net;
using System.Net.Mail;

using System.Net.Configuration;



public class General
{
    public General()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static T Parse<T>(string parameter)
    {
        if (parameter != string.Empty)
            return (T)System.Convert.ChangeType(parameter, Type.GetTypeCode(typeof(T)));
        else
            return (T)System.Convert.ChangeType(0, Type.GetTypeCode(typeof(T)));
    }
   
    public static string ConvertDateTime(string value, string FromFormat, string ToFormat)
    {
        try
        {
            DateTime dtout;
            if (DateTime.TryParseExact(value, FromFormat, null, System.Globalization.DateTimeStyles.None, out dtout))
            {
                return dtout.ToString(ToFormat);
            }

            return string.Empty;
        }
        catch (Exception ex)
        {
            throw ex;
            return string.Empty;
        }
    }
    #region Mail/SMS

    public static void SendMail(string smsemailid, string userName, string userPassword, string userHost, int userPort, string subject, string body, string toAddress, string cc, string bcc)
    {
        if (!String.IsNullOrEmpty(toAddress))
        {

            body = body + "<br/><br /><br /><br /><br /><div><b>This is the system generated e-mail.</b></div>";

            //SmtpSection smtpsetting = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            SmtpSection smtpsetting = new SmtpSection();
            smtpsetting.From = userName;
            smtpsetting.Network.UserName = userName;
            smtpsetting.Network.Password = userPassword;
            smtpsetting.Network.Port = userPort;
            smtpsetting.Network.Host = userHost;

            string fromAddress = smtpsetting.From;

            //MailMessage message = new MailMessage(fromAddress, toAddress);
            MailMessage message = new MailMessage();

            message.From = new MailAddress(fromAddress, "Enquiry");
            message.To.Add(toAddress);
            var smtp = new System.Net.Mail.SmtpClient();
            {

                smtp.Host = smtpsetting.Network.Host;
                smtp.Port = smtpsetting.Network.Port;
                smtp.EnableSsl = true;
                smtp.Timeout = 40000;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                //smtp.Credentials = new NetworkCredential(smtpsetting.Network.UserName, Crypto.Decrypt(smtpsetting.Network.Password, Crypto.Key));
                smtp.Credentials = new NetworkCredential(smtpsetting.Network.UserName, smtpsetting.Network.Password);
                message.Subject = subject;
                body = body.Replace("&NBSP;", "&nbsp;");
                message.Body = body;

                message.IsBodyHtml = true;
                if (!String.IsNullOrEmpty(cc))
                {
                    string[] arrCC = cc.Split(',');
                    for (int i = 0; i < arrCC.Length; i++)
                    {
                        message.CC.Add(new MailAddress(arrCC[i]));
                    }
                }
                if ((bcc != null) && (bcc != string.Empty))
                {
                    message.Bcc.Add(new MailAddress(bcc));
                }

            }

            string paths = HttpContext.Current.Request.PhysicalApplicationPath;

            paths += "/Uploads/Attachments/Mail/" + smsemailid;
            if (String.IsNullOrEmpty(smsemailid))
            {
                paths = string.Empty;
            }
            /*if (System.IO.File.Exists(paths))
            {
                Attachment itm = new Attachment(paths);
                message.Attachments.Add(itm);
            }*/

            if (paths != string.Empty && System.IO.Directory.Exists(paths))
            {
                string[] strFileName = System.IO.Directory.GetFiles(paths);
                if (strFileName.Length > 0)
                {

                    /*foreach (string str in strFileName)
                    {
                        Attachment  itm = new Attachment(str);
                        message.Attachments.Add(itm);

                    }*/
                    //Attachment  itm = new Attachment(strFileName[0]);
                    Attachment[] arritm = new Attachment[strFileName.Length];
                    for (int i = 0; i < strFileName.Length; i++)
                    {
                        //itm = new Attachment(strFileName[i]);
                        //itm.Name=strFileName[i]);
                        //message.Attachments.Add(itm);
                        //itm.Dispose();

                        arritm[i] = new Attachment(strFileName[i]);
                    }
                    foreach (Attachment Att in arritm)
                    {
                        message.Attachments.Add(Att);
                    }
                    smtp.Send(message);
                    //itm.Dispose();
                    for (int i = 0; i < arritm.Length; i++)
                    {

                        arritm[i].Dispose();
                    }
                }
                else
                    smtp.Send(message);

            }
            else
                smtp.Send(message);




            ///////////////Delete Folder////////////////////
            if (paths != string.Empty && System.IO.Directory.Exists(paths))
            {
                string[] strFileName = System.IO.Directory.GetFiles(paths);
                foreach (string str in strFileName)
                {
                    System.IO.File.Delete(str);
                }
                System.IO.Directory.Delete(paths);
            }
            /////////////////////////////////////////////////

        }
    }

    #endregion
}
    