using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IThemeSky.Model;
using IThemeSky.DataAccess;

namespace IThemeSky.UI.Models
{
    /// <summary>
    /// 首页数据模型
    /// </summary>
    public class IndexModel : ModelBase
    {
        public IndexModel()
        {
            LastUpdateThemes = _themeRepository.GetThemes(ThemeSortOption.New, 8);
            RecommendedThemes = _themeRepository.GetThemes(ThemeSortOption.Recommended, 8);
            PopularThemes = _themeRepository.GetThemes(ThemeSortOption.Popular, 8);
            LuckyThemes = _themeRepository.GetRandomThemes(0, ThemeSortOption.Rating, 4);
        }
        /// <summary>
        /// 最新更新主题列表
        /// </summary>
        public List<SimpleThemeView> LastUpdateThemes { get; private set; }
        /// <summary>
        /// 推荐的主题列表
        /// </summary>
        public List<SimpleThemeView> RecommendedThemes { get; private set; }
        /// <summary>
        /// 最受欢迎的主题列表
        /// </summary>
        public List<SimpleThemeView> PopularThemes { get; private set; }
        /// <summary>
        /// 随机播放的主题
        /// </summary>
        public List<SimpleThemeView> LuckyThemes { get; private set; }
    }
}
