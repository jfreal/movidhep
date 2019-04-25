using System;
using System.Configuration;
using Movid.App.Controllers;
using System.Web.Mvc;

namespace Movid.App.Application
{
    public class GlobalValuesToViewBag : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            

            var viewResult = filterContext.Result as ViewResult;

            if (viewResult == null)
                return;

            viewResult.ViewBag.TestMode = Convert.ToBoolean(ConfigurationManager.AppSettings["TestMode"]);

            var baseController = ((BaseController) filterContext.Controller);

            viewResult.ViewBag.LoggedInUser = baseController.LoggedInUser;
            var clinic = ((BaseController) filterContext.Controller).Clinic;
            if (clinic != null)
            {
                viewResult.ViewBag.Clinic = baseController.Clinic;    
            }

            viewResult.ViewBag.Account = baseController.Account;
            viewResult.ViewBag.Clinic = baseController.Clinic;
            viewResult.ViewBag.UnreadBlogs = baseController.UnreadAppBlogPostsCount;
            viewResult.ViewBag.ApplicationAdministrator = baseController.ApplicationAdministrator;

            base.OnActionExecuted(filterContext);
        }
    }
}