using System.Web.Http;
using System.Web.Http.Cors;

namespace Pathfinder.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration pConfig)
        {
			var cors = new EnableCorsAttribute("http://localhost:64463", "*", "*");
			pConfig.EnableCors(cors);

            // Web API configuration and services

            // Web API routes
            pConfig.MapHttpAttributeRoutes();

            pConfig.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
