﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Web;
using System.Collections.Specialized;
using System.IO;
using System.Threading;
using MyDownloader.Core;

namespace iSprite
{
    internal partial class OnlineAppList : UserControl
    {
        #region 变量定义
        AppHelper m_appHelper;
        string m_SearchKey = string.Empty;
        internal event MessageHandler OnMessage;
        Dictionary<string, PackageItem> m_Packagelist;
        public event AddURL2DownloadHandler OnDownloadApp;
        public event SetNodeCountHandler OnSetNodeCount;
        #endregion

        #region 添加到下载队列
        /// <summary>
        /// 添加到下载队列
        /// </summary>
        /// <param name="url"></param>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        void AddURL2Download(string url, string path, string fileName, InstallState state)
        {
            if (null != OnDownloadApp)
            {
                OnDownloadApp(url, path, fileName, state);
            }
        }
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
        public OnlineAppList(iPhoneFileDevice iphoneDevice, AppHelper appHelper)
        {
            m_appHelper = appHelper;

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
            appPager.OnPageChanged += new PageChangedEventHandler(Pager_OnPageChanged);
            appwb.Navigating += new WebBrowserNavigatingEventHandler(Appwb_Navigating);

            txtKey.KeyDown += new KeyEventHandler
                (
                    delegate(object sender, KeyEventArgs e)
                    {
                        if (e.KeyCode == Keys.Enter)
                        {
                            Search();
                        }
                    }
                );

            txtKey.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtKey.AutoCompleteSource = AutoCompleteSource.CustomSource;

            btnGo.Click += new EventHandler
                (
                    delegate(object sender, EventArgs e)
                    {
                        Search();
                    }
                );
        }
        #endregion

        #region 搜索
        /// <summary>
        /// 搜索
        /// </summary>
        public void Search()
        {
            string key = txtKey.Text.Trim();
            string catalogName = string.Empty;
            if (null != chbCatalog.SelectedItem)
            {
                catalogName = chbCatalog.SelectedItem.ToString();
            }
            else
            {
                catalogName = chbCatalog.Text;
            }

            m_SearchKey = key;
            if (string.IsNullOrEmpty(m_SearchKey))
            {
                GetPackagesByCatalog(catalogName);
            }
            else
            {
                m_Packagelist = m_appHelper.SearchPackages(m_appHelper.Packagelist, catalogName, m_SearchKey);
                BindAppData();
            }
            SetNodeCount("Search Result", m_Packagelist.Count,true);
        }
        #endregion

        #region 导航监控
        /// <summary>
        /// 导航监控
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    string PID = nv["pid"];

