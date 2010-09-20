using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IThemeSky.Model
{
    /// <summary>
    /// 简单主题实体（非表映射）
    /// </summary>
    public class SimpleThemeView
    {
        /// <summary>
        /// 主题id
        /// </summary>
        public int ThemeId { get; set; }

        /// <summary>
        /// 分类id
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 主题分类名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 缩略图地址
        /// </summary>
        public string Thumbnail { get; set; }

        /// <summary>
        /// 112x168缩略图地址
        /// </summary>
        public string Thumbnail_112x168 { get; set; }

        /// <summary>
        /// 推荐指数
        /// </summary>
        public int CommendIndex { get; set; }

        /// <summary>
        /// 总下载数
        /// </summary>
        public int Downloads { get; set; }

        /// <summary>
        /// 是否是IPhone4主题
        /// </summary>
        public bool SupportIPhone4 { get; set; }

        /// <summary>
        /// 主题详细页地址
        /// </summary>
        public string ThemeDetailUrl
        {
            get
            {
                return string.Format("/iphone-themes/{0}/{1}"
                    , Title.Trim().Replace(" ", "-")
                    , ThemeId
                    );
            }
        }
    }
}
