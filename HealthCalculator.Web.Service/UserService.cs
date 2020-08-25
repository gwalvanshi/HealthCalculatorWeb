using HealthCalculator.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HealthCalculator.Web.Service
{
   public class UserService
    {
        public async Task<HttpCustomResponse<int>> SaveUserDetails(HttpContent collection)
        {
            return await HttpUtil.PostAsync<int>(collection, UserConstant.USER_ADD_API, false);
        }
    }
}
