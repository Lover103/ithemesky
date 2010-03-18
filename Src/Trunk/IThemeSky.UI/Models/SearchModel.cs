using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IThemeSky.Model;
using IThemeSky.DataAccess;
using System.Text;

namespace IThemeSky.UI.Models
{
    public class SearchModel : ModelBase
    {
        public SearchModel(string keyword, ThemeSortOption sort, int pageIndex, int pageSize)
        {
            Keyword = keyword;
            Sort = sort;
            PageIndex = pageIndex;
            PageSize = pageSize;

            int recordCount = 0;

            Themes = _themeRepository.SearchThemes(Keyword, sort, pageIndex, pageSize, ref recordCount);
            RecordCount = recordCount;

            //计算当前的页面地址
            StringBuilder sbUrlPattern = new StringBuilder();
            sbUrlPattern.Append("/search/");
            sbUrlPattern.Append("{1}/");
            sbUrlPattern.Append(keyword);
            sbUrlPattern.Append("/");
            sbUrlPattern.Append("{0}");
            UrlPatternWithoutSort = sbUrlPattern.ToString();
            UrlPatternWithoutPageIndex = string.Format(UrlPatternWithoutSort, "{0}", Sort.ToString().ToLower());
        }

        public string UrlPatternWithoutPageIndex { get; private set; }

        public string UrlPatternWithoutSort { get; set; }

        public string Keyword { get; set; }
        
        public ThemeSortOption Sort { get; private set; }

        public List<SimpleThemeView> Themes { get; private set; }

        public int PageIndex { get; private set; }
        
        public int PageSize { get; private set; }
        
        public int RecordCount { get; private set; }
    }
}
