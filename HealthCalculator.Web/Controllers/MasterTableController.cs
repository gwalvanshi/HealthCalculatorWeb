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
    public class MasterTableController: Controller
    {
        public ActionResult ManageMasterData()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> ManageSysMasterTable(SysMasterTableModel collection)
        {
           
            MasterTableService objLoginServices = new MasterTableService();
            var stringContent = new StringContent(JsonConvert.SerializeObject(collection).ToString(), Encoding.UTF8, "application/json");
            var status = await objLoginServices.ManageSysMasterTable(stringContent);
            return new JsonResult { Data = status, JsonRequestBehavior=JsonRequestBehavior.AllowGet };

        }
        [HttpGet]
        public async Task<JsonResult> GetSystemMasterTableData(int ?tableType)
        {
            MasterTableService objLoginServices = new MasterTableService();
            var status = await objLoginServices.GetSystemMasterTableData(tableType);
            return new JsonResult { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpGet]
        public async Task<JsonResult> GetSystemMasterTable(int id)
        {
            MasterTableService objLoginServices = new MasterTableService();
            var status = await objLoginServices.GetSystemMasterTable(id);
            return new JsonResult { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}