using System.Web.Http;
using System.Web.Http.Cors;

namespace Pathfinder.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration pConfig)
        {
            pConfig.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            pConfig.MessageHandlers.Add(new LogRequestAndResponseHandler());

            var cors = new EnableCorsAttribute("http://localhost:8888", "*", "*");
            pConfig.EnableCors(cors);
            // Web API configuration and services

            // Web API routes
            pConfig.MapHttpAttributeRoutes();

            pConfig.Routes.MapHttpRoute(
                                        name: "DefaultApi",
                                        routeTemplate: "api/{controller}/{action}/{id}",
                                        defaults: new {id = RouteParameter.Optional}
                                       );
        }
    }
}
