using HealthCalculator.Web.EntityModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace HealthCalculator.Web
{
    public class CommonMethods
    {

        // Set server
        public const string ServerPath = "https://eatingsmart.in/HealthWeb/";
        public const string ServerPathDocs = "https://eatingsmart.in/";
        private const string V = @"C:\\Harish\\Projects\\email\";
        //Server
        public string growthChart = "https://eatingsmart.in/HealthWeb/Home/{0}";
        //Local
        // public string growthChart = "http://localhost:50026/Home/{0}";

        //To set local
        // public const string ServerPath = "http://localhost:50026/";
        public string SendContact(ContactDetails objContactDetails)
        {
            string returnMessage = string.Empty;
            try
            {
                using (MailMessage mm = new MailMessage("hello@EATINGSMART.IN", "healthrating@eatingsmart.in"))
                // using (MailMessage mm = new MailMessage("emailus @d2digitalservices.com", ToEmail))
                {
                    // mm.CC.Add(new MailAddress("healthrating@eatingsmart.in"));
                    mm.Subject = objContactDetails.Subject;
                    mm.Body = objContactDetails.Message;
                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "mail.EATINGSMART.IN";
                    smtp.EnableSsl = false;
                    NetworkCredential NetworkCred = new NetworkCredential();
                    //NetworkCred.UserName = "alerts@EATINGSMART.IN";
                    //NetworkCred.Password = "d73Clh~9";
                    NetworkCred.UserName = "hello@EATINGSMART.IN";
                    NetworkCred.Password = "_6Jzn6o6";
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 26;
                    // isValid = true;
                    //smtp.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    //smtp.PickupDirectoryLocation = V;
                    smtp.Send(mm);
                    // return isValid;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnMessage;
        }

        public bool SendEmail(string toEmail, string Subject, string content, List<Table> dt, EnquiryModel collection, List<Table1> dtRange)
        {
            string emailbody = string.Empty;
            if (dt[0].AgeGroup == "Adult")
                emailbody = AdultEmailBody(dt, collection);
            else if (dt[0].AgeGroup == "Child")
            {
                Subject = "Eating Smart health status report";
                emailbody = ChildEmailBody(dt, collection, dtRange);
            }

            content = emailbody;

            try
            {

                bool isValid = false;
                using (MailMessage mm = new MailMessage("hello@EATINGSMART.IN", toEmail))
                // using (MailMessage mm = new MailMessage("emailus @d2digitalservices.com", ToEmail))
                {
                    mm.CC.Add(new MailAddress("healthrating@eatingsmart.in"));
                    mm.Subject = Subject;
                    mm.Body = content;
                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "mail.EATINGSMART.IN";
                    smtp.EnableSsl = false;
                    NetworkCredential NetworkCred = new NetworkCredential();
                    //NetworkCred.UserName = "alerts@EATINGSMART.IN";
                    //NetworkCred.Password = "d73Clh~9";
                    NetworkCred.UserName = "hello@EATINGSMART.IN";
                    NetworkCred.Password = "_6Jzn6o6";
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 26;
                    isValid = true;
                    //smtp.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    //smtp.PickupDirectoryLocation = V;
                    smtp.Send(mm);
                    return isValid;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string ChildEmailBody(List<Table> dt, EnquiryModel collection, List<Table1> dtRange)
        {
            string filePath = string.Empty;
            string groathChart = string.Empty;
            string TableHeight = string.Empty;
            string TableWiegth = string.Empty;
            string Age1 = string.Empty;
            string Weight1 = string.Empty;
            string Height1 = string.Empty;

            string Age2 = string.Empty;
            string Weight2 = string.Empty;
            string Height2 = string.Empty;

            string Age3 = string.Empty;
            string Weight3 = string.Empty;
            string Height3 = string.Empty;

            string Age4 = string.Empty;
            string Weight4 = string.Empty;
            string Height4 = string.Empty;

            string Age5 = string.Empty;
            string Weight5 = string.Empty;
            string Height5 = string.Empty;
            string BMIStatus = string.Empty;
            /// return content;

            string retVal = string.Empty;
            string ChildWeight = string.Empty;
            string ChildHeight = string.Empty;
            int childAge = Convert.ToInt32(GetChildSaveValue(collection, "txtChildAge"));
            string gender = GetChildSaveValue(collection, "ddlChildGender");
            string weightSel = GetChildSaveValue(collection, "WeightSelection");
            if (weightSel == "KG")
            {
                TableWiegth = "KG";
                ChildWeight = GetChildSaveValue(collection, "ddlChildKilo") + "." + GetChildSaveValue(collection, "ddlChildGaram") + " kg";
            }
            else
            {
                TableWiegth = "lb";
                ChildWeight = GetChildSaveValue(collection, "ddlChildPounds") + " lb";
            }

            string heighsel = GetChildSaveValue(collection, "HeightSelection");
            if (heighsel == "Feet")
            {
                TableHeight = "Feet & Inch";
                ChildHeight = GetChildSaveValue(collection, "ddlChildFeet") + " feet " + GetChildSaveValue(collection, "ddlChildInches") + " inches.";
            }
            else
            {
                TableHeight = "CM";
                ChildHeight = GetChildSaveValue(collection, "ddlChildCentimeter") + " cm";
            }

            retVal = retVal + GetRangTable(dtRange);
            for (int i = 0; i < dtRange.Count; i++)
            {
                if (i == 0)
                {
                    Age1 = dtRange[i].Age;
                    if (weightSel == "KG")
                        Weight1 = dtRange[i].Weight;
                    else
                        Weight1 = dtRange[i].WeightLB;
                    if (heighsel == "Feet")
                        Height1 = dtRange[i].HeightFI;
                    else
                        Height1 = dtRange[i].height;
                }
                if (i == 1)
                {
                    Age2 = dtRange[i].Age;
                    if (weightSel == "KG")
                        Weight2 = dtRange[i].Weight;
                    else
                        Weight2 = dtRange[i].WeightLB;
                    if (heighsel == "Feet")
                        Height2 = dtRange[i].HeightFI;
                    else
                        Height2 = dtRange[i].height;
                }
                if (i == 2)
                {
                    Age3 = dtRange[i].Age;
                    if (weightSel == "KG")
                        Weight3 = dtRange[i].Weight;
                    else
                        Weight3 = dtRange[i].WeightLB;
                    if (heighsel == "Feet")
                        Height3 = dtRange[i].HeightFI;
                    else
                        Height3 = dtRange[i].height;
                }
                if (i == 3)
                {
                    Age4 = dtRange[i].Age;
                    if (weightSel == "KG")
                        Weight4 = dtRange[i].Weight;
                    else
                        Weight4 = dtRange[i].WeightLB;
                    if (heighsel == "Feet")
                        Height4 = dtRange[i].HeightFI;
                    else
                        Height4 = dtRange[i].height;
                }
                if (i == 4)
                {
                    Age5 = dtRange[i].Age;
                    if (weightSel == "KG")
                        Weight5 = dtRange[i].Weight;
                    else
                        Weight5 = dtRange[i].WeightLB;
                    if (heighsel == "Feet")
                        Height5 = dtRange[i].HeightFI;
                    else
                        Height5 = dtRange[i].height;
                }
            }



            if (childAge <= 2)
            {
                // Server
                // filePath = HttpContext.Current.Server.MapPath("~/HealthWeb/emailer/child02.html");
                filePath = HttpContext.Current.Server.MapPath("~/emailer/child02.html");
                if (gender == "Female")
                    groathChart = string.Format(growthChart, "ShowWFLGirl?Enq=" + dt[0].EnquiryId);
                else if (gender == "Male")
                    groathChart = string.Format(growthChart, "ShowGraph?Enq=" + dt[0].EnquiryId);

            }
            else if (childAge > 2 && childAge <= 5)
            {
                // Server
                // filePath = HttpContext.Current.Server.MapPath("~/HealthWeb/emailer/child.html");
                filePath = HttpContext.Current.Server.MapPath("~/emailer/child.html");

                BMIStatus = GetChildBMIstatus(dt);
                if (gender == "Female")
                    groathChart = string.Format(growthChart, "ShowBMI0205Girl?Enq=" + dt[0].EnquiryId);
                else if (gender == "Male")
                    groathChart = string.Format(growthChart, "ShowBMI0205Boy?Enq=" + dt[0].EnquiryId);

            }
            else if (childAge > 5 && childAge <= 18)
            {
                // Server
                //filePath = HttpContext.Current.Server.MapPath("~/HealthWeb/emailer/child.html");
                filePath = HttpContext.Current.Server.MapPath("~/emailer/child.html");
                BMIStatus = GetChildBMIstatus(dt);
                if (gender == "Female")
                    groathChart = string.Format(growthChart, "ShowBMI0518Girl?Enq=" + +dt[0].EnquiryId);
                else if (gender == "Male")
                    groathChart = string.Format(growthChart, "ShowBMI0518Boy?Enq=" + +dt[0].EnquiryId);


            }

            System.IO.StreamReader objReader;
            objReader = new System.IO.StreamReader(filePath);
            string content = objReader.ReadToEnd();
            objReader.Close();

            content = Regex.Replace(content, "@ClientName", FirstCharToUpper(collection.Instance_enquiry.State, gender));


            content = Regex.Replace(content, "@TableWeight", TableWiegth);

            content = Regex.Replace(content, "@TableHeight", TableHeight);


            content = Regex.Replace(content, "@ClientWieght", ChildWeight);

            content = Regex.Replace(content, "@ClientCM", ChildHeight);

            content = Regex.Replace(content, "@Age1", Age1);
            content = Regex.Replace(content, "@Weight1", Weight1);
            content = Regex.Replace(content, "@Height1", Height1);

            content = Regex.Replace(content, "@Age2", Age2);
            content = Regex.Replace(content, "@Weight2", Weight2);
            content = Regex.Replace(content, "@Height2", Height2);

            content = Regex.Replace(content, "@Age3", Age3);
            content = Regex.Replace(content, "@Weight3", Weight3);
            content = Regex.Replace(content, "@Height3", Height3);

            content = Regex.Replace(content, "@Age4", Age4);
            content = Regex.Replace(content, "@Weight4", Weight4);
            content = Regex.Replace(content, "@Height4", Height4);

            content = Regex.Replace(content, "@Age5", Age5);
            content = Regex.Replace(content, "@Weight5", Weight5);
            content = Regex.Replace(content, "@Height5", Height5);
            content = Regex.Replace(content, "@GraphURL", groathChart);

            if ((childAge > 2 && childAge <= 5) || (childAge > 5 && childAge <= 18))
            {
                content = Regex.Replace(content, "@ClientCategory", BMIStatus);
            }


            return content;

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
            return body + "</table></br>";
        }

        public static string GetChildBMIstatus(List<Table> dt)
        {
            string strBMI = dt[0].ChildCategory;
            return strBMI;
        }


        public static string GetChildSaveValue(EnquiryModel enqModel, string type)
        {
            string retValue = "0";
            foreach (Enquiry_Transaction tx in enqModel.Instance_Enquiry_Transaction)
            {
                if (tx != null)
                {
                    if (tx.ControlName == type)
                    {
                        retValue = Convert.ToString(tx.OptionValue);
                        if (type == "ddlChildGaram")
                        {
                            retValue = retValue.Substring(0, 1);
                        }
                        else
                        {

                            if (retValue == "Birth" || retValue == "3 months" || retValue == "6 months" || retValue == "9 months")
                                retValue = "0";
                            else
                                retValue = retValue.Split(' ')[0];
                        }
                    }
                }
            }

            return retValue;
        }

        public static string GetCollectionValue(EnquiryModel enqModel, string type)
        {
            string retValue = string.Empty;
            foreach (Enquiry_Transaction tx in enqModel.Instance_Enquiry_Transaction)
            {
                if (tx != null)
                {
                    if (tx.ControlName == type)
                    {
                        retValue = Convert.ToString(tx.OptionValue);
                    }
                }
            }

            return retValue;
        }
        public string GetHTMLData()
        {
            // Server
            // string filePath = Server.MapPath("~/HealthWeb/emailer/adult.html");
            //Local
            string filePath = HttpContext.Current.Server.MapPath("~/emailer/adult.html");

            System.IO.StreamReader objReader;
            objReader = new System.IO.StreamReader(filePath);
            string content = objReader.ReadToEnd();
            objReader.Close();
            return content;

        }
        public string GetHTMLChildData()
        {
            // Server

            string filePath = HttpContext.Current.Server.MapPath("~/HealthWeb/emailer/adult.html");
            //Local
            // string filePath = HttpContext.Current.Server.MapPath("~/emailer/child.html");

            System.IO.StreamReader objReader;
            objReader = new System.IO.StreamReader(filePath);
            string content = objReader.ReadToEnd();
            objReader.Close();
            return content;

        }
        public string GetHTMLChild02Data()
        {
            // Server
            // string filePath = Server.MapPath("~/HealthWeb/emailer/adult.html");
            //Local
            string filePath = HttpContext.Current.Server.MapPath("~/emailer/child02.html");

            System.IO.StreamReader objReader;
            objReader = new System.IO.StreamReader(filePath);
            string content = objReader.ReadToEnd();
            objReader.Close();
            return content;

        }

        public string GetWeightType(List<Table> dt, EnquiryModel collection)
        {
            string returnWiegth = string.Empty;
            try
            {
                foreach (Enquiry_Transaction tx in collection.Instance_Enquiry_Transaction)
                {
                    if (tx != null)
                    {
                        if (tx.ControlName == "ddlKilo")
                        {
                            returnWiegth = "KG";
                        }
                       
                        if (tx.ControlName == "ddlpounds")
                        {
                            returnWiegth = "LB";
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                //ex.Message;
            }
            return returnWiegth;
        }
        public string AdultEmailBody(List<Table> dt, EnquiryModel collection)
        {
            string retVal = string.Empty;
            string rating = string.Empty;
            string indicate = string.Empty;
            string Gender = GetCollectionValue(collection, "ddlGender");
            //retVal = retVal + "<p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>Hi "+ FirstCharToUpper(collection.Instance_enquiry.FirstName, Gender);
            //retVal = retVal + ",</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>Greetings from our team Eating Smart.</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>Hope you’re doing good.</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='font-family: Verdana; font-size: 11pt;'>Congratulations on taking your first step towards a smart lifestyle &#128515 </span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='font-family: Verdana; font-size: 11pt;'>We sincerely thank you for providing us this opportunity to assess your health status and assist you to enjoy a smart lifestyle with Eating Smart.</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='font-family: Verdana; font-size: 11pt;'>This health rating is curated by Sidra Patel which will help evaluate where you stand in terms of weight and health.</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='font-family: Verdana; font-size: 11pt;'>We believe in the modern concept 'Every natural produce has it's benefits, we just need to channelize it the smart way'.</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>please find herein......health status report.</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>&nbsp;</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt; text-decoration: underline;'>Weight status</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>";
            //retVal = retVal + "Your current weight is &nbsp;<b>" + GetCurrentWeight(dt, collection) + "</b> kg (" + weightStatus(dt, collection) + ")</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>";
            //retVal = retVal + "Your ideal body weight is &nbsp;<b>(" + (dt[0].IdealBodyWeight-2)+"-"+(dt[0].IdealBodyWeight + 2) + ")</b> kg.</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>&nbsp;</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt; text-decoration: underline;'>";
            //retVal = retVal + "Body mass index  status </span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>BMI is measure of weight relative to height which is an indicator of total body fat and related health risks.</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>";
            ////retVal = retVal + "Your BMI is &nbsp;<b>" + dt[0].BMI + "</b> kg/m</span><span style='color: #525252; font-family: Arial; font-size: 10.5pt;'>²</span><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>,";
            //retVal = retVal + "Your BMI = &nbsp;<b>" + dt[0].BMI + "</b></span><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>,";
            //retVal = retVal + " which indicates that you fall under the <b>(" + BMIStatus(dt) + ")</b></span>";
            ////retVal = retVal + "<span style='color: #222222; font-family: Verdana; font-size: 11pt; font-weight: bold;'>[whichever status according to calculation]</span><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>category.</span></p>";
            //retVal = retVal + "<p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>Overweight and obese individuals are at high risks for Type 2 Diabetes, cardiovascular and other serious diseases.</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>";
            //retVal = retVal + "<img src='https://eatingsmart.in/HealthWeb/img/EmailImages/BodyMassIndexImage.jpg' width='300' height ='200' /></span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt; text-decoration: underline;'>Basal Metabolic Rate [BMR] status</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #555555; background-color: #fbfbfb; font-family: Arial; font-size: 11.5pt;'>BMR, is a measure of the rate at which a person's body 'burns' energy, in the form of calories, when at rest.</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>";
            //retVal = retVal + "Your BMR is &nbsp;<b>" + dt[0].BMR + "</b> kcal/day</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'><br /></span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt; text-decoration: underline;'>Hydration status</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>Hydration is one of the most neglected aspect of a healthy lifestyle.</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>";
            //retVal = retVal + "Keeping in mind your health status you need to consume &nbsp;<b>" + dt[0].WaterIntake + "</b> L/ day.</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>*Exceptional if suffering from renal diseases.</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'><br /></span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>";
            //retVal = retVal + "Your health rating.</span></p> <b>" + healthRating(dt, collection)+ "</b>";
            //retVal = retVal + "<img src='https://eatingsmart.in/HealthWeb/img/EmailImages/RatingImage.jpg' width='200' height ='100' /></br>";
            ////Interpretation
            //retVal = retVal + "<p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>Interpretation:</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>For 1 star: Alarming health deficit</span></p><ul style='margin-top: 0px; margin-bottom: 0px;'><li style='line-height: 1.07917; list-style-type: square; color: #222222; font-family: Wingdings; font-size: 11pt;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>Focusing on your health is very crucial at this stage</span></li><li style='line-height: 1.07917; list-style-type: square; color: #222222; font-family: Wingdings; font-size: 11pt;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>A smart lifestyle coupled with a positive state of mind will help you attain good health</span></li><li style='line-height: 1.07917; list-style-type: square; color: #222222; font-family: Wingdings; font-size: 11pt;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>Taking small steps, will lay the foundation for a happy &amp; healthy life</span></li></ul><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>Believe you can and you’re half way through &#128515 </span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>&nbsp;</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>For 2 stars: At the fringe, high time we improve</span></p><ul style='margin-top: 0px; margin-bottom: 0px;'><li style='line-height: 1.07917; list-style-type: square; color: #222222; font-family: Wingdings; font-size: 11pt;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>Our health status is on the verge of deteriorating, taking proactive steps at this stage will help you get back on track</span></li><li style='line-height: 1.07917; list-style-type: square; color: #222222; font-family: Wingdings; font-size: 11pt;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>This is the right time we strive hard in order to live a healthy and safe life ahead</span></li><li style='line-height: 1.07917; list-style-type: square; color: #222222; font-family: Wingdings; font-size: 11pt;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>A smart lifestyle coupled with a positive state of mind will help you attain good health</span></li></ul><p style='text-align: left; margin: 0pt 0pt 0pt 36pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>&nbsp;</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>For 3 stars: Needs improvement</span></p><ul style='margin-top: 0px; margin-bottom: 0px;'><li style='line-height: 1.07917; list-style-type: square; color: #222222; font-family: Wingdings; font-size: 11pt;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>Focusing on a smart lifestyle coupled with a positive state of mind will help you reduce the risks of many diseases in near future</span></li><li style='line-height: 1.07917; list-style-type: square; color: #222222; font-family: Wingdings; font-size: 11pt;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>Evaluating each aspect of our health and taking proactive steps would be the best way to move forward</span></li></ul><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>&nbsp;</span></p><p style='text-align: left; margin: 0pt 0pt 8pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>For 4 stars</span><span style='color: #222222; font-family: Calibri; font-size: 14pt;'>: </span><span style='font-family: Verdana; font-size: 11pt;'>Little emphasis on eating &amp; lifestyle will get you 5 stars</span></p><ul style='margin-top: 0px; margin-bottom: 0px;'><li style='line-height: 1.07917; list-style-type: square; color: #222222; font-family: Wingdings; font-size: 11pt;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>Good health is a state of physical as well as mental wellbeing, monitoring every aspect can help you stay in good health</span></li><li style='line-height: 1.07917; list-style-type: square; color: #222222; font-family: Wingdings; font-size: 11pt;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>Many a times we tend to neglect the very crucial but least weighed aspects of health like water intake, stress, sleep. It’s all a vicious cycle of health and well being</span></li></ul><p style='text-align: left; margin: 0pt 0pt 0pt 36pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>&nbsp;</span></p><p style='text-align: left; margin: 0pt 0pt 0pt 36pt; line-height: 1.07917;'><span style='color: #222222; font-family: Verdana; font-size: 11pt;'>You are what you eat &#128515</span></p>";
            //rating = healthRating(dt, collection);
             
            // Server
            // string filePath = HttpContext.Current.Server.MapPath("~/HealthWeb/emailer/adult.html");
            //Local
            string filePath = HttpContext.Current.Server.MapPath("~/emailer/adult.html");

            string ratingPath = string.Empty;
            string ratingContent = string.Empty;
            System.IO.StreamReader objReader;
            objReader = new System.IO.StreamReader(filePath);
            string content = objReader.ReadToEnd();
            objReader.Close();
            indicate = weightStatus(dt, collection);
            content = Regex.Replace(content, "@ClientName", FirstCharToUpper(collection.Instance_enquiry.FirstName, Gender));
            string WType = GetWeightType(dt, collection);
            string weightStatusCandidate = BMIStatus(dt);
            if (weightStatusCandidate != "Normal")
            {
                string WInde = " , which indicates that you are " + indicate;
                content = Regex.Replace(content, "@indicates", WInde);
                content = Regex.Replace(content, "@NormalCategory", "");
            }
            else
            {
                content = Regex.Replace(content, "@indicates", "");
                content = Regex.Replace(content, "@NormalCategory", "*Ideal body weight formula takes into consideration the gender");

            }

            if (WType == "KG")
            {
                int FromRange = (int)(dt[0].IdealBodyWeight + 0.5);

                string idealbodyweight = (FromRange - 2).ToString() + "-" + (FromRange + 2).ToString() + " kg";
                content = Regex.Replace(content, "@idealbodyweight", idealbodyweight);

                // content = Regex.Replace(content, "@ToRange ", (dt[0].IdealBodyWeight + 2).ToString() + " KG");
            }
            else
            {
                double pounds = (dt[0].IdealBodyWeight) * 2.20462d;
                //  double value = 1.1;
                int roundedValue = (int)(pounds + 0.5);

                string idealbodyweightLb = (roundedValue - 4).ToString() + "-" + (roundedValue + 4).ToString() + " lb";

                content = Regex.Replace(content, "@idealbodyweight", idealbodyweightLb);
            }
           

            content = Regex.Replace(content, "@CurrentWeigth", GetCurrentWeight(dt, collection));

           
          
            int newBMR = (int)(Convert.ToDecimal(dt[0].BMR)+0.5m);
            content = Regex.Replace(content, "@YourBMR", newBMR.ToString());
            content = Regex.Replace(content, "@consumeL", dt[0].WaterIntake.ToString());

            content = Regex.Replace(content, "@ClientBMI", dt[0].BMI.ToString());
            content = Regex.Replace(content, "@BMICategory", weightStatusCandidate);

            rating = healthRating(dt, collection);

            System.IO.StreamReader objReader1;
          
            if (rating == "1")
            {
                ratingPath = HttpContext.Current.Server.MapPath("~/emailer/Rating1.html");
                objReader1 = new System.IO.StreamReader(ratingPath);
                ratingContent = objReader1.ReadToEnd();
                objReader.Close();


                rating = "https://eatingsmart.in/Healthweb/emailer/images/Rating1.png";

            }
            else if (rating == "2")
            {
                ratingPath = HttpContext.Current.Server.MapPath("~/emailer/Rating2.html");
                objReader1 = new System.IO.StreamReader(ratingPath);
                ratingContent = objReader1.ReadToEnd();
                objReader.Close();

                rating = "https://eatingsmart.in/Healthweb/emailer/images/Rating2.png";
            }
            else if (rating == "3")
            {
                ratingPath = HttpContext.Current.Server.MapPath("~/emailer/Rating3.html");
                objReader1 = new System.IO.StreamReader(ratingPath);
                ratingContent = objReader1.ReadToEnd();
                objReader.Close();
                rating = "https://eatingsmart.in/Healthweb/emailer/images/Rating3.png";
            }
            else if (rating == "4")
            {
                ratingPath = HttpContext.Current.Server.MapPath("~/emailer/Rating4.html");
                objReader1 = new System.IO.StreamReader(ratingPath);
                ratingContent = objReader1.ReadToEnd();
                objReader.Close();
                rating = "https://eatingsmart.in/Healthweb/emailer/images/Rating4.png";
            }
            else
            {
                ratingPath = HttpContext.Current.Server.MapPath("~/emailer/Rating5.html");
                objReader1 = new System.IO.StreamReader(ratingPath);
                ratingContent = objReader1.ReadToEnd();
                objReader.Close();
                rating = "https://eatingsmart.in/Healthweb/emailer/images/Rating5.png";
            }

            if (indicate == "Under weight")
            {
                indicate = "https://eatingsmart.in/Healthweb/emailer/images/giphy.gif";
            }
            else if (indicate == "Overweight")
            {
                indicate = "https://eatingsmart.in/Healthweb/emailer/images/giphy.gif";
            }
            else
            {
                indicate = "https://eatingsmart.in/Healthweb/emailer/images/giphy.gif";
            }


            content = Regex.Replace(content, "@HealthRating", rating);

            content = Regex.Replace(content, "@WeigthScale", indicate);
            content = Regex.Replace(content, "@RatingContent", ratingContent);
            

            return content;

        }

        public static string FirstCharToUpper(string s, string gender)
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
                retVal = "Mr. " + retVal;
            else if (gender == "Female")
                retVal = "Ms. " + retVal;

            return retVal;
        }
      

        public static string BMIStatus(List<Table> dt)
        {
            bool isValid = true;
            string retBMI = string.Empty;
             

            double dBMI = dt[0].BMI;
           
            if (dBMI<18.5)
            {
                retBMI = "Underweight";
                isValid = false;
            }

            if(isValid)
            {
                if (dBMI>=18.5&&dBMI<=24.9)
                {
                    retBMI = "Normal";
                    isValid = false;
                }
            }

            if (isValid)
            {
                if (dBMI>=25 && dBMI<=29.9)
                {
                    retBMI = "Overweight";
                    isValid = false;
                }
            }

          
            if (isValid)
            {
                if (dBMI >=30 && dBMI <=40)
                {
                    retBMI = "Obese types 1 [obese]";
                    isValid = false;
                }

            }
            if (isValid)
            {
                if (dBMI >40 && dBMI <=50)
                {
                    retBMI = "Obese types 2 [morbid obese]";
                    isValid = false;
                }

            }

            if (isValid)
            {
                if (dBMI >50)
                {
                    retBMI = "Obese types 3 [super obese]";
                    isValid = false;
                }

            }
            return retBMI;
        }

        public static string GetCurrentWeight(List<Table> dt, EnquiryModel collection)
        {
            string stWeight = string.Empty;
            double idealbodyWeight = dt[0].IdealBodyWeight;
            try
            {
                foreach (Enquiry_Transaction tx in collection.Instance_Enquiry_Transaction)
                {
                    if (tx != null)
                    {
                        if (tx.ControlName == "ddlKilo")
                        {
                            stWeight = tx.OptionValue;
                        }
                        else if (tx.ControlName == "ddlgrams")
                        {
                            string grm = tx.OptionValue.Substring(0, 1);
                            stWeight = stWeight + "." + grm +" kg";
                        }
                        if (tx.ControlName == "ddlpounds")
                        {
                            stWeight = tx.OptionValue +" lb";
                        }
                       
                    }
                }
            }
            catch (Exception ex)
            {
                //ex.Message;
            }
            return stWeight;
        }

        public static string weightStatus(List<Table> dt, EnquiryModel collection)
        {
            string stWeight = string.Empty;
            double idealbodyWeight = dt[0].IdealBodyWeight;
                       
            foreach (Enquiry_Transaction tx in collection.Instance_Enquiry_Transaction)
            {
                if (tx != null)
                {
                    if (tx.ControlName == "ddlKilo")
                    {
                        if (idealbodyWeight >= Convert.ToSingle(tx.OptionValue))
                        {
                            stWeight = "Under weight";
                        }
                        else if (idealbodyWeight <= Convert.ToSingle(tx.OptionValue))
                        {
                            stWeight = "Overweight";
                        }
                        else if (idealbodyWeight == Convert.ToSingle(tx.OptionValue))
                        {
                            stWeight = "Normal";
                        }
                    }
                    if (tx.ControlName == "ddlpounds")
                    {
                        double calPound = (Convert.ToDouble(tx.OptionValue) * 0.454);
                        if (idealbodyWeight >= Convert.ToSingle(calPound))
                        {
                            stWeight = "Under weight";
                        }
                        else if (idealbodyWeight <= Convert.ToSingle(calPound))
                        {
                            stWeight = "Overweight";
                        }
                        else if (idealbodyWeight == Convert.ToSingle(calPound))
                        {
                            stWeight = "Normal";
                        }
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

        public  string healthRating(List<Table> dt, EnquiryModel collection)
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

            string chkThyroidType = string.Empty;
            string chkDiabetesType = string.Empty;
            string chkAPcardiovascular = string.Empty;
            string chkAPCOS = string.Empty;
            string chkAGastrointestinal = string.Empty;
            foreach (Enquiry_Transaction tx in collection.Instance_Enquiry_Transaction)
            {
                if (tx != null)
                {
                    if (tx.ControlName == "chkAGastrointestinal")
                    {
                        chkAGastrointestinal = tx.OptionValue;
                    }
                    if (tx.ControlName == "chkThyroidType")
                    {
                        chkThyroidType = tx.OptionValue;
                    }
                    if (tx.ControlName == "chkDiabetesType")
                    {
                        chkDiabetesType = tx.OptionValue;
                    }
                    if (tx.ControlName == "chkAPcardiovascular")
                    {
                        chkAPcardiovascular = tx.OptionValue;
                    }
                    if (tx.ControlName == "chkAPCOS")
                    {
                        chkAPCOS = tx.OptionValue;
                    }

                    if (tx.ControlName == "ddlSmoke")
                    {
                        stsmoking = tx.OptionValue;
                    }
                    else if (tx.ControlName == "ddlAlcohol")
                    {
                        stAlcohol = tx.OptionValue;
                    }
                    else if (tx.ControlName == "chkAdbType")
                    {
                        diabiticType1 = tx.OptionValue;
                    }
                    else if (tx.ControlName == "chkAdbType2")
                    {
                        diabiticType2 = tx.OptionValue;
                    }
                    else if (tx.ControlName == "chkAdSedentary")
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
                    else if (tx.ControlName == "waterIntakeGlass")
                    {
                       // Waterintake = Convert.ToSingle(tx.OptionValue);
                        Waterintake = Convert.ToDouble(tx.OptionValue)/250;
                    }
                    else if (tx.ControlName == "chkACardio")
                    {
                        chkACardio = tx.OptionValue;
                    }
                    else if (tx.ControlName == "chkStrenghtTarining")
                    {
                        chkStrenghtTarining = tx.OptionValue;
                    }
                    else if (tx.ControlName == "ddlExerciseDurationHour")
                    {
                        ddlExerciseDurationHour = tx.OptionValue;
                    }
                    else if (tx.ControlName == "ddlExerciseDurationMins")
                    {
                        ddlExerciseDurationMins = tx.OptionValue;
                    }
                    else if (tx.ControlName == "ddlHowOften")
                    {
                        ddlHowOften = tx.OptionValue;
                    }
                    else if (tx.ControlName == "Exercise")
                    {
                        Exercise = tx.OptionValue;
                    }
                }
            }
            bool rateInc = increaseRating(Durationofsleep, Waterintake, dt[0].WaterIntake, Exercise);
            bool rateDec = decreaseRating(stsmoking, stAlcohol);
            string obesetype = BMIStatus(dt);
            bool isValidRating = true;
            if (dt[0].BMI > 30.0) //  heathRating = "1 star";
            {
                if (rateInc)
                    heathRating = "2";
                else
                    heathRating = "1";//decrease rating
                isValidRating = false;
            }
            if (isValidRating)
            {
                if (dt[0].BMI > 25 && dt[0].BMI <= 29.9) //heathRating = "2 star";
                {
                    heathRating = "2";
                    if (rateInc && !rateDec) // if increase and no smoking no alcohol increate
                        heathRating = "3";
                    else if (rateInc && rateDec)
                        heathRating = "1";//decrease rating
                    isValidRating = false;
                }
            }
            if (isValidRating)
            {
                if (dt[0].BMI <= 18.5) //heathRating = "3 star";
                {
                        heathRating = "3";
                        if (rateInc && !rateDec) // if increase and no smoking no alcohol increate
                            heathRating = "4";
                        else if (rateInc && rateDec) // if increase and smoking alcohol decr
                            heathRating = "2";

                        if(chkThyroidType=="True"|| chkDiabetesType == "True"|| chkAPcardiovascular == "True"|| chkAPCOS == "True"|| chkAGastrointestinal == "True")
                        {
                            heathRating = "2";
                        }
                    isValidRating = false;
                }
               
            }
                if (isValidRating)
                {
                    if (dt[0].BMI >= 18.5 && dt[0].BMI <= 24.9) //heathRating = "4 or 5 star";
                    {
                        heathRating = "4";
                        if (rateInc && !rateDec) // if increase and no smoking no alcohol increate
                            heathRating = "5";
                        else if (rateInc&&rateDec) // if increase and smoking alcohol decr
                           heathRating = "3";

                    if (chkThyroidType == "True" || chkDiabetesType == "True" || chkAPcardiovascular == "True" || chkAPCOS == "True" || chkAGastrointestinal == "True")
                        {
                            heathRating = "4";
                        }
                    }
                }
            return heathRating;
        }
        public  bool increaseRating(string SleepDuration, double WaterintakeConsume, double idealWaterInke, string Exercise)
        {
            bool inFlag = false;
            double newWaterConsumeinL = (WaterintakeConsume/250);
            if ((SleepDuration == "7–8 hours" || SleepDuration == "10 > hours") && WaterintakeConsume >= idealWaterInke && Exercise!= "No")
            {
                inFlag = true;
            }
            return inFlag;
        }

        public  bool decreaseRating(string smoking, string alcohol)
        {
            bool deFlag = false;
            if (smoking == "Yes" || alcohol == "Yes")
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