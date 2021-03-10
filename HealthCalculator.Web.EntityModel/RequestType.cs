using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCalculator.Web.EntityModel
{
  public class RequestType
    {
        public string Type { get; set; }
        public string OperationType { get; set; }
        public int UserId { get; set; }
        public int Module { get; set; }

    }

    public class NotificationModelRoot
    {
        public string GUID { get; set; }
        public int ReferenceID { get; set; }
        public object FieldName { get; set; }
        public bool IsValid { get; set; }
        public bool UpdateRecord { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<NotificationModel> Data { get; set; }
    }
    public class UserRoot
    {
        public string GUID { get; set; }
        public int ReferenceID { get; set; }
        public object FieldName { get; set; }
        public bool IsValid { get; set; }
        public bool UpdateRecord { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<LoginEntity> Data { get; set; }
    }

    public class UserRootModelView
    {
        public List<LoginEntity> Table { get; set; }
    }
    public class NotificationModelView
    {
        public List<NotificationModel> Table { get; set; }
    }
    public class NotificationModel
    {
        public int EnquiryCount { get; set; }
        public int AssessmentCount { get; set; }
        public int NewUserCount { get; set; }
        public int NewOrder { get; set; }
        public int TrackerFilled { get; set; }
        public int Trackerfalldays { get; set; }
        public int MessageCount { get; set; }
        public int CompletedCount { get; set; }
        public int ActiveCount { get; set; }
        public int DeActiveCount { get; set; }

    }
}
