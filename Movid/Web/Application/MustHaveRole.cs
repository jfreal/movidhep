using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Movid.App.Application
{
    public class MustHaveRole : AuthorizeAttribute 
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // Get the authentication cookie
            string cookieName  = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie  = httpContext.Request.Cookies[cookieName];
  
            // If the cookie can't be found, don't issue the ticket
            if(authCookie == null) 
                return false;

            // Get the authentication ticket and rebuild the principal 
            // & identity
            FormsAuthenticationTicket authTicket  = 
            FormsAuthentication.Decrypt(authCookie.Value);
            string[] cookieAbilities = authTicket.UserData.Split(new Char [] {'|'});
            string[] actionAbilities = Roles.Split(new Char[] { '|' });

            return cookieAbilities.Intersect(actionAbilities).Any();
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectResult("~/account/login");
            }
        }

    }
}