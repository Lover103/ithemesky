using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IThemeSky.DataAccess;
using IThemeSky.Model;

namespace IThemeSky.UI.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<SimpleThemeView> themes = ThemeRepositoryFactory.Default.GetCachedThemeViewRepository().GetThemes(IThemeSky.Model.ThemeSortOption.New, 8);
            ViewData["Message"] = "Welcome to ASP.NET MVC!";
            return View(themes);
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
