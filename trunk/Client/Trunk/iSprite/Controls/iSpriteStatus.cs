using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace iSprite
{
    internal partial class iSpriteStatus : iUserControl
    {
        /// <summary>
        /// 跨线程调用委托
        /// </summary>
        private delegate void ThreadInvokeDelegate();

        public iSpriteStatus()
        {
            InitializeComponent();
            this.Paint += new PaintEventHandler(iSpriteStatus_Paint);
        }

        void iSpriteStatus_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);

            this.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.Opaque, true);  //优化双缓冲
        }

        /// <summary>
        /// 设置状态条文字
        /// </summary>
        public string StatusMsg
        {
            set
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new ThreadInvokeDelegate(
                        delegate()
                        {
                            this.Visible = true;
                            this.lblStatus.Text = value;
                        }
                    ));
                }
                else
                {
                    this.Visible = true;
                    this.lblStatus.Text = value;
                }
            }
        }

    }
}
