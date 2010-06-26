using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace iSprite
{
    internal partial class PlistEditer : iSpriteForm
    {
        private static PlistEditer m_Editer;
        iPhoneFileDevice m_iPhoneFileDevice;
        string m_PlistPath;
        public PlistEditer(iPhoneFileDevice iphonedevice, string plistpath)
        {
            m_iPhoneFileDevice = iphonedevice;
            m_PlistPath = plistpath;

            InitializeComponent();

            this.Text = "PlistEditer(" + plistpath + ")";

            this.DialogResult = DialogResult.Cancel;
            this.ShowInTaskbar = false;
            this.FormClosed += new FormClosedEventHandler(PlistEditer_FormClosed);

            Initialise();
        }

        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        void Initialise()
        {
            string content = m_iPhoneFileDevice.GetFileText(m_PlistPath);
            txtContent.Text = content.Replace("\n", "\r\n");
            this.KeyDown += new KeyEventHandler(PlistEditer_KeyDown);
            txtContent.SelectionLength = 0;
        }

        void PlistEditer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode== Keys.A)
            {
                txtContent.SelectAll();
            }
        }
        #endregion

        public static DialogResult Show(iPhoneFileDevice iphonedevice, string plistpath)
        {
            m_Editer = new PlistEditer(iphonedevice, plistpath);
            return m_Editer.ShowDialog();
        }

        void PlistEditer_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageHelper.ShowConfirm("Are you sure you want to save current Plist content to #AppleDeviceType# ?") == DialogResult.OK)
            {
                if (!m_iPhoneFileDevice.SetFileText(txtContent.Text, m_PlistPath))
                {
                    MessageHelper.ShowError("Fail to save current Plist content to #AppleDeviceType# .");
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
