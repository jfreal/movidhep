using Movid.App.Application;
using System.Web.Mvc;

namespace Movid.App.Controllers
{
    [Authorize]
    [MustHaveRole(Roles= "AccountAdmin")]
    public class MyAccountController : AppController
    {
        [HttpGet]
        [Route("my-account")]
        public ActionResult MyAccount()
        {
            return View();
        }

        [Route("my-account/paymentmodaltemplate")]
        public ActionResult PaymentModalTemplate()
        {
            return View();
        }
    }
}
