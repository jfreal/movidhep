using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Marketing.Controllers;
using Movid.Shared.Model;
using Movid.Shared.PagedList;
using Raven.Client;

namespace Movid.Marketing.Controllers
{
    public class PublicLibraryController : BaseController
    {
        [Route("exercises/{page?}")]
        public ActionResult PublicLibrary(int? page)
        {
            ViewBag.SearchTerm = "";
            ViewBag.GridName = "Exercise Library Management";
            ViewBag.GridDescription = "All exercise details are customizable and unique to your clinic.  Change names, categories, or even sort exercises into your " +
                                      "own custom lists.  Our exercise library is flexible and will support whatever works for your business.";

            if (!page.HasValue)
                page = 1;

            var size = 10;

            RavenQueryStatistics stats;
            var exercises = RavenSession
                .Query<Exercise>()
                .Statistics(out stats)
                //.Customize(x => x.WaitForNonStaleResults(TimeSpan.FromSeconds(5)))
                .Where(x => x.Master )
                .OrderBy(x => x.Name)
                .Skip((page.Value - 1) * size)
                .Take(size)
                .ToList();

            var paged = new StaticPagedList<Exercise>(exercises, page.Value, size, stats.TotalResults);

            return View(paged);
        }

        [Route("exercises/search/{page}")]
        public ActionResult PublicLibrarySearch(int? page, string term)
        {
            if (!page.HasValue)
                page = 1;

            ViewBag.SearchTerm = term;

            var size = 10;

            RavenQueryStatistics stats;
            var exercises = RavenSession
                .Query<Exercise>("ByCategoriesAndNameSortByName")
                .Statistics(out stats)
                .Where(x => x.Name.StartsWith(term) && x.Master)
                .Skip((page.Value - 1) * size)
                .Take(size)
                .OfType<Exercise>()
                .ToList();

            var paged = new StaticPagedList<Exercise>(exercises, page.Value, size, stats.TotalResults);

            return View("PublicLibrary",paged);
        }

        [Route("exercises/detail/{id}")]
        public ActionResult SingleExercise(int id)
        {
            var exercise = RavenSession.Load<Exercise>("exercises/" + id);
            
            return View(exercise);
        }


    }
}
