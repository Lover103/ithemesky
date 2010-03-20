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

namespace IThemeSky.UI.Controllers
{
    public class ServiceController : Controller
    {
        private ICacheThemeViewRepository _themeRepository = ThemeRepositoryFactory.Default.GetCachedThemeViewRepository();

        public ActionResult Download(int themeId)
        {
            return Content("下载了" + themeId);
        }

        public ActionResult GetSortThemes(string sort, int displayNumber)
        {
            ThemeSortOption themeSort = sort.ToEnum<ThemeSortOption>(ThemeSortOption.New);
            List<SimpleThemeView> themes = _themeRepository.GetThemes(themeSort, displayNumber);
            return this.PartialView("NormalThemeRepeater", themes);
        }

        public ActionResult GetSuggestThemes(string keyword, int displayNumber)
        { 
            int recordCount = 0;
            List<SimpleThemeView> themes = _themeRepository.SearchThemes(keyword, ThemeSortOption.Popular, 1, displayNumber, ref recordCount);
            ViewData["RecordCount"] = recordCount;
            return this.PartialView("SuggestThemeRepeater", themes);
        }

        [OutputCache(Location=OutputCacheLocation.None)]
        public ActionResult GetThemeComments(int themeId, int pageIndex, int pageSize)
        {
            CommentListModel model = new CommentListModel(themeId, pageIndex, pageSize);
            return this.PartialView("ThemeCommentRepeater", model);
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
    }
}
