using EventSource.Structure.Config.QueryApi;
using Framework.Host.ExceptionFilter;
using Framework.Host.JsonConverters;
using Owin;
using OWIN.Windsor.DependencyResolverScopeMiddleware;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace EventSource.Structure.QueryApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();

            ConfigIoc(appBuilder, config);
            ConfigWebApi(appBuilder, config);
        }

        #region InternalMethods

        private static void ConfigIoc(IAppBuilder appBuilder, HttpConfiguration config)
        {
            Bootstrapper.WireUp();

            config.DependencyResolver = new DependencyResolver(Bootstrapper.Container);
            appBuilder.UseWindsorDependencyResolverScope(config, Bootstrapper.Container);
        }

        private static void ConfigWebApi(IAppBuilder appBuilder, HttpConfiguration config)
        {
            var routeHandler = HttpClientFactory.CreatePipeline(new HttpControllerDispatcher(config), new DelegatingHandler[] { });

            ConfigExceptionFilter(config);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: null,
                handler: routeHandler
            );

            config.UseJsonFormatters();

            appBuilder.UseWebApi(config);
        }

        private static void ConfigExceptionFilter(HttpConfiguration config)
        {
            var exceptionInfos = new List<ExceptionInfo>
            {
            };

            config.Filters.Add(new CustomExceptionHandlerAttribute(exceptionInfos));
        }

        #endregion
    }
}