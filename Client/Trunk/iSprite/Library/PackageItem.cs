using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Text;
using System.Web;
using System.IO;   

namespace iSprite
{
    public class InstalledDebItem
    {
        public string DebID
        {
            get
            {
                return Package + "-" + Version;
            }
        }

        public string Name { get; set; }
        public string Package { get; set; }
        public string Status { get; set; }
        public string Installed_Size { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public bool IsSystemDeb { get; set; }
    }

    public class CatalogItem
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }

    public class AptData : Hashtable
    {
        public bool ContainsTag(string TagName)
        {
            return ContainsKey(TagName);
        }

        public string GetTagValue(string TagName)
        {
            return GetTagValue(TagName, string.Empty);
        }

        public string GetTagValue(string TagName,string defaultValue)
        {
            if (ContainsTag(TagName))
            {
                return this[TagName].ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public string[] Tags
        {
            get
            {
                if (ContainsKey("Tag"))
                {
                    return this["Tag"].ToString().Split(new char[] { ',' });
                }
                return null;
            }
        }
    }

    public class RepositoryInfo
    {
        int _ItemCount = 0;
        public RepositoryInfo()
        { 
        }

        public string Identifier
        { 
            get 
            {
                return new Uri(URL).Host;
            }
        }
        private int ItemCount { 
            get
            {
                return _ItemCount;
            }
            set
            { 
                _ItemCount = value;
            } 
        }
        public string Name { get; set; }
        public string URL { get; set; }
        public string Category { get; set; }
        public string Contact { get; set; }
        public string Description { get; set; }
        public string Maintaner { get; set; }
        public string IconURL { get; set; }
        public string InfoURL { get; set; }
        public bool? LastUpdateError { get; set; }
        public bool? Lock { get; set; }
        private int? Type { get; set; }
        public string APTCachedPackagesURL { get; set; }
        public string APTCachedReleaseURL { get; set; }
        public string APTDownloadBaseURL { get; set; }
        public string LastUpdate { get; set; }


        public override string ToString ()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Name: {0}\r\n", Name == null ? "" : Name);
            sb.AppendFormat("URL: {0}\r\n", URL == null ? "" : URL);
            sb.AppendFormat("Category: {0}\r\n", Category == null ? "" : Category);
            sb.AppendFormat("Description: {0}\r\n", Description == null ? "" : Description);
            sb.AppendFormat("Maintaner: {0}\r\n", Maintaner == null ? "" : Maintaner);
            sb.AppendFormat("APTCachedPackagesURL: {0}\r\n", APTCachedPackagesURL == null ? "" : APTCachedPackagesURL);
            sb.AppendFormat("APTCachedReleaseURL: {0}\r\n", APTCachedReleaseURL == null ? "" : APTCachedReleaseURL);
            sb.AppendFormat("APTDownloadBaseURL: {0}\r\n", APTDownloadBaseURL == null ? "" : APTDownloadBaseURL);
            sb.AppendFormat("LastUpdate: {0}\r\n", LastUpdate == null ? "" : LastUpdate);

            return sb.ToString();
        }
    }
    public class PackageItem : INotifyPropertyChanging, INotifyPropertyChanged
    {
        /// <summary>
        /// 最终的下载地址
        /// </summary>
        public string FinalDownloadURL(string baseURL)
        {
            return string.Format("http://www.ithemesky.com/dl.aspx?url={0}{1}&name={2}&PID={3}",
                                                  HttpUtility.UrlDecode(baseURL, Encoding.UTF8),
                                                  HttpUtility.UrlDecode(DownloadURL, Encoding.UTF8),
                                                  Path.GetFileName(DownloadURL),
                                                  HttpUtility.UrlDecode(PackageID, Encoding.UTF8)
                                                 );
        }

        public string PackageID
        {
            get
            {
                return Name + "-" + _Identifier + "-" + _Version;
            }
        }
        /// <summary>
        /// 唯一ID，和数据库关联
        /// </summary>
        private long _UID;
        /// <summary>
        /// 软件名称
        /// </summary>
        private string _Name;
        /// <summary>
        /// 软件版本
        /// </summary>
        private string _Version;
        /// <summary>
        /// 类别
        /// </summary>
        private string _Category;
        /// <summary>
        /// 作者联系方式
        /// </summary>
        private string _Contact;
        /// <summary>
        /// 固件版本要求
        /// </summary>
        private string _Pre_Depends;
        /// <summary>
        /// 依赖软件
        /// </summary>
        private string _Dependencies;
        /// <summary>
        /// 冲突软件
        /// </summary>
        private string _Conflicts;
        /// <summary>
        /// 软件描述信息
        /// </summary>
        private string _Description;
        /// <summary>
        /// 软件下载地址
        /// </summary>
        private string _DownloadURL;
        /// <summary>
        /// 软件标识符
        /// </summary>
        private string _Identifier;
        /// <summary>
        /// 维护人
        /// </summary>
        private string _Maintaner;
        /// <summary>
        /// 大小
        /// </summary>
        private long? _Size;
        /// <summary>
        /// 文件hash值
        /// </summary>
        private string _Hash;
        /// <summary>
        /// 主页
        /// </summary>
        private string _Homepage;
        /// <summary>
        /// 等级
        /// </summary>
        private string _Priority;
        /// <summary>
        /// 标签
        /// </summary>
        private string _Tags;

