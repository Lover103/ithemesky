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
using System.Diagnostics;

namespace iSprite
{
    internal partial class AptDownList : BaseUserControl
    {
        #region 变量定义
        DownloadList m_DownloadList;
        DateTime m_LastSaveDownQueueTime = DateTime.MaxValue;
        static object m_InstallLock = new object();
        AppDownloadUtility m_DownUtility;
        #endregion

        public DownloadList DownList
        {
            get { return m_DownloadList;}
        }

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
            m_DownUtility = new AppDownloadUtility(m_appHelper);
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

            EventHandler OnClick = new EventHandler(ToolStripButton_Click);
            AddContextMenu(m_DownloadList, toolapp, new EventHandler(ToolStripButton_Click));

            ToolStripButton btn = new ToolStripButton("Display in Explorer");
            btn.Click += OnClick;
            m_ctxTools.Items.Add(btn);

            m_DownloadList.OnDoInstall += new InstallAppHandler(DoInstallAfterFinishDown);
            m_DownloadList.OnSaveDownQueue += new SaveDownQueueHandler(DownloadList_OnSaveDownQueue);
            m_DownloadList.OnUpdateCount += new UpdataListViewCountHandler(UpdateCount);
            m_DownloadList.DoubleClick += new EventHandler(DownloadList_DoubleClick);
        }

        //打开选中的文件
        void DownloadList_DoubleClick(object sender, EventArgs e)
        {
            DisplayinWindowsExplorer();
        }

        void DisplayinWindowsExplorer()
        {
            if (m_DownloadList.SelectedItems.Count >= 1)
            {
                ListViewItem item = m_DownloadList.SelectedItems[0];
                Downloader d = m_DownloadList.GetDownloaderByItem(item);
                Process.Start("explorer.exe", String.Format("/select,{0}", d.LocalFile.Replace("/", "\\").Replace("\\\\", "\\")));
            }
        }

