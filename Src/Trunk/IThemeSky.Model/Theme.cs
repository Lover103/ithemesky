using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IThemeSky.Model
{
    public class Theme
    {
        public int ThemeId { get; set; }

        public int ParentCategoryId { get; set; }

        public int CategoryId { get; set; }

        public string Title { get; set; }

        public long FileSize { get; set; }

        public string Description { get; set; }

        public DisplayStateOption DisplayState { get; set; }

        public CheckStateOption CheckState { get; set; }

        public int AuthorId { get; set; }

        public int CheckerId { get; set; }

        public int SortNumber { get; set; }

        public int CommendIndex { get; set; }

        public string ThumbnailName { get; set; }

        public DateTime AddTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public int GoodComments { get; set; }

        public int BadComments { get; set; }

        public int Comments { get; set; }

        public int Downloads { get; set; }

        public int Views { get; set; }

        public int LastWeekDownloads { get; set; }

        public int LastMonthDownloads { get; set; }

        public int Source { get; set; }
    }
}
