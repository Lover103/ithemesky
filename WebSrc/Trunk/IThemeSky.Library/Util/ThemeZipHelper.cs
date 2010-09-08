using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;

namespace IThemeSky.Library.Util
{
    public static class ThemeZipHelper
    {
        public static bool ExtractZip(string filename, string directory)
        {
            FastZip fz = new FastZip();
            fz.CreateEmptyDirectories = true;
            fz.ExtractZip(filename, directory, "");
            fz = null;
            return true;
        }

        public static string WriteXML(string directory)
        {
            directory = SelectRightThemePath(directory);
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            sb.AppendLine();
            sb.Append("<Theme>");
            sb.AppendLine();
            if (File.Exists(directory + "\\Wallpaper.png"))
            {
                sb.AppendFormat("<Wallpaper Image=\"{0}/Wallpaper.png\" />", directory);
                sb.AppendLine();
            }

            if (File.Exists(directory + "\\Dock.png"))
            {
                sb.AppendFormat("<Dock Image=\"{0}/Dock.png\" />", directory);
                sb.AppendLine();
            }

            if (File.Exists(directory + "\\StatusBar.png"))
            {
                sb.AppendFormat("<StatusBar Image=\"{0}/StatusBar.png\" />", directory);
                sb.AppendLine();
            }
            sb.Append("<Icons>");
            sb.AppendLine();

            if (Directory.Exists(directory + "\\Icons"))
            {
                foreach (string f in Directory.GetFiles(directory + "\\Icons"))
                {
                    if (Path.GetExtension(f) == ".png")
                    {
                        sb.AppendFormat("<Icon Text=\"{0}\" Image=\"{1}/Icons/{0}.png\" />", Path.GetFileNameWithoutExtension(f), directory);
                        sb.AppendLine();
                    }
                }
            }
            sb.Append("</Icons>");
            sb.AppendLine();
            sb.Append("</Theme>");
            return sb.ToString();
        }

        #region 选择正确的主题包路径
        /// <summary>
        /// 选择正确的主题包路径
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        static string SelectRightThemePath(string dir)
        {
            string[] subfiles = Directory.GetFiles(dir);
            foreach (string file in subfiles)
            {
                if (file.EndsWith("\\LockBackground.png", true, null))
                {
                    return dir;
                }
            }

            string[] subdirs = Directory.GetDirectories(dir);
            if (subdirs.Length == 1)
            {
                if (subdirs[0].EndsWith("\\Icons", true, null))
                {
                    return dir;
                }
                else
                {
                    return SelectRightThemePath(subdirs[0]);
                }
            }
            else
            {
                foreach (string subd in subdirs)
                {
                    if (subd.EndsWith("\\Icons", true, null))
                    {
                        return dir;
                    }
                }
            }
            return string.Empty;
        }
        #endregion
    }
}
