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

    internal partial class InputBox : iSpriteForm
    {
        static private InputBox m_InputBox;

        public InputBox()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
            this.ShowInTaskbar = false;
        }

        /// <summary>
        /// 用户信息输入框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="input">返回内容</param>
        /// <param name="ispwd">是否是密码</param>
        /// <returns></returns>
        public static DialogResult Show(string title, ref string input, bool ispwd)
        {
            m_InputBox = new InputBox();
            m_InputBox.Text = title;
            m_InputBox.txtInput.Text = input;

            m_InputBox.txtInput.UseSystemPasswordChar = ispwd;

            DialogResult result = m_InputBox.ShowDialog();
            input = m_InputBox.txtInput.Text;
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
