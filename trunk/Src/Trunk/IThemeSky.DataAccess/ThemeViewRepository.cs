using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IThemeSky.Model;
using IThemeSky.Library.Data;
using System.Data;

namespace IThemeSky.DataAccess
{
    /// <summary>
    /// 主题视图数据操作类
    /// </summary>
    public class ThemeViewRepository : ThemeRepositoryBase,IThemeViewRepository
    {
        /// <summary>
        /// 默认的过滤器
        /// </summary>
        protected ThemesFilter _filter = new ThemesFilter();
        /// <summary>
        /// 初始化主题视图数据操作类
        /// </summary>
        /// <param name="connectionProvider">数据库连接串提供对象</param>
        public ThemeViewRepository(IConnectionProvider connectionProvider)
            : base(connectionProvider)
        { 
        }
        /// <summary>
        /// 初始化主题视图数据操作类
        /// </summary>
        /// <param name="connectionProvider">数据库连接串提供对象</param>
        /// <param name="checkState">默认审核状态</param>
        /// <param name="displayState">默认显示状态</param>
        public ThemeViewRepository(IConnectionProvider connectionProvider, CheckStateOption checkState, DisplayStateOption displayState)
            : base(connectionProvider)
        {
            _filter.CheckState = checkState;
            _filter.DisplayState = displayState;
        }

        #region IThemeViewRepository members

        /// <summary>
        /// 获取默认的过滤器
        /// </summary>
        public ThemesFilter Filter
        {
            get { return _filter; }
        }

        /// <summary>
        /// 根据主题id获取主题完整实体
        /// </summary>
        /// <param name="themeId">主题id</param>
        /// <returns></returns>
        public FullThemeView GetTheme(int themeId)
        {
            string cmdText = "SELECT TOP 1 * FROM " + GetDataViewName(_filter, ThemeSortOption.New) + " WHERE ThemeId=" + themeId + " AND " + _filter.ToString();
            using (IDataReader reader = SqlHelper.ExecuteReader(_connectionProvider.GetReadConnectionString(), CommandType.Text, cmdText))
            {
                if (reader.Read())
                {
                    return BindFullThemeView(reader);
                }
            }
            return null;
        }

        /// <summary>
        /// 根据指定主题的上一个主题id
        /// </summary>
        /// <param name="themeId">主题id</param>
        /// <returns></returns>
        public int GetPrevThemeId(int themeId)
        {
            string cmdText = "SELECT TOP 1 ThemeId FROM " + GetDataViewName(_filter, ThemeSortOption.New) + " WHERE ThemeId < " + themeId + " AND " + _filter.ToString() + " ORDER BY ThemeId DESC";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(_connectionProvider.GetReadConnectionString(), CommandType.Text, cmdText));
        }

