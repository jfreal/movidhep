using Movid.App.Application;
using Movid.App.Infrastructure.Raven;
using Movid.App.Models;
using Movid.Shared.Model;
using System.Linq;
using System.Web.Mvc;

namespace Movid.App.Controllers
{
    public class TypeAheadController : AppController
    {
        [ValidateInput(false)]
        [Route("library/typeahead/protocols")]
        public ActionResult TypeAheadProtocol(string term)
        {
            var protocols = RavenSession.Query<Protocol>(typeof(ProtocolsByOwnershipAndByName).Name)
                .Where(x => x.Name.StartsWith(term) && x.UserId == LoggedInUser.Id)
                .OrderBy(x => x.Name)
                .ToList();

            var datums = from protocol in protocols
                         select new ProtocolDatum()
                         {
                             value = protocol.Name,
                             tokens = new string[1] { protocol.Name },
                             type = "protocol",
                             protocolId = protocol.Id
                         };

            return Json(datums, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        [Route("library/typeahead/exercises")]
        public ActionResult TypeAheadExercise(string term)
        {
            var exercises = ExerciseQueries.SearchExercisesByName(Account, Clinic, RavenSession, term, this.ApplicationAdministrator).ToList();

            var datums = from e in exercises
                         select new TypeAheadDatumViewModel()
                         {
                             value = e.Name,
                             tokens = new string[1] { e.Name },
                             type = "exercise"
                         };

            return Json(datums, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        [Route("library/typeahead/categories")]
        public ActionResult TypeAheadCategory(string term)
        {
            term = term.Trim().ToLower();

            var results = RavenSession.Query<CategoryPaths.CategoryGroup>(typeof(CategoryPaths).Name)
                                      .Where(x => x.Category.StartsWith(term)).ToList();


            var bodyCategories = from r in results
                                 select r.Category;

            var ordered = bodyCategories.Distinct().OrderBy(x => x);

            var datums = from e in ordered
                         select new TypeAheadDatumViewModel()
                         {
                             value = e,
                             tokens = new string[1] { e },
                             type = "category"
                         };

            return Json(datums, JsonRequestBehavior.AllowGet);
        }

    }
}
