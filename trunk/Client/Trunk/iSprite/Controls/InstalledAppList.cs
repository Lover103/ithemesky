using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace iSprite
{
    internal class InstalledAppList : ListView
    {
        #region 变量定义
        iPhoneFileDevice m_iPhoneDevice;
        AppHelper m_appHelper;
        List<string> m_SystemDeb;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public InstalledAppList(iPhoneFileDevice iphoneDevice, AppHelper apphelper)
        {
            m_iPhoneDevice = iphoneDevice;
            m_appHelper = apphelper;

            m_SystemDeb = new List<string>(new string[] { 
                "adv-cmds", "apt", "base", "bash", "berkeleydb", "bigboss", "bzip2", "coreutils", "cydia", "cydia-sources", "darwintools", "diffutils", "dpkg", "findutils", "gettext", "gnupg", 
                "grep", "gzip", "inetutils", "ispazio.net", "less", "libgcc", "libutil", "modmyifone", "nano", "ncurses", "network-cmds", "pcre", "readline", "saurik", "sed", "shell-cmds", 
                "ste", "system-cmds", "tar", "unzip", "yellowsn0w.com", "zodttd", "firmware", "apr-lib", "apt7-key", "apt7-lib", "coreutils-bin", "essential", "lzma", "pam", "pam-modules", "profile.d"
             });

            Initialise();
        }
        #endregion

        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        void Initialise()
        {
            this.GridLines = true;
            this.View = View.Details;

            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, 18);//分别是宽和高
            this.SmallImageList = imgList;

            ColumnHeader columnFile = new ColumnHeader();
            ColumnHeader columnSize = new ColumnHeader();
            ColumnHeader columnDescription = new ColumnHeader();
            ColumnHeader columnVersion = new ColumnHeader();

            columnFile.Text = "Name";
            columnFile.Width = 200;

            columnVersion.Text = "Version";
            columnVersion.Width = 80;

            columnSize.Text = "Size";
            columnSize.Width = 80;

            columnDescription.Text = "Description";
            columnDescription.Width = 300;

            this.Columns.AddRange(
                new ColumnHeader[] 
                {
                    columnFile,
                    columnVersion,
                    columnSize,
                    columnDescription,
                }
            );
        }
        #endregion

        public int LoadData()
        {
            string content = m_iPhoneDevice.GetFileText("/private/var/lib/dpkg/status");
            List<AptData> AptList  = new List<AptData>();
            Dictionary<string, InstalledDebItem> InstalledList = new Dictionary<string, InstalledDebItem>();
            if (m_appHelper.ParseAptData(content, ref AptList))
            {
                foreach (AptData apt in AptList)
                {
                    InstalledDebItem item = AptData2DebItem(apt);
                    if (!InstalledList.ContainsKey(item.DebID))
                    {
                        item.IsSystemDeb = m_SystemDeb.Contains(item.Package);

                        InstalledList.Add(item.DebID, item);
                    }
                }
            }
            ListViewGroup  usergroup = new ListViewGroup();
            usergroup.Header = "User Deb";
            usergroup.HeaderAlignment = HorizontalAlignment.Left;
            this.Groups.Add(usergroup);

            ListViewGroup systemgroup = new ListViewGroup();
            systemgroup.Header = "System Deb";
            systemgroup.HeaderAlignment = HorizontalAlignment.Left;
            this.Groups.Add(systemgroup);

            foreach (KeyValuePair<string, InstalledDebItem> kv in InstalledList)
            {
                InstalledDebItem item = kv.Value;

                ListViewItem rowitem = new ListViewItem();

                if (item.IsSystemDeb)
                {
                    rowitem.Group = systemgroup;
                }
                else
                {
                    rowitem.Group = usergroup;
                }

                rowitem.Text = item.Package;

                // size
                rowitem.SubItems.Add(item.Version);

                // Installed_Size
                rowitem.SubItems.Add(item.Installed_Size);

                // progress
                rowitem.SubItems.Add(item.Description);

                rowitem.Tag = item;

                this.Items.Add(rowitem);
            }

            return this.Items.Count;
        }

        #region 将AptData转换成PackageItem
        /// <summary>
        /// 将AptData转换成PackageItem
        /// </summary>
        /// <param name="aptdata"></param>
        /// <returns></returns>
        InstalledDebItem AptData2DebItem(AptData aptdata)
        {
            InstalledDebItem item = new InstalledDebItem();
            item.Name = aptdata.GetTagValue("Name");
            item.Package = aptdata.GetTagValue("Package");
            item.Status = aptdata.GetTagValue("Status");
            item.Version = aptdata.GetTagValue("Version");
            item.Installed_Size = aptdata.GetTagValue("Installed-Size");
            item.Description = aptdata.GetTagValue("Description");

            return item;
        }
        #endregion
    }


}
