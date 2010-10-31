using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IThemeSky.Model
{
    public class UserOrder
    {
        public int OrderId { get; set; }

        public string OrderNumber { get; set; }

        public string UserMail { get; set; }

        public string PayerMail { get; set; }

        public int ThemeId { get; set; }

        public double Price { get; set; }

        public string Checksum { get; set; }

        public int Status { get; set; }

        public string Description { get; set; }

        public DateTime AddTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public string UserName { get; set; }
    }
}
