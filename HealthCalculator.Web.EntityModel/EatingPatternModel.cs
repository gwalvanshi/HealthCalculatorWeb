using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCalculator.Web.EntityModel
{
    public class EatingPatternModel
    {
    }

    public class EatingPatterndata
    {
        public Int64 UserId { get; set; }
      //  public bool? IsSubmit { get; set; }
        public string TableType { get; set; }       
        public UserEatingPattern UserEatingPattern { get; set; }
    
    }
    public class UserEatingPattern
    {
        public Int64 UserId { get; set; }
        public int ProductId { get; set; }
        public Int64 OrderId { get; set; }

        public string OnRising { get; set; }      
        public string PreBreakfast { get; set; }
      
        public string Breakfast { get; set; }
      
        public string MidMorning { get; set; }
       
        public string PreWorkout { get; set; }
       
        public string DuringWorkout { get; set; }
       
        public string PreLunch { get; set; }
        
        public string Lunch { get; set; }
       
        public string TeaEvening { get; set; }
        
        public string LateEvening { get; set; }
       
        public string PreDinner { get; set; }
       
        public string Dinner { get; set; }
        
        public string PostDinner { get; set; }
       
        public string BedTime { get; set; }
       
        public string Recipes { get; set; }
       
        public string Notes { get; set; }
        public bool? IsSubmit { get; set; }
        public bool? IsActive { get; set; }
        public Int64 SessionId { get; set; }
        public Int64 AddedBy { get; set; }
        public DateTime? AddedWhen { get; set; }
        public Int64 UpdatedBy { get; set; }
        public DateTime? UpdatedWhen { get; set; }


    }
}
