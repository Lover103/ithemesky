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
            DownloadManager.Instance.LoadDownQueueFromFile(m_appHelper.DownQueueFile);//从保存的下载队列恢复

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

            m_DownloadList.OnDoInstall += new InstallAppHandler(DownloadList_OnDoInstall);
            m_DownloadList.OnSaveDownQueue += new SaveDownQueueHandler(DownloadList_OnSaveDownQueue);
        }
        #endregion

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

                if (DateTime.Now - m_LastSaveDownQueueTime > TimeSpan.FromSeconds(20))
                {
                    new Thread(new System.Threading.ThreadStart(SaveDownQueue)).Start();
                }
            }
        }
        #endregion

        #region 安装软件
        /// <summary>
        /// 安装软件
        /// </summary>
        /// <param name="d"></param>
        void DownloadList_OnDoInstall(Downloader d)
        {
            if (null != d && d.InstallCode == InstallState.Prepare2Install && m_iPhoneDevice.IsConnected)
            {
                try
                {
                    lock (m_InstallLock)
                    {
                        if (d.InstallCode == InstallState.Prepare2Install)
                        {
                            d.InstallCode = InstallState.Installing;

                            string msg = string.Empty;
                            bool flag = SSHHelper.InstallDeb(m_iPhoneDevice, d.LocalFile, out msg);
                            if (flag)
                            {
                                RaiseMessageHandler(this, string.Empty, MessageTypeOption.SuccessInstalled);
                                d.InstallCode = InstallState.Installed;
                                MessageHelper.ShowInfo(msg);
                            }
                            else
                            {
                                MessageHelper.ShowError(msg);
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
