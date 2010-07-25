﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Data;
using iSprite.ThirdControl.FarsiLibrary;
using CE.iPhone.PList;
using System.Text.RegularExpressions;
using Manzana;
using System.Web;
using System.Threading;

namespace iSprite
{
    internal class iThemeBrowser
    {
        #region 变量定义
        iPhoneFileDevice m_iPhoneDevice;
        internal event PathChanged OnPathChanged;
        internal event MessageHandler OnMessage;
        Dictionary<string, string> m_DefaultIconDic;
        FATabStripItem m_tabTheme;
        const string DefaultLanage = "en";
        List<string> m_SystemSettingName;
        ToolStrip m_Toolmenu;
        ThemePriview m_ThemePriview;
        WebBrowser themeBrowser;
        FlowLayoutPanel  m_themesPanel;
        internal event FileProgressHandler OnProgressHandler;
        ToolStripItem tsbtn_Back, tsbtn_Forward;

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

        internal void SetPreviewVisable(bool enable)
        {
            if (enable)
            {
                m_ThemePriview.BringToFront();
            }
            else
            {
                m_ThemePriview.SendToBack();
            }
        }

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="filedevice"></param>
        /// <param name="ctxFavourites"></param>
        internal iThemeBrowser(iPhoneFileDevice filedevice, FATabStripItem tabTheme,Form parentForm)
        {
            m_iPhoneDevice = filedevice;
            this.m_tabTheme = tabTheme;
            m_DefaultIconDic = new Dictionary<string, string>();
            m_SystemSettingName = new List<string>();
            m_SystemSettingName.Add("User Lock Background");//你的墙纸在锁屏状态下也可见
            m_SystemSettingName.Add("No Undocked Icon Labels");
            m_SystemSettingName.Add("User Lock Background");
            m_SystemSettingName.Add("Dim Icons");
            m_SystemSettingName.Add("Black Navigation Bars");//黑色导航条
            m_SystemSettingName.Add("Solid Status Bar");//将状态栏始终保持在白色的状态
            InitThemeTab();

            InitialiseThemePriview(parentForm);

            tabTheme.Resize += new EventHandler(tabTheme_Resize);
        }

        void tabTheme_Resize(object sender, EventArgs e)
        {
            int height = m_tabTheme.Size.Height;
            if (height < 652)
            {
                height = 652;
            }
            this.themeBrowser.Size = new Size(m_tabTheme.Size.Width - 1, height);
            m_themesPanel.Size = new Size(m_tabTheme.Size.Width - 1, height);
        }

        private iThemeBrowser()
        { 
        }

        /// <summary>
        /// 加载ThemePriview
        /// </summary>
        void InitialiseThemePriview(Form parentForm)
        {
            m_ThemePriview = new ThemePriview();
            parentForm.Controls.Add(m_ThemePriview);
            m_ThemePriview.BringToFront();
            m_ThemePriview.Left = (parentForm.Width - m_ThemePriview.Width) / 2;
            m_ThemePriview.Top = (parentForm.Height - m_ThemePriview.Height) / 2;
            m_ThemePriview.OnMessage += new ThemePriviewMessageHandler(ThemePriview_OnMessage);
            m_ThemePriview.Height = 570;
        }

