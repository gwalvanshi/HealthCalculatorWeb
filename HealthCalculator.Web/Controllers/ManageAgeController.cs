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
    public class ManageAgeController: Controller
    {
        public ActionResult AgeMaster()
        {
            return View();
        }      


        [HttpPost]
        public async Task<JsonResult> AddAge(AgeModel collection)
        {

            AgeMasterService objLoginServices = new AgeMasterService();
            var stringContent = new StringContent(JsonConvert.SerializeObject(collection).ToString(), Encoding.UTF8, "application/json");
            var isAuth = await objLoginServices.SaveAge(stringContent);
            return new JsonResult { Data = isAuth };

        }
        [HttpGet]
        public async Task<JsonResult> GetAge(int ? Age)
        {
            AgeMasterService objLoginServices = new AgeMasterService();
            var isAuth = await objLoginServices.GetAge(Age);
            return new JsonResult { Data = isAuth };
        }
    }
}