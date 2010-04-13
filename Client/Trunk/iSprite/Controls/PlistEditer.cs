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

            this.Text = "PlistEditer";

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
            txtContent.SelectionLength = 0;
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
            if (MessageHelper.ShowConfirm("Are you sure you want to save current Plist content to iPhone ?") == DialogResult.OK)
            {
                if (!m_iPhoneFileDevice.SetFileText(txtContent.Text, m_PlistPath))
                {
                    MessageHelper.ShowError("Fail to save current Plist content to iPhone .");
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
