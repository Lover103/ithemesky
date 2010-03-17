using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IThemeSky.Model;
using IThemeSky.DataAccess;

namespace IThemeSky.UI.Models
{
    public class ListModel : ModelBase
    {
        private ICacheThemeViewRepository _themeRepository = ThemeRepositoryFactory.Default.GetCachedThemeViewRepository();
        public ListModel(int categoryId, string categoryName, List<string> tagNames, ThemeSortOption sort, int pageIndex, int pageSize)
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
            TagNames = tagNames;
            Sort = sort;
            PageIndex = pageIndex;
            PageSize = pageSize;
            ThemesFilter filter = _themeRepository.Filter;
            if (categoryId > 0)
            {
                filter.CategoryIds.Add(categoryId);
            }
            int recordCount = 0;
            Themes = _themeRepository.GetThemesByFilter(filter, sort, pageIndex, pageSize, ref recordCount);
            RecordCount = recordCount;
        }

        public int CategoryId { get; private set; }

        public string CategoryName { get; set; }

        public List<string> TagNames { get; private set; }
        
        public ThemeSortOption Sort { get; private set; }

        public List<SimpleThemeView> Themes { get; private set; }

        public int PageIndex { get; private set; }
        
        public int PageSize { get; private set; }
        
        public int RecordCount { get; private set; }
    }
}
