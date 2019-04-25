using Movid.App.Application;
using Movid.App.Infrastructure.Raven;
using Movid.App.Models;
using Movid.Shared.Model;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Raven.Client;

namespace Movid.App.Controllers
{
    [Authorize]
    [ValidateInput(false)]
    public class LibrarySearchController : AppController
    {
        [Route("library/search")]
        public ActionResult Library()
        {
            IEnumerable<Exercise> exercises;

            RavenQueryStatistics stats;
            if (this.ApplicationAdministrator)
            {
                exercises = RavenSession
                .Query<Exercise>(typeof(ByOwnableAndName).Name)
                .Statistics(out stats)
                .Where(x => x.Master)
                .OrderBy(x => x.Name).Take(15).ToList();
            }
            else
            {
                exercises = RavenSession
                .Query<Exercise>(typeof(ByOwnableAndName).Name)
                .Statistics(out stats)
                .Where(x => x.ClinicId == Clinic.Id || x.AccountId == Account.Id)
                .OrderBy(x => x.Name).Take(15).ToList();
            }

            return Json(exercises, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        [Route("library/protocol/{protocolId}")]
        public ActionResult ProtocolMatch(int protocolId)
        {
            var protocols = RavenSession.Load<Protocol>("protocols/" + protocolId);


            return Json(protocols.Exercises, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        [Route("library/search/category")]
        public ActionResult LibraryCategoryMatch(string term)
        {
            var exercises = ExerciseQueries.SearchExercisesByCategory(Account, Clinic, RavenSession, term, this.ApplicationAdministrator);

            return Json(exercises.Take(20).ToList(), JsonRequestBehavior.AllowGet);
        }


        [ValidateInput(false)]
        [Route("library/search/name")]
        public ActionResult LibraryNameMatch(string term)
        {
            var exercises = ExerciseQueries.SearchExercisesByName(Account, Clinic,RavenSession, term, this.ApplicationAdministrator);

            return Json(exercises.Take(20).ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}