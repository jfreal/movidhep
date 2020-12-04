using Movid.App.Application;
using System.Collections.Generic;

namespace Movid.App.Controllers
{
    [ProductionRequireHttps]
    public abstract class AppController : BaseController
    {

        
        public void HighFive(string successMessage)
        {
            var highFives = new List<string>();
            if (TempData.ContainsKey("HighFives"))
            {
                highFives = (List<string>)TempData["HighFives"];
            }

            highFives.Add(successMessage);

            TempData["HighFives"] = highFives;
        }

        public void WarnUser(string successMessage)
        {
            var highFives = new List<string>();
            if (TempData.ContainsKey("Warnings"))
            {
                highFives = (List<string>)TempData["Warnings"];
            }

            highFives.Add(successMessage);

            TempData["Warnings"] = highFives;
        }

    }
}