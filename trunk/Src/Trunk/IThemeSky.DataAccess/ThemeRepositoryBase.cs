using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IThemeSky.Model;
using IThemeSky.Library.Extensions;
using System.Data;

namespace IThemeSky.DataAccess
{
    public abstract class ThemeRepositoryBase
    {
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
    }
}
