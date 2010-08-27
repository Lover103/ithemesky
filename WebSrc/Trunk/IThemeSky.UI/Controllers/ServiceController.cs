using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IThemeSky.Library.Extensions;
using IThemeSky.Model;
using IThemeSky.DataAccess;
using IThemeSky.UI.Models;
using System.Web.UI;
using System.IO;
using System.Text.RegularExpressions;
using IThemeSky.Library.Util;

namespace IThemeSky.UI.Controllers
{
    public class ServiceController : ThemeControllerBase
    {
        private ICacheThemeViewRepository _themeRepository = ThemeRepositoryFactory.Default.GetCachedThemeViewRepository();
        private IThemeManageRepository _themeManageRepository = ThemeRepositoryFactory.Default.GetThemeManageRepository();

        public ActionResult GetCss()
        {
            IpDefender.RemoveIp();
            return Redirect("/Content/css/style.css");
        }

        public ActionResult DownloadTheme(int themeId, string themeName)
        {
            Theme theme = _themeRepository.GetTheme(themeId);
            if (theme != null)
            {
                theme.Downloads++;
                _themeManageRepository.IncreaseDownloads(themeId, 1);
                _themeManageRepository.InsertDownloadHistory(themeId, Request.UserHostAddress);
                return Redirect("http://resource.ithemesky.com/" + theme.DownloadUrl);
            }
            else
            {
                return Redirect("/404.html");
            }
        }
        public ActionResult DownloadThemePic(int themeId, string themeName)
        {
            Theme theme = _themeRepository.GetTheme(themeId);
            if (theme != null)
            {
                return Redirect("http://resource.ithemesky.com/" + theme.ThumbnailName);
            }
            else
            {
                return Redirect("/404.html");
            }
        }
        public ActionResult ParseThemeZip(int themeId)
        {
            IpDefender.RemoveIp();
            Theme theme = _themeRepository.GetTheme(themeId);
            if (theme != null)
            {
                string path = Server.MapPath("/Cache/" + themeId);
                if (System.IO.Directory.Exists(path))
                {
                    return Content(System.IO.File.ReadAllText(System.IO.Path.Combine(path, themeId + ".xml")));
                }
                else
                {
                    ThemeZipHelper.ExtractZip(Server.MapPath("/" + theme.DownloadUrl), path);
                    string content = ThemeZipHelper.WriteXML(path);
                    content = content.Replace(Server.MapPath("/"), "/");
                    System.IO.File.WriteAllText(System.IO.Path.Combine(path, themeId + ".xml"), content);
                    return Content(content);
                }
            }
            else
            {
                return Redirect("/404.html");
            }
        }
        [OutputCache(Location = OutputCacheLocation.None)]
        public ActionResult RateTheme(int themeId, int score)
        {
            Theme theme = _themeRepository.GetTheme(themeId);
            if (theme != null)
            {
                theme.RateScore += score;
                theme.RateNumbers++;
                if (_themeManageRepository.RateTheme(themeId, score, -1, Request.UserHostAddress))
                {
                    return Content("1");
                }
                else
                {
                    return Content("-1");
                }
            }
            else
            {
                return Content("-2");
            }
        }
        public ActionResult GetSortThemes(string sort, int displayNumber)
        {
            ThemeSortOption themeSort = sort.ToEnum<ThemeSortOption>(ThemeSortOption.New);
            List<SimpleThemeView> themes = _themeRepository.GetThemes(themeSort, displayNumber);
            return this.PartialView("NormalThemeRepeater", themes);
        }

        public ActionResult GetSuggestThemes(string keyword, int displayNumber)
        {
            IpDefender.RemoveIp();
            int recordCount = 0;
            List<SimpleThemeView> themes = _themeRepository.SearchThemes(keyword, ThemeSortOption.Popular, 1, displayNumber, ref recordCount);
            ViewData["RecordCount"] = recordCount;
            ViewData["SearchKeyword"] = keyword;
            return this.PartialView("SuggestThemeRepeater", themes);
        }

        [OutputCache(Location=OutputCacheLocation.None)]
        public ActionResult GetThemeComments(int themeId, int pageIndex, int pageSize)
        {
            CommentListModel model = new CommentListModel(themeId, pageIndex, pageSize);
            return this.PartialView("ThemeCommentRepeater", model);
        }

