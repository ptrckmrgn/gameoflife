using FluentValidation.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;

namespace GameOfLife
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);

            FluentValidationModelValidatorProvider.Configure(provider => provider.AddImplicitRequiredValidator = false);

            // jquery for webforms
            ScriptManager.ScriptResourceMapping.AddDefinition("jquery", new ScriptResourceDefinition
            {
                Path = "~/scripts/jquery-1.10.2.min.js",
                DebugPath = "~/scripts/jquery-1.10.2.js",
                CdnPath = "http://ajax.microsoft.com/ajax/jQuery/jquery-1.4.1.min.js",
                CdnDebugPath = "http://ajax.microsoft.com/ajax/jQuery/jquery-1.4.1.js"
            });
        }
    }
}
