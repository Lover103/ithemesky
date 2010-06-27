using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IThemeSky.Model;
using IThemeSky.DataAccess;

namespace IThemeSky.UI.Models
{
    public class SoftCommentDetailModel : ModelBase
    {

        public SoftCommentDetailModel(string softIdentfiy, string softTitle, string softDescription)
        {
            SoftIdentfiy = softIdentfiy;
            SoftTitle = softTitle;
            SoftDescription = softDescription;

            TopDownloadThemes = _themeRepository.GetThemesByCategoryId(0, ThemeSortOption.Popular, 10);
            CommendThemes = _themeRepository.GetThemesByCategoryId(0, ThemeSortOption.Recommended, 10);

            PostComment = new PostSoftCommentModel() 
            { 
                SoftIdentify = "",
                SoftTitle = "Unknow",
            };
        }

        public string SoftIdentfiy { get; private set; }

        public string SoftTitle { get; private set; }

        public string SoftDescription { get; private set; }

        public PostSoftCommentModel PostComment { get; private set; }
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
