using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IThemeSky.DataAccess;
using System.Data;
using IThemeSky.Library.Data;
using System.IO;

namespace IThemeSky.ThemesRobot
{
    class ThemeFileChecker
    {
        private static readonly string SAVE_PATH = System.Configuration.ConfigurationSettings.AppSettings["ThemeFilePath"];
        public static void Check()
        {
            //Dictionary<string, string> dic = new Dictionary<string, string>();
            string connection = ThemeRepositoryFactory.Default.ConnectionProvider.GetReadConnectionString();
            //using (IDataReader reader = SqlHelper.ExecuteReader(connection, CommandType.Text, "SELECT DownloadUrl FROM Theme"))
            //{
            //    while (reader.Read())
            //    {
            //        dic.Add(reader[0].ToString(), reader[0].ToString());
            //    }
            //}
            int i = 0;
            string[] arrFile = Directory.GetFiles(SAVE_PATH + "ThemeFiles/");
            foreach (string file in arrFile)
            {
                if (file.Contains('-'))
                {
                    File.Move(file, file.Replace("-", "_"));
                }
//                FileInfo fileInfo = new FileInfo(file);
//                if (fileInfo.Length == 1532)
//                {
//                    int themeId = 0;
//                    using (IDataReader reader = SqlHelper.ExecuteReader(connection, CommandType.Text, "SELECT ThemeId FROM Theme where DownloadUrl = '" + file.Replace(SAVE_PATH, "") + "'"))
//                    {
//                        if (reader.Read())
//                        {
//                            themeId = Convert.ToInt32(reader[0]);
//                        }
//                    }
//                    SqlHelper.ExecuteNonQuery(connection, CommandType.Text, string.Format(@"delete from themeTagMap where themeId = {0}
//delete from theme where themeId = {0}", themeId));
//                    File.Delete(file);
//                    Console.WriteLine(file + ":2k文件删除");
//                    i++;
//                }
//                else
//                {
//                    SqlHelper.ExecuteNonQuery(connection, CommandType.Text, string.Format(@"update theme set FileSize={1} where DownloadUrl='{0}'", file.Replace(SAVE_PATH, ""), fileInfo.Length));
//                }
                //if (!dic.ContainsKey(file.Replace(SAVE_PATH, "")))
                //{
                //    File.Delete(file);
                //    Console.WriteLine("删除文件" + file);
                //    i++;
                //}
            }
            Console.WriteLine("共计删除" + i + "个文件");
        }
    }
}
