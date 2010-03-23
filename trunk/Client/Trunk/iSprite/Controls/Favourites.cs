using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace iSprite
{
	internal class Favourites
    {
        #region 变量定义
        IFileDevice m_FileDevice;
		private ContextMenuStrip m_ContextMenuStrip;
        private string m_FavCfgPath;
        public event PathChanged OnPathChanged;
        internal event MessageHandler OnMessage;
        const string m_FavMaintainString = "Maintain Favourites";
        const string m_Add2FavouritesString = "Add to Favourites";
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
        internal Favourites(IFileDevice filedevice, ContextMenuStrip ctxFavourites)
        {
            m_FileDevice = filedevice;
			this.m_ContextMenuStrip = ctxFavourites;
        }
        private Favourites()
        { 
        }
        #endregion

        #region 收藏夹路径
        /// <summary>
        /// 收藏夹路径
        /// </summary>
        /// <returns></returns>
        private string GetFavPath(string favID)
        {
            string path = Path.Combine(iSpriteContext.Current.iSpriteApplicationDataPath, favID + "-favs.xml");
            
            return path;
        }
        #endregion

        #region 创建新项
        /// <summary>
        /// 创建新项
        /// </summary>
        /// <param name="text"></param>
        /// <param name="fullName"></param>
        /// <param name="showFullPath"></param>
        /// <returns></returns>
        private ToolStripMenuItem CreateItem(string text,string fullName, bool showFullPath)
		{
            if (text==string.Empty)
            {
                if (!showFullPath)
                {
                    text = Path.GetFileName(fullName.TrimEnd(m_FileDevice.DirectorySeparatorChar));
                }
            }

            ToolStripMenuItem item = new ToolStripMenuItem(text);
            item.Tag = fullName;
            item.Click += new EventHandler(FavItem_Click);

            return item;
        }
        #endregion

        #region 点击下拉按钮事件处理
        /// <summary>
        /// 点击下拉按钮事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FavItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item.Text == m_FavMaintainString && item.Tag.ToString() == "system")
            {
                MaintainFavourites();
            }
            else if (item.Text == m_Add2FavouritesString && item.Tag.ToString() == "system")
            {
                RaiseMessageHandler(this, string.Empty, MessageTypeOption.AddtoFavorites);
            }
            else
            {
                RaisePathChanged(this, item.Tag.ToString());
            }
        }
        #endregion

        #region 加载收藏夹
        /// <summary>
        /// 加载收藏夹
        /// </summary>
        private void LoadFavourites()
		{
            Dictionary<string, string> listDic = new Dictionary<string, string>();

			DataSet dsTemp;
			DataTable dtTemp;

			dsTemp = new DataSet();
			try
			{
                if (File.Exists(m_FavCfgPath))
                {
                    dsTemp.ReadXml(m_FavCfgPath);
                    dtTemp = dsTemp.Tables["FavPaths"];

                    foreach (DataRow row in dtTemp.Rows)
                    {
                        listDic.Add(row["Key"].ToString(),row["Path"].ToString());
                    }
                }
			}
			catch
			{
			}
			dsTemp.Dispose();

            ShowMenu(listDic);
		}

        private void ShowMenu(Dictionary<string, string> dicPaths)
        {
            this.m_ContextMenuStrip.Items.Clear();

            ToolStripMenuItem item;

            //	Add to Favourites
            this.m_ContextMenuStrip.Items.Add(new ToolStripSeparator());
            item = new ToolStripMenuItem(m_Add2FavouritesString);
            item.Tag = "system";
            item.Click += new EventHandler(FavItem_Click);
            this.m_ContextMenuStrip.Items.Add(item);

            //	Maintenance
            this.m_ContextMenuStrip.Items.Add(new ToolStripSeparator());
            item = new ToolStripMenuItem(m_FavMaintainString);
            item.Tag = "system";
            item.Click += new EventHandler(FavItem_Click);
            this.m_ContextMenuStrip.Items.Add(item);

            this.m_ContextMenuStrip.Items.Add(new ToolStripSeparator());

            #region 添加系统收藏夹
            Dictionary<string, string> SysFavPaths = new Dictionary<string, string>();
            if (m_FileDevice.DeviceType == DeviceTypeOption.LocalDisk)
            {
                #region 本地
                SysFavPaths.Add(
                    "Desktop",
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                    );

                SysFavPaths.Add(
                    "My Documents",
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                    );
                SysFavPaths.Add(
                    "My Music",
                    Environment.GetFolderPath(Environment.SpecialFolder.MyMusic)
                    );
                SysFavPaths.Add(
                    "My Pictures",
                    Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
                    );
                #endregion
            }
            else if (m_FileDevice.DeviceType == DeviceTypeOption.iPhone)
            {
                #region iPhone
                SysFavPaths.Add(
                      "My Documents",
                      iSpriteContext.Current.iPhone_MyDocuments_Path
                      );

                SysFavPaths.Add(
                      "WinterBoard",
                      iSpriteContext.Current.iPhone_WinterBoardFile_Path
                      );

                SysFavPaths.Add(
                      "Picture / Screen Shot",
                      iSpriteContext.Current.iPhone_ScreenShot_Path
                      );

                SysFavPaths.Add(
                      "Wallpaper",
                      iSpriteContext.Current.iPhone_Wallpaper_Path
                      );
                #endregion
            }

            foreach (KeyValuePair<string, string> current in SysFavPaths)
            {
                item = CreateItem(current.Key, current.Value, true);
                item.Tag = "system";
                if (item != null)
                {
                    this.m_ContextMenuStrip.Items.Add(item);
                }
            }

            #endregion

            if (dicPaths.Count > 0)
            {
                this.m_ContextMenuStrip.Items.Add(new ToolStripSeparator());
            }

            foreach (KeyValuePair<string, string> current in dicPaths)
            {
                item = CreateItem(current.Key, current.Value, true);
                item.Tag = "user";
                if (item != null)
                {
                    this.m_ContextMenuStrip.Items.Add(item);
                }
            }
        }
        #endregion

        #region 保存收藏夹
        /// <summary>
        /// 保存收藏夹
        /// </summary>
        /// <param name="listFavourites"></param>
        private void SaveFavourites(Dictionary<string, string> listFavourites)
		{
			DataSet ds = new DataSet("Favourites");
			DataTable dt = new DataTable("FavPaths");
            dt.Columns.Add(new DataColumn("Key"));
            dt.Columns.Add(new DataColumn("Path"));

			ds.Tables.Add(dt);
			DataRow newRow;
			foreach (KeyValuePair<string, string> current in listFavourites)
			{
				newRow = dt.NewRow();
                newRow["Key"] = current.Key;
                newRow["Path"] = current.Value;
				dt.Rows.Add(newRow);
			}
			ds.AcceptChanges();
            ds.WriteXml(m_FavCfgPath);
			ds.Dispose();
        }
        private void SaveFavourites()
        {
            Dictionary<string, string> listFavourites = GetMenuItems(this.m_ContextMenuStrip);
            SaveFavourites(listFavourites);
        }
        private Dictionary<string, string> GetMenuItems(ContextMenuStrip ctxStrip)
        {
            Dictionary<string, string> dicPaths = new Dictionary<string, string>();

            foreach (ToolStripItem item in ctxStrip.Items)
            {
                if (item is ToolStripMenuItem)
                {
                    if (item.Tag.ToString() != "system")
                    {
                        dicPaths.Add(item.Text, item.Name);
                    }
                }
            }
            return dicPaths;
        }
        #endregion		
        
        #region 判断收藏夹指定key是否存在
        /// <summary>
        /// 判断收藏夹指定key是否存在
        /// </summary>
        /// <param name="pathIn"></param>
        /// <returns></returns>
        private bool IsContains(string pathIn)
		{
            Dictionary<string, string> dicPaths = GetMenuItems(this.m_ContextMenuStrip);
			return dicPaths.ContainsKey(pathIn);
        }
        #endregion

        #region 维护收藏夹
        internal void MaintainFavourites()
        {
        }
        #endregion

        #region 加载收藏夹
        /// <summary>
        /// 加载收藏夹
        /// </summary>
        internal void Load(string favID)
        {
            m_FavCfgPath = GetFavPath(favID);

			LoadFavourites();
        }
        #endregion

        #region 保存收藏夹
        /// <summary>
        /// 保存收藏夹
        /// </summary>
		internal void Save()
		{
			SaveFavourites();
        }
        #endregion

        #region 添加到收藏夹
        /// <summary>
        /// 添加到收藏夹
        /// </summary>
        /// <param name="name"></param>
        /// <param name="newPath"></param>
        internal void Add(string name, string newPath)
        {
            if (name == string.Empty)
            {
                name = newPath;
            }
            if (!IsContains(name))
            {
                Dictionary<string, string> dicPaths = GetMenuItems(this.m_ContextMenuStrip);
                dicPaths.Add(name, newPath);
                ShowMenu(dicPaths);
                SaveFavourites(dicPaths);
            }
        }
        #endregion
    }
}
