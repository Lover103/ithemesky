using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace iSprite
{
    public class Utility
    {
        static Utility()
        {
        }
        public static bool IsDriver(string path)
        {
            if (path.Length <= 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static iPhoneFileTypeOption GetFileType(string path)
        {
            iPhoneFileTypeOption fileType = iPhoneFileTypeOption.FILE_UNKNOWN;
            string extname = Path.GetExtension(path);
            switch (extname)
            {
                case ".png":
                case ".jpg":
                case ".jpeg":
                case ".gif":
                    fileType = iPhoneFileTypeOption.FILE_IMAGE;
                    break;
                case ".strings":
                case ".plist":
                    fileType = iPhoneFileTypeOption.FILE_PList;
                    break;
                case ".conf":
                case ".txt":
                case ".script":
                case ".html":
                case ".css":
                case ".js":
                    fileType = iPhoneFileTypeOption.FILE_TEXT;
                    break;
                case ".db":
                case ".sqlite":
                    fileType = iPhoneFileTypeOption.FILE_DATABASE;
                    break;
                case ".aiff":
                case ".amr":
                case ".aif":
                case ".caf":
                    fileType = iPhoneFileTypeOption.FILE_AUDIO;
                    break;
                case ".m4r":
                    fileType = iPhoneFileTypeOption.FILE_RINGTONE;
                    break;
                case ".m4a":
                case ".m4p":
                case ".mp3":
                case ".aac":
                    fileType = iPhoneFileTypeOption.FILE_MUSIC;
                    break;
            }
            return fileType;
        }

        public static string CombinePath(string path1, string path2, char DirectorySeparatorChar)
        {
            return path1.TrimEnd(DirectorySeparatorChar)+DirectorySeparatorChar+path2;
        }
        public static string FormatFileSize(ulong FileSize)
        {
            //显示单位为KB
             if (FileSize <= 1048576)
            {
                return string.Format("{0:F0} KB", FileSize / 1024);
            }
                //大于1M，则显示单位为MB
            else if ((FileSize >= 1048576) && (FileSize <= 1073741824))
            {
                return string.Format("{0:F0} MB", FileSize / 1048576);
            }
                //大于1G，则显示单位为G
            else if (FileSize >= 1073741824)
            {
                return string.Format("{0:F0} GB", FileSize / 1073741824);
            }
            return "";
        }
    }
}
