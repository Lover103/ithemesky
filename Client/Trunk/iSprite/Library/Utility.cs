using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
using System.Net;
using Manzana;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace iSprite
{
    public class Utility
    {
        static Utility()
        {
        }
        internal static  string GetDirName(string path)
        {
            DirectoryInfo info = new DirectoryInfo(path);
            return info.Name;
        }
        internal static bool IsDriver(string path)
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

        internal static bool DownloadFile(string url, string destFilePath)
        { 
            return DownloadFile(url, destFilePath, null);
        }

        internal static bool DownloadFile(string url, string destFilePath, FileProgressHandler progresshandler)
        {
            bool returnCode = false;
            Stream inStream = null;
            FileStream filestream = null;
            bool cancelDownload = false;
            try
            {
                HttpWebRequest req = (HttpWebRequest)System.Net.WebRequest.Create(url);
                req.Referer = "http://" + new Uri(url).Host;
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

                inStream = resp.GetResponseStream();

                filestream = new FileStream(destFilePath, FileMode.OpenOrCreate, FileAccess.Write);


                int length = 102400;
                ulong totalfileSize = (ulong)resp.ContentLength;
                ulong finishSize = 0;
                byte[] buffer = new byte[length];
                int bytesread = 0;
                DateTime begintime = DateTime.Now;
                int speed = 0;
                double timeElapse = 0;
                while ((bytesread = inStream.Read(buffer, 0, length)) > 0)
                {
                    filestream.Write(buffer, 0, bytesread);
                    finishSize += (ulong)bytesread;

                    if (progresshandler != null)
                    {
                        timeElapse = (DateTime.Now - begintime).TotalSeconds;
                        if (timeElapse >= 1.0)
                        {
                            speed = (int)Math.Round((double)(finishSize * 1.0 / timeElapse));
                        }

                        progresshandler(FileProgressMode.Internet2PC, totalfileSize, finishSize, speed, timeElapse, Path.GetFileName(destFilePath), ref cancelDownload);
                        if (cancelDownload)
                        {
                            break;
                        }
                    }
                }

                returnCode = !cancelDownload;
            }
            catch //(Exception ex)
            {
                returnCode = false;
            }
            finally
            {
                if (inStream != null)
                {
                    inStream.Close();
                }
                if (filestream != null)
                {
                    filestream.Close();
                }
            }
            return returnCode;
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
                ulong k = (FileSize / 1024);
                if (k <= 0)
                {
                    k = 1;
                }
                return string.Format("{0:F0} KB", k);
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

        public static string FormatFileSizeFloat(ulong FileSize)
        {
            //显示单位为KB
            if (FileSize <= 1048576)
            {
                return string.Format("{0:F2} KB", FileSize * 1.0 / 1024);
            }
            //大于1M，则显示单位为MB
            else if ((FileSize >= 1048576) && (FileSize <= 1073741824))
            {
                return string.Format("{0:F2} MB", FileSize * 1.0 / 1048576);
            }
            //大于1G，则显示单位为G
            else if (FileSize >= 1073741824)
            {
                return string.Format("{0:F2} GB", FileSize * 1.0 / 1073741824);
            }
            return "";
        }

        [DllImport("user32.dll", EntryPoint = "GetWindowLong", CharSet = CharSet.Auto)]
        public static extern int GetWindowLong(HandleRef hWnd, int nIndex);
        [DllImport("user32.dll", EntryPoint = "SetWindowLong", CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowLong(HandleRef hWnd, int nIndex, int dwNewLong);

        public static void SetWindow(Form frm)
        {
            int WS_SYSMENU = 0x00080000; // 系统菜单
            int WS_MINIMIZEBOX = 0x20000; // 最大最小化按钮
            int windowLong = GetWindowLong(new HandleRef(frm, frm.Handle), -16);
            SetWindowLong(new HandleRef(frm, frm.Handle), -16, windowLong | WS_MINIMIZEBOX | WS_SYSMENU);
        }
    }
}
