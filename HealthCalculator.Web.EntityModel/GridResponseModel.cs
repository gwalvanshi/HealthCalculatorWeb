using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCalculator.Web.EntityModel
{
    public class GridResponseModel
    {
        public int RowCount { get; set; }
        public DataTable GridData { get; set; }
    }
}
