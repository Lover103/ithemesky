﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IThemeSky.Model;

namespace IThemeSky.UI.Models
{
    public class DetailModel : ModelBase
    {

        public DetailModel(int themeId)
        {
            CurrentTheme = _themeRepository.GetTheme(themeId);
            string themeName;
            PrevThemeId = _themeRepository.GetPrevThemeId(CurrentTheme.CheckerId, themeId, out themeName);
            PrevThemeName = themeName;
            NextThemeId = _themeRepository.GetNextThemeId(CurrentTheme.CheckerId, themeId, out themeName);
            NextThemeName = themeName;

            LuckyThemes = _themeRepository.GetRandomThemes(CurrentTheme.CategoryId, ThemeSortOption.Rating, 4);
            TopDownloadThemes = _themeRepository.GetThemesByCategoryId(CurrentTheme.CategoryId, ThemeSortOption.Popular, 10);
            CommendThemes = _themeRepository.GetThemesByCategoryId(CurrentTheme.CategoryId, ThemeSortOption.Recommended, 10);

            PostComment = new PostCommentModel() 
            { 
                ThemeId = themeId,
            };
        }
        public FullThemeView CurrentTheme { get; private set; }
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