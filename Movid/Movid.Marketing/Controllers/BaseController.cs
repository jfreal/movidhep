using System.Collections.Generic;
using System.Web.Mvc;
using Movid.Marketing;
using Raven.Client;

namespace Marketing.Controllers
{
    public abstract class BaseController : Controller
    {
        public IDocumentSession RavenSession { get; protected set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            RavenSession = MvcApplication.Store.OpenSession();
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.IsChildAction)
                return;
        }


        public void HighFive(string successMessage)
        {
            var highFives = new List<string>();
            if (TempData.ContainsKey("HighFives"))
            {
                highFives = (List<string>)TempData["HighFives"];
            }

            highFives.Add(successMessage);

            TempData["HighFives"] = highFives;
        }
    }
}
