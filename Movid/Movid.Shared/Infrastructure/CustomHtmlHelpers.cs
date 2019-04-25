using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace Movid.Web.Utility
{
    public static class CustomHtmlHelpers
    {
        //I think I wrote this code tired and dumb, so bad

        public static IHtmlString Active(this HtmlHelper htmlHelper, string linkText, string actionName, string controlName,string icon)
        {
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            String url = urlHelper.Action(actionName, controlName);

            var active = htmlHelper.ViewContext.RouteData.Values["action"].ToString() == actionName &&
                         htmlHelper.ViewContext.RouteData.Values["controller"].ToString() == controlName;


            TagBuilder tbLi = new TagBuilder("li");
            if(active)
                tbLi.AddCssClass("active");

            TagBuilder tba = new TagBuilder("a");
            if (active)
                tbLi.AddCssClass("a");

            var tbI = new TagBuilder("i");
            tbI.AddCssClass(icon);

            tba.InnerHtml = tbI.ToString() + " " + linkText;
            tba.Attributes["href"] = url;

            tbI.InnerHtml = tba.ToString();
            tbLi.InnerHtml = tba.ToString();

            return htmlHelper.Raw(tbLi.ToString());

        }

        public static string ToRouteFriendlyString(this string original)
        {
            return original.Replace(" ", "-").ToLower();
        }
    }
}
