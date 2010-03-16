using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
