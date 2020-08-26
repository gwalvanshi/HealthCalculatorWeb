using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCalculator.Web.EntityModel
{
   public class AgeModel
    {
        public long AgeID { get; set; }
        public Nullable<int> Age { get; set; }
        public Nullable<int> Createdby { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiledBy { get; set; }
        public Nullable<System.DateTime> ModifiledDate { get; set; }
        public Nullable<int> ActionBy { get; set; }
    }
}
