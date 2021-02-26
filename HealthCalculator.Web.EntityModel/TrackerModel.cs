using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCalculator.Web.EntityModel
{
    public class TrackerModel
    {
        public Int64 OrderId { get; set; }
        public Int64 UserId { get; set; }
        public string OrderAmountINR { get; set; }
        public string OrderAmountDoller { get; set; }
        public string CoupenId { get; set; }
        public int ProductId { get; set; }
        public DateTime ? StartActiveDate { get; set; }
        public DateTime ? EndActiveDate { get; set; }
        public Int64 Totaldays { get; set; }
        public bool IsActive { get; set; }
        public Int64 GivenDays { get; set; }
        public string ProductName { get; set; }
        public string Program { get; set; }
    }
    public class Trackerdata
    {
        public string TableType { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public Int64 OrderId { get; set; }
        public List<UserTrackerWeigth> UserTrackerWeigth { get; set; }
        public List<UserTrackerInch> UserTrackerInch { get; set; }
    }
    public class UserTrackerWeigth
    {
        public Int64 UserId { get; set; }
        public int ProductId { get; set; }
        public Int64 OrderId { get; set; }
        public decimal ? Weight { get; set; }
        public decimal ? WeightDifference { get; set; }
        public DateTime? TrackerDate { get; set; }
        public Int64 AddedBy { get; set; }
        public DateTime? AddedWhen { get; set; }
        public Int64 UpdatedBy { get; set; }
        public DateTime? UpdatedWhen { get; set; }
        public string WeightIn { get; set; }
        

    }
    public class UserTrackerInch
    {
        public Int64 UserId { get; set; }
        public int ProductId { get; set; }
        public Int64 OrderId { get; set; }
        public decimal? Chest { get; set; }
        public decimal? Waist { get; set; }
        public decimal? Hip { get; set; }
        public DateTime? InchTrackerDate { get; set; }
        public Int64 AddedBy { get; set; }
        public DateTime? AddedWhen { get; set; }
        public Int64 UpdatedBy { get; set; }
        public DateTime? UpdatedWhen { get; set; }
        public string WeightIn { get; set; }


    }
    public class ReturnUserTrackerDetails
    {
        public string GUID { get; set; }
        public int ReferenceID { get; set; }
        public object FieldName { get; set; }
        public bool IsValid { get; set; }
        public bool UpdateRecord { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public UserDetailsTrackerData Data { get; set; }


    }
    public class UserDetailsTrackerData
    {
        public List<UserTrackerDetails> Table { get; set; }
        public List<UserInchTrackerDetails> Table1 { get; set; }
    }

    public class UserTrackerDetails
    {
        public int Trackerfalldays { get; set; }
        public int PatternCount { get; set; }
        public int MessageCount { get; set; }
        public Int64 UserId { get; set; }
        public int ProductId { get; set; }
        public Int64 OrderId { get; set; }
        public decimal ? Weight { get; set; }
        public decimal ? WeightDifference { get; set; }
        public DateTime? TrackerDate { get; set; }
      
        public string OrderAmountINR { get; set; }
        public string OrderAmountDoller { get; set; }
        public string CoupenId { get; set; }
        
        public DateTime? StartActiveDate { get; set; }
        public DateTime? EndActiveDate { get; set; }
        public Int64 Totaldays { get; set; }
        public bool IsActive { get; set; }
        public Int64 GivenDays { get; set; }
        public string ProductName { get; set; }
        public string Program { get; set; }
        public string WeightIn { get; set; }

    }

    public class UserInchTrackerDetails
    {
        public Int64 UserId { get; set; }
        public int ProductId { get; set; }
        public Int64 OrderId { get; set; }
        public decimal? Chest { get; set; }
        public decimal? Waist { get; set; }
        public decimal? Hip { get; set; }
        public DateTime? InchTrackerDate { get; set; }

        public string OrderAmountINR { get; set; }
        public string OrderAmountDoller { get; set; }
        public string CoupenId { get; set; }

        public DateTime? StartActiveDate { get; set; }
        public DateTime? EndActiveDate { get; set; }
        public Int64 Totaldays { get; set; }
        public bool IsActive { get; set; }
        public Int64 GivenDays { get; set; }
        public string ProductName { get; set; }
        public string Program { get; set; }
        public string WeightIn { get; set; }

    }

    public class ReturnTracker
    {
        public string GUID { get; set; }
        public int ReferenceID { get; set; }
        public object FieldName { get; set; }
        public bool IsValid { get; set; }
        public bool UpdateRecord { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public Trackerdata Data { get; set; }

        
    }

        public class Order
    {
        public Int64 OrderId { get; set; }
        public Int64 UserId { get; set; }
        public string OrderAmountINR { get; set; }
        public int OrderAmountDoller { get; set; }      
        public int CoupenId { get; set; }
        public int ProductId { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedWhen { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedWhen { get; set; }
    }
    public class OrderDetail
    {
        public Int64 OrderDetail_Id { get; set; }
        public Int64 Order_Id { get; set; }
        public Int64 ProductId { get; set; }
        public string INRRate { get; set; }
        public string DollerRate { get; set; }
        public string Discount { get; set; }
        public DateTime StartActiveDate { get; set; }
        public DateTime EndActiveDate { get; set; }
        public Int64 Totaldays { get; set; }
        public int IsActive { get; set; }             

        public string AddedBy { get; set; }
        public DateTime AddedWhen { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedWhen { get; set; }
    }
}
