using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IThemeSky.Library.Extensions
{
    public static class Int64Extensions
    {
        /// <summary>
        /// 将指定的长整值转换为对应的字节大小
        /// </summary>
        /// <param name="FileSize"></param>
        /// <returns></returns>
        public static string ToFileSize(this long FileSize)
        {
            if (FileSize < 0x400L)
            {
                return string.Format("{0}Byte", FileSize);
            }
            if ((FileSize >= 0x400L) && (FileSize <= 0x100000L))
            {
                return string.Format("{0:F2}", FileSize*1.0 / 0x400L).Trim('0').Trim('.') + "KB";
            }
            if ((FileSize >= 0x100000L) && (FileSize <= 0x40000000L))
            {
                return string.Format("{0:F2}", FileSize * 1.0 / 0x100000L).Trim('0').Trim('.') + "MB";
            }
            if (FileSize >= 0x40000000L)
            {
                return string.Format("{0:F2}", FileSize * 1.0 / 0x40000000L).Trim('0').Trim('.') + "GB";
            }
            return "";
        }


    }
}
