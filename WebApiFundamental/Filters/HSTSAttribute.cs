using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WebApiFundamental.Filters
{
    public class HSTSAttribute:RequireHttpsAttribute
    {
        public TimeSpan MaxAge { get; private set; }
        public bool IncludeSubDomains { get; private set; }
        public bool Preload { get; private set; }
        public HSTSAttribute(TimeSpan? maxAge,bool includeSubdomains=true,bool preload=false):base()
        {
            MaxAge = maxAge.HasValue ? maxAge.Value : TimeSpan.FromDays(30);
            IncludeSubDomains = includeSubdomains;
            Preload = preload;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsSecureConnection)
            {
                StringBuilder headerBuilder = new StringBuilder($"max-age={MaxAge.TotalSeconds}");

                if (IncludeSubDomains)
                {
                    headerBuilder.Append("; includeSubDomains");
                }

                if (Preload)
                {
                    headerBuilder.Append("; preload");
                }


                filterContext.HttpContext.Response.Headers.Add("Strict-Transport-Security", headerBuilder.ToString());
            }
            else
            {
                HandleNonHttpsRequest(filterContext);
            }
           
        }
    }
}