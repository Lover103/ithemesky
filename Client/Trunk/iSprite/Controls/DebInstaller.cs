using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;
using System.IO;

namespace iSprite
{

    internal partial class DebInstaller : iSpriteForm
    {
        static private DebInstaller InstallerBox;
        iPhoneFileDevice m_iPhoneDevice;

        public DebInstaller()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
            this.ShowInTaskbar = false;
        }


        public static DialogResult Show(iPhoneFileDevice filedevice)
        {
            InstallerBox = new DebInstaller();
            InstallerBox.m_iPhoneDevice = filedevice;

            return InstallerBox.ShowDialog();
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            string fileName = txtFileName.Text;
            if (fileName == string.Empty)
            {
                MessageHelper.ShowError("please choose .deb file !");
                return;
            }
            if (!File.Exists(fileName))
            {
                MessageHelper.ShowError("the .deb file is not exists!");
                return;
            }
            if (rb1.Checked)
            {
                //cydia
                if (!CheckInstallApp("Cydia"))
                {
                    return;
                }
                List<string> list=m_iPhoneDevice.GetFiles(iSpriteContext.Current.iPhone_CydiaAutoInstallPath);
                bool flag = true;
                foreach (string file in list)
                {
                    if (Path.GetExtension(file).ToLower() == ".deb")
                    {
                        if (MessageHelper.ShowConfirm("The autoInstall folder exist deb file now,to protect iPhone you'd better install one by one,Are you sure to continue?") != DialogResult.OK)
                        {
                            flag = false;
                        }
                        break;
                    }
                }
                if (flag)
                {
                    m_iPhoneDevice.Copy2iPhone(fileName, iSpriteContext.Current.iPhone_CydiaAutoInstallPath);
                    MessageHelper.ShowInfo(Path.GetFileName(fileName)
                        + " has been copied to iPhone, reboot and Respring your iPhone to finish Installation .");
                }
            }
            else if (rb2.Checked)
            {
                //iFile
                if (!CheckInstallApp("iFile"))
                {
                    return;
                }
                m_iPhoneDevice.CheckDirectoryExists("/var/mobile/Library/Document/");
                m_iPhoneDevice.Copy2iPhone(fileName, "/var/mobile/Library/Document/");
                MessageHelper.ShowInfo(Path.GetFileName(fileName) 
                    + " has been copied to iPhone, Run iFile to finish Installation .");
            }
            else if (rb3.Checked)
            {
                //install0us
                if (!CheckInstallApp("install0us"))
                {
                    return;
                }
                m_iPhoneDevice.CheckDirectoryExists("/var/mobile/Library/Downloads/");
                m_iPhoneDevice.Copy2iPhone(fileName, "/var/mobile/Library/Downloads/");
                MessageHelper.ShowInfo(Path.GetFileName(fileName) 
                    + " has been copied to iPhone, Run install0us to finish Installation .");
            }
            else if (rb4.Checked)
            {
                //Mobile Terminal
                if (!CheckInstallApp("MobileTerminal"))
                {
                    return;
                }
                m_iPhoneDevice.CheckDirectoryExists("/tmp/");
                m_iPhoneDevice.Copy2iPhone(fileName, "/tmp/");
                MessageHelper.ShowInfo(Path.GetFileName(fileName) 
                    + " has been copied to iPhone, Run Mobile Terminal to finish Installation .");
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

        private void lnkHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(iSpriteContext.Current.DebInstallerHelpUrl);
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Deb Files(*.deb)|*.deb";
            dialog.Multiselect = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = dialog.FileName;
            }
        }
    }
}
