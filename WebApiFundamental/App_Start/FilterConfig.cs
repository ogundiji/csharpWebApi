using System;
using System.Web;
using System.Web.Mvc;

namespace WebApiFundamental.Core.Filters
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new ExceptionHandlerAttribute());
            //filters.Add(new HSTSAttribute(
            //    TimeSpan.FromMinutes(5),true,false));
            filters.Add(new HandleErrorAttribute());
        }
    }
}
