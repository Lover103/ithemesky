using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Resources;
using System.Threading;

namespace iSprite
{
    internal partial class iSpriteStatus : iUserControl
    {
        /// <summary>
        /// 跨线程调用委托
        /// </summary>
        private delegate void ThreadInvokeDelegate();
        List<Bitmap> m_ImgList = new List<Bitmap>();
        int M_picIndex = 0;
        const int imgCount = 12;
        bool firstload = false;

        public iSpriteStatus()
        {
            InitializeComponent();
            m_ImgList = new List<Bitmap>();
            picbox.BackColor = Color.Transparent;
            for (int i = 1; i <= imgCount; i++)
            {
                m_ImgList.Add((Bitmap)Resource.ResourceManager.GetObject("pg_loader_" + i, null));
            }
            picbox.BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!this.IsDisposed)
            {
                if (!firstload)
                {
                    firstload = true;
                    base.OnPaint(e);
                }
                //this.picbox.BackgroundImage = m_ImgList[M_picIndex];
            }
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        int PicIndex
        {
            set
            {
                if (value >= imgCount)
                {
                    M_picIndex = 0;
                }
                else
                {
                    M_picIndex = value;
                }
                picbox.Invalidate();
            }
            get
            {
                return M_picIndex;
            }
        }


        void Change()
        {
            while (this.Visible)
            {
                PicIndex++;
                Application.DoEvents();
                Thread.Sleep(TimeSpan.FromSeconds(0.05));
            }
        }

        internal void Hidden()
        {
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
                            this.Visible = true;
                            this.lblStatus.Text = value;
                            new Thread(new ThreadStart(Change)).Start();
                        }
                    ));
                }
                else
                {
                    this.Visible = true;
                    this.lblStatus.Text = value;
                    new Thread(new ThreadStart(Change)).Start();
                }
            }
        }        
    }
}
