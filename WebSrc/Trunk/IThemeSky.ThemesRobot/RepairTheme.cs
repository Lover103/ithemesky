using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using IThemeSky.DataAccess;
using IThemeSky.Model;
using IThemeSky.Library.Util;
using IThemeSky.Library.Data;

namespace IThemeSky.ThemesRobot
{
    public class RepairTheme
    {
        private string _themeDataFile;
        private string _connection;
        private readonly string SAVE_PATH = System.Configuration.ConfigurationSettings.AppSettings["ThemeFilePath"];
        public RepairTheme()
        {
            _connection = ThemeRepositoryFactory.Default.ConnectionProvider.GetReadConnectionString();
            _themeDataFile = Path.Combine(SAVE_PATH, "rewrite.dll");
        }

        public void RepairCode()
        { 
            List<Theme> themes = SerializeHelper.BinaryDeserialize<List<Theme>>(_themeDataFile);
            foreach (Theme theme in themes)
            {
                int result = SqlHelper.ExecuteNonQuery(_connection, System.Data.CommandType.Text, "UPDATE Theme SET Title=N'" + theme.Title.Replace("'", "''") + "',Description=N'" + theme.Description.Replace("'", "''") +  "' WHERE AuthorName='" + theme.AuthorName + "' AND Source=1 AND CheckState<1");
                if (result > 0)
                {
                    Console.WriteLine(theme.Title);
                }
            }
        }

        public void Start()
        {
            try
            {
                List<Theme> themes = SerializeHelper.BinaryDeserialize<List<Theme>>(_themeDataFile);
                foreach (Theme theme in themes)
                {
                    try
                    {
                        string fileName = GetFileName("ThemeFiles/201004/" + theme.Title + ".zip");
                        if (File.Exists(fileName))
                        {
                            string newName = Path.Combine(Path.GetDirectoryName(fileName), "ITheme_" + theme.AuthorName + ".zip");
                            File.Move(fileName, newName);
                            SqlHelper.ExecuteNonQuery(_connection, System.Data.CommandType.Text, "UPDATE Theme SET DownloadUrl='" + ("ThemeFiles/201004/ITheme_" + theme.AuthorName + ".zip") + "' WHERE AuthorName='" + theme.AuthorName + "' AND Source=1");
                            Console.WriteLine("转移文件：" + fileName);
                        }

                        string thumbnailName = GetFileName("ThemeThumbnails/201004/" + theme.Title + ".jpg");
                        string thumbnailName112x168 = thumbnailName.Replace(".jpg", "_112x168.jpg");
                        if (File.Exists(thumbnailName) && File.Exists(thumbnailName112x168))
                        {
                            string newThumbnailName = Path.Combine(Path.GetDirectoryName(thumbnailName), "ITheme_" + theme.AuthorName + ".jpg");
                            string newThumbnailName112x168 = Path.Combine(Path.GetDirectoryName(thumbnailName112x168), "ITheme_" + theme.AuthorName + "_112x168.jpg");
                            File.Move(thumbnailName, newThumbnailName);
                            File.Move(thumbnailName112x168, newThumbnailName112x168);
                            SqlHelper.ExecuteNonQuery(_connection, System.Data.CommandType.Text, "UPDATE Theme SET ThumbnailName='" + ("ThemeThumbnails/201004/ITheme_" + theme.AuthorName + ".jpg") + "' WHERE AuthorName='" + theme.AuthorName + "' AND Source=1");
                            Console.WriteLine("转移图片：" + thumbnailName);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        File.AppendAllText(SAVE_PATH + "Error.txt", theme.AuthorName + "\r\n" + ex.ToString() + "\r\n\r\n");
                    }
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(SAVE_PATH + "Error.txt", ex.ToString() + "\r\n\r\n");
            }
        }

        private string GetFileName(string oldName)
        {
            oldName = oldName.Replace(" ", "_").Replace("+", "_");
            return Path.Combine(SAVE_PATH, oldName);
        }
    }
}
