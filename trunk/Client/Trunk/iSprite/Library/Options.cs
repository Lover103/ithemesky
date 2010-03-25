using System;
using System.Collections.Generic;
using System.Text;

namespace iSprite
{
    #region 视图行类型
    /// <summary>
    /// 主窗体显示模式
    /// </summary>
    public enum ViewModeOption
    {
        /// <summary>
        /// 垂直
        /// </summary>
        DUALVERTICAL,
        /// <summary>
        /// 水平
        /// </summary>
        DUALHORIZONTAL,
        /// <summary>
        /// 单列
        /// </summary>
        SINGLE,
    }
    #endregion

    #region 视图行类型
    /// <summary>
    /// 视图行类型
    /// </summary>
    public enum ListViewItemTypeOption
    {
        /// <summary>
        /// 文件
        /// </summary>
        File = 0,
        /// <summary>
        /// 文件夹
        /// </summary>
        Folder = 2
    }
    #endregion

    #region 按钮动作选项
    /// <summary>
    /// 按钮动作选项
    /// </summary>
    public enum ButtonCommandOption
    {
        /// <summary>
        /// 新建文件夹
        /// </summary>
        NewFloder,
        /// <summary>
        /// 刷新
        /// </summary>
        Refresh,
        /// <summary>
        /// 删除
        /// </summary>
        Delete,
        /// <summary>
        /// 拷贝路径
        /// </summary>
        CopyPath,
        /// <summary>
        /// 全选
        /// </summary>
        SelectAll,
        /// <summary>
        /// 取消选中
        /// </summary>
        UnSelectAll,
        /// <summary>
        /// 编辑节点
        /// </summary>
        EditName
    }
    #endregion

    #region 消息类型
    /// <summary>
    /// 消息类型
    /// </summary>
    public enum MessageTypeOption
    { 
        /// <summary>
        /// 提示信息
        /// </summary>
        Info,
        /// <summary>
        /// 错误
        /// </summary>
        Error,
        /// <summary>
        /// 警告
        /// </summary>
        Warning,
        /// <summary>
        /// 重新加载收藏夹
        /// </summary>
        ReloadLoadFavorites,
        /// <summary>
        /// 加入收藏夹
        /// </summary>
        AddtoFavorites,
        /// <summary>
        /// 更新
        /// </summary>
        Upgrade
    }
    #endregion

    #region iPhone文件类型
    /// <summary>
    /// iPhone文件类型
    /// </summary>
    public enum iPhoneFileTypeOption
    {
        FILE_UNKNOWN = 0,
        FILE_MUSIC = 1,
        FILE_MOVIE = 2,
        FILE_TEXT = 3,
        FILE_IMAGE = 4,
        FILE_AUDIO = 5,
        FILE_DATABASE = 6,
        FILE_RINGTONE = 7,
        FILE_PList = 8,
        IMAGE_FOLDER_CLOSED = 9,
        FOLDER_OPEN = 10
    }
    #endregion

    #region iPhone图标类型
    /// <summary>
    /// iPhone图标类型
    /// </summary>
    public enum iPhoneIconOption 
    {
        iPhone = 0,
        CloseFolder = 1,
        OpenFolder = 2,
        File=3
    }
    #endregion

    #region 视图排序类型
    /// <summary>
    /// 视图排序类型
    /// </summary>
    public enum ListViewSortOption
    {
        None,
        Number,
        Text,
        DateTime,
        GroupByInitialLetter,
        GroupByNumber,
        GroupByText,
        GroupByDateTime
    }
    #endregion   

