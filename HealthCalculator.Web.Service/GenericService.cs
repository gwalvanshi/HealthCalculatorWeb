using HealthCalculator.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HealthCalculator.Web.Service
{
   public class GenericService
    {
        public async Task<HttpCustomResponse<T>> PerformDataOperationWithFalse<T>(HttpContent collection)
        {
            return await HttpUtil.PostAsync<T>(collection, Constants.GENERIC_CRUD_API_URL,false);
        }
        public async Task<HttpCustomResponse<T>> PerformDataOperationWithTrue<T>(HttpContent collection)
        {
            return await HttpUtil.PostAsync<T>(collection, Constants.GENERIC_CRUD_API_URL, true);
        }
        public async Task<HttpCustomResponse<T>> PerformDataOperationList<T>(HttpContent collection)
        {
            return await HttpUtil.PostAsync<T>(collection, Constants.GENERIC_CRUD_API_URL);
        }
        public async Task<HttpCustomResponse<T>> GetRecords<T>(HttpContent collection)
        {
            return await HttpUtil.PostListAsync<T>(collection, Constants.GENERIC_API_GETRECORDS);
        }
        public  HttpCustomResponse<T> GetRecordsResult<T>(HttpContent collection)
        {
            return  HttpUtil.PostListAsyncWithResult<T>(collection, Constants.GENERIC_API_GETRECORDS);
        }
        public async Task<HttpCustomResponse<T>> PerformDataOperationDataList<T>(HttpContent collection)
        {
            return await HttpUtil.PostAsync<T>(collection, Constants.GENERIC_CRUD_DATASETRETURN_API_URL);
        }

        


    }
}
