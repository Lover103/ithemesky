using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IThemeSky.DataAccess;
using IThemeSky.UI.Models;

namespace IThemeSky.UI.Controllers
{
    public class ISpiritController : Controller
    {
        //
        // GET: /ISpirit/

        public ActionResult Index(string viewName)
        {
            return View(viewName, new NormalPageModel());
        }

        public ActionResult Donwload()
        { 
            //增加统计信息
            IThemeManageRepository _themeManageRepository = ThemeRepositoryFactory.Default.GetThemeManageRepository();
            _themeManageRepository.InsertDownloadHistory(0, Request.UserHostAddress);
            //转向到下载地址
            return Redirect("http://update.ithemesky.com/iSpirit.exe");
        }
    }
}
