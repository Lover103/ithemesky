using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Resources;

namespace iSprite
{
    internal partial class iSpriteStatus : iUserControl
    {
        /// <summary>
        /// 跨线程调用委托
        /// </summary>
        private delegate void ThreadInvokeDelegate();
        List<PictureBox> pblist = new List<PictureBox>();
        int index = 0;

        public iSpriteStatus()
        {
            InitializeComponent();
            this.Paint += new PaintEventHandler(iSpriteStatus_Paint);

            pblist.Add(picbox);
            picbox.BackColor = Color.Transparent;
            this.picbox.BackgroundImage = global::iSprite.Resource.pg_loader_1;
            for (int i = 2; i <= 12; i++)
            {
                PictureBox box = new PictureBox();
                box.Size = picbox.Size;
                box.Location = picbox.Location;
                this.Controls.Add(box);

                object obj = Resource.ResourceManager.GetObject("pg_loader_" + i, null);
                box.BackColor = Color.Transparent;
                box.BackgroundImage = (Bitmap)(obj);
                pblist.Add(box);
            }
        }

        void iSpriteStatus_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);

            this.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.Opaque, true);  //优化双缓冲
        }

        internal void Hidden()
        {
            this.timer.Enabled = false;
            this.Visible = false;
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
                            this.timer.Enabled = true;
                            this.Visible = true;
                            this.lblStatus.Text = value;
                        }
                    ));
                }
                else
                {
                    this.Visible = true;
                    this.timer.Enabled = true;
                    this.lblStatus.Text = value;
                }
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (index >= 12)
            {
                index = 0;
            }
            pblist[index++].BringToFront();
            Application.DoEvents();
        }

    }
}