                    AddApp2DownloadList(url, fileName, PID);
                }
                catch
                {
                }
            }
            else if (url.Contains("goto:"))
            {
                e.Cancel = true;
                try
                {
                    string nodeName=url.Substring(5);
                    GetPackagesByCatalog(nodeName);
                    SetNodeCount(nodeName, m_Packagelist.Count, true);
                    this.appwb.Focus();
                }
                catch
                {
                }
            }
        }
        #endregion

        #region 添加软件到下载列表
        /// <summary>
        /// 添加软件到下载列表
        /// </summary>
        /// <param name="url"></param>
        /// <param name="fileName"></param>
        /// <param name="PID"></param>
        void AddApp2DownloadList(string url, string mainFileName, string PID)
        {
            RaiseMessageHandler(this, "Prepare to download app...", MessageTypeOption.SetStatusBar);
            Application.DoEvents();
            List<PackageItem> deplist = null;
            PackageItem debitem;

            bool toContinue = true;
            if (m_appHelper.Packagelist.TryGetValue(PID, out debitem))
            {
                List<string> errList;
                if (m_appHelper.CheckConflict2Installed(debitem, out errList))
                {
                    //存在冲突软件
                    MessageHelper.ShowError("The app you select conflict the installed app(" + Utility.List2String(errList) + ").");
                    toContinue = false;
                }
                //检查依赖软件
                deplist = m_appHelper.GetDependsDeb(debitem, out errList);
                if (errList.Count > 0)
                {
                    MessageHelper.ShowError("This app cannot be downloaded completely, because some of its dependencies(" + Utility.List2String(errList) + ") cannot be found.\r\nYou may have a deb source is too old, please update your deb source!");
                    toContinue = false;
                }
            }
            if (toContinue)
            {
                if (string.IsNullOrEmpty(mainFileName))
                {
                    mainFileName = Path.GetRandomFileName() + ".deb";
                }

                //添加主文件到下载队列
                mainFileName = mainFileName.Replace("+", "");//+要用来信息分隔符，所以先去掉
                mainFileName = Utility.GetNotExistFileName(mainFileName);

                List<DownLoadItemInfo> todownlist = new List<DownLoadItemInfo>();

                DownLoadItemInfo downitem = new DownLoadItemInfo();
                downitem.Url = url;
                downitem.SaveDir = m_appHelper.AptDownloadFolder;
                downitem.FileName = mainFileName;
                downitem.State = InstallState.NeedInstall;
                todownlist.Add(downitem);

                int dependCount = 0;
                if (deplist != null)
                {
                    dependCount = deplist.Count;
                    //添加依赖文件到下载队列
                    RepositoryInfo repInfo;
                    foreach (PackageItem item in deplist)
                    {
                        if (m_appHelper.RepositoryList.TryGetValue(item.AdditionalInfoURL, out repInfo))
                        {
                            string depFileName = Utility.GetNotExistFileName(
                                Path.GetFileNameWithoutExtension(mainFileName) + "+" + Path.GetFileName(item.DownloadURL).Replace("+", "")
                                );

                            downitem = new DownLoadItemInfo();
                            downitem.Url = item.FinalDownloadURL(repInfo.APTDownloadBaseURL);
                            downitem.SaveDir = m_appHelper.AptDownloadFolder;
                            downitem.FileName = depFileName;
                            downitem.State = InstallState.DependInstall;
                            todownlist.Add(downitem);

                            Application.DoEvents();
                        }
                    }
                }

                foreach (DownLoadItemInfo item in todownlist)
                {
                    File.WriteAllText(Path.ChangeExtension(item.SaveDir + item.FileName, ".d"), string.Empty);//表示文件下载未完成
                }

                foreach (DownLoadItemInfo item in todownlist)
                {
                    AddURL2Download(item.Url, item.SaveDir, item.FileName, item.State);
                }

                if (dependCount > 0)
                {
                    RaiseMessageHandler(this, "Success to add 1 main app and " + dependCount + " depend app to downlist.iSpirit will auto install app after finish download.", MessageTypeOption.HiddenStatusBar);
                }
                else
                {
                    RaiseMessageHandler(this, "Success to add 1 deb app to downlist.iSpirit will auto install app after finish download.", MessageTypeOption.HiddenStatusBar);
                }
            }
            else
            {
                RaiseMessageHandler(this, string.Empty, MessageTypeOption.HiddenStatusBar);
            }
            Application.DoEvents();
        }
        #endregion

        #region 软件列表分页事件处理
        /// <summary>
        /// 软件列表分页事件处理
        /// </summary>
        /// <param name="e"></param>
        void Pager_OnPageChanged(PageChangedEventArgs e)
        {
            appPager.CurrentPageIndex = e.NewPageIndex;
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
            appPager.RecordCount = packages.Count;
            appPager.DataBind();
            appwb.Navigate(
                m_appHelper.Packages2Html(packages,
                appPager.PageSize, 
                appPager.CurrentPageIndex,
                m_SearchKey
                )
                );
            this.appwb.Focus();
        }
        #endregion

        #region 获取指定分类的软件列表
        /// <summary>
        /// 获取指定分类的软件列表
        /// </summary>
        /// <param name="name"></param>
        public void GetPackagesByCatalog(string name)
        {
            m_SearchKey = string.Empty;
            if (string.IsNullOrEmpty(name) || name == "All Packages")
            {
                m_Packagelist = m_appHelper.Packagelist;
            }
            else
            {
                m_Packagelist = m_appHelper.GetPackagesByCatalog(m_appHelper.Packagelist, name);
            }
            BindAppData();
        }
        #endregion                 
        
        #region 加载类别
        /// <summary>
        /// 加载类别
        /// </summary>
        /// <param name="catalist"></param>
        /// <param name="appNames"></param>
        public void LoadCatalogData(List<string> catalist, List<string> appNames)
        {
            chbCatalog.Items.Clear();
            chbCatalog.Items.Add("All Packages");

            foreach (string name in catalist)
            {
                this.chbCatalog.Items.Add(name);
                Application.DoEvents();
            }
            chbCatalog.SelectedText = "All Packages";

            txtKey.AutoCompleteCustomSource.AddRange(appNames.ToArray());  //搜索提示  
        }
        #endregion 
    }

}
