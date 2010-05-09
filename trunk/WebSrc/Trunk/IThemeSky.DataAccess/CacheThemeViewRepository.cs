using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IThemeSky.Model;
using System.Data;
using IThemeSky.Library.Data;
using System.Data.SqlClient;
using IThemeSky.Library.Util;

namespace IThemeSky.DataAccess
{
    /// <summary>
    /// 带缓存的主题视图数据操作类
    /// </summary>
    public class CacheThemeViewRepository : ThemeViewRepository, ICacheThemeViewRepository
    {
        private bool _enableCache = true;
        private CacheTimeOption _cacheTime = CacheTimeOption.Normal;

        /// <summary>
        /// 初始化带缓存的主题视图数据操作类
        /// </summary>
        /// <param name="connectionProvider">数据库连接串提供对象</param>
        public CacheThemeViewRepository(IConnectionProvider connectionProvider)
            : base(connectionProvider)
        { }

        /// <summary>
        /// 初始化带缓存的主题视图数据操作类
        /// </summary>
        /// <param name="connectionProvider">数据库连接串提供对象</param>
        /// <param name="checkState">默认审核状态</param>
        /// <param name="displayState">默认显示状态</param>
        public CacheThemeViewRepository(IConnectionProvider connectionProvider, CheckStateOption checkState, DisplayStateOption displayState)
            : base(connectionProvider)
        {
            _filter.CheckState = checkState;
            _filter.DisplayState = displayState;
        }

        /// <summary>
        /// 使下次调用该接口的任意方法时不从缓存读取
        /// </summary>
        public void DisableCache()
        {
            _enableCache = false;
        }
        /// <summary>
        /// 设置下次调用该接口的任意方法时的缓存时间
        /// </summary>
        /// <param name="cacheTime">缓存时间</param>
        public void SetCacheTime(CacheTimeOption cacheTime)
        {
            _cacheTime = cacheTime;
        }

        #region 通用的主题列表获取方法(Override)
        /// <summary>
        /// 根据主题id获取主题完整实体
        /// </summary>
        /// <param name="themeId">主题id</param>
        /// <returns></returns>
        public override FullThemeView GetTheme(int themeId)
        {
            FullThemeView theme;
            string cacheKey = BuildCacheKey("GetTheme", themeId);
            if (_enableCache == false)
            {
                theme = base.GetTheme(themeId);
                _enableCache = true;
            }
            else
            {
                if (CacheHelper.Contains(cacheKey))
                {
                    theme = CacheHelper.Get<FullThemeView>(cacheKey);
                }
                else
                {
                    theme = base.GetTheme(themeId);
                    CacheHelper.Set<FullThemeView>(cacheKey, theme, _cacheTime);
                }
            }
            return theme;
        }
        /// <summary>
        /// 根据指定主题的下一个主题id
        /// </summary>
        /// <param name="categoryId">所属分类id</param>
        /// <param name="themeId">主题id</param>
        /// <param name="themeName">主题名称(out)</param>
        /// <returns></returns>
        public override int GetNextThemeId(int categoryId, int themeId, out string themeName)
        {
            int id;
            string cacheKey = BuildCacheKey("GetNextThemeId", themeId);
            string cacheKey2 = BuildCacheKey("GetNextThemeId_themeName", themeId);
            if (_enableCache == false)
            {
                id = base.GetNextThemeId(categoryId, themeId, out themeName);
                _enableCache = true;
            }
            else
            {
                if (CacheHelper.Contains(cacheKey) && CacheHelper.Contains(cacheKey2))
                {
                    id = CacheHelper.Get<int>(cacheKey);
                    themeName = CacheHelper.Get<string>(cacheKey2);
                }
                else
                {
                    id = base.GetNextThemeId(categoryId, themeId, out themeName);
                    CacheHelper.Set<int>(cacheKey, id, _cacheTime);
                    CacheHelper.Set<string>(cacheKey2, themeName, _cacheTime);
                }
            }
            return id;
        }
        /// <summary>
        /// 根据指定主题的上一个主题id
        /// </summary>
        /// <param name="categoryId">所属分类id</param>
        /// <param name="themeId">主题id</param>
        /// <param name="themeName">主题名称(out)</param>
        /// <returns></returns>
        public override int GetPrevThemeId(int categoryId, int themeId, out string themeName)
        {
            int id;
            string cacheKey = BuildCacheKey("GetPrevThemeId", themeId);
            string cacheKey2 = BuildCacheKey("GetPrevThemeId_themeName", themeId);
            if (_enableCache == false)
            {
                id = base.GetPrevThemeId(categoryId, themeId, out themeName);
                _enableCache = true;
            }
            else
            {
                if (CacheHelper.Contains(cacheKey) && CacheHelper.Contains(cacheKey2))
                {
                    id = CacheHelper.Get<int>(cacheKey);
                    themeName = CacheHelper.Get<string>(cacheKey2);
                }
                else
                {
                    id = base.GetPrevThemeId(categoryId, themeId, out themeName);
                    CacheHelper.Set<int>(cacheKey, id, _cacheTime);
                    CacheHelper.Set<string>(cacheKey2, themeName, _cacheTime);
                }
            }
            return id;
        }

