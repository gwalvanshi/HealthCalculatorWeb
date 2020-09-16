using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HealthCalculator.Web.Controllers
{
    public class BaseController: Controller
    {
    }
    public class SessionExpireFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
          
            if (HttpContext.Current.Session["UserID"] == null)
            {
                int userID = Convert.ToInt32(HttpContext.Current.Session["UserID"]);
                if (userID == 0)
                {
                    filterContext.Result = new RedirectResult("~/Home/Index");
                    return;
                }

            }           
            base.OnActionExecuting(filterContext);
        }
    }

}