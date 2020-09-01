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
   public class GridBindService
    {
        public async Task<HttpCustomResponse<GridResponseModel>> GetGridData(HttpContent collection)
        {
            return await HttpUtil.PostAsync<GridResponseModel>(collection, Constants.API_GETGRIDDATA,false);
        }
    }
}
