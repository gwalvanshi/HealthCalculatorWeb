using HealthCalculator.Web.Common;

using HealthCalculator.Web.EntityModel;



using System;

using System.Collections.Generic;

using System.Linq;

using System.Net.Http;

using System.Text;

using System.Threading.Tasks;



namespace HealthCalculator.Web.Service

{

    public class LoginServices

    {

        public async Task<HttpCustomResponse<LoginEntity>> GetToken(HttpContent collection)

        {

            return await HttpUtil.PostAsync<LoginEntity>(collection, LoginConstants.LOGIN_GET_API, false);

        }



        public async Task<HttpCustomResponse<int>> AuthenticateUserCredentials(HttpContent collection)

        {

            return await HttpUtil.PostAsync<int>(collection, LoginConstants.LOGIN_AuthenticateUserCredentials, false);

        }

        public async Task<HttpCustomResponse<bool>> ChangePassword(HttpContent collection)

        {

            return await HttpUtil.PostAsync<bool>(collection, LoginConstants.LOGIN_ChangePassword, false);

        }

        public async Task<HttpCustomResponse<bool>> CheckIfFirstTimeLogin(HttpContent collection)

        {

            return await HttpUtil.PostAsync<bool>(collection, LoginConstants.LOGIN_CheckIfFirstTimeLogin, false);

        }

        public async Task<HttpCustomResponse<bool>> CheckIfPassExpired(HttpContent collection)

        {

            return await HttpUtil.PostAsync<bool>(collection, LoginConstants.LOGIN_CheckIfPassExpired, false);

        }

        public async Task<HttpCustomResponse<bool>> CheckIfPasswordReused(HttpContent collection)

        {

            return await HttpUtil.PostAsync<bool>(collection, LoginConstants.LOGIN_CheckIfPasswordReused, false);

        }



        /// <summary>

        ///  Get All

        /// </summary>

        /// <returns></returns>

        public async Task<HttpCustomResponse<LoginEntity>> GetAll()

        {

            return await HttpUtil.GetListAsync<LoginEntity>(LoginConstants.LOGIN_GET_API);

        }



        /// <summary>

        ///  Save Data

        /// </summary>

        /// <param name="collection"></param>

        /// <returns></returns>

        public async Task<HttpCustomResponse<bool>> Save(HttpContent collection)

        {

            return await HttpUtil.PostAsync<bool>(collection, LoginConstants.LOGIN_GET_API, true);



        }



        /// <summary>

        ///  Update Data

        /// </summary>

        /// <param name="content"></param>

        /// <returns></returns>

        public async Task<HttpCustomResponse<bool>> Update(HttpContent content)

        {

            return await HttpUtil.PutAsync<bool>(content, LoginConstants.LOGIN_GET_API, "saved");



        }



        /// <summary>

        /// GetBy Id

        /// </summary>

        /// <param name="id"></param>

        /// <returns></returns>

        public async Task<HttpCustomResponse<LoginEntity>> GetById(int id)

        {

            return await HttpUtil.GetAsync<LoginEntity>(LoginConstants.LOGIN_GET_API + "?id=" + id);





        }

        public async Task<HttpCustomResponse<LoginEntity>> GetDetailsWithPagingSorting(HttpContent collection)

        {

            return await HttpUtil.PostListAsync<LoginEntity>(collection, LoginConstants.LOGIN_GET_API);

        }

        /// <summary>

        /// Delete Data

        /// </summary>

        /// <param name="id"></param>

        /// <returns></returns>

        public async Task<HttpCustomResponse<bool>> Delete(int id)

        {

            return await HttpUtil.DeleteAsync<bool>(LoginConstants.LOGIN_GET_API + id, true);



        }



    }

}