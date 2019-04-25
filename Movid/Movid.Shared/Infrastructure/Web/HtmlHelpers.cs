using System.Collections.Generic;
using System.Web.Mvc;

namespace Movid.Shared.Infrastructure.Web
{

        //could use a refactor
        public static class MarkdownHelper
        {
            public static List<string> HighFives(this HtmlHelper htmlHelper)
            {
                var tempData = htmlHelper.ViewContext.TempData;

                var highFives = new List<string>();
                if (tempData.ContainsKey("HighFives"))
                {
                    highFives = (List<string>)tempData["HighFives"];
                }

                return highFives;
            }

            public static List<string> Warnings(this HtmlHelper htmlHelper)
            {
                var tempData = htmlHelper.ViewContext.TempData;

                var highFives = new List<string>();
                if (tempData.ContainsKey("Warnings"))
                {
                    highFives = (List<string>)tempData["Warnings"];
                }

                return highFives;
            }


        }
    
}