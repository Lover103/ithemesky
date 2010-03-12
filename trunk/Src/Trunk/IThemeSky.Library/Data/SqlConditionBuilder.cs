using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace IThemeSky.Library.Data
{
    /// <summary>
    /// SQL条件创建类
    /// </summary>
    public static class SqlConditionBuilder
    {
        /// <summary>
        /// 根据泛型列表解析出查询子串，且对单引号进行安全替换。返回带AND前缀的查询子串。 
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="values"></param>
        /// <returns>返回带AND前缀的查询子串</returns>
        public static string GetMultiQueryValues(string fieldName, IList values)
        {
            StringBuilder expression = new StringBuilder();
            int i = 0;
            foreach (var value in values)
            {
                if (i > 0)
                {
                    expression.Append(",");
                }
                if (value.GetType().IsEnum)
                {
                    expression.Append(Convert.ToInt32(value));
                }
                else if (value.GetType().IsValueType)
                {
                    expression.Append(value);
                }
                else
                {
                    expression.Append("'");
                    expression.Append(value.ToString().Replace("'", "''"));
                    expression.Append("'");
                }
                i++;
            }
            //虽然经过证实Sqlserver对In查询时，如果只有一个值时，与等号操作是无异的，但保险起见还是将两种写法分开。
            if (values.Count > 1)
            {
                return " AND " + fieldName + " IN (" + expression + ")";
            }
            else if (values.Count == 1)
            {
                return " AND " + fieldName + " = " + expression;
            }
            //当values为空集合时，则条件不成立
            return " AND 1=2";
        }

        /// <summary>
        /// 根据泛型列表解析出排除的查询子串(不等于或者NOT IN)。返回带AND前缀的查询子串。 
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="values"></param>
        /// <returns>返回带AND前缀的查询子串</returns>
        public static string GetExcepetMultiQueryValues(string fieldName, IList values)
        {
            StringBuilder expression = new StringBuilder();
            int i = 0;
            foreach (var value in values)
            {
                if (i > 0)
                {
                    expression.Append(",");
                }
                if (value.GetType().IsEnum)
                {
                    expression.Append(Convert.ToInt32(value));
                }
                else if (value.GetType().IsValueType)
                {
                    expression.Append(value);
                }
                else
                {
                    expression.Append("'");
                    expression.Append(value.ToString().Replace("'", "''"));
                    expression.Append("'");
                }
                i++;
            }

            if (values.Count > 1)
            {
                return " AND " + fieldName + " NOT IN (" + expression + ")";
            }
            else if (values.Count == 1)
            {
                return " AND " + fieldName + " <> " + expression;
            }
            return "";
        }
    }
}
