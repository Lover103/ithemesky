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
        /// 数据库连接串访问对象
        /// </summary>
        private IConnectionProvider _connection;
        /// <summary>
        /// 初始化主题的管理数据访问类
        /// </summary>
        /// <param name="connection"></param>
        public ThemeManageRepository(IConnectionProvider connection)
        {
            _connection = connection;
        }
        #region IThemeManageRepository members

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
				SqlParameterHelper.BuildInputParameter("@Title",SqlDbType.VarChar, 300, theme.Title),
				SqlParameterHelper.BuildInputParameter("@FileSize",SqlDbType.BigInt, 8, theme.FileSize),
				SqlParameterHelper.BuildInputParameter("@Description",SqlDbType.VarChar, 1000, theme.Description),
				SqlParameterHelper.BuildInputParameter("@DisplayState",SqlDbType.SmallInt, 2, theme.DisplayState.ToInt32()),
				SqlParameterHelper.BuildInputParameter("@CheckState",SqlDbType.SmallInt, 2, theme.CheckState.ToInt32()),
				SqlParameterHelper.BuildInputParameter("@AuthorId",SqlDbType.Int, 4, theme.AuthorId),
				SqlParameterHelper.BuildInputParameter("@CheckerId",SqlDbType.Int, 4, theme.CheckerId),
				SqlParameterHelper.BuildInputParameter("@CommendIndex",SqlDbType.Int, 4, theme.CommendIndex),
				SqlParameterHelper.BuildInputParameter("@ThumbnailName",SqlDbType.VarChar, 40, theme.ThumbnailName),
				SqlParameterHelper.BuildInputParameter("@AddTime",SqlDbType.DateTime, 8, theme.AddTime),
				SqlParameterHelper.BuildInputParameter("@UpdateTime",SqlDbType.DateTime, 8, theme.UpdateTime),
				SqlParameterHelper.BuildInputParameter("@GoodComments",SqlDbType.Int, 4, theme.GoodComments),
				SqlParameterHelper.BuildInputParameter("@BadComments",SqlDbType.Int, 4, theme.BadComments),
				SqlParameterHelper.BuildInputParameter("@Comments",SqlDbType.Int, 4, theme.Comments),
				SqlParameterHelper.BuildInputParameter("@Downloads",SqlDbType.Int, 4, theme.Downloads),
				SqlParameterHelper.BuildInputParameter("@Views",SqlDbType.Int, 4, theme.Views),
				SqlParameterHelper.BuildInputParameter("@LastWeekDownloads",SqlDbType.Int, 4, theme.LastWeekDownloads),
				SqlParameterHelper.BuildInputParameter("@LastMonthDownloads",SqlDbType.Int, 4, theme.LastMonthDownloads),
				SqlParameterHelper.BuildInputParameter("@Source",SqlDbType.Int, 4, theme.Source.ToInt32())
			};
            string cmdText = @"
                INSERT INTO Theme
				    (CategoryId,ParentCategoryId,Title,FileSize,Description,DisplayState,CheckState,AuthorId,CheckerId,CommendIndex,ThumbnailName,AddTime,UpdateTime,GoodComments,BadComments,Comments,Downloads,Views,LastWeekDownloads,LastMonthDownloads,Source)
			    VALUES
				    (@CategoryId,@ParentCategoryId,@Title,@FileSize,@Description,@DisplayState,@CheckState,@AuthorId,@CheckerId,@CommendIndex,@ThumbnailName,@AddTime,@UpdateTime,@GoodComments,@BadComments,@Comments,@Downloads,@Views,@LastWeekDownloads,@LastMonthDownloads,@Source);SELECT @@IDENTITY";
            int themeId = Convert.ToInt32(SqlHelper.ExecuteScalar(_connection.GetWriteConnectionString(), CommandType.Text, cmdText, parameters));
            if (themeId > 0)
            {
                theme.ThemeId = themeId;
                return true;
            }
            return false;
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
				SqlParameterHelper.BuildInputParameter("@Url",SqlDbType.VarChar, 500, url),
				SqlParameterHelper.BuildInputParameter("@Source",SqlDbType.SmallInt, 2, source),
				SqlParameterHelper.BuildInputParameter("@AddTime",SqlDbType.DateTime, 8, DateTime.Now)
			};
            return SqlHelper.ExecuteNonQuery(_connection.GetWriteConnectionString(), CommandType.Text, cmdText, parameters) > 0;
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
				SqlParameterHelper.BuildInputParameter("@TagName", SqlDbType.VarChar, 40, tagName),
			};
            int tagId = Convert.ToInt32(SqlHelper.ExecuteScalar(_connection.GetWriteConnectionString(), CommandType.Text, cmdText, parameters));

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
            SqlHelper.ExecuteNonQuery(_connection.GetWriteConnectionString(), CommandType.Text, cmdText, parameters);
            return true;
        }        #endregion
    }
}