        void InitThemeTab()
        {
            m_Toolmenu = new ToolStrip();
            this.m_tabTheme.Controls.Add(m_Toolmenu);
            m_Toolmenu.Items.Add(CreateMenuItem("tsbtn_InstallFromZIP", "Install From ZIP", global::iSprite.Resource.zip_32));
            m_Toolmenu.Items.Add(CreateMenuItem("tsbtn_InstallFromFolder", "Install From Folder", global::iSprite.Resource.Folder_16));

            #region tsbtn_ThemesIniPhone
            ToolStripSplitButton tsbtn_ThemesIniPhone = new ToolStripSplitButton();
            tsbtn_ThemesIniPhone.ImageTransparentColor = System.Drawing.Color.Magenta;
            tsbtn_ThemesIniPhone.Name = "tsbtn_InstallFromFolder";
            tsbtn_ThemesIniPhone.Size = new System.Drawing.Size(32, 100);
            tsbtn_ThemesIniPhone.ToolTipText = "Themes In " + iSpriteContext.Current.AppleDeviceType;
            tsbtn_ThemesIniPhone.Text = "Themes In " + iSpriteContext.Current.AppleDeviceType;
            tsbtn_ThemesIniPhone.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tsbtn_ThemesIniPhone.Image = global::iSprite.Resource.winterboard;

            tsbtn_ThemesIniPhone.Click += new EventHandler
                (
                    delegate(object sender, EventArgs e)
                    {
                        if (!tsbtn_ThemesIniPhone.DropDown.Visible)
                        {
                            ShowThemesIniPhone();
                            tsbtn_ThemesIniPhone.DropDown.Visible = true;
                        }
                    }
                );

            m_Toolmenu.Items.Add(tsbtn_ThemesIniPhone);

            ContextMenuStrip ctxThemesIniPhone = new ContextMenuStrip();
            ctxThemesIniPhone.OwnerItem = tsbtn_ThemesIniPhone;
            ctxThemesIniPhone.Size = new System.Drawing.Size(153, 26);
            tsbtn_ThemesIniPhone.DropDown = ctxThemesIniPhone;

            ToolStripMenuItem ctxitem = new ToolStripMenuItem("Apply Selected Theme");
            ctxitem.Click += new EventHandler(ThemeManItem_Click);
            ctxThemesIniPhone.Items.Add(ctxitem);

            ctxitem = new ToolStripMenuItem("Delete Selected Themes");
            ctxitem.Click += new EventHandler(ThemeManItem_Click);
            ctxThemesIniPhone.Items.Add(ctxitem);
            ctxitem = new ToolStripMenuItem("Goto Online Themes");
            ctxitem.Click += new EventHandler(ThemeManItem_Click);
            ctxThemesIniPhone.Items.Add(ctxitem);
            #endregion

            #region themeBrowser
            themeBrowser = new WebBrowser();
            this.m_tabTheme.Controls.Add(themeBrowser);

            themeBrowser.Location = new System.Drawing.Point(m_Toolmenu.Location.X, m_Toolmenu.Location.Y + m_Toolmenu.Height);
            //themeBrowser.Anchor = AnchorStyles.Bottom;
            themeBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            themeBrowser.Name = "themeBrowser";
            themeBrowser.Size = new System.Drawing.Size(990, 652);
            themeBrowser.TabIndex = 0;
            themeBrowser.Url = new Uri(iSpriteContext.Current.ThemeHomePage, System.UriKind.Absolute);
            themeBrowser.Navigating += new WebBrowserNavigatingEventHandler(themeBrowser_Navigating);
            themeBrowser.CanGoForwardChanged += new EventHandler(themeBrowser_CanGoForwardChanged);
            themeBrowser.CanGoBackChanged += new EventHandler(themeBrowser_CanGoBackChanged);
            #endregion

            #region m_themesPanel
            m_themesPanel = new FlowLayoutPanel();
            m_themesPanel.BackColor = Color.White;
            m_themesPanel.Size = new Size(990, 652);            
            this.m_tabTheme.Controls.Add(m_themesPanel);
            m_themesPanel.SendToBack();
            m_themesPanel.FlowDirection = FlowDirection.LeftToRight;
            m_themesPanel.AutoScroll = true;
            m_themesPanel.Location = new Point(m_Toolmenu.Location.X, m_Toolmenu.Location.Y + m_Toolmenu.Height);
            #endregion

            tsbtn_Back = CreateMenuItem("tsbtn_Back", "Go Back", global::iSprite.Resource.go_backward);
            tsbtn_Forward = CreateMenuItem("tsbtn_Forward", "Go Forward", global::iSprite.Resource.go_forward);
            m_Toolmenu.Items.Add(tsbtn_Back);
            m_Toolmenu.Items.Add(tsbtn_Forward);

            tsbtn_Back.Enabled = false;
            tsbtn_Forward.Enabled = false;

            //事件
            foreach (ToolStripItem item in m_Toolmenu.Items)
            {
                if (item is ToolStripButton)
                {
                    item.Click += new EventHandler(toolmenu_Click);
                    item.Enabled = false;
                }
            }
        }

