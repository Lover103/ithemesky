using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MyDownloader.Core;
using MyDownloader.Extension.Protocols;
using System.Threading;
using System.IO;

namespace iSprite
{
    internal partial class AptDownList : UserControl
    {
        #region 变量定义
        AppHelper m_appHelper;
        internal event MessageHandler OnMessage;
        public event SetNodeCountHandler OnSetNodeCount;
        DownloadList m_DownloadList;
        DateTime m_LastSaveDownQueueTime = DateTime.MaxValue;
        static object m_InstallLock = new object();
        iPhoneFileDevice m_iPhoneDevice;
        #endregion

        public DownloadList DownList
        {
            get { return m_DownloadList;}
        }

        #region 设置节点数量
        /// <summary>
        /// 设置节点数量
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="count"></param>
        /// <param name="selectNode"></param>
        void SetNodeCount(string nodeName, int count, bool selectNode)
        {
            if (null != OnSetNodeCount)
            {
                OnSetNodeCount(nodeName, count, selectNode);
            }
        }
        #endregion

        #region 消息处理
        /// <summary>
        /// 消息处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="Message"></param>
        /// <param name="messageType"></param>
        private void RaiseMessageHandler(object sender, string Message, MessageTypeOption messageType)
        {
            if (OnMessage != null)
            {
                OnMessage(sender, Message, messageType);
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iphoneDevice"></param>
        /// <param name="appHelper"></param>
        public AptDownList(iPhoneFileDevice iphoneDevice, AppHelper appHelper)
        {
            m_iPhoneDevice = iphoneDevice;
            m_appHelper = appHelper;

            AddControls();

            InitializeComponent();

            InitControls();
        }
        #endregion
        
        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        void InitControls()
        {
            //注册下载可以支持的协议类型
            ProtocolProviderFactory.RegisterProtocolHandler("http", typeof(HttpProtocolProvider));
            ProtocolProviderFactory.RegisterProtocolHandler("https", typeof(HttpProtocolProvider));
            ProtocolProviderFactory.RegisterProtocolHandler("ftp", typeof(FtpProtocolProvider));

            //从保存的下载队列恢复
            DownloadManager.Instance.LoadDownQueueFromFile(m_appHelper.DownQueueFile);

            m_LastSaveDownQueueTime = DateTime.Now;

            this.toolbtnStart.Click += new EventHandler
                (
                    delegate(object sender, EventArgs e)
                    {
                        m_DownloadList.StartSelections();
                    }
                );

            this.toolbtnPause.Click += new EventHandler
                (
                    delegate(object sender, EventArgs e)
                    {
                        m_DownloadList.PauseSelections();
                    }
                );

            this.toolbtnRemove.Click += new EventHandler
                (
                    delegate(object sender, EventArgs e)
                    {
                        m_DownloadList.RemoveSelections();
                    }
                );

            m_DownloadList.OnDoInstall += new InstallAppHandler(DoInstall);
            m_DownloadList.OnSaveDownQueue += new SaveDownQueueHandler(DownloadList_OnSaveDownQueue);
            m_DownloadList.OnUpdateCount += new UpdataListViewCountHandler(UpdateCount);
        }
        #endregion

        void UpdateCount(int count)
        {
            SetNodeCount("Downloaded Packages", m_DownloadList.Items.Count, false);
        }

        void AddControls()
        {
            m_DownloadList = new DownloadList();
            this.Controls.Add(m_DownloadList);
            m_DownloadList.Dock = System.Windows.Forms.DockStyle.Fill;
            m_DownloadList.Location = new System.Drawing.Point(0, 25);
        }

        #region 刷新下载列表
        /// <summary>
        /// 刷新下载列表
        /// </summary>
        public void UpdateDownloadList()
        {
            if (null != m_DownloadList)
            {
                m_DownloadList.UpdateList();

                int count = m_DownloadList.Items.Count;

                if (DateTime.Now - m_LastSaveDownQueueTime > TimeSpan.FromSeconds(20))
                {
                    new Thread(new System.Threading.ThreadStart(SaveDownQueue)).Start();
                }
            }
        }
        #endregion

        internal void AfterDeviceFinishConnected(bool isContected)
        {
            if (isContected)
            {
                SetNodeCount("Downloaded Packages", m_DownloadList.Items.Count, false);
                //安装已经下载完成的软件
                for (int i = 0; i < DownloadManager.Instance.Downloads.Count; i++)
                {
                    Downloader d = DownloadManager.Instance.Downloads[i];
                    if (d.State == DownloaderState.Finished 
                        && (d.InstallCode == InstallState.NeedInstall || d.InstallCode == InstallState.DependInstall))
                    {
                        DoInstall(d);
                    }
                }

            }
        }

        #region 安装软件
        /// <summary>
        /// 安装软件
        /// </summary>
        /// <param name="d"></param>
        void DoInstall(Downloader d)
        {
            if (null != d)
            {
                if (File.Exists(d.LocalFile_D))
                {
                    File.Move(d.LocalFile_D, d.LocalFile_F);//表示下载完成
                }
            }
            else
            {
                return;
            }
            if (null != d 
                && (d.InstallCode == InstallState.NeedInstall || d.InstallCode == InstallState.DependInstall) 
                )
            {
                try
                {
                    lock (m_InstallLock)
                    {
                        if ((d.InstallCode == InstallState.NeedInstall || d.InstallCode == InstallState.DependInstall))
                        {
                            if (d.InstallCode == InstallState.DependInstall)
                            {
                                d.InstallCode = InstallState.InstallFinished;
                            }

                            int index = d.LocalFile.LastIndexOf("+");
                            string mainfilename = string.Empty;
                            if (index == -1)
                            {
                                mainfilename = d.LocalFile;
                            }
                            else
                            {
                                mainfilename = d.LocalFile.Substring(0, index) + ".deb";
                            }

                            if (Directory.GetFiles(m_appHelper.AptDownloadFolder, Path.GetFileName(mainfilename).Replace(".deb", "*.d")).Length == 0)
                            {
                                if (Directory.GetFiles(m_appHelper.AptDownloadFolder, Path.GetFileName(mainfilename).Replace(".deb", "*.f")).Length > 0)
                                {
                                    if (!m_iPhoneDevice.IsConnected)
                                    {
                                        MessageHelper.ShowError(Path.GetFileName(mainfilename) + " has been successfully downloaded,please connect your #AppleDeviceType# to finish installation  .");
                                        return;
                                    }
                                    List<string> toinstalllist = new List<string>();
                                    foreach (string filename in
                                        Directory.GetFiles(m_appHelper.AptDownloadFolder, Path.GetFileName(mainfilename).Replace(".deb", "+*.deb")))
                                    {
                                        toinstalllist.Add(filename);
                                    }
                                    toinstalllist.Add(mainfilename);//主文件最后安装

                                    string msg = string.Empty;
                                    bool flag = SSHHelper.InstallDebs(m_iPhoneDevice, toinstalllist, out msg);
                                    if (flag)
                                    {
                                        RaiseMessageHandler(this, string.Empty, MessageTypeOption.SuccessInstalled);

                                        //更新为已完成安装
                                        for (int i = 0; i < DownloadManager.Instance.Downloads.Count; i++)
                                        {
                                            if (new FileInfo(DownloadManager.Instance.Downloads[i].LocalFile).FullName 
                                                == new FileInfo(mainfilename).FullName)
                                            {
                                                DownloadManager.Instance.Downloads[i].InstallCode = InstallState.InstallFinished;
                                                break;
                                            }
                                        }

                                        MessageHelper.ShowInfo(msg);
                                    }
                                    else if(!string.IsNullOrEmpty(msg))
                                    {
                                        MessageHelper.ShowError(msg);
                                    }
                                }
                            }                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageHelper.ShowError("Can not install app(" + ex.Message + ") .");
                }
            }
        }
        #endregion      


        void DownloadList_OnSaveDownQueue()
        {
            SaveDownQueue();
        }


        internal void SaveDownQueue()
        {
            m_LastSaveDownQueueTime = DateTime.Now;
            DownloadManager.Instance.SaveDownQueue(m_appHelper.DownQueueFile);
        }

    }
}
