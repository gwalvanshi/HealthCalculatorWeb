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
    public class GraphType
    {
        public string EnquiryId { get; set; }
        public string Type { get; set; }
    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Table
    {
        public int EnquiryId { get; set; }
        public double BMI { get; set; }
        public double WaterIntake { get; set; }
        public double IdealBodyWeight { get; set; }
        public double BMR { get; set; }
        public string FirstName { get; set; }
        public object LastName { get; set; }
        public string ContactNumber { get; set; }
        public string Email_ID { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Pin_Code { get; set; }
        public string Country { get; set; }
        public string AgeGroup { get; set; }
    }

    public class Table1
    {
        public int ChartId { get; set; }
        public string Type { get; set; }
        public string Age { get; set; }
        public string Weight { get; set; }
        public string height { get; set; }
        public DateTime CreatedDate { get; set; }
        public object ModifiledBy { get; set; }
        public object ModifiledDate { get; set; }
    }

    public class Data
    {
        public List<Table> Table { get; set; }
        public List<Table1> Table1 { get; set; }
    }

    public class Root
    {
        public string GUID { get; set; }
        public int ReferenceID { get; set; }
        public object FieldName { get; set; }
        public bool IsValid { get; set; }
        public bool UpdateRecord { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public Data Data { get; set; }
    }

    public class WFLRoot
    {
        public string GUID { get; set; }
        public int ReferenceID { get; set; }
        public object FieldName { get; set; }
        public bool IsValid { get; set; }
        public bool UpdateRecord { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<WFLGirl> Data { get; set; }
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
       // public int MasterTable_ID { get; set; }
       public string ControlName { get; set; }
        public string OptionValue { get; set; }
        //public int MasterTableTypeId { get; set; }
        public string OtherOption { get; set; }
    }
    public class Datum
    {
        public int EnquiryId { get; set; }
        public double BMI { get; set; }
        public double WaterIntake { get; set; }
        public double IdealBodyWeight { get; set; }
        public double BMR { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string Email_ID { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Pin_Code { get; set; }
        public string Country { get; set; }
        public string AgeGroup { get; set; }
    }

 
    public class AgeRange
    {
        public string Age { get; set; }
        public string Weigth { get; set; }
        public string Heigth { get; set; }
    }

        public class WFLBoy
    {
        public string Id { get; set; }
        public string Length_cm { get; set; }
        public string L { get; set; }
        public string M { get; set; }
        public string S { get; set; }
        public string First { get; set; }
        public string Third { get; set; }
        public string Fifth { get; set; }
        public string Fifteenth { get; set; }
        public string TwnetyFive { get; set; }
        public string Fifty { get; set; }
        public string SevenyFive { get; set; }
        public string EightyFive { get; set; }
        public string NintyFive { get; set; }
        public string NintySeven { get; set; }
        public string NintyNine { get; set; }

    }

    public class WFLGirl
    {
        public string Id { get; set; }
        public string Length_cm { get; set; }
        public string L { get; set; }
        public string M { get; set; }
        public string S { get; set; }
        public string First { get; set; }
        public string Third { get; set; }
        public string Fifth { get; set; }
        public string Fifteenth { get; set; }
        public string TwnetyFive { get; set; }
        public string Fifty { get; set; }
        public string SevenyFive { get; set; }
        public string EightyFive { get; set; }
        public string NintyFive { get; set; }
        public string NintySeven { get; set; }
        public string NintyNine { get; set; }

    }
    //BMI0205Girl
    public class BMI0205Girl
    {
        public string Id { get; set; }
        public string Length_cm { get; set; }
        public string L { get; set; }
        public string M { get; set; }
        public string S { get; set; }
        public string First { get; set; }
        public string Third { get; set; }
        public string Fifth { get; set; }
        public string Fifteenth { get; set; }
        public string TwnetyFive { get; set; }
        public string Fifty { get; set; }
        public string SevenyFive { get; set; }
        public string EightyFive { get; set; }
        public string NintyFive { get; set; }
        public string NintySeven { get; set; }
        public string NintyNine { get; set; }

    }
    //BMI0205Boy
    public class BMI0205Boy
    {
        public string Id { get; set; }
        public string Length_cm { get; set; }
        public string L { get; set; }
        public string M { get; set; }
        public string S { get; set; }
        public string First { get; set; }
        public string Third { get; set; }
        public string Fifth { get; set; }
        public string Fifteenth { get; set; }
        public string TwnetyFive { get; set; }
        public string Fifty { get; set; }
        public string SevenyFive { get; set; }
        public string EightyFive { get; set; }
        public string NintyFive { get; set; }
        public string NintySeven { get; set; }
        public string NintyNine { get; set; }

    }

    public class BMI0518Boy
    {
        public string Id { get; set; }
        public string Length_cm { get; set; }
        public string Third { get; set; }
        public string Fifth { get; set; }
        public string Tenth { get; set; }
        public string TwnetyFive { get; set; }
        public string Fifty { get; set; }
        public string TwentyThree { get; set; }
        public string TwentySeven { get; set; }
        public string NintyNine { get; set; }

    }
    public class BMI0518Girl
    {
        public string Id { get; set; }
        public string Length_cm { get; set; }
        public string Third { get; set; }
        public string Fifth { get; set; }
        public string Tenth { get; set; }
        public string TwnetyFive { get; set; }
        public string Fifty { get; set; }
        public string TwentyThree { get; set; }
        public string TwentySeven { get; set; }
        public string NintyNine { get; set; }

    }

}

