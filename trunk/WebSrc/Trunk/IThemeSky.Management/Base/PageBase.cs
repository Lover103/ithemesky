using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IThemeSky.Management.Base
{
    public class PageBase : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            if (Request.Cookies["UserLoginInfo"] == null)
            {
                Response.Redirect("/Login.aspx", false);
                return;
            }
            base.OnInit(e);
        }
    }
}
