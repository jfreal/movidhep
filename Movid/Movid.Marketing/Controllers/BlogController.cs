using Marketing.Controllers;
using Movid.Shared.Model;
using Movid.Web.Infrastructure;
using Raven.Client.Documents.Session;
using System.Linq;
using System.Web.Mvc;

namespace Movid.Marketing.Controllers
{
    public class BlogController : BaseController
    {
        [Route("blog")]
        public ActionResult Index(int page = 1)
        {
            int pageSize = 2;

            QueryStatistics stats;
            var blogPosts = RavenSession.Query<BlogEntry>()
                                .Statistics(out stats)
                                .OrderBy(c => c.Published)
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

            var model = new PagedList<BlogEntry>(
                            blogPosts,
                            page - 1,
                            pageSize,
                            stats.TotalResults
                        );

            return View(model);

        }

        [Route("blog/{id}/{*seo}")]
        public ActionResult Single(int id)
        {
            var blog = RavenSession.Load<BlogEntry>("BlogEntries\\" + id);
            
            return View(blog);
        }


    }
}