        /// <summary>
        /// 根据指定主题的下一个主题id
        /// </summary>
        /// <param name="themeId">主题id</param>
        /// <returns></returns>
        public int GetNextThemeId(int themeId)
        {
            string cmdText = "SELECT TOP 1 ThemeId FROM " + GetDataViewName(_filter, ThemeSortOption.New) + " WHERE ThemeId > " + themeId + " AND " + _filter.ToString() + " ORDER BY ThemeId ASC";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(_connectionProvider.GetReadConnectionString(), CommandType.Text, cmdText));
        }

        /// <summary>
        /// 获取主题列表
        /// </summary>
        /// <param name="sort">排序方式</param>
        /// <param name="displayNumber">显示条数</param>
        /// <returns></returns>
        public List<SimpleThemeView> GetThemes(ThemeSortOption sort, int displayNumber)
        {
            return GetSimpleThemes(_filter, sort, displayNumber);
        }

        /// <summary>
        /// 获取主题列表
        /// </summary>
        /// <param name="sort">排序方式</param>
        /// <param name="pageIndex">显示的页码，从1开始计数</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="recordCount">总记录数(ref)</param>
        /// <returns></returns>
        public List<SimpleThemeView> GetThemes(ThemeSortOption sort, int pageIndex, int pageSize, ref int recordCount)
        {
            return GetSimpleThemes(_filter, sort, pageIndex, pageSize, ref recordCount);
        }

        /// <summary>
        /// 根据分类id获取主题列表
        /// </summary>
        /// <param name="categoryId">分类id</param>
        /// <param name="sort">排序方式</param>
        /// <param name="displayNumber">显示条数</param>
        /// <returns></returns>
        public List<SimpleThemeView> GetThemesByCategoryId(int categoryId, ThemeSortOption sort, int displayNumber)
        {
            ThemesFilter filter = _filter.Clone();
            filter.CategoryIds.Add(categoryId);
            return GetSimpleThemes(_filter, sort, displayNumber);
        }

        /// <summary>
        /// 根据分类id获取主题列表
        /// </summary>
        /// <param name="categoryId">分类id</param>
        /// <param name="sort">排序方式</param>
        /// <param name="pageIndex">显示的页码，从1开始计数</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="recordCount">总记录数(ref)</param>
        /// <returns></returns>
        public List<SimpleThemeView> GetThemesByCategoryId(int categoryId, ThemeSortOption sort, int pageIndex, int pageSize, ref int recordCount)
        {
            ThemesFilter filter = _filter.Clone();
            filter.CategoryIds.Add(categoryId);
            return GetSimpleThemes(_filter, sort, pageIndex, pageSize, ref recordCount);
        }

        /// <summary>
        /// 根据分类id获取主题列表
        /// </summary>
        /// <param name="parentCategoryId">父级分类id</param>
        /// <param name="sort">排序方式</param>
        /// <param name="displayNumber">显示条数</param>
        /// <returns></returns>
        public List<SimpleThemeView> GetThemesByParentCategoryId(int parentCategoryId, ThemeSortOption sort, int displayNumber)
        {
            ThemesFilter filter = _filter.Clone();
            filter.ParentCategoryIds.Add(parentCategoryId);
            return GetSimpleThemes(_filter, sort, displayNumber);
        }

        /// <summary>
        /// 根据分类id获取主题列表
        /// </summary>
        /// <param name="parentCategoryId">父级分类id</param>
        /// <param name="sort">排序方式</param>
        /// <param name="pageIndex">显示的页码，从1开始计数</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="recordCount">总记录数(ref)</param>
        /// <returns></returns>
        public List<SimpleThemeView> GetThemesByParentCategoryId(int parentCategoryId, ThemeSortOption sort, int pageIndex, int pageSize, ref int recordCount)
        {
            ThemesFilter filter = _filter.Clone();
            filter.ParentCategoryIds.Add(parentCategoryId);
            return GetSimpleThemes(_filter, sort, pageIndex, pageSize, ref recordCount);
        }

        /// <summary>
        /// 根据多个主题id获取主题列表
        /// </summary>
        /// <param name="themeIds">主题id列表</param>
        /// <returns></returns>
        public List<SimpleThemeView> GetThemesByIds(List<int> themeIds)
        {
            List<SimpleThemeView> themes = new List<SimpleThemeView>();
            string cmdText = "SELECT " + SIMPLE_THEME_FIELDS + " FROM " + GetDataViewName(_filter, ThemeSortOption.New) + " WHERE 1=1" + SqlConditionBuilder.GetMultiQueryValues("ThemeId", themeIds);
            using (IDataReader reader = SqlHelper.ExecuteReader(_connectionProvider.GetReadConnectionString(), CommandType.Text, cmdText))
            {
                if (reader.Read())
                {
                    themes.Add(BindSimpleThemeView(reader));
                }
            }
            return themes;
        }

        /// <summary>
        /// 根据过滤器获取所有主题
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <param name="sort">排序方式</param>
        /// <param name="displayNumber">显示条数</param>
        /// <returns></returns>
        public List<SimpleThemeView> GetThemesByFilter(ThemesFilter filter, ThemeSortOption sort, int displayNumber)
        {
            return GetSimpleThemes(filter, sort, displayNumber);
        }

        /// <summary>
        /// 根据过滤器获取所有主题
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <param name="sort">排序方式</param>
        /// <param name="pageIndex">显示的页码，从1开始计数</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="recordCount">总记录数(ref)</param>
        /// <returns></returns>
        public List<SimpleThemeView> GetThemesByFilter(ThemesFilter filter, ThemeSortOption sort, int pageIndex, int pageSize, ref int recordCount)
        {
            return GetSimpleThemes(filter, sort, pageIndex, pageSize, ref recordCount);
        }

        /// <summary>
        /// 搜索主题
        /// </summary>
        /// <param name="keyword">主题名称关键字</param>
        /// <param name="sort">排序方式</param>
        /// <param name="pageIndex">显示的页码，从1开始计数</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="recordCount">总记录数(ref)</param>
        /// <returns></returns>
        public List<SimpleThemeView> SearchThemes(string keyword, ThemeSortOption sort, int pageIndex, int pageSize, ref int recordCount)
        {
            ThemesFilter filter = _filter.Clone();
            filter.SearchKeyword = keyword;
            return GetSimpleThemes(_filter, sort, pageIndex, pageSize, ref recordCount);
        }

        /// <summary>
        /// 根据过滤器获取所有主题(完整实体)
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <param name="sort">排序方式</param>
        /// <param name="pageIndex">显示的页码，从1开始计数</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="recordCount">总记录数(ref)</param>
        /// <returns></returns>
        public List<FullThemeView> GetFullThemesByFilter(ThemesFilter filter, ThemeSortOption sort, int pageIndex, int pageSize, ref int recordCount)
        {
            return GetFullThemes(filter, sort, pageIndex, pageSize, ref recordCount);
        }        #endregion
    }
}
