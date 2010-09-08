using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IThemeSky.UI.Models;

namespace IThemeSky.UI.Controllers
{
    public class CreatorController : ThemeControllerBase
    {
        //
        // GET: /Help/

        public ActionResult Index(string themeName, string themeId)
        {
            ViewData["ThemeName"] = themeName;
            ViewData["ThemeId"] = themeId;
            return View("Index", new NormalPageModel());
        }
    }
}
