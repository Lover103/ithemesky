using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IThemeSky.UI.Models;

namespace IThemeSky.UI.Controllers
{
    public class HelpController : Controller
    {
        //
        // GET: /Help/

        public ActionResult Index(string viewName)
        {
            return View(viewName, new NormalPageModel());
        }
    }
}
