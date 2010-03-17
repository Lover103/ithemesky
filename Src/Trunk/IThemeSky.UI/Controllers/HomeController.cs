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
            List<string> lstTags;
            if (string.IsNullOrEmpty(tags))
            {
                lstTags = new List<string>();
            }
            else
            {
                lstTags = tags.Split(',').ToList();
            }
            int currPageIndex = 1;
            if (pageIndex.HasValue)
            {
                currPageIndex = pageIndex.Value;
            }
            ThemeSortOption themeSort = sort.ToEnum<ThemeSortOption>(ThemeSortOption.New);
            ListModel model = new ListModel(categoryId, categoryName, lstTags, themeSort, currPageIndex, 6); 
            return View(model);
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
