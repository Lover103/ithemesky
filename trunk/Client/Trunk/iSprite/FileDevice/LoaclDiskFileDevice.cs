using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
using Manzana;
using System.Windows.Forms;

namespace iSprite
{
    public class LoaclDiskFileDevice : IFileDevice
    {
        public event FileCompletedHandler OnCompleteHandler;
        public event FileProgressHandler OnProgressHandler;
        public event MessageHandler OnMessage;

        #region 消息处理
        private void RaiseMessageHandler(object sender, string Message, MessageTypeOption messageType)
        {
            if (OnMessage != null)
            {
                OnMessage(sender, Message, messageType);
            }
        }
        #endregion

        public LoaclDiskFileDevice()
        {
        }

        public bool IsConnected
        {
            get
            {
                return true;
            }
        }

        public char DirectorySeparatorChar
        {
            get
            {
                return '\\';
            }
        }


        public string DeviceName
        {
            get
            {
                return System.Environment.UserName + "'s PC";
            }
        }

        public string UserIdentity
        {
            get
            {
                return DeviceName;
            }
        }



        public DeviceTypeOption DeviceType
        {
            get
            {
                return DeviceTypeOption.LocalDisk;
            }
        }

        public string StartPath
        {
            get
            {
                return SystemInformation.ComputerName;
            }
        }

        internal void CopyFile(string oldname, string newname)
        {
            if (oldname != newname)
            {
                File.Copy(oldname, newname, true);
            }
        }

        internal void CopyDirectory(string sourceDirName, string destDirName)
        {
            if (destDirName.Contains(sourceDirName))
            {
                return;
            }
            try
            {
                if (!Directory.Exists(destDirName))
                {
                    Directory.CreateDirectory(destDirName);
                    File.SetAttributes(destDirName, File.GetAttributes(sourceDirName));
                }

                if (destDirName[destDirName.Length - 1] != Path.DirectorySeparatorChar)
                {
                    destDirName = destDirName + Path.DirectorySeparatorChar;
                }

                string[] files = Directory.GetFiles(sourceDirName);
                foreach (string file in files)
                {
                    if (File.Exists(destDirName + Path.GetFileName(file)))
                        continue;
                    File.Copy(file, destDirName + Path.GetFileName(file), true);
                    File.SetAttributes(destDirName + Path.GetFileName(file), FileAttributes.Normal);
                }

                string[] dirs = Directory.GetDirectories(sourceDirName);
                foreach (string dir in dirs)
                {
                    CopyDirectory(dir, destDirName + Path.GetFileName(dir));
                }
            }
            catch //(Exception ex)
            {
            }
        }   


        /// <summary>
        /// 文件重命名
        /// </summary>
        /// <param name="oldname"></param>
        /// <param name="newname"></param>
        public void ReNameFile(string oldname, string newname)
        {
            if (oldname != newname)
            {
                File.Move(oldname, newname);
            }
        }
        /// <summary>
        /// 文件夹重命名
        /// </summary>
        /// <param name="oldname"></param>
        /// <param name="newname"></param>
        public void ReNameFolder(string oldname, string newname)
        {
            if (oldname != newname)
            {
                Directory.Move(oldname, newname);
            }
        }

        public bool CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
            return true;
        }

        public void DeleteFile(string path)
        {
            File.Delete(path);
        }

        public void DeleteDirectory(string path)
        {
            Directory.Delete(path, true);
        }
        public void DeleteDirectory(string path, bool recursive)
        {
            Directory.Delete(path, recursive);
        }

        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public long GetFileSize(string path)
        {
            return 0;
        }

        public bool HasDirectories(string path)
        {
            if (Utility.IsDriver(path))
            {
                return true;
            }
            else
            {
                try
                {
                    return Directory.GetDirectories(path).Length > 0;
                }
                catch
                {
                    return false;
                }
            }
        }

        public string GetFolderName(string path)
        {
            path = path.TrimEnd(DirectorySeparatorChar);
            int index = path.LastIndexOf(DirectorySeparatorChar);
            if (index != -1)
            {
                path = path.Substring(index + 1);
            }
            return path;
        }

        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="path"></param>
        public void ShowFile(string path)
        {
            try
            {
                System.Diagnostics.Process.Start(path);
            }
            catch
            {
                System.Diagnostics.Process.Start("notepad.exe", path);
            }
        }

        public List<string> GetDirectories(string path)
        {
            List<string> list = new List<string>();
            if (path == string.Empty)
            {
                path = StartPath;
            }
            if (path == StartPath)
            {
                //第一级目录
                foreach (DriveInfo current in DriveInfo.GetDrives())
                {
                    list.Add(current.Name);
                }
            }
            else
            {
                if (Utility.IsDriver(path))
                {
                    if (!new DriveInfo(path).IsReady)
                    {
                        return list;
                    }
                }
                if(Directory.Exists(path))
                {
                    try
                    {
                        foreach (DirectoryInfo d in new DirectoryInfo(path).GetDirectories())
                        {
                            list.Add(d.FullName + "\\");
                        }
                    }
                    catch
                    { 
                    }
                }
            }
            return list;
        }
        public List<string> GetDirectories(string path, string searchPattern)
        {
            List<string> list = new List<string>();
            return list;
        }

        public List<iFileInfo> GetFileInfos(string path)
        {
            List<iFileInfo> list = new List<iFileInfo>();

            if (Directory.Exists(path))
            {
                iFileInfo info;
                foreach (FileInfo f in new DirectoryInfo(path).GetFiles())
                {
                    info = new iFileInfo();
                    info.FileName = f.Name;
                    info.FileSize = (ulong)f.Length;
                    info.FullPath = f.FullName;
                    info.UpdateTime = f.LastWriteTime;
                    list.Add(info);
                }
            }

            return list;
        }

        public List<string> GetFiles(string path)
        {
            List<string> list = new List<string>();
            return list;
        }
        public List<string> GetFiles(string path, string searchPattern)
        {
            List<string> list = new List<string>();
            return list;
        }
        public string GetParent(string path)
        {
            return string.Empty;
        }

        

    }
}
