using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IThemeSky.Model;
using IThemeSky.DataAccess;
using System.Text;
using System.Text.RegularExpressions;

namespace IThemeSky.UI.Models
{
    public class ListModel : ModelBase
    {
        public ListModel(int fw, int categoryId, string categoryName, string tagNames, ThemeSortOption sort, int pageIndex, int pageSize)
        {
            FrameworkVersion = fw;
            CategoryId = categoryId;
            CategoryName = categoryName;
            TagNames = tagNames;
            Sort = sort;
            PageIndex = pageIndex;
            PageSize = pageSize;

            ThemesFilter filter = _themeRepository.Filter;
            if (fw > 0)
            {
                filter.SupportIPhone4 = true;
            }
            if (CategoryId > 0)
            {
                filter.CategoryIds.Add(categoryId);
            }
            int recordCount = 0;
            if (!string.IsNullOrEmpty(tagNames))
            {
                List<List<string>> tagsFilter = new List<List<string>>();
                string[] arrAndTags = Regex.Split(tagNames, "-");
                foreach (string andTags in arrAndTags)
                {
                    List<string> tags = new List<string>();
                    string[] arrOrTags = andTags.Split(',');
                    foreach (string orTag in arrOrTags)
                    {
                        tags.Add(orTag);
                    }
                    tagsFilter.Add(tags);
                }
                Themes = _themeRepository.GetThemesByMultiTags(tagsFilter, filter, sort, pageIndex, pageSize, ref recordCount);
            }
            else
            {
                Themes = _themeRepository.GetThemesByFilter(filter, sort, pageIndex, pageSize, ref recordCount);
            }
            RecordCount = recordCount;

            //计算当前的页面地址
            StringBuilder sbUrlPattern = new StringBuilder();
            if (this.FrameworkVersion == 4)
            {
                sbUrlPattern.Append("/iphone4/");
            }
            else
            {
                sbUrlPattern.Append("/list/");
            }
            sbUrlPattern.Append("{1}");
            sbUrlPattern.Append("/");
            if (CategoryId > 0)
            {
                sbUrlPattern.AppendFormat("{0}_{1}/", CategoryName, CategoryId);
            }
            sbUrlPattern.Append("{0}");
            UrlPatternWithouTags = string.Format(sbUrlPattern.ToString(), pageIndex, Sort.ToString().ToLower()) + "/";
            if (!string.IsNullOrEmpty(TagNames))
            {
                sbUrlPattern.Append("/");
                sbUrlPattern.Append(tagNames);
            }
            UrlPatternWithoutSort = sbUrlPattern.ToString();
            UrlPatternWithoutPageIndex = string.Format(UrlPatternWithoutSort, "{0}", Sort.ToString().ToLower());
        }

        public string UrlPatternWithoutPageIndex { get; private set; }

        public string UrlPatternWithoutSort { get; set; }

        public string UrlPatternWithouTags { get; set; }

        public int FrameworkVersion { get; private set; }

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
