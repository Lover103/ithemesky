using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;
using System.IO;
using Manzana;
using System.Diagnostics;

namespace iSprite
{

    internal partial class MyInstaller : iSpriteForm
    {
        static private MyInstaller InstallerBox;
        iPhoneFileDevice m_iPhoneDevice;
        InstallAppOption m_app;
        internal event FileProgressHandler OnProgressHandler;

        public MyInstaller()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
            this.ShowInTaskbar = false;
        }


        public static DialogResult Show(iPhoneFileDevice filedevice, InstallAppOption app, FileProgressHandler progressHandler)
        {
            InstallerBox = new MyInstaller();
            InstallerBox.m_iPhoneDevice = filedevice;
            InstallerBox.m_app = app;
            InstallerBox.OnProgressHandler = progressHandler;
            InstallerBox.r1.Text = string.Format(InstallerBox.r1.Text, app.ToString());
            InstallerBox.r2.Text = string.Format(InstallerBox.r2.Text, app.ToString());
            return InstallerBox.ShowDialog();
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!m_iPhoneDevice.IsJailbreak)
            {
                MessageHelper.ShowError("To install deb file ,you must Jailbreak your " + iSpriteContext.Current.AppleDeviceType + " first !");
                return;
            }

            //todo增加检测图标
            if (r1.Checked)
            {
                if (m_app == InstallAppOption.OpenSSH)
                {
                    string downloadurl = "http://update.ithemesky.com/openssh.zip";
                    string zippath = iSpriteContext.Current.iSpriteTempPath + "\\" + "openssh.zip";
                    if (Utility.DownloadFile(downloadurl, zippath, OnProgressHandler))
                    {
                        string tozippath = iSpriteContext.Current.iSpriteTempPath + "\\" + Path.GetRandomFileName() + "\\";
                        if (ZipHelper.UnZip(zippath, tozippath) > 0)
                        {
                            if (!m_iPhoneDevice.Copy2iPhone(tozippath, iSpriteContext.Current.iPhone_CydiaAutoInstallPath))
                            {
                                MessageHelper.ShowInfo("OpenSSH can not copy to " + iSpriteContext.Current.AppleDeviceType + ", please try again.");
                            }
                            else
                            {
                                MessageHelper.ShowInfo("OpenSSH has been copied to "
                                    + iSpriteContext.Current.AppleDeviceType
                                    + ", You must reboot your " + iSpriteContext.Current.AppleDeviceType + " to finish Installation.");
                            }
                        }
                        else
                        {
                            MessageHelper.ShowInfo("OpenSSH can not UnZip, please try again.");
                        }

                    }
                    else
                    {
                        MessageHelper.ShowInfo("OpenSSH can not download to your pc, please try again.");
                    }
                }
                else if (m_app == InstallAppOption.Winterboard)
                {
                    string downloadurl = Utility.GetWinterBoardUrl(m_iPhoneDevice);
                    string zippath = iSpriteContext.Current.iSpriteTempPath + "\\" + "winterboard.deb";
                    if (Utility.DownloadFile(downloadurl, zippath, OnProgressHandler))
                    {
                        if (!m_iPhoneDevice.Copy2iPhone(zippath, iSpriteContext.Current.iPhone_CydiaAutoInstallPath))
                        {
                            MessageHelper.ShowInfo("Winterboard can not copy to " + iSpriteContext.Current.AppleDeviceType + ", please try again.");
                        }
                        else
                        {
                            MessageHelper.ShowInfo("WinterBoard has been copied to " 
                                + iSpriteContext.Current.AppleDeviceType
                                + ", You must reboot your " + iSpriteContext.Current.AppleDeviceType + " to finish Installation.");
                        }
                    }
                    else
                    {
                        MessageHelper.ShowInfo("OpenSSH can not download to your pc, please try again.");
                    }
                }
            }
            else//Search From Cydia and install it
            {
                string url = string.Empty;
                if (m_app == InstallAppOption.OpenSSH)
                { 
                }
                else if (m_app == InstallAppOption.Winterboard)
                { 
                }
                if (url != string.Empty)
                {
                    Process.Start(url);
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        bool CheckInstallApp(string name)
        {
            string content = m_iPhoneDevice.GetFileText(iSpriteContext.Current.iPhone_InstallationPath);
            if (!content.Contains("<string>" + name.ToLower() + "</string>"))
            {
                MessageHelper.ShowInfo(name + " has not been installed, please install it first.");
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
