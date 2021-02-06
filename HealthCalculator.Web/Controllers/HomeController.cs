
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

namespace HealthCalculator.Web.Controllers
{
    public class HomeController : Controller
    {
        private const string Subject = "Eating Smart Health Rating";

        #region Program

        [SessionExpireFilterAttributeAdmin]
        public ActionResult AddFreeSession(int userId)
        {
            return View();
            // return RedirectToAction("Index", "User");
        }

        [SessionExpireFilterAttribute]
        public ActionResult payments()
        {
            return View();
            // return RedirectToAction("Index", "User");
        }
       // [SessionExpireFilterAttribute]
        public ActionResult helpDesk(string userId=null)
        {
            
            List<MessageMasterData> objMessageMasterDataList = new List<MessageMasterData>();
            MessageMasterData objMessageMasterData = new MessageMasterData();
            MessageMasterDataList objMessageMasterDataChild= new MessageMasterDataList();
            List<MessageMasterDataList> MessageMasterDataListChild = new List<EntityModel.MessageMasterDataList>();
            int loggedIdUserID = 0;
            if (string.IsNullOrEmpty(userId))
                loggedIdUserID = Session["UserID"] != null ? Convert.ToInt32(Session["UserID"]) : Convert.ToInt32(ConfigurationManager.AppSettings["DefaultUser"].ToString());
            else
                loggedIdUserID = Convert.ToInt32(userId);
            GenericService _genericService = new GenericService();
            IndexScreenParameterModel collection = new IndexScreenParameterModel();
            collection.ScreenID = "112";
            collection.UserId = loggedIdUserID;
            collection.IndexScreenSearchParameterModel = new List<IndexScreenSearchParameterModel>()
                {
                    new IndexScreenSearchParameterModel
                    {
                        SearchParameter = "UserID",
                        SearchParameterDataType = "int",
                        SearchParameterValue = loggedIdUserID.ToString()
                    }
                };
            var stringContent1 = new StringContent(JsonConvert.SerializeObject(collection).ToString(), Encoding.UTF8, "application/json");
            var objCommunication =  _genericService.GetRecordsResult<MessageMaster>(stringContent1);
            if (objCommunication.ErrorMessage == null)
            {
               
                var distinctData = objCommunication.dataCollection.Select(x => x.MessageId).Distinct().ToList();
                for (int i = 0; i < distinctData.Count; i++)
                {
                   
                    MessageMasterDataListChild = new List<EntityModel.MessageMasterDataList>();
                    var masterDataMain = objCommunication.dataCollection.Where(x => x.MessageId == distinctData[i]).FirstOrDefault();
                    objMessageMasterData = new MessageMasterData();
                    objMessageMasterData.MessageId = masterDataMain.MessageId;
                    objMessageMasterData.Message = masterDataMain.Message;
                    objMessageMasterData.Addedwhen = masterDataMain.Addedwhen;
                    objMessageMasterData.UserName = masterDataMain.UserName;
                    var masterData = objCommunication.dataCollection.Where(x => x.MessageId == distinctData[i]).ToList();

                    foreach (var item in masterData)
                    {
                        objMessageMasterDataChild = new MessageMasterDataList();
                        objMessageMasterDataChild.Attachement =CommonMethods.ServerPath+ item.Attachement;                      
                        MessageMasterDataListChild.Add(objMessageMasterDataChild);

                    }
                    objMessageMasterData.MessageMasterDataList = MessageMasterDataListChild;
                    objMessageMasterDataList.Add(objMessageMasterData);
                }
                
            }
            return View(objMessageMasterDataList);
            // return RedirectToAction("Index", "User");
        }
       
