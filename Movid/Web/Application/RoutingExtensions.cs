using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Movid.App.Application
{
    public static class RoutingExtensions
    {
        public static string ActionSlash(this HtmlHelper htmlHelper, string actionName, string controllerName)
        {
            string url = UrlHelper.GenerateUrl(
                null,
                actionName /*actionName*/,
                controllerName /*controllerName*/,
                null,
                htmlHelper.RouteCollection,
                htmlHelper.ViewContext.RequestContext,
                true
                );

            return String.Format("{0}/", url);
        }
    }
}