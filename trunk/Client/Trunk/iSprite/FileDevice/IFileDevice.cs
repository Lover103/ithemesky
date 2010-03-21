using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Manzana;

namespace iSprite
{
    /// <summary>
    /// 设备操作接口
    /// </summary>
    public interface  IFileDevice
    {
        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        bool CreateDirectory(string path);
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path"></param>
        void DeleteFile(string path);
        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="path"></param>
        void DeleteDirectory(string path);
        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="path"></param>
        /// <param name="recursive"></param>
        void DeleteDirectory(string path, bool recursive);
        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        bool FileExists(string path);
        /// <summary>
        /// 检查文件夹是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        bool DirectoryExists(string path);
        /// <summary>
        /// 获取文件夹
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        List<string> GetDirectories(string path);
        /// <summary>
        /// 获取文件夹
        /// </summary>
        /// <param name="path"></param>
        /// <param name="searchPattern"></param>
        /// <returns></returns>
        List<string> GetDirectories(string path, string searchPattern);
        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        List<string> GetFiles(string path);
        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="searchPattern"></param>
        /// <returns></returns>
        List<string> GetFiles(string path, string searchPattern);
        /// <summary>
        /// 获取父路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        string GetParent(string path);
        /// <summary>
        /// 获取当前设备是否连接就绪
        /// </summary>
        bool IsConnected { get; }
        /// <summary>
        /// 获取设备名称
        /// </summary>
        string DeviceName { get; }
        /// <summary>
        /// 获取设备类型
        /// </summary>
        DeviceTypeOption DeviceType { get; }
        /// <summary>
        /// 获取设备的起始路径
        /// </summary>
        string StartPath { get; }
        /// <summary>
        /// 检查目录是否包含子目录
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        bool HasDirectories(string path);
        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        long GetFileSize(string path);
        /// <summary>
        /// 获取指定目录下的文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        List<iFileInfo> GetFileInfos(string path);
        /// <summary>
        /// 获取路径分隔符
        /// </summary>
        char DirectorySeparatorChar { get; }
        /// <summary>
        /// 获取文件夹名称
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        string GetFolderName(string path);
        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="path"></param>
        void ShowFile(string path);
        /// <summary>
        /// 文件重命名
        /// </summary>
        /// <param name="oldname"></param>
        /// <param name="newname"></param>
        void ReNameFile(string oldname, string newname);
        /// <summary>
        /// 文件夹重命名
        /// </summary>
        /// <param name="oldname"></param>
        /// <param name="newname"></param>
        void ReNameFolder(string oldname, string newname);
        /// <summary>
        /// 文件传输完成事件
        /// </summary>
        event FileCompletedHandler OnCompleteHandler;
        /// <summary>
        /// 文件传输进度事件
        /// </summary>
        event FileProgressHandler OnProgressHandler;
    }
}
