
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Movid.App.Models;
using Movid.Shared;

namespace Movid.App.Controllers
{

    public class RecentActivity
    {
        public RecentActivity(List<Program> programs, List<User> users )
        {
            
        }
    }

    [Authorize]
    public class HomeController : AppController
    {
        [Route("")]
        public ActionResult Index()
        {
            var programs = RavenSession.Query<Program>().Where(x => x.AccountId == LoggedInUser.AccountId).OrderByDescending(x => x.Created).ToList();
            var users = RavenSession.Query<User>().Where(x => x.AccountId == LoggedInUser.AccountId).ToList();

            return View();
        }

        [Route("newtrial")]
        public ActionResult Thanks()
        {
            return View("Index");
        }
    }
}