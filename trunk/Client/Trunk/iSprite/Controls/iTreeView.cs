using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Windows.Forms;


namespace iSprite
{
    /// <summary>
    /// 自定义文件夹树
    /// </summary>
	public class iTreeView : System.Windows.Forms.TreeView
    {
        #region 变量定义
        IFileDevice m_FileDevice;
        bool IsCollapsing = false;
        char m_DirectorySeparatorChar;
        internal event MessageHandler OnMessage;
        internal event PathChanged OnPathChanged;
        string m_CurrentPath = string.Empty;
        string m_PreviousPath = string.Empty;
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
        /// <summary>
        /// 当前路径
        /// </summary>
        internal string CurrentPath
        {
            get { return this.m_CurrentPath; }
            set { SetCurrentPath(value); }
        }
        void SetCurrentPath(string newpath)
        {
            if (m_CurrentPath != newpath)
            {
                m_PreviousPath = m_CurrentPath;
                m_CurrentPath = newpath;

                if (this.Nodes.Count == 0)
                {
                    //加载根节点
                    string rootName = string.Empty;
                    int imgInx=0;
                    if (m_FileDevice.DeviceType == DeviceTypeOption.iPhone)
                    {
                        imgInx = (int)iPhoneIconOption.iPhone;
                        rootName = m_FileDevice.DeviceName;
                    }
                    else
                    {
                        imgInx = IconHelper.GetImageIndex(CSIDL.CSIDL_DRIVES);
                        rootName = m_FileDevice.StartPath;
                    }

                    TreeNode root = this.Nodes.Add(m_CurrentPath, rootName, imgInx, imgInx);
                    root.Nodes.Add(CreateEmtyNode());
                    LoadSubNodesByPath(root, root.Name);
                    RaisePathChanged(this, m_CurrentPath);
                }
                else
                {
                    SelectNodeByName(m_CurrentPath, true);
                }
            }
        }
        #endregion

        #region 创建空节点
        /// <summary>
        /// 创建空节点
        /// </summary>
        /// <returns></returns>
        TreeNode CreateEmtyNode()
        {
            return CreateEmtyNode("opening...");
        }

        /// <summary>
        /// 创建空节点
        /// </summary>
        /// <param name="nodeText"></param>
        /// <returns></returns>
        TreeNode CreateEmtyNode(string nodeText)
        {
            int imginx = -1;
            if (m_FileDevice.DeviceType == DeviceTypeOption.iPhone)
            {
                imginx = (int)iPhoneIconOption.CloseFolder;
            }
            else
            {
                imginx = IconHelper.GetImageIndex("dic");
            }
            return new TreeNode(nodeText, imginx, imginx);
        }
        #endregion

