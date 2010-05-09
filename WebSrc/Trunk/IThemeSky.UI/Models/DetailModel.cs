using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IThemeSky.Model;
using IThemeSky.DataAccess;

namespace IThemeSky.UI.Models
{
    public class DetailModel : ModelBase
    {

        public DetailModel(int themeId)
        {
            ThemeRepositoryFactory.Default.GetThemeManageRepository().IncreaseViews(themeId, 1);
            CurrentTheme = _themeRepository.GetTheme(themeId);
            CurrentTheme.Views++;
            Tags = _themeRepository.GetTagsByThemeId(themeId);
            string themeName;
            PrevThemeId = _themeRepository.GetPrevThemeId(CurrentTheme.CategoryId, themeId, out themeName);
            PrevThemeName = themeName;
            NextThemeId = _themeRepository.GetNextThemeId(CurrentTheme.CategoryId, themeId, out themeName);
            NextThemeName = themeName;

            LuckyThemes = _themeRepository.GetRandomThemes(themeId, CurrentTheme.CategoryId, 5).Where(theme => theme.ThemeId != themeId).Take(4).ToList();
            TopDownloadThemes = _themeRepository.GetThemesByCategoryId(CurrentTheme.CategoryId, ThemeSortOption.Popular, 10);
            CommendThemes = _themeRepository.GetThemesByCategoryId(CurrentTheme.CategoryId, ThemeSortOption.Recommended, 10);

            PostComment = new PostCommentModel() 
            { 
                ThemeId = themeId,
            };
        }
        public FullThemeView CurrentTheme { get; private set; }
        public List<string> Tags { get; private set; }
        public int PrevThemeId { get; private set; }
        public string PrevThemeName { get; private set; }
        public int NextThemeId { get; private set; }
        public string NextThemeName { get; private set; }

        public PostCommentModel PostComment { get; private set; }

        /// <summary>
        /// 随机播放的主题
        /// </summary>
        public List<SimpleThemeView> LuckyThemes { get; private set; }
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
