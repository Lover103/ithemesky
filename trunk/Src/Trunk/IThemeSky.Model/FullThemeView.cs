using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IThemeSky.Model
{
    /// <summary>
    /// 完整的主题视图
    /// </summary>
    public class FullThemeView : Theme
    {
        /// <summary>
        /// 主题分类名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 主题详细页地址
        /// </summary>
        public string ThemeDetailUrl
        {
            get
            {
                return string.Format("/iphone-themes/{0}/{1}"
                    , Title.Replace(" ", "-")
                    , ThemeId
                    );
            }
        }
    }
}
