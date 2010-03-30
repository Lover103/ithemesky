using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using IThemeSky.Model;
using IThemeSky.DataAccess;
using IThemeSky.Library.Util;
using System.Diagnostics;

namespace IThemeSky.ThemesRobot
{
    public class MBThemeRobot
    {
        private readonly string SAVE_PATH = System.Configuration.ConfigurationSettings.AppSettings["ThemeFilePath"];
        private const string BASE_URL = "http://www.mbtheme.com/";
        private const string LIST_URL = "http://www.mbtheme.com/Iphone_theme/87-{0}.html";
        private WebClient _webClient = new WebClient();

        public MBThemeRobot()
        {
            if (!Directory.Exists(Path.Combine(SAVE_PATH, "ThemeFiles")))
            {
                Directory.CreateDirectory(Path.Combine(SAVE_PATH, "ThemeFiles"));
            }
            if (!Directory.Exists(Path.Combine(SAVE_PATH, "ThemeThumbnails")))
            {
                Directory.CreateDirectory(Path.Combine(SAVE_PATH, "ThemeThumbnails"));
            }
        }
        public void AnalyseList(int pageIndex)
        {
            try
            {
                Console.WriteLine("开始抓取列表第" + pageIndex + "页：");
                string url = string.Format(LIST_URL, pageIndex);

                string content = _webClient.DownloadString(url);
                string pattern = @"<img alt=""[^""]+"" src=""(?<ThemeImage>[^""]+)"" border=""0"" /></a><br />[^<]*<a href='(?<DetailUrl>[^']+)' title='(?<Title>[^']+)'>[^<]+</a>";
                MatchCollection mc = Regex.Matches(content, pattern);
                foreach (Match match in mc)
                {
                    AnalyseThemeInfo(match);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("生成第" + pageIndex + "页异常");
                File.AppendAllText(SAVE_PATH + "Error.txt", "抓取第" + pageIndex + "页异常\r\n" + ex.ToString() + "\r\n\r\n");
            }
            //HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
            //using (WebResponse response = request.GetResponse())
            //{
            //    using (Stream stream = response.GetResponseStream())
            //    { 
            //        Encoding.UTF8.GetString(stream.Read(
            //    }
            //    response.Close();
            //}
        }

        public void AnalyseThemeInfo(Match match)
        {

            try
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                string detailContent = _webClient.DownloadString(BASE_URL + match.Groups["DetailUrl"].Value);
                Match m = Regex.Match(detailContent, @"uploads/userup/\d+/[^.]+\.jpg");
                string imageUrl = BASE_URL + m.Value;

                m = Regex.Match(detailContent, @"<p>(?<Content>[^<]*)</p>");
                string content = m.Groups["Content"].Value;

                m = Regex.Match(detailContent, @"/plus/download\.php\?open=\d+&aid=\d+&cid=\d+");
                string downUrl = BASE_URL + m.Value;
                string downPageContent = _webClient.DownloadString(downUrl);
                m = Regex.Match(downPageContent, @"<div class=""downloadlinks""><a href='(?<ThemeFileUrl>[^']+)' style='color:red' target='_blank'>");
                string themeFileUrl = BASE_URL + m.Groups["ThemeFileUrl"].Value;

                int downloads = (new Random()).Next(10, 500);
                Theme theme = new Theme()
                {
                    AddTime = DateTime.Now,
                    AuthorId = 0,
                    CategoryId = 0,
                    CheckerId = 0,
                    CheckState = CheckStateOption.CheckSuccess,
                    CommendIndex = 2,
                    Comments = 0,
                    Description = content,
                    DisplayState = DisplayStateOption.Display,
                    Downloads = downloads,
                    DownloadUrl = GetFileName("ThemeFiles/" + match.Groups["Title"].Value.Replace(" ", "-") + ".rar"),
                    FileSize = 0,
                    LastMonthDownloads = downloads,
                    LastWeekDownloads = downloads,
                    ParentCategoryId = 0,
                    RateNumbers = 0,
                    RateScore = 0,
                    Source = SourceOption.IPhoneThemes,
                    ThumbnailName = GetFileName("ThemeThumbnails/" + match.Groups["Title"].Value.Replace(" ", "-") + ".jpg"),
                    Title = match.Groups["Title"].Value,
                    UpdateTime = DateTime.Now,
                    Views = downloads * 2,
                };
                _webClient.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows; U; Windows NT 6.1; zh-CN; rv:1.9.2) Gecko/20100115 Firefox/3.6");
                _webClient.Headers.Add(HttpRequestHeader.Referer, downUrl);
                _webClient.Headers.Add("Cookie", "UniProc1224141825=112691391510413393; bsau=12691391673546121804; bsas=12691391673556384879; UserInteraction4=KonaBase; AJSTAT_ok_pages=2; AJSTAT_ok_times=1; __utma=106534879.1209003502.1269139179.1269139179.1269139179.1; __utmb=106534879.2.10.1269139179; __utmc=106534879; __utmz=106534879.1269139193.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none)");
                downPageContent = _webClient.DownloadString(themeFileUrl);

                m = Regex.Match(downPageContent, @"""(?<ThemeFileUrl>[^""]+)""");
                themeFileUrl = BASE_URL + m.Groups["ThemeFileUrl"].Value;

                _webClient.DownloadFile(themeFileUrl, Path.Combine(SAVE_PATH, theme.DownloadUrl));
                _webClient.DownloadFile(imageUrl, Path.Combine(SAVE_PATH, theme.ThumbnailName));
                //生成缩略图
                ImageHelper.MakeThumbnail(Path.Combine(SAVE_PATH, theme.ThumbnailName), Path.Combine(SAVE_PATH, theme.ThumbnailName.Replace(".jpg", "_112x168.jpg")), 112, 168, "wh");

                IThemeManageRepository repository = ThemeRepositoryFactory.Default.GetThemeManageRepository();
                repository.AddTheme(theme);

                MatchCollection mc = Regex.Matches(detailContent, @"<a href='/tag\.php\?/(?<TagName>[^/]+)/'>");
                foreach (Match mTag in mc)
                {
                    repository.MappingThemeTag(theme.ThemeId, mTag.Groups["TagName"].Value);
                }
                watch.Stop();
                Console.WriteLine("抓取" + match.Groups["DetailUrl"].Value + "完成，用时：" + watch.Elapsed);
            }
            catch (Exception ex)
            {
                File.AppendAllText(SAVE_PATH + "Error.txt", match.Groups["DetailUrl"].Value + "\r\n" + ex.ToString() + "\r\n\r\n");
                File.AppendAllText(SAVE_PATH + "ErrorUrl.txt", match.Groups["DetailUrl"].Value + "\r\n");
            }
        }

        private string GetFileName(string oldName)
        {
            string newName = oldName;
            string file = Path.Combine(SAVE_PATH, oldName);
            int i = 1;
            while (File.Exists(file))
            {
                int index = oldName.LastIndexOf(".");
                newName = oldName.Substring(0, index) + "_" + i + "." + oldName.Substring(index+1);
                file = Path.Combine(SAVE_PATH, newName);
                i++;
            }
            return newName;
        }
    }
}
