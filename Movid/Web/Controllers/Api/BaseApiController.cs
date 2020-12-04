using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Movid.App.Models;
using Movid.Shared;
using Raven.Client;
using Raven.Client.Documents.Session;

namespace Movid.App.Controllers.Api
{
    public class BaseApiController : ApiController, IMovidAppContext
    {
        protected override void Initialize(System.Web.Http.Controllers.HttpControllerContext controllerContext)
        {
            RavenSession = MvcApplication.Store.OpenSession();
            base.Initialize(controllerContext);
        }
        private User _user;

        public User LoggedInUser
        {
            get
            {
                if (!User.Identity.IsAuthenticated)
                    return null;

                return _user ?? (_user = RavenSession.Load<User>("users/" + User.Identity.Name));
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

        public Clinic Clinic { get; protected internal set; }
        public bool ApplicationAdministrator
        {
            get
            {
                return LoggedInUser.Email == "john@movidhep.com" ||
                       LoggedInUser.Email == "mike@movidhep.com" ||
                       LoggedInUser.Email == "michaelcarucci57@gmail.com" ||
                       LoggedInUser.Email == "ray@movidhep.com";
            }
        }

        protected internal IDocumentSession RavenSession { get; set; }
    }
}