using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IThemeSky.Model;
using IThemeSky.DataAccess;
using System.Text;

namespace IThemeSky.UI.Models
{
    public class ListModel : ModelBase
    {
        public ListModel(int categoryId, string categoryName, string tagNames, ThemeSortOption sort, int pageIndex, int pageSize)
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
            TagNames = tagNames;
            Sort = sort;
            PageIndex = pageIndex;
            PageSize = pageSize;

            ThemesFilter filter = _themeRepository.Filter;
            if (CategoryId > 0)
            {
                filter.CategoryIds.Add(categoryId);
            }
            int recordCount = 0;
            Themes = _themeRepository.GetThemesByFilter(filter, sort, pageIndex, pageSize, ref recordCount);
            RecordCount = recordCount;

            //计算当前的页面地址
            StringBuilder sbUrlPattern = new StringBuilder();
            sbUrlPattern.Append("/list/");
            sbUrlPattern.Append("{1}");
            sbUrlPattern.Append("/");
            if (CategoryId > 0)
            {
                sbUrlPattern.AppendFormat("{0}_{1}/", CategoryName, CategoryId);
            }
            sbUrlPattern.Append("{0}");
            if (!string.IsNullOrEmpty(TagNames))
            {
                sbUrlPattern.Append(tagNames);
            }
            UrlPatternWithoutSort = sbUrlPattern.ToString();
            UrlPatternWithoutPageIndex = string.Format(UrlPatternWithoutSort, "{0}", Sort.ToString().ToLower());
        }

        public string UrlPatternWithoutPageIndex { get; private set; }

        public string UrlPatternWithoutSort { get; set; }

        public int CategoryId { get; private set; }

        public string CategoryName { get; set; }

        public string TagNames { get; private set; }
        
        public ThemeSortOption Sort { get; private set; }

        public List<SimpleThemeView> Themes { get; private set; }

        public int PageIndex { get; private set; }
        
        public int PageSize { get; private set; }
        
        public int RecordCount { get; private set; }
    }
}
