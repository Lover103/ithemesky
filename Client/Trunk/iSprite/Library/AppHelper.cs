using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.GZip;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;
using System.Collections.Specialized;

namespace iSprite
{
    /*
     *说明
     * Release文件是对cydia源的一个总体描述，有时候可能内容为空
     * Packages是cydiay源软件列表
    */
    internal class AppHelper
    {
        #region 变量定义
        iPhoneFileDevice m_iPhoneDevice;
        private List<string> ReleaseUrlSuffixList;
        private List<string> PackagesUrlSuffixList;
        string m_CydiaSourceFolder = "/etc/apt/sources.list.d/";
        string m_CydiaPackagesFolder = "/var/lib/apt/lists/";
        string m_AptFolder = string.Empty;
        string m_AptSourceCfg = string.Empty;
        string m_AptPackagesFolder = string.Empty;
        Dictionary<string, RepositoryInfo> m_RepositoryList;
        Dictionary<string, string> m_Repository2PathList;
        internal event MessageHandler OnMessage;
        Dictionary<string, PackageItem> m_Packagelist;
        Dictionary<string, CatalogItem> m_Catalogs;
        Dictionary<string, InstalledDebItem> m_InstalledDebList;
        private List<string> m_AppNames;
        string m_downqueuefile = string.Empty;
        string m_AptDownloadFolder = string.Empty;
        string m_AptDownloadStatusFolder = string.Empty;
        List<string> m_SystemDeb;
        Dictionary<string, PackageItem> m_TempPackagelist;

