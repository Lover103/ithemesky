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
            
            routes.MapRoute(
                "css",
                "Content/css/site.css",
                new { controller = "Service", action = "GetCss" }
            );

            //*/list/排序方式/分类名_分类id/页码/标签名称1,标签名称2
            routes.MapRoute(
                "List4",
                "list/{sort}/{categoryName}_{categoryId}/{pageIndex}/{tags}",
                new { controller = "Home", action = "List", sort = "new", categoryId = 0, categoryName = "", tags = UrlParameter.Optional, pageIndex = 1 }
            );
            //*/list/排序方式/分类名_分类id/页码
            routes.MapRoute(
                "List2",
                "list/{sort}/{categoryName}_{categoryId}/{pageIndex}",
                new { controller = "Home", action = "List", sort = "new", categoryId = 0, categoryName = "", tags = UrlParameter.Optional, pageIndex = UrlParameter.Optional }
            );
            //*/list/排序方式/页码/标签名称1,标签名称2+标签名称3,标签名称4+...
            routes.MapRoute(
                "List3",
                "list/{sort}/{pageIndex}/{tags}",
                new { controller = "Home", action = "List", sort = "new", categoryId = 0, categoryName = "", tags = UrlParameter.Optional, pageIndex = 1 }
            );
            //*/list/排序方式/页码
            routes.MapRoute(
                "List",
                "list/{sort}/{pageIndex}",
                new { controller = "Home", action = "List", sort = "new", categoryId = 0, categoryName = "", tags = UrlParameter.Optional, pageIndex = UrlParameter.Optional }
            );

            //*/search/排序方式/关键字/页码/
            routes.MapRoute(
                "Search",
                "search/{sort}/{keyword}/{pageIndex}",
                new { controller = "Home", action = "Search", sort = "new", keyword = "", pageIndex = 1 }
            );
            

            //详细页
            routes.MapRoute(
                "DetailWithExt", // Route name
                "iphone-themes/{themeName}/{themeId}.zip", // URL with parameters
                new { controller = "Home", action = "Detail" } // Parameter defaults
            );
            routes.MapRoute(
                "Detail", // Route name
                "iphone-themes/{themeName}/{themeId}", // URL with parameters
                new { controller = "Home", action = "Detail" } // Parameter defaults
            );
            
            //接口服务
            routes.MapRoute(
                "GetSortThemesService", // Route name
                "Service/GetSortThemes/{sort},{displayNumber}", // URL with parameters
                new { controller = "Service", action = "GetSortThemes", sort = "new", displayNumber = 8 } // Parameter defaults
            );
            routes.MapRoute(
                "GetSuggestThemesService", // Route name
                "Service/GetSuggestThemes/{keyword},{displayNumber}", // URL with parameters
                new { controller = "Service", action = "GetSuggestThemes", keyword = "new", displayNumber = 3 } // Parameter defaults
            );
            routes.MapRoute(
                "GetThemeCommentsService", // Route name
                "Service/GetThemeComments/{themeId},{pageIndex},{pageSize}", // URL with parameters
                new { controller = "Service", action = "GetThemeComments", pageIndex=1, pageSize = 5 } // Parameter defaults
            );
            routes.MapRoute(
                "AddThemeCommentsService", // Route name
                "Service/AddThemeComment", // URL with parameters
                new { controller = "Service", action = "AddThemeComment" } // Parameter defaults
            );
            routes.MapRoute(
                "DownloadThemeService", // Route name
                "Service/Download/{themeId},{themeName}", // URL with parameters
                new { controller = "Service", action = "DownloadTheme" } // Parameter defaults
            );
            routes.MapRoute(
                "DownloadThemePicService", // Route name
                "Service/DownloadPic/{themeId},{themeName}", // URL with parameters
                new { controller = "Service", action = "DownloadThemePic" } // Parameter defaults
            );
            routes.MapRoute(
                "RateThemeService", // Route name
                "Service/RateTheme/{themeId},{score}", // URL with parameters
                new { controller = "Service", action = "RateTheme" } // Parameter defaults
            );
            //ISpirit专用服务
            routes.MapRoute(
                "iSpiritService", // Route name
                "iSpirit/download", // URL with parameters
                new { controller = "ISpirit", action = "Donwload" } // Parameter defaults
            );
            routes.MapRoute(
                "iSpirit", // Route name
                "iSpirit/{viewName}", // URL with parameters
                new { controller = "iSpirit", action = "Index", viewName = "index" } // Parameter defaults
            );

            //帮助
            routes.MapRoute(
                "Help", // Route name
                "Help/{viewName}", // URL with parameters
                new { controller = "Help", action = "Index", viewName = "faq" } // Parameter defaults
            );
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