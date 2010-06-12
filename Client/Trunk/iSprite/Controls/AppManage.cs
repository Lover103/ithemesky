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

namespace iSprite
{
    internal class AppManage
    {
        #region 变量定义
        iPhoneFileDevice m_iPhoneDevice;
        internal event PathChanged OnPathChanged;
        internal event MessageHandler OnMessage;
        MainForm mainform;

        private TreeView m_tvCatalog;

        AppHelper appHelper;
        iPager m_appPager;
        WebBrowser m_appwb;
        Dictionary<string, PackageItem> m_Packagelist;
        DownloadList m_DownloadList;

        SplitContainer m_appContainer;
        Panel m_app_Paneltop;
        Panel m_app_Panelbutton;

        AppSearchBar m_AppSearchBar;
        AppDownloadBar m_AppDownloadBar;

        string m_SearchKey = string.Empty;
        DateTime m_LastSaveDownQueueTime = DateTime.MaxValue;
        static object m_InstallLock = new object();
        InstalledAppList m_InstalledAppList;
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
            mainform = (MainForm)parentForm;
            m_tvCatalog = mainform.tvCatalog;
            m_appwb = mainform.appwb;
            m_appPager = mainform.appPager;

            m_appContainer= mainform.appContainer;
            m_app_Paneltop = mainform.app_Paneltop;
            m_app_Panelbutton = mainform.app_Panelbuttom;

            appHelper = new AppHelper(m_iPhoneDevice);

            InitControls();            
        }
        #endregion

        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        void InitControls()
        {
            m_appPager.OnPageChanged += new PageChangedEventHandler(Pager_OnPageChanged);
            m_tvCatalog.AfterSelect += new TreeViewEventHandler(Catalog_AfterSelect);

            m_DownloadList = new DownloadList();
            m_appContainer.Panel2.Controls.Add(m_DownloadList);
            m_DownloadList.Dock = System.Windows.Forms.DockStyle.Fill;
            m_DownloadList.Location = new System.Drawing.Point(0, 39);

            m_DownloadList.OnDoInstall += new InstallAppHandler(DownloadList_OnDoInstall);
            m_DownloadList.OnSaveDownQueue += new SaveDownQueueHandler(DownloadList_OnSaveDownQueue);

            m_AppSearchBar = new AppSearchBar();
            m_app_Paneltop.Controls.Add(m_AppSearchBar);
            m_AppSearchBar.Dock = System.Windows.Forms.DockStyle.Fill;
            m_AppSearchBar.OnSearch += new SearchHandler(AppSearchBar_OnSearch);

            m_AppDownloadBar = new AppDownloadBar();
            m_app_Paneltop.Controls.Add(m_AppDownloadBar);
            m_AppDownloadBar.Dock = System.Windows.Forms.DockStyle.Fill;
            m_AppDownloadBar.OnDoAction += new AppDownloadBarEventHandler(AppDownloadBar_OnDoAction);

            m_appwb.Navigating += new WebBrowserNavigatingEventHandler(Appwb_Navigating);

            LoadAdminCatalog();


            m_InstalledAppList = new InstalledAppList(m_iPhoneDevice, appHelper);
            m_appContainer.Panel2.Controls.Add(m_InstalledAppList);
            m_InstalledAppList.Dock = System.Windows.Forms.DockStyle.Fill;
            m_InstalledAppList.Location = new System.Drawing.Point(0, 39);

            //注册下载可以支持的协议类型
            ProtocolProviderFactory.RegisterProtocolHandler("http", typeof(HttpProtocolProvider));
            ProtocolProviderFactory.RegisterProtocolHandler("https", typeof(HttpProtocolProvider));
            ProtocolProviderFactory.RegisterProtocolHandler("ftp", typeof(FtpProtocolProvider));
            DownloadManager.Instance.LoadDownQueueFromFile(appHelper.DownQueueFile);//从保存的下载队列恢复
            m_LastSaveDownQueueTime = DateTime.Now;
        }

        void DownloadList_OnSaveDownQueue()
        {
            SaveDownQueue();
        }

