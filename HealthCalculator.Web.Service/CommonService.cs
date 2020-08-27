using HealthCalculator.Web.Common;

using System;

using System.Collections.Generic;
using System.Configuration;
using System.Linq;

using System.Net.Http;

using System.Net.Http.Headers;

using System.Text;

using System.Threading.Tasks;



namespace HealthCalculator.Web.Service

{

    public class CommonService : BaseAuthorizationToken

    {



        public static HttpClient GetHttpClient()

        {

            HttpClientHandler handler = new HttpClientHandler()

            {

                PreAuthenticate = true,

                UseDefaultCredentials = true,

                UseProxy = false,

                ClientCertificateOptions = ClientCertificateOption.Manual

            };



            var client = new HttpClient(handler, false)

            {

                BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseURL"].ToString())

            };

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Add("Authorization", "Token " + AuthorizationToken);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;

        }



    }

}