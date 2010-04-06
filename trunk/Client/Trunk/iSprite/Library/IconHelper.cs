using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.IO;

namespace iSprite
{
    /// <summary>
    /// IconHelper
    /// </summary>
    public class IconHelper
    {
        #region DLLIMPORT
        // Retrieves information about an object in the file system,
        // such as a file, a folder, a directory, or a drive root.
        [DllImport("shell32",
          EntryPoint = "SHGetFileInfo",
          ExactSpelling = false,
          CharSet = CharSet.Auto,
          SetLastError = true)]
        private static extern IntPtr SHGetFileInfo(
         string pszPath,      //指定的文件名
         FILE_ATTRIBUTE dwFileAttributes, //文件属性
         ref SHFILEINFO sfi,     //返回获得的文件信息,是一个记录类型
         int cbFileInfo,      //文件的类型名
         SHGFI uFlags);

        //	SHGetFileInfo
        [DllImport("shell32.dll", EntryPoint = "SHGetFileInfo", SetLastError = true, CharSet = CharSet.Auto)]
        static extern IntPtr SHGetFileInfo(IntPtr pszPath, int dwFileAttributes, ref SHFILEINFO psfi, int cbFileInfo, uint flags);

        //	SHGetFolderLocation
        [DllImport("shell32.dll", EntryPoint = "SHGetFolderLocation", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int SHGetFolderLocation(IntPtr hwndOwner, int nFolder, IntPtr hToken, int dwReserved, ref IntPtr ppidl);

        //	DestroyIcon
        [DllImport("user32.dll", EntryPoint = "DestroyIcon", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool DestroyIcon(IntPtr hIcon);

        #region STRUCTS
        // Contains information about a file object
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct SHFILEINFO
        {
            public IntPtr hIcon;    //文件的图标句柄
            public IntPtr iIcon;    //图标的系统索引号
            public uint dwAttributes;   //文件的属性值
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;  //文件的显示名
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;   //文件的类型名
        };
        #endregion

        #endregion        

        //保存小图标列表
        static System.Windows.Forms.ImageList smallImageList;
        static Dictionary<string, int> smalldic = new Dictionary<string, int>();
        //保存大图标列表
        static System.Windows.Forms.ImageList largeImageList;
        static IconHelper()
        {
            smallImageList = new System.Windows.Forms.ImageList();
            largeImageList = new System.Windows.Forms.ImageList();
            //将 ImageList中的图标设置为32位色图标，这样可以得到较好的显示效果
            smallImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            largeImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            largeImageList.ImageSize = new Size(32, 32);

            smallImageList.Images.Add(GetIcon("dic", FILE_ATTRIBUTE.DIRECTORY,SHGFI.USEFILEATTRIBUTES | SHGFI.ICON | SHGFI.LARGEICON));
            largeImageList.Images.Add(GetIcon("dic", FILE_ATTRIBUTE.DIRECTORY, SHGFI.USEFILEATTRIBUTES | SHGFI.ICON | SHGFI.LARGEICON));
            smalldic.Add("dic", 0);
        }

        #region 私有方法
        /// <summary>
        /// 获取系统图标
        /// </summary>
        /// <param name="path">文件名</param>
        /// <param name="dwAttr">文件信息</param>
        /// <param name="dwFlag">获取信息控制字</param>
        /// <returns></returns>
        static Icon GetIcon(string path, FILE_ATTRIBUTE dwAttr, SHGFI dwFlag)
        {
            SHFILEINFO fi = new SHFILEINFO();
            Icon ic = null;
            int iTotal = (int)SHGetFileInfo(path, dwAttr, ref fi, 0, dwFlag);
            ic = Icon.FromHandle(fi.hIcon);
            return ic;
        }
        /// <summary>
        /// 向smallInamgeList和largeImageList中
        /// 加入相应文件对应的图标
        /// </summary>
        /// <param name="fileName"></param>
        private static void addFileIcon(string fileName)
        {
            smallImageList.Images.Add(
                GetIcon(fileName, FILE_ATTRIBUTE.NORMAL, SHGFI.USEFILEATTRIBUTES | SHGFI.ICON | SHGFI.SMALLICON)
             );
            largeImageList.Images.Add(
                GetIcon(fileName, FILE_ATTRIBUTE.NORMAL, SHGFI.USEFILEATTRIBUTES | SHGFI.ICON | SHGFI.LARGEICON)
                );
        }
        private static void addFileIcon(SHFILEINFO shfi)
        {
            Icon icTemp = Icon.FromHandle(shfi.hIcon);
            Icon icReturn = (Icon)icTemp.Clone();

            smallImageList.Images.Add(icTemp);
            largeImageList.Images.Add(icTemp);

            DestroyIcon(shfi.hIcon);
        }
        #endregion

        #region 属性
        /// <summary>
        /// 获取系统的小图标列表
        /// </summary>
        public static System.Windows.Forms.ImageList SmallImageList
        {
            get
            {
                return smallImageList;
            }

        }
        /// <summary>
        /// 获取系统的大图标列表
        /// </summary>
        public static System.Windows.Forms.ImageList LargeImageList
        {
            get
            {
                return largeImageList;
            }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 文件对应的图标在ImageList中的索引号
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>索引号</returns>
        public static int GetImageIndex(string fileName)
        {
            int index = 0;
            string extname = Path.GetExtension(fileName);
            if (string.Empty == extname )
            {
                if (fileName.Length <= 3)
                {
                    extname = fileName;
                }
                else if (fileName.EndsWith("\\"))
                {
                    extname = "dic";
                }
            }

            if (smalldic.ContainsKey(extname))
            {
                index = smalldic[extname];
            }
            else
            {
                addFileIcon(extname);
                index = smallImageList.Images.Count - 1;
                smalldic.Add(extname, index);
            }

            return index;
        }
        public static int GetImageIndex(CSIDL icontype)
        {
            int index = -1;
            string key = "FolderIcon-" + (int)icontype;
            if (smalldic.ContainsKey(key))
            {
                index = smalldic[key];
            }
            else
            {
                int dwReserved = 0;
                IntPtr ipDesktop = IntPtr.Zero;
                SHGetFolderLocation(IntPtr.Zero, (int)icontype, IntPtr.Zero, dwReserved, ref ipDesktop);

                SHFILEINFO shfi = new SHFILEINFO();
                uint uFlags = (uint)(SHGFI.PIDL | SHGFI.SYSICONINDEX | SHGFI.ICON | SHGFI.SMALLICON);
                IntPtr ipTemp = SHGetFileInfo(ipDesktop, 0, ref shfi, Marshal.SizeOf(shfi), uFlags);

                addFileIcon(shfi);

                if (ipDesktop != null)
                    Marshal.FreeCoTaskMem(ipDesktop);

                index = smallImageList.Images.Count - 1;
                smalldic.Add(key, index);
            }
            return index;
        }
        #endregion
    }


}
