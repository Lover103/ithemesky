using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IThemeSky.UI.Controllers
{
    public class ServiceController : Controller
    {
        //
        // GET: /Service/

        public ActionResult Download(int themeId)
        {
            return Content("下载了" + themeId);
        }

    }
}
