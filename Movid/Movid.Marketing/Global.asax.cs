using Movid.Marketing.Application;
using Raven.Client.Documents;
using System.Web.Mvc;
using System.Web.Routing;

namespace Movid.Marketing
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Store = new DocumentStore { ConnectionStringName = "RavenDB" };
            Store.Initialize();

            AutoMappingConfigurations.Bootstrap();

            //RavenProfiler.InitializeFor(Store);

            //IndexCreation.CreateIndexes(typeof(ExerciseSearch).Assembly, Store);

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            
            //AreaRegistration.RegisterAllAreas();
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    "Default", // Route name
            //    "{controller}/{action}/{id}", // URL with parameters
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            //);

            routes.MapMvcAttributeRoutes();

        }
        public static DocumentStore Store { get; set; }
    }
}