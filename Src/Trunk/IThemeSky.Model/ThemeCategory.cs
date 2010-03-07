using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IThemeSky.Model
{
    public class ThemeCategory
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public int ParentId { get; set; }

        public string CategoryIcon { get; set; }

        public int SortNumber { get; set; }
    }
}