        [OutputCache(Location = OutputCacheLocation.None)]
        public ActionResult GetSoftComments(string softIdentify, int pageIndex, int pageSize)
        {
            SoftCommentListModel model = new SoftCommentListModel(softIdentify, pageIndex, pageSize);
            return this.PartialView("SoftCommentRepeater", model);
        }

        [HttpPost]
        public ActionResult AddThemeComment(PostCommentModel postComment)
        {
            if (string.IsNullOrEmpty(postComment.UserName))
            {
                return Content("<script type=\"text/javascript\">alert('UserName is required.');</script>");
            }
            if (string.IsNullOrEmpty(postComment.Content))
            {
                return Content("<script type=\"text/javascript\">alert('Comment Content is required.');</script>");
            }
            IThemeCommentRepository commentRespository = ThemeRepositoryFactory.Default.GetThemeCommentRepository();
            bool result = commentRespository.AddComment(
                new ThemeComment() 
                { 
                    AddTime = DateTime.Now,
                    BuryNumber = 0,
                    CommentId = 0,
                    Content = postComment.Content,
                    DiggNumber = 0,
                    RateType = 0,
                    ThemeId = postComment.ThemeId,
                    Title = postComment.Content.SubStr(100),
                    UpdateTime = DateTime.Now,
                    UserId = 0,
                    UserIp = Request.UserHostAddress,
                    UserMail = postComment.UserMail,
                    UserName = postComment.UserName,
                });
            if (result)
            {
                return Content("<script type=\"text/javascript\">parent.PostCommentSuccess()</script>");
            }
            return Content("<script type=\"text/javascript\">alert('post comment failure,please try again for a while.');</script>");
        }
        [HttpPost]
        public ActionResult AddSoftComment(PostSoftCommentModel postComment)
        {
            if (string.IsNullOrEmpty(postComment.UserName))
            {
                return Content("<script type=\"text/javascript\">alert('UserName is required.');</script>");
            }
            if (string.IsNullOrEmpty(postComment.Content))
            {
                return Content("<script type=\"text/javascript\">alert('Comment Content is required.');</script>");
            }
            ISoftCommentRepository commentRespository = ThemeRepositoryFactory.Default.GetSoftCommentRepository();
            bool result = commentRespository.AddComment(
                new SoftComment()
                {
                    AddTime = DateTime.Now,
                    BuryNumber = 0,
                    CommentId = 0,
                    Content = postComment.Content,
                    DiggNumber = 0,
                    RateType = 0,
                    SoftIdentify = postComment.SoftIdentify,
                    SoftTitle = postComment.SoftTitle,
                    SoftDescription = postComment.SoftDescription,
                    Title = postComment.Content.SubStr(100),
                    UpdateTime = DateTime.Now,
                    UserId = 0,
                    UserIp = Request.UserHostAddress,
                    UserMail = postComment.UserMail,
                    UserName = postComment.UserName,
                });
            if (result)
            {
                return Content("<script type=\"text/javascript\">parent.PostSoftCommentSuccess()</script>");
            }
            return Content("<script type=\"text/javascript\">alert('post comment failure,please try again for a while.');</script>");
        }
        [HttpPost]
        public ActionResult AddThemeSupport(ThemeSupport postSupport)
        {
            if (string.IsNullOrEmpty(postSupport.Mail))
            {
                return Content("<script type=\"text/javascript\">alert('Mail is required.');</script>");
            }
            if (string.IsNullOrEmpty(postSupport.Subject))
            {
                return Content("<script type=\"text/javascript\">alert('Subject is required.');</script>");
            }
            if (string.IsNullOrEmpty(postSupport.Description))
            {
                return Content("<script type=\"text/javascript\">alert('Description is required.');</script>");
            }
            IThemeSupportRepository respository = ThemeRepositoryFactory.Default.GetThemeSupportRepository();
            bool result = respository.AddSupport(
                new ThemeSupport()
                {
                    Description = postSupport.Description,
                    Mail = postSupport.Mail,
                    Name = postSupport.Name,
                    Subject = postSupport.Subject,
                    ThemeId = postSupport.ThemeId,
                    SupportType = postSupport.SupportType,
                    AddTime = DateTime.Now,
                    UserIp = Request.UserHostAddress,
                });
            if (result)
            {
                return Content("<script type=\"text/javascript\">parent.SubmitSupportSuccess()</script>");
            }
            return Content("<script type=\"text/javascript\">alert('post comment failure,please try again for a while.');</script>");
        }