        private bool? _AdditionalDataLoaded;
        private string _AdditionalInfoURL;
        private DateTime? _Date;
        private bool? _Essential;
        private string _IconURL;
        private bool? _JailbreakRequired;
        private string _MinOsRequired;
        private string _MoreInfoURL;
        private double? _Rating;
        private DateTime? _RatingLastUpdate;
        private int? _Type;
        
        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);
        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;

        protected virtual void SendPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChangedEventHandler propertyChangedEvent = this.PropertyChanged;
                if (propertyChangedEvent != null)
                {
                    propertyChangedEvent(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }

        protected virtual void SendPropertyChanging()
        {
            if (this.PropertyChanged != null)
            {
                PropertyChangingEventHandler propertyChangingEvent = this.PropertyChanging;
                if (propertyChangingEvent != null)
                {
                    propertyChangingEvent(this, emptyChangingEventArgs);
                }
            }
        }


        /// <summary>
        /// 唯一标识
        /// </summary>
        public long UID
        {
            get
            {
                return this._UID;
            }
            set
            {
                if (this._UID != value)
                {
                    this.SendPropertyChanging();
                    this._UID = value;
                    this.SendPropertyChanged("UID");
                }
            }
        }

        public bool? AdditionalDataLoaded
        {
            get
            {
                return this._AdditionalDataLoaded;
            }
            set
            {
                if (!this._AdditionalDataLoaded.Equals(value))
                {
                    this.SendPropertyChanging();
                    this._AdditionalDataLoaded = value;
                    this.SendPropertyChanged("AdditionalDataLoaded");
                }
            }
        }

        public string AdditionalInfoURL
        {
            get
            {
                return this._AdditionalInfoURL;
            }
            set
            {
                if (!string.Equals(this._AdditionalInfoURL, value))
                {
                    this.SendPropertyChanging();
                    this._AdditionalInfoURL = value;
                    this.SendPropertyChanged("AdditionalInfoURL");
                }
            }
        }

        public string Category
        {
            get
            {
                return this._Category;
            }
            set
            {
                if (!string.Equals(this._Category, value))
                {
                    this.SendPropertyChanging();
                    this._Category = value;
                    this.SendPropertyChanged("Category");
                }
            }
        }

        public string Contact
        {
            get
            {
                return this._Contact;
            }
            set
            {
                if (!string.Equals(this._Contact, value))
                {
                    this.SendPropertyChanging();
                    this._Contact = value;
                    this.SendPropertyChanged("Contact");
                }
            }
        }

        public DateTime? Date
        {
            get
            {
                return this._Date;
            }
            set
            {
                if (!this._Date.Equals(value))
                {
                    this.SendPropertyChanging();
                    this._Date = value;
                    this.SendPropertyChanged("[Date]");
                }
            }
        }

        public string Dependencies
        {
            get
            {
                return this._Dependencies;
            }
            set
            {
                if (!string.Equals(this._Dependencies, value))
                {
                    this.SendPropertyChanging();
                    this._Dependencies = value;
                    this.SendPropertyChanged("Dependencies");
                }
            }
        }

        public string Pre_Depends
        {
            get
            {
                return this._Pre_Depends;
            }
            set
            {
                if (!string.Equals(this._Pre_Depends, value))
                {
                    this.SendPropertyChanging();
                    this._Pre_Depends = value;
                    this.SendPropertyChanged("Pre_Depends");
                }
            }
        }

        public string Conflicts
        {
            get
            {
                return this._Conflicts;
            }
            set
            {
                if (!string.Equals(this._Conflicts, value))
                {
                    this.SendPropertyChanging();
                    this._Conflicts = value;
                    this.SendPropertyChanged("Conflicts");
                }
            }
        }

        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                if (!string.Equals(this._Description, value))
                {
                    this.SendPropertyChanging();
                    this._Description = value;
                    this.SendPropertyChanged("Description");
                }
            }
        }

        public string DownloadURL
        {
            get
            {
                return this._DownloadURL;
            }
            set
            {
                if (!string.Equals(this._DownloadURL, value))
                {
                    this.SendPropertyChanging();
                    this._DownloadURL = value;
                    this.SendPropertyChanged("DownloadURL");
                }
            }
        }

