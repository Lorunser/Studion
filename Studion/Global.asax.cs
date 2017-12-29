using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

//3rd party
using GleamTech.DocumentUltimate.Web;

namespace Studion
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //The default CachePath value is "~/App_Data/DocumentCache"
            //Both virtual and physical paths are allowed.
            DocumentUltimateWebConfiguration.Current.CachePath = "~/App_Data/DocumentCache";
        }
    }
}
