using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IThemeSky.Library.Extensions
{
    public static class Int32Extensions
    {
        /// <summary>
        /// 将Int32值转成指定的枚举类型
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="source">源枚举值</param>
        /// <param name="defaultValue">如果转换失败，返回默认的枚举项</param>
        /// <returns></returns>
        public static T ToEnum<T>(this int source, T defaultValue)
        {
            string source2 = source.ToString();
            if (!string.IsNullOrEmpty(source2))
            {
                try
                {
                    T value = (T)Enum.Parse(typeof(T), source2, true);
                    if (Enum.IsDefined(typeof(T), value))
                    {
                        return value;
                    }
                }
                catch { }
            }
            return defaultValue;
        }
    }
}
