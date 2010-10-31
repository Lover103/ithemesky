using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using IThemeSky.Library.Extensions;
using IThemeSky.DataAccess;

namespace IThemeSky.Management.Services
{
    public class DeleteFile : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string fileName = context.Server.MapPath(context.Request.QueryString["fileName"]);
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            if (fileName.EndsWith(".jpg", StringComparison.CurrentCultureIgnoreCase))
            {
                if (File.Exists(fileName.Replace(".jpg", "_112x168.jpg")))
                {
                    File.Delete(fileName.Replace(".jpg", "_112x168.jpg"));
                }
            }
            int themeId = context.Request.QueryString["themeId"].ToInt32();
            if (themeId > 0)
            {
                IThemeManageRepository repository = ThemeRepositoryFactory.Default.GetThemeManageRepository();
                repository.DeleteThemeImage(themeId, context.Request.QueryString["fileName"]);
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write("ok");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
