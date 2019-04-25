
using System.Collections.Generic;
using Movid.App.Application;
using Movid.App.Infrastructure.Raven;
using Movid.App.Models;
using Movid.Shared;
using Movid.Shared.Model;
using System.Linq;
using System.Web.Mvc;

namespace Movid.App.Controllers
{
    public class AccountAdministrationViewModel
    {
        public Account Account { get; set; }

        public List<User> Users { get; set; }

        public List<Clinic> Clinics { get; set; }

        public List<Exercise> Exercises { get; set; }
    }

    [MustHaveRole(Roles = "Admin")]
    [RoutePrefix("administration")]
    public class AdministrationController : AppController
    {
        [Route("")]
        public ActionResult Index()
        {
            var account = RavenSession.Query<Account>().ToList();
            return View(account);
        }

        [Route("account/{accountId}")]
        public ActionResult SingleAccount(int accountId)
        {
            var account = RavenSession.Load<Account>("accounts/" + accountId);
            var users = RavenSession.Query<User>().Where(x=>x.AccountId == accountId).ToList();
            var clinics = RavenSession.Query<Clinic>().Where(x => x.AccountId == accountId).ToList();
            var exercises = RavenSession.Query<Exercise>().Where(x => x.AccountId == accountId).ToList().OrderBy(x => x.Name).ToList();

            var vm = new AccountAdministrationViewModel()
                         {
                             Account = account,
                             Users = users,
                             Clinics = clinics,
                             Exercises = exercises
                         };

            return View(vm);
        }

        [Route("account/delete/{id}")]
        public ActionResult Delete(int id)
        {
            var user = RavenSession.Load<User>("users/" + id);

            if (user == null)
            {
                WarnUser("User could not be found.");
                return RedirectToAction("Index");
            }

            if (!Ownership.Owns(user, this)) return HttpNotFound();

            var exercises = RavenSession.Query<Exercise>(typeof(ByOwnableAndName).Name).
                Where(x => !x.Master && (x.AccountId == user.AccountId)).Take(1024);

            foreach (var exercise in exercises)
            {
                RavenSession.Delete(exercise);
            }

            var account = RavenSession.Load<Account>("accounts/" + user.AccountId);

            RavenSession.Delete(account);
            RavenSession.Delete(user);

            RavenSession.SaveChanges();

            this.HighFive("User deleted.");

            return RedirectToAction("Index");
        }
    }
}
