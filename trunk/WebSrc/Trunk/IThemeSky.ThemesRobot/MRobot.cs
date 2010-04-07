using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IThemeSky.Library.Data;
using System.Data;
using IThemeSky.Model;
using IThemeSky.Library.Util;
using System.IO;
using System.Net;
using IThemeSky.DataAccess;

namespace IThemeSky.ThemesRobot
{
    public class MRobot
    {
        private readonly string SAVE_PATH = System.Configuration.ConfigurationSettings.AppSettings["ThemeFilePath"];
        private WebClient _webClient = new WebClient();
        public void Build()
        {
            string conn = "Data Source=10.1.242.18;Initial Catalog=NewResourceDB;Persist Security Info=True;User ID=nddblzz;Password=uNm46KP8xSUwfZUK";
            string cmdText = @"SELECT * FROM [dbo].[View_ChannelThemes] where f_platform = 1 and ChannelId = 0";
            List<Theme> themes = new List<Theme>();
            using (IDataReader reader = SqlHelper.ExecuteReader(conn, System.Data.CommandType.Text, cmdText))
            {
                while (reader.Read())
                {
                    themes.Add(new Theme()
                    {
                        AddTime = Convert.ToDateTime(reader["f_upload_time"]),
                        AuthorId = 0,
                        CategoryId = Convert.ToInt32(reader["f_cateid"]),
                        CheckerId = 0,
                        CheckState = CheckStateOption.Waitting,
                        CommendIndex = Convert.ToInt32(reader["f_tuijian"]),
                        Comments = 0,
                        Description = reader["f_summary"].ToString(),
                        DisplayState = DisplayStateOption.Display,
                        Downloads = Convert.ToInt32(reader["f_downnum"]),
                        FileSize = Convert.ToInt64(reader["f_size"]),
                        LastMonthDownloads = 0,
                        LastWeekDownloads = 0,
                        ParentCategoryId = 0,
                        Source = SourceOption.M,
                        ThumbnailName = "http://image.91.com/sj91/upload/" + string.Format("{0}{1}.{2}", reader["f_path"].ToString(), reader["f_picname"].ToString(), reader["f_ext"].ToString()),
                        Title = reader["f_name"].ToString(),
                        UpdateTime = DateTime.Now,
                        Views = Convert.ToInt32(reader["f_viewnum"]),
                        DownloadUrl = string.IsNullOrEmpty(reader["f_downurl"].ToString()) ? reader["f_downurl2"].ToString() : "http://www.91rb.com/theme/iphone/" + reader["f_downurl"].ToString(),
                        RateNumbers = 0,
                        RateScore = 0,
                        AuthorName = reader["f_id"].ToString(),
                    });
                }
            }
            SerializeHelper.BinarySerialize<List<Theme>>(themes, Path.Combine(SAVE_PATH, "rewrite.dll"));
        }

        public void Start()
        {
            List<Theme> themes = SerializeHelper.BinaryDeserialize<List<Theme>>(Path.Combine(SAVE_PATH, "rewrite.dll"));
            int index = 1;
            foreach (Theme sourceTheme in themes)
            {
                try
                {
                    Theme theme = new Theme()
                    {
                        AddTime = sourceTheme.AddTime,
                        AuthorId = 0,
                        CategoryId = sourceTheme.CategoryId,
                        CheckerId = 0,
                        CheckState = CheckStateOption.Waitting,
                        CommendIndex = sourceTheme.CommendIndex,
                        Comments = 0,
                        Description = sourceTheme.Description,
                        DisplayState = DisplayStateOption.Display,
                        Downloads = sourceTheme.Downloads,
                        DownloadUrl = GetFileName("ThemeFiles/201004/" + sourceTheme.Title + ".zip"),
                        FileSize = sourceTheme.FileSize,
                        LastMonthDownloads = 0,
                        LastWeekDownloads = 0,
                        ParentCategoryId = 0,
                        RateNumbers = 0,
                        RateScore = 0,
                        Source = SourceOption.M,
                        ThumbnailName = GetFileName("ThemeThumbnails/201004/" + sourceTheme.Title + ".jpg"),
                        Title = sourceTheme.Title,
                        UpdateTime = sourceTheme.AddTime,
                        Views = sourceTheme.Downloads,
                        AuthorName = sourceTheme.AuthorName,
                    };
                    _webClient.Headers.Add(HttpRequestHeader.Referer, "http://m.91.com");
                    _webClient.DownloadFile(sourceTheme.DownloadUrl, Path.Combine(SAVE_PATH, theme.DownloadUrl));
                    _webClient.DownloadFile(sourceTheme.ThumbnailName, Path.Combine(SAVE_PATH, theme.ThumbnailName));
                    ImageHelper.MakeThumbnail(Path.Combine(SAVE_PATH, theme.ThumbnailName), Path.Combine(SAVE_PATH, theme.ThumbnailName.Replace(".jpg", "_112x168.jpg")), 112, 168, "wh");
                    IThemeManageRepository repository = ThemeRepositoryFactory.Default.GetThemeManageRepository();
                    repository.AddTheme(theme);
                    Console.WriteLine("#" + index + ":" + theme.Title);
                }
                catch (Exception ex)
                {
                    File.AppendAllText(SAVE_PATH + "Error.txt", sourceTheme.AuthorName + "\r\n" + ex.ToString() + "\r\n\r\n");
                    File.AppendAllText(SAVE_PATH + "ErrorUrl.txt", sourceTheme.AuthorName + "\r\n");
                }
                index++;
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
