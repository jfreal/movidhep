using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Web.Mvc;

namespace Movid.App.Controllers.Administration
{
    public class ConfigViewerViewModel
    {
        public Dictionary<string, string> AppSettings { get; set; }
        public string RavenConnectionString { get; set; }
    }



    public class ConfigViewerController : BaseAdminController
    {
        [Route("administration/configuration")]
        public ActionResult Index()
        {
            var appSettings = ConfigurationManager.AppSettings;

            var vm = new ConfigViewerViewModel()
                         {
                             AppSettings = ToDictionary(appSettings),
                             RavenConnectionString = ConfigurationManager.ConnectionStrings["RavenDB"].ConnectionString
                         };

            return View(vm);
        }

        public static Dictionary<string, string> ToDictionary(NameValueCollection col)
        {
            var dict = new Dictionary<string, string>();
            
            foreach (var k in col.AllKeys)
            {
                dict.Add(k, col[k]);
            }

            return dict;
        }
    }
}
