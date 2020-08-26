using HealthCalculator.Web.EntityModel;
using HealthCalculator.Web.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HealthCalculator.Web.Controllers
{
    public class CommonMethodsController: Controller
    {
        

        [HttpGet]
        public async Task<JsonResult> GetDropDownData( int tableType )
        {
            DropdownRequestModel objDropdownRequestModel = new DropdownRequestModel();
            objDropdownRequestModel.LookType = tableType;
            DropdownLookupService objDropdownLookupService = new DropdownLookupService();
            var stringContent = new StringContent(JsonConvert.SerializeObject(objDropdownRequestModel).ToString(), Encoding.UTF8, "application/json");
            var data = await objDropdownLookupService.GetDropdown(stringContent);
            return new JsonResult { Data = data };

        }
    }
}