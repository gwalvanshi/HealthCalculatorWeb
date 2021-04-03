using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCalculator.Web.EntityModel
{
    public class ProgramProductModel
    {
        public int productId { get; set; }
        public int Order_id { get; set; }
        public int UserId { get; set; }
        public bool IsFreeSession { get; set; }


    }

    
}
