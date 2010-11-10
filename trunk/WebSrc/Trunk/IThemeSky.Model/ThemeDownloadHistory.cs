using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IThemeSky.Model
{
    public class ThemeDownloadHistory
    {
        public long HistoryId { get; set; }

        public int ThemeId { get; set; }

        public DateTime AddTime { get; set; }

        public string UserIp { get; set; }

        public string DownloadCode { get; set; }

        public bool PaidTheme { get; set; }
    }
}
