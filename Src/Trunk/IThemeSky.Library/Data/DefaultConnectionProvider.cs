using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace IThemeSky.Library.Data
{
    /// <summary>
    /// 默认的数据库连接配置。使用WebConfig中的DefaultConnectionProvider节点
    /// </summary>
    public class DefaultConnectionProvider : ConfigurationSection, IConnectionProvider
    {

        #region IConnectionProvider Members

        public string GetReadConnectionString()
        {
            return ReadConnectionString;
        }

        public string GetWriteConnectionString()
        {
            return WriteConnectionString;
        }

        #endregion

        /// <summary>
        /// 获取或设置只读的资源数据库连接串
        /// </summary>
        [ConfigurationProperty("ReadConnectionString", DefaultValue = "", IsRequired = true)]
        public string ReadConnectionString
        {
            get { return this["ReadConnectionString"].ToString(); }
            set { this["ReadConnectionString"] = value; }
        }
        /// <summary>
        /// 获取或设置可写的资源数据库连接串
        /// </summary>
        [ConfigurationProperty("WriteConnectionString", DefaultValue = "", IsRequired = true)]
        public string WriteConnectionString
        {
            get { return this["WriteConnectionString"].ToString(); }
            set { this["WriteConnectionString"] = value; }
        }
    }
}
