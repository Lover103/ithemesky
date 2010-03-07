using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using IThemeSky.Library.Data;
using System.Data;

namespace IThemeSky.DataAccess
{
    public class ThemeManageRepository : IThemeManageRepository
    {
        #region IThemeManageRepository Members

        public bool AddTheme(IThemeSky.Model.Theme theme)
        {
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                SqlParameterHelper.BuildInputParameter("@ThemeId", SqlDbType.Int, 4, theme.ThemeId),
                SqlParameterHelper.BuildInputParameter("@ThemeId", SqlDbType.Int, 4, theme.ParentCategoryId),
                SqlParameterHelper.BuildInputParameter("@ThemeId", SqlDbType.Int, 4, theme.CategoryId),
                SqlParameterHelper.BuildInputParameter("@ThemeId", SqlDbType.VarChar, 300, theme.Title),
                SqlParameterHelper.BuildInputParameter("@ThemeId", SqlDbType.BigInt, 8, theme.FileSize),
                SqlParameterHelper.BuildInputParameter("@ThemeId", SqlDbType.VarChar, 1000, theme.Description),
                SqlParameterHelper.BuildInputParameter("@ThemeId", SqlDbType.SmallInt, 2, (int)theme.DisplayState),
                SqlParameterHelper.BuildInputParameter("@ThemeId", SqlDbType.SmallInt, 2, (int)theme.CheckState),
                SqlParameterHelper.BuildInputParameter("@ThemeId", SqlDbType.Int, 4, theme.AuthorId),
                SqlParameterHelper.BuildInputParameter("@ThemeId", SqlDbType.Int, 4, theme.CheckerId),
                SqlParameterHelper.BuildInputParameter("@ThemeId", SqlDbType.Int, 4, theme.CommendIndex),
                SqlParameterHelper.BuildInputParameter("@ThemeId", SqlDbType.VarChar, 40, theme.ThumbnailName),
                SqlParameterHelper.BuildInputParameter("@ThemeId", SqlDbType.DateTime, 8, theme.AddTime),
                SqlParameterHelper.BuildInputParameter("@ThemeId", SqlDbType.DateTime, 8, theme.UpdateTime),
                SqlParameterHelper.BuildInputParameter("@ThemeId", SqlDbType.Int, 4, theme.GoodComments),
                SqlParameterHelper.BuildInputParameter("@ThemeId", SqlDbType.Int, 4, theme.BadComments),
                SqlParameterHelper.BuildInputParameter("@ThemeId", SqlDbType.Int, 4, theme.Comments),
                SqlParameterHelper.BuildInputParameter("@ThemeId", SqlDbType.Int, 4, theme.Downloads),
                SqlParameterHelper.BuildInputParameter("@ThemeId", SqlDbType.Int, 4, theme.Views),
                SqlParameterHelper.BuildInputParameter("@ThemeId", SqlDbType.Int, 4, theme.LastWeekDownloads),
                SqlParameterHelper.BuildInputParameter("@ThemeId", SqlDbType.Int, 4, theme.LastMonthDownloads),
                SqlParameterHelper.BuildInputParameter("@ThemeId", SqlDbType.Int, 4, theme.Source),
            };
            string cmdText = "INSERT INTO Theme (ThemeId,ParentCategoryId,CategoryId,Title,FileSize,Description,DisplayState,CheckState) VALUES ()";
        }

        public bool AddThemeDownloadUrl(int themeId, string url, bool isDefault, int source)
        {
            throw new NotImplementedException();
        }

        public bool MappingThemeTag(int themeId, string tagName)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