    #region CSIDL
    public enum CSIDL
	{
        /// <summary>
        /// 桌面
        /// </summary>
		CSIDL_DESKTOP = 0x0000,        // <desktop>
		CSIDL_INTERNET = 0x0001,        // Internet Explorer (icon on desktop)
		CSIDL_PROGRAMS = 0x0002,        // Start Menu\Programs
		CSIDL_CONTROLS = 0x0003,        // My Computer\Control Panel
		CSIDL_PRINTERS = 0x0004,        // My Computer\Printers
        /// <summary>
        /// 我的文档
        /// </summary>
		CSIDL_PERSONAL = 0x0005,        // My Documents
		CSIDL_FAVORITES = 0x0006,        // <user name>\Favorites
		CSIDL_STARTUP = 0x0007,        // Start Menu\Programs\Startup
		CSIDL_RECENT = 0x0008,        // <user name>\Recent
		CSIDL_SENDTO = 0x0009,        // <user name>\SendTo
		CSIDL_BITBUCKET = 0x000a,        // <desktop>\Recycle Bin
		CSIDL_STARTMENU = 0x000b,        // <user name>\Start Menu
		CSIDL_MYDOCUMENTS = 0x000c,        // logical "My Documents" desktop icon
		CSIDL_MYMUSIC = 0x000d,        // "My Music" folder
		CSIDL_MYVIDEO = 0x000e,        // "My Videos" folder
		CSIDL_DESKTOPDIRECTORY = 0x0010,        // <user name>\Desktop
        /// <summary>
        /// 我的电脑
        /// </summary>
		CSIDL_DRIVES = 0x0011,        // My Computer
		CSIDL_NETWORK = 0x0012,        // Network Neighborhood (My Network Places)
		CSIDL_NETHOOD = 0x0013,        // <user name>\nethood
		CSIDL_FONTS = 0x0014,        // windows\fonts
		CSIDL_TEMPLATES = 0x0015,
		CSIDL_COMMON_STARTMENU = 0x0016,        // All Users\Start Menu
		CSIDL_COMMON_PROGRAMS = 0x0017,        // All Users\Start Menu\Programs
		CSIDL_COMMON_STARTUP = 0x0018,        // All Users\Startup
		CSIDL_COMMON_DESKTOPDIRECTORY = 0x0019,        // All Users\Desktop
		CSIDL_APPDATA = 0x001a,        // <user name>\Application Data
		CSIDL_PRINTHOOD = 0x001b,        // <user name>\PrintHood
		CSIDL_LOCAL_APPDATA = 0x001c,        // <user name>\Local Settings\Applicaiton Data (non roaming)
		CSIDL_ALTSTARTUP = 0x001d,        // non localized startup
		CSIDL_COMMON_ALTSTARTUP = 0x001e,        // non localized common startup
		CSIDL_COMMON_FAVORITES = 0x001f,
		CSIDL_INTERNET_CACHE = 0x0020,
		CSIDL_COOKIES = 0x0021,
		CSIDL_HISTORY = 0x0022,
		CSIDL_COMMON_APPDATA = 0x0023,        // All Users\Application Data
		CSIDL_WINDOWS = 0x0024,        // GetWindowsDirectory()
		CSIDL_SYSTEM = 0x0025,        // GetSystemDirectory()
		CSIDL_PROGRAM_FILES = 0x0026,        // C:\Program Files
		CSIDL_MYPICTURES = 0x0027,        // C:\Program Files\My Pictures
		CSIDL_PROFILE = 0x0028,        // USERPROFILE
		CSIDL_SYSTEMX86 = 0x0029,        // x86 system directory on RISC
		CSIDL_PROGRAM_FILESX86 = 0x002a,        // x86 C:\Program Files on RISC
		CSIDL_PROGRAM_FILES_COMMON = 0x002b,        // C:\Program Files\Common
		CSIDL_PROGRAM_FILES_COMMONX86 = 0x002c,        // x86 Program Files\Common on RISC
		CSIDL_COMMON_TEMPLATES = 0x002d,        // All Users\Templates
		CSIDL_COMMON_DOCUMENTS = 0x002e,        // All Users\Documents
		CSIDL_COMMON_ADMINTOOLS = 0x002f,        // All Users\Start Menu\Programs\Administrative Tools
		CSIDL_ADMINTOOLS = 0x0030,        // <user name>\Start Menu\Programs\Administrative Tools
		CSIDL_CONNECTIONS = 0x0031,        // Network and Dial-up Connections
		CSIDL_COMMON_MUSIC = 0x0035,        // All Users\My Music
		CSIDL_COMMON_PICTURES = 0x0036,        // All Users\My Pictures
		CSIDL_COMMON_VIDEO = 0x0037,        // All Users\My Video
		CSIDL_RESOURCES = 0x0038,        // Resource Direcotry
		CSIDL_RESOURCES_LOCALIZED = 0x0039,        // Localized Resource Direcotry
		CSIDL_COMMON_OEM_LINKS = 0x003a,        // Links to All Users OEM specific apps
		CSIDL_CDBURN_AREA = 0x003b,        // USERPROFILE\Local Settings\Application Data\Microsoft\CD Burning
		// unused                               = 0x003c
		CSIDL_COMPUTERSNEARME = 0x003d,        // Computers Near Me (computered from Workgroup membership)
		CSIDL_FLAG_CREATE = 0x8000,        // combine with CSIDL_ value to force folder creation in SHGetFolderPath()
		CSIDL_FLAG_DONT_VERIFY = 0x4000,        // combine with CSIDL_ value to return an unverified folder path
		CSIDL_FLAG_NO_ALIAS = 0x1000,        // combine with CSIDL_ value to insure non-alias versions of the pidl
		CSIDL_FLAG_PER_USER_INIT = 0x0800,        // combine with CSIDL_ value to indicate per-user init (eg. upgrade)
		CSIDL_FLAG_MASK = 0xFF00        // mask for all possible flag values
    }
    #endregion

