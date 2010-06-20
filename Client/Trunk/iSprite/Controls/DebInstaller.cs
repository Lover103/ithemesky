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
        internal event MessageHandler OnMessage;

        public DebInstaller()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
            this.ShowInTaskbar = false;
        }


        private void RaiseMessageHandler(object sender, string Message, MessageTypeOption messageType)
        {
            if (OnMessage != null)
            {
                OnMessage(sender, Message, messageType);
            }
        }


        public static DialogResult Show(iPhoneFileDevice filedevice,MessageHandler megHandler)
        {
            InstallerBox = new DebInstaller();
            InstallerBox.m_iPhoneDevice = filedevice;
            InstallerBox.OnMessage += megHandler;
            return InstallerBox.ShowDialog();
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!m_iPhoneDevice.IsJailbreak)
            {
                MessageHelper.ShowError("To install deb file ,you must Jailbreak your " + iSpriteContext.Current.AppleDeviceType + " first !");
                return;
            }

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
            this.Visible = false;
            string msg = string.Empty;
            bool flag = SSHHelper.InstallDeb(m_iPhoneDevice, fileName, out msg);
            if (flag)
            {
                RaiseMessageHandler(this,string.Empty, MessageTypeOption.SuccessInstalled);
                MessageHelper.ShowInfo(Path.GetFileName(fileName) + " has been successfully Installed .");
            }
            else
            { 
            }
            this.Visible = true;

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
