using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;


namespace iSprite
{
    /// <summary>
    /// 自定义列表（文件/文件夹列表）
    /// </summary>
	internal class iListView : ListView
    {
        #region 变量定义
        const string CustomDropName = "iSprite-iListView-FileDrop";
		private string[] m_NormalHeadings = { "Name", "Size", "Type", "Date Modified" };
        IFileDevice m_FileDevice;
        private IDataObject dataobj;
        internal event MessageHandler OnMessage;
        internal event PathChanged OnPathChanged;
        #endregion

        #region 消息通知
        private void RaiseMessageHandler(object sender, string Message, MessageTypeOption messageType)
        {
            if (OnMessage != null)
            {
                OnMessage(sender, Message, messageType);
            }
        }
        private void RaisePathChanged(object sender, string newPath)
        {
            if (OnPathChanged != null)
            {
                OnPathChanged(sender, newPath);
            }
        }
        #endregion

        #region 设置当前路径
        string m_CurrentPath = string.Empty;
        string m_PreviousPath = string.Empty;
        /// <summary>
        /// 当前路径
        /// </summary>
        internal string CurrentPath
        {
            get { return this.m_CurrentPath; }
            set { SetCurrentPath(value); }
        }

        void SetCurrentPath(string newPath)
        {
            if (m_CurrentPath != newPath)
            {
                m_PreviousPath = m_CurrentPath;
                m_CurrentPath = newPath;
                LoadListByPath(m_CurrentPath);
            }
        }
        #endregion

        #region 选中的第一个文件夹路径
        /// <summary>
        /// 选中的第一个文件夹路径
        /// </summary>
        internal string FirstFolderPath
        {
            get 
            {
                foreach (ListViewItem item in this.SelectedItems)
                {
                    if ((ListViewItemTypeOption)item.Tag == ListViewItemTypeOption.Folder)
                    {
                        return item.Name;
                    }
                }
                return string.Empty;
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="filedevice"></param>
        /// <param name="imglist"></param>
        internal iListView(IFileDevice filedevice, ImageList imglist)
			: base()
        {
            this.FullRowSelect = true;
            m_FileDevice = filedevice;


            if (m_FileDevice.DeviceType == DeviceTypeOption.LocalDisk)
            {
                this.SmallImageList = IconHelper.SmallImageList;
            }
            else
            {
                this.SmallImageList = imglist;
            }

			Initialise();

            this.BackColor = Color.White;
        }
        private iListView()
        { 
        }

        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        void Initialise()
        {
            this.AllowDrop = true;

            #region 行编辑相关
            this.LabelEdit = true;
            this.BeforeLabelEdit += new LabelEditEventHandler(iListView_BeforeLabelEdit);
            this.AfterLabelEdit += new LabelEditEventHandler(iListView_AfterLabelEdit);
            #endregion

            this.DoubleClick += new EventHandler(iListView_DoubleClick);
            this.KeyDown += new KeyEventHandler(iListView_KeyDown);

            #region 拖拉
            this.ItemDrag += new ItemDragEventHandler(iListView_ItemDrag);
            this.DragEnter += new DragEventHandler(iListView_DragEnter);
            this.DragDrop += new DragEventHandler(iListView_DragDrop);
            #endregion

            this.View = View.Details;

            foreach (string name in m_NormalHeadings)
            {
                if (m_FileDevice.DeviceType != DeviceTypeOption.iPhone
                    || (name != "Date Modified" && m_FileDevice.DeviceType == DeviceTypeOption.iPhone))
                {
                    this.Columns.Add(name, name);
                }
            }
        }
        #endregion

        #endregion

        #region 键盘快捷键处理
        /// <summary>
        /// 键盘快捷键处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void iListView_KeyDown(object sender, KeyEventArgs e)
        {
            bool iscrtl = (e.Modifiers == Keys.Control);
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    OpenSelectItem();
                    break;
                case Keys.F5:
                    RefreshData();
                    break;
                case Keys.F2:
                    ItemBeginEdit();
                    break;
                case Keys.Delete:
                    DeleteFolder();
                    break;
                case Keys.Insert:
                    NewFolder();
                    break;
                case Keys.P:
                    if (iscrtl)
                    {
                        CopyPath();
                    }
                    break;
                case Keys.A:
                    if (iscrtl)
                    {
                        SelectAllItems();
                    }
                    break;
                case Keys.U:
                    if (iscrtl)
                    {
                        UnSelectAllItems();
                    }
                    break;
            }
            this.Focus();
        }
        #endregion

        #region 行编辑相关
        void iListView_BeforeLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (Utility.IsDriver(this.Items[e.Item].Name))
            {
                e.CancelEdit = true;
            }
        }

