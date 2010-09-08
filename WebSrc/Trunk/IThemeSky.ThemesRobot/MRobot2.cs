using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IThemeSky.Library.Data;
using System.Data;
using IThemeSky.Model;
using IThemeSky.Library.Util;
using IThemeSky.Library.Extensions;
using System.IO;
using System.Net;
using IThemeSky.DataAccess;
using System.Text.RegularExpressions;
using System.Xml.XPath;

namespace IThemeSky.ThemesRobot
{
    public class MRobot2
    {
        private readonly string SAVE_PATH = System.Configuration.ConfigurationSettings.AppSettings["ThemeFilePath"];
        private WebClient _webClient = new WebClient();


        public void Start()
        {
            XPathDocument document = new XPathDocument("http://panda.sj.91.com/Service/GetResourceData.aspx?mt=1&qt=103&ps=5000&pi=1");
            var navRoot = document.CreateNavigator();
            XPathNodeIterator items = navRoot.Select("result/data/item");
            int index = 1;
            while (items.MoveNext())
            {
                try
                {
                    var currItem = items.Current;
                    if (currItem.SelectSingleNode("id").Value.ToInt32() > 18045)
                    {
                        Theme theme = new Theme()
                        {
                            AddTime = DateTime.Now,
                            AuthorId = 0,
                            CategoryId = currItem.SelectSingleNode("type").Value.ToInt32(),
                            CheckerId = 0,
                            CheckState = CheckStateOption.Waitting,
                            CommendIndex = 1,
                            Comments = 0,
                            Description = currItem.SelectSingleNode("descript").Value,
                            DisplayState = DisplayStateOption.Display,
                            Downloads = 0,
                            DownloadUrl = GetFileName("ThemeFiles/201007/" + (3860+index) + ".zip"),
                            FileSize = currItem.SelectSingleNode("size").Value.ToInt64() * 1024,
                            LastMonthDownloads = 0,
                            LastWeekDownloads = 0,
                            ParentCategoryId = 0,
                            RateNumbers = 0,
                            RateScore = 0,
                            Source = SourceOption.M,
                            ThumbnailName = GetFileName("ThemeThumbnails/201007/" + (3860 + index) + ".jpg"),
                            Title = currItem.SelectSingleNode("name").Value,
                            UpdateTime = DateTime.Now,
                            Views = 0,
                            AuthorName = currItem.SelectSingleNode("id").Value,
                        };
                        _webClient.Headers.Add(HttpRequestHeader.Referer, "http://m.91.com");
                        _webClient.DownloadFile("http://mobile.91.com/t" + theme.AuthorName, Path.Combine(SAVE_PATH, theme.DownloadUrl));
                        _webClient.DownloadFile(currItem.SelectSingleNode("previewUrl").Value, Path.Combine(SAVE_PATH, theme.ThumbnailName));
                        ImageHelper.MakeThumbnail(Path.Combine(SAVE_PATH, theme.ThumbnailName), Path.Combine(SAVE_PATH, theme.ThumbnailName.Replace(".jpg", "_112x168.jpg")), 112, 168, "wh");
                        IThemeManageRepository repository = ThemeRepositoryFactory.Default.GetThemeManageRepository();
                        repository.AddTheme(theme);
                        Console.WriteLine("#" + index + ":" + theme.Title);
                        File.AppendAllText(SAVE_PATH + "Log.txt", index + "\r\n");
                        index++;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    File.AppendAllText(SAVE_PATH + "Error.txt", items.Current.SelectSingleNode("id").Value + "\r\n" + ex.ToString() + "\r\n\r\n");
                    File.AppendAllText(SAVE_PATH + "ErrorUrl.txt", items.Current.SelectSingleNode("id").Value + "\r\n");
                }
            }
        }

        private string GetFileName(string oldName)
        {
            oldName = oldName.Replace(" ", "_").Replace("+", "_");
            string newName = oldName;
            string file = Path.Combine(SAVE_PATH, oldName);
            int i = 1;
            while (File.Exists(file))
            {
                int index = oldName.LastIndexOf(".");
                newName = oldName.Substring(0, index) + "_" + i + "." + oldName.Substring(index + 1);
                file = Path.Combine(SAVE_PATH, newName);
                i++;
            }
            return newName;
        }
    }
}
