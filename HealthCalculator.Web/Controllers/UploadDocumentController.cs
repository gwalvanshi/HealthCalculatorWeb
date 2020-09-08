using HealthCalculator.Web.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HealthCalculator.Web.Controllers
{
    public class UploadDocumentController:Controller
    {
        public ActionResult UploadMaster()
        {
            return View();
        }
        public string UploadDocuments(int fileSize, string FileFormat, string graphName, string fName)
        {
            string returrMessage = string.Empty;
            try
            {
                int maxFileSize = Convert.ToInt32(ConfigurationManager.AppSettings["MaxFileSizeinKB"].ToString());
                string AllowedFileFormat = ConfigurationManager.AppSettings["AllowedFileFormat"].ToString();
                string RootFolder = ConfigurationManager.AppSettings["RootFolder"].ToString();
                bool isValid = true;
                if (maxFileSize < fileSize)
                {
                    returrMessage = "Max file size should be " + maxFileSize.ToString();
                    isValid = false;
                }
                else if (isValid == true && AllowedFileFormat != null && !string.IsNullOrEmpty(FileFormat) && AllowedFileFormat.Contains(FileFormat.ToLower()) == false)
                {
                    returrMessage = "Please upload documents in proper format(JPEG,jpg,png).";
                    isValid = false;
                }
                if (isValid)
                {
                   
                        string fileName = System.IO.Path.GetFileName(fName);
                        string newFile = graphName;
                        string[] splString = fileName.Split('.');
                        string uploadNewFileName = newFile + "." + splString[1];
                        string filePathTobeSaved = "";
                        string createDir = string.Empty;
                        string digitalPathofDocument = string.Empty;
                       
                        string baseurl = HttpContext.Server.MapPath(RootFolder);
                       
                        filePathTobeSaved = baseurl + "/" + uploadNewFileName;
                      
                        Upload(filePathTobeSaved);
                      
                        returrMessage = "File Uploaded";
                  
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return returrMessage;
        }
        public void Upload(string filetoBeSave)
        {
            HttpContext.Request.Files[0].SaveAs(filetoBeSave);
        }
        public JsonResult UploadFile(string docTypeID = null, string id = null, string userType = null, string Cat = null, string folderId = null, string SubCat = null)
        {
            string returnMsg = string.Empty;
            try
            {
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
                string fileName = System.IO.Path.GetFileName(file.FileName);           
                string[] splString = fileName.Split('.');                
                returnMsg = UploadDocuments(file.ContentLength, splString[1], docTypeID, file.FileName);
                return new JsonResult { Data = returnMsg, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }

        }

    }
}