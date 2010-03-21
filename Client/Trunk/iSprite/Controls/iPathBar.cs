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
    /// <summary>
    /// 路径下拉框
    /// </summary>
    public partial class iPathBar : UserControl
    {
        #region 变量定义
        List<string> m_historyPath = new List<string>();
        const int showHistoryCount = 10;
        int historyIndex = 0;
		private string m_PreviousPath = "";
        IFileDevice m_FileDevice;
        private Favourites m_Favourites;
        internal event MessageHandler OnMessage;
        internal event PathChanged OnPathChanged;
        #endregion

        #region 消息通知
        private void RaiseMessageHandler(object sender, string Message, MessageTypeOption messageType)
		{
            if (messageType == MessageTypeOption.AddtoFavorites)
            {
                Add2Favorites();
            }
            else
            {
                if (OnMessage != null)
                {
                    OnMessage(sender, Message, messageType);
                }
            }
		}
        private void RaisePathChanged(object sender, string newPath)
		{
			if (OnPathChanged != null)
			{
                OnPathChanged(sender, newPath);
			}
        }
        #endregion

        #region 设置当前路径
        string m_CurrentPath = string.Empty;
        /// <summary>
        /// 当前路径
        /// </summary>
        internal string CurrentPath
        {
            get { return this.m_CurrentPath; }
            set { SetCurrentPath(value,false); }
        }

        void SetCurrentPath(string newpath, bool isBack)
        {
            if (m_CurrentPath != newpath)
            {
                m_PreviousPath = m_CurrentPath;
                m_CurrentPath = newpath;

                if (!isBack)
                {
                    m_historyPath.Insert(0, m_CurrentPath);
                }

                cbbpathlist.Items.Clear();
                cbbpathlist.Items.Add(m_CurrentPath);
                foreach (string path in m_historyPath)
                {
                    if (path != m_CurrentPath)
                    {
                        int index = cbbpathlist.Items.IndexOf(path);
                        if (index != -1)
                        {
                            cbbpathlist.Items.RemoveAt(index);
                        }
                        cbbpathlist.Items.Add(path);
                        if (cbbpathlist.Items.Count >= showHistoryCount)
                        {
                            break;
                        }
                    }
                }
                if (cbbpathlist.Items.Count > 0)
                {
                    cbbpathlist.SelectedIndex = 0;
                }

                btnUp.Enabled = (m_CurrentPath != m_FileDevice.StartPath);
                btnBack.Enabled = m_historyPath.Count > 1;
            }
        }
        #endregion

        #region 当前选择的路径
        /// <summary>
        /// 当前选择的路径
        /// </summary>
        string SelectPath
        {
            get
            {
                if (cbbpathlist.SelectedItem != null)
                {
                    return this.cbbpathlist.SelectedItem.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="filedevice"></param>
        internal iPathBar(IFileDevice filedevice)
		{
			InitializeComponent();
            m_FileDevice = filedevice;
			if (!this.DesignMode)
			{
                PathBarInitialise();
                m_Favourites = new Favourites(m_FileDevice, ctxFavourites);
                m_Favourites.OnPathChanged += new PathChanged(Favourites_OnPathChanged);
                m_Favourites.OnMessage += new MessageHandler(Favourites_OnMessage);
                cbbpathlist.SelectionChangeCommitted += new EventHandler(cbbpathlist_SelectionChangeCommitted);
			}
		}
        
        private iPathBar()
        {
        }
        #endregion

        #region 路径改变事件
        void Favourites_OnPathChanged(object sender, string newPath)
        {
            CurrentPath = newPath;
            RaisePathChanged(this, newPath);
        }

        void cbbpathlist_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string selectpath = SelectPath;
            if (selectpath != string.Empty)
            {
                if (m_FileDevice.DirectoryExists(selectpath))
                {
                    RaisePathChanged(this, selectpath);
                }
                else
                {
                    RaiseMessageHandler(this, selectpath + " is not exists", MessageTypeOption.Error);
                }                
            }
        }
        #endregion

        #region 改变下拉框大小相关处理
        private void PathBar_Layout(object sender, LayoutEventArgs e)
		{
			PathBarResize();
		}

		private void PathBar_Resize(object sender, EventArgs e)
		{
			PathBarResize();
		}

		private void PathBarInitialise()
        {
            PathBarResize();
		}

		private void PathBarResize()
		{
            cbbpathlist.Width = tsMain.Width - (btnUp.Width * 3) - tsbtnFavourites.Width - 8;
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

        #region 后退事件处理
        /// <summary>
        /// 后退事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, EventArgs e)
        {
            if (m_historyPath.Count>1)
            {
                historyIndex += 1;
                if (historyIndex >= m_historyPath.Count)
                {
                    historyIndex = 0;
                }

                string selectpath = m_historyPath[historyIndex];

                SetCurrentPath(selectpath, true);
                RaisePathChanged(this, selectpath);
            }
        }
        #endregion

        #region 向上路径处理
        /// <summary>
        /// 向上路径处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUp_Click(object sender, EventArgs e)
        {
            string selectpath = SelectPath;
            if (string.Empty != selectpath)
            {
                selectpath = selectpath.TrimEnd(m_FileDevice.DirectorySeparatorChar);
                int index = selectpath.LastIndexOf(m_FileDevice.DirectorySeparatorChar);
                if (index != -1)
                {
                    CurrentPath = selectpath.Substring(0, index+1);
                    RaisePathChanged(this, CurrentPath);
                }
            }
        }
        #endregion

        #region 跳转路径处理
        /// <summary>
        /// 跳转路径处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnGo_Click(object sender, EventArgs e)
        {
            GoToPath();
        }

        /// <summary>
        /// 跳转路径处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbbpathlist_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GoToPath();
            }
        }

        void GoToPath()
        {
            string selectpath = this.cbbpathlist.Text;
            if (selectpath != string.Empty)
            {
                if (m_FileDevice.DirectoryExists(selectpath))
                {
                    RaisePathChanged(this, selectpath);
                }
                else
                {
                    RaiseMessageHandler(this, selectpath + " is not exists", MessageTypeOption.Error);
                }
            }
        }
        #endregion

        #region 添加到收藏夹事件
        /// <summary>
        /// 添加到收藏夹事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnFavourites_ButtonClick(object sender, EventArgs e)
        {
            Add2Favorites();
        }
        void Add2Favorites()
        {
            string selectpath = this.cbbpathlist.Text;
            if (selectpath != string.Empty)
            {
                if (m_FileDevice.DirectoryExists(selectpath))
                {
                    m_Favourites.Add(selectpath, selectpath);

                    RaiseMessageHandler(this, string.Empty, MessageTypeOption.ReloadLoadFavorites);
                }
            }
        }
        #endregion

        #region 加载收藏夹
        /// <summary>
        /// 加载收藏夹
        /// </summary>
        internal void LoadFavourites()
        {
            m_Favourites.Load(m_FileDevice.DeviceName);
        }
        #endregion

    }
}
