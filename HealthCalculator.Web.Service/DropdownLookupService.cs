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

    public class DropdownLookupService

    {

        public async Task<HttpCustomResponse<DropdownResponseModel>> GetDropdown(HttpContent collection)

        {

            return await HttpUtil.PostListAsync<DropdownResponseModel>(collection, Constants.API_DROPDOWN);

        }

    }

}