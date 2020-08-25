using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCalculator.Web.Common
{
   public class LoginConstants
    {
        public const string LOGIN_GET_API = "api/login";
        public const string LOGIN_AuthenticateUserCredentials = "api/AuthenticateUserCredentials";

        public const string LOGIN_ChangePassword = "api/ChangePassword";

        public const string LOGIN_CheckIfFirstTimeLogin = "api/CheckIfFirstTimeLogin";

        public const string LOGIN_CheckIfPassExpired = "api/CheckIfPassExpired";

        public const string LOGIN_CheckIfPasswordReused = "api/CheckIfPasswordReused";
           
    }
}
