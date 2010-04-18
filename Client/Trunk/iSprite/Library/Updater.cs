using System;
using System.Collections.Generic;

using System.Text;
using System.Windows.Forms;
using System.Data;
using Manzana;
using System.IO;
using System.Diagnostics;

namespace iSprite
{
    internal class Updater
    {
        internal string downurl = string.Empty;
        internal event FileProgressHandler OnProgressHandler;
        internal event MessageHandler OnMessage;


        private void RaiseMessageHandler(object sender, string Message, MessageTypeOption messageType)
        {
            if (OnMessage != null)
            {
                OnMessage(sender, Message, messageType);
            }
        }

        internal void CheckNewVer()
        {
            try
            {
                string path = iSpriteContext.Current.iSpriteApplicationDataPath;
                string updatexmlpath = path + "\\UpdateFiles\\";
                if (!Directory.Exists(updatexmlpath))
                {
                    Directory.CreateDirectory(updatexmlpath);
                }
                updatexmlpath = updatexmlpath + "update.xml";
                if (Utility.DownloadFile(iSpriteContext.Current.UpdateUrl, updatexmlpath))
                {
                    DataSet ds = new DataSet();
                    ds.ReadXml(updatexmlpath);
                    if (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 1)
                    {
                        DataRow row = ds.Tables[0].Rows[0];
                        string serverver = row["Ver"].ToString();
                        downurl = row["downurl"].ToString();
                        if (new Version(serverver) > new Version(iSpriteContext.Current.CurrentVersion))
                        {
                            RaiseMessageHandler(this,
                                string.Format("The lastest iSpirit version is V{0}, Would you to upgrade it ?", serverver),
                                MessageTypeOption.Upgrade
                                );
                        }
                    }
                }
            }
            catch
            { 
            }
        }

        internal bool DoUpdate()
        { 
            //下载文件
            //下载更新程序
            string path = iSpriteContext.Current.iSpriteApplicationDataPath;
            string iSpriteUpgradePath = path + "\\iSpiritUpgrade.exe";
            if (Utility.DownloadFile(downurl + "\\iSpiritUpgrade.exe", iSpriteUpgradePath, OnProgressHandler))
            {
                string iSpritePath = path + "\\iSpirit.zip";
                if (Utility.DownloadFile(downurl + "\\iSpirit.zip", iSpritePath, OnProgressHandler))
                {
                    if (ZipHelper.UnZip(iSpritePath, path + "\\UpdateFiles\\") > 0)
                    {
                        string appName = "iSpirit";
                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.FileName = iSpriteUpgradePath;
                        startInfo.Arguments = appName + ";" + Application.StartupPath;
                        startInfo.WindowStyle = ProcessWindowStyle.Hidden;

                        Process.Start(startInfo);

                    }
                }
            }
            return false;
        }
    }
}
