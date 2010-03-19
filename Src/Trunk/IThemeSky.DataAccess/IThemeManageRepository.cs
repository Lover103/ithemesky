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
    }
}