        public bool? Essential
        {
            get
            {
                return this._Essential;
            }
            set
            {
                if (!this._Essential.Equals(value))
                {
                    this.SendPropertyChanging();
                    this._Essential = value;
                    this.SendPropertyChanged("Essential");
                }
            }
        }

        public string Hash
        {
            get
            {
                return this._Hash;
            }
            set
            {
                if (!string.Equals(this._Hash, value))
                {
                    this.SendPropertyChanging();
                    this._Hash = value;
                    this.SendPropertyChanged("Hash");
                }
            }
        }

        public string Homepage
        {
            get
            {
                return this._Homepage;
            }
            set
            {
                if (!string.Equals(this._Homepage, value))
                {
                    this.SendPropertyChanging();
                    this._Homepage = value;
                    this.SendPropertyChanged("Homepage");
                }
            }
        }

        public string IconURL
        {
            get
            {
                return this._IconURL;
            }
            set
            {
                if (!string.Equals(this._IconURL, value))
                {
                    this.SendPropertyChanging();
                    this._IconURL = value;
                    this.SendPropertyChanged("IconURL");
                }
            }
        }

        public string Identifier
        {
            get
            {
                return this._Identifier;
            }
            set
            {
                if (!string.Equals(this._Identifier, value))
                {
                    this.SendPropertyChanging();
                    this._Identifier = value;
                    this.SendPropertyChanged("Identifier");
                }
            }
        }

        public bool? JailbreakRequired
        {
            get
            {
                return this._JailbreakRequired;
            }
            set
            {
                if (!this._JailbreakRequired.Equals(value))
                {
                    this.SendPropertyChanging();
                    this._JailbreakRequired = value;
                    this.SendPropertyChanged("JailbreakRequired");
                }
            }
        }

        public string Maintaner
        {
            get
            {
                return this._Maintaner;
            }
            set
            {
                if (!string.Equals(this._Maintaner, value))
                {
                    this.SendPropertyChanging();
                    this._Maintaner = value;
                    this.SendPropertyChanged("Maintaner");
                }
            }
        }

        public string MinOsRequired
        {
            get
            {
                return this._MinOsRequired;
            }
            set
            {
                if (!string.Equals(this._MinOsRequired, value))
                {
                    this.SendPropertyChanging();
                    this._MinOsRequired = value;
                    this.SendPropertyChanged("MinOsRequired");
                }
            }
        }

        public string MoreInfoURL
        {
            get
            {
                return this._MoreInfoURL;
            }
            set
            {
                if (!string.Equals(this._MoreInfoURL, value))
                {
                    this.SendPropertyChanging();
                    this._MoreInfoURL = value;
                    this.SendPropertyChanged("MoreInfoURL");
                }
            }
        }

        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                if (!string.Equals(this._Name, value))
                {
                    this.SendPropertyChanging();
                    this._Name = value;
                    this.SendPropertyChanged("Name");
                }
            }
        }

        public string Priority
        {
            get
            {
                return this._Priority;
            }
            set
            {
                if (!string.Equals(this._Priority, value))
                {
                    this.SendPropertyChanging();
                    this._Priority = value;
                    this.SendPropertyChanged("Priority");
                }
            }
        }

        public double? Rating
        {
            get
            {
                return this._Rating;
            }
            set
            {
                if (!this._Rating.Equals(value))
                {
                    this.SendPropertyChanging();
                    this._Rating = value;
                    this.SendPropertyChanged("Rating");
                }
            }
        }

        public DateTime? RatingLastUpdate
        {
            get
            {
                return this._RatingLastUpdate;
            }
            set
            {
                if (!this._RatingLastUpdate.Equals(value))
                {
                    this.SendPropertyChanging();
                    this._RatingLastUpdate = value;
                    this.SendPropertyChanged("RatingLastUpdate");
                }
            }
        }  

        public long? Size
        {
            get
            {
                return this._Size;
            }
            set
            {
                if (!this._Size.Equals(value))
                {
                    this.SendPropertyChanging();
                    this._Size = value;
                    this.SendPropertyChanged("Size");
                }
            }
        }

        public string Tags
        {
            get
            {
                return this._Tags;
            }
            set
            {
                if (!string.Equals(this._Tags, value))
                {
                    this.SendPropertyChanging();
                    this._Tags = value;
                    this.SendPropertyChanged("Tags");
                }
            }
        }

        public int? Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                if (!this._Type.Equals(value))
                {
                    this.SendPropertyChanging();
                    this._Type = value;
                    this.SendPropertyChanged("Type");
                }
            }
        }

        public string Version
        {
            get
            {
                return this._Version;
            }
            set
            {
                if (!string.Equals(this._Version, value))
                {
                    this.SendPropertyChanging();
                    this._Version = value;
                    this.SendPropertyChanged("Version");
                }
            }
        }
    }
}
