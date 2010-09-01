using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;
using System.IO;
using Manzana;

namespace iSprite
{

    internal partial class IPAInstaller : iSpriteForm
    {
        static private IPAInstaller InstallerBox;
        iPhoneFileDevice m_iPhoneDevice;
        internal event MessageHandler OnMessage;
        internal FileProgressHandler m_OnProgressHandler;

        public IPAInstaller()
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


        public static DialogResult Show(iPhoneFileDevice filedevice, MessageHandler msgHandler, FileProgressHandler progressHandler)
        {
            InstallerBox = new IPAInstaller();
            InstallerBox.m_iPhoneDevice = filedevice;
            InstallerBox.m_OnProgressHandler = progressHandler;
            InstallerBox.OnMessage += msgHandler;
            return InstallerBox.ShowDialog();
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            this.HiddenForm();

            if (!m_iPhoneDevice.CheckJailbreak())
            {
                this.Close();
                return;
            }

            if (!m_iPhoneDevice.CheckInstallDebApp("openssh"))
            {
                if (MessageHelper.ShowConfirm("Please install Open ssh to finish install app, Would you want to install?") == DialogResult.OK)
                {
                    //安装Open ssh
                    MyInstaller.Show(m_iPhoneDevice, InstallAppOption.OpenSSH, m_OnProgressHandler);
                    this.Close();
                    return;
                }
                else
                {
                    this.Close();
                    return;
                }
            }

            string fileName = txtFileName.Text;
            if (fileName == string.Empty)
            {
                MessageHelper.ShowError("please choose .ipa file !");
                this.ShowForm();
                return;
            }
            if (!File.Exists(fileName))
            {
                MessageHelper.ShowError("the .ipa file is not exists!");
                this.ShowForm();
                return;
            }

            string tozippath = iSpriteContext.Current.iSpriteTempPath + "\\" + Path.GetRandomFileName() + "\\";

            RaiseMessageHandler(this, "Prepare to unzip .ipa", MessageTypeOption.SetStatusBar);

            if (ZipHelper.UnZip(fileName, tozippath) > 0)
            {
                RaiseMessageHandler(this, "Prepare to Copy files to  iDevice...", MessageTypeOption.HiddenStatusBar);
                if (Directory.Exists(tozippath + "Payload"))
                {
                    string[] dirs = Directory.GetDirectories(tozippath + "Payload");
                    if (dirs.Length == 1)
                    {
                        string apppath = dirs[0];

                        bool flag = SSHHelper.InstallIPA(m_iPhoneDevice, apppath, out msg);
                        if (flag)
                        {
                            MessageHelper.ShowInfo(msg);
                            this.ShowForm();
                        }
                        else if (!string.IsNullOrEmpty(msg))
                        {
                            MessageHelper.ShowError(msg);
                            this.ShowForm();
                        }

                        this.DialogResult = DialogResult.OK;
                        this.Close();

                    }
                    else
                    {
                        MessageHelper.ShowError("the .ipa file is invalid!");
                        this.ShowForm();
                        return;
                    }
                }
                else
                {
                    MessageHelper.ShowError("the .ipa file is invalid!");
                    this.ShowForm();
                    return;
                }
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
            dialog.Filter = "ipa Files(*.ipa)|*.ipa";
            dialog.Multiselect = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = dialog.FileName;
            }
        }
    }
}
