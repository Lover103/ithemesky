using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Manzana;
using System.Threading;
using CE.iPhone.PList;
using System.Diagnostics;

namespace iSprite
{
    public class iPhoneFileDevice : IFileDevice
    {
        Manzana.iPhone iPhoneInterface;
        public event FileCompletedHandler OnCompleteHandler;
        public event FileProgressHandler OnProgressHandler;

        private iPhoneFileDevice()
        {
        }

        public iPhoneFileDevice(Manzana.iPhone iphone)
        {
            this.iPhoneInterface = iphone;
        }

        public bool IsConnected
        {
            get
            {
                return iPhoneInterface != null && iPhoneInterface.IsConnected;
            }
        }

        public char DirectorySeparatorChar
        {
            get
            {
                return '/';
            }
        }

        public string StartPath
        {
            get
            {
                if (IsConnected)
                {
                    return DirectorySeparatorChar.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public DeviceTypeOption DeviceType
        {
            get
            {
                return DeviceTypeOption.iPhone;
            }
        }

        public string DeviceName
        {
            get
            {
                if (IsConnected)
                {
                    //AFCDeviceInfo info = iPhoneInterface.QueryDeviceInfo();
                    return iPhoneInterface.DeviceName;
                }
                else
                {
                    return "none iPhone";
                }
            }
        }
        /// <summary>
        /// 文件重命名
        /// </summary>
        /// <param name="oldname"></param>
        /// <param name="newname"></param>
        public void ReNameFile(string oldname, string newname)
        {
            if (IsConnected && oldname != newname)
            {
                iPhoneInterface.Rename(oldname, newname);
            }
        }
        /// <summary>
        /// 文件夹重命名
        /// </summary>
        /// <param name="oldname"></param>
        /// <param name="newname"></param>
        public void ReNameFolder(string oldname, string newname)
        {
            if (IsConnected && oldname != newname)
            {
                iPhoneInterface.Rename(oldname, newname);
            }
        }

        public bool CreateDirectory(string path)
        {
            if (IsConnected && iPhoneInterface.CreateDirectory(path))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void DeleteFile(string path)
        {
            if (IsConnected)
            {
                iPhoneInterface.DeleteFile(path);
            }
        }
        public void DeleteDirectory(string path)
        {
            if (IsConnected)
            {
                iPhoneInterface.DeleteDirectory(path);
            }
        }
        public void DeleteDirectory(string path, bool recursive)
        {
            if (IsConnected )
            {
                iPhoneInterface.DeleteDirectory(path, recursive);
            }
        }

        public bool FileExists(string path)
        {
            if (IsConnected && iPhoneInterface.Exists(path))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DirectoryExists(string path)
        {            
            if (IsConnected && iPhoneInterface.Exists(path))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public long GetFileSize(string path)
        {
            if (IsConnected && iPhoneInterface.Exists(path))
            {
                return Convert.ToInt64(iPhoneInterface.FileSize(path));
            }
            else
            {
                return 0;
            }
        }

        public bool HasDirectories(string path)
        {
            if (IsConnected && iPhoneInterface.Exists(path))
            {
                return iPhoneInterface.HasDirectories(path);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="path"></param>
        public void ShowFile(string path)
        {
            if (IsConnected && iPhoneInterface.Exists(path))
            {
                if (iPhoneInterface.IsLink(path))
                {
                    return;
                }
                string tmppath = iSpriteContext.Current.iSpriteTempPath + Guid.NewGuid() + Path.GetExtension(path);
                if (iPhoneInterface.Downlod2PC(path, tmppath, this.OnProgressHandler, this.OnCompleteHandler))
                {
                    try
                    {
                        iPhoneFileTypeOption filetype = Utility.GetFileType(tmppath);
                        string tmppath2 = string.Empty;
                        switch (filetype)
                        {
                            case iPhoneFileTypeOption.FILE_IMAGE:
                                tmppath2 = iSpriteContext.Current.iSpriteTempPath + Guid.NewGuid() + Path.GetExtension(path);
                                iPhonePNG.ImageFromFile(tmppath).Save(tmppath2);
                                System.Diagnostics.Process.Start(tmppath2);
                                break;
                            case iPhoneFileTypeOption.FILE_PList:
                                tmppath2 = iSpriteContext.Current.iSpriteTempPath + Guid.NewGuid() + Path.GetExtension(path);
                                PListRoot root = PListRoot.Load(tmppath);
                                root.Save(tmppath2, PListFormat.Xml);
                                string plistcontent = File.ReadAllText(tmppath2, Encoding.UTF8);
                                plistcontent = plistcontent.Replace("\n","\r\n");
                                File.WriteAllText(tmppath2,plistcontent, Encoding.UTF8);
                                Process.Start("notepad.exe", tmppath2);
                                break;
                            default:
                                Process.Start(tmppath);
                                break;
                        }
                    }
                    catch
                    {
                        Process.Start("notepad.exe", tmppath);
                    }
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

        public List<string> GetDirectories(string path)
        { 
            List<string> list = new List<string>();
            if (IsConnected && iPhoneInterface.Exists(path))
            {
                foreach (string name in iPhoneInterface.GetDirectories(path))
                {
                    list.Add(path.TrimEnd(DirectorySeparatorChar) + DirectorySeparatorChar + name + DirectorySeparatorChar);
                }
            }
            return list;
        }
        public List<string> GetDirectories(string path, string searchPattern)
        {
            List<string> list = new List<string>();
            return list;
        }
        public List<string> GetFiles(string path)
        {
            List<string> list = new List<string>();
            if (IsConnected)
            {
                foreach (string name in iPhoneInterface.GetFiles(path))
                {
                    list.Add(name);
                }
            }
            return list;
        }

        public List<iFileInfo> GetFileInfos(string path)
        {
            List<iFileInfo> list = new List<iFileInfo>();
            if (IsConnected)
            {
                foreach (string name in iPhoneInterface.GetFiles(path))
                {
                    iFileInfo fileinfo = new iFileInfo();
                    fileinfo.FileName = name;
                    fileinfo.FullPath = path.TrimEnd('/') + "/" + name;
                    fileinfo.FileSize = iPhoneInterface.FileSize(fileinfo.FullPath);
                    fileinfo.UpdateTime = DateTime.MinValue;
                    list.Add(fileinfo);
                }
            }
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
        /// <summary>
        /// 将文件通过异步方式下载到pc
        /// </summary>
        /// <param name="srcpath_iPhone"></param>
        /// <param name="destpath_Computer"></param>
        public bool Downlod2PC(string srcpath_iPhone, string destpath_Computer)
        {
            if (!this.IsConnected)
            {
                return false;
            }
            return this.iPhoneInterface.Downlod2PC(
                srcpath_iPhone,
                destpath_Computer,
                this.OnProgressHandler, 
                this.OnCompleteHandler
                );
        }
        /// <summary>
        /// 将文件通过异步方式下载到pc
        /// </summary>
        /// <param name="srcpath_iPhone"></param>
        /// <param name="destpath_Computer"></param>
        public void AsyncDownlod2PC(string srcpath_iPhone, string destpath_Computer)
        {
            if (!this.IsConnected)
            {
                return;
            }
            iPhoneFileAsyncTransfer transfer = new iPhoneFileAsyncTransfer(
                this.iPhoneInterface, 
                srcpath_iPhone,
                destpath_Computer,
                this.OnProgressHandler, 
                this.OnCompleteHandler
                );
            new Thread(new ThreadStart(transfer.InvokeDownlod2PC)) 
            { 
                IsBackground = true 
            }.Start();
        }

        /// <summary>
        /// 将文件上传到iPhone
        /// </summary>
        /// <param name="srcpath_Computer"></param>
        /// <param name="destpath_iPhone"></param>
        public bool Copy2iPhone(string srcpath_Computer, string destpath_iPhone
            )
        {
            if (!this.IsConnected)
            {
                return false;
            }

            iPhoneInterface.StartSync();

            bool returnCode = iPhoneInterface.Copy2iPhone(
                srcpath_Computer,
                destpath_iPhone,
                this.OnProgressHandler, 
                this.OnCompleteHandler
                );
            iPhoneInterface.EndSync();

            return returnCode;
        }

        /// <summary>
        /// 通过异步方式将文件上传到iPhone
        /// </summary>
        /// <param name="srcpath_Computer"></param>
        /// <param name="destpath_iPhone"></param>
        public void AsyncCopy2iPhone(string srcpath_Computer,string destpath_iPhone
            )
        {
            if (!this.IsConnected)
            {
                return;
            }
            iPhoneFileAsyncTransfer transfer = new iPhoneFileAsyncTransfer(
                this.iPhoneInterface,
                srcpath_Computer,
                destpath_iPhone,
                this.OnProgressHandler,
                this.OnCompleteHandler
                );
            new Thread(new ThreadStart(transfer.InvokeCopy2iPhone))
            {
                IsBackground = true
            }.Start();
        }
    }

    public class iPhoneFileAsyncTransfer
    {
        private event FileCompletedHandler completehandler = null;
        private event FileProgressHandler progresshandler = null;
        private string destPath = "";
        private iPhone iphone;
        private string srcPath = "";

        private iPhoneFileAsyncTransfer()
        {
        }
        public iPhoneFileAsyncTransfer(iPhone phone, string srcPath, string destPath,
            FileProgressHandler progressHandler, FileCompletedHandler completedHandler)
        {
            this.srcPath = srcPath;
            this.destPath = destPath;
            this.progresshandler = progressHandler;
            this.completehandler = completedHandler;
            this.iphone = phone;
        }

        public void InvokeDownlod2PC()
        {
            if (iphone != null && iphone.IsConnected)
            {
                this.iphone.Downlod2PC(
                    this.SourcePath,
                    this.DestinationPath,
                    this.ProgressHandler,
                    this.CompletedHandler
                    );
            }
        }

        public void InvokeCopy2iPhone()
        {
            if (iphone != null && iphone.IsConnected)
            {
                this.iphone.Copy2iPhone(
                    this.SourcePath,
                    this.DestinationPath,
                    this.ProgressHandler,
                    this.CompletedHandler
                    );
            }
        }

        public FileCompletedHandler CompletedHandler
        {
            get
            {
                return this.completehandler;
            }
            set
            {
                this.completehandler = value;
            }
        }

        public string DestinationPath
        {
            get
            {
                return this.destPath;
            }
            set
            {
                this.destPath = value;
            }
        }

        public FileProgressHandler ProgressHandler
        {
            get
            {
                return this.progresshandler;
            }
            set
            {
                this.progresshandler = value;
            }
        }

        public string SourcePath
        {
            get
            {
                return this.srcPath;
            }
            set
            {
                this.srcPath = value;
            }
        }
    }
}
