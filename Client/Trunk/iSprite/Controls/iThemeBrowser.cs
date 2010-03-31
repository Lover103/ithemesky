using System;
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


        internal event FileProgressHandler OnProgressHandler;

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
            m_ThemePriview.Height = 548;
        }

        void InitThemeTab()
        {
            m_Toolmenu = new ToolStrip();
            this.m_tabTheme.Controls.Add(m_Toolmenu);
            m_Toolmenu.Items.Add(CreateMenuItem("tsbtn_InstallFromZIP", "Install From ZIP", global::iSprite.Resource.zip_32));
            m_Toolmenu.Items.Add(CreateMenuItem("tsbtn_InstallFromFolder", "Install From Folder", global::iSprite.Resource.Folder_16));

            foreach (ToolStripItem item in m_Toolmenu.Items)
            {
                if (item is ToolStripButton)
                {
                    item.Click += new EventHandler(toolmenu_Click);
                    item.Enabled = false;
                }
            }

            WebBrowser themeBrowser = new WebBrowser();
            this.m_tabTheme.Controls.Add(themeBrowser);

            //themeBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            themeBrowser.Location = new System.Drawing.Point(m_Toolmenu.Location.X, m_Toolmenu.Location.Y + m_Toolmenu.Height);
            themeBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            themeBrowser.Name = "themeBrowser";
            themeBrowser.Size = new System.Drawing.Size(990, 652);
            themeBrowser.TabIndex = 0;
            themeBrowser.Url = new System.Uri(iSpriteContext.Current.ThemeHomePage, System.UriKind.Absolute);

            themeBrowser.Navigating += new WebBrowserNavigatingEventHandler(themeBrowser_Navigating);
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
            string action = string.Empty;
            if (action.Equals("download", StringComparison.CurrentCultureIgnoreCase))
            {
                e.Cancel = true;

                //判断是否安装WinterBoard
                if (!CheckInstallWinterBoard())
                {
                    return;
                }

                string url = string.Empty;
                string themeName = string.Empty;

                string filepath = iSpriteContext.Current.iSpriteTempPath + "/" + Path.GetFileNameWithoutExtension(themeName) + ".zip";

                if (Utility.DownloadFile(url, filepath, OnProgressHandler))
                {
                    InstallFromZIP(filepath);
                }

            }
        }
        #endregion

        #region 创建新按钮
        /// <summary>
        /// 按钮事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void toolmenu_Click(object sender, EventArgs e)
        {
            ToolStripItem item = (ToolStripItem)sender;
            switch (item.Name)
            {
                case "tsbtn_InstallFromZIP":
                    InstallFromZIP();
                    break;

                case "tsbtn_InstallFromFolder":
                    InstallFromFolder();
                    break;
            }
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
            string tozippath = iSpriteContext.Current.iSpriteTempPath + "\\" + Path.GetRandomFileName() + "\\";

            if (ZipHelper.UnZip(zippath, tozippath) > 0)
            {
                string themepath = SelectRightThemePath(tozippath);

                if (themepath != string.Empty)
                {
                    InstallFromFolder(
                        Path.GetFileNameWithoutExtension(themepath).TrimEnd('\\'),
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
                string themeName = Path.GetFileName(themepath.TrimEnd('\\'));
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
                RaiseMessageHandler(this, "Fail to operate ! iPhone is disconnected .", MessageTypeOption.Error);
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
            return iconnum > 0;
        }
        #endregion

        #endregion

        #region 设置主题
        void PreSetTheme(string themeName, string themePath)
        {
            if (CheckSameThemeName(themeName))
            {
                if (MessageHelper.ShowConfirm(
                    string.Format("The theme[{0}] is already exist, would you want to recover it?", themeName))
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
            List<string> themeInfo = new List<string>();
            themeInfo.Add(themeName);
            themeInfo.Add(themePath);
            m_ThemePriview.ShowPriview(themeInfo);
        }

        void ThemePriview_OnMessage(List<string> themeInfo, ThemePriviewMessageTypeOption messagetype)
        {
            if (messagetype == ThemePriviewMessageTypeOption.Apply)
            {
                SetTheme(themeInfo[0], themeInfo[1]);
            }
        }
        /// <summary>
        /// 设置主题
        /// </summary>
        /// <param name="themeName"></param>
        /// <param name="themePath"></param>
        void SetTheme(string themeName, string themePath)
        {
            if (!m_iPhoneDevice.Copy2iPhone(themePath, iSpriteContext.Current.iPhone_WinterBoardFile_Path + "/" + themeName))
            {
                RaiseMessageHandler(this, "Fail to copy the theme file to iPhone .", MessageTypeOption.Error);
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
                string trueTheme = string.Format("<dict>\n\t\t\t<key>Active</key>\n\t\t\t<false />\n\t\t\t<key>Name</key>\n\t\t\t<string>{0}</string>\n\t\t</dict>\n", themeName);
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
                    RaiseMessageHandler(this, "Successfully install theme[" + themeName + "] ,Please open winterboard to apply it. ", MessageTypeOption.Info);
                }
                else
                {
                    RaiseMessageHandler(this, "Fail to copy the theme setting file to iPhone .", MessageTypeOption.Error);
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
            }
            foreach (ToolStripItem item in m_Toolmenu.Items)
            {
                if (item is ToolStripButton)
                {
                    item.Enabled = isContected;
                }
            }
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
                return true;
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


        bool CheckInstalliReboot()
        {
            return false;
        }


        #region 安装WinterBoard
        /// <summary>
        /// 安装WinterBoard
        /// </summary>
        /// <returns></returns>
        bool InstallWinterBoard()
        {
            string wbxmlpath = iSpriteContext.Current.iSpriteTempPath + "\\" + "update.xml";
            if (Utility.DownloadFile(iSpriteContext.Current.WinterBoardXML, wbxmlpath, OnProgressHandler))
            { 
                DataSet ds = new DataSet();
                ds.ReadXml(wbxmlpath);
                if (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count >= 1)
                {
                    DataRow[] rows = ds.Tables[0].Select("Ver='" + m_iPhoneDevice.DeviceVersion + "'");
                    if (rows.Length == 0)
                    {
                        rows = ds.Tables[0].Select("Ver='all'");
                    }
                    if (rows.Length >0)
                    {
                        string wburl = rows[0]["downurl"].ToString();
                        string wbfilepath = iSpriteContext.Current.iSpriteTempPath + "\\" + "winterboard.deb";
                        if (Utility.DownloadFile(wburl, wbfilepath, OnProgressHandler))
                        {
                            if (m_iPhoneDevice.Copy2iPhone(wbfilepath,
                                iSpriteContext.Current.iPhone_CydiaAutoInstallPath))
                            {
                                MessageHelper.ShowConfirm("WinterBoard has been copied to iPhone, please reboot your iPhone to finish Installation.");
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
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
    }
}