        void iListView_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Label != null && e.Label.Length > 0)
            {
                string newName = this.m_CurrentPath + e.Label;
                ListViewItem item = this.Items[e.Item];
                if ((ListViewItemTypeOption)item.Tag == ListViewItemTypeOption.File)
                {
                    //重命名文件
                    if (!m_FileDevice.FileExists(newName))
                    {
                        m_FileDevice.ReNameFile(item.Name, newName);
                        item.Name = newName;
                    }
                    else
                    {
                        e.CancelEdit = true;
                        MessageHelper.ShowError("This name already exists !");
                    }
                }
                else
                {
                    //重命名文件夹
                    if (!m_FileDevice.DirectoryExists(newName))
                    {
                        newName = newName + m_FileDevice.DirectorySeparatorChar;
                        m_FileDevice.ReNameFolder(item.Name, newName);
                        item.Name = newName;
                    }
                    else
                    {
                        e.CancelEdit = true;
                        MessageHelper.ShowError("This name already exists !");
                    }
                }

            }
        }
        #endregion

        #region 拖动相关事件
        /// <summary>
        /// 开始拖动项时发生的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void iListView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                List<string> filelist = new List<string>();

                foreach (ListViewItem item in this.SelectedItems)
                {
                    filelist.Add(item.Name);
                }

                if (filelist.Count > 0)
                {
                    Object2Drop obj = new Object2Drop();
                    obj.fileDrive = this.m_FileDevice;
                    obj.fileList = filelist;
                    dataobj = new DataObject(CustomDropName, obj);
                    DoDragDrop(dataobj, DragDropEffects.Copy);
                }
            }
        }

        /// <summary>
        /// 完成拖动时发生的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void iListView_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(CustomDropName))
            {
                //自定义拖拉
                Object2Drop dropobj = new Object2Drop();
                dropobj = (Object2Drop)e.Data.GetData(CustomDropName, true);  
                DropItems(this, dropobj);
            }
            else
            { 
                //从系统文件夹拖入
                if (e.Data.GetDataPresent("FileDrop"))
                {
                    Object2Drop dropobj = new Object2Drop();
                    dropobj.fileDrive = new LoaclDiskFileDevice();
                    List<string> pathList = new List<string>();
                    foreach (string path in (string[])e.Data.GetData("FileDrop", true))
                    {
                        pathList.Add(path);
                    }
                    dropobj.fileList = pathList;

                    DropItems(this, dropobj);
                }
            }
        }

        void DropItems(iListView curentList, Object2Drop dropobj)
        {
            List<string> pathList = new List<string>();
            foreach (string file in dropobj.fileList)
            {
                pathList.Add(file);
            }

            if (pathList.Count > 0)
            {
                if (dropobj.fileDrive.DeviceType == DeviceTypeOption.LocalDisk)
                {
                    if (curentList.m_FileDevice.DeviceType == DeviceTypeOption.iPhone)
                    {
                        //从PC拖到到iPhone
                        iPhoneFileDevice iphonedriver = (iPhoneFileDevice)curentList.m_FileDevice;
                        foreach (string sourceName in pathList)
                        {
                            iphonedriver.Copy2iPhone(sourceName, m_CurrentPath);
                        }
                        curentList.RefreshData();
                    }
                    else if (curentList.m_FileDevice.DeviceType == DeviceTypeOption.LocalDisk)
                    {
                        string destName = string.Empty;
                        LoaclDiskFileDevice pcdriver = (LoaclDiskFileDevice)dropobj.fileDrive;
                        foreach (string sourceName in pathList)
                        {
                            if (sourceName.EndsWith(@"\") || pcdriver.DirectoryExists(sourceName))
                            {
                                //文件夹
                                destName = m_CurrentPath + Utility.GetDirName(sourceName);
                                pcdriver.CopyDirectory(sourceName, destName);
                            }
                            else
                            {
                                //文件  
                                destName = m_CurrentPath + Path.GetFileName(sourceName);
                                pcdriver.CopyFile(sourceName, destName);
                            }
                        }
                        curentList.RefreshData();
                    }
                }
                else if (dropobj.fileDrive.DeviceType == DeviceTypeOption.iPhone)
                {
                    if (curentList.m_FileDevice.DeviceType == DeviceTypeOption.LocalDisk)
                    {
                        //从iPhone拖到到PC
                        iPhoneFileDevice iphonedriver = (iPhoneFileDevice)dropobj.fileDrive;
                        foreach (string sourceName in pathList)
                        {
                            iphonedriver.Downlod2PC(sourceName, m_CurrentPath);
                        }
                        curentList.RefreshData();
                    }
                }
            }
        }

        /// <summary>
        /// 将对象拖动到对象的边界时发生的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void iListView_DragEnter(object sender, DragEventArgs e)
        {
            if (
                (m_FileDevice.DeviceType == DeviceTypeOption.LocalDisk && this.m_CurrentPath != m_FileDevice.StartPath)
                || m_FileDevice.DeviceType == DeviceTypeOption.iPhone
                )
            {
                if (e.Data.GetDataPresent(CustomDropName) || e.Data.GetDataPresent("FileDrop") || e.Data.GetDataPresent("FilenameW"))
                {
                    e.Effect = e.AllowedEffect;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        #endregion

        #region 根据指令执行相应动作
        /// <summary>
        /// 根据指令执行相应动作
        /// </summary>
        /// <param name="command"></param>
        public void DoByCommandOption(ButtonCommandOption command)
        {
            switch (command)
            {
                case ButtonCommandOption.Refresh:
                    RefreshData();
                    break;
                case ButtonCommandOption.NewFloder:
                    NewFolder();
                    break;
                case ButtonCommandOption.Delete:
                    DeleteFolder();
                    break;
                case ButtonCommandOption.CopyPath:
                    CopyPath();
                    break;
                case ButtonCommandOption.SelectAll:
                    SelectAllItems();
                    break;
                case ButtonCommandOption.UnSelectAll:
                    UnSelectAllItems();
                    break;
                case ButtonCommandOption.EditName:
                    ItemBeginEdit();
                    break;

                case ButtonCommandOption.PermissionsSetting:
                    PermissionsSetting();
                    break;
            }
        }
        #endregion

        #region 设置权限
        void PermissionsSetting()
        {
            if (this.SelectedItems.Count >= 1)
            {
                ListViewItem item = this.SelectedItems[0];
                ChmodEditer.Show((iPhoneFileDevice)m_FileDevice,
                       new MessageHandler(RaiseMessageHandler), item.Name
                       );
            }
        }
        #endregion

        #region 开始修改名字
        /// <summary>
        /// 开始修改名字
        /// </summary>
        void ItemBeginEdit()
        {
            if (this.SelectedItems.Count >= 1)
            {
                ListViewItem item = this.SelectedItems[0]; 
                item.BeginEdit();
            }
        }
        #endregion

        #region 全选
        /// <summary>
        /// 全选
        /// </summary>
        void SelectAllItems()
        {
            foreach (ListViewItem item in this.Items)
            {
                item.Selected = true;
            }
            this.Focus();
        }
        #endregion

        #region 取消所有选中状态
        /// <summary>
        /// 取消所有选中状态
        /// </summary>
        void UnSelectAllItems()
        {
            foreach (ListViewItem item in this.Items)
            {
                item.Selected = !item.Selected;
            }
            this.Focus();
        }
        #endregion

        #region 刷新列表
        /// <summary>
        /// 刷新列表
        /// </summary>
        void RefreshData()
        {
            LoadListByPath(m_CurrentPath);
        }
        #endregion

        #region 拷贝路径
        /// <summary>
        /// 拷贝路径
        /// </summary>
        void CopyPath()
        {
            StringBuilder sb = new StringBuilder();
            foreach (ListViewItem item in this.SelectedItems)
            {
                sb.AppendLine(item.Name);
            }
            if (sb.Length > 0)
            {
                Clipboard.SetDataObject(sb.ToString().Trim());
            }
        }
        #endregion

        #region 新建文件夹
        /// <summary>
        /// 新建文件夹
        /// </summary>
        void NewFolder()
        {
            if (m_FileDevice.DeviceType == DeviceTypeOption.LocalDisk
                && m_CurrentPath == m_FileDevice.StartPath)
            {
                return;
            }
            string baseName = "new Folder";
            string folderName = this.m_CurrentPath + baseName + m_FileDevice.DirectorySeparatorChar;
            int i = 0;
            while (m_FileDevice.DirectoryExists(folderName))
            {
                folderName = this.m_CurrentPath + baseName + string.Format(" ({0})", ++i) + m_FileDevice.DirectorySeparatorChar;
            }
            if (m_FileDevice.CreateDirectory(folderName))
            {
                //Name
                ListViewItem item = CreateItem(folderName, ListViewItemTypeOption.Folder);

                //Size
                item.SubItems.Add(new ListViewItem.ListViewSubItem().Text = "");
                //Type
                item.SubItems.Add(new ListViewItem.ListViewSubItem().Text = "Folder");
                this.Items.Add(item);

                item.BeginEdit();
            }
        }
        #endregion

        #region 删除文件夹
        /// <summary>
        /// 删除文件夹
        /// </summary>
        void DeleteFolder()
        {
            //这里要增加删除之前的确认
            if (this.SelectedItems.Count > 0)
            {
                if (MessageHelper.ShowConfirm("Are you sure you want to delete the selected file / folder ? ") == DialogResult.OK)
                {
                    foreach (ListViewItem item in this.SelectedItems)
                    {
                        if ((ListViewItemTypeOption)item.Tag == ListViewItemTypeOption.File)
                        {
                            //文件
                            m_FileDevice.DeleteFile(item.Name);
                        }
                        else
                        {
                            //文件夹
                            m_FileDevice.DeleteDirectory(item.Name,true);
                        }
                        this.Items.RemoveByKey(item.Name);
                    }
                }
            }
        }
        #endregion        

        #region 行双击事件
        /// <summary>
        /// 行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void iListView_DoubleClick(object sender, EventArgs e)
        {
            OpenSelectItem();
        }
        void OpenSelectItem()
        {
            if (this.SelectedItems.Count >= 1)
            {
                ListViewItem item = this.SelectedItems[0];
                if ((ListViewItemTypeOption)item.Tag == ListViewItemTypeOption.File)
                {
                    //打开文件
                    m_FileDevice.ShowFile(item.Name);
                }
                else
                {
                    //打开文件夹
                    RaisePathChanged(this, item.Name);
                    this.SetCurrentPath(item.Name);
                }
            }
        }
        #endregion

        #region 创建行对象
        /// <summary>
        /// 创建行对象
        /// </summary>
        /// <param name="name"></param>
        /// <param name="itemType"></param>
        /// <returns></returns>
        ListViewItem CreateItem(string name,ListViewItemTypeOption itemType)
        {
            ListViewItem item = new ListViewItem();
            item.Name = name;

            //图标设置
            if (m_FileDevice.DeviceType == DeviceTypeOption.LocalDisk)
            {
                item.ImageIndex = IconHelper.GetImageIndex(name);
            }
            else if (m_FileDevice.DeviceType == DeviceTypeOption.iPhone)
            {
                if (itemType == ListViewItemTypeOption.Folder)
                {
                    item.ImageIndex = (int)iPhoneIconOption.CloseFolder;
                }
                else
                {
                    item.ImageIndex = (int)iPhoneIconOption.File;
                }
            }

            if (itemType == ListViewItemTypeOption.Folder)
            {
                item.Text = Utility.GetDirName(name);
            }
            else
            {
                item.Text = Path.GetFileName(name);
            }
            item.Tag = itemType;

            return item;
        }
        #endregion

        #region 加载列表
        /// <summary>
        /// 加载列表
        /// </summary>
        /// <param name="path"></param>
        private void LoadListByPath(string path)
        {
            this.Items.Clear();
            Application.DoEvents();
            ListViewItem item;


            foreach (iFileInfo fileinfo in m_FileDevice.GetFileInfos(path))
            {
                item = CreateItem(fileinfo.FullPath, ListViewItemTypeOption.File);

                //Size
                item.SubItems.Add(new ListViewItem.ListViewSubItem().Text = Utility.FormatFileSize(fileinfo.FileSize));

                //Type
                item.SubItems.Add(new ListViewItem.ListViewSubItem().Text = "File");

                if (fileinfo.UpdateTime != DateTime.MinValue)
                {
                    item.SubItems.Add(new ListViewItem.ListViewSubItem().Text = fileinfo.UpdateTime.ToString("yyyy-MM-dd"));
                }

                this.Items.Add(item);
                Application.DoEvents();
            }


            foreach (string fullname in m_FileDevice.GetDirectories(path))
            {
                //Name
                item = CreateItem(fullname, ListViewItemTypeOption.Folder);

                //Size
                item.SubItems.Add(new ListViewItem.ListViewSubItem().Text = "");

                //Type
                item.SubItems.Add(new ListViewItem.ListViewSubItem().Text = "Folder");
                this.Items.Add(item);
                Application.DoEvents();
            }


            if (this.Columns.Count > 0)
            {
                Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                if (Columns[0].Width < 30)
                {
                    Columns[0].Width = 30;
                }
            }

            if (this.Items.Count > 0)
            {
                this.Items[0].Focused = true;
            }
        }
        #endregion
	}
}