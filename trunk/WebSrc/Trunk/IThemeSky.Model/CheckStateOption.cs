using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IThemeSky.Model
{
    /// <summary>
    /// 主题审核状态
    /// </summary>
    public enum CheckStateOption
    {
        /// <summary>
        /// 所有状态
        /// </summary>
        All = -2,
        /// <summary>
        /// 审核不通过
        /// </summary>
        CheckFailure = -1,
        /// <summary>
        /// 等待审核
        /// </summary>
        Waitting = 0,
        /// <summary>
        /// 审核通过
        /// </summary>
        CheckSuccess = 1,
    }
}
