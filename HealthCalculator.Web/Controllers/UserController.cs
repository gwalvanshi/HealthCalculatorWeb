
using CaptchaMvc.HtmlHelpers;

using HealthCalculator.Web.Controllers;

using HealthCalculator.Web.EntityModel;

using HealthCalculator.Web.Service;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
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
            //collection.Password = DataEncryption.Encrypt(collection.Password.Trim());
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

                var stringContent3 = new StringContent(JsonConvert.SerializeObject(collection).ToString(), Encoding.UTF8, "application/json");

                LoginServices objLoginServices = new LoginServices();

                var status = await objLoginServices.GetToken(stringContent3);

                if (status.IsSuccessStatusCode)

                {

                    errorMessage = string.Empty;

                    Session["Access_Token"] = status.data.authToken;

                    Session["UserId"] = status.data.userId;
                    var encriptUserId=  DataEncryption.Encrypt(status.data.userId.ToString());
                    Session["UserName"] = status.data.EmployeeName;
                    Session["RoleID"] = status.data.RoleID;
                    status.data.EncriptUserId = encriptUserId;



                }

                return new JsonResult { Data = status,JsonRequestBehavior=JsonRequestBehavior.AllowGet };

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

        [HttpPost]
        public async Task<JsonResult> ForgetPassword(LoginEntity objForgetPassword)
        {
            try
            {
                int loggedIdUserID = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultUser"].ToString());
                GenericService _genericService = new GenericService();
                IndexScreenParameterModel collection = new IndexScreenParameterModel();
                collection.ScreenID = "114";
                collection.UserId = loggedIdUserID;
                collection.IndexScreenSearchParameterModel = new List<IndexScreenSearchParameterModel>()
                {
                    new IndexScreenSearchParameterModel
                    {
                        SearchParameter = "UserName",
                        SearchParameterDataType = "string",
                        SearchParameterValue = objForgetPassword.Email
                    }
                };

                var stringContent1 = new StringContent(JsonConvert.SerializeObject(collection).ToString(), Encoding.UTF8, "application/json");
                var objCommunication = await _genericService.GetRecords<LoginEntity>(stringContent1);
                if (objCommunication != null)
                {
                    if (objCommunication.dataCollection.Count > 0)
                    {
                        string password = objCommunication.dataCollection[0].Password;
                        CommonMethods cm = new CommonMethods();
                        if (!string.IsNullOrEmpty(objCommunication.dataCollection[0].Email))
                        {
                            string NameOfUser = string.Empty;
                            ForgetPassword fc = new ForgetPassword();
                            fc.userMailId = objCommunication.dataCollection[0].Email;
                            fc.Subject = "Your eating health password";
                            if(!string.IsNullOrEmpty(objCommunication.dataCollection[0].FirstName) || !string.IsNullOrEmpty(objCommunication.dataCollection[0].LastName))
                            {
                                NameOfUser = objCommunication.dataCollection[0].FirstName + " " + objCommunication.dataCollection[0].LastName;
                            }
                            else
                            {
                                NameOfUser = "Valued Customer";
                            }
                            fc.Message = forgetPasswordMailBody(objCommunication.dataCollection[0].Password, NameOfUser);

                            cm.SendMailForgetPassword(fc);
                        }
                    }
                }
                return new JsonResult { Data = objCommunication, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
            }
           
        }

        public string forgetPasswordMailBody(string password,string userName)
        {
            string retVal = string.Empty;
            retVal = retVal + "Dear <b>" + userName + "</b>,<br/><br/>";
            retVal = retVal + "Thanks for contacting to us <br/>";
            retVal = retVal + "your current password :<b>" + password + "</b>.Please change password once you receive email.<br/><br/>";
            retVal = retVal + "Thanks & Regards <br/><br/>@2020 Eating Smart. All rights reserved";
            return retVal;

        }




    }





}


