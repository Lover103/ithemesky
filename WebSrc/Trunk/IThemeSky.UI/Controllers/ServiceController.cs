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
using System.Text;
using System.Net;
using System.Collections.Specialized;

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

        public ActionResult DownloadTheme(int themeId, string themeName, string downloadCode)
        {
            FullThemeView theme = _themeRepository.GetTheme(themeId);
            if (theme != null)
            {
                if (theme.Price > 0)
                {
                    IOrderRepository orderRepository = ThemeRepositoryFactory.Default.GetOrderRepository();
                    UserOrder order = orderRepository.GetOrder(themeId, downloadCode);
                    if (order != null)
                    {
                        _themeManageRepository.IncreaseDownloads(themeId, 1);
                        _themeManageRepository.InsertDownloadHistory(themeId, Request.UserHostAddress, downloadCode);
                        HttpWebRequest request = HttpWebRequest.Create("http://resource.ithemesky.com/" + theme.DownloadUrl) as HttpWebRequest;
                        WebResponse response = request.GetResponse();
                        string fileName = theme.Title + ".zip";
                        this.Response.AddHeader("Content-Disposition", "attachment;  filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                        return new FileStreamResult(response.GetResponseStream(), "application/zip");
                    }
                    else
                    {
                        //return View("Store/Fail", new PayResultModel() { Theme = theme, Description="Invaild Download Code:" + downloadCode });                    
                        throw new ApplicationException(theme.Title + " is a paid theme,<br />But you typed the invalid download code:" + downloadCode);
                    }
                }
                else
                {
                    theme.Downloads++;
                    _themeManageRepository.IncreaseDownloads(themeId, 1);
                    _themeManageRepository.InsertDownloadHistory(themeId, Request.UserHostAddress, "");
                    return Redirect("http://resource.ithemesky.com/" + theme.DownloadUrl);
                }
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

        [OutputCache(Location = OutputCacheLocation.None)]
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

        public ActionResult GetSiteMapXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"
                <?xml version=""1.0"" encoding=""UTF-8""?>
                <urlset xmlns=""http://www.sitemaps.org/schemas/sitemap/0.9"">
            ");
            sb.Append(BuildUrlNode("http://www.ithemesky.com", "1.0", "always"));
            sb.Append(BuildUrlNode("http://www.ithemesky.com/ispirit", "1.0", "always"));
            sb.Append(BuildUrlNode("http://www.ithemesky.com/ispirit/help", "1.0", "daily"));
            sb.Append(BuildUrlNode("http://www.ithemesky.com/creator", "1.0", "always"));
            sb.Append(BuildUrlNode("http://www.ithemesky.com/help/how-to-use-winterboard", "0.8", "daily"));

            List<SimpleThemeView> themes = ThemeRepositoryFactory.Default.GetCachedThemeViewRepository().GetThemes(ThemeSortOption.Default, int.MaxValue);
            foreach (SimpleThemeView theme in themes)
            {
                sb.Append(BuildUrlNode("http://www.ithemesky.com/iphone-themes/" + theme.Title.Replace("&", "-").Replace(" ", "-") + "/" + theme.ThemeId, "0.9", "daily"));
            }
            sb.Append("</urlset>");
            return Content(sb.ToString());
        }
        private string BuildUrlNode(string url, string priority, string changefreq)
        {
            return string.Format(@"
                <url>
		            <loc>{0}</loc>
		            <lastmod>{1}</lastmod>
		            <changefreq>{3}</changefreq>
		            <priority>{2}</priority>
	            </url>                
            "
                , url
                , DateTime.Now.ToString("yyyy-MM-dd")
                , priority
                , changefreq
                );
        }

        public ActionResult GetPayReturnUrl(int themeId, string mail, string userName)
        {
            double price = 1;
            string checksum = CryptoHelper.MD5_Encrypt(string.Format("{0}|{1}|{2}|{3}|{4}"
                , themeId
                , price
                , mail
                , userName
                , ORDER_CHECKSUM_KEY
                ), "utf-8");
            string result = string.Format("themeId={0}&price={1}&mail={2}&userName={3}&checksum={4}"
                , themeId
                , price
                , mail
                , userName
                , checksum
                );
            return Content(result);
        }

        public ActionResult CheckIPN(int themeId)
        {
            string strFormValues;
            string strNewValue;
            string strResponse;
            //创建回复的 request
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(PAYPAY_GATEWAY);
            //设置 request 的属性
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            byte[] param = Request.BinaryRead(Request.ContentLength);
            strFormValues = Encoding.ASCII.GetString(param);
            //建议在此将接受到的信息记录到日志文件中以确认是否收到 IPN 信息
            logger.Debug("<p>#" + DateTime.Now + "：IPN:" + strFormValues + "</p>");
            strNewValue = strFormValues + "&cmd=_notify-validate";
            req.ContentLength = strNewValue.Length;
            //发送 request
            StreamWriter stOut = new StreamWriter(req.GetRequestStream(),
            System.Text.Encoding.ASCII);
            stOut.Write(strNewValue);
            stOut.Close();
            //回复 IPN 并接受反馈信息
            StreamReader stIn = new StreamReader(req.GetResponse().GetResponseStream());
            strResponse = stIn.ReadToEnd();
            stIn.Close();
            //确认 IPN 是否合法
            if (strResponse == "VERIFIED")
            {
                /*
                mc_gross=1.00&protection_eligibility=Eligible&address_status=confirmed&payer_id=AMZ4SJNSZN2HL&tax=0.00&address_street=1+Main+St&payment_date=18%3A45%3A08+Oct+29%2C+2010+PDT&payment_status=Completed&charset=windows-1252&address_zip=95131&first_name=Test&mc_fee=0.33&address_country_code=US&address_name=Test+User¬ify_version=3.0&custom=&payer_status=verified&business=sanvy_1287488194_biz%40gmail.com&address_country=United+States&address_city=San+Jose&quantity=1&verify_sign=Aa8KFr.XMFu5pAvposAYvnw8fk1IAlYmeNvGug168f-KE73i6y9KrLpz&payer_email=tooqy_1287487314_per%40gmail.com&txn_id=4WY9900120299182X&payment_type=instant&last_name=User&address_state=CA&receiver_email=sanvy_1287488194_biz%40gmail.com&payment_fee=0.33&receiver_id=8PHX32KJJPZBW&txn_type=web_accept&item_name=Chess&mc_currency=USD&item_number=&residence_country=US&test_ipn=1&handling_amount=0.00&transaction_subject=Chess&payment_gross=1.00&shipping=0.00
                */

                //检查付款状态
                //检查 txn_id 是否已经处理过
                //检查 receiver_email 是否是您的 PayPal 账户中的 EMAIL 地址
                //检查付款金额和货币单位是否正确
                //处理这次付款，包括写数据库
                if (Request.Form["payment_status"].Equals("Completed", StringComparison.CurrentCultureIgnoreCase))
                {
                    IOrderRepository repository = ThemeRepositoryFactory.Default.GetOrderRepository();
                    repository.UpdateOrderFromIPN(new UserOrder() 
                    {
                        AddTime = DateTime.Now,
                        Checksum = "FROM IPN",
                        Description = strFormValues,
                        OrderNumber = Request.Form["txn_id"],
                        PayerMail = Request.Form["payer_email"],
                        Price = Convert.ToDouble(Request.Form["mc_gross"]),
                        Status = 1,
                        ThemeId = themeId,
                        UpdateTime = DateTime.Now,
                        UserMail = Request.Form["payer_email"],
                        UserName = Request.Form["first_name"] + " " + Request.Form["last_name"]
                    });
                    logger.Debug("<p>#" + DateTime.Now + "：IPN UPDATE SUCCESS:</p>");
                }

            }
            return Content(strResponse);
        }
    }
}
