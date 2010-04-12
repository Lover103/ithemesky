using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace IThemeSky.Library.Extensions
{
    /// <summary>
    /// 对RepeaterItem的扩展
    /// </summary>
    public static class RepeaterItemExtensions
    {
        /// <summary>
        /// 执行父Repater控件中的数据表达式
        /// </summary>
        /// <param name="control">当前的repeater子项</param>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        public static object ParentEval(this RepeaterItem control, string expression)
        {
            Control parentControl = control.Parent;
            while (parentControl != null)
            {
                if (parentControl is RepeaterItem)
                {
                    return DataBinder.Eval((parentControl as RepeaterItem).DataItem, expression);
                }
                parentControl = parentControl.Parent;
            }
            return "";
        }
    }
}
