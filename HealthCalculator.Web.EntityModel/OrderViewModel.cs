using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCalculator.Web.EntityModel
{
    public class ContactDetails
    {

        public string Email { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }


    }
    public class OrderViewModel
    {
        
            public int OrderId { get; set; }
            public int UserId { get; set; }
            public string PaymentorderId { get; set; }
            public string PaymentType { get; set; }
            public bool IsPaymentDone { get; set; }
          
        
    }
    public class CoupanModel
    {

        public int CoupenId { get; set; }
        public string Coupen { get; set; }
        public string Amount { get; set; }
        public string DollerAmount { get; set; }
        public bool IsActive { get; set; }


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

        public int CoupenId { get; set; }

        public DateTime? StartDate { get; set; }
        public bool? IsStarted { get; set; }

    }
}
