using HealthCalculator.Web.EntityModel;
using HealthCalculator.Web.Service;
using Newtonsoft.Json;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HealthCalculator.Web.Controllers
{
    public class OrderModel
    {
        public string orderId { get; set; }
        public string razorpayKey { get; set; }
        public int amount { get; set; }
        public string currency { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string contactNumber { get; set; }
        public string address { get; set; }
        public string description { get; set; }
    }
    public class PaymentInitiateModel
    {
        public string name { get; set; }
        public string email { get; set; }
        public string contactNumber { get; set; }
        public string address { get; set; }
        public int amount { get; set; }
        public int OrderId { get; set; }
        public int UserId { get; set; }
        

    }
    public class PaymentController : BaseController
    {
        public ActionResult PaymentView(string Id = null, string OrderId = null, string UserId = null, string Type=null)
        {
            Session["PaypalData"] = Id + "-" +OrderId + "-" + UserId;
            if (Type == "P")
            {
                return RedirectToAction("PaymentWithPaypal", "Payment");
            }
            else
            {
                return RedirectToAction("Index", "Payment");
            }
        }
        public ActionResult FailureView()
        {
            return View();
        }
        public ActionResult SuccessView()
        {
            return View();
        }

        public ActionResult PaymentWithPaypal(string Cancel = null)
        {
            string guid = string.Empty;
            if (Session["PaypalData"] != null)
            {
                guid = Convert.ToString(Session["PaypalData"]);
            }
            //getting the apiContext  
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            OrderViewModel objGraphType = new OrderViewModel();
            CommonMethods objCommonMethods = new CommonMethods();
            GenericOperationModel SendObjData = new GenericOperationModel();
            GenericService _genericService = new GenericService();
          //  var guid = orderData;
            try
            {

                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Payment/PaymentWithPayPal?";
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                  
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid, guid.Split('-')[0].ToString());
                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid  
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  
                    guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        objGraphType.OrderId = Convert.ToInt32(guid.Split('-')[1].ToString());
                        objGraphType.UserId = Convert.ToInt32(guid.Split('-')[2].ToString());
                        objGraphType.IsPaymentDone = false;
                        objGraphType.PaymentorderId = guid;
                        objGraphType.PaymentType = "PayPal";

                        SendObjData.ScreenID = "116";
                        SendObjData.UserID = objGraphType.UserId;
                        SendObjData.Operation = "ADD";

                        string stringTOXml1 = objCommonMethods.GetXMLFromObject(objGraphType);
                        SendObjData.XML = stringTOXml1;


                        var stringContent1 = new StringContent(JsonConvert.SerializeObject(SendObjData).ToString(), Encoding.UTF8, "application/json");
                        var status1 = _genericService.GetRecordsResult<OrderViewModel>(stringContent1);
                        return View("FailureView");
                    }
                }
            }
            catch (Exception ex)
            {
                objGraphType.OrderId = Convert.ToInt32(guid.Split('-')[1].ToString());
                objGraphType.UserId = Convert.ToInt32(guid.Split('-')[2].ToString());
                objGraphType.IsPaymentDone = true;
                objGraphType.PaymentorderId = guid;
                objGraphType.PaymentType = "PayPal";

                SendObjData.ScreenID = "116";
                SendObjData.UserID = objGraphType.UserId;
                SendObjData.Operation = "ADD";

                string stringTOXml2 = objCommonMethods.GetXMLFromObject(objGraphType);
                SendObjData.XML = stringTOXml2;


                var stringContent2 = new StringContent(JsonConvert.SerializeObject(SendObjData).ToString(), Encoding.UTF8, "application/json");
                var status2 = _genericService.GetRecordsResult<OrderViewModel>(stringContent2);
                return View("FailureView");
            }
            objGraphType.OrderId = Convert.ToInt32(guid.Split('-')[1].ToString());
            objGraphType.UserId = Convert.ToInt32(guid.Split('-')[2].ToString());
            objGraphType.IsPaymentDone = true;
            objGraphType.PaymentorderId = guid;
            objGraphType.PaymentType = "PayPal";

            SendObjData.ScreenID = "116";
            SendObjData.UserID = objGraphType.UserId;
            SendObjData.Operation = "ADD";

            string stringTOXml = objCommonMethods.GetXMLFromObject(objGraphType);
            SendObjData.XML = stringTOXml;


            var stringContent = new StringContent(JsonConvert.SerializeObject(SendObjData).ToString(), Encoding.UTF8, "application/json");
            var status = _genericService.GetRecordsResult<OrderViewModel>(stringContent);
            //on successful payment, show success page to user.  
            return View("SuccessView");
        }
        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        private Payment CreatePayment(APIContext apiContext, string redirectUrl, string Id)
        {
            //create itemlist and add item objects to it  
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };
            //Adding Item Details like name, currency, price etc  
            itemList.items.Add(new Item()
            {
                name = "ServiceCharge ",
                currency = "USD",
                price = Id.ToString(),
                quantity = "1",
                sku = "1"
            });
            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };
            // Adding Tax, shipping and Subtotal details  
            var details = new Details()
            {
                tax = "0",
                shipping = "0",
                subtotal = Id.ToString()
            };
            //Final amount with details  
            var amount = new Amount()
            {
                currency = "USD",
                total = Id.ToString(), // Total must be equal to sum of tax, shipping and subtotal.  
                details = details
            };
            var transactionList = new List<Transaction>();
            // Adding description about the transaction  
            transactionList.Add(new Transaction()
            {
                description = "Service Charges",
                invoice_number = Guid.NewGuid().ToString(), //Generate an Invoice No  
                amount = amount,
                item_list = itemList
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext  
            return this.payment.Create(apiContext);
        }
        public ActionResult Index()
        {
            string guid = string.Empty;
            if (Session["PaypalData"] != null)
            {
                guid = Convert.ToString(Session["PaypalData"]);
            }
            PaymentInitiateModel _requestData = new PaymentInitiateModel();
            _requestData.amount = Convert.ToInt32(guid.Split('-')[0].ToString());
            _requestData.OrderId = Convert.ToInt32(guid.Split('-')[1].ToString());
            _requestData.UserId= Convert.ToInt32(guid.Split('-')[2].ToString());

            //int loggedIdUserID = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultUser"].ToString());
            GenericService _genericService = new GenericService();
            IndexScreenParameterModel collection = new IndexScreenParameterModel();
            collection.ScreenID = "114";
            collection.UserId = _requestData.UserId;
            collection.IndexScreenSearchParameterModel = new List<IndexScreenSearchParameterModel>()
                {
                    new IndexScreenSearchParameterModel
                    {
                        SearchParameter = "UserId",
                        SearchParameterDataType = "int",
                        SearchParameterValue =_requestData.UserId.ToString()
                    }
                };

            var stringContent1 = new StringContent(JsonConvert.SerializeObject(collection).ToString(), Encoding.UTF8, "application/json");
            var objCommunication =  _genericService.GetRecordsResult<LoginEntity>(stringContent1);
            _requestData.contactNumber = objCommunication.dataCollection[0].MobileNo;
            _requestData.email = objCommunication.dataCollection[0].Email;
            _requestData.address = "NA";
            _requestData.name = objCommunication.dataCollection[0].FirstName + " " + objCommunication.dataCollection[0].LastName;
            return View(_requestData);
        }
        public ActionResult PaymentPage()
        {
           
            return View();
        }
        //[HttpPost]
        //public JsonResult RazorOrder(PaymentInitiateModel _requestData)
        //{
        //    try
        //    {
        //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //        // Generate random receipt number for order
        //        Random randomObj = new Random();
        //        string transactionId = randomObj.Next(10000000, 100000000).ToString();
        //        Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_test_dcqe3aqpMbfHdP", "f20iAuCr0Rz9ZE2dSvccDesd");
        //        Dictionary<string, object> options = new Dictionary<string, object>();
        //        options.Add("amount", _requestData.amount * 100);  // Amount will in paise
        //        options.Add("receipt", transactionId);
        //        options.Add("currency", "INR");
        //        options.Add("payment_capture", "0"); // 1 - automatic  , 0 - manual
        //                                             //options.Add("notes", "-- You can put any notes here --");
        //        Razorpay.Api.Order orderResponse = client.Order.Create(options);
        //        string orderId = orderResponse["id"].ToString();
        //        // Create order model for return on view
        //        OrderModel orderModel = new OrderModel
        //        {
        //            orderId = orderResponse.Attributes["id"],
        //            razorpayKey = "rzp_test_dcqe3aqpMbfHdP",
        //            amount = _requestData.amount * 100,
        //            currency = "INR",
        //            name = _requestData.name,
        //            email = _requestData.email,
        //            contactNumber = _requestData.contactNumber,
        //            address = _requestData.address,
        //            description = "Testing description"
        //        };
        //        return new JsonResult { Data = orderModel };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult { Data = new HttpCustomResponse<bool>(ex) };
        //    }
        //}


        [HttpPost]
        public ActionResult CreateOrder(PaymentInitiateModel _requestData)
        {
            OrderViewModel objGraphType = new OrderViewModel();
            CommonMethods objCommonMethods = new CommonMethods();
            GenericOperationModel SendObjData = new GenericOperationModel();
            GenericService _genericService = new GenericService();

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            // Generate random receipt number for order
            Random randomObj = new Random();
            string transactionId = randomObj.Next(10000000, 100000000).ToString();
            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_test_dcqe3aqpMbfHdP", "f20iAuCr0Rz9ZE2dSvccDesd");
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", _requestData.amount * 100);  // Amount will in paise
            options.Add("receipt", transactionId);           
            options.Add("currency", "INR");
            options.Add("payment_capture", "0"); // 1 - automatic  , 0 - manual
                                                 //options.Add("notes", "-- You can put any notes here --");
            Razorpay.Api.Order orderResponse = client.Order.Create(options);
            string orderId = orderResponse["id"].ToString();
            // Create order model for return on view
            OrderModel orderModel = new OrderModel
            {
                orderId = orderResponse.Attributes["id"],
                razorpayKey = "rzp_test_dcqe3aqpMbfHdP",
                amount = _requestData.amount * 100,
                currency = "INR",
                name = _requestData.name,
                email = _requestData.email,
                contactNumber = _requestData.contactNumber,
                address = _requestData.address,
                description = "Testing description"
            };
            objGraphType.OrderId = Convert.ToInt32(_requestData.OrderId);
            objGraphType.UserId = Convert.ToInt32(_requestData.UserId);
            objGraphType.IsPaymentDone = false;
            objGraphType.PaymentorderId = orderId;
            objGraphType.PaymentType = "RazorPayTemp";

            SendObjData.ScreenID = "116";
            SendObjData.UserID = objGraphType.UserId;
            SendObjData.Operation = "ADD";

            string stringTOXml = objCommonMethods.GetXMLFromObject(objGraphType);
            SendObjData.XML = stringTOXml;


            var stringContent = new StringContent(JsonConvert.SerializeObject(SendObjData).ToString(), Encoding.UTF8, "application/json");
            var status = _genericService.GetRecordsResult<OrderViewModel>(stringContent);
            // Return on PaymentPage with Order data
            return View("PaymentPage", orderModel);
        }

        [HttpPost]
        public ActionResult Complete()
        {
            // Payment data comes in url so we have to get it from url
            // This id is razorpay unique payment id which can be use to get the payment details from razorpay server
            string paymentId = Request.Params["rzp_paymentid"];
            // This is orderId
            string orderId = Request.Params["rzp_orderid"];
          
            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_test_dcqe3aqpMbfHdP", "f20iAuCr0Rz9ZE2dSvccDesd");
            Razorpay.Api.Payment payment = client.Payment.Fetch(paymentId);
            // This code is for capture the payment 
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", payment.Attributes["amount"]);
           
            Razorpay.Api.Payment paymentCaptured = payment.Capture(options);
            string amt = paymentCaptured.Attributes["amount"];
               
            OrderViewModel objGraphType = new OrderViewModel();
            CommonMethods objCommonMethods = new CommonMethods();
            GenericOperationModel SendObjData = new GenericOperationModel();
            GenericService _genericService = new GenericService();
            //// Check payment made successfully
          
            if (paymentCaptured.Attributes["status"] == "captured")
            {
               
                objGraphType.OrderId = Convert.ToInt32(0);
                objGraphType.UserId = Convert.ToInt32(0);
                objGraphType.IsPaymentDone = true;
                objGraphType.PaymentorderId = orderId;
                objGraphType.PaymentType = "RazorPay";
               
                SendObjData.ScreenID = "116";
                SendObjData.UserID = objGraphType.UserId;
                SendObjData.Operation = "ADD";

                string stringTOXml = objCommonMethods.GetXMLFromObject(objGraphType);
                SendObjData.XML = stringTOXml;

              
                var stringContent = new StringContent(JsonConvert.SerializeObject(SendObjData).ToString(), Encoding.UTF8, "application/json");
                var status =  _genericService.GetRecordsResult<OrderViewModel>(stringContent);
                //return new JsonResult { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

                // Create these action method
                ///return RedirectToAction("Success");
                return RedirectToAction("Success", "Payment", new { Trans = orderId });
            }
            else
            {
                objGraphType.OrderId = Convert.ToInt32(0);
                objGraphType.UserId = Convert.ToInt32(0);
                objGraphType.IsPaymentDone = false;
                objGraphType.PaymentorderId = orderId;
                objGraphType.PaymentType = "RazorPay";

                SendObjData.ScreenID = "116";
                SendObjData.UserID = objGraphType.UserId;
                SendObjData.Operation = "ADD";

                string stringTOXml = objCommonMethods.GetXMLFromObject(objGraphType);
                SendObjData.XML = stringTOXml;


                var stringContent = new StringContent(JsonConvert.SerializeObject(SendObjData).ToString(), Encoding.UTF8, "application/json");
                var status = _genericService.GetRecordsResult<OrderViewModel>(stringContent);
                return RedirectToAction("Failed", "Payment", new { Trans = orderId });
            }
        }
        public ActionResult Success(string Trans= null)
        {
            return View();
        }
        public ActionResult Failed(string Trans=null)
        {
            return View();
        }
    }
}