
using CaptchaMvc.HtmlHelpers;

using HealthCalculator.Web.Controllers;

using HealthCalculator.Web.EntityModel;

using HealthCalculator.Web.Service;

using Newtonsoft.Json;

using System;

using System.Configuration;

using System.Net.Http;

using System.Text;

using System.Threading.Tasks;

using System.Web;

using System.Web.Mvc;




namespace HealthCalculator.Web.Controllers

{

    public class UserController : BaseController

    {

        // GET: Login/User

        // GET: Login/User

        public ActionResult Index()

        {



            return View();

        }
        public ActionResult SingUp()
        {



            return View();

        }
        [HttpPost]
        public async Task<JsonResult> AddUserDetails(LoginEntity collection)
        {
            collection.Password = DataEncryption.Encrypt(collection.Password.Trim());
            UserService objLoginServices = new UserService();      
            var stringContent = new StringContent(JsonConvert.SerializeObject(collection).ToString(), Encoding.UTF8, "application/json");
            var isAuth = await objLoginServices.SaveUserDetails(stringContent);
            return new JsonResult { Data = isAuth };

        }

        public ActionResult Logout()

        {

            Session["Access_Token"] = null;

            Session["UserId"] = null;

            Session["UserName"] = null;

            // return RedirectToAction("Index", "User", new { area = "Login" });

            return View();

        }

        [HttpPost]

        public async Task<JsonResult> ChangePassword(LoginEntity collection)

        {

            try

            {

                string errorMessage = string.Empty;

                string password = collection.Password;

                string oldPassword = collection.OldPassword;

                string confirmPassword = "";

                LoginServices objLoginServices = new LoginServices();

                collection.OldPassword = DataEncryption.Encrypt(collection.OldPassword.Trim());

                collection.Password = DataEncryption.Encrypt(collection.Password.Trim());

                var stringContent = new StringContent(JsonConvert.SerializeObject(collection).ToString(), Encoding.UTF8, "application/json");



                var isValidUser = await objLoginServices.AuthenticateUserCredentials(stringContent);

                if (string.IsNullOrEmpty(isValidUser.ErrorMessage))

                {

                    if (isValidUser.data == 0)

                    {

                        errorMessage = "Incorrect UserName/Password";

                    }

                    else if (isValidUser.data == -1)

                    {

                        errorMessage = "Account locked. Please contact admin.";

                    }

                }

                else

                {

                    errorMessage = isValidUser.ErrorMessage;

                }



                string pwdValidationMsg;

                if (string.IsNullOrEmpty(errorMessage))

                {

                    if (password.Trim() == oldPassword.Trim())

                    {

                        errorMessage = "Password should not be same as Old Password.";

                    }

                    else if (!PasswordPolicy.IsValid(password.Trim(), out pwdValidationMsg))

                    {

                        errorMessage = pwdValidationMsg;

                    }

                    else if (password.Trim() != confirmPassword.Trim())

                    {

                        errorMessage = "Confirm Passowrd must match Password.";

                    }

                }

                if (string.IsNullOrEmpty(errorMessage))

                {

                    var stringContent1 = new StringContent(JsonConvert.SerializeObject(collection).ToString(), Encoding.UTF8, "application/json");

                    objLoginServices = new LoginServices();

                    var isAuth = await objLoginServices.CheckIfPasswordReused(stringContent1);

                    if (string.IsNullOrEmpty(isAuth.ErrorMessage))

                    {

                        if (isAuth.data)

                        {

                            errorMessage = "Cannot reuse an Old Password.";

                        }



                    }

                }



                if (string.IsNullOrEmpty(errorMessage))

                {

                    var stringContent2 = new StringContent(JsonConvert.SerializeObject(collection).ToString(), Encoding.UTF8, "application/json");

                    objLoginServices = new LoginServices();

                    var isChangePwd = await objLoginServices.ChangePassword(stringContent2);



                    if (string.IsNullOrEmpty(isChangePwd.ErrorMessage))

                    {

                        if (isChangePwd.data)

                        {

                            errorMessage = "Password is changed,Please login";

                        }



                    }

                }



                return new JsonResult { Data = errorMessage };



            }

            catch (Exception ex)

            {

                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };

            }

        }

        public ActionResult ChangePassword()

        {

            return View();

        }



        [HttpPost]

        public async Task<JsonResult> Token(LoginEntity collection)

