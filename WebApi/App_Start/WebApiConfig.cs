using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Formatters.XmlFormatter.AddQueryStringMapping("$format", "xml", "application/xml");
            config.Formatters.JsonFormatter.AddQueryStringMapping("$format", "json", "application/json");
            

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                //defaults: new { controller = "Make", action = "ReadAll", id = RouteParameter.Optional }
            );

      
        }
    }
}
