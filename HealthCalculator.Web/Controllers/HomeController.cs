﻿using HealthCalculator.Web.Common;
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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public async Task<JsonResult> GetWFLBoy()
        {
            try
            {
                int loggedIdUserID = 1;
                GenericService _genericService = new GenericService();
                IndexScreenParameterModel collection = new IndexScreenParameterModel();
                collection.ScreenID = "103";
                collection.UserId = loggedIdUserID;               
                var stringContent1 = new StringContent(JsonConvert.SerializeObject(collection).ToString(), Encoding.UTF8, "application/json");
                var objCommunication = await _genericService.GetRecords<WFLBoy>(stringContent1);
                return new JsonResult { Data = objCommunication, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
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
                var status = await _genericService.PerformDataOperationList<Root>(stringContent);

                CommonMethods cm = new CommonMethods();
                if (!string.IsNullOrEmpty(collection.Instance_enquiry.Email_ID))
                {
                    cm.SendEmail(collection.Instance_enquiry.Email_ID, "Welcome to Eating india", "", status.data.Data, collection);
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