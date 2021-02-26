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
    public class ProgramController : Controller
    {
        // GET: Program

        private void Upload(string filePathTobeSaved)
        {
            HttpContext.Request.Files[0].SaveAs(filePathTobeSaved);
        }

        public JsonResult UploadDocument(string userID)
        {
            try
            {
                string returrMessage = string.Empty;
                HttpFileCollectionBase files = Request.Files;
                HttpPostedFileBase file = files[0];
                string[] testfiles;
                string fname = "";
                if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                {
                    testfiles = file.FileName.Split(new char[] { '\\' });
                    fname = testfiles[testfiles.Length - 1];
                }
                else
                {
                    fname = file.FileName;
                }

                string fileName = fname;
                string[] splString = fileName.Split('.');
                string newFile = userID + "_" + splString[0]; 
               
                string uploadNewFileName = newFile + "." + splString[1];
                string filePathTobeSaved = "";
                string baseurl = "";
               
                if (userID == "1")
                {
                   

                    
                    baseurl = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["ProfilePhotoFolderBefore"]);
                    returrMessage = ConfigurationManager.AppSettings["ProfilePhotoFolderBefore"].ToString().Replace("~", "") + "/" + uploadNewFileName;
                }
                else
                {
                    baseurl = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["ProfilePhotoFolderAfter"]);
                    returrMessage = ConfigurationManager.AppSettings["ProfilePhotoFolderAfter"].ToString().Replace("~", "") + "/" + uploadNewFileName;
                }

                filePathTobeSaved = baseurl + "/" + uploadNewFileName;
                Upload(filePathTobeSaved);

                return new JsonResult { Data = returrMessage, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }

        }

        public JsonResult UploadDocumentReport(string userID)
        {
            try
            {
                string returrMessage = string.Empty;
                HttpFileCollectionBase files = Request.Files;
                HttpPostedFileBase file = files[0];
                string[] testfiles;
                string fname = "";
                if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                {
                    testfiles = file.FileName.Split(new char[] { '\\' });
                    fname = testfiles[testfiles.Length - 1];
                }
                else
                {
                    fname = file.FileName;
                }
                string fileName = fname;
                string newFile = "UserID_" + userID;
                string[] splString = fileName.Split('.');
                string uploadNewFileName = newFile + "." + splString[1];
                string filePathTobeSaved = "";
                string baseurl = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["UploadReportFolder"]);
                filePathTobeSaved = baseurl + "/" + uploadNewFileName;
                Upload(filePathTobeSaved);
                returrMessage = ConfigurationManager.AppSettings["UploadReportFolder"].ToString().Replace("~", "") + "/" + uploadNewFileName;
                return new JsonResult { Data = returrMessage, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }

        }

        [HttpPost]
        public async Task<JsonResult> SavePoto(Assessment collection)
        {

            try
            {
                CommonMethods objCommonMethods = new CommonMethods();
                GenericOperationModel SendObjData = new GenericOperationModel();
                SendObjData.ScreenID = "110";
                SendObjData.UserID = Session["UserID"] != null ? Convert.ToInt32(Session["UserID"]) : Convert.ToInt32(ConfigurationManager.AppSettings["DefaultUser"].ToString()); ;
                SendObjData.Operation = "Add";
                string stringTOXml = objCommonMethods.GetXMLFromObject(collection);
                SendObjData.XML = stringTOXml;

                GenericService _genericService = new GenericService();
                var stringContent = new StringContent(JsonConvert.SerializeObject(SendObjData).ToString(), Encoding.UTF8, "application/json");
                var status = await _genericService.PerformDataOperationList<Assessment>(stringContent);

                return new JsonResult { Data = status };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }
        }

        [HttpPost]
        public async Task<JsonResult> SaveAsseeement(Assessment collection)
        {
            try
            {
                CommonMethods objCommonMethods = new CommonMethods();
                GenericOperationModel SendObjData = new GenericOperationModel();
                SendObjData.ScreenID = "110";
                SendObjData.UserID = Session["UserID"] != null ? Convert.ToInt32(Session["UserID"]) : Convert.ToInt32(ConfigurationManager.AppSettings["DefaultUser"].ToString()); 
                SendObjData.Operation = "Add";
                string stringTOXml = objCommonMethods.GetXMLFromObject(collection);
                SendObjData.XML = stringTOXml;

                GenericService _genericService = new GenericService();
                var stringContent = new StringContent(JsonConvert.SerializeObject(SendObjData).ToString(), Encoding.UTF8, "application/json");
                var status = await _genericService.PerformDataOperationList<Assessment>(stringContent);
                return new JsonResult { Data = status };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }
        }
        [HttpPost]
        public async Task<JsonResult> GetAsseeement(Assessment collection)
        {
            try
            {
                CommonMethods objCommonMethods = new CommonMethods();
                GenericOperationModel SendObjData = new GenericOperationModel();
                SendObjData.ScreenID = "110";
                SendObjData.UserID = Session["UserID"] != null ? Convert.ToInt32(Session["UserID"]) : Convert.ToInt32(ConfigurationManager.AppSettings["DefaultUser"].ToString());
                SendObjData.Operation = "Add";
                string stringTOXml = objCommonMethods.GetXMLFromObject(collection);
                SendObjData.XML = stringTOXml;

                GenericService _genericService = new GenericService();
                var stringContent = new StringContent(JsonConvert.SerializeObject(SendObjData).ToString(), Encoding.UTF8, "application/json");
                var status = await _genericService.PerformDataOperationDataList<ReturnAssessment>(stringContent);


                return new JsonResult { Data = status };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }
        }
        public ActionResult register()
        {
            return View();
            // return RedirectToAction("Index", "User");
        }
        public ActionResult about()
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
        [HttpPost]
        public async Task<JsonResult> SaveMessage(MessageMaster collection)
        {
            try
            {
                CommonMethods objCommonMethods = new CommonMethods();
                GenericOperationModel SendObjData = new GenericOperationModel();
                SendObjData.ScreenID = "111";
                SendObjData.UserID = Session["UserID"] != null ? Convert.ToInt32(Session["UserID"]) : Convert.ToInt32(ConfigurationManager.AppSettings["DefaultUser"].ToString());
                SendObjData.Operation = "Add";
                string stringTOXml = objCommonMethods.GetXMLFromObject(collection);
                SendObjData.XML = stringTOXml;

                GenericService _genericService = new GenericService();
                var stringContent = new StringContent(JsonConvert.SerializeObject(SendObjData).ToString(), Encoding.UTF8, "application/json");
                var status = await _genericService.PerformDataOperationList<MessageMaster>(stringContent);

                return new JsonResult { Data = status };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }
        }
        [HttpPost]
        public async Task<JsonResult> SaveMessageNotification(MessageMaster collection)
        {
            try
            {
                CommonMethods objCommonMethods = new CommonMethods();
                GenericOperationModel SendObjData = new GenericOperationModel();
                SendObjData.ScreenID = "118";
                SendObjData.UserID = Session["UserID"] != null ? Convert.ToInt32(Session["UserID"]) : Convert.ToInt32(ConfigurationManager.AppSettings["DefaultUser"].ToString());
                SendObjData.Operation = "Add";
                string stringTOXml = objCommonMethods.GetXMLFromObject(collection);
                SendObjData.XML = stringTOXml;

                GenericService _genericService = new GenericService();
                var stringContent = new StringContent(JsonConvert.SerializeObject(SendObjData).ToString(), Encoding.UTF8, "application/json");
                var status = await _genericService.PerformDataOperationList<MessageMaster>(stringContent);

                return new JsonResult { Data = status };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }
        }
        [HttpGet]
        public async Task<JsonResult> GetMessage(int MessageId)
        {
            try
            {
                int loggedIdUserID = Convert.ToInt32(Session["UserID"]);
                GenericService _genericService = new GenericService();
                IndexScreenParameterModel collection = new IndexScreenParameterModel();
                collection.ScreenID = "112";
                collection.UserId = loggedIdUserID;
                collection.IndexScreenSearchParameterModel = new List<IndexScreenSearchParameterModel>()
                {
                    new IndexScreenSearchParameterModel
                    {
                        SearchParameter = "MessageId",
                        SearchParameterDataType = "int",
                        SearchParameterValue = MessageId.ToString()
                    }
                };
                var stringContent1 = new StringContent(JsonConvert.SerializeObject(collection).ToString(), Encoding.UTF8, "application/json");
                var objCommunication = await _genericService.GetRecords<MessageMaster>(stringContent1);

                return new JsonResult { Data = objCommunication, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }
        }
        [HttpGet]
        public async Task<JsonResult> GetProductProgram(string userId)
        {
            try
            {

                GenericService _genericService = new GenericService();
                IndexScreenParameterModel collection = new IndexScreenParameterModel();
                collection.ScreenID = "125";
                collection.UserId = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultUser"].ToString());
                collection.IndexScreenSearchParameterModel = new List<IndexScreenSearchParameterModel>()
                {
                    new IndexScreenSearchParameterModel
                    {
                        SearchParameter = "UserId",
                        SearchParameterDataType = "int",
                        SearchParameterValue = userId
                    }
                };


                var stringContent1 = new StringContent(JsonConvert.SerializeObject(collection).ToString(), Encoding.UTF8, "application/json");
                var objCommunication = await _genericService.GetRecords<ProgramProductModel>(stringContent1);

                return new JsonResult { Data = objCommunication, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


                //GenericService _genericService = new GenericService();
                //IndexScreenParameterModel collection = new IndexScreenParameterModel();
                //collection.ScreenID = "113";
                //collection.UserId = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultUser"].ToString());

                //var stringContent1 = new StringContent(JsonConvert.SerializeObject(collection).ToString(), Encoding.UTF8, "application/json");
                //var objCommunication = await _genericService.GetRecords<ProductModel>(stringContent1);

                //return new JsonResult { Data = objCommunication, JsonRequestBehavior = JsonRequestBehavior.AllowGet };




            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }
        }


        [HttpGet]
        public async Task<JsonResult> GetProduct(int Program_Id)
        {
            try
            {
             
                GenericService _genericService = new GenericService();
                IndexScreenParameterModel collection = new IndexScreenParameterModel();
                collection.ScreenID = "113";
                collection.UserId =  Convert.ToInt32(ConfigurationManager.AppSettings["DefaultUser"].ToString()); 
                collection.IndexScreenSearchParameterModel = new List<IndexScreenSearchParameterModel>()
                {
                    new IndexScreenSearchParameterModel
                    {
                        SearchParameter = "Program_Id",
                        SearchParameterDataType = "int",
                        SearchParameterValue = Program_Id.ToString()
                    }
                };
                var stringContent1 = new StringContent(JsonConvert.SerializeObject(collection).ToString(), Encoding.UTF8, "application/json");
                var objCommunication = await _genericService.GetRecords<ProductModel>(stringContent1);

                return new JsonResult { Data = objCommunication, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }
        }
        public async Task<JsonResult> GetCartProduct(string Id)
        {

            try
            {
                ProductId objGraphType = new ProductId();
                objGraphType.ProductIds = Id;
                objGraphType.UserId = Session["UserID"] != null ? Convert.ToInt32(Session["UserID"]) : Convert.ToInt32(ConfigurationManager.AppSettings["DefaultUser"].ToString());

                CommonMethods objCommonMethods = new CommonMethods();
                GenericOperationModel SendObjData = new GenericOperationModel();
                SendObjData.ScreenID = "113";
                SendObjData.UserID = Session["UserID"] != null ? Convert.ToInt32(Session["UserID"]) : Convert.ToInt32(ConfigurationManager.AppSettings["DefaultUser"].ToString());
                SendObjData.Operation = "ADD";

                string stringTOXml = objCommonMethods.GetXMLFromObject(objGraphType);
                SendObjData.XML = stringTOXml;

                GenericService _genericService = new GenericService();
                var stringContent = new StringContent(JsonConvert.SerializeObject(SendObjData).ToString(), Encoding.UTF8, "application/json");
                var status = await _genericService.PerformDataOperationList<ProductRoot>(stringContent);
                return new JsonResult { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }

        }
        public async Task<JsonResult> GetTracker(int userId)
        {
            try
            {
                int loggedIdUserID = Convert.ToInt32(Session["UserID"]);
                GenericService _genericService = new GenericService();
                IndexScreenParameterModel collection = new IndexScreenParameterModel();
                collection.ScreenID = "115";
                collection.UserId = loggedIdUserID;
                collection.IndexScreenSearchParameterModel = new List<IndexScreenSearchParameterModel>()
                {
                    new IndexScreenSearchParameterModel
                    {
                        SearchParameter = "userId",
                        SearchParameterDataType = "int",
                        SearchParameterValue = userId.ToString()
                    }
                };
                var stringContent1 = new StringContent(JsonConvert.SerializeObject(collection).ToString(), Encoding.UTF8, "application/json");
                var objCommunication = await _genericService.GetRecords<TrackerModel>(stringContent1);

                return new JsonResult { Data = objCommunication, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }
        }


        [HttpPost]
        public async Task<JsonResult> SaveTracker(Trackerdata collection)
        {
            try
            {
                CommonMethods objCommonMethods = new CommonMethods();
                GenericOperationModel SendObjData = new GenericOperationModel();
                SendObjData.ScreenID = "115";
                SendObjData.UserID = Session["UserID"] != null ? Convert.ToInt32(Session["UserID"]) : Convert.ToInt32(ConfigurationManager.AppSettings["DefaultUser"].ToString());
                SendObjData.Operation = "Add";
                string stringTOXml = objCommonMethods.GetXMLFromObject(collection);
                SendObjData.XML = stringTOXml;

                GenericService _genericService = new GenericService();
                var stringContent = new StringContent(JsonConvert.SerializeObject(SendObjData).ToString(), Encoding.UTF8, "application/json");
                var status = await _genericService.PerformDataOperationList<Trackerdata>(stringContent);
                return new JsonResult { Data = status };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }
        }

        [HttpPost]
        public async Task<JsonResult> GetUserEnquiryDetails(int  userId)
        {
            try
            {
                Trackerdata collection = new Trackerdata();
                collection.UserId = userId;
                CommonMethods objCommonMethods = new CommonMethods();
                GenericOperationModel SendObjData = new GenericOperationModel();
                SendObjData.ScreenID = "126";
                SendObjData.UserID = Session["UserID"] != null ? Convert.ToInt32(Session["UserID"]) : Convert.ToInt32(ConfigurationManager.AppSettings["DefaultUser"].ToString());
                SendObjData.Operation = "Add";
                string stringTOXml = objCommonMethods.GetXMLFromObject(collection);
                SendObjData.XML = stringTOXml;

                GenericService _genericService = new GenericService();
                var stringContent = new StringContent(JsonConvert.SerializeObject(SendObjData).ToString(), Encoding.UTF8, "application/json");
                var status = await _genericService.PerformDataOperationDataList<VEnquiryModelControleViewRoot>(stringContent);
                return new JsonResult { Data = status };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }
        }


        public async Task<JsonResult> GetUserTrackerDetails(int userId)
        {
            try
            { 
              Trackerdata collection = new Trackerdata();
                collection.UserId = userId;
                CommonMethods objCommonMethods = new CommonMethods();
                GenericOperationModel SendObjData = new GenericOperationModel();
                SendObjData.ScreenID = "121";
                SendObjData.UserID = userId;// Session["UserID"] != null ? Convert.ToInt32(Session["UserID"]) : Convert.ToInt32(ConfigurationManager.AppSettings["DefaultUser"].ToString());
                SendObjData.Operation = "Add";
                string stringTOXml = objCommonMethods.GetXMLFromObject(collection);
                SendObjData.XML = stringTOXml;

                GenericService _genericService = new GenericService();
                var stringContent = new StringContent(JsonConvert.SerializeObject(SendObjData).ToString(), Encoding.UTF8, "application/json");
                var status = await _genericService.PerformDataOperationDataList<ReturnUserTrackerDetails>(stringContent);
               // return new JsonResult { Data = status };
                return new JsonResult { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }
        }

        

        //public async Task<JsonResult> GetTrackerData(int id)
        //{
        //    try
        //    {
        //        int loggedIdUserID = Session["UserID"] != null ? Convert.ToInt32(Session["UserID"]) : Convert.ToInt32(ConfigurationManager.AppSettings["DefaultUser"].ToString());


        //        GenericService _genericService = new GenericService();
        //        IndexScreenParameterModel collection = new IndexScreenParameterModel();
        //        collection.ScreenID = "119";
        //        collection.UserId = loggedIdUserID;
        //        collection.IndexScreenSearchParameterModel = new List<IndexScreenSearchParameterModel>()
        //        {
        //            new IndexScreenSearchParameterModel
        //            {
        //                SearchParameter = "OrderId",
        //                SearchParameterDataType = "int",
        //                SearchParameterValue = id.ToString()
        //            }
        //        };
        //        var stringContent1 = new StringContent(JsonConvert.SerializeObject(collection).ToString(), Encoding.UTF8, "application/json");
        //        var objCommunication = await _genericService.GetRecords<UserTracker>(stringContent1);

        //        return new JsonResult { Data = objCommunication, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
        //    }
        //}

        public async Task<JsonResult> SaveEatingPattern(EatingPatterndata collection)
        {
            try
            {
                CommonMethods objCommonMethods = new CommonMethods();
                GenericOperationModel SendObjData = new GenericOperationModel();
                SendObjData.ScreenID = "122";
                SendObjData.UserID = Session["UserID"] != null ? Convert.ToInt32(Session["UserID"]) : Convert.ToInt32(ConfigurationManager.AppSettings["DefaultUser"].ToString());
                SendObjData.Operation = "Add";
                string stringTOXml = objCommonMethods.GetXMLFromObject(collection);
                SendObjData.XML = stringTOXml;

                GenericService _genericService = new GenericService();
                var stringContent = new StringContent(JsonConvert.SerializeObject(SendObjData).ToString(), Encoding.UTF8, "application/json");
                var status = await _genericService.PerformDataOperationList<EatingPatterndata>(stringContent);
                return new JsonResult { Data = status };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }
        }
        public async Task<JsonResult> GetEatingPattern(int userId,int orderId,int productId,int SessionID)
        {
            try
            {
                int loggedIdUserID = Session["UserID"] != null ? Convert.ToInt32(Session["UserID"]) : Convert.ToInt32(ConfigurationManager.AppSettings["DefaultUser"].ToString());


                GenericService _genericService = new GenericService();
                IndexScreenParameterModel collection = new IndexScreenParameterModel();
                collection.ScreenID = "122";
                collection.UserId = loggedIdUserID;
                var IndexScreenSearchParameterModelList = new List<IndexScreenSearchParameterModel>();
                  var obj1 = new IndexScreenSearchParameterModel
                  {
                      SearchParameter = "UserId",
                      SearchParameterDataType = "int",
                      SearchParameterValue = userId.ToString()
                  };
                IndexScreenSearchParameterModelList.Add(obj1);
                var obj2 = new IndexScreenSearchParameterModel
                {
                    SearchParameter = "orderId",
                    SearchParameterDataType = "int",
                    SearchParameterValue = orderId.ToString()
                };
                IndexScreenSearchParameterModelList.Add(obj2);
                var obj3 = new IndexScreenSearchParameterModel
                {
                    SearchParameter = "productId",
                    SearchParameterDataType = "int",
                    SearchParameterValue = productId.ToString()
                };
                IndexScreenSearchParameterModelList.Add(obj3);

                var obj4 = new IndexScreenSearchParameterModel
                {
                    SearchParameter = "SessionId",
                    SearchParameterDataType = "int",
                    SearchParameterValue = SessionID.ToString()
                };
                IndexScreenSearchParameterModelList.Add(obj4);

                collection.IndexScreenSearchParameterModel = IndexScreenSearchParameterModelList;

                var stringContent1 = new StringContent(JsonConvert.SerializeObject(collection).ToString(), Encoding.UTF8, "application/json");
                var objCommunication = await _genericService.GetRecords<UserEatingPattern>(stringContent1);

                return new JsonResult { Data = objCommunication, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }
        }
    }
}