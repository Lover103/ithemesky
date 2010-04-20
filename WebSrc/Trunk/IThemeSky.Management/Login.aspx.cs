using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IThemeSky.Management.Base;

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
                UserContext.UserId = 1;
                Response.Cookies["UserLoginInfo"]["UserName"] = "兴柏";
                Response.Redirect("/Theme/ThemeListManage.aspx");
            }
            if (txtPassword.Text.Equals("在灼"))
            {
                UserContext.UserId = 2;
                Response.Cookies["UserLoginInfo"]["UserName"] = "在灼";
                Response.Redirect("/Theme/ThemeListManage.aspx");
            }
            if (txtPassword.Text.Equals("伟伟"))
            {
                UserContext.UserId = 3;
                Response.Cookies["UserLoginInfo"]["UserName"] = "伟伟";
                Response.Redirect("/Theme/ThemeListManage.aspx");
            }
        }
    }
}
