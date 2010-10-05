using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IThemeSky.Library.Extensions;
using IThemeSky.DataAccess;
using IThemeSky.Model;
using IThemeSky.UI.Models;

namespace IThemeSky.UI.Controllers
{
    public class HomeController : ThemeControllerBase
    {
        public ActionResult Index()
        {
            if (Request.QueryString.Count > 0 && Request.QueryString["chl"] != null)
            {
                IThemeSupportRepository repository = ThemeRepositoryFactory.Default.GetThemeSupportRepository();
                repository.AddISpiritUserInfo(
                    new ISpiritUserInfo() 
                    {
                        AddTime = DateTime.Now,
                        DeviceId = Request.QueryString["DeviceId"],
                        PhoneVersion = Request.QueryString["phonever"],
                        SoftVersion = Request.QueryString["softver"],
                        ITunesVersion = Request.QueryString["itunesver"],
                        DeviceType = Request.QueryString["deviceType"],
                        UserAgent = Request.UserAgent,
                        UserIP = Request.UserHostAddress,
                    });
            }
            IndexModel model = new IndexModel();
            return View(model);
        }

        public ActionResult List(string sort, int fw, int categoryId, string categoryName, string tags, int? pageIndex)
        {
            int currPageIndex = 1;
            if (pageIndex.HasValue)
            {
                currPageIndex = pageIndex.Value;
            }
            ThemeSortOption themeSort = sort.ToEnum<ThemeSortOption>(ThemeSortOption.New);
            ListModel model = new ListModel(fw, categoryId, categoryName, tags, themeSort, currPageIndex, 20);
            if (fw == 4)
            {
                return View("iphone4", model);
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult Search(string sort, string keyword, int? pageIndex)
        {
            int currPageIndex = 1;
            if (pageIndex.HasValue)
            {
                currPageIndex = pageIndex.Value;
            }
            ThemeSortOption themeSort = sort.ToEnum<ThemeSortOption>(ThemeSortOption.New);
            SearchModel model = new SearchModel(keyword, themeSort, currPageIndex, 20);
            return View(model);
        }

        public ActionResult Detail(int themeId)
        {
            return View(new DetailModel(themeId));
        }

        public ActionResult FAQ()
        {
            return View(new NormalPageModel());
        }

        public ActionResult Help()
        {
            return View(new NormalPageModel());
        }

        public ActionResult SubmitTheme()
        {
            return View(new NormalPageModel());
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View(new NormalPageModel());
        }

        public ActionResult Test()
        {
            return Content("hello mvc");
        }
    }
}