        /// <summary>
        /// 获取符何指定过滤器条件的主题列表
        /// </summary>
        /// <param name="viewName">视图名称</param>
        /// <param name="searchCondition">查询条件</param>
        /// <param name="sortExpression">排序方式</param>
        /// <param name="dispayNumber">提取条数</param>
        /// <returns></returns>
        protected override List<SimpleThemeView> GetSimpleThemes(string viewName, string searchCondition, string sortExpression, int dispayNumber)
        {
            List<SimpleThemeView> themes;
            string cacheKey = BuildCacheKey("GetSimpleThemes", viewName, searchCondition, sortExpression, dispayNumber);
            if (_enableCache == false)
            {
                themes = base.GetSimpleThemes(viewName, searchCondition, sortExpression, dispayNumber);
                _enableCache = true;
            }
            else
            {
                if (CacheHelper.Contains(cacheKey))
                {
                    themes = CacheHelper.Get<List<SimpleThemeView>>(cacheKey);
                }
                else
                {
                    themes = base.GetSimpleThemes(viewName, searchCondition, sortExpression, dispayNumber);
                    CacheHelper.Set<List<SimpleThemeView>>(cacheKey, themes, _cacheTime);
                }
            }
            return themes;
        }

        /// <summary>
        /// 获取符何指定过滤器条件的主题列表
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <param name="sort">排序方式</param>
        /// <param name="pageIndex">显示的页码，从1开始计数</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="recordCount">总记录数(ref)</param>
        /// <returns></returns>
        protected override List<SimpleThemeView> GetSimpleThemes(ThemesFilter filter, ThemeSortOption sort, int pageIndex, int pageSize, ref int recordCount)
        {
            List<SimpleThemeView> themes;
            string cacheKey = BuildCacheKey("GetSimpleThemes", filter.ToString(), sort.ToString(), pageIndex, pageSize);
            string cacheKeyOfRecordCount = BuildCacheKey("GetSimpleThemes", filter.ToString(), sort.ToString(), pageSize);
            if (_enableCache == false)
            {
                themes = base.GetSimpleThemes(filter, sort, pageIndex, pageSize, ref recordCount);
                _enableCache = true;
            }
            else
            {
                if (CacheHelper.Contains(cacheKeyOfRecordCount))
                {
                    recordCount = CacheHelper.Get<int>(cacheKeyOfRecordCount);
                }
                if (CacheHelper.Contains(cacheKey))
                {
                    themes = CacheHelper.Get<List<SimpleThemeView>>(cacheKey);
                }
                else
                {
                    themes = base.GetSimpleThemes(filter, sort, pageIndex, pageSize, ref recordCount);
                    CacheHelper.Set<List<SimpleThemeView>>(cacheKey, themes, _cacheTime);
                    CacheHelper.Set<int>(cacheKeyOfRecordCount, recordCount, _cacheTime);
                }
            }
            return themes;
        }

        /// <summary>
        /// 获取符何指定过滤器条件的主题列表
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <param name="sort">排序方式</param>
        /// <param name="dispayNumber">提取条数</param>
        /// <returns></returns>
        protected override List<FullThemeView> GetFullThemes(ThemesFilter filter, ThemeSortOption sort, int dispayNumber)
        {
            List<FullThemeView> themes;
            string cacheKey = BuildCacheKey("GetFullThemes", filter.ToString(), sort.ToString(), dispayNumber);
            if (_enableCache == false)
            {
                themes = base.GetFullThemes(filter, sort, dispayNumber);
                _enableCache = true;
            }
            else
            {
                if (CacheHelper.Contains(cacheKey))
                {
                    themes = CacheHelper.Get<List<FullThemeView>>(cacheKey);
                }
                else
                {
                    themes = base.GetFullThemes(filter, sort, dispayNumber);
                    CacheHelper.Set<List<FullThemeView>>(cacheKey, themes, _cacheTime);
                }
            }
            return themes;
        }

