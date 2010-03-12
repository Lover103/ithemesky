using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IThemeSky.Model
{
    /// <summary>
    /// 主题显示状态
    /// </summary>
    public enum DisplayStateOption
    {
        /// <summary>
        /// 所有状态
        /// </summary>
        All = -2,
        /// <summary>
        /// 已删除
        /// </summary>
        Deleted = -1,
        /// <summary>
        /// 隐藏
        /// </summary>
        Hidden = 0,
        /// <summary>
        /// 显示
        /// </summary>
        Display = 1,
    }
}
