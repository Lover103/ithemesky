using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IThemeSky.Model;

namespace IThemeSky.UI.Models
{
    public class PayResultModel : NormalPageModel
    {
        public PayResultModel()
            : base()
        {
            Results = new Dictionary<string, string>();
        }

        public bool Success { get; set; }

        public string txn_id 
        {
            get { return Results["txn_id"]; }
        }

        public string payment_gross
        {
            get { return Results["payment_gross"]; }
        }

        public string payer_email
        {
            get { return Results["payer_email"]; }
        }

        public string item_name
        {
            get { return Results["item_name"]; }
        }

        public string UserMail { get; set; }

        public FullThemeView Theme { get; set; }

        public string Description { get; set; }

        public Dictionary<string, string> Results { get; private set; }
    }
}
