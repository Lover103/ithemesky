using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IThemeSky.Model;

namespace IThemeSky.UI.Models
{
    public class NormalPageModel : ModelBase
    {
        public NormalPageModel()
        {
            TopDownloadThemes = _themeRepository.GetThemes(ThemeSortOption.Popular, 10);
            CommendThemes = _themeRepository.GetThemes(ThemeSortOption.Recommended, 10);
        }

        /// <summary>
        /// 下载排行的主题
        /// </summary>
        public List<SimpleThemeView> TopDownloadThemes { get; private set; }
        /// <summary>
        /// 推荐的主题
        /// </summary>
        public List<SimpleThemeView> CommendThemes { get; set; }
    }
}
