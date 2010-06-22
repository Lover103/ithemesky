using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;


using Manzana;
using System.Threading;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.IO;
using System.Diagnostics;

namespace iSprite
{
    /// <summary>
    /// 主窗体
    /// </summary>
    internal partial class MainForm : iSpriteForm
    {
        #region 变量定义
        /// <summary>
        /// 跨线程调用委托
        /// </summary>
        private delegate void ThreadInvokeDelegate();
        /// <summary>
        /// iPhone操作面板
        /// </summary>
        private FilePanel m_iPhonePanel;
        /// <summary>
        /// 本地磁盘操作面板
        /// </summary>
        private FilePanel m_LoaclDiskPanel;

        /// <summary>
        /// 源操作面板
        /// </summary>
        private FilePanel m_SourcePanel;
        /// <summary>
        /// 目的操作面板
        /// </summary>
        private FilePanel m_DestPanel;
        /// <summary>
        /// 进度条
        /// </summary>
        private iProgress progressBar;
        /// <summary>
        /// iPhone操作对象
        /// </summary>
        private iPhone iphone;
        /// <summary>
        /// 指示是否停止传输
        /// </summary>
        private bool m_stoptransfer = false;
        /// <summary>
        /// 显示视图
        /// </summary>
        private ViewModeOption m_ViewMode = ViewModeOption.DUALVERTICAL;
        iThemeBrowser m_themeManage;
        IFileDevice iphonedriver;
        IFileDevice localdiskdriver;
        Updater m_Updater;
        AppManage m_AppManage;

        /// <summary>
        /// 状态条
        /// </summary>
        private iSpriteStatus statusBar;

        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            this.Text = "iSpirit (V" + iSpriteContext.Current.CurrentVersion + ")";
            this.FormClosing += new FormClosingEventHandler(MainForm_FormClosing);
            Utility.SetWindow(this);
        }
        #endregion

        #region 加载进度条
        /// <summary>
        /// 加载进度条
        /// </summary>
        void InitProgress()
        {
            progressBar = new iProgress();
            Controls.Add(progressBar);
            progressBar.BringToFront();
            progressBar.Left = (this.Width - progressBar.Width) / 2;
            progressBar.Top = (this.Height - progressBar.Height) / 2;
            progressBar.Visible = false;
            progressBar.OnCancel += new CancelHandler(progressBar_OnCancel);

            statusBar = new iSpriteStatus();
            Controls.Add(statusBar);
            statusBar.BringToFront();
            statusBar.Left = (this.Width - statusBar.Width) / 2;
            statusBar.Top = (this.Height - statusBar.Height) / 2;
            statusBar.Visible = false;
        }
        #endregion

