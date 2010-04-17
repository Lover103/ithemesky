using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using IThemeSky.Library.Data;
using System.Data;
using IThemeSky.Library.Extensions;
using IThemeSky.Model;

namespace IThemeSky.DataAccess
{
    /// <summary>
    /// 主题的管理数据访问类
    /// </summary>
    public class ThemeManageRepository : ThemeRepositoryBase, IThemeManageRepository
    {
        /// <summary>
        /// 初始化主题的管理数据访问类
        /// </summary>
        /// <param name="connection"></param>
        public ThemeManageRepository(IConnectionProvider connection)
            : base(connection)
        {

        }
        #region IThemeManageRepository members
        /// <summary>
        /// 获取一个主题实体
        /// </summary>
        /// <param name="themeId">主题id</param>
        public Theme GetTheme(int themeId)
        {
            string cmdText = "select * from Theme where ThemeId=@ThemeId ";
            SqlParameter[] parameters = new SqlParameter[]
			{
				SqlParameterHelper.BuildInputParameter("@ThemeId",SqlDbType.Int, 4, themeId)
			};
            Theme model = null;
            using (IDataReader dataReader = SqlHelper.ExecuteReader(_connectionProvider.GetReadConnectionString(), CommandType.Text, cmdText, parameters))
            {
                if (dataReader.Read())
                {
                    model = BindTheme(dataReader);
                }
            }
            return model;
        }
        /// <summary>
        /// 添加新的主题
        /// </summary>
        /// <param name="theme">主题对象</param>
        /// <returns></returns>
        public bool AddTheme(Theme theme)
        {
            SqlParameter[] parameters = new SqlParameter[]
			{
				SqlParameterHelper.BuildInputParameter("@CategoryId",SqlDbType.Int, 4, theme.CategoryId),
				SqlParameterHelper.BuildInputParameter("@ParentCategoryId",SqlDbType.Int, 4, theme.ParentCategoryId),
				SqlParameterHelper.BuildInputParameter("@Title",SqlDbType.NVarChar, 300, theme.Title),
				SqlParameterHelper.BuildInputParameter("@FileSize",SqlDbType.BigInt, 8, theme.FileSize),
				SqlParameterHelper.BuildInputParameter("@Description",SqlDbType.NVarChar, 1000, theme.Description),
				SqlParameterHelper.BuildInputParameter("@DisplayState",SqlDbType.SmallInt, 2, theme.DisplayState.ToInt32()),
				SqlParameterHelper.BuildInputParameter("@CheckState",SqlDbType.SmallInt, 2, theme.CheckState.ToInt32()),
				SqlParameterHelper.BuildInputParameter("@AuthorId",SqlDbType.Int, 4, theme.AuthorId),
				SqlParameterHelper.BuildInputParameter("@CheckerId",SqlDbType.Int, 4, theme.CheckerId),
				SqlParameterHelper.BuildInputParameter("@CommendIndex",SqlDbType.Int, 4, theme.CommendIndex),
				SqlParameterHelper.BuildInputParameter("@ThumbnailName",SqlDbType.NVarChar, 100, theme.ThumbnailName),
				SqlParameterHelper.BuildInputParameter("@AddTime",SqlDbType.DateTime, 8, theme.AddTime),
				SqlParameterHelper.BuildInputParameter("@UpdateTime",SqlDbType.DateTime, 8, theme.UpdateTime),
				SqlParameterHelper.BuildInputParameter("@RateScore",SqlDbType.Int, 4, theme.RateScore),
				SqlParameterHelper.BuildInputParameter("@RateNumbers",SqlDbType.Int, 4, theme.RateNumbers),
				SqlParameterHelper.BuildInputParameter("@Comments",SqlDbType.Int, 4, theme.Comments),
				SqlParameterHelper.BuildInputParameter("@Downloads",SqlDbType.Int, 4, theme.Downloads),
				SqlParameterHelper.BuildInputParameter("@Views",SqlDbType.Int, 4, theme.Views),
				SqlParameterHelper.BuildInputParameter("@LastWeekDownloads",SqlDbType.Int, 4, theme.LastWeekDownloads),
				SqlParameterHelper.BuildInputParameter("@LastMonthDownloads",SqlDbType.Int, 4, theme.LastMonthDownloads),
				SqlParameterHelper.BuildInputParameter("@Source",SqlDbType.Int, 4, theme.Source.ToInt32()),
                SqlParameterHelper.BuildInputParameter("@DownloadUrl", SqlDbType.NVarChar, 300, theme.DownloadUrl),
                SqlParameterHelper.BuildInputParameter("@AuthorName", SqlDbType.NVarChar, 128, theme.AuthorName),
                SqlParameterHelper.BuildInputParameter("@AuthorMail", SqlDbType.NVarChar, 128, theme.AuthorMail),
			};
            string cmdText = @"
                INSERT INTO Theme
				    (CategoryId,ParentCategoryId,Title,FileSize,Description,DisplayState,CheckState,AuthorId,CheckerId,CommendIndex,ThumbnailName,AddTime,UpdateTime,RateScore,RateNumbers,Comments,Downloads,Views,LastWeekDownloads,LastMonthDownloads,Source,DownloadUrl,AuthorName,AuthorMail)
			    VALUES
				    (@CategoryId,@ParentCategoryId,@Title,@FileSize,@Description,@DisplayState,@CheckState,@AuthorId,@CheckerId,@CommendIndex,@ThumbnailName,@AddTime,@UpdateTime,@RateScore,@RateNumbers,@Comments,@Downloads,@Views,@LastWeekDownloads,@LastMonthDownloads,@Source,@DownloadUrl,@AuthorName,@AuthorMail);SELECT @@IDENTITY";
            int themeId = Convert.ToInt32(SqlHelper.ExecuteScalar(_connectionProvider.GetWriteConnectionString(), CommandType.Text, cmdText, parameters));
            if (themeId > 0)
            {
                theme.ThemeId = themeId;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="theme">主题实体</param>
        public bool UpdateTheme(Theme theme)
        {
            string cmdText = @"
			update Theme set
				CategoryId=@CategoryId,
				ParentCategoryId=@ParentCategoryId,
				Title=@Title,
				FileSize=@FileSize,
				Description=@Description,
				DisplayState=@DisplayState,
				CheckState=@CheckState,
				AuthorId=@AuthorId,
				CheckerId=@CheckerId,
				CommendIndex=@CommendIndex,
				ThumbnailName=@ThumbnailName,
				AddTime=@AddTime,
				UpdateTime=@UpdateTime,
				RateScore=@RateScore,
				RateNumbers=@RateNumbers,
				Comments=@Comments,
				Downloads=@Downloads,
				Views=@Views,
				LastWeekDownloads=@LastWeekDownloads,
				LastMonthDownloads=@LastMonthDownloads,
				Source=@Source,
				DownloadUrl=@DownloadUrl,
                AuthorName=@AuthorName,
                AuthorMail=@AuthorMail
			where 
				ThemeId=@ThemeId 
			";
            SqlParameter[] parameters = new SqlParameter[]
			{
				SqlParameterHelper.BuildInputParameter("@ThemeId", SqlDbType.Int, 4, theme.ThemeId),
				SqlParameterHelper.BuildInputParameter("@CategoryId", SqlDbType.Int, 4, theme.CategoryId),
				SqlParameterHelper.BuildInputParameter("@ParentCategoryId", SqlDbType.Int, 4, theme.ParentCategoryId),
				SqlParameterHelper.BuildInputParameter("@Title", SqlDbType.NVarChar, 300, theme.Title),
				SqlParameterHelper.BuildInputParameter("@FileSize", SqlDbType.BigInt, 8, theme.FileSize),
				SqlParameterHelper.BuildInputParameter("@Description", SqlDbType.NVarChar, 1000, theme.Description),
				SqlParameterHelper.BuildInputParameter("@DisplayState", SqlDbType.SmallInt, 2, theme.DisplayState),
				SqlParameterHelper.BuildInputParameter("@CheckState", SqlDbType.SmallInt, 2, theme.CheckState),
				SqlParameterHelper.BuildInputParameter("@AuthorId", SqlDbType.Int, 4, theme.AuthorId),
				SqlParameterHelper.BuildInputParameter("@CheckerId", SqlDbType.Int, 4, theme.CheckerId),
				SqlParameterHelper.BuildInputParameter("@CommendIndex", SqlDbType.Int, 4, theme.CommendIndex),
				SqlParameterHelper.BuildInputParameter("@ThumbnailName", SqlDbType.NVarChar, 100, theme.ThumbnailName),
				SqlParameterHelper.BuildInputParameter("@AddTime", SqlDbType.DateTime, 8, theme.AddTime),
				SqlParameterHelper.BuildInputParameter("@UpdateTime", SqlDbType.DateTime, 8, theme.UpdateTime),
				SqlParameterHelper.BuildInputParameter("@RateScore", SqlDbType.Int, 4, theme.RateScore),
				SqlParameterHelper.BuildInputParameter("@RateNumbers", SqlDbType.Int, 4, theme.RateNumbers),
				SqlParameterHelper.BuildInputParameter("@Comments", SqlDbType.Int, 4, theme.Comments),
				SqlParameterHelper.BuildInputParameter("@Downloads", SqlDbType.Int, 4, theme.Downloads),
				SqlParameterHelper.BuildInputParameter("@Views", SqlDbType.Int, 4, theme.Views),
				SqlParameterHelper.BuildInputParameter("@LastWeekDownloads", SqlDbType.Int, 4, theme.LastWeekDownloads),
				SqlParameterHelper.BuildInputParameter("@LastMonthDownloads", SqlDbType.Int, 4, theme.LastMonthDownloads),
				SqlParameterHelper.BuildInputParameter("@Source", SqlDbType.Int, 4, theme.Source),
				SqlParameterHelper.BuildInputParameter("@DownloadUrl", SqlDbType.NVarChar, 300, theme.DownloadUrl),
                SqlParameterHelper.BuildInputParameter("@AuthorName", SqlDbType.NVarChar, 128, theme.AuthorName),
                SqlParameterHelper.BuildInputParameter("@AuthorMail", SqlDbType.NVarChar, 128, theme.AuthorMail),
			};
            return SqlHelper.ExecuteNonQuery(_connectionProvider.GetWriteConnectionString(), CommandType.Text, cmdText, parameters) > 0;
        }

        /// <summary>
        /// 为指定的主题添加下载地址
        /// </summary>
        /// <param name="themeId"></param>
        /// <param name="url"></param>
        /// <param name="isDefault"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public bool AddThemeDownloadUrl(int themeId, string url, bool isDefault, SourceOption source)
        {
            string cmdText = @"
			insert into ThemeFiles
				(ThemeId,IsDefault,Url,Source,AddTime)
			values
				(@ThemeId,@IsDefault,@Url,@Source,@AddTime)
			";
            SqlParameter[] parameters = new SqlParameter[]
			{
				SqlParameterHelper.BuildInputParameter("@ThemeId",SqlDbType.Int, 4, themeId),
				SqlParameterHelper.BuildInputParameter("@IsDefault",SqlDbType.SmallInt, 2, isDefault ? 1 : 0),
				SqlParameterHelper.BuildInputParameter("@Url",SqlDbType.NVarChar, 500, url),
				SqlParameterHelper.BuildInputParameter("@Source",SqlDbType.SmallInt, 2, source),
				SqlParameterHelper.BuildInputParameter("@AddTime",SqlDbType.DateTime, 8, DateTime.Now)
			};
            return SqlHelper.ExecuteNonQuery(_connectionProvider.GetWriteConnectionString(), CommandType.Text, cmdText, parameters) > 0;
        }

        /// <summary>
        /// 维护主题与标签的映射关系
        /// </summary>
        /// <param name="themeId"></param>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public bool MappingThemeTag(int themeId, string tagName)
        {
            string cmdText = @"
                IF EXISTS (SELECT 1 FROM ThemeTag WHERE TagName=@TagName)
                    SELECT TagId FROM ThemeTag WHERE TagName=@TagName
                ELSE
                    INSERT INTO ThemeTag (TagName) VALUES (@TagName);SELECT @@IDENTITY;
            ";
            SqlParameter[] parameters = new SqlParameter[]
			{
				SqlParameterHelper.BuildInputParameter("@TagName", SqlDbType.NVarChar, 40, tagName),
			};
            int tagId = Convert.ToInt32(SqlHelper.ExecuteScalar(_connectionProvider.GetWriteConnectionString(), CommandType.Text, cmdText, parameters));

            //添加映射关系
            cmdText = @"
                IF NOT EXISTS (SELECT 1 FROM ThemeTagMap WHERE ThemeId=@ThemeId AND TagId=@TagId)
                    INSERT INTO ThemeTagMap (ThemeId,TagId) VALUES (@ThemeId,@TagId)
            ";
            parameters = new SqlParameter[]
			{
				SqlParameterHelper.BuildInputParameter("@ThemeId",SqlDbType.Int, 4, themeId),
				SqlParameterHelper.BuildInputParameter("@TagId",SqlDbType.Int, 4, tagId),
			};
            SqlHelper.ExecuteNonQuery(_connectionProvider.GetWriteConnectionString(), CommandType.Text, cmdText, parameters);
            return true;
        }

        /// <summary>
        /// 删除指定主题的所有标签映射关系
        /// </summary>
        /// <param name="themeId"></param>
        /// <returns></returns>
        public bool DeleteTagMaps(int themeId)
        {
            string cmdText = "DELETE FROM ThemeTagMap WHERE ThemeId=@ThemeId";
            SqlParameter[]  parameters = new SqlParameter[]
			{
				SqlParameterHelper.BuildInputParameter("@ThemeId",SqlDbType.Int, 4, themeId),
			};
            SqlHelper.ExecuteNonQuery(_connectionProvider.GetWriteConnectionString(), CommandType.Text, cmdText, parameters);
            return true;
        }

        /// <summary>
        /// 增加主题分类
        /// </summary>
        /// <param name="category">主题分类实体</param>
        public bool AddCategory(ThemeCategory category)
        {
            string cmdText = @"
			insert into ThemeCategory
				(CategoryName,ParentId,CategoryIcon,SortNumber,BindTagCategories)
			values
				(@CategoryName,@ParentId,@CategoryIcon,@SortNumber,@BindTagCategories);SELECT @@IDENTITY
			";
            SqlParameter[] parameters = new SqlParameter[]
			{
				SqlParameterHelper.BuildInputParameter("@CategoryName",SqlDbType.NVarChar, 300, category.CategoryName),
				SqlParameterHelper.BuildInputParameter("@ParentId",SqlDbType.Int, 4, category.ParentId),
				SqlParameterHelper.BuildInputParameter("@CategoryIcon",SqlDbType.NVarChar, 300, category.CategoryIcon),
				SqlParameterHelper.BuildInputParameter("@SortNumber",SqlDbType.Int, 4, category.SortNumber),
				SqlParameterHelper.BuildInputParameter("@BindTagCategories",SqlDbType.NVarChar, 500, category.BindTagCategories)
			};
            category.CategoryId = Convert.ToInt32(SqlHelper.ExecuteScalar(_connectionProvider.GetWriteConnectionString(), CommandType.Text, cmdText, parameters));
            return category.CategoryId > 0;
        }
        /// <summary>
        /// 更新主题分类
        /// </summary>
        /// <param name="category">主题分类实体</param>
        public bool UpdateCategory(ThemeCategory category)
        {
            string cmdText = @"
			update ThemeCategory set
				CategoryName=@CategoryName,
				ParentId=@ParentId,
				CategoryIcon=@CategoryIcon,
				SortNumber=@SortNumber,
				BindTagCategories=@BindTagCategories
			where 
				CategoryId=@CategoryId 
			";
            SqlParameter[] parameters = new SqlParameter[]
			{
				SqlParameterHelper.BuildInputParameter("@CategoryId", SqlDbType.Int, 4, category.CategoryId),
				SqlParameterHelper.BuildInputParameter("@CategoryName", SqlDbType.NVarChar, 300, category.CategoryName),
				SqlParameterHelper.BuildInputParameter("@ParentId", SqlDbType.Int, 4, category.ParentId),
				SqlParameterHelper.BuildInputParameter("@CategoryIcon", SqlDbType.NVarChar, 300, category.CategoryIcon),
				SqlParameterHelper.BuildInputParameter("@SortNumber", SqlDbType.Int, 4, category.SortNumber),
				SqlParameterHelper.BuildInputParameter("@BindTagCategories", SqlDbType.NVarChar, 500, category.BindTagCategories)
			};
            return SqlHelper.ExecuteNonQuery(_connectionProvider.GetWriteConnectionString(), CommandType.Text, cmdText, parameters) > 0;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="categoryId">主题分类id</param>
        public bool DeleteCategory(int categoryId)
        {
            string cmdText = "delete from ThemeCategory where CategoryId=@CategoryId ";
            SqlParameter[] parameters = new SqlParameter[]
			{
				SqlParameterHelper.BuildInputParameter("@CategoryId",SqlDbType.Int, 4, categoryId)
			};
            return SqlHelper.ExecuteNonQuery(_connectionProvider.GetWriteConnectionString(), CommandType.Text, cmdText, parameters) > 0;
        }

        /// <summary>
        /// 增加主题下载数
        /// </summary>
        /// <param name="themeId"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public bool IncreaseDownloads(int themeId, int number)
        {
            string cmdText = string.Format("UPDATE Theme SET Downloads = Downloads + {1} WHERE ThemeId={0}", themeId, number);
            return SqlHelper.ExecuteNonQuery(_connectionProvider.GetWriteConnectionString(), CommandType.Text, cmdText) > 0;
        }
        /// <summary>
        /// 增加主题浏览数
        /// </summary>
        /// <param name="themeId"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public bool IncreaseViews(int themeId, int number)
        {
            string cmdText = string.Format("UPDATE Theme SET Views = Views + {1} WHERE ThemeId={0}", themeId, number);
            return SqlHelper.ExecuteNonQuery(_connectionProvider.GetWriteConnectionString(), CommandType.Text, cmdText) > 0;
        }
        /// <summary>
        /// 评分主题
        /// </summary>
        /// <param name="themeId"></param>
        /// <param name="score"></param>
        /// <param name="userId"></param>
        /// <param name="userIp"></param>
        /// <returns></returns>
        public bool RateTheme(int themeId, int score, int userId, string userIp)
        {
            string cmdText = string.Format(@"
                IF NOT EXISTS (SELECT 1 FROM ThemeRateHistory WHERE ThemeId={0} AND UserIp = '{3}')
                BEGIN
                    UPDATE Theme SET RateScore = RateScore + {1},RateNumbers = RateNumbers + 1 WHERE ThemeId={0};
                    INSERT INTO ThemeRateHistory (ThemeId, RateScore, AddTime, UserId, UserIp) VALUES ({0}, {1}, getdate(), {2}, '{3}');
                END
            "
                , themeId
                , score
                , userId
                , userIp.Replace("'", "''")
                );
            return SqlHelper.ExecuteNonQuery(_connectionProvider.GetWriteConnectionString(), CommandType.Text, cmdText) > 0;        }        #endregion
    }
}
