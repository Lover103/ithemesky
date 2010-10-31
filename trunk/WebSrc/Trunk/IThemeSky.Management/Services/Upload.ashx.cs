using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.IO;
using IThemeSky.Library.Util;

namespace IThemeSky.Management.Services
{
    public class Upload : IHttpHandler, IReadOnlySessionState 
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string fileName = string.Format("/ThemeThumbnails/{0}/{1}.jpg", DateTime.Now.ToString("yyyyMM"), Guid.NewGuid().ToString());

                if (!Directory.Exists(Path.GetDirectoryName(context.Server.MapPath(fileName))))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(context.Server.MapPath(fileName)));
                }
                using (FileStream fs = new FileStream(context.Server.MapPath(fileName), FileMode.Create))
                {
                    //for ie. fuck
                    if (context.Request.Files.Count > 0)
                    {
                        HttpPostedFile file = context.Request.Files[0];
                        using (Stream stream = file.InputStream)
                        {
                            byte[] buffer = new byte[1024];
                            int result;
                            while ((result = stream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                fs.Write(buffer, 0, result);
                            }
                        }
                    }
                    //chrome支持
                    else
                    {
                        using (Stream stream = context.Request.InputStream)
                        {
                            byte[] buffer = new byte[1024];
                            int result;
                            while ((result = stream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                fs.Write(buffer, 0, result);
                            }
                        }
                    }
                }
                //生成缩略图
                ImageHelper.MakeThumbnail(context.Server.MapPath(fileName), context.Server.MapPath(fileName.Replace(".jpg", "_112x168.jpg")), 112, 168, "wh");

                context.Response.ContentType = "text/plain";
                context.Response.Write(string.Format("{{success:true, fileName:'{0}', thumbnail:'{1}'}}", fileName, fileName.Replace(".jpg", "_112x168.jpg")));
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write(string.Format("{{error:'{0}'}}", ex.Message.Replace("'", "\\'")));
            }
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
