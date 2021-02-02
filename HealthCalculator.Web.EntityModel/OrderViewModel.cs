using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCalculator.Web.EntityModel
{
   public class OrderViewModel
    {
        
            public int OrderId { get; set; }
            public int UserId { get; set; }
            public string PaymentorderId { get; set; }
            public string PaymentType { get; set; }
            public bool IsPaymentDone { get; set; }
          
        
    }
    public class UserOrderViewModel
    {
        public int ProgramId { get; set; }
        public string UserId { get; set; }
        public string OrderDetail_Id { get; set; }
        public string Order_Id { get; set; }
        public string ProductId { get; set; }
        public string INRRate { get; set; }
        public string DollerRate { get; set; }

        public string OrderAmountINR { get; set; }
        public string OrderAmountDoller { get; set; }

        public string Discount { get; set; }
        public DateTime? StartActiveDate { get; set; }
        public DateTime? EndActiveDate { get; set; }
        public int? Totaldays { get; set; }
        public bool? IsActive { get; set; }

        public string ActivePlan { get; set; }
        public string ProductName { get; set; }
        public string Program { get; set; }
        public string PaymentDone { get; set; }
        public string PaymentType { get; set; }

        public string IsPaymentDone { get; set; }



    }
}
