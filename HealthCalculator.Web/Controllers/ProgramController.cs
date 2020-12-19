﻿using HealthCalculator.Web.Common;
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
                string newFile = "UserID_" + userID;
                string[] splString = fileName.Split('.');
                string uploadNewFileName = newFile + "." + splString[1];
                string filePathTobeSaved = "";
                string baseurl = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["ProfilePhotoFolder"]);
                filePathTobeSaved = baseurl + "/" + uploadNewFileName;
                Upload(filePathTobeSaved);
                returrMessage = ConfigurationManager.AppSettings["ProfilePhotoFolder"].ToString().Replace("~", "") + "/" + uploadNewFileName;
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
                SendObjData.UserID = 1;//Session["UserID"] != null ? Convert.ToInt32(Session["UserID"]) : Constants.Default_UserId;
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
                SendObjData.ScreenID ="110";
                SendObjData.UserID = 1;//Session["UserID"] != null ? Convert.ToInt32(Session["UserID"]) : Constants.Default_UserId;
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
    

         
    }
}