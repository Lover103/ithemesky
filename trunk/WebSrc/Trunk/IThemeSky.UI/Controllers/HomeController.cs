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
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IndexModel model = new IndexModel();
            return View(model);
        }

        public ActionResult List(string sort, int categoryId, string categoryName, string tags, int? pageIndex)
        {
            int currPageIndex = 1;
            if (pageIndex.HasValue)
            {
                currPageIndex = pageIndex.Value;
            }
            ThemeSortOption themeSort = sort.ToEnum<ThemeSortOption>(ThemeSortOption.New);
            ListModel model = new ListModel(categoryId, categoryName, tags, themeSort, currPageIndex, 20); 
            return View(model);
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

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Test()
        {
            return Content("hello mvc");
        }
    }
}
