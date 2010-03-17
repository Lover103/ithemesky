using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IThemeSky.Model;

namespace IThemeSky.DataAccess
{
    /// <summary>
    /// 主题视图数据操作接口
    /// </summary>
    public interface IThemeViewRepository
    {
        /// <summary>
        /// 获取默认的过滤器
        /// </summary>
        ThemesFilter Filter { get; }
        /// <summary>
        /// 根据主题id获取主题完整实体
        /// </summary>
        /// <param name="themeId">主题id</param>
        /// <returns></returns>
        FullThemeView GetTheme(int themeId);
        /// <summary>
        /// 根据指定主题的上一个主题id
        /// </summary>
        /// <param name="themeId">主题id</param>
        /// <returns></returns>
        int GetPrevThemeId(int themeId);
        /// <summary>
        /// 根据指定主题的下一个主题id
        /// </summary>
        /// <param name="themeId">主题id</param>
        /// <returns></returns>
        int GetNextThemeId(int themeId);
        /// <summary>
        /// 获取主题列表
        /// </summary>
        /// <param name="sort">排序方式</param>
        /// <param name="displayNumber">显示条数</param>
        /// <returns></returns>
        List<SimpleThemeView> GetThemes(ThemeSortOption sort, int displayNumber);
        /// <summary>
        /// 获取主题列表
        /// </summary>
        /// <param name="sort">排序方式</param>
        /// <param name="pageIndex">显示的页码，从1开始计数</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="recordCount">总记录数(ref)</param>
        /// <returns></returns>
        List<SimpleThemeView> GetThemes(ThemeSortOption sort, int pageIndex, int pageSize, ref int recordCount);
        /// <summary>
        /// 根据分类id获取主题列表
        /// </summary>
        /// <param name="categoryId">分类id</param>
        /// <param name="sort">排序方式</param>
        /// <param name="displayNumber">显示条数</param>
        /// <returns></returns>
        List<SimpleThemeView> GetThemesByCategoryId(int categoryId, ThemeSortOption sort, int displayNumber);
        /// <summary>
        /// 根据分类id获取主题列表
        /// </summary>
        /// <param name="categoryId">分类id</param>
        /// <param name="sort">排序方式</param>
        /// <param name="pageIndex">显示的页码，从1开始计数</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="recordCount">总记录数(ref)</param>
        /// <returns></returns>
        List<SimpleThemeView> GetThemesByCategoryId(int categoryId, ThemeSortOption sort, int pageIndex, int pageSize, ref int recordCount);
        /// <summary>
        /// 根据分类id获取主题列表
        /// </summary>
        /// <param name="parentCategoryId">父级分类id</param>
        /// <param name="sort">排序方式</param>
        /// <param name="displayNumber">显示条数</param>
        /// <returns></returns>
        List<SimpleThemeView> GetThemesByParentCategoryId(int parentCategoryId, ThemeSortOption sort, int displayNumber);
        /// <summary>
        /// 根据分类id获取主题列表
        /// </summary>
        /// <param name="parentCategoryId">父级分类id</param>
        /// <param name="sort">排序方式</param>
        /// <param name="pageIndex">显示的页码，从1开始计数</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="recordCount">总记录数(ref)</param>
        /// <returns></returns>
        List<SimpleThemeView> GetThemesByParentCategoryId(int parentCategoryId, ThemeSortOption sort, int pageIndex, int pageSize, ref int recordCount);
        /// <summary>
        /// 根据多个主题id获取主题列表
        /// </summary>
        /// <param name="themeIds">主题id列表</param>
        /// <returns></returns>
        List<SimpleThemeView> GetThemesByIds(List<int> themeIds);
        /// <summary>
        /// 根据过滤器获取所有主题
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <param name="sort">排序方式</param>
        /// <param name="displayNumber">显示条数</param>
        /// <returns></returns>
        List<SimpleThemeView> GetThemesByFilter(ThemesFilter filter, ThemeSortOption sort, int displayNumber);
        /// <summary>
        /// 根据过滤器获取所有主题
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <param name="sort">排序方式</param>
        /// <param name="pageIndex">显示的页码，从1开始计数</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="recordCount">总记录数(ref)</param>
        /// <returns></returns>
        List<SimpleThemeView> GetThemesByFilter(ThemesFilter filter, ThemeSortOption sort, int pageIndex, int pageSize, ref int recordCount);
        /// <summary>
        /// 搜索主题
        /// </summary>
        /// <param name="keyword">主题名称关键字</param>
        /// <param name="sort">排序方式</param>
        /// <param name="pageIndex">显示的页码，从1开始计数</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="recordCount">总记录数(ref)</param>
        /// <returns></returns>
        List<SimpleThemeView> SearchThemes(string keyword, ThemeSortOption sort, int pageIndex, int pageSize, ref int recordCount);
        /// <summary>
        /// 获取随机主题
        /// </summary>
        /// <param name="sort">排序方式</param>
        /// <param name="displayNumber">显示条数</param>
        /// <returns></returns>
        List<SimpleThemeView> GetRandomThemes(ThemeSortOption sort, int displayNumber);
        /// <summary>
        /// 根据过滤器获取所有主题(完整实体)
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <param name="sort">排序方式</param>
        /// <param name="pageIndex">显示的页码，从1开始计数</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="recordCount">总记录数(ref)</param>
        /// <returns></returns>
        List<FullThemeView> GetFullThemesByFilter(ThemesFilter filter, ThemeSortOption sort, int pageIndex, int pageSize, ref int recordCount);
        /// <summary>
        /// 获取所有主题分类列表
        /// </summary>
        /// <returns></returns>
        List<ThemeCategory> GetThemeCategories();
    }
}
