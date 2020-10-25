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
        ///public const string ServerPath = "../HealthWeb";
        
        //To set local
        public const string ServerPath = "";
        public bool SendEmail(string toEmail, string Subject, string content, List<Datum> dt, EnquiryModel collection)
        {

            string emailbody = CommonMethods.emailBody(dt,collection);

            try
            {

                bool isValid = false;
                using (MailMessage mm = new MailMessage("Admin@EATINGSMART.IN", toEmail))
                //using (MailMessage mm = new MailMessage("harish.lavangade@gmail.com", toEmail))
                // using (MailMessage mm = new MailMessage("emailus @d2digitalservices.com", ToEmail))
                {
                    mm.Subject = Subject;                                    
                    mm.Body = content;
                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    //smtp.Host = "Hostmailbox.com";
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    // smtp.Host = "smtpout.secureserver.net";
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = true;
                    NetworkCredential NetworkCred = new NetworkCredential("Admin@EATINGSMART.IN", "P@ssword@123");
                    //NetworkCredential NetworkCred = new NetworkCredential("harish.lavangade@gmail.com", "XXXXX*");
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

        public static string emailBody(List<Datum> dt, EnquiryModel collection)
        {
            string retVal = string.Empty;
            retVal = retVal + "Hi Ms/ Mr "+ collection.Instance_enquiry.FirstName +",</br>";
            retVal = retVal + "Greetings from our team Eating Smart.</br>";
            retVal = retVal + "Hope you’re doing good.</br>";
            retVal = retVal + "Congratulations on taking your first step towards a smart lifestyle 😊 </br>";
            retVal = retVal + "We sincerely thank you for providing us this opportunity to assess your health </br>";
            retVal = retVal + "status and assist you to enjoy a smart lifestyle with Eating Smart.</br>";

            retVal = retVal + "This health rating is curated by Sidra Patel which will help evaluate where you </br>";
            retVal = retVal + "stand in terms of weight and health.</br>";
            retVal = retVal + "We believe in the modern concept 'Every natural produce has it’s benefits, we </br>";
            retVal = retVal + "just need to channelize it the smart way'.";
            retVal = retVal + "Please find herein below, your health status report:</br></br>";

            retVal = retVal + "<u>Weight status</u></br>";
            retVal = retVal + "Your current weight is "+ GetCurrentWeight(dt,collection )+" kg</br>";
            retVal = retVal + "which indicates that you are "+weightStatus(dt,collection) +" kg </br>";
            retVal = retVal + "Your ideal body weight is "+dt[0].IdealBodyWeight +" kg</br>";

            retVal = retVal + "Body mass index [BMI] status</br>";
            retVal = retVal + "BMI is measure of weight relative to height which is an indicator of total body fat </br>";
            retVal = retVal + "and related health risks.</br>";
            retVal = retVal + "Your BMI is "+dt[0].BMI +"  kg/m², which indicates that you fall under the"+ BMIStatus(dt) +" category.</br>";

            retVal = retVal + "Basal Metabolic Rate [BMR] status</br>";
            retVal = retVal + "BMR, is a measure of the rate at which a person's body 'burns' energy, in the form of calories, when at rest</br>";
            retVal = retVal + "Your BMR is"+ dt[0].BMR +" kcal/day</br>";
            retVal = retVal + "Basal Metabolic Rate [BMR] status</br>";
          

            retVal = retVal + "Hydration status";
            retVal = retVal + "Hydration is one of the most neglected aspect of a healthy lifestyle.";
            retVal = retVal + "Keeping in mind your health status you need to consume "+dt[0].WaterIntake +" L/ day.";
            retVal = retVal + "*Exceptional if suffering from renal diseases.";

            retVal = retVal + "Your health rating :"+ healthRating(dt,collection);


            return retVal;
        }

        public static string BMIStatus(List<Datum> dt)
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
            //switch (dt[0].BMI)
            //{
            //    case double n when (n >= 18.5):
            //        retBMI = "Underweight";
            //        break;

            //    case double n when (n < 18.5 && n >= 22.9):
            //        retBMI = "Normal";
            //        break;
            //    case double n when (n < 23 && n >= 24.9):
            //        retBMI = "Overweight";
            //        break;
            //    case double n when (n < 25 && n >= 29.9):
            //        retBMI = "Pre – Obese";
            //        break;
            //    case double n when (n < 30 && n >= 40):
            //        retBMI = "Obese types 1 [obese]";
            //        break;
            //    case double n when (n < 40.1 && n >= 50):
            //        retBMI = "Obese types 2 [morbid obese]";
            //        break;
            //    case double n when (n > 50):
            //        retBMI = "Obese types 3 [super obese]";
            //        break;
            //}
            return retBMI;
        }

        public static string GetCurrentWeight(List<Datum> dt, EnquiryModel collection)
        {
            string stWeight = string.Empty;
            double idealbodyWeight = dt[0].IdealBodyWeight;

            foreach (Enquiry_Transaction tx in collection.Instance_Enquiry_Transaction)
            {
                if (tx.ControlName == "ddlKilo")
                {
                    stWeight = tx.OptionValue;
                }
            }

            return stWeight;
        }

        public static string weightStatus(List<Datum> dt, EnquiryModel collection)
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

        public static string healthRating(List<Datum> dt, EnquiryModel collection)
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
            }
            string obesetype = BMIStatus(dt);
            if (dt[0].BMI > 30.0)
            {
                heathRating = "1 star";
            }
            if (dt[0].BMI > 23 && dt[0].BMI <= 30.9)
            {
                if (stsmoking == "No" || stAlcohol == "No")
                    heathRating = "2 star";
                else
                    heathRating = "1 star";
            }
            if (dt[0].BMI <= 18.5)
            {
                if(stsmoking == "No" || stAlcohol == "No")
                    heathRating = "3 star";
                else
                    heathRating = "2 star";
            }
            if (dt[0].BMI > 18.5 && dt[0].BMI <= 22.9)
            {
                if ((stsmoking == "No" && stAlcohol == "No") && Exercise == "Yes" && Waterintake >= dt[0].WaterIntake && (Durationofsleep == "7–8 hours" || Durationofsleep == "10> hours"))
                {
                    heathRating = "5 star";
                }
                else
                {
                    heathRating = "4 star";
                }
            }
                return heathRating;
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