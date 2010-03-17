using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace IThemeSky.UI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //*/list/排序方式/页码
            routes.MapRoute(
                "List",
                "list/{sort}/{pageIndex}",
                new { controller = "Home", action = "List", sort = "new", categoryId = 0, categoryName = "", tags = UrlParameter.Optional, pageIndex = UrlParameter.Optional }
            );
            //*/list/排序方式/分类名_分类id/页码
            routes.MapRoute(
                "List2",
                "list/{sort}/{categoryName}_{categoryId}/{pageIndex}",
                new { controller = "Home", action = "List", sort = "new", categoryId = 0, categoryName = "", tags = UrlParameter.Optional, pageIndex = UrlParameter.Optional }
            );
            //*/list/排序方式/页码/标签名称1,标签名称2
            routes.MapRoute(
                "List3",
                "list/{sort}/{pageIndex}/{tags}",
                new { controller = "Home", action = "List", sort = "new", categoryId = 0, categoryName = "", tags = UrlParameter.Optional, pageIndex = 1 }
            );
            //*/list/排序方式/分类名_分类id/页码/标签名称1,标签名称2
            routes.MapRoute(
                "List4",
                "list/{sort}/{categoryName}_{categoryId}/{pageIndex}/{tags}",
                new { controller = "Home", action = "List", sort = "new", categoryId = 0, categoryName = "", tags = UrlParameter.Optional, pageIndex = 1 }
            );

            //默认Route
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{themeId}", // URL with parameters
                new { controller = "Home", action = "Index", themeId = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
        }
    }
}