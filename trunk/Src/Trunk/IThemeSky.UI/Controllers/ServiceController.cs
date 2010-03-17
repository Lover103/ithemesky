using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IThemeSky.Library.Extensions;
using IThemeSky.Model;
using IThemeSky.DataAccess;

namespace IThemeSky.UI.Controllers
{
    public class ServiceController : Controller
    {
        private ICacheThemeViewRepository _themeRepository = ThemeRepositoryFactory.Default.GetCachedThemeViewRepository();

        public ActionResult Download(int themeId)
        {
            return Content("下载了" + themeId);
        }

        public ActionResult GetSortThemes(string sort, int DisplayNumber)
        {
            ThemeSortOption themeSort = sort.ToEnum<ThemeSortOption>(ThemeSortOption.New);
            List<SimpleThemeView> themes = _themeRepository.GetThemes(themeSort, 8);
            return this.PartialView("NormalThemeRepeater", themes);
        }

    }
}
