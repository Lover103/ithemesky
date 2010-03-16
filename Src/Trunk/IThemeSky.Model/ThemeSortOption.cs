using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IThemeSky.Model
{
    /// <summary>
    /// 主题排序方式
    /// </summary>
    public enum ThemeSortOption
    {
        /// <summary>
        /// 最新排序
        /// </summary>
        New,
        /// <summary>
        /// 下载排序
        /// </summary>
        Popular,
        /// <summary>
        /// 月下载排行
        /// </summary>
        Rank_Of_Month,
        /// <summary>
        /// 周下载排行
        /// </summary>
        Rank_Of_Week,
        /// <summary>
        /// 推荐排序
        /// </summary>
        Recommended,
        /// <summary>
        /// 评价排行
        /// </summary>
        RateScore,
        /// <summary>
        /// 标签排序
        /// </summary>
        TagSort,
    }
}
