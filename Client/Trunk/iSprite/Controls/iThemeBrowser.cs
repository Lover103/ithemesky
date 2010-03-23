using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Data;
using iSprite.ThirdControl.FarsiLibrary;
using CE.iPhone.PList;

namespace iSprite
{
    internal class iThemeBrowser
    {
        #region 变量定义
        iPhoneFileDevice m_iPhoneDevice;
        public event PathChanged OnPathChanged;
        internal event MessageHandler OnMessage;
        Dictionary<string, string> m_icondic;
        FATabStripItem m_tabTheme;
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
            m_icondic = new Dictionary<string, string>(); 
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
            themeBrowser.Url = new System.Uri("http://www.ithemesky.com/", System.UriKind.Absolute);
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
                themepath = iSpriteContext.Current.iSpriteTempPath+"\\"+Guid.NewGuid().ToString()+"\\";
                if (ZipHelper.UnZip(dialog.FileName, themepath) > 0)
                {
                    InstallFromFolder(themepath);
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
            string themepath = string.Empty;
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                themepath = fbd.SelectedPath;
                InstallFromFolder(themepath);
            }
        }

        /// <summary>
        /// 从文件夹安装主题
        /// </summary>
        /// <param name="themepath"></param>
        void InstallFromFolder(string themepath)
        {
            if (m_iPhoneDevice.IsConnected)
            {
            }
            else
            {
                RaiseMessageHandler(this, "Fail to operate ! iPhone is disconnected .", MessageTypeOption.Error);
            }
        }
        bool CheckThemePacket(string themepath)
        {
            return true;
        }
        #endregion

        internal void AfterDeviceFinishConnected()
        {
            m_icondic = GetLocalizedApplicationNames();
        }

        void RenameThemeIconByLocalized(string themePath)
        { 
        }
        void SetTheme(string themeName,string themePath)
        {
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

        Dictionary<string, string> GetLocalizedApplicationNames()
        {
            Dictionary<string, string>  icondic = new Dictionary<string, string>(); 
            string iphonecfgpath = string.Format(iSpriteContext.Current.iPhone_LocalizedApplicationNames_Path, "English.lproj");
            string localcfgpath = iSpriteContext.Current.iSpriteApplicationDataPath + "\\LocalizedApplicationNames.strings";
            if (m_iPhoneDevice.Downlod2PC(iphonecfgpath, localcfgpath))
            {
                PListRoot root = PListRoot.Load(localcfgpath);
            }

            return icondic;
        }

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
