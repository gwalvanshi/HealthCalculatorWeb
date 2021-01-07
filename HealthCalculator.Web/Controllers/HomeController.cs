using DevExpress.Web.Mvc;
using HealthCalculator.Web.Common;
using HealthCalculator.Web.EntityModel;
using HealthCalculator.Web.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        [SessionExpireFilterAttribute]
        public ActionResult payments()
        {
            return View();
            // return RedirectToAction("Index", "User");
        }
       // [SessionExpireFilterAttribute]
        public ActionResult helpDesk()
        {
            List<MessageMasterData> objMessageMasterDataList = new List<MessageMasterData>();
            MessageMasterData objMessageMasterData = new MessageMasterData();
            MessageMasterDataList objMessageMasterDataChild= new MessageMasterDataList();
            List<MessageMasterDataList> MessageMasterDataListChild = new List<EntityModel.MessageMasterDataList>();

            int loggedIdUserID = 16;
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
        public ActionResult MyEatingPattern()
        {
            return View();
            // return RedirectToAction("Index", "User");
        }
        [SessionExpireFilterAttribute]
        public ActionResult MyEatingSmartTools()
        {
            return View();
            // return RedirectToAction("Index", "User");
        }

        [SessionExpireFilterAttribute]
        public ActionResult tracker()
        {
            return View();
            // return RedirectToAction("Index", "User");
        }
        [SessionExpireFilterAttribute]
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
        public ActionResult Logout()
        {
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("login", "Home");          
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


        public ActionResult HtmlEditorPartial()
        {
            return PartialView("_HtmlEditorPartial");
        }
        public ActionResult HtmlEditorPartialImageSelectorUpload()
        {
            HtmlEditorExtension.SaveUploadedImage("HtmlEditor", HomeControllerHtmlEditorSettings.ImageSelectorSettings);
            return null;
        }
        public ActionResult HtmlEditorPartialImageUpload()
        {
            HtmlEditorExtension.SaveUploadedFile("HtmlEditor", HomeControllerHtmlEditorSettings.ImageUploadValidationSettings, HomeControllerHtmlEditorSettings.ImageUploadDirectory);
            return null;
        }

        [ValidateInput(false)]
        public ActionResult FileManagerPartial()
        {
            return PartialView("_FileManagerPartial", HomeControllerFileManagerSettings.Model);
        }

        public FileStreamResult FileManagerPartialDownload()
        {
            return FileManagerExtension.DownloadFiles(HomeControllerFileManagerSettings.DownloadSettings, HomeControllerFileManagerSettings.Model);
        }
    }
    public class HomeControllerFileManagerSettings
    {
        public const string RootFolder = @"~\";

        public static string Model { get { return RootFolder; } }
        public static DevExpress.Web.Mvc.FileManagerSettings DownloadSettings
        {
            get
            {
                var settings = new DevExpress.Web.Mvc.FileManagerSettings { Name = "FileManager" };
                settings.SettingsEditing.AllowDownload = true;
                return settings;
            }
        }
    }

    public class HomeControllerHtmlEditorSettings
    {
        public const string ImageUploadDirectory = "~/Content/UploadImages/";
        public const string ImageSelectorThumbnailDirectory = "~/Content/Thumb/";

        public static DevExpress.Web.UploadControlValidationSettings ImageUploadValidationSettings = new DevExpress.Web.UploadControlValidationSettings()
        {
            AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".jpe", ".gif", ".png" },
            MaxFileSize = 4000000
        };

        static DevExpress.Web.Mvc.MVCxHtmlEditorImageSelectorSettings imageSelectorSettings;
        public static DevExpress.Web.Mvc.MVCxHtmlEditorImageSelectorSettings ImageSelectorSettings
        {
            get
            {
                if (imageSelectorSettings == null)
                {
                    imageSelectorSettings = new DevExpress.Web.Mvc.MVCxHtmlEditorImageSelectorSettings(null);
                    imageSelectorSettings.Enabled = true;
                    imageSelectorSettings.UploadCallbackRouteValues = new { Controller = "Home", Action = "HtmlEditorPartialImageSelectorUpload" };
                    imageSelectorSettings.CommonSettings.RootFolder = ImageUploadDirectory;
                    imageSelectorSettings.CommonSettings.ThumbnailFolder = ImageSelectorThumbnailDirectory;
                    imageSelectorSettings.CommonSettings.AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".jpe", ".gif" };
                    imageSelectorSettings.UploadSettings.Enabled = true;
                }
                return imageSelectorSettings;
            }
        }
    }

}