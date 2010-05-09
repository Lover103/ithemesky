using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IThemeSky.Model
{
    public class ThemeSupport
    {
        public int SupportId { get; set; }

        public string Name { get; set; }

        public string Mail { get; set; }

        public SupportTypeOption SupportType { get; set; }

        public int ThemeId { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public DateTime AddTime { get; set; }

        public string UserIp { get; set; }
    }
}