        void Appwb_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            string url = e.Url.ToString();
            if (url.Contains("dl.aspx?"))
            {
                e.Cancel = true;
                try
                {
                    NameValueCollection nv = HttpUtility.ParseQueryString(e.Url.Query, Encoding.UTF8);
                    string fileName = nv["name"];
                    if (string.IsNullOrEmpty(fileName))
                    {
                        fileName = Path.GetRandomFileName() + ".deb";
                    }

                    fileName = Utility.GetNotExistFileName(fileName);
                    m_DownloadList.AddURL2Download(url, appHelper.AptDownloadFolder, fileName);
                }
                catch
                { 
                }
            }
        }

        void AppDownloadBar_OnDoAction(ActionOption option)
        {
            if (null != m_DownloadList)
            {
                switch (option)
                {
                    case ActionOption.Start:
                        m_DownloadList.StartSelections();
                        break;
                    case ActionOption.Pause:
                        m_DownloadList.PauseSelections();
                        break;
                    case ActionOption.Remove:
                        m_DownloadList.RemoveSelections();
                        break;
                }
            }
        }

        void AppSearchBar_OnSearch(string key, string catalogName)
        {
            m_SearchKey = key;
            if (string.IsNullOrEmpty(m_SearchKey))
            {
                GetPackagesByCatalog(catalogName);
            }
            else
            {
                m_Packagelist = appHelper.SearchPackages(appHelper.Packagelist, catalogName, m_SearchKey);
                BindAppData();
            }
            SelectNode("Search Result");
            SetAppCount("Search Result", m_Packagelist.Count);
        }
        #endregion

        void DownloadList_OnDoInstall(Downloader d)
        {
            if (null != d && d.InstallCode == InstallState.Prepare2Install)
            {
                lock (m_InstallLock)
                {
                    if (d.InstallCode == InstallState.Prepare2Install)
                    {
                        d.InstallCode = InstallState.Installing;

                        m_iPhoneDevice.CheckDirectoryExists("/tmp/");
                        m_iPhoneDevice.Copy2iPhone(d.LocalFile, "/tmp/");
                        bool flag = SSHHelper.RunCmd("dpkg -i \"/tmp/" + Path.GetFileName(d.LocalFile) + "\"");
                        if (flag)
                        {
                            m_iPhoneDevice.Respring();
                            d.InstallCode = InstallState.Prepare2Install;
                            MessageHelper.ShowInfo(Path.GetFileName(d.LocalFile) + " has been successfully Installed .");
                        }
                        else
                        {
                        }
                    }
                }
            }
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
                    new Thread(new ThreadStart(SaveDownQueue)).Start();
                }
            }
        }
        internal void SaveDownQueue()
        {
            m_LastSaveDownQueueTime = DateTime.Now;
            DownloadManager.Instance.SaveDownQueue(appHelper.DownQueueFile);
        }
        #endregion

        void SetViewMode(string nodeName)
        {
            switch (nodeName)
            {
                case "Downloaded Packages":
                    m_DownloadList.BringToFront();
                    m_AppDownloadBar.BringToFront();
                    break;
                case "Installed Packages":
                    m_InstalledAppList.BringToFront();
                    break;
                default:
                    m_appwb.BringToFront();
                    m_AppSearchBar.BringToFront();
                    break;
            }
        }

        #region 搜索
        /// <summary>
        /// 搜索
        /// </summary>
        void Search()
        {
            
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
                    GetPackagesByCatalog(e.Node.Name);
                }
                else if (e.Node.Tag.Equals("1"))
                {
                    switch (e.Node.Name)
                    {
                        case "All Packages":
                            SetAppCount(e.Node.Name, appHelper.Packagelist.Count);
                            GetPackagesByCatalog(e.Node.Name);
                            break;
                    }
                }

                SetViewMode(e.Node.Name);
            }
        }
        #endregion

        #region 获取指定分类的软件列表
        /// <summary>
        /// 获取指定分类的软件列表
        /// </summary>
        /// <param name="name"></param>
        void GetPackagesByCatalog(string name)
        {
            m_SearchKey = string.Empty;
            if (string.IsNullOrEmpty(name) || name == "All Packages")
            {
                m_Packagelist = appHelper.Packagelist;
            }
            else
            {
                m_Packagelist = appHelper.GetPackagesByCatalog(appHelper.Packagelist, name);
            }
            BindAppData();
        }
        #endregion

        #region 软件列表分页事件处理
        /// <summary>
        /// 软件列表分页事件处理
        /// </summary>
        /// <param name="e"></param>
        void Pager_OnPageChanged(PageChangedEventArgs e)
        {
            m_appPager.CurrentPageIndex = e.NewPageIndex;
            BindAppData();
        }
        #endregion

        #region 绑定软件列表
        /// <summary>
        /// 绑定软件列表
        /// </summary>
        void BindAppData()
        {
            Dictionary<string, PackageItem> packages = m_Packagelist;
            m_appPager.RecordCount = packages.Count;
            m_appPager.DataBind();
            m_appwb.Navigate(appHelper.Packages2Html(packages, m_appPager.PageSize, m_appPager.CurrentPageIndex, m_SearchKey));
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
                appHelper.InitAppData();
                LoadCatalogs();            
                SelectNode("All Packages");
                m_InstalledAppList.LoadData();
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
            foreach (KeyValuePair<string, CatalogItem> item in appHelper.Catalogs)
            {
                c.Add(item.Key);
            }
            c.Sort();
            c.Remove("Others");
            c.Add("Others");
            CatalogItem catalog;
            foreach (string name in c)
            {
                if (appHelper.Catalogs.TryGetValue(name, out catalog))
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

            m_AppSearchBar.LoadData(c,appHelper.AppNames);
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
            dics.Add("Favorites", "Favorites");
            dics.Add("Search Result", "Search Result");
            dics.Add("Installed Packages", "Installed Packages");
            dics.Add("Downloaded Packages", "Downloaded Packages");
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

        #region 选中节点
        /// <summary>
        /// 选中节点
        /// </summary>
        /// <param name="nodeName"></param>
        void SelectNode(string nodeName)
        {
            TreeNode[] tnArray = m_tvCatalog.Nodes.Find(nodeName, false);
            if (tnArray.Length > 0)
            {
                TreeNode tnFind = null;
                tnFind = tnArray[0];
                if (tnFind != null)
                {
                    m_tvCatalog.SelectedNode = tnFind;
                    m_tvCatalog.Focus();
                }
            }
        }
        #endregion

        #region 设置数量
        /// <summary>
        /// 设置数量
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="count"></param>
        void SetAppCount(string nodeName,int count)
        {
            TreeNode[] tnArray = m_tvCatalog.Nodes.Find(nodeName, false);
            if (tnArray.Length > 0)
            {
                TreeNode tnFind = null;
                tnFind = tnArray[0];
                if (tnFind != null)
                {
                    tnFind.Text = tnFind.Name + "(" + count + ")";
                }
            }
        }
        #endregion
    }
}
