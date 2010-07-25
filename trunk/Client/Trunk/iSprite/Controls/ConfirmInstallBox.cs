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

    internal partial class ConfirmInstallBox : iSpriteForm
    {
        static private ConfirmInstallBox m_Box;

        public ConfirmInstallBox()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
            this.ShowInTaskbar = false;
        }

        public static DialogResult Show(string name, string detail, ref bool autoInstall)
        {
            m_Box = new ConfirmInstallBox();

            m_Box.txtInput.Text = name;
            m_Box.txtIntroduce.Text = detail;

            DialogResult result = m_Box.ShowDialog();
            autoInstall = m_Box.chb.Checked;
            return result;
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
