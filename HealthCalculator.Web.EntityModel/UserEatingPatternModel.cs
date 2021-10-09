using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCalculator.Web.EntityModel
{
   public class UserEatingPatternModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public string OrderId { get; set; }
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
        public string IsSubmit { get; set; }
        public string IsActive { get; set; }
        public string AddedBy { get; set; }
        public string AddedWhen { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedWhen { get; set; }
        public string SessionId { get; set; }
        public string CheckedByUser { get; set; }

    }
}
