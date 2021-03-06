﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GameOfLife
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "HomeRedirect",
                url: "Home",
                defaults: new { controller = "Home", action = "Index", redirect = true }
            );

            routes.MapRoute(
                name: "HomeIndexRedirect",
                url: "Home/Index",
                defaults: new { controller = "Home", action = "Index", redirect = true }
            );

            routes.MapRoute(
                name: "ActiveDelete",
                url: "Games/Active/Delete/{sessionId}",
                defaults: new { controller = "Games", action = "DeleteActive", sessionId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Rules",
                url: "Rules",
                defaults: new { controller = "Home", action = "Rules" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
