using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCalculator.Web.EntityModel
{
   public class vSysMasttablewithfilter
    {
        public int ID { get; set; }
        public string displayName { get; set; }
        public string SysMastertableName { get; set; }
        public Nullable<bool> isdefault { get; set; }
        public string Remark { get; set; }
        public Nullable<int> filterid { get; set; }
        public Nullable<int> parent_id { get; set; }
        public Nullable<bool> ISDeleted { get; set; }
    }
   
}
