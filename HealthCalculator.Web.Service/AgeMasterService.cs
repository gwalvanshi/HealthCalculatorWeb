using HealthCalculator.Web.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HealthCalculator.Web.Service
{
   public class AgeMasterService
    {
        public async Task<HttpCustomResponse<int>> SaveAge(HttpContent collection)
        {
            return await HttpUtil.PostAsync<int>(collection, AgeMasterConstants.AGE_SAVE_API, false);
        }
        public async Task<HttpCustomResponse<string>> GetAge(int ? ageId)
        {
            return await HttpUtil.GetAsyncDataSet<string>(AgeMasterConstants.AGE_GET_API+ "?age="+ ageId);
        }
    }
}
