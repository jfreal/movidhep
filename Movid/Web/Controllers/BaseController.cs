using Movid.App.Application;
using Movid.App.Models;
using Movid.Shared;
using Movid.Shared.Model;
using Raven.Client;
using Raven.Client.Documents.Session;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;

namespace Movid.App.Controllers
{
    public interface IMovidAppContext
    {
        User LoggedInUser { get; }
        Account Account { get; }
        Clinic Clinic { get; }
        bool ApplicationAdministrator { get; }
    }



    public abstract class BaseController : Controller, IMovidAppContext
    {
        public int UnreadAppBlogPostsCount
        {
            get
            {
                if (Account == null)
                    return 0;

                var lastRead = Account.LastNotificationsRead;

                var unreadBlogs = RavenSession.Query<BlogEntry>().Count(x => x.AppLevel && x.Published > lastRead);

                return unreadBlogs;
            }
        }


        private User _user;
        public User LoggedInUser
        {
            get
            {
                if (!User.Identity.IsAuthenticated)
                    return null;

                _user = _user ?? (_user = RavenSession.Load<User>("users/" + User.Identity.Name));

                return _user;
            }
        }

        public Account Account
        {
            get
            {
                if (LoggedInUser != null)
                {
                    return RavenSession.Load<Account>("accounts/" + LoggedInUser.AccountId);
                }

                return null;
            }
        }

        public Clinic Clinic
        {

            //TODO add session override
            get
            {
                if (LoggedInUser == null)
                    return null;

                if (LoggedInUser.ClinicIds.Any())
                {
                    var clinic = RavenSession.Load<Clinic>("clinics/" + LoggedInUser.ClinicIds[0]);

                    return clinic;
                }

                var onboarder = new UserOnboardProcess(RavenSession);

                var accountId = onboarder.CreateNewAccount();
                var clinicId = onboarder.CreateNewClinic(accountId, "New Clinic");

                LoggedInUser.AccountId = accountId;
                LoggedInUser.ClinicIds.Add(clinicId);
                RavenSession.SaveChanges();

                return RavenSession.Load<Clinic>("clinics/" + clinicId);
            }
        }

        public bool ApplicationAdministrator
        {
            get
            {
                if (LoggedInUser == null)
                {
                    return false;
                }

                return LoggedInUser.Email == "john@movidhep.com" ||
                       LoggedInUser.Email == "mike@movidhep.com" ||
                       LoggedInUser.Email == "michaelcarucci57@gmail.com" ||
                       LoggedInUser.Email == "ray@movidhep.com";
            }
        }


        public IDocumentSession RavenSession { get; protected internal set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            RavenSession = MvcApplication.Store.OpenSession();
            //var migrator = new TemporaryMigrator(RavenSession);

            //migrator.CreateClinicIfUserDoesntHaveOne(LoggedInUser);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.IsChildAction)
                return;
        }

    }

    //public class TemporaryMigrator
    //{
    //    private IDocumentSession _documentSession;
    //    public TemporaryMigrator(IDocumentSession ravenSession)
    //    {
    //        _documentSession = ravenSession;
    //    }

    //    public void CreateClinicIfUserDoesntHaveOne(User user)
    //    {
    //        if (user == null)
    //            return;

    //        if (!user.ClinicIds.Any())
    //        {
    //            var newClinic = new Clinic() { Name = "My Clinic" };
    //            _documentSession.Store(newClinic);
    //            _documentSession.SaveChanges();

    //            user.ClinicIds.Add(newClinic.Id);
    //            _documentSession.SaveChanges();
    //        }


    //    }
    //}
}
