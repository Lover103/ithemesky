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
            this.HiddenForm();
            if (!m_iPhoneDevice.CheckJailbreak())
            {
                this.Close();
                return;
            }
            //todo增加检测图标
            if (r1.Checked)
            {
                if (!m_iPhoneDevice.CheckInstallApp("Cydia"))
                {
                    MessageHelper.ShowError("Please install Cydia to continue.");
                    this.Close();
                    return;
                }

                if (!m_iPhoneDevice.FileExists("/System/Library/LaunchDaemons/com.saurik.Cydia.Startup.plist"))
                {
                    MessageHelper.ShowError("Please enable Cydia LaunchDaemon to continue.");
                    this.Close();
                    return;
                }
                if (m_app == InstallAppOption.OpenSSH)
                {
                    string downloadurl = "http://update.ithemesky.com/openssh.zip";
                    string zippath = iSpriteContext.Current.iSpriteTempPath + "\\" + "openssh.zip";
                    if (Utility.DownloadFile(downloadurl, zippath, OnProgressHandler))
                    {
                        string tozippath = iSpriteContext.Current.iSpriteTempPath + "\\" + Path.GetRandomFileName() + "\\";
                        if (ZipHelper.UnZip(zippath, tozippath) > 0)
                        {
                            if (!m_iPhoneDevice.CopyDirectory2iPhone(tozippath, iSpriteContext.Current.iPhone_CydiaAutoInstallPath))
                            {
                                MessageHelper.ShowError("OpenSSH can not copy to #AppleDeviceType#, please try again.");
                                this.ShowForm();
                            }
                            else
                            {
                                MessageHelper.ShowInfo("OpenSSH has been copied to #AppleDeviceType#, You must Manual reboot your #AppleDeviceType# to finish Installation.");

                                this.ShowForm();
                            }
                        }
                        else
                        {
                            MessageHelper.ShowError("OpenSSH can not UnZip, please try again.");
                            this.ShowForm();
                        }

                    }
                    else
                    {
                        MessageHelper.ShowError("OpenSSH can not download to your pc, please try again.");
                        this.ShowForm();
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
                            MessageHelper.ShowError("Winterboard can not copy to #AppleDeviceType#, please try again.");
                            this.ShowForm();
                        }
                        else
                        {
                            MessageHelper.ShowInfo("WinterBoard has been copied to #AppleDeviceType#, You must Manual reboot your #AppleDeviceType# to finish Installation.");
                            this.ShowForm();
                        }
                    }
                    else
                    {
                        MessageHelper.ShowError("WinterBoard can not download to your pc, please try again.");
                        this.ShowForm();
                    }
                }
            }
            else//Search From Cydia and install it
            {
                string url = "http://www.ithemesky.com/GetAnswer.aspx?";
                if (m_app == InstallAppOption.OpenSSH)
                {
                    url += "qid=1";
                }
                else if (m_app == InstallAppOption.Winterboard)
                {
                    url += "qid=2";
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
