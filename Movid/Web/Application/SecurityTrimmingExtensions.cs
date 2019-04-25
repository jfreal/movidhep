using Movid.App.Controllers;
using System.Web.Mvc;

namespace Movid.App.Application
{
    public static class SecurityTrimmingExtensions
    {
        public static bool HasActionPermission(this HtmlHelper htmlHelper, string roleName)
        {
            var controller = htmlHelper.ViewContext.Controller as BaseController;

            if (controller == null)
            {
                return false;
            }

            if (controller.LoggedInUser.Roles.Contains(roleName))
            {
                return true;
            }

            if (controller.ApplicationAdministrator)
            {
                return true;
            }

            return false;
        }
    }
}