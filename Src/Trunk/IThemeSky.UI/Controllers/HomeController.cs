using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IThemeSky.UI.Models;

namespace IThemeSky.UI.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private IThemeSkyEntities _db = new IThemeSkyEntities(); 
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to ASP.NET MVC!";
            var theme = (from t in _db.Theme where t.ThemeId < 2 select t).First();
            return View(theme);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
