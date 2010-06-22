using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace iSprite
{
    internal partial class AptInstalledList : UserControl
    {
        #region 变量定义
        AppHelper m_appHelper;
        internal event MessageHandler OnMessage;
        public event SetNodeCountHandler OnSetNodeCount;
        iPhoneFileDevice m_iPhoneDevice;
        ListView m_InstalledAppList;
        ListViewGroup m_UserGroup = new ListViewGroup();
        ListViewGroup m_SystemGroup = new ListViewGroup();
        #endregion

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
        public AptInstalledList(iPhoneFileDevice iphoneDevice, AppHelper appHelper)
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
            this.toolbtnRemove.Click += new EventHandler
                (
                    delegate(object sender, EventArgs e)
                    {
                        RemoveApps();
                    }
                );
        }
        #endregion


        #region 添加控件
        /// <summary>
        /// 添加控件
        /// </summary>
        void AddControls()
        {
            m_InstalledAppList = new ListView();
            this.Controls.Add(this.m_InstalledAppList);
            this.m_InstalledAppList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_InstalledAppList.Location = new System.Drawing.Point(0, 25);

            m_InstalledAppList.FullRowSelect = true;
            m_InstalledAppList.View = View.Details;
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(18, 18);//分别是宽和高
            imgList.Images.Add(global::iSprite.Resource.normal_App);
            imgList.Images.Add(global::iSprite.Resource.system_app);
            m_InstalledAppList.SmallImageList = imgList;

            ColumnHeader columnFile = new ColumnHeader();
            ColumnHeader columnSize = new ColumnHeader();
            ColumnHeader columnDescription = new ColumnHeader();
            ColumnHeader columnVersion = new ColumnHeader();

            columnFile.Text = "Name";
            columnFile.Width = 180;

            columnVersion.Text = "Version";
            columnVersion.Width = 80;

            columnSize.Text = "Size";
            columnSize.Width = 80;

            columnDescription.Text = "Description";
            columnDescription.Width = 360;

            m_InstalledAppList.Columns.AddRange(
                new ColumnHeader[] 
                {
                    columnFile,
                    columnVersion,
                    columnSize,
                    columnDescription,
                }
            );


            m_UserGroup.Header = "User Deb";
            m_UserGroup.Tag = m_UserGroup.Header;
            m_UserGroup.HeaderAlignment = HorizontalAlignment.Left;
            m_InstalledAppList.Groups.Add(m_UserGroup);

            m_SystemGroup.Header = "System Deb";
            m_SystemGroup.Tag = m_SystemGroup.Header;
            m_SystemGroup.HeaderAlignment = HorizontalAlignment.Left;
            m_InstalledAppList.Groups.Add(m_SystemGroup);

        }
        #endregion

        #region 删除程序
        /// <summary>
        /// 删除程序
        /// </summary>
        public void RemoveApps()
        {
            if (m_InstalledAppList.SelectedItems.Count > 0)
            {
                if (MessageHelper.ShowConfirm("Are you sure you want to delete the selected apps ? ") == DialogResult.OK)
                {
                    try
                    {
                        m_InstalledAppList.BeginUpdate();

                        bool flag = false;
                        for (int i = m_InstalledAppList.SelectedItems.Count - 1; i >= 0; i--)
                        {
                            ListViewItem item = m_InstalledAppList.SelectedItems[i];
                            InstalledDebItem debitem = (InstalledDebItem)item.Tag;
                            if (!debitem.IsSystemDeb)
                            {
                                string msg = string.Empty;
                                if (!SSHHelper.UnInstallDeb(m_iPhoneDevice, debitem.Package, out msg))
                                {
                                    MessageHelper.ShowError(msg);
                                    break;
                                }
                                else
                                {
                                    m_appHelper.RemoveInstallDeb(debitem);
                                    m_InstalledAppList.Items.RemoveByKey(item.Name);
                                    flag = true;
                                }
                            }
                        }
                        if (flag)
                        {
                            m_iPhoneDevice.Respring();
                        }
                    }
                    finally
                    {
                        UpdateGroupCount();
                        m_InstalledAppList.EndUpdate();
                    }
                }
            }
        }
        #endregion

        #region 加载数据
        /// <summary>
        /// 加载数据
        /// </summary>
        /// <returns></returns>
        public void UpdataList()
        {
            Dictionary<string, InstalledDebItem> InstalledList = m_appHelper.InstalledDebList;
            m_InstalledAppList.Items.Clear();

            foreach (KeyValuePair<string, InstalledDebItem> kv in InstalledList)
            {
                InstalledDebItem item = kv.Value;
                if (item.Status.Equals("purge ok not-installed", StringComparison.CurrentCultureIgnoreCase))
                {
                    continue;
                }

                ListViewItem rowitem = new ListViewItem();
                rowitem.Name = item.DebID;
                if (item.IsSystemDeb)
                {
                    rowitem.ImageIndex = 1;
                    rowitem.Group = m_SystemGroup;
                }
                else
                {
                    rowitem.ImageIndex = 0;
                    rowitem.Group = m_UserGroup;
                }
                if (item.Name == string.Empty)
                {
                    item.Name = item.Package;
                }
                rowitem.Text = " " + item.Name;

                rowitem.SubItems.Add(item.Version);

                rowitem.SubItems.Add(item.Installed_Size);

                rowitem.SubItems.Add(item.Description);

                rowitem.Tag = item;

                m_InstalledAppList.Items.Add(rowitem);
            }
            UpdateGroupCount();
        }

        void UpdateGroupCount()
        {
            m_UserGroup.Header = m_UserGroup.Tag + "(" + m_UserGroup.Items.Count + ")";
            m_SystemGroup.Header = m_SystemGroup.Tag + "(" + m_SystemGroup.Items.Count + ")";

            int count = m_InstalledAppList.Items.Count;
            SetNodeCount("Installed Packages", count, false);
        }
        #endregion
    }
}