        void themeBrowser_CanGoBackChanged(object sender, EventArgs e)
        {
            tsbtn_Back.Enabled = themeBrowser.CanGoBack;
        }

        void themeBrowser_CanGoForwardChanged(object sender, EventArgs e)
        {
            tsbtn_Forward.Enabled = themeBrowser.CanGoForward;
        }

        #endregion

        #region iPhone上的主题管理
        /// <summary>
        /// iPhone上的主题管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ThemeManItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            ThemePreviewItem p;
            ThemeInfo themeInfo;
            switch (item.Text)
            {
                case "Apply Selected Theme":
                    foreach (Control c in m_themesPanel.Controls)
                    {
                        if (c is ThemePreviewItem)
                        {
                            p = (ThemePreviewItem)c;
                            if (p.IsSelected)
                            {
                                themeInfo = (ThemeInfo)p.Tag;
                                m_ThemePriview.ShowPreview(themeInfo);
                                break;
                            }
                        }
                    }
                    break;
                case "Delete Selected Themes":
                    List<string> list = new List<string>();

                    foreach (Control c in m_themesPanel.Controls)
                    {
                        if (c is ThemePreviewItem)
                        {
                            p = (ThemePreviewItem)c;
                            if (p.IsSelected)
                            {
                                themeInfo = (ThemeInfo)p.Tag;
                                list.Add(themeInfo.Name);
                            }
                        }
                    }

                    if (list.Count > 0)
                    {
                        DeleteThemes(list);
                    }

                    break;
                case "Goto Online Themes":
                    themeBrowser.BringToFront();
                    m_themesPanel.SendToBack();
                    break;
            }
        }
        #endregion

