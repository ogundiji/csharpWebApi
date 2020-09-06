using Microsoft.Web.Http;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApiFundamental
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //configure cors
            var corsAttribute = new EnableCorsAttribute("http://localhost:8080", "*", "*") { SupportsCredentials=true};
            config.EnableCors(corsAttribute);

            //serializing an web api with a config
            //changing the case of JSON
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new
                CamelCasePropertyNamesContractResolver();


            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }
    }
}
