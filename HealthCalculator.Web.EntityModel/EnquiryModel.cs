using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCalculator.Web.EntityModel
{
   public class EnquiryModel
    {
       
        public Enquiry Instance_enquiry { get; set; }
        public List<Enquiry_Transaction> Instance_Enquiry_Transaction { get; set; }

     /*   public string Age { get; set; }
        public string Gender { get; set; }
        public string Weight { get; set; }
        public string HealthIssue { get; set; }
        public string HealthIssueOther { get; set; }
        public string ActivityLevel { get; set; }
        public string SleepDuration { get; set; }
        public string WaterIntake { get; set; }
        public string Exercise { get; set; }
        public string ExerciseDuration { get; set; }
        public string ExerciseOften { get; set; }
        public string Smoking { get; set; }

        public string Alcohol { get; set; }
        public string Emailid { get; set; } */

    }
    public class Enquiry
    {
        public int Enquiry_Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Contactnumber { get; set; }
        public string Email_ID { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Pin_Code { get; set; }
        public string Country { get; set; }
        public string AgeGroup { get; set; }
        public string UserId { get; set; }

    }
    public class Enquiry_Transaction
    {
        public int  Trans_Id { get; set; }
        public int  Enquiry_ID { get; set; }
        public int MasterTable_ID { get; set; }
        public string OptionValue { get; set; }
        public int MasterTableTypeId { get; set; }
    }
 
}

