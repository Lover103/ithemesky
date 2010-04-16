using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
using Manzana;

namespace iSprite
{
    internal class iSpriteContext
    {
        /// <summary>
        /// 当前软件版本
        /// </summary>
        internal string CurrentVersion { private set; get; }

        private static iSpriteContext _context;

        /// <summary>
        /// 获取或设置当前应用程序数据路径
        /// </summary>
        internal string iSpriteApplicationDataPath { private set; get; }

        /// <summary>
        /// 获取或设置当前应用程序临时文件夹路径
        /// </summary>
        internal string iSpriteTempPath { private set; get; }
        /// <summary>
        /// iPhone图库路径
        /// </summary>
        internal string iPhone_PhotosLibrary_Path { private set; get; }
        /// <summary>
        /// iPhone壁纸路径
        /// </summary>
        internal string iPhone_Wallpaper_Path { private set; get; }
        /// <summary>
        /// iPhone当前壁纸路径
        /// </summary>
        internal string iPhone_LockBackground_Path { private set; get; }
        /// <summary>
        /// iPhone图标排序文件路径
        /// </summary>
        internal string iPhone_DisplayOrder_Path { private set; get; }
        /// <summary>
        /// iPhone我的文档路径
        /// </summary>
        internal string iPhone_MyDocuments_Path { private set; get; }
        /// <summary>
        /// iPhone主题程序安装路径
        /// </summary>
        internal string iPhone_WinterBoardApp_Path { private set; get; }
        /// <summary>
        /// iPhone主题文件保存路径
        /// </summary>
        internal string iPhone_WinterBoardFile_Path { private set; get; }
        /// <summary>
        /// iPhone主题配置文件保存路径
        /// </summary>
        internal string iPhone_WinterBoardSetting_Path { private set; get; }
        /// <summary>
        /// iPhone程序名称语言包路径
        /// </summary>
        internal string iPhone_LocalizedApplicationNames_Path { private set; get; }
        /// <summary>
        /// 屏幕截图
        /// </summary>
        internal string iPhone_ScreenShot_Path { private set; get; }

        /// <summary>
        /// iPhone GlobalPreferences路径
        /// </summary>
        internal string iPhone_GlobalPreferences_Path { private set; get; }

        /// <summary>
        /// 主题入口
        /// </summary>
        internal string ThemeHomePage { private set; get; }
        /// <summary>
        /// 帮助
        /// </summary>
        internal string HelpUrl { private set; get; }
        /// <summary>
        /// 更新地址
        /// </summary>
        internal string UpdateUrl { private set; get; }
        /// <summary>
        /// WinterBoard
        /// </summary>
        internal string WinterBoardXML { private set; get; }
        /// <summary>
        /// CydiaAutoInstallPath
        /// </summary>
        internal string iPhone_CydiaAutoInstallPath { private set; get; }/// <summary>
        /// iPhone_InstallationPath
        /// </summary>
        internal string iPhone_InstallationPath { private set; get; }

        /// <summary>
        /// 静态构造器
        /// </summary>
        static iSpriteContext()
        {
            _context = new iSpriteContext();
        }
        /// <summary>
        /// 私有构造器（禁止从外部实例化）
        /// </summary>
        private iSpriteContext()
        {
            Reload();
        }

