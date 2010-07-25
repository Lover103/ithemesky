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
        List<Bitmap> m_ImgList = new List<Bitmap>();
        int M_picIndex = 0;
        const int imgCount = 12;

        MainForm m_MainForm;

        public iSpriteStatus(Form parentForm)
        {
            m_MainForm = (MainForm)parentForm;
            m_ImgList = new List<Bitmap>();
            for (int i = 1; i <= imgCount; i++)
            {
                m_ImgList.Add((Bitmap)Resource.ResourceManager.GetObject("pg_loader_" + i, null));
            }
            InitializeComponent();
            picbox.BackColor = Color.Transparent;
        }
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
            }
            get
            {
                return M_picIndex;
            }
        }

        void SetOtherControls(bool enable)
        {
            foreach (Control ctl in m_MainForm.Controls)
            {
                if (!(ctl is iSpriteStatus))
                {
                    ctl.Enabled = enable;
                    Application.DoEvents();
                }
            }
        }

        void Change()
        {
            while (this.Visible)
            {
                PicIndex++;
                Thread.Sleep(TimeSpan.FromSeconds(0.05));

                this.Invoke(new ThreadInvokeDelegate(
                        delegate()
                        {
                            this.picbox.BackgroundImage = m_ImgList[M_picIndex];
                            picbox.Invalidate();
                            Application.DoEvents();
                        }
                    ));
            }
        }

        internal void Hidden()
        {
            SetOtherControls(true);
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
                            SetOtherControls(false);
                            new Thread(new ThreadStart(Change)).Start();
                            Application.DoEvents();
                        }
                    ));
                }
                else
                {
                    this.Visible = true;
                    this.lblStatus.Text = value;
                    SetOtherControls(false);
                    new Thread(new ThreadStart(Change)).Start();
                    Application.DoEvents();
                }
            }
        }        
    }
}