        #region 浏览器导航事件
        /// <summary>
        /// 浏览器导航事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void themeBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            string url = e.Url.ToString();
            if (url.Contains("/Service/Download/"))
            {
                e.Cancel = true;

                try
                {
                    //判断是否安装WinterBoard
                    if (!CheckInstallWinterBoard())
                    {
                        return;
                    }

                    string themeName = url.Substring(url.LastIndexOf(",") + 1);
                    themeName = HttpUtility.UrlDecode(themeName, Encoding.UTF8);

                    string filepath = iSpriteContext.Current.iSpriteTempPath + "/" + Path.GetFileNameWithoutExtension(themeName) + ".zip";
                    string previewImg = iSpriteContext.Current.iSpriteTempPath + "/" + Path.GetFileNameWithoutExtension(themeName) + ".jpg";

                    if (Utility.DownloadFile(url, filepath, OnProgressHandler))
                    {
                        InstallFromZIP(filepath, url);
                    }
                    else
                    {
                        RaiseMessageHandler(this, "Fail to download the theme files, try again please .", MessageTypeOption.Error);
                    }
                }
                catch
                { 
                }

            }
        }
        #endregion

        #region 按钮事件处理
        /// <summary>
        /// 按钮事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void toolmenu_Click(object sender, EventArgs e)
        {
            ToolStripItem item = (ToolStripItem)sender;
            m_themesPanel.SendToBack();
            switch (item.Name)
            {
                case "tsbtn_InstallFromZIP":
                    themeBrowser.BringToFront();
                    InstallFromZIP();
                    break;

                case "tsbtn_InstallFromFolder":
                    InstallFromFolder();
                    break;

                case "tsbtn_Back":
                    themeBrowser.GoBack();
                    break;

                case "tsbtn_Forward":
                    themeBrowser.GoForward();
                    break;
            }
        }
        #endregion

        #region 获取iPhone上的主题列表
        /// <summary>
        /// 获取iPhone上的主题列表
        /// </summary>
        void ShowThemesIniPhone()
        {
            m_themesPanel.BringToFront();
            string wallpaperpath = string.Empty;
            string localpath = string.Empty;
            string themeiconpath = string.Empty;
            string localthemepcaketpath = string.Empty;
            Image imgTheme;
            ThemePreviewItem p;
            string themeName = string.Empty;

            RaiseMessageHandler(this, "Begin to Load Themes", MessageTypeOption.SetStatusBar);

            Application.DoEvents();

            foreach (string dir in m_iPhoneDevice.GetDirectories(iSpriteContext.Current.iPhone_WinterBoardFile_Path))
            {
                if (!dir.EndsWith(".theme") && !m_themesPanel.Controls.ContainsKey(dir))
                {
                    wallpaperpath = dir + "/Wallpaper.png";
                    themeiconpath = dir + "/Icons/";
                    if (m_iPhoneDevice.FileExists(wallpaperpath) && m_iPhoneDevice.DirectoryExists(themeiconpath))
                    {
                        localthemepcaketpath = iSpriteContext.Current.iSpriteTempPath + Path.GetRandomFileName().Replace(".","");
                        if (m_iPhoneDevice.Downlod2PC(dir, localthemepcaketpath, false))
                        {
                            Application.DoEvents();
                            imgTheme = m_ThemePriview.ShowPreview(localthemepcaketpath);
                            themeName = new DirectoryInfo(dir).Name;
                            p = new ThemePreviewItem(imgTheme, themeName);
                            p.Click += new EventHandler(ThemePreviewItem_Click);
                            ThemeInfo themeInfo = new ThemeInfo();
                            themeInfo.Name=(themeName);
                            themeInfo.LocalPath=(localthemepcaketpath);
                            themeInfo.IsExistsIniPhone = true;
                            p.Tag = themeInfo;
                            p.Name = dir;
                            m_themesPanel.Controls.Add(p);
                            Application.DoEvents();
                        }
                    }
                }
            }
            RaiseMessageHandler(this, "", MessageTypeOption.HiddenStatusBar);
        }
        #endregion

        #region iPhone上的主题预览
        /// <summary>
        /// iPhone上的主题预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ThemePreviewItem_Click(object sender, EventArgs e)
        {
            m_ThemePriview.ShowPreview((ThemeInfo)((ThemePreviewItem)sender).Tag);
        }
        #endregion

        #region 从ZIP包安装主题
        /// <summary>
        /// 从ZIP包安装主题
        /// </summary>
        void InstallFromZIP()
        {
            //判断是否安装WinterBoard
            if (!CheckInstallWinterBoard())
            {
                return;
            }
            string themepath = string.Empty;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "All Files(*.zip)|*.zip";
            dialog.Multiselect = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                InstallFromZIP(dialog.FileName);
            }
        }
        void InstallFromZIP(string zippath)
        {
            InstallFromZIP(zippath,string.Empty);
        }
        void InstallFromZIP(string zippath,string url)
        {
            string tozippath = iSpriteContext.Current.iSpriteTempPath + "\\" + Path.GetRandomFileName() + "\\";


            if (ZipHelper.UnZip(zippath, tozippath) > 0)
            {
                string themepath = SelectRightThemePath(tozippath);

                if (themepath != string.Empty)
                {
                    if (!Directory.Exists(themepath + "\\Icons\\")
                        || !File.Exists(themepath + "\\Wallpaper.png"))
                    {
                        Utility.DownloadFile(url.Replace("/Download/", "/DownloadPic/"), themepath + "\\previewImg.jpg", OnProgressHandler);
                    }

                    InstallFromFolder(
                        Utility.GetDirName(themepath),
                        themepath
                        );
                }
                else
                {
                    RaiseMessageHandler(this, "Invalid theme packet, Please check it!", MessageTypeOption.Error);
                    return;
                }
            }
        }
        #endregion

        #region 从文件夹安装主题
        /// <summary>
        /// 从文件夹安装主题
        /// </summary>
        void InstallFromFolder()
        {
            //判断是否安装WinterBoard
            if (!CheckInstallWinterBoard())
            {
                return;
            }

            string themepath = string.Empty;
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                themepath = fbd.SelectedPath;
                string themeName = Utility.GetDirName(themepath);
                InstallFromFolder(themeName,themepath);
            }
        }

        /// <summary>
        /// 从文件夹安装主题
        /// </summary>
        /// <param name="themepath"></param>
        void InstallFromFolder(string themeName,string themepath)
        {
            if (m_iPhoneDevice.IsConnected)
            {
                PreSetTheme(themeName, themepath);
            }
            else
            {
                RaiseMessageHandler(this, "Fail to operate ! #AppleDeviceType# is disconnected .", MessageTypeOption.Error);
            }
        }

        #region 检查主题是否合法
        /// <summary>
        /// 检查主题是否合法
        /// </summary>
        /// <param name="themepath"></param>
        /// <returns></returns>
        bool CheckThemePacket(string themePath)
        {
            int iconnum = 0;
            foreach (KeyValuePair<string, string> item in m_DefaultIconDic)
            {
                if (item.Key == "com.apple.MobileSMS")
                {
                    if (!File.Exists(themePath + "/Icons/" + item.Value + ".png")
                        && File.Exists(themePath + "/Icons/Text.png")
                        )
                    {
                        File.Copy(themePath + "/Icons/Text.png", themePath + "/Icons/" + item.Value + ".png");
                        iconnum++;
                    }
                }
                if (File.Exists(themePath + "/Icons/" + item.Value + ".png"))
                {
                    iconnum++;
                }
            }

            return true;
            //return iconnum > 0;
        }
        #endregion

        #endregion

        #region 设置主题
        void PreSetTheme(string themeName, string themePath)
        {
            if (CheckSameThemeName(themeName))
            {
                if (MessageHelper.ShowConfirm(
                    string.Format("The theme \"" + themeName + "\"  is already exist, would you want to recover it?", themeName))
                    != DialogResult.OK
                    )
                {
                    return;//取消
                }
            }
            if (!CheckThemePacket(themePath))
            {
                RaiseMessageHandler(this, "Invalid theme packet, Please check it!", MessageTypeOption.Error);
                return;
            }

            if (!RenameThemeIconByLocalized(themePath))
            {
                RaiseMessageHandler(this, "Can't convert theme for your current lang !", MessageTypeOption.Error);
                return;
            }
            ThemeInfo themeInfo = new ThemeInfo();
            themeInfo.Name=(themeName);
            themeInfo.LocalPath=(themePath);
            m_ThemePriview.ShowPreview(themeInfo);
            //SetTheme(themeInfo[0], themeInfo[1]);
        }

        void ThemePriview_OnMessage(ThemeInfo themeInfo, ThemePriviewMessageTypeOption messagetype)
        {
            if (messagetype != ThemePriviewMessageTypeOption.Cancel)
            {
                SetTheme(themeInfo, messagetype);
            }
        }
        /// <summary>
        /// 设置主题
        /// </summary>
        /// <param name="themeInfo"></param>
        void SetTheme(ThemeInfo themeInfo, ThemePriviewMessageTypeOption messagetype)
        {
            string themeName = themeInfo.Name;
            string themePath = themeInfo.LocalPath;

            if (!themeInfo.IsExistsIniPhone)//如果主题已经在iPhone上面存在就无需再次上传
            {
                if (!m_iPhoneDevice.Copy2iPhone(themePath, iSpriteContext.Current.iPhone_WinterBoardFile_Path + "/" + themeName))
                {
                    RaiseMessageHandler(this, "Fail to copy the theme file to #AppleDeviceType# .", MessageTypeOption.Error);
                    return;
                }
            }

            if (messagetype == ThemePriviewMessageTypeOption.Upload)
            {
                //如果只是上传就不需要拷贝plist文件到iPhone
                RaiseMessageHandler(this, "The theme \"" + themeName + "\" has been successfully copy to #AppleDeviceType# .", MessageTypeOption.Info);
                return;
            }


            string iPhoneThemeSetting = iSpriteContext.Current.iPhone_WinterBoardSetting_Path;
            string localThemeSetting = iSpriteContext.Current.iSpriteTempPath + Path.GetFileName(iPhoneThemeSetting);

            string content = m_iPhoneDevice.GetFileText(iPhoneThemeSetting);

            if (!string.IsNullOrEmpty(content))
            {
                #region 处理主题配置文件
                Match match = new Regex("<dict>[\\s]+<key>Active</key>[\\s]+<(?<ActiveValue>[\\S]*?) />[\\s]+<key>Name</key>[\\s]+<[u]*string>(?<NameValue>[\\S ]*?)</[u]*string>[\\s]+</dict>",
                RegexOptions.IgnoreCase | RegexOptions.Compiled).Match(content);

                while (match.Success)
                {
                    string name = match.Result("${NameValue}");
                    if (!m_SystemSettingName.Contains(name))
                    {
                        if (name == themeName)
                        {
                            content = content.Replace(match.Value, "");
                        }
                        else
                        {
                            if (match.Result("${ActiveValue}") == "true")
                            {
                                content = content.Replace(match.Value, match.Value.Replace("<true />", "<false />"));
                            }
                        }
                    }
                    match = match.NextMatch();
                }

                //将当前主题排在第一位
                string trueTheme = string.Format("<dict>\n\t\t\t<key>Active</key>\n\t\t\t<true />\n\t\t\t<key>Name</key>\n\t\t\t<string>{0}</string>\n\t\t</dict>\n", themeName);
                content = content.Insert(content.IndexOf("<array>\n\t\t") + "<array>\n\t\t".Length, trueTheme);

                using (StreamWriter writer = new StreamWriter(localThemeSetting, false, Encoding.UTF8))
                {
                    writer.Write(content);
                    writer.Flush();
                    writer.Close();
                }

                PListRoot root = PListRoot.Load(localThemeSetting);
                root.Save(localThemeSetting, PListFormat.Binary);//转换成二进制
                #endregion

                //拷贝配置文件
                if (m_iPhoneDevice.Copy2iPhone(localThemeSetting, iPhoneThemeSetting))
                {
                    m_iPhoneDevice.Respring();
                    RaiseMessageHandler(this, "The theme \"" + themeName + "\" has been successfully installed .", MessageTypeOption.Info);
                }
                else
                {
                    RaiseMessageHandler(this, "Fail to copy the theme setting file to #AppleDeviceType# .", MessageTypeOption.Error);
                }
            }
            else
            {
                RaiseMessageHandler(this, "Fail to copy the theme setting file to pc .", MessageTypeOption.Error);
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
                m_DefaultIconDic = new Dictionary<string, string>();
                m_DefaultIconDic = GetLocalizedApplicationNamesByLan(DefaultLanage);

                themeBrowser.Url = new Uri(iSpriteContext.Current.ThemeHomePage, System.UriKind.Absolute);
            }
            foreach (ToolStripItem item in m_Toolmenu.Items)
            {
                if (item is ToolStripButton)
                {
                    item.Enabled = isContected;
                }
            }

            tsbtn_Back.Enabled = false;
            tsbtn_Forward.Enabled = false;
        }
        #endregion

        #region 重命名图标（根据用户语言）
        /// <summary>
        /// 重命名图标（根据用户语言）
        /// </summary>
        /// <param name="themePath"></param>
        bool RenameThemeIconByLocalized(string themePath)
        {            
            try
            {
                if (m_iPhoneDevice.CurrentLang != DefaultLanage)
                {
                    Dictionary<string, string> currenticondic = GetLocalizedApplicationNamesByLan(m_iPhoneDevice.CurrentLang);

                    foreach (KeyValuePair<string, string> item in currenticondic)
                    {
                        if (m_DefaultIconDic.ContainsKey(item.Key) && m_DefaultIconDic[item.Key] != item.Value)
                        {
                            if (File.Exists(themePath + "/Icons/" + item.Value + ".png"))
                            {
                                File.Move(themePath + "/Icons/" + item.Value + ".png", themePath + "/Icons/" + m_DefaultIconDic[item.Key] + ".png");
                            }
                        }
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 判断是否存在同名主题
        /// <summary>
        /// 判断是否存在同名主题
        /// </summary>
        /// <returns></returns>
        bool CheckSameThemeName(string themeName)
        {
            return m_iPhoneDevice.DirectoryExists(iSpriteContext.Current.iPhone_WinterBoardFile_Path + "/" + themeName+"/");
        }
        #endregion

        #region 判断是否安装WinterBoard
        /// <summary>
        /// 判断是否安装WinterBoard
        /// </summary>
        /// <returns></returns>
        bool CheckInstallWinterBoard()
        {
            if (!m_iPhoneDevice.CheckJailbreak())
            {
                return false;
            }

            if (!m_iPhoneDevice.DirectoryExists(iSpriteContext.Current.iPhone_WinterBoardApp_Path))
            {
                if (MessageHelper.ShowConfirm("WinterBoard has not been installed, Would you want to install?") == DialogResult.OK)
                {
                    //安装WinterBoard
                    InstallWinterBoard();
                }
                return false;
            }
            else
            {
                //判断图标有没有显示出来
                string content = m_iPhoneDevice.GetFileText(iSpriteContext.Current.iPhone_InstallationPath);
                if (!content.Contains("<key>com.saurik.WinterBoard</key>"))
                {
                    //如果没有显示，需要重启Springboard
                    if (MessageHelper.ShowInfo("WinterBoard has been installed, ispirit will reboot #AppleDeviceType# SpringBoard to show WinterBoard in  home screen.") 
                        == DialogResult.OK)
                    {
                        //重启Respring
                        m_iPhoneDevice.Respring();
                    }
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        #endregion

        #region 选择正确的主题包路径
        /// <summary>
        /// 选择正确的主题包路径
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        string SelectRightThemePath(string dir)
        {
            string[] subfiles = Directory.GetFiles(dir);
            foreach (string file in subfiles)
            {
                if (file.EndsWith("\\LockBackground.png", true, null))
                {
                    return dir;
                }
            }

            string[] subdirs = Directory.GetDirectories(dir);
            if (subdirs.Length == 1)
            {
                if (subdirs[0].EndsWith("\\Icons", true, null))
                {
                    return dir;
                }
                else
                {
                    return SelectRightThemePath(subdirs[0]);
                }
            }
            else
            {
                foreach (string subd in subdirs)
                {
                    if (subd.EndsWith("\\Icons", true, null))
                    {
                        return dir;
                    }
                }
            }
            return string.Empty;
        }
        #endregion

        #region 安装WinterBoard
        /// <summary>
        /// 安装WinterBoard
        /// </summary>
        /// <returns></returns>
        bool InstallWinterBoard()
        {
            return MyInstaller.Show(m_iPhoneDevice, InstallAppOption.Winterboard, OnProgressHandler) == DialogResult.OK;
        }
        #endregion        

        #region 获取在不同语言下系统应用程序名称
        /// <summary>
        /// 获取在不同语言下系统应用程序名称
        /// </summary>
        /// <param name="lanage"></param>
        /// <returns></returns>
        Dictionary<string, string> GetLocalizedApplicationNamesByLan(string lanage)
        {
            string localizedPath = "English.lproj";
            switch (lanage.ToLower())
            {
                case "en":
                    localizedPath = "English.lproj";
                    break;
                case "zh-hans":
                    localizedPath = "zh_CN.lproj";
                    break;
                case "zh-hant":
                    localizedPath = "zh_TW.lproj";
                    break;
                case "ja":
                    localizedPath = "Japanese.lproj";
                    break;
                case "fr":
                    localizedPath = "French.lproj";
                    break;
                case "it":
                    localizedPath = "Italian.lproj";
                    break;
                case "de":
                    localizedPath = "German.lproj";
                    break;
                case "es":
                    localizedPath = "Spanish.lproj";
                    break;
                case "nl":
                    localizedPath = "Dutch.lproj";
                    break;
                case "pt-pt":
                    localizedPath = "pt_PT.lproj";
                    break;
                case "da":
                    localizedPath = "da.lproj";
                    break;
                case "fi":
                    localizedPath = "fi.lproj";
                    break;
                case "sv":
                    localizedPath = "sv.lproj";
                    break;
                case "ko":
                    localizedPath = "ko.lproj";
                    break;
                case "ru":
                    localizedPath = "ru.lproj";
                    break;
                case "pl":
                    localizedPath = "pl.lproj";
                    break;
                default:
                    localizedPath = "English.lproj";
                    break;
            }
            return GetLocalizedApplicationNames(localizedPath);
        }
        /// <summary>
        /// 获取在不同语言下系统应用程序名称
        /// </summary>
        /// <param name="localizedPath"></param>
        /// <returns></returns>
        Dictionary<string, string> GetLocalizedApplicationNames(string localizedPath)
        {
            Dictionary<string, string>  icondic = new Dictionary<string, string>();
            string iphonecfgpath = string.Format(iSpriteContext.Current.iPhone_LocalizedApplicationNames_Path, localizedPath);

            string content = m_iPhoneDevice.GetFileText(iphonecfgpath);
            Match match = new Regex("<key>(?<Appkey>[\\S]*?)</key>(?<B>[\\s]*?)<string>(?<AppValue>[\\S]*?)</string>", 
                RegexOptions.IgnoreCase | RegexOptions.Compiled).Match(content);
            while (match.Success)
            {
                icondic.Add(match.Result("${Appkey}"), match.Result("${AppValue}"));
                match = match.NextMatch();
            }

            match = new Regex("<key>(?<Appkey>[\\S]*?)</key>(?<B>[\\s]*?)<ustring>(?<AppValue>[\\S]*?)</ustring>",
                RegexOptions.IgnoreCase | RegexOptions.Compiled).Match(content);
            while (match.Success)
            {
                icondic.Add(match.Result("${Appkey}"), match.Result("${AppValue}"));
                match = match.NextMatch();
            }

            return icondic;
        }
        #endregion

        #region 创建新按钮
        /// <summary>
        /// 创建新按钮
        /// </summary>
        /// <param name="name"></param>
        /// <param name="text"></param>
        /// <param name="img"></param>
        /// <returns></returns>
        ToolStripButton CreateMenuItem(string name, string text, Image img)
        {
            ToolStripButton button = new ToolStripButton();

            button.ImageTransparentColor = System.Drawing.Color.Magenta;
            button.Name = name;
            button.Size = new System.Drawing.Size(32, 100);
            button.ToolTipText = text;
            button.Text = text;
            button.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            if (null != img)
            {
                button.Image = img;
            }

            return button;
        }
        #endregion

        #region 删除主题
        /// <summary>
        /// 删除主题
        /// </summary>
        /// <param name="list"></param>
        void DeleteThemes(List<string> list)
        {
            if (list.Count > 0 &&
                MessageHelper.ShowConfirm("Are you sure you want to delete the selected themes ? ") == DialogResult.OK)
            {
                RaiseMessageHandler(this, "Begin to Delete Themes", MessageTypeOption.SetStatusBar);
                foreach (string themeName in list)
                {
                    string dir = iSpriteContext.Current.iPhone_WinterBoardFile_Path + themeName + "/";
                    m_iPhoneDevice.DeleteDirectory(dir, true);
                    if (m_themesPanel.Controls.ContainsKey(dir))
                    {
                        m_themesPanel.Controls.RemoveByKey(dir);
                    }
                }
                RaiseMessageHandler(this, "", MessageTypeOption.HiddenStatusBar);
            }
        }
        #endregion
    }
}
