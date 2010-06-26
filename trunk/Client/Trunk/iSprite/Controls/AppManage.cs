using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections.Specialized;
using MyDownloader.Extension.Protocols;
using MyDownloader.Core;
using System.Web;
using System.IO;
using System.Threading;
using Manzana;

namespace iSprite
{
    internal class AppManage
    {
        #region 变量定义

        internal event PathChanged OnPathChanged;
        internal event MessageHandler OnMessage;

        iPhoneFileDevice m_iPhoneDevice;
        AppHelper m_appHelper;

        MainForm m_MainForm;
        private TreeView m_tvCatalog;
        SplitContainer m_appContainer;


        OnlineAppList m_OnlineAppList;
        AptInstalledList m_AptInstalledList;
        AptDownList m_AptDownList;
        CydiaSourceList m_CydiaSourceList;

        #endregion

        #region 消息处理
        private void RaiseMessageHandler(object sender, string Message, MessageTypeOption messageType)
        {
            if (OnMessage != null)
            {
                OnMessage(sender, Message, messageType);
            }
        }
        internal void RaisePathChanged(object sender, string newpath)
        {
            if (OnPathChanged != null)
            {
                OnPathChanged(sender, newpath);
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="filedevice"></param>
        /// <param name="parentForm"></param>
        internal AppManage(iPhoneFileDevice filedevice, Form parentForm)
        {
            m_iPhoneDevice = filedevice;
            m_MainForm = (MainForm)parentForm;
            m_tvCatalog = m_MainForm.tvCatalog;
            m_appContainer = m_MainForm.appContainer;
            m_appHelper = new AppHelper(m_iPhoneDevice);

            InitControls();            
        }
        #endregion

        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        void InitControls()
        {
            m_tvCatalog.AfterSelect += new TreeViewEventHandler(Catalog_AfterSelect);


            //在线软件列表
            m_OnlineAppList = new OnlineAppList(m_iPhoneDevice, m_appHelper);
            m_OnlineAppList.OnMessage += new MessageHandler(RaiseMessageHandler);
            m_OnlineAppList.OnDownloadApp += new AddURL2DownloadHandler(AddURL2Download);
            m_OnlineAppList.OnSetNodeCount += new SetNodeCountHandler(SetNodeCount);
            m_appContainer.Panel2.Controls.Add(m_OnlineAppList);
            m_OnlineAppList.Dock = System.Windows.Forms.DockStyle.Fill;
            m_OnlineAppList.Location = new System.Drawing.Point(0, 0);

            //下载列表
            m_AptDownList = new AptDownList(m_iPhoneDevice, m_appHelper);
            m_AptDownList.OnMessage += new MessageHandler(RaiseMessageHandler);
            m_AptDownList.OnSetNodeCount += new SetNodeCountHandler(SetNodeCount);
            m_appContainer.Panel2.Controls.Add(m_AptDownList);
            m_AptDownList.Dock = System.Windows.Forms.DockStyle.Fill;
            m_AptDownList.Location = new System.Drawing.Point(0, 0);

            //安装列表
            m_AptInstalledList = new AptInstalledList(m_iPhoneDevice, m_appHelper);
            m_AptInstalledList.OnMessage += new MessageHandler(RaiseMessageHandler);
            m_AptInstalledList.OnSetNodeCount += new SetNodeCountHandler(SetNodeCount);
            m_appContainer.Panel2.Controls.Add(m_AptInstalledList);
            m_AptInstalledList.Dock = System.Windows.Forms.DockStyle.Fill;
            m_AptInstalledList.Location = new System.Drawing.Point(0, 0);


            //cydia源
            m_CydiaSourceList = new CydiaSourceList(m_iPhoneDevice, m_appHelper);
            m_CydiaSourceList.OnSetNodeCount += new SetNodeCountHandler(SetNodeCount);
            m_CydiaSourceList.OnUpdataCatalogCount += new UpdataCatalogCountHandler(UpdataCatalogCount);
            m_CydiaSourceList.OnMessage += new MessageHandler(RaiseMessageHandler);
            m_appContainer.Panel2.Controls.Add(m_CydiaSourceList);
            m_CydiaSourceList.Dock = System.Windows.Forms.DockStyle.Fill;
            m_CydiaSourceList.Location = new System.Drawing.Point(0, 0);


            LoadAdminCatalog();
        }

        #endregion

        #region 更新类别数量
        /// <summary>
        /// 更新类别数量
        /// </summary>
        void UpdataCatalogCount()
        {
            List<string> c = new List<string>();
            foreach (KeyValuePair<string, CatalogItem> item in m_appHelper.Catalogs)
            {
                c.Add(item.Key);
            }
            c.Add("Others");
            CatalogItem catalog;
            foreach (string name in c)
            {
                if (m_appHelper.Catalogs.TryGetValue(name, out catalog))
                {
                    SetNodeCount(catalog.Name, catalog.Count, false);
                    Application.DoEvents();
                }
            }
            SetNodeCount("All Packages", m_appHelper.Packagelist.Count, false);
            m_OnlineAppList.LoadCatalogData(c, m_appHelper.AppNames);
        }
        #endregion

        #region 设置当前选中视图
        /// <summary>
        /// 添加到下载队列
        /// </summary>
        /// <param name="url"></param>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        void AddURL2Download(string url, string path, string fileName, InstallState state)
        {
            m_AptDownList.DownList.AddURL2Download(url, path, fileName, state);
        }
        #endregion

        #region 重新加载已经安装的软件列表
        /// <summary>
        /// 重新加载已经安装的软件列表
        /// </summary>
        public void SafeRefreshApp()
        {
            m_appHelper.LoadInstallDebs();
            if (m_MainForm.InvokeRequired)
            {
                m_MainForm.Invoke(new ThreadInvokeDelegate(
                    delegate()
                    {
                        m_AptInstalledList.UpdataList();
                    }
                ));
            }
            else
            {
                m_AptInstalledList.UpdataList();
            }
        }
        #endregion        
        
        #region 设置当前选中视图
        /// <summary>
        /// 设置当前选中视图
        /// </summary>
        /// <param name="nodeName"></param>
        void SetViewMode(string nodeName)
        {
            switch (nodeName)
            {
                case "Downloaded Packages":
                    m_AptDownList.BringToFront();
                    break;
                case "Installed Packages":
                    m_AptInstalledList.BringToFront();
                    break;
                case "Cydia Sources":
                    m_CydiaSourceList.BringToFront();
                    break;
                default:
                    m_OnlineAppList.BringToFront();
                    break;
            }
        }
        #endregion

        #region 更新下载列表
        /// <summary>
        /// 更新下载列表
        /// </summary>
        internal void UpdateDownloadList()
        {
            if (m_AptDownList != null)
            {
                m_AptDownList.UpdateDownloadList();
            }
        }
        #endregion

        #region 保存下载队列
        /// <summary>
        /// 保存下载队列
        /// </summary>
        internal void SaveDownQueue()
        {
            if (m_AptDownList != null)
            {
                m_AptDownList.SaveDownQueue();
            }
        }
        #endregion

        #region 树菜单选中事件处理
        /// <summary>
        /// 树菜单选中事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Catalog_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null)
            {
                if (e.Node.Tag.Equals("0"))
                {
                    m_OnlineAppList.GetPackagesByCatalog(e.Node.Name);
                }
                else if (e.Node.Tag.Equals("1"))
                {
                    switch (e.Node.Name)
                    {
                        case "All Packages":
                            m_OnlineAppList.GetPackagesByCatalog(e.Node.Name);
                            SetNodeCount(e.Node.Name, m_appHelper.Packagelist.Count,true);
                            break;
                        case "Search Result":
                            m_OnlineAppList.Search();
                            break;
                    }
                }

                SetViewMode(e.Node.Name);
            }
        }
        #endregion        

        #region iPhone连接后初始化
        /// <summary>
        /// iPhone连接后初始化
        /// </summary>
        /// <param name="isContected"></param>
        internal void AfterDeviceFinishConnected(bool isContected)
        {
            if (isContected)
            {
                ThreadStart threadStart = delegate
                {
                    m_appHelper.InitAppData
                        (
                            delegate()
                            {
                                m_MainForm.Invoke(new ThreadInvokeDelegate(
                                            delegate()
                                            {
                                                LoadCatalogs();
                                                m_AptInstalledList.UpdataList();
                                                m_CydiaSourceList.UpdataList();
                                                m_AptDownList.AfterDeviceFinishConnected(isContected);
                                            }
                                        ));
                            }
                        );
                };
                new Thread(threadStart).Start();
            }
        }
        #endregion

        #region 加载软件分类
        /// <summary>
        /// 加载软件分类
        /// </summary>
        void LoadCatalogs()
        {
            List<string> c = new List<string>();
            foreach (KeyValuePair<string, CatalogItem> item in m_appHelper.Catalogs)
            {
                c.Add(item.Key);
            }
            c.Sort();
            c.Remove("Others");
            c.Add("Others");
            CatalogItem catalog;
            foreach (string name in c)
            {
                if (m_appHelper.Catalogs.TryGetValue(name, out catalog))
                {
                    m_tvCatalog.Nodes.Add(
                        catalog.Name,
                        catalog.Name + "(" + catalog.Count + ")",
                        catalog.Name + ".png",
                        catalog.Name + ".png"
                        ).Tag = "0";

                    Application.DoEvents();
                }
            }
            m_tvCatalog.Enabled = true;

            m_OnlineAppList.LoadCatalogData(c, m_appHelper.AppNames);
            SetNodeCount("All Packages", m_appHelper.Packagelist.Count, true);
        }
        #endregion

        #region 加载树操作菜单
        /// <summary>
        /// 加载树操作菜单
        /// </summary>
        void LoadAdminCatalog()
        {
            m_tvCatalog.Nodes.Clear();
            Dictionary<string, string> dics = new Dictionary<string, string>();
            dics.Add("Cydia Sources", "Cydia Sources");
            dics.Add("Installed Packages", "Installed Packages");
            dics.Add("Downloaded Packages", "Downloaded Packages");
            dics.Add("Search Result", "Search Result");
            dics.Add("All Packages", "All Packages");
            try
            {
                foreach (KeyValuePair<string, string> item in dics)
                {
                    m_tvCatalog.Nodes.Add(item.Key, item.Value, item.Key + ".png", item.Key + ".png").Tag = "1"; ;
                    Application.DoEvents();
                }
            }
            catch
            { 
            }
            m_tvCatalog.Enabled = false;
        }
        #endregion

        #region 设置数量
        /// <summary>
        /// 设置数量
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="count"></param>
        void SetNodeCount(string nodeName, int count, bool selectNode)
        {
            TreeNode[] tnArray = m_tvCatalog.Nodes.Find(nodeName, false);
            if (tnArray.Length > 0)
            {
                TreeNode tnFind = null;
                tnFind = tnArray[0];
                if (tnFind != null)
                {
                    m_MainForm.BeginInvoke(
                        (MethodInvoker)delegate() 
                        {
                            tnFind.Text = tnFind.Name + "(" + count + ")";

                            if (selectNode)
                            {
                                m_tvCatalog.SelectedNode = tnFind;
                                m_tvCatalog.Focus();
                            }
                        }
                    );                    
                }
            }
        }
        #endregion
    }
}
