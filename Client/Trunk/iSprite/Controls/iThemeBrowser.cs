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

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="filedevice"></param>
        /// <param name="ctxFavourites"></param>
        internal iThemeBrowser(iPhoneFileDevice filedevice, FATabStripItem tabTheme)
        {
            m_iPhoneDevice = filedevice;
            this.m_tabTheme = tabTheme;
            m_DefaultIconDic = new Dictionary<string, string>(); 
            InitThemeTab();
        }

        private iThemeBrowser()
        { 
        }


        void InitThemeTab()
        {
            ToolStrip toolmenu = new ToolStrip();
            this.m_tabTheme.Controls.Add(toolmenu);
            toolmenu.Items.Add(CreateMenuItem("tsbtn_InstallFromZIP", "Install From ZIP", global::iSprite.Resource.zip_32));
            toolmenu.Items.Add(CreateMenuItem("tsbtn_InstallFromFolder", "Install From Folder", global::iSprite.Resource.Folder_16));

            foreach (ToolStripItem item in toolmenu.Items)
            {
                if (item is ToolStripButton)
                {
                    item.Click += new EventHandler(toolmenu_Click);
                }
            }

            WebBrowser themeBrowser = new WebBrowser();
            this.m_tabTheme.Controls.Add(themeBrowser);

            themeBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            themeBrowser.Location = new System.Drawing.Point(0, 0);
            themeBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            themeBrowser.Name = "themeBrowser";
            themeBrowser.Size = new System.Drawing.Size(990, 652);
            themeBrowser.TabIndex = 0;
            themeBrowser.Url = new System.Uri(iSpriteContext.Current.ThemeHomePage, System.UriKind.Absolute);

            themeBrowser.Navigating += new WebBrowserNavigatingEventHandler(themeBrowser_Navigating);
        }

        void themeBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            string action = string.Empty;
            if (action.Equals("download", StringComparison.CurrentCultureIgnoreCase))
            {
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
            string themepath = iSpriteContext.Current.iSpriteTempPath + "\\" + Path.GetRandomFileName() + "\\";

            if (ZipHelper.UnZip(zippath, themepath) > 0)
            {
                string[] dirs = Directory.GetDirectories(themepath);
                if (dirs.Length == 1
                    && !dirs[0].EndsWith("\\Icons\\", true, null))
                {
                    themepath = dirs[0];
                }

                InstallFromFolder(
                    Path.GetFileNameWithoutExtension(zippath),
                    themepath
                    );
            }
        }
        #endregion

        #region 从文件夹安装主题
        /// <summary>
        /// 从文件夹安装主题
        /// </summary>
        void InstallFromFolder()
        {
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
                SetTheme(themeName, themepath);
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
                if (File.Exists(themePath + "/Icons/" + item.Value + ".png"))
                {
                    iconnum++;
                }
            }
            return iconnum > 0;
        }
        #endregion

        #endregion

        internal void AfterDeviceFinishConnected()
        {
            m_DefaultIconDic = GetLocalizedApplicationNamesByLan(DefaultLanage);
        }

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

                    foreach (KeyValuePair<string, string> item in m_DefaultIconDic)
                    {
                        if (currenticondic.ContainsKey(item.Key) && currenticondic[item.Key] != item.Value)
                        {
                            if (File.Exists(themePath + "/Icons/" + item.Value + ".png"))
                            {
                                File.Move(themePath + "/Icons/" + item.Value + ".png", themePath + "/Icons/" + currenticondic[item.Key] + ".png");
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

        #region 设置主题
        /// <summary>
        /// 设置主题
        /// </summary>
        /// <param name="themeName"></param>
        /// <param name="themePath"></param>
        void SetTheme(string themeName, string themePath)
        {
            if (!CheckThemePacket(themePath))
            {
                RaiseMessageHandler(this, "Invalid theme !", MessageTypeOption.Error);
                return;
            }

            //if (!RenameThemeIconByLocalized(themePath))
            //{
            //    RaiseMessageHandler(this, "Can't convert theme for your current lang !", MessageTypeOption.Error);
            //    return;
            //}

            if (!m_iPhoneDevice.Copy2iPhone(themePath, iSpriteContext.Current.iPhone_WinterBoardFile_Path))
            {
                RaiseMessageHandler(this, "Fail to copy the theme file to iPhone .", MessageTypeOption.Error);
                return;
            }
            return;

            string iPhoneThemeSetting = iSpriteContext.Current.iPhone_WinterBoardSetting_Path;
            string localThemeSetting = iSpriteContext.Current.iSpriteTempPath + Path.GetFileName(iPhoneThemeSetting);
            if (m_iPhoneDevice.Downlod2PC(iPhoneThemeSetting, localThemeSetting))
            {
                StreamReader reader = new StreamReader(localThemeSetting, Encoding.UTF8);
                string xml = reader.ReadToEnd();
                reader.Close();
                string trueTheme = string.Format("<dict>\n\t\t\t<key>Active</key>\n\t\t\t<true/>\n\t\t\t<key>Name</key>\n\t\t\t<string>{0}</string>\n\t\t</dict>\n", themeName);
                string falseTheme = string.Format("<dict>\n\t\t\t<key>Active</key>\n\t\t\t<false/>\n\t\t\t<key>Name</key>\n\t\t\t<string>{0}</string>\n\t\t</dict>\n", themeName);
                xml = xml.Replace(trueTheme, "").Replace(falseTheme, "");
                xml = xml.Insert(xml.IndexOf("<array>\n\t\t") + "<array>\n\t\t".Length, trueTheme); ;
                StreamWriter writer = new StreamWriter(iPhoneThemeSetting, false, Encoding.UTF8);
                writer.Write(xml);
                writer.Flush();
                writer.Close();

                if (m_iPhoneDevice.Copy2iPhone(localThemeSetting, iPhoneThemeSetting))
                {
                    RaiseMessageHandler(this, "Successfully install theme .", MessageTypeOption.Info);
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
