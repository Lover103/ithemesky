using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IThemeSky.Model
{
    public class ISpiritUserInfo
    {
        public string PhoneVersion { get; set; }

        public string SoftVersion { get; set; }

        public string ITunesVersion { get; set; }

        public string DeviceId { get; set; }

        public string DeviceType { get; set; }

        public string UserAgent { get; set; }

        public DateTime AddTime { get; set; }

        public string UserIP { get; set; }
    }
}
