using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IThemeSky.Model;

namespace IThemeSky.UI.Models
{
    public class ListModel : ModelBase
    {
        public ListModel(int categoryId, List<int> tagIds, ThemeSortOption sort, int pageIndex, int pageSize)
        {
            CategoryId = categoryId;
            TagIds = tagIds;
            Sort = sort;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public int CategoryId { get; private set; }
        
        public List<int> TagIds { get; private set; }
        
        public ThemeSortOption Sort { get; private set; }
        
        public int PageIndex { get; private set; }
        
        public int PageSize { get; private set; }
        
        public int RecordCount { get; private set; }
    }
}
