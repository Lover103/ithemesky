using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IThemeSky.DataAccess;
using System.Data;
using IThemeSky.Library.Data;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace IThemeSky.ThemesRobot
{
    public class ThemeFileChecker
    {
        private static readonly string SAVE_PATH = System.Configuration.ConfigurationSettings.AppSettings["ThemeFilePath"];
        public static void Check()
        {
            string cmdText = "SELECT * FROM Theme";
            string connection = ThemeRepositoryFactory.Default.ConnectionProvider.GetReadConnectionString();
            using (IDataReader reader = SqlHelper.ExecuteReader(connection, CommandType.Text, cmdText))
            {
                while (reader.Read())
                {
                    try
                    {
                        string themeFile = Path.Combine(SAVE_PATH, reader["DownloadUrl"].ToString());
                        if (File.Exists(themeFile))
                        {
                            if (!ThemeIsOK(themeFile))
                            {
                                cmdText = "UPDATE Theme SET Title = Title + '(D)', CheckState=-1, DisplayState=-1 WHERE ThemeId=" + reader["ThemeId"].ToString();
                                SqlHelper.ExecuteNonQuery(connection, CommandType.Text, cmdText);
                                Console.WriteLine("#" + reader["ThemeId"] + ":" + reader["Title"].ToString() + "," + reader["DownloadUrl"].ToString());
                                File.AppendAllText(Path.Combine(Environment.CurrentDirectory, "DY.txt"), "#" + reader["ThemeId"] + ":" + reader["Title"].ToString() + "," + reader["DownloadUrl"].ToString() + "\r\n");
                            }
                        }
                        else
                        {
                            cmdText = "UPDATE Theme SET Title = Title + '(F)', CheckState=-1, DisplayState=-1 WHERE ThemeId=" + reader["ThemeId"].ToString();
                            SqlHelper.ExecuteNonQuery(connection, CommandType.Text, cmdText);
                            Console.WriteLine("NotFound：#" + reader["ThemeId"] + ":" + reader["Title"].ToString() + "," + reader["DownloadUrl"].ToString());
                            File.AppendAllText(Path.Combine(Environment.CurrentDirectory, "NotFound.txt"), "#" + reader["ThemeId"] + ":" + reader["Title"].ToString() + "," + reader["DownloadUrl"].ToString() + "\r\n");
                        }
                    }
                    catch (Exception ex)
                    {
                        cmdText = "UPDATE Theme SET Title = Title + '(Z)', CheckState=-1, DisplayState=-1 WHERE ThemeId=" + reader["ThemeId"].ToString();
                        SqlHelper.ExecuteNonQuery(connection, CommandType.Text, cmdText);
                        File.AppendAllText(Path.Combine(Environment.CurrentDirectory, "Error.txt"), "#" + reader["ThemeId"] + ":" + reader["Title"].ToString() + "," + reader["DownloadUrl"].ToString() + "\r\n" + ex.ToString() + "\r\n\r\n\r\n\r\n");
                    }
                }
            }
        }

        public static bool ThemeIsOK(string zipfile)
        {
            bool hasbg = false;
            bool hasicons = false;

            ZipInputStream s = null;
            ZipEntry theEntry = null;

            string fileName;
            try
            {
                s = new ZipInputStream(File.OpenRead(zipfile));
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    if (theEntry.Name != String.Empty)
                    {
                        fileName = theEntry.Name.Replace("/", "\\");

                        if (fileName.ToLower().Contains("\\icons\\") || fileName.ToLower().StartsWith("icons\\"))
                        {
                            hasicons = true;
                        }

                        //判断文件路径是否是文件夹
                        if (fileName.EndsWith("/") || fileName.EndsWith("\\"))
                        {
                            //文件夹

                        }
                        else
                        {
                            //文件
                            if (Path.GetFileName(fileName).Equals("Wallpaper.png", StringComparison.CurrentCultureIgnoreCase))
                            {
                                hasbg = true;
                            }
                        }
                    }

                    if (hasbg && hasicons)
                    {
                        break;
                    }
                }
            }
            finally
            {
                if (theEntry != null)
                {
                    theEntry = null;
                }
                if (s != null)
                {
                    s.Close();
                    s = null;
                }
            }
            return hasbg && hasicons;
        }
    }
}
