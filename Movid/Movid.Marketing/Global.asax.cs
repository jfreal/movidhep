using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;

using Brainnom.Core.ObjectSummary;
using Brainnom.Core.ObjectSummary.KeyProviders;
using Movid.Marketing.Application;
using Movid.Shared.Infrastructure;
using Movid.Shared.Infrastructure.Conventions;

using Movid.Web.Conventions;
using Movid.Web.Conventions.Validation;
using Movid.Web.Infrastructure;
using Movid.Web.Infrastructure.Conventions;
using Movid.Web.Infrastructure.DescriptionProviders;
using Raven.Client.Document;
using Raven.Client.Indexes;

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