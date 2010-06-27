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
    public class SoftController : ThemeControllerBase
    {
        public ActionResult Comment(string softIdentify, string softTitle, string softDescription)
        {
            return View(new SoftCommentDetailModel(softIdentify, softTitle, softDescription));
        }
    }
}