        /// <summary>
        /// 加载实例
        /// </summary>
        public void Reload()
        {
            CurrentVersion = "2.0";

            ThemeHomePage = Manzana.Utility.Decrypt("60A99C413E1F93068451A6E02805A1E0A306054C5C2C6E15001DC23B9551C12B");
            HelpUrl = "http://www.ithemesky.com/help/";
            UpdateUrl = "http://update.ithemesky.com/update/update.xml";
            WinterBoardXML = Manzana.Utility.Decrypt("86307D8B09A4E77ADDB7DB85E40A158DDC1507B95EC2AB9193B6DC72D41F9565B73EB7DFAB12C7012632317568B94A3A91CC3CBACC1FFA99");
            iPhone_PhotosLibrary_Path = "/private/var/root/Media/Photos/";
            iPhone_Wallpaper_Path = "/Library/Wallpaper/";
            iPhone_LockBackground_Path = "/private/var/root/Library/";
            iPhone_LockBackground_Path = "/System/Library/CoreServices/SpringBoard.app/DisplayOrder.plist";
            iPhone_MyDocuments_Path = "/private/var/root/Media/My Documents/";
            iPhone_WinterBoardApp_Path = "/Applications/WinterBoard.app/";
            iPhone_WinterBoardSetting_Path = "/private/var/mobile/Library/Preferences/com.saurik.WinterBoard.plist";
            iPhone_LocalizedApplicationNames_Path = "/System/Library/CoreServices/SpringBoard.app/{0}/LocalizedApplicationNames.strings";
            iPhone_ScreenShot_Path = "/private/var/mobile/Media/DCIM/100APPLE/";
            iPhone_GlobalPreferences_Path = "/private/var/root/Library/Preferences/.GlobalPreferences.plist";
            iPhone_CydiaAutoInstallPath = "/private/var/root/Media/Cydia/AutoInstall/";
            iPhone_InstallationPath = "/var/mobile/Library/Caches/com.apple.mobile.installation.plist";

            iSpriteApplicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles).TrimEnd('\\')
                   + @"\iSprite\";

            if (!Directory.Exists(iSpriteApplicationDataPath))
            {
                Directory.CreateDirectory(iSpriteApplicationDataPath);
            }

            iSpriteTempPath = Path.GetTempPath().TrimEnd('\\')
                   + @"\iSprite\";

            if (!Directory.Exists(iSpriteTempPath))
            {
                Directory.CreateDirectory(iSpriteTempPath);
            }
            else
            {
                //清除文件和文件夹
                try
                {
                    foreach (string name in Directory.GetFiles(iSpriteTempPath))
                    {
                        File.Delete(name);
                    }

                    foreach (string name in Directory.GetDirectories(iSpriteTempPath))
                    {
                        Directory.Delete(name, true);
                    }
                }
                catch
                { 
                }
            }
        }

        /// <summary>
        /// 获取当前的应用程序配置唯一实例
        /// </summary>
        public static iSpriteContext Current
        {
            get { return _context; }
        }

        /// <summary>
        /// AfterDeviceFinishConnected
        /// </summary>
        /// <param name="ver"></param>
        public void AfterDeviceFinishConnected(iPhone iphone)
        {
            if (!iphone.IsConnected)
            {
                _context.ThemeHomePage = "http://www.ithemesky.com/";
                return;
            }

            Reload();

            string ver = iphone.DeviceVersion;
            ver = ver.Trim();
            _context.ThemeHomePage += "?chl=iSprite&phonever=" + ver
                + "&softver=" + _context.CurrentVersion
                + "&iTunesVer=" + iphone.iTunesVer
                + "&deviceid=" + iphone.DeviceId;

            if (ver.Split('.').Length < 3)
            {
                ver = ver + ".0";
            }
            if (Convert.ToInt32(ver.Replace(".", "")) >= 113)
            {
                //如果 iPhone 版本号大于等于 1.1.3 ，地址要改为 mobile 下
                _context.iPhone_PhotosLibrary_Path = _context.iPhone_PhotosLibrary_Path.Replace("/root/", "/mobile/");
                _context.iPhone_LockBackground_Path = _context.iPhone_LockBackground_Path.Replace("/root/", "/mobile/");
                _context.iPhone_MyDocuments_Path = _context.iPhone_MyDocuments_Path.Replace("/root/", "/mobile/");
                _context.iPhone_GlobalPreferences_Path = _context.iPhone_GlobalPreferences_Path.Replace("/root/", "/mobile/");

            }
            if (!iphone.Exists(_context.iPhone_MyDocuments_Path))
            {
                iphone.CreateDirectory(_context.iPhone_MyDocuments_Path);
            }
            if (!iphone.Exists(_context.iPhone_CydiaAutoInstallPath))
            {
                iphone.CreateDirectory(_context.iPhone_CydiaAutoInstallPath);
            }

            //foreach (string path in iphone.GetDirectories("/private/var/stash/"))
            //{
            //    if (path.StartsWith("Themes."))
            //    {
            //        _context.iPhone_WinterBoardFile_Path = "/private/var/stash/" + path + "/";
            //        break;
            //    }
            //}
            _context.iPhone_WinterBoardFile_Path = "/Library/Themes/";


        }
    }
}
