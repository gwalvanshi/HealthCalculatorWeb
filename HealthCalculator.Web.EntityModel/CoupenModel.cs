using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCalculator.Web.EntityModel
{
    public class CoupenModeldata
    {
        public int  CoupenId { get; set; }
        public string  Coupen { get; set; }
        public string Amount { get; set; }
        public string DollerAmount { get; set; }
        public int IsActive{ get; set; }

    }

    public class CoupenModel
    {           
        public string TableType { get; set; }
        public CoupenModeldata CoupenModeldata { get; set; }

    }
}
