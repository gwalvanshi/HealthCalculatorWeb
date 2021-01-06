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
  
    public class ProductId
    {
       
        public int UserId { get; set; }
        public string ProductIds { get; set; }
    }
    public class ProductRoot
    {
        public string GUID { get; set; }
        public int ReferenceID { get; set; }
        public object FieldName { get; set; }
        public bool IsValid { get; set; }
        public bool UpdateRecord { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }        
        public List<ProductModel> Data { get; set; }
    }
}
