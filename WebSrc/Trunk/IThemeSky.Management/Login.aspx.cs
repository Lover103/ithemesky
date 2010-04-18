using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IThemeSky.Management
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text.Equals("兴柏"))
            {
                Response.Cookies["UserLoginInfo"]["UserName"] = "兴柏";
                Response.Redirect("/Theme/ThemeListManage.aspx");
            }
        }
    }
}