        [HttpPost]
        public ActionResult SubmitTheme(PostThemeModel postTheme)
        {
            if (string.IsNullOrEmpty(postTheme.Title))
            {
                return Content("<script type=\"text/javascript\">alert('Title is required.');</script>");
            }
            if (postTheme.CategoryId <= 0)
            {
                return Content("<script type=\"text/javascript\">alert('CategoryId is required.');</script>");
            }
            if (string.IsNullOrEmpty(Request.Files["ThemeFile"].FileName))
            {
                return Content("<script type=\"text/javascript\">alert('You must select a theme file.');</script>");
            }
            if (!Regex.IsMatch(Request.Files["ThemeFile"].FileName, @".+\.(zip|rar)$", RegexOptions.IgnoreCase))
            {
                return Content("<script type=\"text/javascript\">alert('The format of theme file must be one of zip,rar');</script>");
            }
            if (Request.Files["ThemeFile"].ContentLength > 20 * 1024 * 1024)
            {
                return Content("<script type=\"text/javascript\">alert('The size of selected theme is not bigger than 20M');</script>");
            }
            if (string.IsNullOrEmpty(Request.Files["ThemeThumbnail"].FileName))
            {
                return Content("<script type=\"text/javascript\">alert('You must select a theme screenshot.');</script>");
            }
            if (!Regex.IsMatch(Request.Files["ThemeThumbnail"].FileName, @".+\.(jpg|gif|png)$", RegexOptions.IgnoreCase))
            {
                return Content("<script type=\"text/javascript\">alert('The format of screenshot must be one of jpg,png,gif');</script>");
            }
            if (Request.Files["ThemeThumbnail"].ContentLength > 2 * 1024 * 1024)
            {
                return Content("<script type=\"text/javascript\">alert('The size of selected screenshot is not bigger than 2M');</script>");
            }
            if (string.IsNullOrEmpty(postTheme.AuthorMail))
            {
                return Content("<script type=\"text/javascript\">alert('AuthorMail is required.');</script>");
            }

            //创建主题文件
            postTheme.DownloadUrl = "ThemeFiles/UserUploads/" + Guid.NewGuid().ToString() + ".zip";
            string filePath = Server.MapPath("/" + postTheme.DownloadUrl);
            byte[] buffer = new byte[1024];
            using (Stream stream = Request.Files["ThemeFile"].InputStream)
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    int readCount = 0;
                    while ((readCount = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fs.Write(buffer, 0, readCount);
                    }
                }
            }

            //创建图片文件
            postTheme.ThumbnailName = "ThemeThumbnails/UserUploads/" + Guid.NewGuid().ToString() + "." + Path.GetExtension(Request.Files["ThemeThumbnail"].FileName);
            string thumbnailFile = Server.MapPath("/" + postTheme.ThumbnailName);
            buffer = new byte[1024];
            using (Stream stream = Request.Files["ThemeThumbnail"].InputStream)
            {
                using (FileStream fs = new FileStream(thumbnailFile, FileMode.Create))
                {
                    int readCount = 0;
                    while ((readCount = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fs.Write(buffer, 0, readCount);
                    }
                }
            }

            IThemeManageRepository repository = ThemeRepositoryFactory.Default.GetThemeManageRepository();
            bool result = repository.AddTheme(
                new Theme()
                {
                    AddTime = DateTime.Now,
                    AuthorId = 0,
                    CategoryId = postTheme.CategoryId,
                    CheckerId = 0,
                    CheckState = CheckStateOption.Waitting,
                    CommendIndex = 0,
                    Comments = 0,
                    Description = postTheme.Description,
                    DisplayState = DisplayStateOption.Display,
                    Downloads = 0,
                    DownloadUrl = postTheme.DownloadUrl,
                    FileSize = Request.Files["ThemeFile"].ContentLength,
                    LastMonthDownloads = 0,
                    LastWeekDownloads = 0,
                    ParentCategoryId = 0,
                    RateNumbers = 0,
                    RateScore = 0,
                    Source = SourceOption.IPhoneThemes,
                    ThumbnailName = postTheme.ThumbnailName,
                    Title = postTheme.Title,
                    UpdateTime = DateTime.Now,
                    Views = 0,
                    AuthorMail = postTheme.AuthorMail,
                    AuthorName = postTheme.AuthorName,
                });
            if (result)
            {
                return Content("<script type=\"text/javascript\">parent.SubmitThemeSuccess()</script>");
            }
            return Content("<script type=\"text/javascript\">alert('post comment failure,please try again for a while.');</script>");
        }
    }
}
