using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

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

            this.lblMsg.Font = new Font("Arial", 10F,
                FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
        }

        public string Message
        {
            set 
            { 
                this.lblMsg.Text = value;
                if (this.lblMsg.PreferredHeight > this.pnl.Height)
                {
                    this.Height += (this.lblMsg.PreferredHeight - this.pnl.Height);
                    this.pnl.Height = this.lblMsg.PreferredHeight;
                    MsgBox.picbox.Top = this.pnl.Top + (int)((this.pnl.Height - MsgBox.picbox.Height) / 2.0);
                }
            }
        }

        void iSpriteMessageBox_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        public static DialogResult Show(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            message = message.Replace("#AppleDeviceType#", iSpriteContext.Current.AppleDeviceType);

            MsgBox = new iSpriteMessageBox();
            MsgBox.Text = title;
            MsgBox.Visible = false;
            MsgBox.btnOK.Visible = false;
            MsgBox.btnCancel.Visible = false;
            MsgBox.btnAbort.Visible = false;

            MsgBox.Message = message;
            switch (icon)
            { 
                case MessageBoxIcon.Information:
                    MsgBox.picbox.Image = global::iSprite.Resource.Msg_Info;
                    break;
                case MessageBoxIcon.Error:
                    MsgBox.picbox.Image = global::iSprite.Resource.Msg_Error;
                    break;
                case MessageBoxIcon.Question:
                    MsgBox.picbox.Image = global::iSprite.Resource.Msg_Confirm;
                    break;
                case MessageBoxIcon.Warning:
                    MsgBox.picbox.Image = global::iSprite.Resource.Msg__Warning;
                    break;
            }
            switch (buttons)
            {
                case MessageBoxButtons.OK:
                    MsgBox.btnOK.Visible = true;
                    MsgBox.btnCancel.Visible = false;
                    MsgBox.btnOK.Left = (MsgBox.Width - MsgBox.btnOK.Width) / 2;
                    break;
                case MessageBoxButtons.OKCancel:
                    MsgBox.btnCancel.Select();
                    MsgBox.btnOK.Visible = true;
                    MsgBox.btnCancel.Visible = true;
                    MsgBox.btnCancel.Left = (MsgBox.Width - MsgBox.btnCancel.Width * 2) / 3;
                    MsgBox.btnOK.Left = MsgBox.btnCancel.Left * 2 + MsgBox.btnOK.Width;
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
