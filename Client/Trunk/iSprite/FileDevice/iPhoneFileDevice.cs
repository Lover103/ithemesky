using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
using Manzana;
using System.Threading;
using CE.iPhone.PList;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace iSprite
{
    internal class iPhoneFileDevice : IFileDevice
    {
        Manzana.iPhone iPhoneInterface;
        public event FileCompletedHandler OnCompleteHandler;
        public event FileProgressHandler OnProgressHandler;
        string m_deviceName = "no iPhone/iPod found";
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

        internal string CurrentLang { private set;get;}

        private iPhoneFileDevice()
        {
        }

        public iPhoneFileDevice(Manzana.iPhone iphone)
        {
            this.iPhoneInterface = iphone;
        }

        internal void AfterDeviceFinishConnected()
        {
            if (IsConnected)
            {
                string iphonecfgpath = iSpriteContext.Current.iPhone_GlobalPreferences_Path;
                string content = GetFileText(iphonecfgpath);
                int fromindex = content.IndexOf("<key>AppleLanguages</key>");
                if (fromindex != -1)
                {
                    fromindex = content.IndexOf("<string>", fromindex);
                    if (fromindex != -1)
                    {
                        fromindex += 8;
                        int endindex = content.IndexOf("</string>", fromindex);
                        if (endindex != -1)
                        {
                            CurrentLang = content.Substring(fromindex, endindex - fromindex);
                        }
                    }
                }
                if (string.IsNullOrEmpty(CurrentLang))
                {
                    CurrentLang = "en";
                }
                iphonecfgpath = "/private/var/root/Library/Lockdown/data_ark.plist";
                content = GetFileText(iphonecfgpath);
                Match match = new Regex("<key>-DeviceName</key>[\\s]+<[u]*string>(?<NameValue>[\\S ]*?)</[u]*string>",
                    RegexOptions.IgnoreCase | RegexOptions.Compiled).Match(content);
                if (match.Success)
                {
                    m_deviceName = match.Result("${NameValue}");
                }
                else
                {
                    m_deviceName = "no named";
                }
            }
            else
            {
                m_deviceName = "no iPhone/iPod found";
            }
        }
        void NotConnectedErrot()
        { 
            RaiseMessageHandler(this, "Please Make sure you have connected your iPhone to PC via USB cable.", MessageTypeOption.Error);
        }
        /// <summary>
        /// 注销iPhone
        /// </summary>
        internal void Respring()
        {
            if (iPhoneInterface.IsConnected)
            {
                iPhoneInterface.Respring();
            }
            else
            {
                NotConnectedErrot();
            }
        }

        internal string GetFileText(string iPhonePath)
        {
            string localpath = iSpriteContext.Current.iSpriteTempPath + Path.GetFileName(iPhonePath);
            if (Downlod2PC(iPhonePath, localpath))
            {
                if (Utility.GetFileType(localpath) == iPhoneFileTypeOption.FILE_PList)
                {
                    PListRoot root = PListRoot.Load(localpath);
                    root.Save(localpath, PListFormat.Xml);
                }
                return File.ReadAllText(localpath, Encoding.UTF8);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 保存文件内容到iPhone
        /// </summary>
        /// <param name="content"></param>
        /// <param name="iPhonePath"></param>
        internal bool SetFileText(string content,string iPhonePath)
        {
            string localpath = iSpriteContext.Current.iSpriteTempPath + Path.GetFileName(iPhonePath);
            File.WriteAllText(localpath, content,Encoding.UTF8);

            if (Utility.GetFileType(localpath) == iPhoneFileTypeOption.FILE_PList)
            {
                PListRoot root = PListRoot.Load(localpath);
                root.Save(localpath, PListFormat.Binary);
            }

            return Copy2iPhone(localpath, iPhonePath);
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
                //return "iSprite's iPhone";
                return m_deviceName;
            }
        }

        public string UserIdentity
        {
            get
            {
                if (IsConnected)
                {
                    return iPhoneInterface.DeviceId;
                }
                else
                {
                    return "";
                }
            }
        }

        public string DeviceVersion
        {
            get
            {
                if (IsConnected)
                {
                    return iPhoneInterface.DeviceVersion;
                }
                else
                {
                    return "";
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
            if (IsConnected)
            {
                if (oldname != newname)
                {
                    iPhoneInterface.Rename(oldname, newname);
                }
            }
            else
            {
                NotConnectedErrot();
            }
        }

        public bool CreateDirectory(string path)
        {
            if (IsConnected)
            {
                if (iPhoneInterface.CreateDirectory(path))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                NotConnectedErrot();
                return false;
            }
        }
        public void DeleteFile(string path)
        {
            if (IsConnected)
            {
                iPhoneInterface.DeleteFile(path);
            }
            else
            {
                NotConnectedErrot();
            }
        }
        public void DeleteDirectory(string path)
        {
            if (IsConnected)
            {
                iPhoneInterface.DeleteDirectory(path);
            }
            else
            {
                NotConnectedErrot();
            }
        }
        public void DeleteDirectory(string path, bool recursive)
        {
            if (IsConnected )
            {
                iPhoneInterface.DeleteDirectory(path, recursive);
            }
            else
            {
                NotConnectedErrot();
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
                                RaiseMessageHandler(this, path, MessageTypeOption.EditPlist);
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
            if (IsConnected )
            {
                if (iPhoneInterface.Exists(path))
                {
                    try
                    {
                        foreach (string name in iPhoneInterface.GetDirectories(path))
                        {
                            list.Add(path.TrimEnd(DirectorySeparatorChar) + DirectorySeparatorChar + name + DirectorySeparatorChar);
                        }
                    }
                    catch (Exception ex)
                    {
                        RaiseMessageHandler(this, ex.Message, MessageTypeOption.Error);
                    }
                }
            }
            else
            {
                NotConnectedErrot();
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
            else
            {
                NotConnectedErrot();
            }
            return list;
        }

        public List<iFileInfo> GetFileInfos(string path)
        {
            List<iFileInfo> list = new List<iFileInfo>();
            if (IsConnected)
            {
                try
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
                catch(Exception ex)
                {
                    RaiseMessageHandler(this,ex.Message, MessageTypeOption.Error);
                }
            }
            else
            {
                NotConnectedErrot();
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
        public bool Downlod2PC(string srcpath_iPhone, string destpath_Computer)
        {
            return Downlod2PC(srcpath_iPhone, destpath_Computer, true);
        }
        /// <summary>
        /// 将文件通过异步方式下载到pc
        /// </summary>
        /// <param name="srcpath_iPhone"></param>
        /// <param name="destpath_Computer"></param>
        public bool Downlod2PC(string srcpath_iPhone, string destpath_Computer, bool showProgress)
        {
            if (!this.IsConnected)
            {
                NotConnectedErrot();
                return false;
            }
            if (showProgress)
            {
                return this.iPhoneInterface.Downlod2PC(
                    srcpath_iPhone,
                    destpath_Computer,
                    this.OnProgressHandler,
                    this.OnCompleteHandler
                    );
            }
            else
            {
                return this.iPhoneInterface.Downlod2PC(
                    srcpath_iPhone,
                    destpath_Computer,
                    null,
                    null
                    );
            }
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
                NotConnectedErrot();
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
                NotConnectedErrot();
                return false;
            }

            //iPhoneInterface.StartSync();

            bool returnCode = iPhoneInterface.Copy2iPhone(
                srcpath_Computer,
                destpath_iPhone,
                this.OnProgressHandler, 
                this.OnCompleteHandler
                );
            //iPhoneInterface.EndSync();

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
                NotConnectedErrot();
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
