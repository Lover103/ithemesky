using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IThemeSky.Model
{
    public class ThemeImage
    {
        public int ImageId { get; set; }

        public int ThemeId { get; set; }

        public string ImageUrl { get; set; }

        public DateTime AddTime { get; set; }
    }
}
