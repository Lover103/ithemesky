using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace iSprite
{
    internal partial class CydiaSourceList : UserControl
    {
        #region 变量定义
        AppHelper m_appHelper;
        internal event MessageHandler OnMessage;
        public event SetNodeCountHandler OnSetNodeCount;
        public event UpdataCatalogCountHandler OnUpdataCatalogCount;
        iPhoneFileDevice m_iPhoneDevice;
        ListView m_SourceList;
        ContextMenuStrip m_ctxTools;
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

        #region 更新类别数量
        /// <summary>
        /// 更新类别数量
        /// </summary>
        void UpdataCatalogCount()
        {
            if (null != OnUpdataCatalogCount)
            {
                OnUpdataCatalogCount();
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
        public CydiaSourceList(iPhoneFileDevice iphoneDevice, AppHelper appHelper)
        {
            m_iPhoneDevice = iphoneDevice;
            m_appHelper = appHelper;

            AddControls();

            InitializeComponent();

            InitControls();

        }
        #endregion

        #region 添加控件
        /// <summary>
        /// 添加控件
        /// </summary>
        void AddControls()
        {
            m_SourceList = new ListView();
            this.Controls.Add(this.m_SourceList);
            this.m_SourceList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_SourceList.Location = new System.Drawing.Point(0, 25);

            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(22, 22);//分别是宽和高
            imgList.Images.Add(global::iSprite.Resource.site);
            m_SourceList.SmallImageList = imgList;

            m_SourceList.FullRowSelect = true;
            m_SourceList.View = View.Details;
            m_SourceList.GridLines = true;


            ColumnHeader columnFile = new ColumnHeader();
            ColumnHeader columnURL = new ColumnHeader();
            ColumnHeader columnDescription = new ColumnHeader();
            ColumnHeader columnLastUpdate = new ColumnHeader();

            columnFile.Text = "Name";
            columnFile.Width = 120;

            columnURL.Text = "URL";
            columnURL.Width = 150;

            columnLastUpdate.Text = "Last Update";
            columnLastUpdate.Width = 120;

            columnDescription.Text = "Description";
            columnDescription.Width = 360;

            m_SourceList.Columns.AddRange(
                new ColumnHeader[] 
                {
                    columnURL,
                    columnFile,
                    columnLastUpdate,
                    columnDescription,
                }
            );

        }
        #endregion
        
        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        void InitControls()
        {
            #region 行编辑相关
            m_SourceList.LabelEdit = true;
            m_SourceList.BeforeLabelEdit += new LabelEditEventHandler(SourceList_BeforeLabelEdit);
            m_SourceList.AfterLabelEdit += new LabelEditEventHandler(SourceList_AfterLabelEdit);
            #endregion

            m_ctxTools = new ContextMenuStrip(this.components);
            m_SourceList.ContextMenuStrip = m_ctxTools;
            ToolStripButton btn;
            foreach (ToolStripItem item in toolapp.Items)
            {
                if (item is ToolStripButton)
                {
                    btn = new ToolStripButton(item.Text);
                    btn.Click += new EventHandler(ToolStripButton_Click);
                    item.Click += new EventHandler(ToolStripButton_Click);
                    m_ctxTools.Items.Add(btn);
                    m_ctxTools.Items.Add(new ToolStripSeparator());
                }
            }
            btn = new ToolStripButton("Cancel");
            btn.Click += new EventHandler(ToolStripButton_Click);
            m_ctxTools.Items.Add(btn);

        }

        void ToolStripButton_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripButton)
            {
                ToolStripButton btn = (ToolStripButton)sender;
                switch (btn.Text)
                {
                    case "Add Source":
                        AddSource();
                        break;
                    case "Update Source":
                        UpdateSource();
                        break;
                    case "Remove Source":
                        RemoveSource();
                        break;
                    case "Cancel":
                        m_ctxTools.Hide();
                        break;
                }
            }
        }
        #endregion

        #region 行编辑相关
        void SourceList_BeforeLabelEdit(object sender, LabelEditEventArgs e)
        {
            ListViewItem rowitem = m_SourceList.Items[e.Item];
            if (rowitem.Tag is RepositoryInfo)
            {
                e.CancelEdit = true;
            }
            
        }

        void SourceList_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Label != null && e.Label.Length > 7)
            {
                int errCode = 0;
                ListViewItem rowitem = m_SourceList.Items[e.Item];

                RepositoryInfo repItem = new RepositoryInfo();
                repItem.URL = e.Label;

                RaiseMessageHandler(this, "Prepare to add a new cydia source...", MessageTypeOption.SetStatusBar);

                if (m_appHelper.AddCydiaSource(repItem, out errCode))
                {
                    rowitem.Name = repItem.Name;
                    rowitem.Text = repItem.URL;
                    rowitem.SubItems.Add(repItem.Name);
                    rowitem.SubItems.Add("");
                    rowitem.SubItems.Add(repItem.Description);
                    rowitem.Tag = repItem;

                    RaiseMessageHandler(this, "Success to add a new cydia source", MessageTypeOption.HiddenStatusBar);

                    if (MessageHelper.ShowConfirm("Success to add a Cydia source, Do you want to update it now ? ") == DialogResult.OK)
                    {
                        UnSelectAllItems();
                        rowitem.Selected = true;
                        UpdateSource();
                    }
                }
                else
                {
                    if (errCode == 1)
                    {
                        MessageHelper.ShowError("Sorry, Current Cydia source is exists!");
                    }
                    else
                    {
                        MessageHelper.ShowError("Current Url is not a valid Cydia source!");
                    }
                    RaiseMessageHandler(this, string.Empty, MessageTypeOption.HiddenStatusBar);
                    rowitem.BeginEdit();
                }
            }
            else
            {
                try
                {
                    e.CancelEdit = true;
                    ListViewItem rowitem = m_SourceList.Items[e.Item];
                    if (null == rowitem.Tag)
                    {
                        m_SourceList.Items.Remove(rowitem);
                    }
                }
                catch
                { 
                }
            }
        }
        #endregion

        #region 取消所有选中状态
        /// <summary>
        /// 取消所有选中状态
        /// </summary>
        void UnSelectAllItems()
        {
            foreach (ListViewItem item in m_SourceList.Items)
            {
                item.Selected = false;
            }
        }
        #endregion

        #region 添加源
        /// <summary>
        /// 添加源
        /// </summary>
        void AddSource()
        {
            ListViewItem rowitem = new ListViewItem();
            rowitem.ImageIndex = 0;
            rowitem.Text = "http://";
            m_SourceList.Items.Add(rowitem);
            rowitem.BeginEdit();
        }
        #endregion

        #region 更新源
        /// <summary>
        /// 更新源
        /// </summary>
        void UpdateSource()
        {
            if (m_SourceList.SelectedItems.Count > 0)
            {
                RaiseMessageHandler(this, "Prepare to update cydia source...", MessageTypeOption.SetStatusBar);
                List<ListViewItem> selectedItems = new List<ListViewItem>();
                foreach (ListViewItem item in m_SourceList.SelectedItems)
                {
                    selectedItems.Add(item);
                }

                ThreadStart threadStart = delegate 
                { 
                    m_appHelper.UpdateCydiaSource
                        (
                            selectedItems,
                            delegate(ListViewItem item, bool finished)
                            {
                                this.Invoke(new ThreadInvokeDelegate(
                                            delegate()
                                            {
                                                RepositoryInfo repItem = (RepositoryInfo)item.Tag;
                                                if (!finished)
                                                {
                                                    RaiseMessageHandler(this, "Begin to update cydia source (" + repItem.Name + ")", MessageTypeOption.SetStatusBar);
                                                }
                                                else
                                                {
                                                    RaiseMessageHandler(this, "Finish update cydia source (" + repItem.Name + ")", MessageTypeOption.SetStatusBar);
                                                    item.SubItems[1].Text = repItem.Name;
                                                    item.SubItems[2].Text = repItem.LastUpdate;
                                                    item.SubItems[3].Text = repItem.Description;
                                                }
                                            }
                                        ));
                            },
                            delegate(int sucessCount)
                            {
                                m_appHelper.SaveAptSourceCfg();
                                string msg = string.Empty;
                                if (sucessCount > 0)
                                {
                                    msg = "Success to  update " + sucessCount + " cydia source(s).";
                                }
                                else
                                {
                                    msg = "Fail to  update all cydia source.";
                                }
                                this.Invoke(new ThreadInvokeDelegate(
                                            delegate()
                                            {
                                                RaiseMessageHandler(this, msg, MessageTypeOption.HiddenStatusBar);
                                                UpdataCatalogCount();
                                            }
                                        ));
                            }
                        ); 
                };
                new Thread(threadStart).Start();

            }
            else
            {
                MessageHelper.ShowWarning("Please select a cydia source to update!");
            }
        }
        #endregion

        #region 删除源
        /// <summary>
        /// 删除源
        /// </summary>
        void RemoveSource()
        {
            if (m_SourceList.SelectedItems.Count > 0)
            {
                if (MessageHelper.ShowConfirm("Are you sure you want to remove the selected cydia source ? ") == DialogResult.OK)
                {
                    RepositoryInfo repItem;

                    for (int i = m_SourceList.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        ListViewItem item = m_SourceList.SelectedItems[i];
                        repItem = (RepositoryInfo)item.Tag;
                        m_appHelper.RemoveCydiaSource(repItem);
                        m_SourceList.Items.Remove(item);
                    }
                    m_appHelper.SaveAptSourceCfg();
                    UpdataCatalogCount();
                }
            }
            else
            {
                MessageHelper.ShowError("Please select a cydia source to remove!");
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
            Dictionary<string, RepositoryInfo> RepositoryList = m_appHelper.RepositoryList;
            m_SourceList.Items.Clear();

            foreach (KeyValuePair<string, RepositoryInfo> kv in RepositoryList)
            {
                RepositoryInfo item = kv.Value;

                ListViewItem rowitem = new ListViewItem();
                rowitem.Name = item.Name;
                rowitem.ImageIndex = 0;

                // URL
                rowitem.Text = item.URL;

                rowitem.SubItems.Add(item.Name);

                // LastUpdate
                rowitem.SubItems.Add(item.LastUpdate);

                // Description
                rowitem.SubItems.Add(item.Description);


                rowitem.Tag = item;

                m_SourceList.Items.Add(rowitem);
            }

            SetNodeCount("Cydia Sources", m_SourceList.Items.Count, false);
        }
        #endregion
    }
}
