using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace iSprite
{

    internal partial class iSpriteMessageBox : iSpriteForm
    {
        static private iSpriteMessageBox MsgBox;

        public iSpriteMessageBox()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
            this.ShowInTaskbar = false;
        }

        public string Message
        {
            set { this.lblMsg.Text = value; }
        }

        void iSpriteMessageBox_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        static public DialogResult Show(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            //m_returnButton= null;
            MsgBox = new iSpriteMessageBox();
            MsgBox.Text = title;
            MsgBox.Visible = false;
            MsgBox.btnOK.Visible = false;
            MsgBox.btnCancel.Visible = false;
            MsgBox.btnAbort.Visible = false;

            MsgBox.Message = message;
            switch (buttons)
            {
                case MessageBoxButtons.OK:
                    MsgBox.btnOK.Visible = true;
                    MsgBox.btnCancel.Visible = false;
                    MsgBox.btnOK.Left = (MsgBox.Width - MsgBox.btnOK.Width) / 2;
                    break;
                case MessageBoxButtons.OKCancel:
                    MsgBox.btnOK.Visible = true;
                    MsgBox.btnCancel.Visible = true;
                    MsgBox.btnOK.Left = (MsgBox.Width - MsgBox.btnOK.Width * 2) / 3;
                    MsgBox.btnCancel.Left = MsgBox.btnOK.Left * 2 + MsgBox.btnOK.Width;
                    break;
            }
            return MsgBox.ShowDialog();
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

        private void btnAbort_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
            this.Close();
        }
    }
}
