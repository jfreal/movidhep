using System.Linq;
using System.Web.Mvc;

using Marketing.Controllers;
using Movid.Shared.Model;

namespace Movid.Marketing.Controllers
{
    public class HomeController : BaseController
    {
        [Route("")]
        public ActionResult Index()
        {
                        var blogPosts = RavenSession.Query<BlogEntry>()
                               .OrderBy(c => c.Published)
                               .Take(5)
                               .ToList();

            return View("index2",blogPosts);

        }

        [Route("index2")]
        public ActionResult Index2()
        {
            var blogPosts = RavenSession.Query<BlogEntry>()
                   .OrderBy(c => c.Published)
                   .Take(5)
                   .ToList();

            return View(blogPosts);

        }

        public ActionResult About()
        {
            return View();
        }
    }
}