        [SessionExpireFilterAttribute]
        public ActionResult MyEatingPattern(string userId = null)
        {
            return View();
            // return RedirectToAction("Index", "User");
        }
        [SessionExpireFilterAttribute]
        public ActionResult MyEatingSmartTools(string userId = null)
        {
            List<UserOrderViewModel> objModelList = new List<UserOrderViewModel>();
            EatingToolModel objEatingToolModel = new EatingToolModel();
            try
            {

             
                int loggedIdUserID = 0;
                if (string.IsNullOrEmpty(userId))
                    loggedIdUserID = Session["UserID"] != null ? Convert.ToInt32(Session["UserID"]) : Convert.ToInt32(ConfigurationManager.AppSettings["DefaultUser"].ToString());
                else
                    loggedIdUserID = Convert.ToInt32(userId);
                GenericService _genericService = new GenericService();
                IndexScreenParameterModel collection = new IndexScreenParameterModel();
                collection.ScreenID = "116";
                collection.UserId = loggedIdUserID;
                collection.IndexScreenSearchParameterModel = new List<IndexScreenSearchParameterModel>()
                {
                    new IndexScreenSearchParameterModel
                    {
                        SearchParameter = "UserId",
                        SearchParameterDataType = "int",
                        SearchParameterValue = loggedIdUserID.ToString()
                    }
                };
                var stringContent1 = new StringContent(JsonConvert.SerializeObject(collection).ToString(), Encoding.UTF8, "application/json");
                var objCommunication =  _genericService.GetRecordsResult<UserOrderViewModel>(stringContent1);
                objModelList = objCommunication.dataCollection;
                if (string.IsNullOrEmpty(objCommunication.ErrorMessage))
                {
                    bool isValidTool = false;

                    if (objCommunication.dataCollection.Count > 0)
                    {
                        foreach (var item in objCommunication.dataCollection)
                        {
                            if (item.ProgramId != 9 || item.ProgramId != 8 || item.ProgramId != 2)
                            {
                                isValidTool = true;
                            }
                        }
                    }
                   
                    objEatingToolModel.IsValid = isValidTool;
                }

                       

                }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(objEatingToolModel);
            // return RedirectToAction("Index", "User");
        }
      
        [SessionExpireFilterAttribute]
        public ActionResult tracker(string userId = null)
        {
            return View();
            // return RedirectToAction("Index", "User");
        }
        [SessionExpireFilterAttribute]
        public ActionResult assessment(string userId = null)
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
        public ActionResult Logout()
        {
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("login", "Home");          
        }

        [SessionExpireFilterAttribute]
        
        public ActionResult ViewPlan()
        {
            return View();
            // return RedirectToAction("Index", "User");
        }
       // [SessionExpireFilterAttributeAdmin]

        public ActionResult EditPlan(int orderId)
        {
            return View();
            // return RedirectToAction("Index", "User");
        }

        [SessionExpireFilterAttributeAdmin]
        #endregion
        public ActionResult AdminView()
        {
            return View();
            // return RedirectToAction("Index", "User");
        }
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

