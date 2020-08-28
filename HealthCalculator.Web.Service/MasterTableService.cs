using HealthCalculator.Web.Common;
using HealthCalculator.Web.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HealthCalculator.Web.Service
{
   public class MasterTableService
    {
        public async Task<HttpCustomResponse<List<vSysMasttablewithfilter>>> GetSystemMasterTableData(int ? tableType)
        {
            return await HttpUtil.GetListAsync<List<vSysMasttablewithfilter>>(Constants.API_GETSYSTEMMASTERTABLEDATA + "?tableType=" + tableType);
        }
        public async Task<HttpCustomResponse<vSysMasttablewithfilter>> GetSystemMasterTable(int  id)
        {
            return await HttpUtil.GetAsync<vSysMasttablewithfilter>(Constants.API_GETSYSTEMMASTERTABLE+ "?id="+ id);
        }
        public async Task<HttpCustomResponse<int>> ManageSysMasterTable(HttpContent collection)
        {
            return await HttpUtil.PostAsync<int>(collection, Constants.API_SAVEMASTERTABLE, false);
        }
    }
}
