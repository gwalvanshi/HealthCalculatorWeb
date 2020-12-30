using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCalculator.Web.EntityModel
{
   public class ProductModel
    {
        public int Id{get;set;}
      public int Program_Id{get;set;}
      public string ProductName{get;set;}
      public string INRRate{get;set;}
      public string DollerRate{get;set;}
      public string DiscountInValue{get;set;}
      public string DiscountInPer{get;set;}
      public bool IsDscount{get;set;}
      public bool Status{get;set;}

       public string Program { get; set; }
    }
}
