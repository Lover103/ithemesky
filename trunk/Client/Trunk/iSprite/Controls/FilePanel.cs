using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace iSprite
{
    internal partial class FilePanel : UserControl
    {
        #region 变量定义
        string m_CurrentPath = string.Empty;

        /// <summary>
        /// 文件操作接口
        /// </summary>
        IFileDevice m_FileDevice;

        /// <summary>
        /// 地址工具条
        /// </summary>
		private iPathBar m_PathBar;
        /// <summary>
        /// 文件夹操作树
        /// </summary>
        private iTreeView m_FolderTree;
        /// <summary>
        /// 文件夹/文件视图
        /// </summary>
        private iListView m_DirectoryDetailList;
        /// <summary>
        /// 收藏夹操作类
        /// </summary>
        private Favourites m_Favourites;
		private bool m_ChangingFolderCheck = false;

		private int m_SplitDistance = 0;

		private string m_FileFilter = "*.*";
		private string m_PanelName = "";
		private string m_PreviousPath = "";
		private string m_RootPath = SystemInformation.ComputerName;
        #endregion

        #region 消息处理
        internal event MessageHandler OnMessage;

        /// <summary>
        /// 消息处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="Message"></param>
        /// <param name="messagetype"></param>
        private void RaiseMessageHandler(object sender, string Message, MessageTypeOption messagetype)
		{
            if (messagetype == MessageTypeOption.AddtoFavorites)
            {
                AddToFavourites();
            }
            else if (messagetype == MessageTypeOption.ReloadLoadFavorites)
            {
                //从地址工具条发送该消息
                this.LoadFavourites();
            }
            else
            {
                if (OnMessage != null)
                {
                    OnMessage(sender, Message, messagetype);
                }
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="filerName"></param>
		internal FilePanel(IFileDevice filedevice)
		{
            m_FileDevice = filedevice;
			InitializeComponent();
            if (!this.DesignMode)
            {
                InitFilePanel();
            }
		}
        private FilePanel()
        { 
        }

        public void InitFilePanel()
        {
			this.DoubleBuffered = true;

            m_PathBar = new iPathBar(m_FileDevice);
			panPathBar.Controls.Add(m_PathBar);
			m_PathBar.Dock = DockStyle.Top;
            m_PathBar.OnPathChanged += new PathChanged(OnPathChanged);


            m_FolderTree = new iTreeView(m_FileDevice, this.imageList);
			splitContainer1.Panel1.Controls.Add(m_FolderTree);
			m_FolderTree.BringToFront();
			m_FolderTree.Dock = DockStyle.Fill;
			m_FolderTree.Font = this.Font;
            m_FolderTree.OnPathChanged += new PathChanged(OnPathChanged);

            m_DirectoryDetailList = new iListView(m_FileDevice, this.imageList);
			splitContainer1.Panel2.Controls.Add(m_DirectoryDetailList);
			m_DirectoryDetailList.BringToFront();
			m_DirectoryDetailList.Dock = DockStyle.Fill;
			m_DirectoryDetailList.GridLines = mnuViewShowGridlines.Checked;
			m_DirectoryDetailList.Font = this.Font;
            m_DirectoryDetailList.OnPathChanged += new PathChanged(OnPathChanged);

			tsbtnLVFilter.ForeColor = Color.Black;

			m_SplitDistance = (int)splitContainer1.Width / 3;
			splitContainer1.SplitterDistance = m_SplitDistance;

            foreach (ToolStripItem item in tsTV.Items)
            {
                if (item is ToolStripButton)
                {
                    item.Click += new EventHandler(TreeViewCommandItem_Click);
                }
            }

            foreach (ToolStripItem item in ctxTVTools.Items)
            {
                if (item is ToolStripMenuItem)
                {
                    item.Click += new EventHandler(TreeViewCommandItem_Click);
                }
            }

            foreach (ToolStripItem item in tsListView.Items)
            {
                if (item is ToolStripButton)
                {
                    item.Click += new EventHandler(ListViewCommandItem_Click);
                }
            }

            foreach (ToolStripItem item in ctxLVTools.Items)
            {
                if (item is ToolStripMenuItem)
                {
                    item.Click += new EventHandler(ListViewCommandItem_Click);
                }
            }

            m_Favourites = new Favourites(m_FileDevice, ctxFavourites);
            m_Favourites.OnPathChanged += new PathChanged
               (
                   delegate(object sender, string newPath)
                   {
                       SetCurrentPath(newPath);
                   }
               );

            m_Favourites.OnMessage += new MessageHandler(Favourites_OnMessage);

            tsbtnFavourites.Click += new EventHandler
                (
                    delegate(object sender, EventArgs e)
                    {
                        this.AddToFavourites();
                    }
                );

            AfterDeviceFinishConnected();
        }
        #endregion

        #region 收藏动作消息
        /// <summary>
        /// 收藏动作消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="Message"></param>
        /// <param name="messagetype"></param>
        void Favourites_OnMessage(object sender, string Message, MessageTypeOption messagetype)
        {
            RaiseMessageHandler(sender, Message, messagetype);
        }
        #endregion

        #region Treeview的相关工具栏事件处理
        /// <summary>
        /// Treeview的相关工具栏事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TreeViewCommandItem_Click(object sender, EventArgs e)
        {
            ToolStripItem item = (ToolStripItem)sender;
            switch (item.Name)
            {
                case "tsbtnTVRefresh":
                case "mnuTVRefresh":
                    this.m_FolderTree.DoByCommandOption(ButtonCommandOption.Refresh);
                    break;
                case "tsbtnNewFolder":
                case "mnuTVNewFolder":
                    this.m_FolderTree.DoByCommandOption(ButtonCommandOption.NewFloder);
                    break;
                case "tsbtnDelete":
                case "mnuTVDeleteMarked":
                    this.m_FolderTree.DoByCommandOption(ButtonCommandOption.Delete);
                    break;
                case "tsbtnTVCopyPath":
                case "mnuTVCopyPath":
                    this.m_FolderTree.DoByCommandOption(ButtonCommandOption.CopyPath);
                    break;

                case "mnuTVCancel":
                    this.ctxTVTools.Hide();
                    break;
            }
        }
        #endregion

        #region Listview列表的工具栏事件处理
        /// <summary>
        /// Listview列表的工具栏事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ListViewCommandItem_Click(object sender, EventArgs e)
        {
            ToolStripItem item = (ToolStripItem)sender;
            switch (item.Name)
            {
                case "tsbnNewFolder":
                case "mnuLVNewFolder":
                    this.m_DirectoryDetailList.DoByCommandOption(ButtonCommandOption.NewFloder);
                    break;
                case "tsbnDelete":
                case "mnuLVDeleteMarked":
                    this.m_DirectoryDetailList.DoByCommandOption(ButtonCommandOption.Delete);
                    break;
                case "tsbtnLVRefresh":
                case "mnuLVRefresh":
                    this.m_DirectoryDetailList.DoByCommandOption(ButtonCommandOption.Refresh);
                    break;
                case "tsbtnLVCopyPath":
                case "mnuLVCopyPath":
                    this.m_DirectoryDetailList.DoByCommandOption(ButtonCommandOption.CopyPath);
                    break;
                case "mnuLVSelectAll":
                    this.m_DirectoryDetailList.DoByCommandOption(ButtonCommandOption.SelectAll);
                    break;
                case "mnuLVUnselectAll":
                    this.m_DirectoryDetailList.DoByCommandOption(ButtonCommandOption.UnSelectAll);
                    break;
                case "tsbtnShowFolders":
                    SetShowTreeView();
                    break;
                case "mnuLVEditItem":
                    this.m_DirectoryDetailList.DoByCommandOption(ButtonCommandOption.EditName);
                    break;
                case "mnuLVCancel":
                    this.ctxLVTools.Hide();
                    break;
            }
        }
        #endregion

        #region 加载收藏夹
        /// <summary>
        /// 加载收藏夹
        /// </summary>
        internal void LoadFavourites()
        {
            m_Favourites.Load(m_FileDevice.UserIdentity);
        }
        #endregion   

        #region 添加到收藏夹
        /// <summary>
        /// 添加到收藏夹
        /// </summary>
        internal void AddToFavourites()
        {
            string path = m_DirectoryDetailList.FirstFolderPath;

            if (path != string.Empty)
            {
                m_Favourites.Add(path, path);

                //加载另外一个收藏夹
                m_PathBar.LoadFavourites();
            }
        }
        #endregion   

        #region 设备正常连接事件处理
        /// <summary>
        /// 设备正常连接事件处理
        /// </summary>
        public void AfterDeviceFinishConnected()
        {
            if (m_FileDevice.IsConnected)
            {
                this.m_PanelName = m_FileDevice.DeviceName;

                gbxMain.Text = this.m_PanelName;

                SetCurrentPath(m_FileDevice.StartPath);

                m_PathBar.LoadFavourites();

                LoadFavourites();
            }
        }
        #endregion       
        
        #region 获取或设置面板名称
        /// <summary>
        /// 获取或设置面板名称
        /// </summary>
        internal string PanelName
        {
            get
            {
                return this.m_PanelName;
            }
            set
            {
                this.m_PanelName = value;

                gbxMain.Text = this.m_PanelName;
            }
        }
        #endregion    

        #region 列表的消息处理
        /// <summary>
        /// 列表的消息处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="Message"></param>
        /// <param name="messagetype"></param>
        void m_DirectoryDetailList_OnMessage(object sender, string Message, MessageTypeOption messagetype)
		{
            RaiseMessageHandler(sender, Message, messagetype);
        }
        #endregion

        #region 设置各个控件的当前路径
        void OnPathChanged(object sender, string path)
        {
            SetCurrentPath(sender, path);
        }
        void SetCurrentPath(string newPath)
        {
            if (m_CurrentPath != newPath)
            {
                m_PreviousPath = m_CurrentPath;
                m_CurrentPath = newPath;
                m_PathBar.CurrentPath = m_CurrentPath;
                m_DirectoryDetailList.CurrentPath = m_CurrentPath;
                m_FolderTree.CurrentPath = m_CurrentPath;
            }
        }
        /// <summary>
        /// 设置各个控件的当前路径（保持一致）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="newPath"></param>
		private void SetCurrentPath(object sender, string newPath)
		{
			//保存之前的路径
            m_CurrentPath = newPath;
			m_PreviousPath = m_CurrentPath;
			if (sender == m_FolderTree)
			{
                m_PathBar.CurrentPath=newPath;
				m_DirectoryDetailList.CurrentPath = newPath;
			}

			else if (sender == m_DirectoryDetailList)
            {
                m_PathBar.CurrentPath = newPath;
                m_FolderTree.CurrentPath = newPath;

			}
			else if (sender == m_PathBar)
			{
                m_FolderTree.CurrentPath = newPath;
                m_DirectoryDetailList.CurrentPath = newPath;
			}
			m_CurrentPath = newPath;
        }
        #endregion

        #region 设置菜单状态
        /// <summary>
        /// 设置菜单状态
        /// </summary>
        internal void UpdateMenuStatus()
		{
			bool blnStatus;
			blnStatus = (m_DirectoryDetailList.SelectedItems.Count >= 1);
            tsbnDelete.Enabled = mnuLVDeleteMarked.Enabled 
                = mnuLVCopyPath.Enabled = tsbtnLVCopyPath.Enabled
                = mnuLVEditItem.Enabled
                = blnStatus;

            blnStatus = this.m_FolderTree.SelectedNode != null;
            tsbtnDelete.Enabled = mnuTVDeleteMarked.Enabled 
                = tsbtnTVCopyPath.Enabled = mnuTVCopyPath.Enabled 
                = blnStatus;
        }
        #endregion

        #region 显示或隐藏文件夹树
        /// <summary>
        /// 显示或隐藏文件夹树
        /// </summary>
		private void SetShowTreeView()
		{
			if (m_ChangingFolderCheck)
				return;

			m_ChangingFolderCheck = true;

			mnuViewShowFolders.Checked = !mnuViewShowFolders.Checked;

			if (mnuViewShowFolders.Checked)
			{
				splitContainer1.SplitterDistance = m_SplitDistance;
				tsbtnShowFolders.ToolTipText = "Hide Folders";
				tsbtnShowFolders.Checked = true;
			}
			else
			{
				splitContainer1.SplitterDistance = 0;
				tsbtnShowFolders.ToolTipText = "Show Folders";
				tsbtnShowFolders.Checked = false;
			}
			m_ChangingFolderCheck = false;
        }
        #endregion

        #region 移动拆分器
        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
		{
			if (mnuViewShowFolders.Checked)
				m_SplitDistance = splitContainer1.SplitterDistance;
		}

		private void splitContainer1_SplitterMoving(object sender, SplitterCancelEventArgs e)
		{
			if (!mnuViewShowFolders.Checked)
				e.Cancel = true;
        }
        #endregion   

	}
}
