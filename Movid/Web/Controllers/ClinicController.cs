using System.Web.Mvc;
using Movid.App.Application;

namespace Movid.App.Controllers
{
    [Authorize]
    [MustHaveRole(Roles = "AccountAdmin")]
    public class ClinicController : AppController
    {
        [Route("clinics/")]
        public ActionResult ClinicsSpa()
        {
            return View();
        }
    }
}