        #region 加载控件
        /// <summary>
        /// 加载控件
        /// </summary>
        private void LoadControls()
        {
            RunTestCode();


            InitProgress();

            //this.tabs.SelectedItem = this.tabFile;
            mainsplitcontainer.SplitterDistance = this.ClientSize.Width / 2;

            iphone = new iPhone();
            iphonedriver = new iPhoneFileDevice(iphone);
            iphonedriver.OnMessage += new MessageHandler(ShowMessage);

            iphonedriver.OnCompleteHandler += new FileCompletedHandler(iphonedriver_OnCompleteHandler);
            iphonedriver.OnProgressHandler += new FileProgressHandler(DoProgressHandler);

            m_iPhonePanel = new FilePanel(iphonedriver);
            mainsplitcontainer.Panel1.Controls.Add(m_iPhonePanel);
            m_iPhonePanel.Dock = DockStyle.Fill;
            m_iPhonePanel.Enabled = false;
            m_iPhonePanel.OnMessage += new MessageHandler(ShowMessage);

            //	本地磁盘操作面板
            localdiskdriver = new LoaclDiskFileDevice();
            m_LoaclDiskPanel = new FilePanel(localdiskdriver);
            mainsplitcontainer.Panel2.Controls.Add(m_LoaclDiskPanel);
            m_LoaclDiskPanel.Dock = DockStyle.Fill;
            m_LoaclDiskPanel.OnMessage += new MessageHandler(ShowMessage);

            m_SourcePanel = m_iPhonePanel;
            m_DestPanel = m_LoaclDiskPanel;

            foreach (ToolStripItem item in Filetoolmenu.Items)
            {
                if (item is ToolStripButton)
                {
                    item.Click += new EventHandler(toolmenuitem_Click);
                }
            }

            foreach (ToolStripItem item in this.toolapp.Items)
            {
                if (item is ToolStripButton)
                {
                    item.Click += new EventHandler(toolmenuitem_Click);
                }
            }

            //主题管理
            m_themeManage = new iThemeBrowser((iPhoneFileDevice)iphonedriver, tabTheme, this);
            m_themeManage.OnMessage += new MessageHandler(ShowMessage);
            m_themeManage.OnProgressHandler += new FileProgressHandler(DoProgressHandler);

            //程序管理
            m_AppManage = new AppManage((iPhoneFileDevice)iphonedriver, this);
            m_AppManage.OnMessage += new MessageHandler(ShowMessage);
            m_AppManage.OnProgressHandler += new FileProgressHandler(DoProgressHandler);

            //检查更新
            m_Updater = new Updater();
            m_Updater.OnProgressHandler += new FileProgressHandler(DoProgressHandler);
            m_Updater.OnMessage += new MessageHandler(ShowMessage);
            new Thread(new ThreadStart(m_Updater.CheckNewVer)).Start();

            this.tabs.BackgroundImage = global::iSprite.Resource.BackgroundImage;
            this.tabFile.BackgroundImage = global::iSprite.Resource.BackgroundImage;

            this.Resize += new EventHandler(MainForm_Resize);


            //	iPhone操作面板
            if (iphone.IsInstalliTunes)
            {
                iphone.Connect += new ConnectEventHandler(iphone_Connect);
                iphone.Disconnect += new ConnectEventHandler(iphone_Disconnect);
                iphone.StartContect();
            }
            else
            {
                ShowMessage(this, "Current program rely on iTunes, please check you have installed it.", MessageTypeOption.Error);
            }

        }
        #endregion

