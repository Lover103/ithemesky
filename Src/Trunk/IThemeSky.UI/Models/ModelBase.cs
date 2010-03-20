using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IThemeSky.Model;
using IThemeSky.DataAccess;
using System.Globalization;

namespace IThemeSky.UI.Models
{
    public abstract class ModelBase
    {
        protected ICacheThemeViewRepository _themeRepository = ThemeRepositoryFactory.Default.GetCachedThemeViewRepository();
        public List<ThemeCategory> ThemeCategories
        {
            get
            {
                return _themeRepository.GetThemeCategories();
            }
        }

        public CultureInfo USACultureInfo
        {
            get
            {
                return new CultureInfo("en-US");
            }
        }
    }
}
