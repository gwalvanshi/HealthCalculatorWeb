using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCalculator.Web.EntityModel
{
  public class EatingToolModel
    {
        public int IsValid { get; set; }
        public int Trackerfalldays { get; set; }
        public int PatternCount { get; set; }
        public int MessageCount { get; set; }
    }
}