        public ActionResult PaymentOption()
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
                SendObjData.UserID = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultUser"].ToString());
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
                SendObjData.UserID = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultUser"].ToString());
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
                SendObjData.UserID = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultUser"].ToString());
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
                SendObjData.UserID = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultUser"].ToString());
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
                SendObjData.UserID = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultUser"].ToString());
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
                SendObjData.UserID = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultUser"].ToString());
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
                SendObjData.UserID = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultUser"].ToString());
                collection.Instance_enquiry.UserId =ConfigurationManager.AppSettings["DefaultUser"].ToString();
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
        [HttpGet]
        public async Task<JsonResult> GetUserDetails()
        {
            try
            {
                int loggedIdUserID = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultUser"].ToString());
                GenericService _genericService = new GenericService();
                IndexScreenParameterModel collection = new IndexScreenParameterModel();
                collection.ScreenID = "114";
                collection.UserId = loggedIdUserID;
                collection.IndexScreenSearchParameterModel = new List<IndexScreenSearchParameterModel>()
                {
                    new IndexScreenSearchParameterModel
                    {
                        SearchParameter = "RoleId",
                        SearchParameterDataType = "int",
                        SearchParameterValue = "1"
                    }
                };

                var stringContent1 = new StringContent(JsonConvert.SerializeObject(collection).ToString(), Encoding.UTF8, "application/json");
                var objCommunication = await _genericService.GetRecords<LoginEntity>(stringContent1);

                return new JsonResult { Data = objCommunication, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }
        }
        [HttpGet]
        public async Task<JsonResult> GetUserPlanDetails(int userId)
        {
            try
            {
                int loggedIdUserID = userId;
                GenericService _genericService = new GenericService();
                IndexScreenParameterModel collection = new IndexScreenParameterModel();
                collection.ScreenID = "116";
                collection.UserId = loggedIdUserID;
                collection.IndexScreenSearchParameterModel = new List<IndexScreenSearchParameterModel>()
                {
                    new IndexScreenSearchParameterModel
                    {
                        SearchParameter = "UserId",
                        SearchParameterDataType = "int",
                        SearchParameterValue = loggedIdUserID.ToString()
                    }
                };
                var stringContent1 = new StringContent(JsonConvert.SerializeObject(collection).ToString(), Encoding.UTF8, "application/json");
                var objCommunication = await _genericService.GetRecords<UserOrderViewModel>(stringContent1);

                return new JsonResult { Data = objCommunication, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }
        }
        public async Task<JsonResult> GetUserPlanOrderDetails(int OrderdetailID, int userId)
        {
            try
            {
                int loggedIdUserID = userId;
                GenericService _genericService = new GenericService();
                IndexScreenParameterModel collection = new IndexScreenParameterModel();
                collection.ScreenID = "116";
                collection.UserId = loggedIdUserID;
                var objList = new List<IndexScreenSearchParameterModel>();
                var obj1 = new IndexScreenSearchParameterModel
                {
                    SearchParameter = "UserId",
                    SearchParameterDataType = "int",
                    SearchParameterValue = loggedIdUserID.ToString()
                };
                objList.Add(obj1);
                var obj2 = new IndexScreenSearchParameterModel
                {
                    SearchParameter = "OrderDetail_Id",
                    SearchParameterDataType = "int",
                    SearchParameterValue = OrderdetailID.ToString()
                };
                objList.Add(obj2);
                collection.IndexScreenSearchParameterModel = objList;
                var stringContent1 = new StringContent(JsonConvert.SerializeObject(collection).ToString(), Encoding.UTF8, "application/json");
                var objCommunication = await _genericService.GetRecords<UserOrderViewModel>(stringContent1);

                return new JsonResult { Data = objCommunication, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }
        }

        public async Task<JsonResult> GetEnquiryRatingView()
        {
            try
            {
                int loggedIdUserID = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultUser"].ToString()); 
                GenericService _genericService = new GenericService();
                IndexScreenParameterModel collection = new IndexScreenParameterModel();
                collection.ScreenID = "124";
                collection.UserId = loggedIdUserID;
                var objList = new List<IndexScreenSearchParameterModel>();
                var obj1 = new IndexScreenSearchParameterModel
                {
                    SearchParameter = "CheckedByAdmin",
                    SearchParameterDataType = "bit",
                    SearchParameterValue = "0"
                };
                objList.Add(obj1);
                var stringContent1 = new StringContent(JsonConvert.SerializeObject(collection).ToString(), Encoding.UTF8, "application/json");
                var objCommunication = await _genericService.GetRecords<VEnquiryModelView>(stringContent1);

                return new JsonResult { Data = objCommunication, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }
        }
        [HttpPost]
        public async Task<JsonResult> SaveOrderDetailsForUser(UserOrderViewModel collection)
        {
            try
            {
                if(collection.PaymentDone== "Yes")
                {
                    collection.IsPaymentDone = "Yes";
                }
                else
                {
                    collection.IsPaymentDone = "No";
                }
                if (collection.ActivePlan == "Yes")
                {
                    collection.IsActive = true;
                }
                else
                {
                    collection.IsActive = false;
                }
                CommonMethods objCommonMethods = new CommonMethods();
                GenericOperationModel SendObjData = new GenericOperationModel();
                SendObjData.ScreenID = "116";
                SendObjData.UserID = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultUser"].ToString());
                SendObjData.Operation = "UPDATE";

                string stringTOXml = objCommonMethods.GetXMLFromObject(collection);
                SendObjData.XML = stringTOXml;

                GenericService _genericService = new GenericService();
                var stringContent = new StringContent(JsonConvert.SerializeObject(SendObjData).ToString(), Encoding.UTF8, "application/json");
                var status = await _genericService.PerformDataOperationList<UserOrderViewModel>(stringContent);

                return new JsonResult { Data = status };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }
        }

        [HttpPost]
        public async Task<JsonResult> SaveOrderDetails(UserOrderViewModel collection)
        {
            try
            {
                CommonMethods objCommonMethods = new CommonMethods();
                GenericOperationModel SendObjData = new GenericOperationModel();
                SendObjData.ScreenID = "120";
                SendObjData.UserID = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultUser"].ToString());               
                SendObjData.Operation = "ADD";

                string stringTOXml = objCommonMethods.GetXMLFromObject(collection);
                SendObjData.XML = stringTOXml;

                GenericService _genericService = new GenericService();
                var stringContent = new StringContent(JsonConvert.SerializeObject(SendObjData).ToString(), Encoding.UTF8, "application/json");
                var status = await _genericService.PerformDataOperationList<UserOrderViewModel>(stringContent);
              
                return new JsonResult { Data = status };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }
        }
        [HttpPost]
        public async Task<JsonResult> AdminNotificationMessage(RequestType collection)
        {
            try
            {
                CommonMethods objCommonMethods = new CommonMethods();
                GenericOperationModel SendObjData = new GenericOperationModel();
                SendObjData.ScreenID = "123";
                SendObjData.UserID = Session["UserID"] != null ? Convert.ToInt32(Session["UserID"]) : Convert.ToInt32(ConfigurationManager.AppSettings["DefaultUser"].ToString());
                SendObjData.Operation = "Add";
                string stringTOXml = objCommonMethods.GetXMLFromObject(collection);
                SendObjData.XML = stringTOXml;
                GenericService _genericService = new GenericService();
                var stringContent = new StringContent(JsonConvert.SerializeObject(SendObjData).ToString(), Encoding.UTF8, "application/json");
                var status = await _genericService.PerformDataOperationList<UserRoot>(stringContent);
                return new JsonResult { Data = status };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }
        }
        [HttpPost]
        public async Task<JsonResult> AdminNotificationEnquiryMessage(RequestType collection)
        {
            try
            {               
                CommonMethods objCommonMethods = new CommonMethods();
                GenericOperationModel SendObjData = new GenericOperationModel();
                SendObjData.ScreenID = "123";
                SendObjData.UserID = Session["UserID"] != null ? Convert.ToInt32(Session["UserID"]) : Convert.ToInt32(ConfigurationManager.AppSettings["DefaultUser"].ToString());
                SendObjData.Operation = "Add";
                string stringTOXml = objCommonMethods.GetXMLFromObject(collection);
                SendObjData.XML = stringTOXml;
                GenericService _genericService = new GenericService();
                var stringContent = new StringContent(JsonConvert.SerializeObject(SendObjData).ToString(), Encoding.UTF8, "application/json");
                var status = await _genericService.PerformDataOperationList<UserRoot>(stringContent);
                return new JsonResult { Data = status };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }
        }

        [HttpPost]
        public async Task<JsonResult> GetAdminNotification(RequestType collection)
        {
            try
            {
                CommonMethods objCommonMethods = new CommonMethods();
                GenericOperationModel SendObjData = new GenericOperationModel();
                SendObjData.ScreenID = "124";
                SendObjData.UserID = Session["UserID"] != null ? Convert.ToInt32(Session["UserID"]) : Convert.ToInt32(ConfigurationManager.AppSettings["DefaultUser"].ToString());
                SendObjData.Operation = "Add";
                string stringTOXml = objCommonMethods.GetXMLFromObject(collection);
                SendObjData.XML = stringTOXml;

                GenericService _genericService = new GenericService();
                var stringContent = new StringContent(JsonConvert.SerializeObject(SendObjData).ToString(), Encoding.UTF8, "application/json");
                var status = await _genericService.PerformDataOperationList<NotificationModelRoot>(stringContent);

                return new JsonResult { Data = status };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }
        }

    }



}