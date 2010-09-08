﻿using System;
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

    internal partial class DebInstaller : iSpriteForm
    {
        static private DebInstaller InstallerBox;
        iPhoneFileDevice m_iPhoneDevice;
        internal event MessageHandler OnMessage;
        internal FileProgressHandler m_OnProgressHandler;

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


        public static DialogResult Show(iPhoneFileDevice filedevice, MessageHandler msgHandler, FileProgressHandler progressHandler)
        {
            InstallerBox = new DebInstaller();
            InstallerBox.m_iPhoneDevice = filedevice;
            InstallerBox.m_OnProgressHandler = progressHandler;
            InstallerBox.OnMessage += msgHandler;
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
                MessageHelper.ShowError("please choose .deb file !");
                this.ShowForm();
                return;
            }
            if (!File.Exists(fileName))
            {
                MessageHelper.ShowError("the .deb file is not exists!");
                this.ShowForm();
                return;
            }
            //this.Visible = false;
            string msg = string.Empty;
            bool flag = SSHHelper.InstallDeb(m_iPhoneDevice, fileName, out msg);
            if (flag)
            {
                RaiseMessageHandler(this,string.Empty, MessageTypeOption.SuccessInstalled);
                MessageHelper.ShowInfo(msg);
                this.ShowForm();
            }
            else if(!string.IsNullOrEmpty(msg))
            {
                MessageHelper.ShowError(msg);
                this.ShowForm();
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
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