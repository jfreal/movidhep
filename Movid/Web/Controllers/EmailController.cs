using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Movid.App.Application;
using Movid.App.Models;
using Movid.Shared;
using Raven.Client;

namespace Movid.App.Controllers
{
    public class EmailController : AppController
    {
        protected string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        [Route("internal-email/invitation/{id}")]
        public ActionResult NewUserInvitationEmail(string id)
        {
            var vm = UserInvitationBuilder.Build(id, RavenSession);

            if (vm.UserInvitation.Validated)
                return HttpNotFound("User already validated");

            return View(vm);
        }

     
    }
}