        #region html模板
        const string itemhtmltemplate = @"
        <li id=""{5}"">
			<div class='appIcon' name='itemIcon'><img src='icons/normal_App.png' alt=""{0}"" width='32' height='32' /></div>
			<dl class='appIntro'>
				<dt>{0}</dt>
				<dd>{1} </dd>
			</dl>
			<ul class='appInfo'>
				<li class='size'>{2}</li>
				<li class='rate' name='itemrate'>
					<a href='goto:{4}' title='Catalog:{4}'>{4}</a><br>
                    <a href=""http://www.ithemesky.com/Soft/Comment/{5}/{6}/{7}/"" target='_blank'>Comments</a>
				</li>
				<li class='btn'>
					<a href='{3}' class='download'>Download</a>
				</li>
			</ul>
		</li>";
        const string htmltemplate = @"
<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
<html xmlns='http://www.w3.org/1999/xhtml'>
<head>
<meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
<title>app list</title>
<style type='text/css'>
body{ background:#FFF; font:normal 11px/1.5 Verdana, Geneva, sans-serif; color:#333; padding:0; margin:0;}
em{font-style:normal;color:#cc0000}
p,h1,h2,h3,h4,h5,h6{ padding:0; margin:0;}
a:link,a:visited{ text-decoration:none; color:#004E98;}
a:hover,a:active{ text-decoration:underline; color:#F60;}
ul,li,dl,dt,dd,ol{ padding:0; margin:0; list-style:none;}
.appList{ overflow:hidden;}
.appList li{ padding:10px; height:45px; border-bottom:1px solid #CCC;}
.appList li.selected{ background:#FAF7EB;}
.appIcon{ float:left; padding:6px 0; height:32px;}
.appIntro{ float:left; width:400px; padding:0 10px;}
.appIntro dt{ font-weight:bold; padding:2px 0;}
.appIntro dd{ color:#555; line-height:1.2;}
.appInfo{ float:right; width:260px; }
.appInfo li{ padding:0; height:auto; border-bottom:0;}
.appInfo li.size{ float:left; width:80px; text-align:center; line-height:40px;}
.appInfo li.rate{ float:left; width:90px; text-align:center; padding-right:10px;}
.appInfo li.rate img{ margin:5px 1px 5px 0;}
.appInfo li.btn{ float:left; width:80px; margin-top:11px;}
.appInfo li.btn a:link,.appInfo li.btn a:visited{ background:url(icons/btn.png) no-repeat; display:block; width:78px; height:23px; line-height:22px; font-size:10px; color:#333;}
.appInfo li.btn a:hover,.appInfo li.btn a:active{ text-decoration:none; color:#333;}
.appInfo li.btn a.download:link,.appInfo li.btn a.download:visited{ background-position:0 -30px; text-indent:20px;}
.appInfo li.btn a.download:hover,.appInfo li.btn a.download:active{ background-position:-80px -30px;}
.appInfo li.btn a.update:link,.appInfo li.btn a.update:visited{ background-position:0 -60px; text-indent:25px;}
.appInfo li.btn a.update:hover,.appInfo li.btn a.update:active{ background-position:-80px -60px;}
.appInfo li.btn .installed{ background:url(icons/btn.png) no-repeat; display:block; width:78px; height:23px; line-height:22px; color:#666; text-align:center;}
.pageInfo{ clear:both; margin:0 auto; width:340px; height:30px; padding:20px 0;}
.pageInfo li{ float:left; font-weight:bold; padding:0 3px;}
.pageInfo li a:link,.pageInfo li a:visited{ display:block; width:20px; height:18px; border:1px solid #FFF; text-align:center;}
.pageInfo li a:hover,.pageInfo li a:active{ text-decoration:none; border:1px solid #95B5DB; background:url(icons/btn.png) no-repeat -20px -32px; color:#004E98;}
.pageInfo li.current a:link,.pageInfo li.current a:visited,.pageInfo li.current a:hover,.pageInfo li.current a:active{ text-decoration:none; background:url(icons/btn.png) no-repeat -20px -62px; border:1px solid #F90; color:#F60;}
</style>
</head>
<script type='text/javascript'>
function loadInfo()
{
    try
    {
        loadAppData();
    }
    catch(err)
    {
    }
}
</script>
<body onload='loadInfo();'>
<div class='appList'>
	<ul>
{Content}
    </ul>
</div>
<script language='javascript' type='text/javascript' src='http://www.ithemesky.com/appfunction.js'></script>
</body>
</html>
";
        #endregion

        #endregion

        #region 属性
        internal string DownQueueFile
        {
            get
            {
                return m_downqueuefile;
            }
        }

        /// <summary>
        /// 下载保存路径
        /// </summary>
        internal string AptDownloadFolder
        {
            get
            {
                return m_AptDownloadFolder;
            }
        }
        /// <summary>
        /// 待下载的文件下载状态保存路径
        /// </summary>
        internal string AptDownloadStatusFolder
        {
            get
            {
                return m_AptDownloadStatusFolder;
            }
        }

        internal List<string> AppNames
        {
            get
            {
                return m_AppNames;
            }
        }

        internal Dictionary<string, CatalogItem> Catalogs
        {
            get
            {
                return m_Catalogs;
            }
        }

        /// <summary>
        /// RepositoryList
        /// </summary>
        internal Dictionary<string, RepositoryInfo> RepositoryList
        {
            get 
            {
                return m_RepositoryList;
            }
        }

        /// <summary>
        /// Packagelist
        /// </summary>
        internal Dictionary<string, PackageItem> Packagelist
        {
            get
            {
                return m_Packagelist;
            }
        }

        internal Dictionary<string, InstalledDebItem> InstalledDebList
        {
            get
            {
                return m_InstalledDebList;
            }
        }
        #endregion

        #region 消息处理
        private void RaiseMessageHandler(string message, MessageTypeOption messageType)
        {
            if (OnMessage != null)
            {
                OnMessage(this, message, messageType);
            }
        }
        #endregion

        #region 构造函数
        private AppHelper()
        { 
        }
        /// <summary>
        /// 静态构造函数
        /// </summary>
        public AppHelper(iPhoneFileDevice filedevice)
        {
            m_iPhoneDevice = filedevice;
            m_RepositoryList = new Dictionary<string, RepositoryInfo>();
            m_Repository2PathList = new Dictionary<string, string>();
            m_Packagelist = new Dictionary<string, PackageItem>();
            m_Catalogs = new Dictionary<string, CatalogItem>();
            m_AppNames = new List<string>();
            m_InstalledDebList = new Dictionary<string, InstalledDebItem>();
            m_TempPackagelist = new Dictionary<string, PackageItem>();

            m_AptFolder = iSpriteContext.Current.iSpriteApplicationDataPath + "Apt\\";
            if (!Directory.Exists(m_AptFolder))
            {
                Directory.CreateDirectory(m_AptFolder);
            }

            m_AptSourceCfg = m_AptFolder + "SourceCfg.ini";
            m_downqueuefile = m_AptFolder + "downqueue.xml";

            m_AptPackagesFolder = m_AptFolder + "Packages\\";
            if (!Directory.Exists(m_AptPackagesFolder))
            {
                Directory.CreateDirectory(m_AptPackagesFolder);
            }

            m_AptDownloadFolder = m_AptFolder + "download\\";
            if (!Directory.Exists(m_AptPackagesFolder))
            {
                Directory.CreateDirectory(m_AptPackagesFolder);
            }

            m_AptDownloadStatusFolder = m_AptDownloadFolder + "status\\";
            if (!Directory.Exists(m_AptDownloadStatusFolder))
            {
                Directory.CreateDirectory(m_AptDownloadStatusFolder);
            }

            //ReleaseUrlSuffixList
            ReleaseUrlSuffixList = new List<string>();

            ReleaseUrlSuffixList.Add("Release");
            ReleaseUrlSuffixList.Add("dists/stable/Release");
            ReleaseUrlSuffixList.Add("dists/tangelo/Release");
            ReleaseUrlSuffixList.Add("dists/hnd/Release");
            ReleaseUrlSuffixList.Add("dists/tangelo-3.7/Release");

            ReleaseUrlSuffixList.Add("Release.bz2");
            ReleaseUrlSuffixList.Add("dists/stable/Release.bz2");
            ReleaseUrlSuffixList.Add("dists/tangelo/Release.bz2");
            ReleaseUrlSuffixList.Add("dists/hnd/Release.bz2");
            ReleaseUrlSuffixList.Add("dists/tangelo-3.7/Release.bz2");

            ReleaseUrlSuffixList.Add("Release.gz");
            ReleaseUrlSuffixList.Add("dists/stable/Release.gz");
            ReleaseUrlSuffixList.Add("dists/tangelo/Release.gz");
            ReleaseUrlSuffixList.Add("dists/hnd/Release.gz");
            ReleaseUrlSuffixList.Add("dists/tangelo-3.7/Release.gz");

            //PackagesUrlSuffixList
            PackagesUrlSuffixList = new List<string>();
            PackagesUrlSuffixList.Add("Packages");
            PackagesUrlSuffixList.Add("dists/stable/main/binary-iphoneos-arm/Packages");
            PackagesUrlSuffixList.Add("dists/tangelo/main/binary-iphoneos-arm/Packages");
            PackagesUrlSuffixList.Add("dists/hnd/main/binary-iphoneos-arm/Packages");
            PackagesUrlSuffixList.Add("dists/tangelo-3.7/main/binary-iphoneos-arm/Packages");

            PackagesUrlSuffixList.Add("Packages.bz2");
            PackagesUrlSuffixList.Add("dists/stable/main/binary-iphoneos-arm/Packages.bz2");
            PackagesUrlSuffixList.Add("dists/tangelo/main/binary-iphoneos-arm/Packages.bz2");
            PackagesUrlSuffixList.Add("dists/hnd/main/binary-iphoneos-arm/Packages.bz2");
            PackagesUrlSuffixList.Add("dists/tangelo-3.7/main/binary-iphoneos-arm/Packages.bz2");

            PackagesUrlSuffixList.Add("Packages.gz");
            PackagesUrlSuffixList.Add("dists/stable/main/binary-iphoneos-arm/Packages.gz");
            PackagesUrlSuffixList.Add("dists/tangelo/main/binary-iphoneos-arm/Packages.gz");
            PackagesUrlSuffixList.Add("dists/hnd/main/binary-iphoneos-arm/Packages.gz");
            PackagesUrlSuffixList.Add("dists/tangelo-3.7/main/binary-iphoneos-arm/Packages.gz");


            m_SystemDeb = new List<string>(new string[] { 
                "adv-cmds", "apt", "base", "bash", "berkeleydb", "bigboss", "bzip2", "coreutils", "cydia", "cydia-sources", "darwintools", "diffutils", "dpkg", "findutils", "gettext", "gnupg", 
                "grep", "gzip", "inetutils", "ispazio.net", "less", "libgcc", "libutil", "modmyifone", "nano", "ncurses", "network-cmds", "pcre", "readline", "saurik", "sed", "shell-cmds", 
                "ste", "system-cmds", "tar", "zip", "unzip", "yellowsn0w.com", "zodttd", "firmware", "apr-lib", "apt7-key", "apt7-lib", "coreutils-bin", "essential", "lzma", "pam", "pam-modules", "profile.d"
             });
        }
        #endregion

        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        internal void InitAppData(FinishLoadAppDataHandler doFinishLoad)
        {
            LoadInstallDebs();
            LoadCydiaSource();
            LoadCydiaPackages();

            doFinishLoad();
        }
        #endregion

        #region 根据ID获取指定的deb软件信息
        /// <summary>
        /// 根据ID获取指定的deb软件信息
        /// </summary>
        /// <param name="PID"></param>
        /// <param name="debitem"></param>
        /// <returns></returns>
        internal bool GetPackageByID(string PID,out PackageItem debitem)
        {
            return Packagelist.TryGetValue(PID, out debitem);
        }
        #endregion

        #region 根据Identifier获取指定的Repository信息
        /// <summary>
        /// 根据Identifier获取指定的Repository信息
        /// </summary>
        /// <param name="Identifier"></param>
        /// <param name="repInfo"></param>
        /// <returns></returns>
        internal bool GetRepositoryByIdentifier(string Identifier, out RepositoryInfo repInfo)
        {
            return RepositoryList.TryGetValue(Identifier, out repInfo);
        }
        #endregion

        #region 读取Cydia源
        /// <summary>
        /// 读取Cydia源
        /// </summary>
        public void LoadCydiaSource()
        {
            List<string> filelist = m_iPhoneDevice.GetFiles(m_CydiaSourceFolder);

            Regex r = new Regex("deb (?<url>[\\S]*?) ", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            List<string> urllist = new List<string>();
            foreach (string filepath in filelist)
            {
                string content = m_iPhoneDevice.GetFileText(m_CydiaSourceFolder + filepath);

                Match m = r.Match(content);
                if (m.Success)
                {
                    urllist.Add(m.Result("${url}"));
                }
            }

            if (File.Exists(m_AptSourceCfg))
            {
                GetAptSourceCfg();
            }

            foreach (string url in urllist)
            {
                CheckAptSourceCfg(url);
            }

            LoadRepository2PathList();

            foreach (KeyValuePair<string, RepositoryInfo> item in m_RepositoryList)
            {
                if (string.IsNullOrEmpty(item.Value.APTCachedPackagesURL))
                {
                    if (m_Repository2PathList.ContainsKey(item.Value.Identifier))
                    {
                        item.Value.APTCachedPackagesURL = "http://" + m_Repository2PathList[item.Value.Identifier].Replace("_", "/");
                    }
                }
                if (string.IsNullOrEmpty(item.Value.APTCachedReleaseURL) && !string.IsNullOrEmpty(item.Value.APTCachedPackagesURL))
                {
                    item.Value.APTCachedReleaseURL =
                        item.Value.APTCachedPackagesURL.Replace("/main/binary-iphoneos-arm/Packages", "/Release");
                }
            }

            SaveAptSourceCfg();
        }
        #endregion

        #region 获取iPhone上cydia源和Packages稳健对应关系
        /// <summary>
        /// 获取iPhone上cydia源和Packages稳健对应关系
        /// </summary>
        void LoadRepository2PathList()
        {
            List<string> ifilelist = m_iPhoneDevice.GetFiles(m_CydiaPackagesFolder);
            foreach (string path in ifilelist)
            {
                string filename = Path.GetFileName(path);
                if (filename.EndsWith("_Packages"))
                {
                    int index = filename.IndexOf("_");
                    if (index > -1)
                    {
                        string host = filename.Substring(0, index);
                        if (!m_Repository2PathList.ContainsKey(host))
                        {
                            m_Repository2PathList.Add(host, filename);
                        }
                    }
                }
            }
        }
        #endregion

        #region 读取Cydia软件列表
        /// <summary>
        /// 读取Cydia软件列表
        /// </summary>
        public void LoadCydiaPackages()
        {
            List<iFileInfo> ifilelist = m_iPhoneDevice.GetFileInfos(m_CydiaPackagesFolder);

            #region 比较并下载文件到本地
            foreach (iFileInfo fileinfo in ifilelist)
            {
                string localfile = m_AptPackagesFolder + fileinfo.FileName;
                bool flag = false;
                if (File.Exists(localfile))
                {
                    if (new FileInfo(localfile).Length != Convert.ToInt64(fileinfo.FileSize))
                    {
                        flag = true;
                    }
                }
                else
                {
                    flag = true;
                }
                if (flag)
                {
                    m_iPhoneDevice.Downlod2PC(fileinfo.FullPath, localfile, false);
                }
            }
            #endregion

            string[] localfilelist = Directory.GetFiles(m_AptPackagesFolder);

            foreach (string filepath in localfilelist)
            {
                if (filepath.EndsWith("_Packages"))
                {
                    int index = Path.GetFileName(filepath).IndexOf("_");
                    if (index > -1)
                    {
                        string host = Path.GetFileName(filepath).Substring(0, index);
                        
                        string content = File.ReadAllText(filepath, Encoding.UTF8);
                        List<AptData> list = new List<AptData>();
                        if (ParseAptData(content, ref list))
                        {
                            foreach (AptData aptdata in list)
                            {
                                PackageItem item = AptData2PackageItem(aptdata);
                                AddPackageItem2Temp(host, item);
                            }
                        }
                    }
                }
            }

            //排序
            SortPackages();
        }

        bool AddPackageItem2Temp(string repositoryIdentifier, PackageItem item)
        {
            CatalogItem catalogitem;
            if (!m_TempPackagelist.ContainsKey(item.PackageID))
            {
                item.Category = FormatCatalog(item.Category);
                if (m_Catalogs.TryGetValue(item.Category, out catalogitem))
                {
                    catalogitem.Count += 1;
                }
                else
                {
                    catalogitem = new CatalogItem();
                    catalogitem.Name = item.Category;
                    catalogitem.Count = 1;
                    m_Catalogs.Add(catalogitem.Name, catalogitem);
                }

                item.RepositoryIdentifier = repositoryIdentifier;

                m_TempPackagelist.Add(item.PackageID, item);

                if (item.Category != "Themes" //主题包太多了
                    && !m_AppNames.Contains(item.Name))
                {
                    m_AppNames.Add(item.Name);//搜索用的
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 排序
        /// </summary>
        void SortPackages()
        {
            List<string> appnamelist = new List<string>();
            foreach (KeyValuePair<string, PackageItem> item in m_TempPackagelist)
            {
                appnamelist.Add(item.Key);
            }
            appnamelist.Sort();
            foreach (string packageid in appnamelist)
            {
                PackageItem item;
                if (m_TempPackagelist.TryGetValue(packageid, out item) && !m_Packagelist.ContainsKey(packageid))
                {
                    m_Packagelist.Add(packageid, item);
                }
            }

            m_AppNames.Sort();
        }
        #endregion

        #region 获取已经安装的deb软件
        /// <summary>
        /// 获取已经安装的deb软件
        /// </summary>
        /// <returns></returns>
        public void LoadInstallDebs()
        {
            string content = m_iPhoneDevice.GetFileText("/private/var/lib/dpkg/status");
            List<AptData> AptList = new List<AptData>();
            if (ParseAptData(content, ref AptList))
            {
                InstalledDebItem item;
                Dictionary<string, InstalledDebItem> templist = new Dictionary<string, InstalledDebItem>();
                foreach (AptData apt in AptList)
                {
                    item = AptData2DebItem(apt);
                    //if (item.Name == "WinterBoard")
                    //{ 
                    //}
                    if ((item.Installed_Size != string.Empty || item.Status.Contains("install ok"))
                        && !templist.ContainsKey(item.DebID))
                    {
                        item.IsSystemDeb = m_SystemDeb.Contains(item.Package) || apt.GetTagValue("Essential") == "yes";

                        templist.Add(item.DebID, item);
                    }
                }
                List<string> appnamelist = new List<string>();
                foreach (KeyValuePair<string, InstalledDebItem> kvp in templist)
                {
                    appnamelist.Add(kvp.Key);
                }
                appnamelist.Sort();
                m_InstalledDebList.Clear();
                foreach (string key in appnamelist)
                {
                    if (templist.TryGetValue(key, out item) && !m_InstalledDebList.ContainsKey(key))
                    {
                        m_InstalledDebList.Add(key, item);
                    }
                }
            }
        }
        #endregion

        Version GetVer(string ver)
        {
            return new Version(ver.Replace("-", "."));
        }

        #region 判断是否已经安装
        bool HasInstalled(string packageIdentifier, string ver, string compare)
        {
            string packageName = string.Empty;
            return HasInstalled(packageIdentifier, ver, compare, out packageName);
        }
        /// <summary>
        /// 判断是否已经安装
        /// </summary>
        /// <param name="packagename"></param>
        /// <param name="ver"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        bool HasInstalled(string packageIdentifier, string ver, string compare,out string packageName)
        {
            packageName = string.Empty;

            try
            {
                if (!string.IsNullOrEmpty(ver))
                {
                    if (compare == "=")
                    {
                        return m_InstalledDebList.ContainsKey(packageIdentifier + "-" + ver);
                    }
                    else
                    {
                        Version v = GetVer(ver);
                        foreach (KeyValuePair<string, InstalledDebItem> item in m_InstalledDebList)
                        {
                            if (item.Value.Version != "" && item.Value.Package.Equals(packageIdentifier, StringComparison.CurrentCultureIgnoreCase))
                            {
                                Version _v = GetVer(item.Value.Version);
                                if (_v >= v)
                                {
                                    packageName = item.Value.Name == "" ? item.Value.Package : item.Value.Name;
                                    return true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (KeyValuePair<string, InstalledDebItem> item in m_InstalledDebList)
                    {
                        if (item.Value.Package.Equals(packageIdentifier, StringComparison.CurrentCultureIgnoreCase))
                        {
                            packageName = item.Value.Name == "" ? item.Value.Package : item.Value.Name;
                            return true;
                        }
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 从字典删除已经安装的deb软件
        /// <summary>
        /// 从字典删除已经安装的deb软件
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public void RemoveInstallDeb(InstalledDebItem item)
        {
            m_InstalledDebList.Remove(item.DebID);
        }
        #endregion

        #region 根据名称（版本号）获取软件信息
        /// <summary>
        /// 根据名称（版本号）获取软件信息
        /// </summary>
        /// <param name="packagename"></param>
        /// <param name="ver"></param>
        /// <returns></returns>
        PackageItem GetPackageItem(string packageIdentifier, string ver, string compare)
        {
            try
            {
                if (!string.IsNullOrEmpty(ver))
                {
                    //需要根据版本号进行比较
                    if (compare == "=")
                    {
                        //返回相同版本号的
                        PackageItem debitem;
                        if (GetPackageByID(packageIdentifier + "-" + ver, out debitem))
                        {
                            return debitem;
                        }
                    }
                    else
                    {
                        //返回>=当前版本号的
                        Version v = GetVer(ver);
                        foreach (KeyValuePair<string, PackageItem> item in m_Packagelist)
                        {
                            if (item.Value.Version != ""
                                && item.Value.Identifier.Equals(packageIdentifier, StringComparison.CurrentCultureIgnoreCase))
                            {
                                Version _v = GetVer(item.Value.Version);
                                if (_v >= v)
                                {
                                    return item.Value;
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (KeyValuePair<string, PackageItem> item in m_Packagelist)
                    {
                        if (item.Value.Identifier.Equals(packageIdentifier, StringComparison.CurrentCultureIgnoreCase))
                        {
                            return item.Value;
                        }
                    }
                }
            }
            catch
            { 
            }

            return null;
        }
        #endregion

        #region 判断是否和当前已经安装的软件冲突
        /// <summary>
        /// 判断是否和当前已经安装的软件冲突
        /// </summary>
        /// <param name="debitem"></param>
        /// <param name="errList"></param>
        /// <returns></returns>
        public bool CheckConflict2Installed(PackageItem debitem, out List<string> errList)
        {
            errList = new List<string>();
            if (string.IsNullOrEmpty(debitem.Conflicts))
            {
                return false;
            }
            else
            {
                foreach (string d in (debitem.Conflicts).Split(','))
                {
                    string p = d.Trim();
                    if (p != string.Empty)
                    {
                        string packageName = string.Empty;
                        if (HasInstalled(p, string.Empty, string.Empty, out packageName))
                        {
                            errList.Add(packageName);
                        }
                    }
                }
            }
            return errList.Count > 0;
        }
        #endregion

        #region 获取依赖软件
        /// <summary>
        /// 获取依赖软件
        /// </summary>
        /// <param name="debitem"></param>
        /// <param name="errList"></param>
        /// <returns></returns>
        public List<PackageItem> GetDependsDeb(PackageItem debitem, out List<string> errList)
        {
            List<PackageItem> deplist = new List<PackageItem>();
            errList = new List<string>();
            string packageIdentifier;
            string packageVer;
            string compare;
            foreach (string d in (debitem.Pre_Depends + "," + debitem.Dependencies).Split(','))
            {
                string p = d.Trim();
                if (p != string.Empty)
                {
                    #region 获取软件标识、版本等信息
                    packageIdentifier = string.Empty;
                    packageVer = string.Empty;
                    compare = string.Empty;
                    int index = p.IndexOf("(");
                    if (index > -1)
                    {
                        packageIdentifier = p.Substring(0,index).Trim();
                        if (p.Contains(">="))
                        {
                            compare = ">=";
                        }
                        else
                        {
                            compare = "=";
                        }

                        index = p.IndexOf(compare) + compare.Length;
                        int endindex = p.IndexOf(")");
                        if (endindex > -1)
                        {
                            packageVer = p.Substring(index, endindex - index).Trim();
                        }
                        else
                        {
                            packageVer = "";
                        }
                    }
                    else
                    {
                        packageIdentifier = p;
                    }
                    #endregion

                    if (!HasInstalled(packageIdentifier, packageVer, compare))
                    {
                        PackageItem dep_debitem = GetPackageItem(packageIdentifier, packageVer, compare);
                        if (null != dep_debitem)
                        {
                            if (!deplist.Contains(dep_debitem))
                            {
                                deplist.Add(dep_debitem);
                            }
                        }
                        else
                        {
                            errList.Add(p);
                        }
                    }
                }
            }
            return deplist;
        }
        #endregion

        #region 根据类别获取软件
        /// <summary>
        /// 根据类别获取软件
        /// </summary>
        /// <param name="packages"></param>
        /// <param name="catalogName"></param>
        /// <returns></returns>
        public Dictionary<string, PackageItem> GetPackagesByCatalog(Dictionary<string, PackageItem> packages, string catalogName)
        {
            Dictionary<string, PackageItem> list = new Dictionary<string, PackageItem>();
            foreach (KeyValuePair<string, PackageItem> item in packages)
            {
                if (item.Value.Category.Equals(catalogName, StringComparison.CurrentCultureIgnoreCase))
                {
                    list.Add(item.Key,item.Value);
                }
            }
            return list;
        }
        #endregion

        #region 根据类别和关键字搜索软件
        /// <summary>
        /// 根据类别和关键字搜索软件
        /// </summary>
        /// <param name="packages"></param>
        /// <param name="catalogName"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public Dictionary<string, PackageItem> SearchPackages(Dictionary<string, PackageItem> packages, string catalogName,string key)
        {
            key = key.ToLower();
            Dictionary<string, PackageItem> list = new Dictionary<string, PackageItem>();
            Dictionary<string, PackageItem> listtemp = new Dictionary<string, PackageItem>();
            NameValueCollection nc = new NameValueCollection();
            List<double> listSimilarity = new List<double>();
            
            //先搜索标题
            foreach (KeyValuePair<string, PackageItem> item in packages)
            {
                if (
                    (string.IsNullOrEmpty(catalogName) || catalogName == "All Packages" || item.Value.Category.Equals(catalogName, StringComparison.CurrentCultureIgnoreCase))
                    && (item.Value.Name.ToLower().Contains(key))
                    )
                {
                    listtemp.Add(item.Key, item.Value);
                    double k = key.Length * 1.0 / item.Value.Name.Length;
                    if (!listSimilarity.Contains(k))
                    {
                        listSimilarity.Add(k);
                    }
                    nc.Add(k.ToString(), item.Key);
                }
            }
            listSimilarity.Sort();
            PackageItem tempitem;
            for (int index = listSimilarity.Count - 1; index >= 0; index--)
            {
                double k = listSimilarity[index];
                foreach (string PackageID in nc[k.ToString()].Split(','))
                {
                    if (listtemp.TryGetValue(PackageID, out tempitem))
                    {
                        list.Add(tempitem.PackageID, tempitem);
                    }
                }
            }
            //后搜索描述
            foreach (KeyValuePair<string, PackageItem> item in packages)
            {
                if (
                    (string.IsNullOrEmpty(catalogName) || catalogName == "All Packages" || item.Value.Category.Equals(catalogName, StringComparison.CurrentCultureIgnoreCase))
                    && (item.Value.Description.ToLower().Contains(key))
                    )
                {
                    if (!list.ContainsKey(item.Key))
                    {
                        list.Add(item.Key, item.Value);
                    }
                }
            }
            return list;
        }
        #endregion

        #region 添加Cydia源
        /// <summary>
        /// 添加Cydia源
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool AddCydiaSource(RepositoryInfo repositoryInfo, out int errCode)
        {
            errCode = 0;
            string APTCachedReleaseURL=string.Empty;
            if (GetRepositoryInfoByUrl(repositoryInfo.URL, ref APTCachedReleaseURL, ref repositoryInfo))
            {
                if (!m_RepositoryList.ContainsKey(repositoryInfo.Identifier))
                {
                    m_RepositoryList.Add(repositoryInfo.Identifier, repositoryInfo);
                    SaveAptSourceCfg();
                    return true;
                }
                else
                {
                    errCode = 1;
                    return false;
                }
            }
            else
            {
                errCode = 2;
                return false;
            }
        }
        #endregion

        #region 删除源
        /// <summary>
        /// 添加Cydia源
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool RemoveCydiaSource(RepositoryInfo repositoryInfo)
        {
            m_RepositoryList.Remove(repositoryInfo.Identifier);

            List<string> appnamelist = new List<string>(); 
            CatalogItem catalogitem;
            foreach (KeyValuePair<string, PackageItem> item in m_TempPackagelist)
            {
                if (item.Value.RepositoryIdentifier.Equals(repositoryInfo.Identifier, StringComparison.CurrentCultureIgnoreCase))
                {
                    appnamelist.Add(item.Key); 
                    
                    if (m_Catalogs.TryGetValue(item.Value.Category, out catalogitem))
                    {
                        catalogitem.Count -= 1;
                    }
                }
            }
            foreach (string packageid in appnamelist)
            {
                m_Packagelist.Remove(packageid);
                m_TempPackagelist.Remove(packageid);
            }

            try
            {
                //删除文件
                string fileName = repositoryInfo.APTCachedPackagesURL.Substring(7).Replace("/", "_");
                fileName = Path.GetFileNameWithoutExtension(fileName);

                File.Delete(m_AptPackagesFolder + fileName);
                m_iPhoneDevice.DeleteFile(m_CydiaPackagesFolder + fileName);
                return true;
            }
            catch
            {
                return false;
            }

        }
        #endregion

        #region 更新源
        /// <summary>
        /// 更新源
        /// </summary>
        /// <param name="selectedItems"></param>
        /// <param name="action"></param>
        /// <param name="finishAction"></param>
        public void UpdateCydiaSource(List<ListViewItem> selectedItems,
            CydiaSourceActionHandler action,
            FinishAppHelpActionHandler finishAction)
        {
            RepositoryInfo repItem;
            int sucessCount = 0;
            foreach (ListViewItem item in selectedItems)
            {
                if (item.Tag is RepositoryInfo)
                {
                    action(item, false);
                    repItem = (RepositoryInfo)item.Tag; 
                    
                    if (string.IsNullOrEmpty(repItem.Description))
                    {
                        string APTCachedReleaseURL = repItem.APTCachedReleaseURL;
                        if (GetRepositoryInfoByUrl(repItem.URL, ref APTCachedReleaseURL, ref repItem))
                        {
                            repItem.APTCachedReleaseURL = APTCachedReleaseURL;
                        }
                    }
                    if (UpdatePackagesByCydiaSource(repItem))
                    {
                        sucessCount++;
                        action(item, true);
                    }
                }
            }
            finishAction(sucessCount);
        }
        #endregion

        #region 更新指定的cydia源的软件包
        /// <summary>
        /// 更新指定的cydia源的软件包
        /// </summary>
        /// <param name="repInfo"></param>
        public bool UpdatePackagesByCydiaSource(RepositoryInfo repInfo)
        {
            if (string.IsNullOrEmpty(repInfo.APTCachedPackagesURL))
            {
                //Packages下载地址未知 todo
            }
            List<PackageItem> packages = new List<PackageItem>();
            string packetcontent = string.Empty;
            if (GetPackagesByUrl(repInfo, out packages, ref packetcontent))
            {

                int count = 0;
                foreach (PackageItem item in packages)
                {
                    if (AddPackageItem2Temp(repInfo.Identifier, item))
                    {
                        count++;
                    }
                }
                if (count > 0)
                {
                    SortPackages();
                    //写成文件保存到m_AptPackagesFolder

                    string fileName = repInfo.APTCachedPackagesURL.Substring(7).Replace("/", "_");
                    fileName=Path.GetFileNameWithoutExtension(fileName);

                    File.WriteAllText(m_AptPackagesFolder + fileName, packetcontent, Encoding.UTF8);

                    //保存到iPhone的m_CydiaPackagesFolder
                    m_iPhoneDevice.Copy2iPhone(m_AptPackagesFolder + fileName, m_CydiaPackagesFolder + fileName, false);
                }

                repInfo.LastUpdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 检查iphone上的cydia源在本地的SourceCfg.ini是否存在，不存在则添加
        /// <summary>
        /// 检查iphone上的cydia源在本地的SourceCfg.ini是否存在，不存在则添加
        /// </summary>
        /// <param name="url"></param>
        /// <param name="replist"></param>
        private void CheckAptSourceCfg(string url)
        {
            string host = new Uri(url).Host;
            bool flag = m_RepositoryList.ContainsKey(host);
            if (!flag)
            {
                RepositoryInfo info = new RepositoryInfo();
                info.Name = host;
                info.URL = url;
                info.APTDownloadBaseURL = url;

                if (!m_RepositoryList.ContainsKey(info.Identifier))
                {
                    m_RepositoryList.Add(info.Identifier, info);
                }
            }
        }
        #endregion

        #region 保存源配置文件（SourceCfg.ini）
        /// <summary>
        /// 保存源配置文件（SourceCfg.ini）
        /// </summary>
        /// <param name="replist"></param>
        public void SaveAptSourceCfg()
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, RepositoryInfo> info in m_RepositoryList)
            {
                sb.Append(info.Value.ToString());
                sb.AppendLine();
            }

            File.WriteAllText(m_AptSourceCfg, sb.ToString(), Encoding.UTF8);
        }
        #endregion

        #region 将源配置文件（SourceCfg.ini）转换成List<RepositoryInfo>
        /// <summary>
        /// 将源配置文件（SourceCfg.ini）转换成List<RepositoryInfo>
        /// </summary>
        /// <param name="replist"></param>
        /// <returns></returns>
        bool GetAptSourceCfg()
        {
            string content = File.ReadAllText(m_AptSourceCfg, Encoding.UTF8);
            List<AptData> list = new List<AptData>();
            if (ParseAptData(content, ref list))
            {
                foreach (AptData aptdata in list)
                {
                    RepositoryInfo info = new RepositoryInfo();
                    info.Name = aptdata.GetTagValue("Name");
                    info.URL = aptdata.GetTagValue("URL");
                    info.Category = aptdata.GetTagValue("Category");
                    info.Description = aptdata.GetTagValue("Description");
                    info.Maintaner = aptdata.GetTagValue("Maintaner");
                    info.APTCachedPackagesURL = aptdata.GetTagValue("APTCachedPackagesURL");
                    info.APTCachedReleaseURL = aptdata.GetTagValue("APTCachedReleaseURL");
                    info.APTDownloadBaseURL = aptdata.GetTagValue("APTDownloadBaseURL");
                    info.LastUpdate = aptdata.GetTagValue("LastUpdate");
                    if (!m_RepositoryList.ContainsKey(info.Identifier))
                    {
                        m_RepositoryList.Add(info.Identifier, info);
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 获取cydiay源描述
        /// <summary>
        /// 获取cydiay源描述
        /// </summary>
        /// <param name="BaseURL"></param>
        /// <param name="APTCachedReleaseURL"></param>
        /// <param name="repositoryInfo"></param>
        /// <returns></returns>
        public bool GetRepositoryInfoByUrl(string BaseURL, ref string APTCachedReleaseURL,
            ref RepositoryInfo repositoryInfo)
        {
            List<AptData> list = new List<AptData>();
            bool isRepoInfoAvailable = false;

            if (!string.IsNullOrEmpty(APTCachedReleaseURL))
            {
                if (GetAptDataByUrl(APTCachedReleaseURL, ref list) && list.Count > 0)
                {
                    isRepoInfoAvailable = true;
                }
            }

            if (!isRepoInfoAvailable)
            {
                foreach (string urlSuffix in ReleaseUrlSuffixList)
                {
                    APTCachedReleaseURL = BaseURL.TrimEnd('/') + "/" + urlSuffix;
                    if (GetAptDataByUrl(APTCachedReleaseURL, ref list) && list.Count > 0)
                    {
                        isRepoInfoAvailable = true;
                        break;
                    }
                }
            }

            if (isRepoInfoAvailable)
            {
                repositoryInfo.URL = BaseURL;
                int index = APTCachedReleaseURL.IndexOf("/dists/");
                if (index != -1)
                {
                    repositoryInfo.APTDownloadBaseURL = APTCachedReleaseURL.Substring(0, index + 1);
                }
                else
                {
                    index = APTCachedReleaseURL.IndexOf("/Release");
                    if (index != -1)
                    {
                        repositoryInfo.APTDownloadBaseURL = APTCachedReleaseURL.Substring(0, index + 1);
                    }
                    else
                    {
                        repositoryInfo.APTDownloadBaseURL = "http://" + new Uri(BaseURL).Host;
                    }
                }
                repositoryInfo.APTCachedReleaseURL = APTCachedReleaseURL;
                if (list.Count > 0)
                {
                    AptData aptdata = list[0];
                    repositoryInfo.Name = aptdata.GetTagValue("Origin");
                    repositoryInfo.Description = aptdata.GetTagValue("Description");
                    repositoryInfo.Maintaner = aptdata.GetTagValue("Codename");
                }
            }
            else
            {
                APTCachedReleaseURL = "";
            }
            return isRepoInfoAvailable;
        }
        #endregion

        #region 获取cydiay源软件列表
        /// <summary>
        /// 获取cydiay源软件列表
        /// </summary>
        /// <param name="BaseURL"></param>
        /// <param name="CachedPackagesURL"></param>
        /// <param name="packages"></param>
        /// <returns></returns>
        public bool GetPackagesByUrl(RepositoryInfo repositoryInfo, out List<PackageItem> packages, ref string content)
        {
            packages = new List<PackageItem>();
            List<AptData> list = new List<AptData>();
            bool isPackagesAvailable = false;

            if (!string.IsNullOrEmpty(repositoryInfo.APTCachedPackagesURL))
            {
                if (GetAptDataByUrl(repositoryInfo.APTCachedPackagesURL, ref list, ref content) && list.Count > 0)
                {
                    isPackagesAvailable = true;
                }
            }
            else if (!string.IsNullOrEmpty(repositoryInfo.APTCachedReleaseURL))
            {
                if (GetAptDataByUrl(repositoryInfo.APTCachedReleaseURL.Replace("/Release", "/main/binary-iphoneos-arm/Packages"), ref list, ref content) && list.Count > 0)
                {
                    repositoryInfo.APTCachedPackagesURL = repositoryInfo.APTCachedReleaseURL.Replace("/Release", "/main/binary-iphoneos-arm/Packages");
                    isPackagesAvailable = true;
                }
            }

            if (!isPackagesAvailable)
            {
                foreach (string urlSuffix in PackagesUrlSuffixList)
                {
                    repositoryInfo.APTCachedPackagesURL = repositoryInfo.URL.TrimEnd('/') + "/" + urlSuffix;
                    if (GetAptDataByUrl(repositoryInfo.APTCachedPackagesURL, ref list, ref content) && list.Count > 0)
                    {
                        isPackagesAvailable = true;
                        break;
                    }
                }
            }

            if (isPackagesAvailable)
            {
                foreach (AptData aptdata in list)
                {
                    packages.Add(AptData2PackageItem(aptdata));
                }
            }
            else
            {
                repositoryInfo.APTCachedPackagesURL = "";
            }
            return isPackagesAvailable;
        }
        #endregion

        #region 获取远程Url的字节流并转化成List<AptData>
        /// <summary>
        /// 获取远程Url的字节流并转化成List<AptData>
        /// </summary>
        /// <param name="apturl"></param>
        /// <param name="aptData"></param>
        /// <returns></returns>
        bool GetAptDataByUrl(string apturl, ref List<AptData> aptData)
        {
            string content = string.Empty;
            return GetAptDataByUrl(apturl, ref  aptData, ref  content);
        }
        /// <summary>
        /// 获取远程Url的字节流并转化成List<AptData>
        /// </summary>
        /// <param name="apturl"></param>
        /// <param name="RemoteData"></param>
        /// <returns></returns>
        bool GetAptDataByUrl(string apturl, ref List<AptData> aptData, ref string content)
        {
            Stream remoteStream = null;
            try
            {
                if (CheckRemoteFileAvailable(apturl, out remoteStream))
                {
                    content = string.Empty;
                    if (apturl.EndsWith("bz2"))
                    {
                        if (!UnpackBZ2(remoteStream, ref content))
                        {
                            content = string.Empty;
                        }
                    }
                    else if (apturl.EndsWith("gz"))
                    {
                        if (!UnpackGZ(remoteStream, ref content))
                        {
                            content = string.Empty;
                        }
                    }
                    else
                    {
                        if (!GetHTTPContent(remoteStream, ref content))
                        {
                            content = string.Empty;
                        }
                    }
                    if (content != string.Empty && ParseAptData(content, ref aptData))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch
            {
            }
            finally
            {
                if (remoteStream != null)
                {
                    remoteStream.Close();
                }
            }
            return false;
        }
        #endregion

        #region 将AptData转换成PackageItem
        /// <summary>
        /// 将AptData转换成PackageItem
        /// </summary>
        /// <param name="aptdata"></param>
        /// <returns></returns>
        PackageItem AptData2PackageItem(AptData aptdata)
        {
            PackageItem item = new PackageItem();
            item.Name = aptdata.GetTagValue("Name");
            item.Version = aptdata.GetTagValue("Version");
            item.Description = aptdata.GetTagValue("Description");
            item.Identifier = aptdata.GetTagValue("Package");
            item.Category = aptdata.GetTagValue("Section");
            item.Contact = aptdata.GetTagValue("Author");
            //item.Date = aptdata.GetTagValue("Date");
            item.Dependencies = aptdata.GetTagValue("Depends");
            item.Pre_Depends = aptdata.GetTagValue("Pre-Depends");
            item.Conflicts = aptdata.GetTagValue("Conflicts");
            item.Homepage = aptdata.GetTagValue("Homepage");
            item.JailbreakRequired = true;
            item.Maintaner = aptdata.GetTagValue("Maintainer");
            item.Priority = aptdata.GetTagValue("Priority");
            item.Size = Convert.ToInt64(aptdata.GetTagValue("Size"));
            item.Tags = aptdata.GetTagValue("Tag");
            item.DownloadURL = aptdata.GetTagValue("Filename");
            item.Hash = aptdata.GetTagValue("MD5sum");

            return item;
        }
        #endregion

        #region 将AptData转换成InstalledDebItem
        /// <summary>
        /// 将AptData转换成InstalledDebItem
        /// </summary>
        /// <param name="aptdata"></param>
        /// <returns></returns>
        InstalledDebItem AptData2DebItem(AptData aptdata)
        {
            InstalledDebItem item = new InstalledDebItem();
            item.Name = aptdata.GetTagValue("Name");
            item.Package = aptdata.GetTagValue("Package");
            item.Status = aptdata.GetTagValue("Status");
            item.Version = aptdata.GetTagValue("Version");
            item.Installed_Size = aptdata.GetTagValue("Installed-Size");
            item.Description = aptdata.GetTagValue("Description");

            return item;
        }
        #endregion

        #region 分析数据文件
        /// <summary>
        /// 分析数据文件
        /// </summary>
        /// <param name="DataContent"></param>
        /// <param name="AptData"></param>
        /// <returns></returns>
        public bool ParseAptData(string DataContent, ref List<AptData> AptData)
        {
            string propertyValue = string.Empty;
            string propertyKey = string.Empty;
            StringReader reader = new StringReader(DataContent);
            List<AptData> list = new List<AptData>();
            AptData item = new AptData();
            while (reader.Peek() > 0)
            {
                string line = reader.ReadLine();
                if (line != string.Empty)
                {
                    if (line.StartsWith(" ") || line.StartsWith("\t"))
                    {
                        propertyValue = propertyValue + line;
                    }
                    else
                    {
                        #region 将健和值写入
                        if (propertyKey != string.Empty)
                        {
                            if (item.ContainsKey(propertyKey))
                            {
                                item[propertyKey] = item[propertyKey] + propertyValue;
                            }
                            else
                            {
                                item.Add(propertyKey, propertyValue);
                            }
                            propertyKey = string.Empty;
                            propertyValue = string.Empty;
                        }
                        #endregion

                        if (!line.Contains(":"))
                        {
                            //用空格分隔健和值
                            if (!line.Contains(" "))
                            {
                                //属性为空
                                propertyKey = line;
                                propertyValue = string.Empty;
                            }
                            else
                            {
                                propertyKey = line.Substring(0, line.IndexOf(" "));
                                propertyValue = line.Substring(line.IndexOf(" ") + 2, (line.Length - line.IndexOf(" ")) - 2);
                            }
                        }
                        else if (line.EndsWith(":"))
                        {
                            //属性为空
                            propertyKey = line.Substring(0, line.Length - 1);
                            propertyValue = string.Empty;
                        }
                        else
                        {
                            //用:分隔健和值
                            propertyKey = line.Substring(0, line.IndexOf(":"));//属性名称
                            propertyValue = line.Substring(line.IndexOf(":") + 2, (line.Length - line.IndexOf(":")) - 2);//属性值
                        }
                    }
                }
                else
                {
                    #region 将健和值写入
                    if (propertyKey != string.Empty)
                    {
                        if (item.ContainsKey(propertyKey))
                        {
                            item[propertyKey] = item[propertyKey] + propertyValue;
                        }
                        else
                        {
                            item.Add(propertyKey, propertyValue);
                        }
                        propertyKey = string.Empty;
                        propertyValue = string.Empty;
                    }
                    #endregion

                    if (item.Count > 0)
                    {
                        list.Add(item);
                    }
                    item = new AptData();
                }
            }

            if (propertyKey != string.Empty)
            {
                #region 将健和值写入
                if (item.ContainsKey(propertyKey))
                {
                    item[propertyKey] = item[propertyKey] + propertyValue;
                }
                else
                {
                    item.Add(propertyKey, propertyValue);
                }
                #endregion

                propertyKey = string.Empty;
                propertyValue = string.Empty;
                if (item.Count > 0)
                {
                    list.Add(item);
                }
            }
            AptData = list;
            return true;
        }
        #endregion

        #region 远程文件是否有效
        /// <summary>
        /// 远程文件是否有效
        /// </summary>
        /// <param name="URL"></param>
        /// <returns></returns>
        bool CheckRemoteFileAvailable(string URL, out Stream stream)
        {
            HttpWebRequest request;
            HttpWebResponse response;
            stream = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(URL);
                request.Timeout = 0x1d4c0;
                request.KeepAlive = false;
                request.AllowAutoRedirect = false;
                request.Method = "GET";
                request.UserAgent = "Cydia/0.9 CFNetwork/342.1 Darwin/9.4.1";
                request.Headers.Add("X-Unique-ID", "000000000000100080000017f205aa5c00000000");
                request.Headers.Add("X-Firmware", "2.2.1");
                request.Headers.Add("X-Machine", "iPhone");

                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    stream = response.GetResponseStream();
                    return true;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }
        #endregion        

        #region 获取url内容
        /// <summary>
        /// 获取url内容
        /// </summary>
        /// <param name="URL"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        private bool GetHTTPContent(Stream stream, ref string Content)
        {
            bool flag;
            try
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                Content = reader.ReadToEnd();
                reader.Close();
                return true;

            }
            catch (Exception ex)
            {
                flag = false;
                return flag;
            }
        }
        #endregion

        #region 解压相关方法
        private bool UnpackBZ2(string FilePath, ref string Content)
        {
            bool flag;
            FileStream stream2 = new FileStream(FilePath, FileMode.Open);
            MemoryStream stream3 = new MemoryStream();
            BZip2InputStream stream = new BZip2InputStream(stream2);
            byte[] buffer = new byte[0x400];
            try
            {
                int num;
                do
                {
                    num = stream.Read(buffer, 0, buffer.Length);
                    stream3.Write(buffer, 0, num);
                }
                while (num > 0);
                stream3.Seek(0L, SeekOrigin.Begin);
                Content = new StreamReader(stream3, Encoding.UTF8).ReadToEnd();
                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;
        }

        private bool UnpackBZ2(Stream InStream, ref string Content)
        {
            bool flag;
            MemoryStream stream2 = new MemoryStream();
            BZip2InputStream stream = new BZip2InputStream(InStream);
            byte[] buffer = new byte[0x400];
            try
            {
                int num;
                do
                {
                    num = stream.Read(buffer, 0, buffer.Length);
                    stream2.Write(buffer, 0, num);
                }
                while (num > 0);
                stream2.Seek(0L, SeekOrigin.Begin);
                Content = new StreamReader(stream2, Encoding.UTF8).ReadToEnd();
                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;
        }

        private bool UnpackGZ(Stream InStream, ref string Content)
        {
            bool flag;
            MemoryStream stream2 = new MemoryStream();
            GZipInputStream stream = new GZipInputStream(InStream);
            byte[] buffer = new byte[0x400];
            try
            {
                int num;
                do
                {
                    num = stream.Read(buffer, 0, buffer.Length);
                    stream2.Write(buffer, 0, num);
                }
                while (num > 0);
                stream2.Seek(0L, SeekOrigin.Begin);
                Content = new StreamReader(stream2, Encoding.UTF8).ReadToEnd();
                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;
        }

        private bool UnpackGZ(string FilePath, ref string Content)
        {
            bool flag;
            FileStream baseInputStream = new FileStream(FilePath, FileMode.Open);
            MemoryStream stream3 = new MemoryStream();
            GZipInputStream stream = new GZipInputStream(baseInputStream);
            byte[] buffer = new byte[0x400];
            try
            {
                int num;
                do
                {
                    num = stream.Read(buffer, 0, buffer.Length);
                    stream3.Write(buffer, 0, num);
                }
                while (num > 0);
                stream3.Seek(0L, SeekOrigin.Begin);
                Content = new StreamReader(stream3, Encoding.UTF8).ReadToEnd();
                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;
        }
        #endregion

        #region 转换成html
        /// <summary>
        /// 转换成html
        /// </summary>
        /// <param name="packagelist"></param>
        /// <param name="pagesize"></param>
        /// <param name="pageindex"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Packages2Html(Dictionary<string, PackageItem> packagelist, int pagesize, int pageindex, string key)
        {
            if (pageindex < 1)
            {
                pageindex = 1;
            }
            string filePath = iSpriteContext.Current.iSpriteTempPath + Path.GetRandomFileName() + ".html";
            StringBuilder itemsb = new StringBuilder();
            int beginindex = pagesize * (pageindex - 1);
            int index = 0;
            int len = 120;

            PackageItem packageItem;
            foreach(KeyValuePair<string, PackageItem> item in packagelist)
            {
                if (index++ >= beginindex)
                {
                    packageItem = item.Value;
                    string description = packageItem.Description;
                    if (description.Length > len)
                    {
                        description = description.Substring(0, len) + "...";
                    }
                    string ver = packageItem.Version;
                    if (!string.IsNullOrEmpty(ver))
                    {
                        ver = " (V" + ver + ")";
                    }

                    if (!string.IsNullOrEmpty(key))
                    {
                        itemsb.AppendFormat(itemhtmltemplate,
                            Regex.Replace(packageItem.Name, key, "<em>" + key + "</em>", RegexOptions.IgnoreCase) + ver,
                            Regex.Replace(description, key, "<em>" + key + "</em>", RegexOptions.IgnoreCase),
                            Utility.FormatFileSize((ulong)packageItem.Size),
                            string.Format("http://www.ithemesky.com/dl.aspx?PID={0}", HttpUtility.UrlEncode(packageItem.PackageID, Encoding.UTF8)),
                            packageItem.Category,
                            HttpUtility.UrlEncode(packageItem.Identifier, Encoding.UTF8).TrimEnd('.'),
                            HttpUtility.UrlEncode(packageItem.Name, Encoding.UTF8).TrimEnd('.'),
                            HttpUtility.UrlEncode(packageItem.Description, Encoding.UTF8).TrimEnd('.')
                            );
                    }
                    else
                    {
                        itemsb.AppendFormat(itemhtmltemplate,
                            packageItem.Name + ver,
                            description,
                            Utility.FormatFileSize((ulong)packageItem.Size),
                            string.Format("http://www.ithemesky.com/dl.aspx?PID={0}", HttpUtility.UrlEncode(packageItem.PackageID, Encoding.UTF8)),
                            packageItem.Category,
                            HttpUtility.UrlEncode(packageItem.Identifier, Encoding.UTF8).TrimEnd('.'),
                            HttpUtility.UrlEncode(packageItem.Name, Encoding.UTF8).TrimEnd('.'),
                            HttpUtility.UrlEncode(packageItem.Description.Replace("\"", ""), Encoding.UTF8).TrimEnd('.')
                            );
                    }
                }

                if (index > beginindex + pagesize)
                {
                    break;
                }
            }
            string htmlcontent = htmltemplate.Replace("{Content}", itemsb.ToString());
            File.WriteAllText(filePath,htmlcontent,Encoding.UTF8);
            return filePath;
        }
        #endregion

        #region 类别名称格式化
        /// <summary>
        /// 类别名称格式化
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string FormatCatalog(string input)
        {
            string name = string.Empty;
            switch (input)
            {
                case "Administration":
                    name = "Administration";
                    break;
                case "App Addons":
                case "App Addons (Iconoclasm)":
                    name = "App Addons";
                    break;
                case "Archiving":
                    name = "Archiving";
                    break;
                case "Carrier Bundles":
                    name = "Carrier Bundles";
                    break;
                case "Cydgets (Lock)":
                case "Cydgets_(Lock)":
                    name = "Cydgets (Lock)";
                    break;
                case "Data Storage":
                case "Data_Storage":
                    name = "Data Storage";
                    break;
                case "Development":
                case "Developer":
                    name = "Development";
                    break;
                case "Dictionaries":
                case "Hunspell_Dictionaries":
                    name = "Dictionaries";
                    break;
                case "eBooks":
                    name = "eBooks";
                    break;
                case "Education":
                    name = "Education";
                    break;
                case "Entertainment":
                    name = "Entertainment";
                    break;
                case "Games":
                    name = "Games";
                    break;
                case "Java":
                    name = "Java";
                    break;
                case "Keyboards":
                    name = "Keyboards";
                    break;
                case "Localization":
                    name = "Localization";
                    break;
                case "LockInfo Addons":
                    name = "LockInfo Addons";
                    break;
                case "Media":
                case "Multimedia":
                    name = "Multimedia";
                    break;
                case "Messages":
                case "Messaging":
                case "SMS Application":
                    name = "Messaging";
                    break;
                case "Navigation":
                    name = "Navigation";
                    break;
                case "Networking":
                    name = "Networking";
                    break;
                case "Packaging":
                case "Productivity":
                case "Repositories":
                    name = "Packaging";
                    break; 
                case "SBSettings Addons":
                    name = "SBSettings Addons";
                    break;
                case "Scripting":
                    name = "Scripting";
                    break;
                case "Security":
                    name = "Security";
                    break;
                case "Social":
                    name = "Social";
                    break;
                case "Ringtones":
                case "Soundboards":
                    name = "Ringtones";
                    break;
                case "System":
                    name = "System";
                    break;
                case "Terminal Support":
                case "Terminal_Support":
                    name = "Terminal Support";
                    break;
                case "Text Editors":
                case "Text_Editors":
                    name = "Text Editors";
                    break;
                case "Themes":
                case "Themes (Addons)":
                case "Themes (Apps)":
                case "Themes (Battery)":
                case "Themes (Complete)":
                case "Themes (Dialer)":
                case "Themes (Dock)":
                case "Themes (Icy)":
                case "Themes (Keyboard)":
                case "Themes (Locksceen)":
                case "Themes (LockScreen)":
                case "Themes (PogoPlank)":
                case "Themes (SBSettings)":
                case "Themes (SMS)":
                case "Themes (Sounds)":
                case "Themes (Springboard)":
                case "Themes (SpringBoard)":
                case "Themes (SummerBoard)":
                case "Themes (System)":
                case "Themes (Video)":
                case "Themes (Weather)":
                case "Themes (Widgets)":
                case "Themes (WinterBoard)":
                    name = "Themes";
                    break;
                case "Toys":
                    name = "Toys";
                    break;
                case "Utilities":
                case "Utility":
                    name = "Utilities";
                    break;
                case "Wallpaper":
                case "Wallpapers":
                case "Wallpaper (iPad)":
                    name = "Wallpaper";
                    break;
                case "Site-Specific Apps":
                case "WebClips":
                case "WebApps":
                    name = "WebClips";
                    break;
                case "Widgets":
                    name = "Widgets";
                    break;
                case "X_Window":
                    name = "X_Window";
                    break;
                case "BossPaper Addons":
                case "FontSwap":
                case "Tweaks":
                    name = "Others";
                    break;
                default:
                    if (input.StartsWith("Themes"))
                    {
                        name = "Themes";
                    }
                    else
                    {
                        name = "Others";
                    }
                    break;
            }
            return name;
        }
        #endregion
    }
}
