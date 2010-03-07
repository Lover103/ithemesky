using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IThemeSky.Model
{
    public class SimpleThemeView
    {
        public int ThemeId { get; set; }

        public int CategoryId { get; set; }

        public string Title { get; set; }

        public string Thumbnail { get; set; }

        public string Thumbnail_112x168 { get; set; }

        public int CommendIndex { get; set; }

        public int Downloads { get; set; }
    }
}
