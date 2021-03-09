
//using HealthCalculator.Web.Common;
//using HealthCalculator.Web.EntityModel;
//using HealthCalculator.Web.Service;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.IO;
//using System.Linq;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Mvc;

using HealthCalculator.Web.Common;
using HealthCalculator.Web.EntityModel;
using HealthCalculator.Web.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace HealthCalculator.Web.Controllers
{
    public class AdminController : Controller
    {
        [SessionExpireFilterAttributeAdmin]
        public ActionResult CoupenMaster()
        {
            var chkTimeOut = Session.Timeout;
            if (chkTimeOut < 10)
            {
                // set new time out to session  
                Session.Timeout = 60;

            }
            return View();
        }

        public async Task<JsonResult> SavePromocode(CoupenModel collection)
        {
            try
            {
                CommonMethods objCommonMethods = new CommonMethods();
                GenericOperationModel SendObjData = new GenericOperationModel();
                SendObjData.ScreenID = "129";
                SendObjData.UserID = Session["UserID"] != null ? Convert.ToInt32(Session["UserID"]) : Convert.ToInt32(ConfigurationManager.AppSettings["DefaultUser"].ToString()); ;
                SendObjData.Operation = "Add";
                string stringTOXml = objCommonMethods.GetXMLFromObject(collection);
                SendObjData.XML = stringTOXml;

                GenericService _genericService = new GenericService();
                var stringContent = new StringContent(JsonConvert.SerializeObject(SendObjData).ToString(), Encoding.UTF8, "application/json");
                var status = await _genericService.PerformDataOperationList<CoupenModel>(stringContent);

                return new JsonResult { Data = status };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }
        }
        public async Task<JsonResult> GetPromocodeView(string CoupenId)
        {
            try
            {
                int loggedIdUserID = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultUser"].ToString());
                GenericService _genericService = new GenericService();
                IndexScreenParameterModel collection = new IndexScreenParameterModel();
                collection.ScreenID = "129";
                collection.UserId = loggedIdUserID;
                if (CoupenId != "0")
                {
                    var objList = new List<IndexScreenSearchParameterModel>();
                    var obj1 = new IndexScreenSearchParameterModel
                    {
                        SearchParameter = "CoupenId",
                        SearchParameterDataType = "int",
                        SearchParameterValue = CoupenId
                    };
                    objList.Add(obj1);
                    collection.IndexScreenSearchParameterModel = objList;
                }
                var stringContent1 = new StringContent(JsonConvert.SerializeObject(collection).ToString(), Encoding.UTF8, "application/json");
                var objCommunication = await _genericService.GetRecords<CoupenModeldata>(stringContent1);


                return new JsonResult { Data = objCommunication, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }
        }




    }



}