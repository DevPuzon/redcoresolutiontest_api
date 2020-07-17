using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace RedCoreApi_Test
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //[EnableCors(origins: "*", headers: "*", methods: "*")]
            // Web API configuration and services

            // Web API routes
            var cors = new EnableCorsAttribute("http://localhost:8100/", "*", "*");
            config.EnableCors(cors);
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //config.();
            //// Web API configuration and services
            //// Web API routes
            //config.MapHttpAttributeRoutes();
            //config.Routes.MapHttpRoute(
            //name: "DefaultApi",
            //routeTemplate: "api/{controller}/{id}",
            //defaults: new { id = RouteParameter.Optional }
            //);
        }
    }
}
