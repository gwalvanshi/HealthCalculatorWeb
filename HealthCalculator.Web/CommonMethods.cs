﻿using HealthCalculator.Web.EntityModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace HealthCalculator.Web
{
    public class CommonMethods
    {

        // Set server
        ///public const string ServerPath = "../HealthWeb";
        
        //To set local
        public const string ServerPath = "";
        public bool SendEmail(string toEmail, string Subject, string content)
        {
            try
            {

                bool isValid = false;
                using (MailMessage mm = new MailMessage("Admin@EATINGSMART.IN", toEmail))
                // using (MailMessage mm = new MailMessage("emailus @d2digitalservices.com", ToEmail))
                {
                    mm.Subject = Subject;                                    
                    mm.Body = content;
                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "Hostmailbox.com";
                    smtp.Port = 25;
                    // smtp.Host = "smtpout.secureserver.net";
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = true;
                    NetworkCredential NetworkCred = new NetworkCredential("Admin@EATINGSMART.IN", "P@ssword@123");
                    // NetworkCredential NetworkCred = new NetworkCredential("emailus@d2digitalservices.com", "Ddig@87!");          
                    smtp.Credentials = NetworkCred;
                    smtp.TargetName = "STARTTLS/Hostmailbox.com";
                    // smtp.Port = 565;
                    smtp.Send(mm);
                     isValid = true;
                    return isValid;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }
        public string GetXMLFromObject(object o)
        {
            StringWriter sw = new StringWriter();
            XmlTextWriter tw = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(o.GetType());
                tw = new XmlTextWriter(sw);
                serializer.Serialize(tw, o);
            }
            catch (Exception ex)
            {
                //Handle Exception Code
            }
            finally
            {
                sw.Close();
                if (tw != null)
                {
                    tw.Close();
                }
            }
            return sw.ToString();
        }


        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        public static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name.ToLower() == column.ColumnName.ToLower())
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }


    }
}