
using Brainnom.Core.ObjectSummary;
using Brainnom.Core.ObjectSummary.KeyProviders;
using Elmah;
using Movid.App.Application;
using Movid.App.Controllers;
using Movid.Web.Infrastructure;
using Movid.Web.Infrastructure.DescriptionProviders;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Movid.App
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {

            filters.Add(new GlobalValuesToViewBag());
            
            #if !DEBUG
                filters.Add(new ElmahHandleErrorAttribute());
            #endif

        }

        void ErrorMail_Filtering(object sender, ExceptionFilterEventArgs e)
        {

            #if DEBUG
                        e.Dismiss();
            #endif

        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapMvcAttributeRoutes();
        }

        public static class WebApiConfig
        {
            public static void Register(HttpConfiguration config)
            {

                config.MapHttpAttributeRoutes();
                config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional }
                );
            }
        }

        protected void Application_Start()
        {

            GlobalConfiguration.Configure(WebApiConfig.Register);
            var formatters = GlobalConfiguration.Configuration.Formatters;
            var jsonFormatter = formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;
            settings.Converters.Add(new IsoDateTimeConverter());
            settings.Formatting = Formatting.Indented;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            
            Store = new DocumentStore { ConnectionStringName = "RavenDB" };
            Store.Initialize();
            RavenDbErrorLog.ConfigureWith(Store);

            AutomappingConfiguration.Bootstrap();

            //RavenProfiler.InitializeFor(Store);

            IndexCreation.CreateIndexes(typeof(HomeController).Assembly, Store);

            ConventionsBootstrapper.Bootstrap();

            ObjectSummarizer.KeyProviders.Add(new RegisteredKeyProvider() { Provider = new DbKeyFieldAsClassPropertyKeyProvider() });
            ObjectSummarizer.DescriptionProviders.Add(new RegisteredDescriptionProvider() { Provider = new NameOrDescriptionProperty() });

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
         
            AreaRegistration.RegisterAllAreas();
        }


        public static DocumentStore Store { get; set; }

    }
}