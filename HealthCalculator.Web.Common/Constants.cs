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
        //public const string Default_UserId =; ConfigurationManager.AppSettings["DefaultUser"]
        public const string API_DROPDOWN = "api/GetDropdownData";
        public const string API_SAVEMASTERTABLE = "api/SaveMasterTable";
        public const string API_GETSYSTEMMASTERTABLEDATA = "api/GetSystemMasterTableData";
        public const string API_GETSYSTEMMASTERTABLE = "api/GetSystemMasterTable";
        public const string API_GETGRIDDATA = "api/GetGridData";
        public const string GENERIC_CRUD_API_URL = "api/PerformDataOperation";
        public const string GENERIC_API_GETRECORDS = "api/GetRecords";
      
        public const string GENERIC_CRUD_DATASETRETURN_API_URL = "api/PerformDataOperationDataSet";
        
        #region ScreenID Constant
        public const string MasterTable_ScreenID = "100";
        public const string MasterTableType_ScreenID = "101";
        public const string Enquiry_ScreenID = "102";
        
        #endregion
    }
}