        {

            try

            {

                string errorMessage = string.Empty;



                string data = string.Empty;

                if (!this.IsCaptchaValid("Validate"))

                {

                    errorMessage = "Invalid Captcha";

                }

                else

                {

                    LoginServices objLoginServices = new LoginServices();

                    if (collection.LoginType == 1)

                    {

                        //check if LDAP login.

                        var stringContent3 = new StringContent(JsonConvert.SerializeObject(collection).ToString(), Encoding.UTF8, "application/json");

                        objLoginServices = new LoginServices();

                        var status = await objLoginServices.GetToken(stringContent3);

                        if (status.IsSuccessStatusCode)

                        {

                            errorMessage = string.Empty;

                            Session["Access_Token"] = status.data.authToken;

                            Session["UserId"] = status.data.EmployeeID;

                            Session["UserName"] = status.data.EmployeeName;

                        }

                        else

                        {

                            errorMessage = status.ErrorMessage;

                        }

                    }

                    else

                    {



                        collection.Password = DataEncryption.Encrypt(collection.Password);



                        var days = ConfigurationManager.AppSettings["PasswordExpiredDays"].ToString();

                        collection.Days = Convert.ToInt32(days);

                        var stringContent = new StringContent(JsonConvert.SerializeObject(collection).ToString(), Encoding.UTF8, "application/json");

                        var isValidUser = await objLoginServices.AuthenticateUserCredentials(stringContent);

                        if (string.IsNullOrEmpty(isValidUser.ErrorMessage))

                        {

                            if (isValidUser.data == 0)

                            {

                                errorMessage = "Incorrect UserName/Password";

                            }

                            else if (isValidUser.data == -1)

                            {

                                errorMessage = "Account locked. Please contact admin.";

                            }

                        }

                        else

                        {

                            errorMessage = isValidUser.ErrorMessage;

                        }

                        if (string.IsNullOrEmpty(errorMessage))

                        {

                            objLoginServices = new LoginServices();

                            var stringContent1 = new StringContent(JsonConvert.SerializeObject(collection).ToString(), Encoding.UTF8, "application/json");

                            var isAuth = await objLoginServices.CheckIfFirstTimeLogin(stringContent1);

                            if (string.IsNullOrEmpty(isAuth.ErrorMessage))

                            {

                                if (isAuth.data)

                                {

                                    // errorMessage = "First time login,Please change the password";

                                }



                            }

                            else

                            {

                                errorMessage = isAuth.ErrorMessage;

                            }

                        }





                        if (string.IsNullOrEmpty(errorMessage))

                        {

                            var stringContent2 = new StringContent(JsonConvert.SerializeObject(collection).ToString(), Encoding.UTF8, "application/json");

                            objLoginServices = new LoginServices();

                            var isExpired = await objLoginServices.CheckIfPassExpired(stringContent2);



                            if (string.IsNullOrEmpty(isExpired.ErrorMessage))

                            {

                                if (isExpired.data)

                                {

                                    /// errorMessage = "Password is expired, Please change the password";

                                }



                            }

                            else

                            {

                                errorMessage = isExpired.ErrorMessage;

                            }

                        }

                        if (string.IsNullOrEmpty(errorMessage))

                        {

                            var stringContent3 = new StringContent(JsonConvert.SerializeObject(collection).ToString(), Encoding.UTF8, "application/json");

                            objLoginServices = new LoginServices();

                            var status = await objLoginServices.GetToken(stringContent3);

                            if (status.IsSuccessStatusCode)

                            {

                                errorMessage = string.Empty;

                                Session["Access_Token"] = status.data.authToken;

                                Session["UserId"] = status.data.EmployeeID;

                                Session["UserName"] = status.data.EmployeeName;

                            }

                            else

                            {

                                errorMessage = status.ErrorMessage;

                            }

                        }

                    }

                }





                return new JsonResult { Data = errorMessage };



            }

            catch (Exception ex)

            {

                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };

            }

        }



        [HttpPost]

        public async Task<JsonResult> CheckfirstTimeLoginUser(LoginEntity collection)

        {

            LoginServices objLoginServices = new LoginServices();

            objLoginServices = new LoginServices();

            var stringContent = new StringContent(JsonConvert.SerializeObject(collection).ToString(), Encoding.UTF8, "application/json");

            var isAuth = await objLoginServices.CheckIfFirstTimeLogin(stringContent);



            return new JsonResult { Data = isAuth };

        }

       



    }

}