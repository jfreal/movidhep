using System.Web.Mvc;

namespace Movid.App.Controllers
{
    public class TemplateController : BaseController
    {
        [Route("template/{name}")]
        public ActionResult Index(string name)
        {
            return View(name + "Tmpl");
        }

    }
}