        /// <summary>
        /// 获取符何指定过滤器条件的主题列表
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <param name="sort">排序方式</param>
        /// <param name="pageIndex">显示的页码，从1开始计数</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="recordCount">总记录数(ref)</param>
        /// <returns></returns>
        protected override List<FullThemeView> GetFullThemes(ThemesFilter filter, ThemeSortOption sort, int pageIndex, int pageSize, ref int recordCount)
        {
            List<FullThemeView> themes;
            string cacheKey = BuildCacheKey("GetFullThemes", filter.ToString(), sort.ToString(), pageIndex, pageSize);
            string cacheKeyOfRecordCount = BuildCacheKey(filter.ToString(), sort.ToString(), pageSize);
            if (_enableCache == false)
            {
                themes = base.GetFullThemes(filter, sort, pageIndex, pageSize, ref recordCount);
                _enableCache = true;
            }
            else
            {
                if (CacheHelper.Contains(cacheKeyOfRecordCount))
                {
                    recordCount = CacheHelper.Get<int>(cacheKeyOfRecordCount);
                }
                if (CacheHelper.Contains(cacheKey))
                {
                    themes = CacheHelper.Get<List<FullThemeView>>(cacheKey);
                }
                else
                {
                    themes = base.GetFullThemes(filter, sort, pageIndex, pageSize, ref recordCount);
                    CacheHelper.Set<List<FullThemeView>>(cacheKey, themes, _cacheTime);
                    CacheHelper.Set<int>(cacheKeyOfRecordCount, recordCount, _cacheTime);
                }
            }
            return themes;
        }
        /// <summary>
        /// 获取随机主题
        /// </summary>
        /// <param name="seed">种子</param>
        /// <param name="categoryId">所属分类id</param>
        /// <param name="displayNumber">显示条数</param>
        /// <returns></returns>
        public override List<SimpleThemeView> GetRandomThemes(int seed, int categoryId, int displayNumber)
        {
            List<SimpleThemeView> themes;
            string cacheKey = BuildCacheKey("GetRandomThemes", seed, categoryId, displayNumber);
            if (_enableCache == false)
            {
                themes = base.GetRandomThemes(seed, categoryId, displayNumber);
                _enableCache = true;
            }
            else
            {
                if (CacheHelper.Contains(cacheKey))
                {
                    themes = CacheHelper.Get<List<SimpleThemeView>>(cacheKey);
                }
                else
                {
                    themes = base.GetRandomThemes(seed, categoryId, displayNumber);
                    CacheHelper.Set<List<SimpleThemeView>>(cacheKey, themes, _cacheTime);
                }
            }
            return themes;
        }
        /// <summary>
        /// 获取指定主题的标签列表
        /// </summary>
        /// <param name="themeId">主题id</param>
        /// <returns></returns>
        public override List<string> GetTagsByThemeId(int themeId)
        {
            List<string> tags;
            string cacheKey = BuildCacheKey("GetTagsByThemeId", themeId);
            if (_enableCache == false)
            {
                tags = base.GetTagsByThemeId(themeId);
                _enableCache = true;
            }
            else
            {
                if (CacheHelper.Contains(cacheKey))
                {
                    tags = CacheHelper.Get<List<string>>(cacheKey);
                }
                else
                {
                    tags = base.GetTagsByThemeId(themeId);
                    CacheHelper.Set<List<string>>(cacheKey, tags, _cacheTime);
                }
            }
            return tags;

        }
        /// <summary>
        /// 获取所有主题分类列表
        /// </summary>
        /// <returns></returns>
        public override List<ThemeCategory> GetThemeCategories()
        {
            List<ThemeCategory> categories;
            string cacheKey = BuildCacheKey("GetThemeCategories");
            if (_enableCache == false)
            {
                categories = base.GetThemeCategories();
                _enableCache = true;
            }
            else
            {
                if (CacheHelper.Contains(cacheKey))
                {
                    categories = CacheHelper.Get<List<ThemeCategory>>(cacheKey);
                }
                else
                {
                    categories = base.GetThemeCategories();
                    CacheHelper.Set<List<ThemeCategory>>(cacheKey, categories, _cacheTime);
                }
            }
            return categories;
        }
        #endregion


        /// <summary>
        /// 创建缓存的key
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private string BuildCacheKey(params object[] args)
        {
            StringBuilder sbCacheKey = new StringBuilder(this.GetType().FullName);
            foreach (object obj in args)
            {
                sbCacheKey.Append("_");
                sbCacheKey.Append(obj);
            }
            return sbCacheKey.ToString();
        }
    }
}
