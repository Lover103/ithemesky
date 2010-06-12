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
        private List<string> m_AppNames;
        string m_downqueuefile = string.Empty;
        string m_AptDownloadFolder = string.Empty;

        #region html模板
        const string itemhtmltemplate = @"
        <li>
			<div class='appIcon'><img src='http://img1.qq.com/pcdesk/pics/14661/14661865.gif' alt='{0}' width='32' height='32' /></div>
			<dl class='appIntro'>
				<dt>{0}</dt>
				<dd>{1} </dd>
			</dl>
			<ul class='appInfo'>
				<li class='size'>{2}</li>
				<li class='rate'>
					<img src='icons/ico_star_full.png' /><img src='icons/ico_star_full.png' /><img src='icons/ico_star_full.png' /><img src='icons/ico_star_full.png' /><img src='icons/ico_star_half.png' />
					<p>4.5 Stars <a href='#'>Rate</a></p>
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
<body>
<div class='appList'>
	<ul>
{Content}
    </ul>
</div>
</body>
</html>
";
        #endregion

        #endregion

        internal string DownQueueFile
        {
            get
            {
                return m_downqueuefile;
            }
        }

        internal string AptDownloadFolder
        {
            get
            {
                return m_AptDownloadFolder;
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

            m_AptFolder = iSpriteContext.Current.iSpriteApplicationDataPath + "Apt\\";
            if (!Directory.Exists(m_AptFolder))
            {
                Directory.CreateDirectory(m_AptFolder);
            }

            m_AptSourceCfg = m_AptFolder + "SourceCfg.ini";
            m_AptPackagesFolder = m_AptFolder + "\\Packages\\";
            if (!Directory.Exists(m_AptPackagesFolder))
            {
                Directory.CreateDirectory(m_AptPackagesFolder);
            }


            m_downqueuefile = m_AptFolder + "downqueue.xml";
            m_AptDownloadFolder = m_AptFolder + "\\download\\";
            if (!Directory.Exists(m_AptPackagesFolder))
            {
                Directory.CreateDirectory(m_AptPackagesFolder);
            }

            //ReleaseUrlSuffixList
            ReleaseUrlSuffixList = new List<string>();
            ReleaseUrlSuffixList.Add("dists/stable/Release");
            ReleaseUrlSuffixList.Add("Release.bz2");
            ReleaseUrlSuffixList.Add("Release.gz");
            ReleaseUrlSuffixList.Add("Release");
            ReleaseUrlSuffixList.Add("dists/stable/Release.bz2");
            ReleaseUrlSuffixList.Add("dists/stable/Release.gz");
            ReleaseUrlSuffixList.Add("dists/stable/Release");

            ReleaseUrlSuffixList.Add("dists/tangelo/Release.bz2");
            ReleaseUrlSuffixList.Add("dists/tangelo/Release.gz");
            ReleaseUrlSuffixList.Add("dists/tangelo/Release");


            ReleaseUrlSuffixList.Add("dists/tangelo-3.7/Release.bz2");
            ReleaseUrlSuffixList.Add("dists/tangelo-3.7/Release.gz");
            ReleaseUrlSuffixList.Add("dists/tangelo-3.7/Release");

            ReleaseUrlSuffixList.Add("dists/hnd/Release");

            //PackagesUrlSuffixList
            PackagesUrlSuffixList = new List<string>();
            PackagesUrlSuffixList.Add("dists/hnd/main/binary-iphoneos-arm/Packages.bz2");
            PackagesUrlSuffixList.Add("dists/hnd/main/binary-iphoneos-arm/Packages");
            PackagesUrlSuffixList.Add("Packages.bz2");
            PackagesUrlSuffixList.Add("Packages.gz");
            PackagesUrlSuffixList.Add("Packages");
            PackagesUrlSuffixList.Add("main/binary-iphoneos-arm/Packages.bz2");
            PackagesUrlSuffixList.Add("main/binary-iphoneos-arm/Packages");
            PackagesUrlSuffixList.Add("dists/stable/main/binary-iphoneos-arm/Packages.bz2");
            PackagesUrlSuffixList.Add("dists/stable/main/binary-iphoneos-arm/Packages.gz");
            PackagesUrlSuffixList.Add("dists/stable/main/binary-iphoneos-arm/Packages");
            PackagesUrlSuffixList.Add("dists/tangelo/main/binary-iphoneos-arm/Packages.bz2");
            PackagesUrlSuffixList.Add("dists/tangelo/main/binary-iphoneos-arm/Packages.gz");
            PackagesUrlSuffixList.Add("dists/tangelo/main/binary-iphoneos-arm/Packages");

            PackagesUrlSuffixList.Add("dists/tangelo-3.7/main/binary-iphoneos-arm/Packages.bz2");
            PackagesUrlSuffixList.Add("dists/tangelo-3.7/main/binary-iphoneos-arm/Packages.gz");
            PackagesUrlSuffixList.Add("dists/tangelo-3.7/main/binary-iphoneos-arm/Packages");
        }
        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        public void InitAppData()
        {
            LoadCydiaSource();
            LoadCydiaPackages();
        }

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
                GetAptSourceCfg(m_RepositoryList);
            }

            foreach (string url in urllist)
            {
                CheckAptSourceCfg(url, m_RepositoryList);
            }

            LoadRepository2PathList();

            foreach (KeyValuePair<string, RepositoryInfo> item in m_RepositoryList)
            {
                if (string.IsNullOrEmpty(item.Value.APTCachedPackagesURL))
                {
                    if (m_Repository2PathList.ContainsKey(item.Value.Name))
                    {
                        item.Value.APTCachedPackagesURL = "http://" + m_Repository2PathList[item.Value.Name].Replace("_", "/");
                    }
                }
                if (string.IsNullOrEmpty(item.Value.APTCachedReleaseURL) && !string.IsNullOrEmpty(item.Value.APTCachedPackagesURL))
                {
                    item.Value.APTCachedReleaseURL =
                        item.Value.APTCachedPackagesURL.Replace("/main/binary-iphoneos-arm/Packages", "/Release");
                }
            }

            SaveAptSourceCfg(m_RepositoryList);
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
            //List<string> c = new List<string>();

            CatalogItem catalogitem;
            Dictionary<string, PackageItem> packages = new Dictionary<string, PackageItem>();
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
                                if ( !packages.ContainsKey(item.PackageID))
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
                                    item.AdditionalInfoURL = host;
                                    packages.Add(item.PackageID, item);

                                    if (item.Category != "Themes" //主题包太多了
                                        && !m_AppNames.Contains(item.Name))
                                    {
                                        m_AppNames.Add(item.Name);//搜索用的
                                    }
                                }

                                //if (!c.Contains(item.Category))
                                //{
                                //    c.Add(item.Category);
                                //}
                            }
                        }
                    }
                }
            }
            //c.Sort();

            //StringBuilder sb = new StringBuilder();
            //foreach (string name in c)
            //{
            //    sb.AppendFormat("case \"{0}\":\r\n name=\"{0}\";\r\nbreak;\r\n", name);
            //}

            //排序
            List<string> appnamelist = new List<string>();
            foreach (KeyValuePair<string, PackageItem> item in packages)
            {
                appnamelist.Add(item.Key);
            }
            appnamelist.Sort();
            foreach (string packageid in appnamelist)
            {
                PackageItem item;
                if (packages.TryGetValue(packageid, out item))
                {
                    m_Packagelist.Add(packageid, item);
                }
            }

            m_AppNames.Sort();
        }
        #endregion

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

        public Dictionary<string, PackageItem> SearchPackages(Dictionary<string, PackageItem> packages, string catalogName,string key)
        {
            key = key.ToLower();
            Dictionary<string, PackageItem> list = new Dictionary<string, PackageItem>();
            foreach (KeyValuePair<string, PackageItem> item in packages)
            {
                if (
                    (string.IsNullOrEmpty(catalogName) || catalogName == "All Packages" || item.Value.Category.Equals(catalogName, StringComparison.CurrentCultureIgnoreCase))
                    && (item.Value.Name.ToLower().Contains(key) || item.Value.Description.ToLower().Contains(key))
                    )
                {
                    list.Add(item.Key, item.Value);
                }
            }
            return list;
        }

        #region 添加Cydia源
        /// <summary>
        /// 添加Cydia源
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool AddCydiaSource(string url)
        { 
            RepositoryInfo repositoryInfo;
            string APTCachedReleaseURL=string.Empty;
            if (GetRepositoryInfoByUrl(url, ref APTCachedReleaseURL, out repositoryInfo))
            {
                if (!m_RepositoryList.ContainsKey(repositoryInfo.Name))
                {
                    m_RepositoryList.Add(repositoryInfo.Name, repositoryInfo);
                }
                return true;
            }
            else
            {
                return false;
            }
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
                //写成文件保存到m_AptPackagesFolder
                string fileName =  repInfo.APTCachedPackagesURL.Substring(7).Replace("/","_");
                File.WriteAllText(m_AptPackagesFolder +fileName, packetcontent,Encoding.UTF8);
                //保存到iPhone的m_CydiaPackagesFolder
                m_iPhoneDevice.Copy2iPhone(m_AptPackagesFolder + fileName, m_CydiaPackagesFolder + fileName);
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
        private void CheckAptSourceCfg(string url, Dictionary<string, RepositoryInfo> replist)
        {
            string host = new Uri(url).Host;
            bool flag = replist.ContainsKey(host);
            if (!flag)
            {
                RepositoryInfo info = new RepositoryInfo();
                info.Name = host;
                info.URL = url;
                info.APTDownloadBaseURL = url;

                if (!replist.ContainsKey(info.Name))
                {
                    replist.Add(info.Name, info);
                }
            }
        }
        #endregion

        #region 保存源配置文件（SourceCfg.ini）
        /// <summary>
        /// 保存源配置文件（SourceCfg.ini）
        /// </summary>
        /// <param name="replist"></param>
        private void SaveAptSourceCfg(Dictionary<string, RepositoryInfo> replist)
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, RepositoryInfo> info in replist)
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
        bool GetAptSourceCfg(Dictionary<string, RepositoryInfo> replist)
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
                    if (!replist.ContainsKey(info.Name))
                    {
                        replist.Add(info.Name,info);
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
            out RepositoryInfo repositoryInfo)
        {
            repositoryInfo = new RepositoryInfo();
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
                repositoryInfo.APTDownloadBaseURL = BaseURL;
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

        public string Packages2Html(Dictionary<string, PackageItem> packagelist,int pagesize,int pageindex,string key)
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

            RepositoryInfo repInfo;

            foreach(KeyValuePair<string, PackageItem> item in packagelist)
            {
                if (index++ >= beginindex)
                {
                    string description = item.Value.Description;
                    if (description.Length > len)
                    {
                        description = description.Substring(0, len) + "...";
                    }

                    if (m_RepositoryList.TryGetValue(item.Value.AdditionalInfoURL, out repInfo))
                    {
                        if (!string.IsNullOrEmpty(key))
                        {
                            itemsb.AppendFormat(itemhtmltemplate,
                                Regex.Replace(item.Value.Name, key, "<em>" + key + "</em>", RegexOptions.IgnoreCase),
                                Regex.Replace(description, key, "<em>" + key + "</em>", RegexOptions.IgnoreCase),
                                Utility.FormatFileSize((ulong)item.Value.Size),
                                string.Format("http://www.ithemesky.com/dl.aspx?url={0}{1}&name={2}",
                                                HttpUtility.UrlDecode(repInfo.APTDownloadBaseURL, Encoding.UTF8),
                                                HttpUtility.UrlDecode(item.Value.DownloadURL, Encoding.UTF8),
                                                Path.GetFileName(item.Value.DownloadURL)
                                               )
                                );
                        }
                        else
                        {
                            itemsb.AppendFormat(itemhtmltemplate,
                                item.Value.Name,
                                description,
                                Utility.FormatFileSize((ulong)item.Value.Size),
                                string.Format("http://www.ithemesky.com/dl.aspx?url={0}{1}&name={2}",
                                                HttpUtility.UrlDecode(repInfo.APTDownloadBaseURL,Encoding.UTF8),
                                                HttpUtility.UrlDecode(item.Value.DownloadURL, Encoding.UTF8), 
                                                Path.GetFileName(item.Value.DownloadURL)
                                               )
                                );
                        }
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
