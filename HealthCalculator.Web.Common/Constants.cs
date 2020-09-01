using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace HealthCalculator.Web.Common
{
   public class Constants
    {
      //  public const string BASE_URL = ConfigurationManager.AppSettings["BaseURL"].ToString();
        public const string API_DROPDOWN = "api/GetDropdownData";
        public const string API_SAVEMASTERTABLE = "api/SaveMasterTable";
        public const string API_GETSYSTEMMASTERTABLEDATA = "api/GetSystemMasterTableData";
        public const string API_GETSYSTEMMASTERTABLE = "api/GetSystemMasterTable";
        public const string API_GETGRIDDATA = "api/GetGridData";
     
    }
}
