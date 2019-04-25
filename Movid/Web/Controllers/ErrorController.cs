using System;
using System.Web.Mvc;

namespace Movid.App.Controllers
{
    public class ErrorController : PublicController
    {
        [Route("error")]
        public ActionResult Error()
        {
            return View();
        }

        [Route("error-test")]
        public ActionResult ThrowException()
        {
            throw new Exception("This is just a test error");
        }
    }
}
