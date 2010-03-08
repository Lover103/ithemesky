using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using IThemeSky.Library.Data;
using IThemeSky.DataAccess;

namespace IThemeSky.DataAccess
{
    /// <summary>
    /// 主题数据层对象访问工厂
    /// </summary>
    public class ThemeRepositoryFactory
    {
        private static ThemeRepositoryFactory _defaultRepositoryFactory;
        private IConnectionProvider _connectionProvider;

        /// <summary>
        /// 初始化默认的手机用户数据访问层工厂类
        /// </summary>
        static ThemeRepositoryFactory()
        {
            IConnectionProvider connectionProvider = ConfigurationManager.GetSection("IThemeSkyConnectionProvider") as IConnectionProvider;
            if (connectionProvider == null)
            {
                throw new ConfigurationErrorsException("使用[手机用户数据访问工厂]默认对象，必须在config配置OrderConnectionProvider子节点");
            }
            _defaultRepositoryFactory = new ThemeRepositoryFactory(connectionProvider);
        }
        /// <summary>
        /// 获取默认手机用户数据访问层对象。您需要在Web.Config中配置IThemeSkyConnectionProvider中节点
        /// </summary>
        public static ThemeRepositoryFactory Default
        {
            get { return _defaultRepositoryFactory; }
        }
        /// <summary>
        /// 获取当前的数据库连接对象
        /// </summary>
        public IConnectionProvider ConnectionProvider
        {
            get { return _connectionProvider; }
        }
        /// <summary>
        /// 使用指定的数据库连接提供对象来初始化手机用户数据访问层工厂
        /// </summary>
        /// <param name="connectionProvider">数据库连接提供对象</param>
        public ThemeRepositoryFactory(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }
        /// <summary>
        /// 获取主题管理数据访问层对象
        /// </summary>
        /// <returns></returns>
        public IThemeManageRepository GetThemeManageRepository()
        {
            return new ThemeManageRepository(_connectionProvider);
        }
    }
}
