using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCalculator.Web.EntityModel
{
   public class GridViewRequestModel
    {
        public int LookType { get; set; }
        public int PageSize { get; set; }
        public int Skip { get; set; }
        public string SortColumn { get; set; }
        public string SortColumnDirection { get; set; }
        public string CommonSearch { get; set; }
        public string FixedSearch { get; set; }

    }
}
