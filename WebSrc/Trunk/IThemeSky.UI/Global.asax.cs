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
                "GetSoftCommentsService", // Route name
                "Service/GetSoftComments/{softIdentify},{pageIndex},{pageSize}", // URL with parameters
                new { controller = "Service", action = "GetSoftComments", pageIndex = 1, pageSize = 5 } // Parameter defaults
            );
            routes.MapRoute(
                "AddThemeCommentsService", // Route name
                "Service/AddThemeComment", // URL with parameters
                new { controller = "Service", action = "AddThemeComment" } // Parameter defaults
            );
            routes.MapRoute(
                "AddSoftCommentsService", // Route name
                "Service/AddSoftComment", // URL with parameters
                new { controller = "Service", action = "AddSoftComment" } // Parameter defaults
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
                "ParseThemeZipService", // Route name
                "Service/ParseThemeZip/{themeId}", // URL with parameters
                new { controller = "Service", action = "ParseThemeZip" } // Parameter defaults
            );
            routes.MapRoute(
                "RateThemeService", // Route name
                "Service/RateTheme/{themeId},{score}", // URL with parameters
                new { controller = "Service", action = "RateTheme" } // Parameter defaults
            );
            routes.MapRoute(
                "SiteMapService", // Route name
                "sitemap.xml", // URL with parameters
                new { controller = "Service", action = "GetSiteMapXml" } // Parameter defaults
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

            //About
            routes.MapRoute(
                "About", // Route name
                "About/{viewName}", // URL with parameters
                new { controller = "About", action = "Index", viewName = "index" } // Parameter defaults
            );

            //DIY
            //routes.MapRoute(
            //    "DIY", // Route name
            //    "DIY/{themeName},{themeId}", // URL with parameters
            //    new { controller = "DIY", action = "Index", themeId = "100", themeName = "iThemeSky" } // Parameter defaults
            //);
            routes.MapRoute(
                "CreatorDefalut", // Route name
                "Creator", // URL with parameters
                new { controller = "Creator", action = "Index", themeName = "", themeId = "" } // Parameter defaults
            );
            routes.MapRoute(
                "Creator", // Route name
                "Creator/{themeName},{themeId}", // URL with parameters
                new { controller = "Creator", action = "Index", themeName = "", themeId = "" } // Parameter defaults
            );

            //软件相关
            routes.MapRoute(
                "Soft", // Route name
                "Soft/Comment/{softIdentify}/{softTitle}/{softDescription}", // URL with parameters
                new { controller = "Soft", action = "Comment" } // Parameter defaults
            );

            //默认
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