    #region FileOps
    [Flags]
	public enum FileOps
	{
		WM_USER = 0x0400,
		FO_MOVE = 0x0001,
		FO_COPY = 0x0002,
		FO_DELETE = 0x0003,
		FO_RENAME = 0x0004,
		FO_JSHORTCUT = (WM_USER + 1),
		FOF_MULTIDESTFILES = 0x0001,
		FOF_CONFIRMMOUSE = 0x0002,
		FOF_SILENT = 0x0004,  // don't create progress/report
		FOF_RENAMEONCOLLISION = 0x0008,
		FOF_NOCONFIRMATION = 0x0010,  // Don't prompt the user.
		FOF_WANTMAPPINGHANDLE = 0x0020,  // Fill in SHFILEOPSTRUCT.hNameMappings - Must be freed using SHFreeNameMappings
		FOF_ALLOWUNDO = 0x0040,
		FOF_FILESONLY = 0x0080,  // on *.*, do only files
		FOF_SIMPLEPROGRESS = 0x0100,  // means don't show names of files
		FOF_NOCONFIRMMKDIR = 0x0200,  // don't confirm making any needed dirs
		FOF_NOERRORUI = 0x0400,  // don't put up errorString UI
		FOF_NOCOPYSECURITYATTRIBS = 0x0800,  // dont copy NT file Security Attributes
		FOF_NORECURSION = 0x1000,  // don't recurse into directories.
		FOF_NO_CONNECTED_ELEMENTS = 0x2000,  // don't operate on connected elements.
		FOF_WANTNUKEWARNING = 0x4000,  // during delete operation, warn if nuking instead of recycling (partially overrides FOF_NOCONFIRMATION)
		FOF_NORECURSEREPARSE = 0x8000  // treat reparse points as objects, not containers
	}
    #endregion        

    #region SHGFI
    [Flags]
    public enum SHGFI : uint
    {
        ADDOVERLAYS = 0x20,
        ATTR_SPECIFIED = 0x20000,
        /// <summary>
        /// 获得属性
        /// </summary>
        ATTRIBUTES = 0x800,
        /// <summary>
        /// 获得显示名
        /// </summary>
        DISPLAYNAME = 0x200,
        EXETYPE = 0x2000,
        /// <summary>
        /// 获得图标
        /// </summary>
        ICON = 0x100,
        ICONLOCATION = 0x1000,
        /// <summary>
        /// 获得大图标
        /// </summary>
        LARGEICON = 0,
        LINKOVERLAY = 0x8000,
        OPENICON = 2,
        OVERLAYINDEX = 0x40,
        PIDL = 8,
        SELECTED = 0x10000,
        SHELLICONSIZE = 4,
        /// <summary>
        /// 获得小图标
        /// </summary>
        SMALLICON = 1,
        SYSICONINDEX = 0x4000,
        /// <summary>
        /// 获得类型名
        /// </summary>
        TYPENAME = 0x400,
        USEFILEATTRIBUTES = 0x10
    }
    #endregion

    #region FILE_ATTRIBUTE
    [Flags]
    public enum FILE_ATTRIBUTE
    {
        READONLY = 0x00000001,
        HIDDEN = 0x00000002,
        SYSTEM = 0x00000004,
        DIRECTORY = 0x00000010,
        ARCHIVE = 0x00000020,
        DEVICE = 0x00000040,
        NORMAL = 0x00000080,
        TEMPORARY = 0x00000100,
        SPARSE_FILE = 0x00000200,
        REPARSE_POINT = 0x00000400,
        COMPRESSED = 0x00000800,
        OFFLINE = 0x00001000,
        NOT_CONTENT_INDEXED = 0x00002000,
        ENCRYPTED = 0x00004000
    }
    #endregion
}
