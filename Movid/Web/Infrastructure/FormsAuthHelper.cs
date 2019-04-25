using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Movid.App.Models;
using Movid.Shared;

namespace Movid.App.Infrastructure
{
    public class FormsAuthHelper
    {
        public static void SetAuthenticationCookie(HttpResponseBase response, User user)
        {


            FormsAuthenticationTicket authTicket =
                new FormsAuthenticationTicket(1,
                                              user.Id.ToString(),
                                              DateTime.Now,
                                              DateTime.MaxValue,
                                              false,
                                              string.Join("|", user.Roles));

            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            response.Cookies.Add(faCookie);
        }

      
    }
}