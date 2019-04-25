using Movid.App.Application;
using Movid.App.Models;
using System.Web.Mvc;

namespace Movid.App.Controllers
{
    [Authorize]
    public class SettingsController : AppController
    {
        [Route("settings")]
        public ActionResult Index()
        {
            var settings = Account.Settings;

            return View(settings);
        }

        [HttpPost]
        [Route("settings")]
        public ActionResult Index(AccountSettings settings )
        {
            if (!ModelState.IsValid)
            {
                return View(settings);
            }
           
            UpdateModel(Account.Settings);

            LoggedInUser.OnboardingTasks.Add(OnboardingTask.SavedSettings.ToString());
            HighFive("Settings Saved");

            RavenSession.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
