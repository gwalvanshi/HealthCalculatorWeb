using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCalculator.Web.EntityModel
{
    public class Assessment
    {
        public string TableType { get; set; }
        public int UserId { get; set; }
        public UserGeneralInfo UserGeneralInfo { get; set; }
        public UserPhoto UserPhoto { get; set; }
        public UserAnthropometry UserAnthropometry { get; set; }
        public UserMedicalHistory UserMedicalHistory { get; set; }

        public List<UserMedications> UserMedications { get; set; }
        public UserDietaryAndLifeStyleDetails UserDietaryAndLifeStyleDetails { get; set; }
        public UserDirectCall UserDirectCall { get; set; }
        public UserFoodFequency UserFoodFequency { get; set; }
        public UserAnyKeyInSights UserAnyKeyInSights { get; set; }

    }
    public class UserGeneralInfo
    {
        public Int64 UserId { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string EmailID { get; set; }
        public int PhoneNumber { get; set; }
        public string Caste { get; set; }
        public string MaritalStatus { get; set; }
        public int DoYouHaveChildren { get; set; }
        public string TypeOfFamily { get; set; }

        public int CurrentlyWorking { get; set; }
        public string Occupation { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedWhen { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedWhen { get; set; }

    }

    public class UserPhoto
    {
        public Int64 UserId { get; set; }
        public string PhotoURL { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedWhen { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedWhen { get; set; }
        public string PhotoURLAfter { get; set; }
    }
    public class UserAnthropometry
    {
        public Int64 UserId { get; set; }
        public string Weight { get; set; }
        public string WeightType { get; set; }
        public string Height { get; set; }
        public string HeightType { get; set; }
        public string TargetWeight { get; set; }
        public string TargetWeightType { get; set; }
        public string YourGoals { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedWhen { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedWhen { get; set; }
    }
    public class UserMedicalHistory
    {
        public Int64 UserId { get; set; }
        public string HealthIssues { get; set; }
        public string Reports { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedWhen { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedWhen { get; set; }
        public string ReportPath { get; set; }
    }
    public class UserMedications
    {

        public Int64 UserId { get; set; }
        public string HealthIssues { get; set; }
        public string Medication { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedWhen { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedWhen { get; set; }
    }
    public class UserDietaryAndLifeStyleDetails
    {

        public Int64 UserId { get; set; }
        public string Lifestyle { get; set; }
        public string EatingHabits { get; set; }
        public string Frequencyofsmoking { get; set; }
        public string Frequencyofalcoholconsumption { get; set; }
        public string WaterintakeperdayType { get; set; }
        public string Waterintakeperday { get; set; }
        public string Frequencyofrestaurantvisit { get; set; }
        public string Preferredcuisine { get; set; }
        public string Whocooksathome { get; set; }
        public string FoodAversions { get; set; }
        public string FoodPreferences { get; set; }
        public string FoodAllergies { get; set; }


        public string AddedBy { get; set; }
        public DateTime AddedWhen { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedWhen { get; set; }
    }
    public class UserDirectCall
    {

        public Int64 UserId { get; set; }
        public string OnwakingupTime { get; set; }
        public string OnwakingupConsumption { get; set; }
        public string BreakfastTime { get; set; }
        public string BreakfastConsumption { get; set; }
        public string MidmorningTime { get; set; }
        public string MidmorningConsumption { get; set; }
        public string LunchTime { get; set; }
        public string LunchConsumption { get; set; }
        public string SnackTime { get; set; }
        public string SnackConsumption { get; set; }
        public string DinnerTime { get; set; }
        public string DinnerConsumption { get; set; }
        public string Bedtime { get; set; }
        public string BedConsumption { get; set; }

        public string MidnightMunchingTime { get; set; }
        public string MidnightConsumption { get; set; }

        public string AddedBy { get; set; }
        public DateTime AddedWhen { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedWhen { get; set; }
    }
    public class UserFoodFequency
    {

        public Int64 UserId { get; set; }
        public string Roti { get; set; }
        public string Rice { get; set; }
        public string BreadPav { get; set; }
        public string ChaatSnacks { get; set; }
        public string Nonveg { get; set; }
        public string Eggs { get; set; }
        public string Teacoffee { get; set; }
        public string Desert { get; set; }
        public string Fruits { get; set; }
        public string Nuts { get; set; }


        public string AddedBy { get; set; }
        public DateTime AddedWhen { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedWhen { get; set; }
    }
    public class UserAnyKeyInSights
    {
        public Int64 UserId { get; set; }
        public string Comments { get; set; }

        public string AddedBy { get; set; }
        public DateTime AddedWhen { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedWhen { get; set; }
    }

    public class ReturnAssessment
    {
        public string GUID { get; set; }
        public int ReferenceID { get; set; }
        public object FieldName { get; set; }
        public bool IsValid { get; set; }
        public bool UpdateRecord { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public AssementData Data { get; set; }
    }
    public class AssementData
    {
        public List<RspoonseAssessment> Table { get; set; }
        // public List<UserMedications> Table1 { get; set; }
        public List<RspoonseUserMedications> Table1 { get; set; }
        
    }
    public class UserNotification
    {
        public int Trackerfalldays { get; set; }
        public int PatternCount { get; set; }       
        public int MessageCount { get; set; }
        public int Trackerfilled { get; set; }

    }
    public class UserNotificationData
    {
        public string GUID { get; set; }
        public int ReferenceID { get; set; }
        public object FieldName { get; set; }
        public bool IsValid { get; set; }
        public bool UpdateRecord { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<UserNotification> Data { get; set; }
    }
    public class RspoonseAssessment
    {
        public int Trackerfalldays { get; set; }
        public int PatternCount { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int MessageCount { get; set; }
        public int UserId { get; set; }
        public string Gender { get; set; }

        public string UserName { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string EmailID { get; set; }
        public int PhoneNumber { get; set; }
        public string Caste { get; set; }
        public string MaritalStatus { get; set; }
        public int DoYouHaveChildren { get; set; }
        public string TypeOfFamily { get; set; }
        public int CurrentlyWorking { get; set; }
        public string Occupation { get; set; }
        public object PhotoURL { get; set; }
        public object PhotoURLAfter { get; set; }
        public string Weight { get; set; }
        public string WeightType { get; set; }
        public string Height { get; set; }
        public string HeightType { get; set; }
        public string TargetWeight { get; set; }
        public string TargetWeightType { get; set; }
        public string YourGoals { get; set; }
        public object HealthIssues { get; set; }
        public object Reports { get; set; }
        public object ReportPath { get; set; }
        public string UM_HealthIssues { get; set; }
        public string Medication { get; set; }
        public string Lifestyle { get; set; }
        public string EatingHabits { get; set; }
        public string Frequencyofsmoking { get; set; }
        public string Frequencyofalcoholconsumption { get; set; }
        public string WaterintakeperdayType { get; set; }
        public string Waterintakeperday { get; set; }
        public string Frequencyofrestaurantvisit { get; set; }
        public string Preferredcuisine { get; set; }
        public string Whocooksathome { get; set; }
        public string FoodAversions { get; set; }
        public string FoodPreferences { get; set; }
        public string FoodAllergies { get; set; }
        public string OnwakingupTime { get; set; }
        public string OnwakingupConsumption { get; set; }
        public string BreakfastTime { get; set; }
        public string BreakfastConsumption { get; set; }
        public string MidmorningTime { get; set; }
        public string MidmorningConsumption { get; set; }
        public string LunchTime { get; set; }
        public string LunchConsumption { get; set; }
        public string SnackTime { get; set; }
        public string SnackConsumption { get; set; }
        public string DinnerTime { get; set; }
        public string DinnerConsumption { get; set; }
        public string Bedtime { get; set; }
        public string BedConsumption { get; set; }
        public string MidnightMunchingTime { get; set; }
        public string MidnightConsumption { get; set; }

        public string Roti { get; set; }
        public string Rice { get; set; }
        public string BreadPav { get; set; }
        public string ChaatSnacks { get; set; }
        public string Nonveg { get; set; }
        public string Eggs { get; set; }
        public string Teacoffee { get; set; }
        public string Desert { get; set; }
        public string Fruits { get; set; }
        public string Nuts { get; set; }
        public string Comments { get; set; }

        public bool ? IsSubmitUGI { get; set; }
        public bool ? IsSubmitUP { get; set; }
        public bool ? IsSubmitUA { get; set; }
        public bool ? IsSubmitUMH { get; set; }
        public bool ? IsSubmitUM { get; set; }
        public bool ? IsSubmitUDALS { get; set; }
        public bool ? IsSubmitUDC { get; set; }
        public bool ? IsSubmitUFF { get; set; }
        public bool ? IsSubmitUAKIS { get; set; }

    }

    public class RspoonseUserMedications
    {

        public Int64 UserId { get; set; }
        public string HealthIssues { get; set; }
        public string Medication { get; set; }
        public bool IsSubmitUM { get; set; }
    }
}
