using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using MyDownloader.Core;

namespace iSprite
{
    internal class AppDownloadUtility
    {
        protected AppHelper m_appHelper;
        public AppDownloadUtility(AppHelper appHelper)
        {
            m_appHelper = appHelper;
        }

        /// <summary>
        /// 标记为开始下载
        /// </summary>
        /// <param name="todownlist"></param>
        public void Marked2Begin(List<DownLoadItemInfo> todownlist)
        {
            foreach (DownLoadItemInfo item in todownlist)
            {
                string localFile = item.SaveDir + item.FileName;
                string statusFile = Path.ChangeExtension(localFile, ".d")
                    .Replace(m_appHelper.AptDownloadFolder, m_appHelper.AptDownloadStatusFolder);

                File.WriteAllText(statusFile, item.Hash, Encoding.UTF8);//表示文件下载未完成
            }
        }
        /// <summary>
        /// 标记为下载完成
        /// </summary>
        /// <param name="localFile"></param>
        public bool Marked2Finished(Downloader d)
        {
            string localFile = d.LocalFile;
            string statusFile = Path.ChangeExtension(localFile, ".d")
                .Replace(m_appHelper.AptDownloadFolder, m_appHelper.AptDownloadStatusFolder);

            if (File.Exists(statusFile))
            {
                string content = File.ReadAllText(statusFile, Encoding.UTF8);
                string hash = CalcFileHash(localFile);
                if (content != string.Empty && content == hash)
                {
                    File.Move(statusFile, Path.ChangeExtension(statusFile, ".f"));//表示下载完成
                    return true;
                }
                else
                {
                    d.State = DownloaderState.EndedWithError;
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        public void RemoveFiles(Downloader d)
        {
            string localFile = d.LocalFile;
            File.Delete(localFile);
            string statusFile = Path.ChangeExtension(localFile, ".d")
                .Replace(m_appHelper.AptDownloadFolder, m_appHelper.AptDownloadStatusFolder);

            if (File.Exists(statusFile))
            {
                File.Delete(statusFile);
            }
            else
            {
                statusFile = Path.ChangeExtension(localFile, ".f");
                if (File.Exists(statusFile))
                {
                    File.Delete(statusFile);
                }
            }
        }
        /// <summary>
        /// 计算文件hash值
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        internal string CalcFileHash(string FilePath)
        {
            try
            {
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                byte[] hash;
                using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096))
                {
                    hash = md5.ComputeHash(fs);
                }
                return BitConverter.ToString(hash).Replace("-","").ToLower();
            }
            catch
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// 获取主程序路径
        /// </summary>
        /// <param name="localFile"></param>
        /// <returns></returns>
        internal string GetMainAppFullFileName(string localFile)
        {
            string mainAppfilename = string.Empty;
            int index = localFile.LastIndexOf("+");
            if (index == -1)
            {
                mainAppfilename = localFile;
            }
            else
            {
                mainAppfilename = localFile.Substring(0, index) + ".deb";
            }
            return mainAppfilename;
        }

        /// <summary>
        /// 判断当前主程序相关的依赖文件和自身文件是否都成功下载
        /// </summary>
        /// <param name="mainAppfilename"></param>
        /// <returns></returns>
        internal bool AllAppFilesIsFinished(string mainAppfilename)
        {
            string fileName = Path.GetFileName(mainAppfilename);
            if (Directory.GetFiles(m_appHelper.AptDownloadStatusFolder, fileName.Replace(".deb", "*.d")).Length == 0)
            {
                if (Directory.GetFiles(m_appHelper.AptDownloadStatusFolder, fileName.Replace(".deb", "*.f")).Length > 0)
                {
                    return true;
                }
            }
            return false;
        }

        internal void UpdataInstallStatus(string mainAppfilename, bool isfinish)
        {
            for (int i = 0; i < DownloadManager.Instance.Downloads.Count; i++)
            {
                if (new FileInfo(DownloadManager.Instance.Downloads[i].LocalFile).FullName
                    == new FileInfo(mainAppfilename).FullName)
                {
                    DownloadManager.Instance.Downloads[i].InstallCode = isfinish ? InstallState.InstallFinished : InstallState.Error;
                    break;
                }
            }
        }
    }
}
