using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCalculator.Web.EntityModel
{
  public  class SysMasterTableModel
    {
        public int SysMasterTable_id { get; set; }
        public Nullable<int> SysMasterTabletype_id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Nullable<int> Parent_id { get; set; }
        public string Remark { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy_ID { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy_ID { get; set; }
        public Nullable<bool> ISDeleted { get; set; }
        public Nullable<bool> IsDefault { get; set; }
        public string Operation { get; set; }
        public int UserID { get; set; }
    }
}
