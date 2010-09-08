using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IThemeSky.Model;
using IThemeSky.Library.Data;
using System.Data;
using System.Data.SqlClient;

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
        public virtual FullThemeView GetTheme(int themeId)
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
        /// <param name="categoryId">所属分类id</param>
        /// <param name="themeId">主题id</param>
        /// <param name="themeName">主题名称(out)</param>
        /// <returns></returns>
        public virtual int GetPrevThemeId(int categoryId, int themeId, out string themeName)
        {
            ThemesFilter filter = _filter.Clone();
            if (categoryId > 0)
            {
                filter.CategoryIds.Add(categoryId);
            }
            int prevThemeId = 0;
            themeName = "";
            string cmdText = "SELECT TOP 1 ThemeId,Title FROM " + GetDataViewName(_filter, ThemeSortOption.New) + " WHERE ThemeId < " + themeId + " AND " + filter.ToString() + " ORDER BY ThemeId DESC";
            using (IDataReader reader = SqlHelper.ExecuteReader(_connectionProvider.GetReadConnectionString(), CommandType.Text, cmdText))
            {
                if (reader.Read())
                {
                    prevThemeId = Convert.ToInt32(reader["ThemeId"]);
                    themeName = reader["Title"].ToString();
                }
            }
            return prevThemeId;
        }

        /// <summary>
        /// 根据指定主题的下一个主题id
        /// </summary>
        /// <param name="categoryId">所属分类id</param>
        /// <param name="themeId">主题id</param>
        /// <param name="themeName">主题名称(out)</param>
        /// <returns></returns>
        public virtual int GetNextThemeId(int categoryId, int themeId, out string themeName)
        {
            ThemesFilter filter = _filter.Clone();
            if (categoryId > 0)
            {
                filter.CategoryIds.Add(categoryId);
            }
            int nextThemeId = 0;
            themeName = "";
            string cmdText = "SELECT TOP 1 ThemeId,Title FROM " + GetDataViewName(_filter, ThemeSortOption.New) + " WHERE ThemeId > " + themeId + " AND " + filter.ToString() + " ORDER BY ThemeId ASC";
            using (IDataReader reader = SqlHelper.ExecuteReader(_connectionProvider.GetReadConnectionString(), CommandType.Text, cmdText))
            {
                if (reader.Read())
                {
                    nextThemeId = Convert.ToInt32(reader["ThemeId"]);
                    themeName = reader["Title"].ToString();
                }
            }
            return nextThemeId;
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
            return GetSimpleThemes(filter, sort, displayNumber);
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
            return GetSimpleThemes(filter, sort, pageIndex, pageSize, ref recordCount);
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
            return GetSimpleThemes(filter, sort, displayNumber);
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
            return GetSimpleThemes(filter, sort, pageIndex, pageSize, ref recordCount);
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
            return GetSimpleThemes(filter, sort, pageIndex, pageSize, ref recordCount);
        }

        /// <summary>
        /// 获取随机主题
        /// </summary>
        /// <param name="seed">种子</param>
        /// <param name="categoryId">所属分类id</param>
        /// <param name="displayNumber">显示条数</param>
        /// <returns></returns>
        public virtual List<SimpleThemeView> GetRandomThemes(int seed, int categoryId, int displayNumber)
        {
            ThemesFilter filter = _filter.Clone();
            if (categoryId > 0)
            {
                filter.CategoryIds.Add(categoryId);
            }
            string searchCondition = filter.ToString() + " AND CommendIndex>=4";
            string cmdText = "SELECT TOP " + displayNumber + " * FROM (SELECT NEWID() rndNumber," + SIMPLE_THEME_FIELDS + " FROM " + GetDataViewName(filter, ThemeSortOption.Default) + " WHERE " + searchCondition + ") t ORDER BY rndNumber";
            List<SimpleThemeView> themes = new List<SimpleThemeView>();
            using (IDataReader reader = SqlHelper.ExecuteReader(_connectionProvider.GetReadConnectionString(), CommandType.Text, cmdText))
            {
                while (reader.Read())
                {
                    themes.Add(BindSimpleThemeView(reader));
                }
            }
            return themes; 
        }

        /// <summary>
        /// 根据多种组合的标签名称获取主题列表
        /// </summary>
        /// <param name="tags">组合的标签列表</param>
        /// <param name="filter">过滤器</param>
        /// <param name="sort">排序方式</param>
        /// <param name="pageIndex">显示的页码，从1开始计数</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="recordCount">总记录数(ref)</param>
        /// <returns></returns>
        public List<SimpleThemeView> GetThemesByMultiTags(List<List<string>> tags, ThemesFilter filter, ThemeSortOption sort, int pageIndex, int pageSize, ref int recordCount)
        {
            if (tags.Count > 0)
            {
                StringBuilder sbInnerSql = new StringBuilder();
                for (int i = 0; i < tags.Count; i++)
                {
                    if (i > 0)
                    {
                        sbInnerSql.Append(" INNER JOIN ");
                    }
                    sbInnerSql.AppendFormat("(SELECT ThemeId FROM dbo.View_TagTheme WHERE 1=1 {0} GROUP BY ThemeId) T{1}", SqlConditionBuilder.GetMultiQueryValues("TagName", tags[i]), i+1);
                    if (i > 0)
                    {
                        sbInnerSql.AppendFormat(" ON T{0}.ThemeId = T{1}.ThemeId", i, i+1);
                    }
                }
                string searchCondition = string.Format("{0} AND ThemeId in (SELECT T1.ThemeId FROM {1})", filter.ToString(), sbInnerSql.ToString());
                SqlParameter[] parameters = new SqlParameter[] 
			    {
				    SqlParameterHelper.BuildParameter("@RecordNum",SqlDbType.Int, 4, ParameterDirection.InputOutput, recordCount),
				    SqlParameterHelper.BuildInputParameter("@SelectList",SqlDbType.VarChar,2000, SIMPLE_THEME_FIELDS),
				    SqlParameterHelper.BuildInputParameter("@TableSource",SqlDbType.VarChar,300, "View_Theme"),
				    SqlParameterHelper.BuildInputParameter("@SearchCondition",SqlDbType.VarChar,2000, searchCondition),
				    SqlParameterHelper.BuildInputParameter("@OrderExpression",SqlDbType.VarChar,1000, GetSortExpression(sort)),
				    SqlParameterHelper.BuildInputParameter("@PageSize",SqlDbType.Int,4,pageSize),
				    SqlParameterHelper.BuildInputParameter("@PageIndex",SqlDbType.Int,4,pageIndex)
			    };
                List<SimpleThemeView> themes = new List<SimpleThemeView>();
                using (IDataReader dataReader = SqlHelper.ExecuteReader(_connectionProvider.GetReadConnectionString(), CommandType.StoredProcedure, "PR_GetDataByPageIndex", parameters))
                {
                    while (dataReader.Read())
                    {
                        themes.Add(BindSimpleThemeView(dataReader));
                    }
                }
                recordCount = Convert.ToInt32(parameters[0].Value);
                return themes;
            }
            return new List<SimpleThemeView>();
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
        }
        /// <summary>
        /// 获取指定主题的标签列表
        /// </summary>
        /// <param name="themeId">主题id</param>
        /// <returns></returns>
        public virtual List<string> GetTagsByThemeId(int themeId)
        {
            List<string> tags = new List<string>();
            string cmdText = "SELECT TagName FROM ThemeTagMap INNER JOIN ThemeTag ON ThemeTagMap.TagId = ThemeTag.TagId WHERE ThemeId=" + themeId;
            using (IDataReader reader = SqlHelper.ExecuteReader(_connectionProvider.GetReadConnectionString(), CommandType.Text, cmdText))
            {
                while (reader.Read())
                {
                    tags.Add(reader["TagName"].ToString());
                }
            }
            return tags;
        }
        /// <summary>
        /// 获取所有主题分类列表
        /// </summary>
        /// <returns></returns>
        public virtual List<ThemeCategory> GetThemeCategories()
        {
            List<ThemeCategory> categories = new List<ThemeCategory>();
            string cmdText = "SELECT * FROM ThemeCategory ORDER BY SortNumber DESC";
            using (IDataReader reader = SqlHelper.ExecuteReader(_connectionProvider.GetReadConnectionString(), CommandType.Text, cmdText))
            {
                while (reader.Read())
                {
                    categories.Add(BindThemeCategory(reader));                }            }
            return categories;        }        #endregion
    }
}
