using System;
using System.Collections.Generic;

using System.Text;
using System.Windows.Forms;
using MyDownloader.Core;


namespace iSprite
{
    public class ThemeInfo
    {
        public string Name = string.Empty;
        public string LocalPath = string.Empty;
        public bool IsExistsIniPhone = false;
    }

    public class DownLoadItemInfo
    {
        //string url, string path, string fileName,InstallState state
        public string Url = string.Empty;
        public string SaveDir = string.Empty;
        public string FileName = string.Empty;
        public InstallState State = InstallState.NoNeedInstall;
        public string Hash = string.Empty;
    }

    /// <summary>
    /// 拷贝中传递的对象
    /// </summary>
    public class Object2Drop
    {
        /// <summary>
        /// 文件操作接口（如iPhone、pc）
        /// </summary>
        public IFileDevice fileDrive { get; set; }

        /// <summary>
        /// 待拷贝的文件/文件夹列表
        /// </summary>
        public List<string> fileList { get; set; }
    }

    /// <summary>
    /// 跨线程调用委托
    /// </summary>
    public delegate void ThreadInvokeDelegate();

    /// <summary>
    /// 路径变更委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="newPath"></param>
    public delegate void PathChanged(object sender, string newPath);
    /// <summary>
    /// 消息传送委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="Message"></param>
    /// <param name="messagetype"></param>
    public delegate void MessageHandler(object sender, string Message, MessageTypeOption messagetype);

    /// <summary>
    /// 消息传送委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="Message"></param>
    /// <param name="messagetype"></param>
    public delegate void ThemePriviewMessageHandler(ThemeInfo themeInfo, ThemePriviewMessageTypeOption messagetype);


    public delegate void SetNodeCountHandler(string nodeName, int count, bool selectNode);
    public delegate void UpdataCatalogCountHandler();

    public delegate void FinishAppActionHandler();
    public delegate void FinishLoadAppDataHandler();

    public delegate void CydiaSourceActionHandler(ListViewItem item, bool finished);
    public delegate void FinishAppHelpActionHandler(int sucessCount);
    public delegate void AddURL2DownloadHandler(string url, string path, string fileName, InstallState state);
    public delegate void UpdataListViewCountHandler(int count);

    /// <summary>
    /// 文件信息
    /// </summary>
    public class iFileInfo
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 完整路径
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public ulong FileSize { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }

    /// <summary>
    /// 设备类型
    /// </summary>
    public enum DeviceTypeOption : long
    {
        /// <summary>
        /// iPhone 
        /// </summary>
        iPhone = 0,
        /// <summary>
        /// 本地磁盘
        /// </summary>
        LocalDisk = 2
    }
}
