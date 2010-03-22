using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IThemeSky.Library.Data
{
    /// <summary>
    /// 数据库连接提供接口
    /// </summary>
    public interface IConnectionProvider
    {
        /// <summary>
        /// 获取只读数据库的连接字符串
        /// </summary>
        /// <returns></returns>
        string GetReadConnectionString();
        /// <summary>
        /// 获取写入数据库的连接字符串
        /// </summary>
        /// <returns></returns>
        string GetWriteConnectionString();
    }
}