        #region 获取当前选中的路径
        /// <summary>
        /// 获取当前选中的路径
        /// </summary>
        private string SelectPath
        {
            get
            {
                if (this.SelectedNode != null)
                {
                    return this.SelectedNode.Name;
                }
                else
                {
                    return m_CurrentPath;
                }
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="filedevice"></param>
        /// <param name="startPath"></param>
        /// <param name="imglist"></param>
        public iTreeView(IFileDevice filedevice, ImageList imglist)
            : base()
        {
            m_FileDevice = filedevice;

            m_DirectorySeparatorChar = m_FileDevice.DirectorySeparatorChar;


            if (m_FileDevice.DeviceType == DeviceTypeOption.LocalDisk)
            {
                this.ImageList = IconHelper.SmallImageList;
            }
            else
            {
                this.ImageList = imglist;
            }

			this.HideSelection = false;

            this.AfterExpand += new TreeViewEventHandler(iTreeView_AfterExpand);
            this.AfterSelect += new TreeViewEventHandler(iTreeView_AfterSelect);

            this.BeforeCollapse += new TreeViewCancelEventHandler
                (
                    delegate(object sender, TreeViewCancelEventArgs e)
                    {
                        IsCollapsing = true;
                    }
                );

            this.AfterCollapse += new TreeViewEventHandler
                (
                    delegate(object sender, TreeViewEventArgs e)
                    {
                        IsCollapsing = false;
                        if (m_FileDevice.DeviceType == DeviceTypeOption.iPhone)
                        {
                            e.Node.ImageIndex = (int)iPhoneIconOption.CloseFolder;
                        }
                    }
                );

            #region 节点编辑相关
            this.LabelEdit = true;
            this.BeforeLabelEdit += new NodeLabelEditEventHandler(iTreeView_BeforeLabelEdit);
            this.AfterLabelEdit += new NodeLabelEditEventHandler(iTreeView_AfterLabelEdit);
            #endregion

            this.KeyDown += new KeyEventHandler(iTreeView_KeyDown);

        }
        private iTreeView()
        { 
        }
        #endregion  
        
        #region 根据指令执行相应动作
        /// <summary>
        /// 根据指令执行相应动作
        /// </summary>
        /// <param name="command"></param>
        internal void DoByCommandOption(ButtonCommandOption command)
        {
            switch (command)
            {
                case ButtonCommandOption.Refresh:
                    RefreshTree();
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
            }
        }
        #endregion

        #region 键盘快捷键处理
        /// <summary>
        /// 键盘快捷键处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void iTreeView_KeyDown(object sender, KeyEventArgs e)
        {
            bool iscrtl = (e.Modifiers == Keys.Control);
            switch (e.KeyCode)
            {
                case Keys.F5:
                    RefreshTree();
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
            }
        }
        #endregion

        #region 节点编辑相关
        /// <summary>
        /// 节点开始编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void iTreeView_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (Utility.IsDriver(e.Node.Name) || null == e.Node.Parent)
            {
                e.CancelEdit = true;
            }
        }
        /// <summary>
        /// 节点完成编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void iTreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label != null && e.Label.Length > 0)
            {
                TreeNode node = e.Node;
                if (null != node.Parent)
                {
                    string newName = node.Parent.Name + e.Label;


                    //重命名文件夹
                    newName = newName + m_FileDevice.DirectorySeparatorChar;

                    if (!m_FileDevice.DirectoryExists(newName))
                    {
                        m_FileDevice.ReNameFolder(node.Name, newName);

                        node.Name = newName;
                        m_CurrentPath = node.Name;
                        RaisePathChanged(this, m_CurrentPath);
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

        #region 节点选择后事件处理
        /// <summary>
        /// 节点选择后事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void iTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null)
            {
                return;
            }

            if (!IsCollapsing && !this.SelectedNode.IsExpanded)
            {
                if (SelectedNode.Nodes.Count > 0)
                {
                    this.SelectedNode.Expand();
                }
                else
                {
                    LoadSubNodesByPath(e.Node, e.Node.Name);
                    RaisePathChanged(this, SelectPath);
                }
            }
            else
            {
                RaisePathChanged(this, SelectPath);
            }
        }
        #endregion

        #region 在展开树节点后发生
        /// <summary>
        /// 在展开树节点后发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void iTreeView_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (null != e.Node)
            {
                //加载节点
                LoadSubNodesByPath(e.Node, e.Node.Name);
                //通知路径变更
                RaisePathChanged(this, SelectPath); 
            }
        }
        #endregion

        #region 开始修改名字
        /// <summary>
        /// 开始修改名字
        /// </summary>
        void ItemBeginEdit()
        {
            if (this.SelectedNode != null)
            {
                SelectedNode.BeginEdit();
            }
        }
        #endregion

        #region 强制刷新指定节点
        /// <summary>
        /// 强制刷新指定节点
        /// </summary>
        void RefreshTree()
        {
            if (this.SelectedNode != null)
            {
                LoadSubNodesByPath(SelectedNode, SelectedNode.Name, true);
            }
        }
        #endregion

        #region 拷贝路径
        /// <summary>
        /// 拷贝路径
        /// </summary>
        void CopyPath()
        {
            if (this.SelectedNode != null)
            {
                Clipboard.SetDataObject(SelectedNode.Name);
            }
        }
        #endregion

        #region 新建文件夹
        /// <summary>
        /// 新建文件夹
        /// </summary>
        void NewFolder()
        {
            if (this.SelectedNode != null)
            {
                if (m_FileDevice.DeviceType == DeviceTypeOption.LocalDisk && Utility.IsDriver(this.SelectedNode.Name))
                {
                    return;
                }
                string baseName = "new Folder";
                string folderName = SelectedNode.Name + baseName;
                int i = 0;
                while (m_FileDevice.DirectoryExists(folderName))
                {
                    folderName = SelectedNode.Name + baseName + string.Format(" ({0})", ++i);
                }
                if (m_FileDevice.CreateDirectory(folderName))
                {
                    TreeNode node = CreateEmtyNode(Path.GetFileName(folderName));
                    node.Name = folderName + m_FileDevice.DirectorySeparatorChar;
                    this.SelectedNode.Nodes.Add(node);
                    SelectNodeByName(node.Name,true);

                    node.BeginEdit();
                }
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
            TreeNode selectNode = this.SelectedNode;
            if (selectNode != null && selectNode.Parent != null)
            {
                if (MessageHelper.ShowConfirm("Are you sure you want to delete the selected folder ?") != DialogResult.OK)
                {
                    return;
                }

                if (m_FileDevice.DeviceType == DeviceTypeOption.LocalDisk && Utility.IsDriver(this.SelectedNode.Name))
                {
                    return;
                }
                TreeNode prevNode = selectNode.PrevNode;
                m_FileDevice.DeleteDirectory(SelectedNode.Name);

                if (null == prevNode)
                {
                    prevNode = selectNode.Parent;
                    LoadSubNodesByPath(prevNode, prevNode.Name, true);
                }
                else
                {
                    selectNode.Parent.Nodes.RemoveByKey(selectNode.Name);
                    this.SelectedNode = prevNode;
                }
            }
        }
        #endregion

        #region 加载节点
        /// <summary>
        /// 加载节点
        /// </summary>
        /// <param name="startNode"></param>
        /// <param name="path"></param>
        void LoadSubNodesByPath(TreeNode startNode, string path)
        {
            LoadSubNodesByPath(startNode, path, false);
        }
        /// <summary>
        /// 加载节点
        /// </summary>
        /// <param name="startNode"></param>
        /// <param name="path"></param>
        /// <param name="forceRefresh"></param>
        void LoadSubNodesByPath(TreeNode startNode, string path, bool forceRefresh)
        {
            if (SelectedNode != startNode)
            {
                this.SelectedNode = startNode;
            }

            if (startNode.Tag != null && startNode.Tag.Equals(true) && !forceRefresh)
            {
                return;
            }

            startNode.Tag = true;//设置已经绑定

            //this.BeginUpdate();
            startNode.Nodes.Clear();
            Application.DoEvents();
            foreach (string fullname in m_FileDevice.GetDirectories(path))
            {
                if (m_FileDevice.DeviceType == DeviceTypeOption.LocalDisk)
                {
                    int imginx = IconHelper.GetImageIndex(fullname);

                    TreeNode node = startNode.Nodes.Add(
                        fullname, 
                        m_FileDevice.GetFolderName(fullname),
                        imginx,
                        imginx//选中状态和未选中一样
                        );

                }
                else  if (m_FileDevice.DeviceType == DeviceTypeOption.iPhone)
                {
                    TreeNode node = startNode.Nodes.Add(
                        fullname,
                        m_FileDevice.GetFolderName(fullname),
                        (int)iPhoneIconOption.CloseFolder,
                        (int)iPhoneIconOption.OpenFolder
                        );
                }
                
                Application.DoEvents();
            }

            for (int i = 0; i < startNode.Nodes.Count;i++ )
            {
                TreeNode node = startNode.Nodes[i];

                if (m_FileDevice.HasDirectories(node.Name))
                {
                    node.Nodes.Add(CreateEmtyNode());
                }

                Application.DoEvents();
            }
            startNode.Expand();
            //this.EndUpdate();
        }
        #endregion

        #region 查找指定名称的节点
        /// <summary>
        /// 查找指定名称的节点
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="searchAllChildren"></param>
        /// <returns></returns>
        TreeNode GetNodeByName(string nodeName, bool searchAllChildren)
		{
			TreeNode[] tnFound = this.Nodes.Find(nodeName, searchAllChildren);

			if (tnFound.Length > 0)
				return tnFound[0];
			return null;
		}

        /// <summary>
        /// 从指定的节点开始查找指定名称的节点
        /// </summary>
        /// <param name="tnStart"></param>
        /// <param name="nodeName"></param>
        /// <param name="searchAllChildren"></param>
        /// <returns></returns>
        TreeNode GetNodeByName(TreeNode tnStart, string nodeName, bool searchAllChildren)
		{
			if (tnStart != null)
			{
				tnStart.Collapse();
				tnStart.Expand();

				TreeNode[] tnFound = tnStart.Nodes.Find(nodeName, searchAllChildren);

				if (tnFound.Length > 0)
					return tnFound[0];
			}
			return null;
        }
        #endregion

        #region 设置指定节点为选中节点
        /// <summary>
        /// 设置指定节点为选中节点
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="isFilePath"></param>
        /// <param name="searchAllChildren"></param>
        /// <returns></returns>
        TreeNode SelectNodeByName(string nodeName,bool searchAllChildren)
		{
			if (nodeName == "")
				return null;

			this.BeginUpdate();

			TreeNode tnFind = null;


			TreeNode[] tnArray = this.Nodes.Find(nodeName, true);
            if (tnArray.Length > 0)
            {
                tnFind = tnArray[0];
            }

			if (tnFind != null)
			{
				this.SelectedNode = tnFind;
				tnFind.Collapse();
				tnFind.Expand();
			}
			else
            {
                string searchPath = string.Empty;

                string[] nameArray = nodeName.Split(m_DirectorySeparatorChar);

				foreach (string current in nameArray)
				{
					if (current != "")
					{
                        searchPath = Utility.CombinePath(searchPath, current, m_DirectorySeparatorChar) + m_DirectorySeparatorChar;

						tnFind = (TreeNode)GetNodeByName(searchPath, true);
						if (tnFind != null)
						{
							this.SelectedNode = tnFind;
							tnFind.Collapse();
							tnFind.Expand();
						}
					}
				}
			}

			this.EndUpdate();

			return tnFind;
		}

        /// <summary>
        /// 从指定节点开始设置指定节点为选中节点
        /// </summary>
        /// <param name="tnStart"></param>
        /// <param name="nodeName"></param>
        /// <param name="isFilePath"></param>
        /// <param name="searchAllChildren"></param>
        /// <returns></returns>
        TreeNode SelectNodeByName(TreeNode tnStart, string nodeName, bool isFilePath, bool searchAllChildren)
		{
			if (nodeName == "")
				return null;

			this.BeginUpdate();

			TreeNode tnFind = null;
			string searchPath = "";

			//	Let's see if it already exists
			TreeNode[] tnArray = this.Nodes.Find(nodeName, true);
			if (tnArray.Length > 0)
				tnFind = tnArray[0];

			if (tnFind != null)
			{
				this.SelectedNode = tnFind;
				tnFind.Collapse();
				tnFind.Expand();
			}
			else
			{
				string[] nameArray = nodeName.Split(Path.DirectorySeparatorChar);

				foreach (string current in nameArray)
				{
					if (current != "")
					{
						searchPath = Path.Combine(searchPath, current);
						if (isFilePath && searchPath.Length == 2)
							searchPath += Path.DirectorySeparatorChar.ToString();
						tnFind = (TreeNode)GetNodeByName(tnStart, searchPath, searchAllChildren);
						if (tnFind != null)
						{
							this.SelectedNode = tnFind;
							tnFind.Collapse();
							tnFind.Expand();
						}
					}
				}
			}

			this.EndUpdate();

			return tnFind;
        }

        #endregion

    }
}