        void ToolStripButton_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripButton)
            {
                ToolStripButton btn = (ToolStripButton)sender;
                switch (btn.Text)
                {
                    case "Start":
                        m_DownloadList.StartSelections();
                        break;
                    case "Pause":
                        m_DownloadList.PauseSelections();
                        break;
                    case "Remove Selected":
                        if (m_DownloadList.SelectedItems.Count > 0)
                        {
                            RemoveSelections();
                        }
                        else
                        {
                            RaiseMessageHandler(this, "No item to be removed.", MessageTypeOption.Warning);
                        }
                        break;
                    case "Remove Installed":
                        UnSelectAllItems();
                        foreach (ListViewItem item in m_DownloadList.Items)
                        {
                            Downloader d = m_DownloadList.GetDownloaderByItem(item);
                            if (d.InstallCode == InstallState.InstallFinished)
                            {
                                m_DownUtility.RemoveFiles(d);
                                item.Selected = true;
                            }
                        }
                        if (m_DownloadList.SelectedItems.Count > 0)
                        {
                            RemoveSelections();
                        }
                        else
                        {
                            RaiseMessageHandler(this, "No item to be removed.", MessageTypeOption.Warning);
                        }
                        break;
                    case "Install App":
                        int count = 0;
                        foreach (ListViewItem item in m_DownloadList.SelectedItems)
                        {
                            Downloader d = m_DownloadList.GetDownloaderByItem(item);
                            if (d.State == DownloaderState.Finished)
                            {
                                count++;
                                d.InstallCode = InstallState.NeedInstall;
                                DoInstallAfterFinishDown(d);
                            }
                        }
                        if (count == 0)
                        {
                            RaiseMessageHandler(this, "No item to be Installed.", MessageTypeOption.Warning);
                        }
                        break;
                    case "Display in Explorer":
                        DisplayinWindowsExplorer();
                        break;
                    case "Cancel":
                        m_ctxTools.Hide();
                        break;
                }
            }
        }
        void RemoveSelections()
        {
            if (MessageHelper.ShowConfirm("Are you sure that you want to remove selected downloads") == DialogResult.OK)
            {
                foreach (ListViewItem item in m_DownloadList.SelectedItems)
                {
                    Downloader d = m_DownloadList.GetDownloaderByItem(item);
                    m_DownUtility.RemoveFiles(d);
                }

                m_DownloadList.RemoveSelections();
            }
        }
        #endregion

        #region 取消所有选中状态
        /// <summary>
        /// 取消所有选中状态
        /// </summary>
        void UnSelectAllItems()
        {
            foreach (ListViewItem item in m_DownloadList.Items)
            {
                item.Selected = false;
            }
        }
        #endregion

        #region 更新节点数量
        /// <summary>
        /// 更新节点数量
        /// </summary>
        /// <param name="count"></param>
        void UpdateCount(int count)
        {
            SetNodeCount("Downloaded Packages", m_DownloadList.Items.Count, false);
        }
        #endregion

        #region 添加控件
        /// <summary>
        /// 添加控件
        /// </summary>
        void AddControls()
        {
            m_DownloadList = new DownloadList();
            this.Controls.Add(m_DownloadList);
            m_DownloadList.Dock = System.Windows.Forms.DockStyle.Fill;
            m_DownloadList.Location = new System.Drawing.Point(0, 25);
        }
        #endregion

        #region 刷新下载列表
        /// <summary>
        /// 刷新下载列表
        /// </summary>
        public void UpdateDownloadList()
        {
            if (null != m_DownloadList)
            {

                if (IsHandleCreated)
                {
                    this.BeginInvoke((MethodInvoker)delegate() { m_DownloadList.UpdateList(); });
                }
                else
                {
                    m_DownloadList.UpdateList();
                }

                int count = m_DownloadList.Items.Count;

                if (DateTime.Now - m_LastSaveDownQueueTime > TimeSpan.FromSeconds(20))
                {
                    new Thread(new System.Threading.ThreadStart(SaveDownQueue)).Start();
                }
            }
        }
        #endregion

        #region 成功连接iPhone
        /// <summary>
        /// 成功连接iPhone
        /// </summary>
        /// <param name="isContected"></param>
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
                        DoInstallAfterFinishDown(d);
                    }
                }

            }
        }
        #endregion

        #region 下载完成后安装软件
        /// <summary>
        /// 下载完成后安装软件
        /// </summary>
        /// <param name="d"></param>
        void DoInstallAfterFinishDown(Downloader d)
        {
            if (null != d)
            {
                if (!m_DownUtility.Marked2Finished(d))
                {
                    if (d.State == DownloaderState.EndedWithError)
                    {
                        RaiseMessageHandler(this,
                            "The file (" + Path.GetFileName(d.LocalFile) + ") has finished download, but file hash is Incorrect, you can try again",
                            MessageTypeOption.Error
                            );
                    }
                    return;
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

                            string mainAppfilename = m_DownUtility.GetMainAppFullFileName(d.LocalFile);

                            if (m_DownUtility.AllAppFilesIsFinished(d.LocalFile))//判断当前主程序相关的依赖文件和自身文件是否都成功下载
                            {
                                #region 环境检测
                                if (!m_iPhoneDevice.IsConnected)
                                {
                                    MessageHelper.ShowError(Path.GetFileName(mainAppfilename) + " has been successfully downloaded,please connect your #AppleDeviceType# to finish installation  .");
                                    return;
                                }

                                if (!m_iPhoneDevice.CheckInstallDebApp("openssh"))
                                {
                                    if (MessageHelper.ShowConfirm("Please install Open ssh to finish install app, Would you want to install?")
                                        == DialogResult.OK)
                                    {
                                        //安装Open ssh
                                        MyInstaller.Show(m_iPhoneDevice, InstallAppOption.OpenSSH, null);
                                        return;
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }
                                #endregion

                                List<string> toinstalllist = new List<string>();
                                foreach (string filename in
                                    Directory.GetFiles(m_appHelper.AptDownloadFolder, Path.GetFileName(mainAppfilename).Replace(".deb", "+*.deb")))
                                {
                                    toinstalllist.Add(filename);
                                }
                                toinstalllist.Add(mainAppfilename);//主文件最后安装

                                string msg = string.Empty;
                                bool flag = SSHHelper.InstallDebs(m_iPhoneDevice, toinstalllist, out msg);
                                if (flag)
                                {
                                    RaiseMessageHandler(this, string.Empty, MessageTypeOption.SuccessInstalled);

                                    m_DownUtility.UpdataInstallStatus(mainAppfilename,true);

                                    MessageHelper.ShowInfo(msg);
                                }
                                else
                                {
                                    m_DownUtility.UpdataInstallStatus(mainAppfilename, false);
                                    if (!string.IsNullOrEmpty(msg))
                                    {
                                        MessageHelper.ShowError(msg);
                                    }
                                }
                            }

                            UpdateDownloadList();
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
        
        #region 保存队列信息
        void DownloadList_OnSaveDownQueue()
        {
            SaveDownQueue();
        }

        internal void SaveDownQueue()
        {
            m_LastSaveDownQueueTime = DateTime.Now;
            DownloadManager.Instance.SaveDownQueue(m_appHelper.DownQueueFile);
        }
        #endregion

    }
}
