using System.Web.Mvc;

namespace Movid.Marketing.Controllers
{
    public class LandingPageController : Controller
    {
        [Route("marketing")]
        public ActionResult Marketing() { return View(); }

        [Route("home-exercise-programs")]
        public ActionResult HEP() { return View(); }

        [Route("physical-therapy-exercise-handouts")]
        public ActionResult Handouts()
        {
            return View();
        }
    }
}
