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
        /// <summary>
        /// 获取主题视图数据查询的访问对象
        /// </summary>
        /// <returns></returns>
        public IThemeViewRepository GetThemeViewRepository()
        {
            return new ThemeViewRepository(_connectionProvider);
        }
        /// <summary>
        /// 获取审核过的主题视图数据查询的访问对象
        /// </summary>
        /// <returns></returns>
        public IThemeViewRepository GetCheckedThemeViewRepository()
        {
            return new ThemeViewRepository(_connectionProvider, IThemeSky.Model.CheckStateOption.CheckSuccess, IThemeSky.Model.DisplayStateOption.Display);
        }
        /// <summary>
        /// 获取审核过的主题视图数据查询的访问对象(缓存接口)
        /// </summary>
        /// <returns></returns>
        public ICacheThemeViewRepository GetCachedThemeViewRepository()
        {
            return new CacheThemeViewRepository(_connectionProvider, IThemeSky.Model.CheckStateOption.CheckSuccess, IThemeSky.Model.DisplayStateOption.Display);
        }
        /// <summary>
        /// 获取主题的评论数据访问对象
        /// </summary>
        /// <returns></returns>
        public IThemeCommentRepository GetThemeCommentRepository()
        {
            return new ThemeCommentRepository(_connectionProvider);
        }
        /// <summary>
        /// 获取主题的反馈数据访问对象
        /// </summary>
        /// <returns></returns>
        public IThemeSupportRepository GetThemeSupportRepository()
        {
            return new ThemeSupportRepository(_connectionProvider);
        }
        /// <summary>
        /// 获取软件的评论数据访问对象
        /// </summary>
        /// <returns></returns>
        public ISoftCommentRepository GetSoftCommentRepository()
        {
            return new SoftCommentRepository(_connectionProvider);
        }
    }
}
