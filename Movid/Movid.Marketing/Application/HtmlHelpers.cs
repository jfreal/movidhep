using System.Configuration;
using System.Web.Mvc;

namespace Movid.Marketing.Application
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString RegisterLink(this HtmlHelper htmlHelper)
        {
            var siteUrl = ConfigurationManager.AppSettings["AppUrl"];

            return new MvcHtmlString(siteUrl);
        }

        public static MvcHtmlString LoginLink(this HtmlHelper htmlHelper)
        {
            var siteUrl = ConfigurationManager.AppSettings["LoginUrl"];

            return new MvcHtmlString(siteUrl);
        }
    }
}