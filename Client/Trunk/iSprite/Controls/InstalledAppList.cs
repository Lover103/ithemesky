﻿using System;
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
        ListViewGroup m_UserGroup = new ListViewGroup();
        ListViewGroup m_SystemGroup = new ListViewGroup();

        public event SetNodeCountHandler OnUpdateAppCount;
        #endregion

        void RaiseUpdateAppCount(int count)
        {
            if (OnUpdateAppCount != null)
            {
                OnUpdateAppCount("Installed Packages", count, false);
            }
        }

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public InstalledAppList(iPhoneFileDevice iphoneDevice, AppHelper apphelper)
        {
            m_iPhoneDevice = iphoneDevice;
            m_appHelper = apphelper;


            Initialise();
        }
        #endregion

        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        void Initialise()
        {
            this.FullRowSelect = true;
            this.View = View.Details;

            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, 18);//分别是宽和高
            this.SmallImageList = imgList;

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

            this.Columns.AddRange(
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
            this.Groups.Add(m_UserGroup);

            m_SystemGroup.Header = "System Deb";
            m_SystemGroup.Tag = m_UserGroup.Header;
            m_SystemGroup.HeaderAlignment = HorizontalAlignment.Left;
            this.Groups.Add(m_SystemGroup);
        }
        #endregion

        #region 删除程序
        /// <summary>
        /// 删除程序
        /// </summary>
        public void RemoveApps()
        {
            if (this.SelectedItems.Count > 0)
            {
                if (MessageHelper.ShowConfirm("Are you sure you want to delete the selected apps ? ") == DialogResult.OK)
                {
                    try
                    {
                        this.BeginUpdate();

                        bool flag = false;
                        for (int i = this.SelectedItems.Count - 1; i >= 0; i--)
                        {
                            ListViewItem item = this.SelectedItems[i];
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
                                    this.Items.RemoveByKey(item.Name);
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
                        this.EndUpdate();
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
            this.Items.Clear();

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
                    rowitem.Group = m_SystemGroup;
                }
                else
                {
                    rowitem.Group = m_UserGroup;
                }
                if (item.Name == string.Empty)
                {
                    item.Name = item.Package;
                }
                rowitem.Text = item.Name;

                rowitem.SubItems.Add(item.Version);

                rowitem.SubItems.Add(item.Installed_Size);

                rowitem.SubItems.Add(item.Description);

                rowitem.Tag = item;

                this.Items.Add(rowitem);
            }
            UpdateGroupCount();
        }

        void UpdateGroupCount()
        {
            m_UserGroup.Header = m_UserGroup.Tag + "(" + m_UserGroup.Items.Count + ")";
            m_SystemGroup.Header = m_SystemGroup.Tag + "(" + m_SystemGroup.Items.Count + ")";

            int count = this.Items.Count;
            RaiseUpdateAppCount(count);
        }
        #endregion
        
    }


}