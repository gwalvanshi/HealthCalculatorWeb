using HealthCalculator.Web.EntityModel;
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
       // public const string ServerPath = "../HealthWeb";
        private const string V = @"C:\\Harish\\Projects\\email\";
        private const string growthChart = " <p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><a href='http://localhost:50026/Home/{0}' target='_blank'>Growth chart</a></p>";

        //To set local
      public const string ServerPath = "";
        public bool SendEmail(string toEmail, string Subject, string content, List<Table> dt, EnquiryModel collection, List<Table1> dtRange)
        {
            string emailbody = string.Empty;
            if (dt[0].AgeGroup == "Adult")
                emailbody = CommonMethods.AdultEmailBody(dt, collection);
            else if(dt[0].AgeGroup == "Child")
                emailbody = CommonMethods.ChildEmailBody(dt, collection,dtRange);

            content = emailbody;

            try
            {

                bool isValid = false;
                using (MailMessage mm = new MailMessage("alerts@EATINGSMART.IN", toEmail))
                // using (MailMessage mm = new MailMessage("emailus @d2digitalservices.com", ToEmail))
                {
                    mm.Subject = Subject;
                    mm.Body = content;
                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "mail.EATINGSMART.IN";
                    smtp.EnableSsl = false;
                    NetworkCredential NetworkCred = new NetworkCredential();
                    //NetworkCred.UserName = "alerts@EATINGSMART.IN";
                    //NetworkCred.Password = "d73Clh~9";
                    NetworkCred.UserName = "alerts@EATINGSMART.IN";
                    NetworkCred.Password = "d73Clh~9";
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 26;
                    isValid = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtp.PickupDirectoryLocation = V;
                    smtp.Send(mm);
                    return isValid;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }

        public static string ChildEmailBody(List<Table> dt, EnquiryModel collection, List<Table1> dtRange)
        {
            string retVal = string.Empty;
            string ChildWeight = string.Empty;
            string ChildHeight = string.Empty;
            int childAge = Convert.ToInt32(GetChildSaveValue(collection, "txtChildAge"));
            string gender = GetChildSaveValue(collection, "ddlChildGender");
            string weightSel = GetChildSaveValue(collection, "WeightSelection");
            if (weightSel == "KG")
                ChildWeight = GetChildSaveValue(collection, "ddlChildKilo") + "." + GetChildSaveValue(collection, "ddlChildGaram") + " KG";
            else
                ChildWeight = GetChildSaveValue(collection, "ddlChildPounds") + " Pounds";

            string heighsel = GetChildSaveValue(collection, "HeightSelection");
            if (heighsel == "Feet")
                ChildHeight = GetChildSaveValue(collection, "ddlChildFeet") + " feet " + GetChildSaveValue(collection, "ddlChildInches") + " inches.";
            else
                ChildHeight = GetChildSaveValue(collection, "ddlChildCentimeter") + " Centimeter";

            retVal = retVal + "<p style='font-family: Verdana; font-size: 11pt; text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222;'>Hi Ms/ Mr <b>";
            retVal = retVal + collection.Instance_enquiry.State+ "</b>, </span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>Greetings from our team Eating Smart.</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>Hope you and your little one are doing good.</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='font-family: Verdana; font-size: 11pt;'>Congratulations on taking your first step towards a smart lifestyle &#128515 </span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='font-family: Verdana; font-size: 11pt;'>We sincerely thank you for providing us this opportunity to assess your child’s health status and assist your little one to enjoy a smart lifestyle with Eating Smart.</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='font-family: Verdana; font-size: 11pt;'>We believe in the modern concept 'Every natural produce has it's benefits, we just need to channelize it the smart way”.</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>Please find herein below, your child’s health status report.</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>According to the inputs weight and height for age should be as follows:</span></p>";
            retVal = retVal + GetRangTable(dtRange);
            if (childAge <=2)
            {
                if (gender == "Female")
                    retVal = retVal + string.Format(growthChart, "ShowWFLGirl?Enq=" + dt[0].EnquiryId);
                else if (gender == "Male")
                    retVal = retVal + string.Format(growthChart, "ShowGraph?Enq=" + dt[0].EnquiryId);

                retVal = retVal + "<p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>Your child's current weight is <b>"+ ChildWeight + "</b> and Height is <b>" + ChildHeight + "</b> </span></p>";
                retVal = retVal + "<p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #ff0000; font-family: Verdana; font-size: 11pt;'>Growth chart</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='font-family: Arial; font-size: 10.5pt;'>Growth charts help to assess how your child is growing compared with other kids of the same age and gender.&nbsp;</span></p><p style='text-align: left; margin: 12pt 0pt 18pt; line-height: 16.8pt;'><span style='font-family: Arial; font-size: 10.5pt;'>On the growth charts, the percentiles are shown as lines drawn in curved patterns.&nbsp;</span><span style='font-family: Verdana; font-size: 10.5pt;'>The higher the percentile number, the bigger a child is compared with other kids of the same age and gender, whether it's for height or weight.</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='font-family: Verdana; font-size: 10.5pt;'>The lower the percentile number, the smaller the child is.</span></p>";
            }
            else if(childAge >2 && childAge <=5)
            {
                retVal = retVal + "<p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Arial; font-size: 11pt;'>Body mass&nbsp;index (BMI) is weight relative to height which is a measure used to determine&nbsp;childhood overweight&nbsp;and&nbsp;obesity</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #ff0000; font-family: Verdana; font-size: 11pt;'>";
                //retVal = retVal + "Accordingly mention saying: ";
                retVal = retVal + "Your child falls under the <b>(" + GetChildBMIstatus(dt) + ")</b> category</span></p>";
                //retVal = retVal + "<table border='0' cellspacing='0' cellpadding='0' style='border-collapse: collapse;'><tbody><tr><td valign='top' width='301' style='width: 225.4pt; padding: 0pt 5.4pt; border-width: 1pt; border-style: solid; border-color: #000000;'><p style='text-align: left; margin: 0pt;'><span style='color: #526173; font-family: &quot;Microsoft Sans Serif&quot;; font-size: 11pt;'>Percentile</span></p></td><td valign='top' width='301' style='width: 225.4pt; padding: 0pt 5.4pt; border-width: 1pt; border-style: solid; border-color: #000000;'><p style='text-align: left; margin: 0pt;'><span style='color: #526173; font-family: &quot;Microsoft Sans Serif&quot;; font-size: 11pt;'>BMI for age status</span></p></td></tr><tr><td valign='top' width='301' style='width: 225.4pt; padding: 0pt 5.4pt; border-width: 1pt; border-style: solid; border-color: #000000;'><p style='text-align: left; margin: 0pt;'><span style='color: #526173; font-family: &quot;Microsoft Sans Serif&quot;; font-size: 11pt;'>99</span></p></td><td valign='top' width='301' style='width: 225.4pt; padding: 0pt 5.4pt; border-width: 1pt; border-style: solid; border-color: #000000;'><p style='text-align: left; margin: 0pt;'><span style='color: #526173; font-family: &quot;Microsoft Sans Serif&quot;; font-size: 11pt;'>Obese</span></p></td></tr><tr><td valign='top' width='301' style='width: 225.4pt; padding: 0pt 5.4pt; border-width: 1pt; border-style: solid; border-color: #000000;'><p style='text-align: left; margin: 0pt;'><span style='color: #526173; font-family: &quot;Microsoft Sans Serif&quot;; font-size: 11pt;'>97</span></p></td><td valign='top' width='301' style='width: 225.4pt; padding: 0pt 5.4pt; border-width: 1pt; border-style: solid; border-color: #000000;'><p style='text-align: left; margin: 0pt;'><span style='color: #526173; font-family: &quot;Microsoft Sans Serif&quot;; font-size: 11pt;'>Overweight</span></p></td></tr><tr><td valign='top' width='301' style='width: 225.4pt; padding: 0pt 5.4pt; border-width: 1pt; border-style: solid; border-color: #000000;'><p style='text-align: left; margin: 0pt;'><span style='color: #526173; font-family: &quot;Microsoft Sans Serif&quot;; font-size: 11pt;'>85</span></p></td><td valign='top' width='301' style='width: 225.4pt; padding: 0pt 5.4pt; border-width: 1pt; border-style: solid; border-color: #000000;'><p style='text-align: left; margin: 0pt;'><span style='color: #526173; font-family: &quot;Microsoft Sans Serif&quot;; font-size: 11pt;'>Risk of overweight</span></p></td></tr><tr><td valign='top' width='301' style='width: 225.4pt; padding: 0pt 5.4pt; border-width: 1pt; border-style: solid; border-color: #000000;'><p style='text-align: left; margin: 0pt;'><span style='color: #526173; font-family: &quot;Microsoft Sans Serif&quot;; font-size: 11pt;'>50</span></p></td><td valign='top' width='301' style='width: 225.4pt; padding: 0pt 5.4pt; border-width: 1pt; border-style: solid; border-color: #000000;'><p style='text-align: left; margin: 0pt;'><span style='color: #526173; font-family: &quot;Microsoft Sans Serif&quot;; font-size: 11pt;'>Normal</span></p></td></tr><tr><td valign='top' width='301' style='width: 225.4pt; padding: 0pt 5.4pt; border-width: 1pt; border-style: solid; border-color: #000000;'><p style='text-align: left; margin: 0pt;'><span style='color: #526173; font-family: &quot;Microsoft Sans Serif&quot;; font-size: 11pt;'>15</span></p></td><td valign='top' width='301' style='width: 225.4pt; padding: 0pt 5.4pt; border-width: 1pt; border-style: solid; border-color: #000000;'><p style='text-align: left; margin: 0pt;'><span style='color: #526173; font-family: &quot;Microsoft Sans Serif&quot;; font-size:11pt;'>Normal</span></p></td></tr><tr><td valign='top' width='301' style='width: 225.4pt; padding: 0pt 5.4pt; border-width: 1pt; border-style: solid; border-color: #000000;'><p style='text-align: left; margin: 0pt;'><span style='color: #526173; font-family: &quot;Microsoft Sans Serif&quot;; font-size: 11pt;'>3</span></p></td><td valign='top' width='301' style='width: 225.4pt; padding: 0pt 5.4pt; border-width: 1pt; border-style: solid; border-color: #000000;'><p style='text-align: left; margin: 0pt;'><span style='color: #526173; font-family: &quot;Microsoft Sans Serif&quot;; font-size:11pt;'>Wasted</span></p></td></tr></tbody></table>";
                retVal = retVal + "<p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='font-family: Calibri; font-size: 11pt;'>&nbsp;</span></p>";
              
                if (gender == "Female")
                    retVal = retVal + string.Format(growthChart, "ShowBMI0205Girl?Enq=" + dt[0].EnquiryId);
                else if(gender == "Male")
                    retVal = retVal + string.Format(growthChart, "ShowBMI0205Boy?Enq=" + dt[0].EnquiryId);

                retVal = retVal + "<p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>Your child's current weight is <b>" + ChildWeight + "</b> and Height is <b>" + ChildHeight + "</b> </span></p>";
            }
            else if(childAge >5 && childAge <=18)
            {
                retVal = retVal + "<p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Arial; font-size: 11pt;'>Body mass&nbsp;index (BMI) is weight relative to height which is a measure used to determine&nbsp;childhood overweight&nbsp;and&nbsp;obesity</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #ff0000; font-family: Verdana; font-size: 11pt;'>";
                //retVal = retVal + "Accordingly mention saying:";
                retVal = retVal + "Your child falls under the  <b>(" + GetChildBMIstatus(dt) + ")</b> category</span></p>";
                //retVal = retVal + "<table border='0' cellspacing='0' cellpadding='0' style='border-collapse: collapse;'><tbody><tr><td valign='top' width='301' style='width: 225.4pt; padding: 0pt 5.4pt; border-width: 1pt; border-style: solid; border-color: #000000;'><p style='text-align: left; margin: 0pt;'><span style='color: #526173; font-family: &quot;Microsoft Sans Serif&quot;; font-size: 11pt;'>Percentile</span></p></td><td valign='top' width='301' style='width: 225.4pt; padding: 0pt 5.4pt; border-width: 1pt; border-style: solid; border-color: #000000;'><p style='text-align: left; margin: 0pt;'><span style='color: #526173; font-family: &quot;Microsoft Sans Serif&quot;; font-size: 11pt;'>BMI for age status</span></p></td></tr><tr><td valign='top' width='301' style='width: 225.4pt; padding: 0pt 5.4pt; border-width: 1pt; border-style: solid; border-color: #000000;'><p style='text-align: left; margin: 0pt;'><span style='color: #526173; font-family: &quot;Microsoft Sans Serif&quot;; font-size: 11pt;'>99</span></p></td><td valign='top' width='301' style='width: 225.4pt; padding: 0pt 5.4pt; border-width: 1pt; border-style: solid; border-color: #000000;'><p style='text-align: left; margin: 0pt;'><span style='color: #526173; font-family: &quot;Microsoft Sans Serif&quot;; font-size: 11pt;'>Obese</span></p></td></tr><tr><td valign='top' width='301' style='width: 225.4pt; padding: 0pt 5.4pt; border-width: 1pt; border-style: solid; border-color: #000000;'><p style='text-align: left; margin: 0pt;'><span style='color: #526173; font-family: &quot;Microsoft Sans Serif&quot;; font-size: 11pt;'>97</span></p></td><td valign='top' width='301' style='width: 225.4pt; padding: 0pt 5.4pt; border-width: 1pt; border-style: solid; border-color: #000000;'><p style='text-align: left; margin: 0pt;'><span style='color: #526173; font-family: &quot;Microsoft Sans Serif&quot;; font-size: 11pt;'>Overweight</span></p></td></tr><tr><td valign='top' width='301' style='width: 225.4pt; padding: 0pt 5.4pt; border-width: 1pt; border-style: solid; border-color: #000000;'><p style='text-align: left; margin: 0pt;'><span style='color: #526173; font-family: &quot;Microsoft Sans Serif&quot;; font-size: 11pt;'>85</span></p></td><td valign='top' width='301' style='width: 225.4pt; padding: 0pt 5.4pt; border-width: 1pt; border-style: solid; border-color: #000000;'><p style='text-align: left; margin: 0pt;'><span style='color: #526173; font-family: &quot;Microsoft Sans Serif&quot;; font-size: 11pt;'>Risk of overweight</span></p></td></tr><tr><td valign='top' width='301' style='width: 225.4pt; padding: 0pt 5.4pt; border-width: 1pt; border-style: solid; border-color: #000000;'><p style='text-align: left; margin: 0pt;'><span style='color: #526173; font-family: &quot;Microsoft Sans Serif&quot;; font-size: 11pt;'>50</span></p></td><td valign='top' width='301' style='width: 225.4pt; padding: 0pt 5.4pt; border-width: 1pt; border-style: solid; border-color: #000000;'><p style='text-align: left; margin: 0pt;'><span style='color: #526173; font-family: &quot;Microsoft Sans Serif&quot;; font-size: 11pt;'>Normal</span></p></td></tr><tr><td valign='top' width='301' style='width: 225.4pt; padding: 0pt 5.4pt; border-width: 1pt; border-style: solid; border-color: #000000;'><p style='text-align: left; margin: 0pt;'><span style='color: #526173; font-family: &quot;Microsoft Sans Serif&quot;; font-size: 11pt;'>15</span></p></td><td valign='top' width='301' style='width: 225.4pt; padding: 0pt 5.4pt; border-width: 1pt; border-style: solid; border-color: #000000;'><p style='text-align: left; margin: 0pt;'><span style='color: #526173; font-family: &quot;Microsoft Sans Serif&quot;; font-size:11pt;'>Normal</span></p></td></tr><tr><td valign='top' width='301' style='width: 225.4pt; padding: 0pt 5.4pt; border-width: 1pt; border-style: solid; border-color: #000000;'><p style='text-align: left; margin: 0pt;'><span style='color: #526173; font-family: &quot;Microsoft Sans Serif&quot;; font-size: 11pt;'>3</span></p></td><td valign='top' width='301' style='width: 225.4pt; padding: 0pt 5.4pt; border-width: 1pt; border-style: solid; border-color: #000000;'><p style='text-align: left; margin: 0pt;'><span style='color: #526173; font-family: &quot;Microsoft Sans Serif&quot;; font-size:11pt;'>Wasted</span></p></td></tr></tbody></table>";
                retVal = retVal + "<p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='font-family: Calibri; font-size: 11pt;'>&nbsp;</span></p>";
             
                if (gender == "Female")
                    retVal = retVal + string.Format(growthChart, "ShowBMI0518Girl?Enq=" + +dt[0].EnquiryId);
                else if (gender == "Male")
                    retVal = retVal + string.Format(growthChart, "ShowBMI0518Boy?Enq=" + +dt[0].EnquiryId);

                retVal = retVal + "<p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>Your Child's current weight is <b>" + ChildWeight + "</b> and Height is <b>" + ChildHeight + "</b> </span></p>";

            }
            //Last intendation
            retVal = retVal + "<p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>If you wish to improve your child’s health status &amp; opt for a smart lifestyle please feel free to get in touch with team Eating Smart. Our nutrition experts shall be happy to assist your little one.</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>Smart lifestyle is just a call away: +91 8452 811506</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>&nbsp;</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>Do you want your child to start Eating Smart?</span></p>";
            return retVal;

        }

        public static string GetRangTable(List<Table1> dtRange)
        {
            string body = string.Empty;
            body += "<table border='1'><tr><th>Age</th><th>Weight</th><th>Height</th></tr>";
            foreach (var item in dtRange)
            {
                body += "<tr><td>" + item.Age + "</td>";
                body += "<td>" + item.Weight + "</td>";
                body += "<td>" + item.height + "</td></tr>";
            }
            return body +"</table></br>";
        }
       
        public static string GetChildBMIstatus( List<Table> dt)
        {
            string strBMI = string.Empty;
            double ChBMI = dt[0].BMI;
            if (ChBMI <= 3)
            {
                strBMI = "Wasted";
            }
            else if (ChBMI > 3 && ChBMI <= 50)
            {
                strBMI = "Normal";
            }
            else if (ChBMI > 50 && ChBMI <= 85)
            {
                strBMI = "Risk of overweight";
            }
            else if (ChBMI > 85 && ChBMI <= 97)
            {
                strBMI = "Overweight";
            }
            else if (ChBMI > 97)
            {
                strBMI = "Obese";
            }
            return strBMI;
        }
      
        public static string GetChildSaveValue(EnquiryModel enqModel,string type)
        {
            string retValue = "0";
            foreach (Enquiry_Transaction tx in enqModel.Instance_Enquiry_Transaction)
            {
                if (tx.ControlName == type)
                {
                    retValue = Convert.ToString(tx.OptionValue);
                    if (retValue == "Birth" || retValue == "3 months" || retValue == "6 months" || retValue == "9 months")
                        retValue = "0";
                    else
                        retValue = retValue.Split(' ')[0];
                }
            }

            return retValue;
        }

        public static string GetCollectionValue(EnquiryModel enqModel, string type)
        {
            string retValue = string.Empty;
            foreach (Enquiry_Transaction tx in enqModel.Instance_Enquiry_Transaction)
            {
                if (tx.ControlName == type)
                {
                    retValue = Convert.ToString(tx.OptionValue);
                }
            }

            return retValue;
        }

        public static string AdultEmailBody(List<Table> dt, EnquiryModel collection)
        {
            string retVal = string.Empty;
            string Gender = GetCollectionValue(collection, "ddlGender");
            retVal = retVal + "<p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>Hi "+ FirstCharToUpper(collection.Instance_enquiry.FirstName, Gender);
            retVal = retVal + ",</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>Greetings from our team Eating Smart.</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>Hope you’re doing good.</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='font-family: Verdana; font-size: 11pt;'>Congratulations on taking your first step towards a smart lifestyle &#128515 </span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='font-family: Verdana; font-size: 11pt;'>We sincerely thank you for providing us this opportunity to assess your health status and assist you to enjoy a smart lifestyle with Eating Smart.</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='font-family: Verdana; font-size: 11pt;'>This health rating is curated by Sidra Patel which will help evaluate where you stand in terms of weight and health.</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='font-family: Verdana; font-size: 11pt;'>We believe in the modern concept 'Every natural produce has it's benefits, we just need to channelize it the smart way'.</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>please find herein......health status report.</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>&nbsp;</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt; text-decoration: underline;'>Weight status</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>";
            retVal = retVal + "Your current weight is &nbsp;<b>" + GetCurrentWeight(dt, collection) + "</b> kg (" + weightStatus(dt, collection) + ")</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>";
            retVal = retVal + "Your ideal body weight is &nbsp;<b>(" + (dt[0].IdealBodyWeight-2)+"-"+(dt[0].IdealBodyWeight + 2) + ")</b> kg.</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>&nbsp;</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt; text-decoration: underline;'>";
            retVal = retVal + "Body mass index  status </span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>BMI is measure of weight relative to height which is an indicator of total body fat and related health risks.</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>";
            //retVal = retVal + "Your BMI is &nbsp;<b>" + dt[0].BMI + "</b> kg/m</span><span style='color: #525252; font-family: Arial; font-size: 10.5pt;'>²</span><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>,";
            retVal = retVal + "Your BMI = &nbsp;<b>" + dt[0].BMI + "</b></span><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>,";
            retVal = retVal + " which indicates that you fall under the <b>(" + BMIStatus(dt) + ")</b></span>";
            //retVal = retVal + "<span style='color: #222222; font-family: Verdana; font-size: 11pt; font-weight: bold;'>[whichever status according to calculation]</span><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>category.</span></p>";
            retVal = retVal + "<p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>Overweight and obese individuals are at high risks for Type 2 Diabetes, cardiovascular and other serious diseases.</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>";
            retVal = retVal + "<img src='https://eatingsmart.in/HealthWeb/img/EmailImages/BodyMassIndexImage.jpg' width='300' height ='200' /></span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt; text-decoration: underline;'>Basal Metabolic Rate [BMR] status</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #555555; background-color: #fbfbfb; font-family: Arial; font-size: 11.5pt;'>BMR, is a measure of the rate at which a person's body 'burns' energy, in the form of calories, when at rest.</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>";
            retVal = retVal + "Your BMR is &nbsp;<b>" + dt[0].BMR + "</b> kcal/day</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'><br /></span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt; text-decoration: underline;'>Hydration status</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>Hydration is one of the most neglected aspect of a healthy lifestyle.</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>";
            retVal = retVal + "Keeping in mind your health status you need to consume &nbsp;<b>" + dt[0].WaterIntake + "</b> L/ day.</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>*Exceptional if suffering from renal diseases.</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'><br /></span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>";
            retVal = retVal + "Your health rating.</span></p> <b>" + healthRating(dt, collection)+ "</b>";
            retVal = retVal + "<img src='https://eatingsmart.in/HealthWeb/img/EmailImages/RatingImage.jpg' width='200' height ='100' /></br>";
            //Interpretation
            retVal = retVal + "<p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>Interpretation:</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>For 1 star: Alarming health deficit</span></p><ul style='margin-top: 0px; margin-bottom: 0px;'><li style='line-height: 1.07917; list-style-type: square; color: #222222; font-family: Wingdings; font-size: 11pt;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>Focusing on your health is very crucial at this stage</span></li><li style='line-height: 1.07917; list-style-type: square; color: #222222; font-family: Wingdings; font-size: 11pt;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>A smart lifestyle coupled with a positive state of mind will help you attain good health</span></li><li style='line-height: 1.07917; list-style-type: square; color: #222222; font-family: Wingdings; font-size: 11pt;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>Taking small steps, will lay the foundation for a happy &amp; healthy life</span></li></ul><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>Believe you can and you’re half way through &#128515 </span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>&nbsp;</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>For 2 stars: At the fringe, high time we improve</span></p><ul style='margin-top: 0px; margin-bottom: 0px;'><li style='line-height: 1.07917; list-style-type: square; color: #222222; font-family: Wingdings; font-size: 11pt;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>Our health status is on the verge of deteriorating, taking proactive steps at this stage will help you get back on track</span></li><li style='line-height: 1.07917; list-style-type: square; color: #222222; font-family: Wingdings; font-size: 11pt;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>This is the right time we strive hard in order to live a healthy and safe life ahead</span></li><li style='line-height: 1.07917; list-style-type: square; color: #222222; font-family: Wingdings; font-size: 11pt;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>A smart lifestyle coupled with a positive state of mind will help you attain good health</span></li></ul><p style='text-align: left; margin: 0pt 0pt 0pt 36pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>&nbsp;</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>For 3 stars: Needs improvement</span></p><ul style='margin-top: 0px; margin-bottom: 0px;'><li style='line-height: 1.07917; list-style-type: square; color: #222222; font-family: Wingdings; font-size: 11pt;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>Focusing on a smart lifestyle coupled with a positive state of mind will help you reduce the risks of many diseases in near future</span></li><li style='line-height: 1.07917; list-style-type: square; color: #222222; font-family: Wingdings; font-size: 11pt;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>Evaluating each aspect of our health and taking proactive steps would be the best way to move forward</span></li></ul><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>&nbsp;</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>For 4 stars</span><span style='color: #222222; font-family: Calibri; font-size: 14pt;'>: </span><span style='font-family: Verdana; font-size: 11pt;'>Little emphasis on eating &amp; lifestyle will get you 5 stars</span></p><ul style='margin-top: 0px; margin-bottom: 0px;'><li style='line-height: 1.07917; list-style-type: square; color: #222222; font-family: Wingdings; font-size: 11pt;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>Good health is a state of physical as well as mental wellbeing, monitoring every aspect can help you stay in good health</span></li><li style='line-height: 1.07917; list-style-type: square; color: #222222; font-family: Wingdings; font-size: 11pt;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>Many a times we tend to neglect the very crucial but least weighed aspects of health like water intake, stress, sleep. It’s all a vicious cycle of health and well being</span></li></ul><p style='text-align: left; margin: 0pt 0pt 0pt 36pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>&nbsp;</span></p><p style='text-align: left; margin: 0pt 0pt 0pt 36pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>You are what you eat &#128515</span></p>";

            //retVal = retVal + "Hi Ms/ Mr "+ collection.Instance_enquiry.FirstName +",</br>";
            //retVal = retVal + "Greetings from our team Eating Smart.</br>";
            //retVal = retVal + "Hope you’re doing good.</br>";
            //retVal = retVal + "Congratulations on taking your first step towards a smart lifestyle 😊 </br>";
            //retVal = retVal + "We sincerely thank you for providing us this opportunity to assess your health </br>";
            //retVal = retVal + "status and assist you to enjoy a smart lifestyle with Eating Smart.</br>";

            //retVal = retVal + "This health rating is curated by Sidra Patel which will help evaluate where you </br>";
            //retVal = retVal + "stand in terms of weight and health.</br>";
            //retVal = retVal + "We believe in the modern concept 'Every natural produce has it’s benefits, we </br>";
            //retVal = retVal + "just need to channelize it the smart way'.";
            //retVal = retVal + "Please find herein below, your health status report:</br></br>";

            //retVal = retVal + "<u>Weight status</u></br>";
            //retVal = retVal + "Your current weight is "+ GetCurrentWeight(dt,collection )+" kg</br>";
            //retVal = retVal + "which indicates that you are "+weightStatus(dt,collection) +" kg </br>";
            //retVal = retVal + "Your ideal body weight is "+dt[0].IdealBodyWeight +" kg</br>";

            //retVal = retVal + "Body mass index [BMI] status</br>";
            //retVal = retVal + "BMI is measure of weight relative to height which is an indicator of total body fat </br>";
            //retVal = retVal + "and related health risks.</br>";
            //retVal = retVal + "Your BMI is "+dt[0].BMI +"  kg/m², which indicates that you fall under the"+ BMIStatus(dt) +" category.</br>";

            //retVal = retVal + "Basal Metabolic Rate [BMR] status</br>";
            //retVal = retVal + "BMR, is a measure of the rate at which a person's body 'burns' energy, in the form of calories, when at rest</br>";
            //retVal = retVal + "Your BMR is"+ dt[0].BMR +" kcal/day</br>";
            //retVal = retVal + "Basal Metabolic Rate [BMR] status</br>";


            //retVal = retVal + "Hydration status";
            //retVal = retVal + "Hydration is one of the most neglected aspect of a healthy lifestyle.";
            //retVal = retVal + "Keeping in mind your health status you need to consume "+dt[0].WaterIntake +" L/ day.";
            //retVal = retVal + "*Exceptional if suffering from renal diseases.";

            //retVal = retVal + "Your health rating :"+ healthRating(dt,collection);


            return retVal;
        }

        public static string FirstCharToUpper(string s,string gender)
        {
            string retVal = string.Empty;
            // Check for empty string.  
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.  
            retVal = char.ToUpper(s[0]) + s.Substring(1);
            if (gender == "Male")
                retVal = "Mr. "+ retVal;
            else if(gender == "Female")
                retVal = "Ms. " + retVal;

            return retVal;
        }

        public static string BMIStatus( List<Table> dt)
        {
            string retBMI = string.Empty;

            double dBMI = dt[0].BMI;

            if(dBMI <= 18.5)
            {
                retBMI = "Underweight";
            }
            else if(dBMI > 18.5 && dBMI <= 22.9)
            {
                retBMI = "Normal";
            }
            else if(dBMI >23 && dBMI <= 24.9 )
            {
                retBMI = "Overweight";
            }
            else if(dBMI > 25 && dBMI <= 29.9)
            {
                retBMI = "Pre – Obese";
            }
            else if(dBMI > 30 && dBMI <= 40)
            {
                retBMI = "Obese types 1 [obese]";
            }
            else if (dBMI > 40 && dBMI <= 50)
            {
                retBMI = "Obese types 2 [morbid obese]";
            }
            else if (dBMI > 50)
            {
                retBMI = "Obese types 3 [super obese]";
            }
            return retBMI;
        }

        public static string GetCurrentWeight( List<Table> dt, EnquiryModel collection)
        {
            string stWeight = string.Empty;
            double idealbodyWeight = dt[0].IdealBodyWeight;
            try
            {
                foreach (Enquiry_Transaction tx in collection.Instance_Enquiry_Transaction)
                {
                    if (tx.ControlName == "ddlKilo")
                    {
                        stWeight = tx.OptionValue;
                    }
                    else if (tx.ControlName == "ddlgrams")
                    {
                        stWeight = stWeight + "." + tx.OptionValue;
                    }
                }
            }
            catch (Exception ex)
            {
                //ex.Message;
            }
            return stWeight;
        }

        public static string weightStatus( List<Table> dt, EnquiryModel collection)
        {
            string stWeight = string.Empty;
            double idealbodyWeight = dt[0].IdealBodyWeight;
            
            foreach( Enquiry_Transaction tx in collection.Instance_Enquiry_Transaction)
            {
                if(tx.ControlName == "ddlKilo")
                {
                    if(idealbodyWeight >= Convert.ToSingle(tx.OptionValue))
                    {
                        stWeight = "Under weight";
                    }
                    else if(idealbodyWeight <= Convert.ToSingle(tx.OptionValue))
                    {
                        stWeight = "Overweight";
                    }
                    else if(idealbodyWeight == Convert.ToSingle(tx.OptionValue))
                    {
                        stWeight = "Normal";
                    }
        }
            }

            return stWeight;
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

        public static string healthRating( List<Table> dt, EnquiryModel collection)
        {
            string heathRating = string.Empty;
            string stAlcohol = string.Empty;
            string stsmoking = string.Empty;
            string diabiticType1 = string.Empty;
            string diabiticType2 = string.Empty;
            string stActivitylevelchkAdSedentary = string.Empty;
            string stActivitylevelchkAdLight = string.Empty;
            string stActivitylevelchkAdModerate = string.Empty;
            string stActivitylevelchkAdHeavy = string.Empty;
            string Durationofsleep = string.Empty;
            double Waterintake = 0.0;
            string Exercise = string.Empty;
            string chkACardio = string.Empty;
            string chkStrenghtTarining = string.Empty;
            string ddlExerciseDurationHour = string.Empty;
            string ddlExerciseDurationMins = string.Empty;
            string ddlHowOften = string.Empty;
            
            foreach (Enquiry_Transaction tx in collection.Instance_Enquiry_Transaction)
            {
                if (tx.ControlName == "ddlSmoke")
                {
                    stsmoking = tx.OptionValue; 
                }
                else if(tx.ControlName == "ddlAlcohol")
                {
                    stAlcohol = tx.OptionValue;
                }
                else if(tx.ControlName== "chkAdbType")
                {
                    diabiticType1 = tx.OptionValue;
                }
                else if (tx.ControlName == "chkAdbType2")
                {
                    diabiticType2 = tx.OptionValue;
                }
                else if(tx.ControlName == "chkAdSedentary")
                {
                    stActivitylevelchkAdSedentary = tx.OptionValue;
                }
                else if (tx.ControlName == "chkAdLight")
                {
                    stActivitylevelchkAdLight = tx.OptionValue;
                }
                else if (tx.ControlName == "chkAdModerate")
                {
                    stActivitylevelchkAdModerate = tx.OptionValue;
                }
                else if (tx.ControlName == "chkAdHeavy")
                {
                    stActivitylevelchkAdHeavy = tx.OptionValue;
                }
                else if (tx.ControlName == "ddlSleepDuration")
                {
                    Durationofsleep = tx.OptionValue;
                }
                else if (tx.ControlName == "waterIntakeLitter")
                {
                    Waterintake = Convert.ToSingle(tx.OptionValue);
                }
                else if (tx.ControlName == "chkACardio")
                {
                    chkACardio = tx.OptionValue;
                }
                else if (tx.ControlName == "chkStrenghtTarining")
                {
                    chkStrenghtTarining = tx.OptionValue;
                }
                else if(tx.ControlName== "ddlExerciseDurationHour")
                {
                    ddlExerciseDurationHour = tx.OptionValue;
                }
                else if(tx.ControlName== "ddlExerciseDurationMins")
                {
                    ddlExerciseDurationMins = tx.OptionValue;
                }
                else if(tx.ControlName == "ddlHowOften")
                {
                    ddlHowOften = tx.OptionValue;
                }
                else if(tx.ControlName== "Exercise")
                {
                    Exercise = tx.OptionValue;
                }
            }
            bool rateInc = increaseRating(Durationofsleep, Waterintake, dt[0].WaterIntake, Exercise);
            bool rateDec = decreaseRating(stsmoking, stAlcohol);
            string obesetype = BMIStatus(dt);
            if (dt[0].BMI > 30.0) //  heathRating = "1 star";
            {
                if (rateInc)
                    heathRating = "2 star";
                else
                    heathRating = "1 star";//decrease rating
            }
            if (dt[0].BMI > 23 && dt[0].BMI <= 30.9) //heathRating = "2 star";
            {
              
                if (rateInc && rateDec) //if both are true than no change
                    heathRating = "2 star";
                else if(rateInc && !rateDec) // if increase and no smoking no alcohol increate 
                    heathRating = "3 star";
                else
                    heathRating = "1 star";//decrease rating
            }
            if (dt[0].BMI <= 18.5) //heathRating = "3 star";
            {
                if (rateInc && rateDec) //if both are true than no change
                    heathRating = "3 star";
                else if (rateInc && !rateDec) // if increase and no smoking no alcohol increate 
                    heathRating = "4 star";
                else
                    heathRating = "2 star";//decrease rating
            }
            if (dt[0].BMI > 18.5 && dt[0].BMI <= 22.9) //heathRating = "4 or 5 star";
            {
                if (rateInc && rateDec) //if both are true than no change
                    heathRating = "4 star";
                else if (rateInc && !rateDec) // if increase and no smoking no alcohol increate 
                    heathRating = "5 star";
                else
                    heathRating = "3 star"; //decrease rating
            }
                return heathRating;
        }
        public static bool increaseRating(string SleepDuration,double WaterintakeConsume,double idealWaterInke,string Exercise)
        {
            bool inFlag = false;
            if((SleepDuration == "7–8 hours" || SleepDuration== "10 > hours") && WaterintakeConsume >= idealWaterInke && Exercise =="Yes")
            {
                inFlag = true;
            }
            return inFlag;
        }

        public static bool decreaseRating(string smoking,string alcohol)
        {
            bool deFlag = false;
            if(smoking =="Yes" || alcohol =="Yes")
            {
                deFlag = true;
            }
            return deFlag;
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