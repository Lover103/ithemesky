using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using Manzana;

namespace iSprite
{
    /// <summary>
    /// 主窗体
    /// </summary>
    public partial class MainForm : Form
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
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            iphone = new iPhone();
            this.FormClosed += new FormClosedEventHandler(MainForm_FormClosed);
        }
        /// <summary>
        /// 加载进度条
        /// </summary>
        void InitialiseProgress()
        {
            progressBar = new iProgress();
            Controls.Add(progressBar);
            progressBar.BringToFront();
            progressBar.Left = (this.Width - progressBar.Width) / 2;
            progressBar.Top = (this.Height - progressBar.Height) / 2;
            progressBar.Visible = false;
            progressBar.OnCancel += new CancelHandler(progressBar_OnCancel);
        }
        private void Initialise()
        {
            InitialiseProgress();

            mainsplitcontainer.SplitterDistance = this.ClientSize.Width / 2;

            //	iPhone操作面板
            iphone.Connect += new ConnectEventHandler(iphone_Connect);
            iphone.Disconnect += new ConnectEventHandler(iphone_Disconnect);
            iphonedriver = new iPhoneFileDevice(iphone);

            iphonedriver.OnCompleteHandler += new FileCompletedHandler(iphonedriver_OnCompleteHandler);
            iphonedriver.OnProgressHandler += new FileProgressHandler(iphonedriver_OnProgressHandler);
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

            m_themeManage = new iThemeBrowser((iPhoneFileDevice)iphonedriver, tabTheme);
            m_themeManage.OnMessage += new MessageHandler(ShowMessage);
        }      


        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            Initialise();
        }
        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (null != iphone)
            {
                //移除事件
                //if (iphone.Connect!=null) iphone.Connect -= new ConnectEventHandler(iphone_Connect);
                //iphone.Disconnect -= new ConnectEventHandler(iphone_Disconnect);
            }
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
                        m_iPhonePanel.Enabled = enable;
                        if (enable)
                        {
                            iSpriteContext.Current.AfterDeviceFinishConnected(iphone);
                            m_iPhonePanel.AfterDeviceFinishConnected();
                            m_themeManage.AfterDeviceFinishConnected();
                            iSpriteContext.Current.AfterDeviceFinishConnected(iphone);
                            ((iPhoneFileDevice)iphonedriver).AfterDeviceFinishConnected();
                        }
                    }
                ));
            }
            else
            {
                m_iPhonePanel.Enabled = enable;
                if (enable)
                {
                    iSpriteContext.Current.AfterDeviceFinishConnected(iphone);
                    m_iPhonePanel.AfterDeviceFinishConnected();
                    m_themeManage.AfterDeviceFinishConnected();
                    iSpriteContext.Current.AfterDeviceFinishConnected(iphone);
                    ((iPhoneFileDevice)iphonedriver).AfterDeviceFinishConnected();
                }
            }
        }
        #endregion        

        /// <summary>
        /// 消息处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="Message"></param>
        /// <param name="messagetype"></param>
        void ShowMessage(object sender, string Message, MessageTypeOption messagetype)
        {
            switch (messagetype)
            {
                case MessageTypeOption.Info:
                    MessageHelper.ShowInfo(Message);
                    break;

                case MessageTypeOption.Warning:
                    MessageHelper.ShowWarning(Message);
                    break;

                case MessageTypeOption.Error:
                    MessageHelper.ShowError(Message);
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
        /// <param name="totalSize"></param>
        /// <param name="completeSize"></param>
        /// <param name="speed"></param>
        /// <param name="file"></param>
        /// <param name="cancel"></param>
        void iphonedriver_OnProgressHandler(ulong totalSize, ulong completeSize, int speed, string file, ref bool cancel)
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
                progressBar.SetProgress(totalSize, completeSize);
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
    }
}
