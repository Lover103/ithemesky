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
        /// 添加新的主题
        /// </summary>
        /// <param name="theme">主题对象</param>
        /// <returns></returns>
        bool AddTheme(Theme theme);
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
    }
}