        #region 窗体加载
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                LoadControls();
            }
            catch (Exception ex)
            {
                ShowMessage(this, ex.Message + ex.StackTrace, MessageTypeOption.Error);
            }
        }
        #endregion

        #region 窗体关闭中
        /// <summary>
        /// 窗体关闭中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //ShowMessage(this, "Begin to Save Setting...", MessageTypeOption.SetStatusBar);
            //Application.DoEvents();

            if (null != iphone)
            {
                //移除事件
                iphone.Connect -= new ConnectEventHandler(iphone_Connect);
                iphone.Disconnect -= new ConnectEventHandler(iphone_Disconnect);
            }
            if (null != m_AppManage)
            {
                m_AppManage.SaveDownQueue();
            }
            //ShowMessage(this, "", MessageTypeOption.HiddenStatusBar);

            Process[] pList = Process.GetProcesses();
            for (int j = 0; j < pList.Length; j++)
            {
                if (pList[j].ProcessName.StartsWith("ispirit", StringComparison.CurrentCultureIgnoreCase))
                {
                    pList[j].Kill();
                }
            }
        }
        #endregion

        #region 窗体改变大小
        /// <summary>
        /// 窗体改变大小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainForm_Resize(object sender, EventArgs e)
        {
        }
        #endregion

        #region 事件处理

        #region iPhone连接与断开事件处理

        /// <summary>
        /// 建立连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void iphone_Connect(object sender, ConnectEventArgs args)
        {
            ConnectSetting(args);
        }
        /// <summary>
        /// 断开连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void iphone_Disconnect(object sender, ConnectEventArgs args)
        {
            ConnectSetting(args);
        }
        /// <summary>
        /// 连接设置
        /// </summary>
        /// <param name="args"></param>
        void ConnectSetting(ConnectEventArgs args)
        {
            bool enable = false;
            if (args.Message == NotificationMessage.Connected)
            {
                enable = true;
            }
            else
            {
                enable = false;
            }

            if (this.InvokeRequired)
            {
                this.Invoke(new ThreadInvokeDelegate(
                    delegate()
                    {
                        DoConnectSetting(enable);
                    }
                ));
            }
            else
            {
                DoConnectSetting(enable);
            }
        }
        void DoConnectSetting(bool enable)
        {
            try
            {
                m_iPhonePanel.Enabled = enable;
                if (enable)
                {
                    iSpriteContext.Current.AfterDeviceFinishConnected(iphone);
                    ((iPhoneFileDevice)iphonedriver).AfterDeviceFinishConnected();
                    m_iPhonePanel.AfterDeviceFinishConnected();
                }
                m_themeManage.AfterDeviceFinishConnected(enable);
                m_AppManage.AfterDeviceFinishConnected(enable);
                tsbtnDeb.Enabled = tsbtnreSpring.Enabled = enable;
            }
            catch (Exception ex)
            {
                ShowMessage(this, ex.Message, MessageTypeOption.Error);
            }


            ShowMessage(this, "Prepare to add...", MessageTypeOption.SetStatusBar);
            //new Thread(new ThreadStart(Change)).Start();
        }
        #endregion

        /// <summary>
        /// 消息处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        /// <param name="messagetype"></param>
        void ShowMessage(object sender, string message, MessageTypeOption messagetype)
        {
            switch (messagetype)
            {
                case MessageTypeOption.Info:
                    MessageHelper.ShowInfo(message);
                    break;

                case MessageTypeOption.Warning:
                    MessageHelper.ShowWarning(message);
                    break;

                case MessageTypeOption.Error:
                    MessageHelper.ShowError(message);
                    break;
                case MessageTypeOption.Upgrade:
                    if (MessageHelper.ShowConfirm(message) == DialogResult.OK)
                    {
                        m_Updater.DoUpdate();
                    }
                    break;
                case MessageTypeOption.EditPlist:
                    PlistEditer.Show((iPhoneFileDevice)iphonedriver, message);
                    break;
                case MessageTypeOption.SetStatusBar:
                    statusBar.StatusMsg = message;
                    break;
                case MessageTypeOption.HiddenStatusBar:
                    if (message != string.Empty)
                    {
                        statusBar.StatusMsg = message;
                        Thread.Sleep(TimeSpan.FromSeconds(1.5));
                    }
                    statusBar.Hidden();
                    break;
                case MessageTypeOption.SuccessInstalled:
                    if (null != m_AppManage)
                    {
                        m_AppManage.SafeRefreshApp();
                    }
                    break;

            }
        }

        /// <summary>
        /// 中止传输
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="cancel"></param>
        void progressBar_OnCancel(object sender, bool cancel)
        {
            m_stoptransfer = cancel;
        }
        /// <summary>
        /// 文件传输进度处理
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="totalSize"></param>
        /// <param name="completeSize"></param>
        /// <param name="speed"></param>
        /// <param name="timeElapse"></param>
        /// <param name="file"></param>
        /// <param name="cancel"></param>
        void DoProgressHandler(FileProgressMode mode, ulong totalSize, ulong completeSize, int speed, double timeElapse, string file, ref bool cancel)
        {
            if (m_stoptransfer)
            {
                //取消传输
                cancel = true;
                m_stoptransfer = false;
                progressBar.Visible = false;
            }
            else
            {
                if (totalSize <= completeSize)
                {
                    progressBar.Visible = false;
                }
                else
                {
                    progressBar.SetProgress(mode, totalSize, completeSize, speed, timeElapse, file);
                }
            }
        }
        /// <summary>
        /// 文件传输完成处理
        /// </summary>
        /// <param name="success"></param>
        /// <param name="file"></param>
        /// <param name="lastErr"></param>
        void iphonedriver_OnCompleteHandler(bool success, string file, string lastErr)
        {
            progressBar.Visible = false;
        }

        /// <summary>
        /// 定时器事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            if (null != m_iPhonePanel)
            {
                m_iPhonePanel.UpdateMenuStatus();
            }
            if (null != m_LoaclDiskPanel)
            {
                m_LoaclDiskPanel.UpdateMenuStatus();
            }
            if (null != m_AppManage)
            {
                m_AppManage.UpdateDownloadList();
            }
        }

        #region 按钮事件处理
        /// <summary>
        /// 按钮事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void toolmenuitem_Click(object sender, EventArgs e)
        {
            ToolStripButton item = (ToolStripButton)sender;
            switch (item.Name)
            {
                case "tsbtnTileVertical":
                    this.m_ViewMode = ViewModeOption.DUALVERTICAL;
                    SetViewMode(item);
                    break;
                case "tsbtnTileHorizontal":
                    this.m_ViewMode = ViewModeOption.DUALHORIZONTAL;
                    SetViewMode(item);
                    break;
                case "tsbtnSinglePane":
                    this.m_ViewMode = ViewModeOption.SINGLE;
                    SetViewMode(item);
                    break;
                case "tsbtnFlipPanes":
                    SwapPanes();
                    break;
                case "tsbtnreSpring":
                    ReSpring();
                    break;
                case "tsbtnHelp":
                    System.Diagnostics.Process.Start(iSpriteContext.Current.HelpUrl);
                    break;
                case "tsbtnDeb":
                case "tsbtnInstallDeb":
                    DebInstaller.Show((iPhoneFileDevice)iphonedriver, new MessageHandler(ShowMessage));
                    break;
                case "tsbtnInstallIPA":
                case "tsbtnInstallPXL":
                    ShowMessage(this,"Coming soon...", MessageTypeOption.Info);
                    break;
            }
        }
        #endregion

        #endregion

        #region  设置展示视图
        /// <summary>
        /// 状态重置
        /// </summary>
        private void ResetViewMode()
        {
            tsbtnTileVertical.Checked = false;
            tsbtnTileHorizontal.Checked = false;
            tsbtnSinglePane.Checked = false;
        }

        /// <summary>
        /// 设置展示视图
        /// </summary>
        /// <param name="item"></param>
        private void SetViewMode(ToolStripButton item)
        {
            if (item.Checked)
            {
                return;
            }

            ResetViewMode();
            item.Checked = true;

            switch (m_ViewMode)
            {
                case ViewModeOption.DUALVERTICAL:
                    m_DestPanel.Dock = DockStyle.Fill;
                    m_DestPanel.Visible = true;
                    mainsplitcontainer.Orientation = Orientation.Vertical;
                    mainsplitcontainer.SplitterDistance = this.ClientSize.Width / 2;
                    break;

                case ViewModeOption.DUALHORIZONTAL:
                    m_DestPanel.Dock = DockStyle.Fill;
                    m_DestPanel.Visible = true;
                    mainsplitcontainer.Orientation = Orientation.Horizontal;
                    mainsplitcontainer.SplitterDistance = this.ClientSize.Height / 2;
                    break;

                case ViewModeOption.SINGLE:
                    m_DestPanel.Dock = DockStyle.None;
                    m_DestPanel.Visible = false;
                    mainsplitcontainer.Orientation = Orientation.Vertical;
                    mainsplitcontainer.SplitterDistance = this.Width;
                    break;
            }
        }
        #endregion

        #region 面板交换
        /// <summary>
        /// 面板交换
        /// </summary>
        private void SwapPanes()
        {
            if (m_SourcePanel == m_iPhonePanel)
            {
                mainsplitcontainer.Panel1.Controls.Remove(m_iPhonePanel);
                mainsplitcontainer.Panel2.Controls.Remove(m_LoaclDiskPanel);

                mainsplitcontainer.Panel1.Controls.Add(m_LoaclDiskPanel);
                mainsplitcontainer.Panel2.Controls.Add(m_iPhonePanel);

                m_SourcePanel = m_LoaclDiskPanel;
                m_DestPanel = m_iPhonePanel;
            }
            else
            {
                mainsplitcontainer.Panel1.Controls.Remove(m_LoaclDiskPanel);
                mainsplitcontainer.Panel2.Controls.Remove(m_iPhonePanel);

                mainsplitcontainer.Panel1.Controls.Add(m_iPhonePanel);
                mainsplitcontainer.Panel2.Controls.Add(m_LoaclDiskPanel);

                m_SourcePanel = m_iPhonePanel;
                m_DestPanel = m_LoaclDiskPanel;
            }
        }
        #endregion

        #region 重启Springboard
        /// <summary>
        /// 重启Springboard
        /// </summary>
        void ReSpring()
        {
            if (MessageHelper.ShowConfirm("Are you sure you want to reboot Springboard ?") == DialogResult.OK)
            {
                ((iPhoneFileDevice)iphonedriver).Respring();
            }
        }
        #endregion

        #region tab切换
        /// <summary>
        /// tab切换
        /// </summary>
        /// <param name="e"></param>
        private void TabStripItemSelectionChanged(iSprite.ThirdControl.FarsiLibrary.TabStripItemChangedEventArgs e)
        {
            if (m_themeManage != null)
            {
                if (e.Item.Name == "tabTheme")
                {
                    m_themeManage.SetPreviewVisable(true);
                }
                else
                {
                    m_themeManage.SetPreviewVisable(false);
                }
            }
        }
        #endregion

        #region 测试代码
        void RunTestCode()
        {
            return;
            //DataSet ds = new DataSet();
            //ds.ReadXml(@"E:\ithemesky\Client\Trunk\iSprite\CydiaSourceCfg.xml");
            //List<RepositoryInfo> repositoryInfos = new List<RepositoryInfo>();
            //foreach (DataRow row in ds.Tables[0].Rows)
            //{
            //    string url = row["Release"].ToString().Trim();
            //    string APTCachedReleaseURL = url;
            //    RepositoryInfo repositoryInfo;
            //    string content = string.Empty;
            //    if (PackageDataContext.GetRepositoryInfoByUrl(url, ref  APTCachedReleaseURL, out  repositoryInfo))
            //    {
            //        repositoryInfos.Add(repositoryInfo);
            //    }
            //}
            //List<string> list = new List<string>();
            ////list.Add("http://apt.saurik.com/");
            //list.Add("http://apt.bigboss.us.com/repofiles/cydia/");
            //list.Add("http://apt.modmyi.com/");
            //list.Add("http://cydia.zodttd.com/");
            //list.Add("http://apt9.yellowsn0w.com/");
            //list.Add("http://cydia.zodttd.com/");
            //list.Add("http://iphone.hackndev.org/");
            //list.Add("http://iphonehe.com/");
            //list.Add("http://ispaziorepo.com/cydia/apt");
            //list.Add("http://repo.smxy.org/cydia/apt/");
            //list.Add("http://urbanfanatics.com/");
            //list.Add("http://www.ispaziorepo.com/");
            //list.Add("http://www.zodttd.com/");
            //foreach (string url in list)
            //{
            //    string APTCachedReleaseURL = string.Empty;
            //    RepositoryInfo repositoryInfo;
            //    if (PackageDataContext.GetRepositoryInfoByUrl(url, ref  APTCachedReleaseURL, out  repositoryInfo))
            //    {
            //        repositoryInfos.Add(repositoryInfo);
            //    }
            //}
            //string APTCachedReleaseURL = string.Empty;
            //RepositoryInfo repositoryInfo;
            //PackageDataContext.GetRepositoryInfoByUrl("http://apt.saurik.com/", ref  APTCachedReleaseURL, out  repositoryInfo);
            string CachedPackagesURL = string.Empty;
            List<PackageItem> packages;
            //PackageDataContext.GetPackagesByUrl("http://apt.saurik.com/", ref  CachedPackagesURL, out  packages);
        }
        #endregion
    }
}
