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
    public class CommonMethodsController : Controller
    {
        public JsonResult UploadReportFiles(string userID)
        {
            try
            {
                string returrMessage = string.Empty;
                HttpFileCollectionBase files = Request.Files;

                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];
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

                    long result = DateTime.Now.Year * 10000000000 + DateTime.Now.Month * 100000000 + DateTime.Now.Day * 1000000 + DateTime.Now.Hour * 10000 + DateTime.Now.Minute * 100 + DateTime.Now.Second + i;
                    string newFile = "UserID_" + userID + "_" + splString[0] + "_" + result;

                    string uploadNewFileName = newFile + "." + splString[1];
                    string filePathTobeSaved = "";
                    // string filePathTobeSaved = "";
                    string baseurl = "";

                    baseurl = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["UploadReportFolder"]);
                    // returrMessage = ConfigurationManager.AppSettings["MessageDocuments"].ToString().Replace("~", "") + "/" + uploadNewFileName;

                    filePathTobeSaved = baseurl + "/" + uploadNewFileName;
                    Upload(filePathTobeSaved);

                    // string baseurl = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["MessageDocuments"]);
                    filePathTobeSaved = baseurl + "/" + uploadNewFileName;
                    Upload(filePathTobeSaved);
                    if (files.Count == i + 1)
                    {
                        returrMessage += ConfigurationManager.AppSettings["UploadReportFolder"].ToString().Replace("~", "") + "/" + uploadNewFileName;
                    }
                    else
                    {
                        returrMessage += ConfigurationManager.AppSettings["UploadReportFolder"].ToString().Replace("~", "") + "/" + uploadNewFileName + ",";
                    }
                }

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
                string[] splString = fileName.Split('.');
               // string newFile = userID + "_" + splString[0];

              //  string uploadNewFileName = newFile + "." + splString[1];

                long result = DateTime.Now.Year * 10000000000 + DateTime.Now.Month * 100000000 + DateTime.Now.Day * 1000000 + DateTime.Now.Hour * 10000 + DateTime.Now.Minute * 100 + DateTime.Now.Second;
                string newFile = "UserID_" + userID + "_" + splString[0] + "_" + result;

                string uploadNewFileName = newFile + "." + splString[1];
                string filePathTobeSaved = "";

               // string filePathTobeSaved = "";
                string baseurl = "";

                if (userID == "1")
                {



                    baseurl = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["UploadReportFolder"]);
                    returrMessage = ConfigurationManager.AppSettings["UploadReportFolder"].ToString().Replace("~", "") + "/" + uploadNewFileName;
                }
                else
                {
                    baseurl = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["UploadReportFolder"]);
                    returrMessage = ConfigurationManager.AppSettings["UploadReportFolder"].ToString().Replace("~", "") + "/" + uploadNewFileName;
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
        private void Upload(string filePathTobeSaved)
        {
            HttpContext.Request.Files[0].SaveAs(filePathTobeSaved);
        }
        public JsonResult UploadFiles(string userID)
        {
            try
            {
                string returrMessage = string.Empty;
                HttpFileCollectionBase files = Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];
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
                  
                    long result = DateTime.Now.Year * 10000000000 + DateTime.Now.Month * 100000000 + DateTime.Now.Day * 1000000 + DateTime.Now.Hour * 10000 + DateTime.Now.Minute * 100 + DateTime.Now.Second+ i;
                    string newFile = "UserID_" + userID + "_" + splString[0]+"_" + result;

                    string uploadNewFileName = newFile + "." + splString[1];
                    string filePathTobeSaved = "";
                   // string filePathTobeSaved = "";
                    string baseurl = "";

                    baseurl = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["MessageDocuments"]);
                   // returrMessage = ConfigurationManager.AppSettings["MessageDocuments"].ToString().Replace("~", "") + "/" + uploadNewFileName;

                    filePathTobeSaved = baseurl + "/" + uploadNewFileName;
                    Upload(filePathTobeSaved);

                   // string baseurl = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["MessageDocuments"]);
                    filePathTobeSaved = baseurl + "/" + uploadNewFileName;
                    Upload(filePathTobeSaved);
                    if (files.Count == i + 1)
                    {
                        returrMessage += ConfigurationManager.AppSettings["MessageDocuments"].ToString().Replace("~", "") + "/" + uploadNewFileName;
                    }
                    else
                    {
                        returrMessage += ConfigurationManager.AppSettings["MessageDocuments"].ToString().Replace("~", "") + "/" + uploadNewFileName + ",";
                    }
                }
            
                return new JsonResult { Data = returrMessage, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }

        }
 

        [HttpGet]
        public async Task<JsonResult> GetDropDownData(int tableType, string FixedSearchParam)
        {
            try
            {
                DropdownRequestModel objDropdownRequestModel = new DropdownRequestModel();
                objDropdownRequestModel.LookType = tableType;
                objDropdownRequestModel.FixedSearchParam = FixedSearchParam;
                DropdownLookupService objDropdownLookupService = new DropdownLookupService();
                var stringContent = new StringContent(JsonConvert.SerializeObject(objDropdownRequestModel).ToString(), Encoding.UTF8, "application/json");
                var data = await objDropdownLookupService.GetDropdown(stringContent);
                return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)

            {

                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };

            }


        }
    }
}