using HealthCalculator.Web.Common;
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
    public class HomeController : Controller
    {
        private const string Subject = "Eating Smart Health Rating";

        #region Program

        public ActionResult payments()
        {
            return View();
            // return RedirectToAction("Index", "User");
        }

        public ActionResult helpDesk()
        {
            return View();
            // return RedirectToAction("Index", "User");
        }
        public ActionResult MyEatingPattern()
        {
            return View();
            // return RedirectToAction("Index", "User");
        }
        public ActionResult MyEatingSmartTools()
        {
            return View();
            // return RedirectToAction("Index", "User");
        }


        public ActionResult tracker()
        {
            return View();
            // return RedirectToAction("Index", "User");
        }
        public ActionResult assessment()
        {
            return View();
            // return RedirectToAction("Index", "User");
        }

        public ActionResult register()
        {
            return View();
            // return RedirectToAction("Index", "User");
        }
       
        public ActionResult contact()
        {
            return View();
        }
        public ActionResult faq()
        {
            return View();
        }
        public ActionResult privacyPolicy()
        {
            return View();
        }
        public ActionResult careers()
        {
            return View();
        }
        public ActionResult testimonials()
        {
            return View();
        }
        public ActionResult corporateWellness()
        {
            return View();
        }
        public ActionResult blogs()
        {
            return View();
        }
        public ActionResult smartWeightLoss()
        {
            return View();
        }

        public ActionResult smartWeightGain()
        {
            return View();
        }
        public ActionResult smartManagementForDisease()
        {
            return View();
        }
        public ActionResult smartBodyToningBuilding()
        {
            return View();
        }
        public ActionResult smartPsychologicalWellBeing()
        {
            return View();
        }
        public ActionResult smartEatingOut()
        {
            return View();
        }
        public ActionResult smartEnhancer()
        {
            return View();
        }
        public ActionResult smartChildNutrition()
        {
            return View();
        }
        public ActionResult smartEatingForSpecialChildren()
        {
            return View();
        }
        public ActionResult eatingSmartTools()
        {
            return View();
        }
        public ActionResult login()
        {
            return View();
        }

        #endregion
        public ActionResult about()
        {
            return View();
            // return RedirectToAction("Index", "User");
        }
        public ActionResult howItWorks()
        {
            return View();
            // return RedirectToAction("Index", "User");
        }
        public ActionResult Index()
        {           
            return View();
            // return RedirectToAction("Index", "User");
        }
        public ActionResult ShowGraph()
        {
            return View();
            // return RedirectToAction("Index", "User");
        }

        public ActionResult ShowWFLGirl()
        {
            return View();
            // return RedirectToAction("Index", "User");
        }

        public ActionResult ShowBMI0205Boy()
        {
            return View();
            // return RedirectToAction("Index", "User");
        }

        public ActionResult ShowBMI0205Girl()
        {
            return View();
            // return RedirectToAction("Index", "User");
        }

        public ActionResult ShowBMI0518Boy()
        {
            return View();
            // return RedirectToAction("Index", "User");
        }

        public ActionResult ShowBMI0518Girl()
        {
            return View();
            // return RedirectToAction("Index", "User");
        }       

       
        [HttpGet]
        public async Task<JsonResult> GetBMI0518Girl(string Enq)
        {
            try
            {
                GraphType objGraphType = new GraphType();
                objGraphType.EnquiryId = Enq;
                objGraphType.Type = "G518";

                CommonMethods objCommonMethods = new CommonMethods();
                GenericOperationModel SendObjData = new GenericOperationModel();
                SendObjData.ScreenID = "109";
                SendObjData.UserID = Session["UserID"] != null ? Convert.ToInt32(Session["UserID"]) : Constants.Default_UserId; ;
                SendObjData.Operation = "ADD";

                string stringTOXml = objCommonMethods.GetXMLFromObject(objGraphType);
                SendObjData.XML = stringTOXml;

                GenericService _genericService = new GenericService();
                var stringContent = new StringContent(JsonConvert.SerializeObject(SendObjData).ToString(), Encoding.UTF8, "application/json");
                var status = await _genericService.PerformDataOperationList<WFLRootGirl0518Girl>(stringContent);
                return new JsonResult { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }
        }
        [HttpGet]
        public async Task<JsonResult> GetBMI0518Boy(string Enq)
        {
            try
            {
                GraphType objGraphType = new GraphType();
                objGraphType.EnquiryId = Enq;
                objGraphType.Type = "G518";

                CommonMethods objCommonMethods = new CommonMethods();
                GenericOperationModel SendObjData = new GenericOperationModel();
                SendObjData.ScreenID = "109";
                SendObjData.UserID = Session["UserID"] != null ? Convert.ToInt32(Session["UserID"]) : Constants.Default_UserId; ;
                SendObjData.Operation = "ADD";

                string stringTOXml = objCommonMethods.GetXMLFromObject(objGraphType);
                SendObjData.XML = stringTOXml;

                GenericService _genericService = new GenericService();
                var stringContent = new StringContent(JsonConvert.SerializeObject(SendObjData).ToString(), Encoding.UTF8, "application/json");
                var status = await _genericService.PerformDataOperationList<WFLRootGirl0518Boy>(stringContent);
                return new JsonResult { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetBMI0205Boy(string Enq)
        {
            try
            {
                GraphType objGraphType = new GraphType();
                objGraphType.EnquiryId = Enq;
                objGraphType.Type = "G25";

                CommonMethods objCommonMethods = new CommonMethods();
                GenericOperationModel SendObjData = new GenericOperationModel();
                SendObjData.ScreenID = "109";
                SendObjData.UserID = Session["UserID"] != null ? Convert.ToInt32(Session["UserID"]) : Constants.Default_UserId; ;
                SendObjData.Operation = "ADD";

                string stringTOXml = objCommonMethods.GetXMLFromObject(objGraphType);
                SendObjData.XML = stringTOXml;

                GenericService _genericService = new GenericService();
                var stringContent = new StringContent(JsonConvert.SerializeObject(SendObjData).ToString(), Encoding.UTF8, "application/json");
                var status = await _genericService.PerformDataOperationList<WFLRootGirl0205Boy>(stringContent);
                return new JsonResult { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }
        }
        [HttpGet]
        public async Task<JsonResult> GetBMI0205Girl(string Enq)
        {
            try
            {
                GraphType objGraphType = new GraphType();
                objGraphType.EnquiryId = Enq;
                objGraphType.Type = "G25";

                CommonMethods objCommonMethods = new CommonMethods();
                GenericOperationModel SendObjData = new GenericOperationModel();
                SendObjData.ScreenID = "109";
                SendObjData.UserID = Session["UserID"] != null ? Convert.ToInt32(Session["UserID"]) : Constants.Default_UserId; ;
                SendObjData.Operation = "ADD";

                string stringTOXml = objCommonMethods.GetXMLFromObject(objGraphType);
                SendObjData.XML = stringTOXml;

                GenericService _genericService = new GenericService();
                var stringContent = new StringContent(JsonConvert.SerializeObject(SendObjData).ToString(), Encoding.UTF8, "application/json");
                var status = await _genericService.PerformDataOperationList<WFLRootGirl0205Girl>(stringContent);
                return new JsonResult { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetWFLGirl(string Enq)
        {
            try
            {
                GraphType objGraphType = new GraphType();
                objGraphType.EnquiryId = Enq;
                objGraphType.Type = "G02";

                CommonMethods objCommonMethods = new CommonMethods();
                GenericOperationModel SendObjData = new GenericOperationModel();
                SendObjData.ScreenID = "109";
                SendObjData.UserID = Session["UserID"] != null ? Convert.ToInt32(Session["UserID"]) : Constants.Default_UserId; ;
                SendObjData.Operation = "ADD";

                string stringTOXml = objCommonMethods.GetXMLFromObject(objGraphType);
                SendObjData.XML = stringTOXml;

                GenericService _genericService = new GenericService();
                var stringContent = new StringContent(JsonConvert.SerializeObject(SendObjData).ToString(), Encoding.UTF8, "application/json");
                var status = await _genericService.PerformDataOperationList<WFLRootGirl>(stringContent);
                return new JsonResult { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetWFLBoy(string Enq)
        {
            try
            {
                GraphType objGraphType = new GraphType();
                objGraphType.EnquiryId = Enq;
                objGraphType.Type = "G02";
            
                CommonMethods objCommonMethods = new CommonMethods();
                GenericOperationModel SendObjData = new GenericOperationModel();
                SendObjData.ScreenID = "109";
                SendObjData.UserID = Session["UserID"] != null ? Convert.ToInt32(Session["UserID"]) : Constants.Default_UserId;
                SendObjData.Operation = "ADD";

                string stringTOXml = objCommonMethods.GetXMLFromObject(objGraphType);
                SendObjData.XML = stringTOXml;

                GenericService _genericService = new GenericService();
                var stringContent = new StringContent(JsonConvert.SerializeObject(SendObjData).ToString(), Encoding.UTF8, "application/json");
                var status = await _genericService.PerformDataOperationList<WFLRootBoy>(stringContent);             
                return new JsonResult { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }
        }

        [HttpPost]
        public async Task<JsonResult> Save(EnquiryModel collection)
        {
            try
            {
                CommonMethods objCommonMethods = new CommonMethods();
                GenericOperationModel SendObjData = new GenericOperationModel();
                SendObjData.ScreenID = Constants.Enquiry_ScreenID;
                SendObjData.UserID = Session["UserID"] != null ? Convert.ToInt32(Session["UserID"]) : Constants.Default_UserId;
                collection.Instance_enquiry.UserId = Session["UserID"] != null ? Convert.ToString(Session["UserID"]) : Convert.ToString(Constants.Default_UserId);
                SendObjData.Operation = "ADD";
                
                string stringTOXml = objCommonMethods.GetXMLFromObject(collection);
                SendObjData.XML = stringTOXml;

                GenericService _genericService = new GenericService();
                var stringContent = new StringContent(JsonConvert.SerializeObject(SendObjData).ToString(), Encoding.UTF8, "application/json");
                var status = await _genericService.PerformDataOperationDataList<Root>(stringContent);


                CommonMethods cm = new CommonMethods();
                if (!string.IsNullOrEmpty(collection.Instance_enquiry.Email_ID))
                {
                    cm.SendEmail(collection.Instance_enquiry.Email_ID, Subject, "", status.data.Data.Table, collection, status.data.Data.Table1);
                }
                return new JsonResult { Data = status };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }
        }
    }
}