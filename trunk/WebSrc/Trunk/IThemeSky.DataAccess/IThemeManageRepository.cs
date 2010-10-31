using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IThemeSky.Model;

namespace IThemeSky.DataAccess
{
    /// <summary>
    /// 该接口提供对主题的管理方法
    /// </summary>
    public interface IThemeManageRepository
    {
        /// <summary>
        /// 获取一个主题实体
        /// </summary>
        /// <param name="themeId">主题id</param>
        Theme GetTheme(int themeId);
        /// <summary>
        /// 添加新的主题
        /// </summary>
        /// <param name="theme">主题对象</param>
        /// <returns></returns>
        bool AddTheme(Theme theme);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="theme">主题实体</param>
        bool UpdateTheme(Theme theme);
        /// <summary>
        /// 为指定的主题添加下载地址
        /// </summary>
        /// <param name="themeId"></param>
        /// <param name="url"></param>
        /// <param name="isDefault"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        bool AddThemeDownloadUrl(int themeId, string url, bool isDefault, SourceOption source);
        /// <summary>
        /// 维护主题与标签的映射关系
        /// </summary>
        /// <param name="themeId"></param>
        /// <param name="tagName"></param>
        /// <returns></returns>
        bool MappingThemeTag(int themeId, string tagName);
        /// <summary>
        /// 删除指定主题的所有标签映射关系
        /// </summary>
        /// <param name="themeId"></param>
        /// <returns></returns>
        bool DeleteTagMaps(int themeId);
        /// <summary>
        /// 增加主题分类
        /// </summary>
        /// <param name="category">主题分类实体</param>
        bool AddCategory(ThemeCategory category);
        /// <summary>
        /// 更新主题分类
        /// </summary>
        /// <param name="category">主题分类实体</param>
        bool UpdateCategory(ThemeCategory category);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="categoryId">主题分类id</param>
        bool DeleteCategory(int categoryId);
        /// <summary>
        /// 增加主题下载数
        /// </summary>
        /// <param name="themeId"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        bool IncreaseDownloads(int themeId, int number);
        /// <summary>
        /// 增加主题浏览数
        /// </summary>
        /// <param name="themeId"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        bool IncreaseViews(int themeId, int number);
         /// <summary>
        /// 评分主题
        /// </summary>
        /// <param name="themeId"></param>
        /// <param name="score"></param>
        /// <param name="userId"></param>
        /// <param name="userIp"></param>
        /// <returns></returns>
        bool RateTheme(int themeId, int score, int userId, string userIp);
        /// <summary>
        /// 增加下载历史
        /// </summary>
        /// <param name="themeId">主题id</param>
        /// <param name="userIp">用户ip</param>
        /// <returns></returns>
        bool InsertDownloadHistory(int themeId, string userIp, string downloadCode);

        /// <summary>
        /// 添加主题图片地址
        /// </summary>
        /// <param name="themeId"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        bool AddThemeImage(int themeId, string url);

        /// <summary>
        /// 删除主题图片地址
        /// </summary>
        /// <param name="themeId"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        bool DeleteThemeImage(int themeId, string url);
    }
}
