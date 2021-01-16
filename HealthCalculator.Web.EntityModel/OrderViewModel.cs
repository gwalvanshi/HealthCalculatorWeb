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
}
