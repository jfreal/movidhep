
using System.Web.Mvc;

namespace Movid.Marketing.Controllers
{
    public class ContentController : Controller
    {
        [Route("pricing")]
        public ActionResult Pricing() { return View(); }

        [Route("about")]
        public ActionResult About() { return View(); }

        [Route("sample-videos")]
        public ActionResult SampleVideos() { return View(); }

        [Route("buy")]
        public ActionResult Buy() { return View(); }

        [Route("feature-videos")]
        public ActionResult FeatureVideos() { return View(); }

        [Route("calculate-your-patient-retention-rate")]
        public ActionResult PatientRetentionCalculator() { return View(); }

        [Route("hall-of-shame")]
        public ActionResult HallofShame()
        {
            return View();
        }

    }
}
