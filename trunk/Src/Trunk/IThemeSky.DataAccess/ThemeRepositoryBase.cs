using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IThemeSky.Model;
using IThemeSky.Library.Extensions;
using System.Data;
using IThemeSky.Library.Data;
using System.Data.SqlClient;

namespace IThemeSky.DataAccess
{
    public abstract class ThemeRepositoryBase
    {
        /// <summary>
        /// 简单实体对应的查询字段列表
        /// </summary>
        protected const string SIMPLE_THEME_FIELDS = "ThemeId,CategoryId,CategoryName,Title,Thumbnail,CommendIndex,Downloads";
        /// <summary>
        /// 数据库连接字符串提供对象
        /// </summary>
        protected IConnectionProvider _connectionProvider = null;

        public ThemeRepositoryBase(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        #region 通用的主题列表获取方法

        /// <summary>
        /// 获取符何指定过滤器条件的主题列表
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <param name="sort">排序方式</param>
        /// <param name="dispayNumber">提取条数</param>
        /// <returns></returns>
        protected virtual List<SimpleThemeView> GetSimpleThemes(ThemesFilter filter, ThemeSortOption sort, int dispayNumber)
        {
            List<SimpleThemeView> themes = new List<SimpleThemeView>();
            string sortExpression = GetSortExpression(sort);
            string cmdText = string.Format("SELECT top {3} {4} FROM {0} Where {1} {2}"
                , GetDataViewName(filter, sort)
                , filter.ToString()
                , sortExpression.Length > 0 ? " ORDER BY " + sortExpression : ""
                , dispayNumber
                , SIMPLE_THEME_FIELDS
                );
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
        /// 获取符何指定过滤器条件的主题列表
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <param name="sort">排序方式</param>
        /// <param name="pageIndex">显示的页码，从1开始计数</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="recordCount">总记录数(ref)</param>
        /// <returns></returns>
        protected virtual List<SimpleThemeView> GetSimpleThemes(ThemesFilter filter, ThemeSortOption sort, int pageIndex, int pageSize, ref int recordCount)
        {
            SqlParameter[] parameters = new SqlParameter[] 
			{
				SqlParameterHelper.BuildParameter("@RecordNum",SqlDbType.Int, 4, ParameterDirection.InputOutput, recordCount),
				SqlParameterHelper.BuildInputParameter("@SelectList",SqlDbType.VarChar,2000, SIMPLE_THEME_FIELDS),
				SqlParameterHelper.BuildInputParameter("@TableSource",SqlDbType.VarChar,300, GetDataViewName(filter, sort)),
				SqlParameterHelper.BuildInputParameter("@SearchCondition",SqlDbType.VarChar,2000, filter.ToString()),
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

        /// <summary>
        /// 获取符何指定过滤器条件的主题列表
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <param name="sort">排序方式</param>
        /// <param name="dispayNumber">提取条数</param>
        /// <returns></returns>
        protected virtual List<FullThemeView> GetFullThemes(ThemesFilter filter, ThemeSortOption sort, int dispayNumber)
        {
            List<FullThemeView> themes = new List<FullThemeView>();
            string sortExpression = GetSortExpression(sort);
            string cmdText = string.Format("SELECT top {3} * FROM {0} Where {1} {2}"
                , GetDataViewName(filter, sort)
                , filter.ToString()
                , sortExpression.Length > 0 ? " ORDER BY " + sortExpression : ""
                , dispayNumber
                );
            using (IDataReader reader = SqlHelper.ExecuteReader(_connectionProvider.GetReadConnectionString(), CommandType.Text, cmdText))
            {
                while (reader.Read())
                {
                    themes.Add(BindFullThemeView(reader));
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
        protected virtual List<FullThemeView> GetFullThemes(ThemesFilter filter, ThemeSortOption sort, int pageIndex, int pageSize, ref int recordCount)
        {
            SqlParameter[] parameters = new SqlParameter[] 
			{
				SqlParameterHelper.BuildParameter("@RecordNum",SqlDbType.Int, 4, ParameterDirection.InputOutput, recordCount),
				SqlParameterHelper.BuildInputParameter("@SelectList",SqlDbType.VarChar,2000, "*"),
				SqlParameterHelper.BuildInputParameter("@TableSource",SqlDbType.VarChar,300, GetDataViewName(filter, sort)),
				SqlParameterHelper.BuildInputParameter("@SearchCondition",SqlDbType.VarChar,2000, filter.ToString()),
				SqlParameterHelper.BuildInputParameter("@OrderExpression",SqlDbType.VarChar,1000, GetSortExpression(sort)),
				SqlParameterHelper.BuildInputParameter("@PageSize",SqlDbType.Int,4,pageSize),
				SqlParameterHelper.BuildInputParameter("@PageIndex",SqlDbType.Int,4,pageIndex)
			};
            List<FullThemeView> themes = new List<FullThemeView>();
            using (IDataReader dataReader = SqlHelper.ExecuteReader(_connectionProvider.GetReadConnectionString(), CommandType.StoredProcedure, "PR_GetDataByPageIndex", parameters))
            {
                while (dataReader.Read())
                {
                    themes.Add(BindFullThemeView(dataReader));
                }
            }
            recordCount = Convert.ToInt32(parameters[0].Value);
            return themes;
        }

        /// <summary>
        /// 根据过滤器和排序条件获取对应的视图名称
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        protected virtual string GetDataViewName(ThemesFilter filter, ThemeSortOption sort)
        {
            if (filter.TagIds.Count > 0 || sort == ThemeSortOption.TagSort)
            {
                return "View_TagTheme";
            }
            return "View_Theme";
        }

        /// <summary>
        /// 获取排序子句，不带"Order By"前缀
        /// </summary>
        /// <param name="sort"></param>
        /// <returns></returns>
        protected virtual string GetSortExpression(ThemeSortOption sort)
        {
            switch (sort)
            {
                case ThemeSortOption.New:
                    return "AddTime DESC";
                case ThemeSortOption.Popular:
                    return "Downloads DESC";
                case ThemeSortOption.Rank_Of_Month:
                    return "LastMonthDownloads DESC";
                case ThemeSortOption.Rank_Of_Week:
                    return "LastWeekDownloads DESC";
                case ThemeSortOption.TagSort:
                    return "TagSortNumber DESC";
                default:
                    return "ThemeId DESC";
            }
        }
        #endregion

        #region 实体绑定
        /// <summary>
        /// 绑定主题完整实体
        /// </summary>
        protected virtual Theme BindTheme(IDataReader dataReader)
        {
            return new Theme()
            {
                ThemeId = Convert.ToInt32(dataReader["ThemeId"]),
                CategoryId = Convert.ToInt32(dataReader["CategoryId"]),
                ParentCategoryId = Convert.ToInt32(dataReader["ParentCategoryId"]),
                Title = dataReader["Title"].ToString(),
                FileSize = Convert.ToInt64(dataReader["FileSize"]),
                Description = dataReader["Description"].ToString(),
                DisplayState = Convert.ToInt32(dataReader["DisplayState"]).ToEnum<DisplayStateOption>(DisplayStateOption.Hidden),
                CheckState = Convert.ToInt32(dataReader["CheckState"]).ToEnum<CheckStateOption>(CheckStateOption.Waitting),
                AuthorId = Convert.ToInt32(dataReader["AuthorId"]),
                CheckerId = Convert.ToInt32(dataReader["CheckerId"]),
                CommendIndex = Convert.ToInt32(dataReader["CommendIndex"]),
                ThumbnailName = dataReader["ThumbnailName"].ToString(),
                AddTime = Convert.ToDateTime(dataReader["AddTime"]),
                UpdateTime = Convert.ToDateTime(dataReader["UpdateTime"]),
                GoodComments = Convert.ToInt32(dataReader["GoodComments"]),
                BadComments = Convert.ToInt32(dataReader["BadComments"]),
                Comments = Convert.ToInt32(dataReader["Comments"]),
                Downloads = Convert.ToInt32(dataReader["Downloads"]),
                Views = Convert.ToInt32(dataReader["Views"]),
                LastWeekDownloads = Convert.ToInt32(dataReader["LastWeekDownloads"]),
                LastMonthDownloads = Convert.ToInt32(dataReader["LastMonthDownloads"]),
                Source = Convert.ToInt32(dataReader["Source"]).ToEnum<SourceOption>(SourceOption.IThemeSky),
            };
        }
        /// <summary>
        /// 绑定简单主题视图
        /// </summary>
        protected virtual SimpleThemeView BindSimpleThemeView(IDataReader dataReader)
        {
            return new SimpleThemeView()
            {
                ThemeId = Convert.ToInt32(dataReader["ThemeId"]),
                CategoryId = Convert.ToInt32(dataReader["CategoryId"]),
                Title = dataReader["Title"].ToString(),
                CommendIndex = Convert.ToInt32(dataReader["CommendIndex"]),
                Downloads = Convert.ToInt32(dataReader["Downloads"]),
                CategoryName = dataReader["CategoryName"].ToString(),
            };
        }

        /// <summary>
        /// 绑定主题完整视图
        /// </summary>
        protected virtual FullThemeView BindFullThemeView(IDataReader dataReader)
        {
            FullThemeView fullTheme = BindTheme(dataReader) as FullThemeView;
            fullTheme.CategoryName = dataReader["CategoryName"].ToString();
            return fullTheme;
        }
        #endregion
    }
}
