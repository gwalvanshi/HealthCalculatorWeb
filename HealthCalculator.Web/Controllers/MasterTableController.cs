using HealthCalculator.Web.Common;
using HealthCalculator.Web.EntityModel;
using HealthCalculator.Web.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        public async Task<JsonResult> Save(SysMasterTableModel collection1)
        {
            try
            {
                CommonMethods objCommonMethods = new CommonMethods();
                GenericOperationModel collection = new GenericOperationModel();
                collection.ScreenID = Constants.Enquiry_ScreenID;
                collection.UserID = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultUser"].ToString());
                collection.Operation = "ADD";
                collection.XML = objCommonMethods.GetXMLFromObject(collection1);
                 GenericService _genericService = new GenericService();
                var stringContent = new StringContent(JsonConvert.SerializeObject(collection).ToString(), Encoding.UTF8, "application/json");
                var status = await _genericService.PerformDataOperationList<SysMasterTableModel>(stringContent);

                return new JsonResult { Data = status };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }
        }
        [HttpGet]
        public async Task<JsonResult> Get(int id, string ScreenID)
        {
            try
            {
                int loggedIdUserID = Convert.ToInt32(Session["UserID"]);
                GenericService _genericService = new GenericService();
                IndexScreenParameterModel collection = new IndexScreenParameterModel();              
                collection.ScreenID = ScreenID;
                collection.UserId = loggedIdUserID;
                collection.IndexScreenSearchParameterModel = new List<IndexScreenSearchParameterModel>()
                {
                    new IndexScreenSearchParameterModel
                    {
                        SearchParameter = "CommTemplateMaster_Id",
                        SearchParameterDataType = "int",
                        SearchParameterValue = id.ToString()
                    }
                };
                var stringContent1 = new StringContent(JsonConvert.SerializeObject(collection).ToString(), Encoding.UTF8, "application/json");
                var objCommunication = await _genericService.GetRecords<SysMasterTableModel>(stringContent1);
                return new JsonResult { Data = objCommunication, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }
        }





        public List<IndexScreenSearchParameterModel> GetSearchList(int loggedIdUserID)
        {
            List<IndexScreenSearchParameterModel> objSearchList = new List<IndexScreenSearchParameterModel>();
            IndexScreenSearchParameterModel objCategory = new IndexScreenSearchParameterModel
            {
                SearchParameter = "UserId",
                SearchParameterDataType = "int",
                SearchParameterValue = loggedIdUserID.ToString()
            };
            objSearchList.Add(objCategory);
            IndexScreenSearchParameterModel objIsDelete = new IndexScreenSearchParameterModel
            {
                SearchParameter = "IsDeleted",
                SearchParameterDataType = "int",
                SearchParameterValue = "0"
            };
            objSearchList.Add(objIsDelete);

            IndexScreenSearchParameterModel objIsActive = new IndexScreenSearchParameterModel
            {
                SearchParameter = "IsActive",
                SearchParameterDataType = "int",
                SearchParameterValue = "1"
            };
            objSearchList.Add(objIsActive);

            return objSearchList;
        }
        public async Task<JsonResult> GetAssessment()
        {
            int loggedIdUserID = Convert.ToInt32(Session["UserID"]);
            MasterTableService objLoginServices = new MasterTableService();
            IndexScreenParameterModel collection = new IndexScreenParameterModel
            {
                IndexScreenSearchParameterModel = GetSearchList(loggedIdUserID),
                ScreenID = "x_OnlineAssessmentMaster" ,
                UserId = Convert.ToInt32(Session["UserID"])

            };
            var stringContent = new StringContent(JsonConvert.SerializeObject(collection).ToString(), Encoding.UTF8, "application/json");
            var status = await objLoginServices.ManageSysMasterTable(stringContent);
            return new JsonResult { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public async Task<JsonResult> ManageSysMasterTable(SysMasterTableModel collection)
        {
            try
            {
                MasterTableService objLoginServices = new MasterTableService();
                collection.Operation = "ADD";
            var stringContent = new StringContent(JsonConvert.SerializeObject(collection).ToString(), Encoding.UTF8, "application/json");
            var status = await objLoginServices.ManageSysMasterTable(stringContent);
            return new JsonResult { Data = status, JsonRequestBehavior=JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)

            {

                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };

            }

        }
        [HttpPost]
        public async Task<JsonResult> LoadMasterTableGrid(int ? tableID)
        {
          
            try
            {
                GridViewRequestModel collection = new GridViewRequestModel();
                GridBindService objGridBindService = new GridBindService();
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                // Skiping number of Rows count
                var start = Request.Form["start"];
                // Paging Length 10,20
                var length = Request.Form["length"];
                // Sort Column Name
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"];
                // Sort Column Direction ( asc ,desc)
                var sortColumnDirection = Request.Form["order[0][dir]"];
                // Search Value from (Search box)
                var searchValue = Request.Form["search[value]"];

                //Paging Size (10,20,50,100)
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                collection.CommonSearch = searchValue;
                collection.FixedSearch = tableID==null?"": tableID.ToString();
                collection.PageSize = pageSize;
                collection.Skip = skip;
                collection.SortColumn = sortColumn;
                collection.SortColumnDirection = sortColumnDirection;
                collection.LookType = 2;
                var stringContent = new StringContent(JsonConvert.SerializeObject(collection).ToString(), Encoding.UTF8, "application/json");
                var status = await objGridBindService.GetGridData(stringContent);

                CommonMethods objCommonMethods = new CommonMethods();
                List<vSysMasttablewithfilter> objList = new List<vSysMasttablewithfilter>();
               
                if(status.data!=null)
                {
                    for (int i = 0; i <status.data.GridData.Rows.Count; i++)
                    {
                        vSysMasttablewithfilter objvSysMasttablewithfilter = new vSysMasttablewithfilter();
                        objvSysMasttablewithfilter.displayName = status.data.GridData.Rows[i]["displayName"].ToString();
                        objvSysMasttablewithfilter.ID =Convert.ToInt32(status.data.GridData.Rows[i]["ID"]);
                        objvSysMasttablewithfilter.SysMastertableName= Convert.ToString(status.data.GridData.Rows[i]["SysMastertableName"]);
                        objvSysMasttablewithfilter.Remark = Convert.ToString(status.data.GridData.Rows[i]["Remark"]);
                        objList.Add(objvSysMasttablewithfilter);
                    }
                   
                }

               // objList = CommonMethods.ConvertDataTable<vSysMasttableGrid>(status.data.GridData);
               //Returning Json Data
                return Json(new { draw = draw, recordsFiltered = status.data.RowCount, recordsTotal = status.data.RowCount, data = objList });

            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }

        }

        [HttpGet]
        public async Task<JsonResult> GetSystemMasterTableData(int ?tableType)
        {
            try
            {
                MasterTableService objLoginServices = new MasterTableService();
                var status = await objLoginServices.GetSystemMasterTableData(tableType);
                return new JsonResult { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)

            {

                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };

            }
          
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