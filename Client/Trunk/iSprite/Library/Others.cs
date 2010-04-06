using System;
using System.Collections.Generic;

using System.Text;


namespace iSprite
{
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
    public delegate void ThemePriviewMessageHandler(List<string> themeInfo, ThemePriviewMessageTypeOption messagetype);

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
