using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

namespace iSprite
{
    internal class iSpriteForm : Form
    {
        bool m_IsDrag = false;
        Rectangle m_Rectangle;
        Point M_StartPoint;

        public iSpriteForm()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;

            DoubleBuffered = true; 
            this.StartPosition =FormStartPosition.CenterScreen;

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (null != this.BackgroundImage)
            {
                GetNewImg(
                    e.Graphics,
                    new int[] { 2, 2, 30, 2 },
                    (Bitmap)this.BackgroundImage,
                    this.Width,
                    this.Height
                    );
            }
        }

        Rectangle R0, R1, R2, R3, R4, R5, R6, R7, R8, R9;
        void GetNewImg(Graphics img2Draw, int[] splitSetting, Bitmap Img, int destWidth, int destHeight)
        {
            if (splitSetting.Length == 4)
            {
                int m_width1, m_width2, m_height1, m_height2;

                m_width1 = splitSetting[0];
                m_width2 = splitSetting[1];
                m_height1 = splitSetting[2];
                m_height2 = splitSetting[3];

                R1 = new Rectangle(new Point(0, 0), new Size(m_width1, m_height1));
                Rectangle _R1 = R1;

                R2 = new Rectangle(new Point(R1.Width, 0), new Size(Img.Width - (m_width1 + m_width2), R1.Height));
                Rectangle _R2 = new Rectangle(new Point(_R1.Width, 0), new Size(destWidth - (m_width1 + m_width2), _R1.Height));

                R3 = new Rectangle(new Point(R1.Width + R2.Width, 0), new Size(m_width2, m_height1));
                Rectangle _R3 = new Rectangle(new Point(_R1.Width + _R2.Width, 0), new Size(m_width2, m_height1));

                R4 = new Rectangle(new Point(0, R1.Height), new Size(R1.Width, Img.Height - (m_height1 + m_height2)));
                Rectangle _R4 = new Rectangle(new Point(0, _R1.Height), new Size(_R1.Width, destHeight - (m_height1 + m_height2)));

                R5 = new Rectangle(new Point(R4.Width, R1.Height), new Size(R2.Width, R4.Height));
                Rectangle _R5 = new Rectangle(new Point(_R4.Width, _R1.Height), new Size(_R2.Width, _R4.Height));

                R6 = new Rectangle(new Point(R4.Width + R5.Width, R1.Height), new Size(R3.Width, R4.Height));
                Rectangle _R6 = new Rectangle(new Point(_R4.Width + _R5.Width, _R1.Height), new Size(_R3.Width, _R4.Height));

                R7 = new Rectangle(new Point(0, R1.Height + R4.Height), new Size(R1.Width, m_height2));
                Rectangle _R7 = new Rectangle(new Point(0, _R1.Height + _R4.Height), new Size(_R1.Width, m_height2));

                R8 = new Rectangle(new Point(R7.Width, R1.Height + R4.Height), new Size(R5.Width, R7.Height));
                Rectangle _R8 = new Rectangle(new Point(_R7.Width, _R1.Height + _R4.Height), new Size(_R5.Width, _R7.Height));

                R9 = new Rectangle(new Point(R7.Width + R8.Width, R1.Height + R4.Height), new Size(R3.Width, R7.Height));
                Rectangle _R9 = new Rectangle(new Point(_R7.Width + _R8.Width, _R1.Height + _R4.Height), new Size(_R3.Width, _R7.Height));


                img2Draw.DrawImage(Img, _R1, R1, GraphicsUnit.Pixel);
                img2Draw.DrawImage(Img, _R2, R2, GraphicsUnit.Pixel);
                img2Draw.DrawImage(Img, _R3, R3, GraphicsUnit.Pixel);
                img2Draw.DrawImage(Img, _R4, R4, GraphicsUnit.Pixel);
                img2Draw.DrawImage(Img, _R5, R5, GraphicsUnit.Pixel);
                img2Draw.DrawImage(Img, _R6, R6, GraphicsUnit.Pixel);
                img2Draw.DrawImage(Img, _R7, R7, GraphicsUnit.Pixel);
                img2Draw.DrawImage(Img, _R8, R8, GraphicsUnit.Pixel);
                img2Draw.DrawImage(Img, _R9, R9, GraphicsUnit.Pixel);
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.iSpriteForm_MouseUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.iSpriteForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.iSpriteForm_MouseMove);
            this.Resize += new EventHandler(this.iSpriteForm_Resize);
            this.ResumeLayout(false);

            btn_close = new Button();
            btn_close.Size = new System.Drawing.Size(21, 21);
            btn_close.Text = "X";
            this.Controls.Add(btn_close);
            btn_close.Location = new Point(this.Width - btn_close.Width - 8, 5);

            btn_close.Click += new EventHandler(
                delegate(object sender, EventArgs e)
                {
                    this.Close();
                }
            );

            btn_maximize = new Button();
            btn_maximize.Size = new System.Drawing.Size(21, 21);
            btn_maximize.Text = "@";
            this.Controls.Add(btn_maximize);
            btn_maximize.Location = new Point(btn_close.Location.X - btn_close.Width - 8, 5);

            btn_maximize.Click += new EventHandler(
                delegate(object sender, EventArgs e)
                {
                    this.WindowState = FormWindowState.Maximized;
                }
            );

            btn_minimize = new Button();
            btn_minimize.Size = new System.Drawing.Size(21, 21);
            btn_minimize.Text = "-";
            this.Controls.Add(btn_minimize);
            btn_minimize.Location = new Point(btn_maximize.Location.X - btn_maximize.Width - 8, 5);

            btn_minimize.Click += new EventHandler(
                delegate(object sender, EventArgs e)
                {
                    this.WindowState = FormWindowState.Minimized;
                }
            );

            lblTitle = new Label();
            lblTitle.Location = new Point(5, 5);
            this.lblTitle.Font = new System.Drawing.Font("Arial", 11F, 
                FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lblTitle.BackColor = Color.Transparent;
            this.Controls.Add(lblTitle);           


        }
        Button btn_close, btn_maximize, btn_minimize;
        Label lblTitle;
        internal void ChangeControlsLocation()
        {
            lblTitle.Text = this.Text;
            btn_close.Location = new Point(this.Width - btn_close.Width - 8, 5);
            btn_maximize.Location = new Point(btn_close.Location.X - btn_close.Width - 8, 5);
            btn_minimize.Location = new Point(btn_maximize.Location.X - btn_maximize.Width - 8, 5);
        }

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

        private void iSpriteForm_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
            ChangeControlsLocation();
        }

        #region 拖动窗体相关
        private void iSpriteForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.FormBorderStyle == FormBorderStyle.None  )
            {
                m_IsDrag = true;
                M_StartPoint = this.PointToClient(new Point(this.Left + e.X, this.Top + e.Y));
                m_Rectangle = new Rectangle(this.Left, this.Top, this.Size.Width, this.Size.Height);
                ControlPaint.DrawReversibleFrame(m_Rectangle, ControlPaint.ContrastControlDark, FrameStyle.Dashed);
            }
        }

        private void iSpriteForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.FormBorderStyle == FormBorderStyle.None && m_IsDrag&& e.Button== MouseButtons.Left)
            {
                Point endPoint = this.PointToScreen(new Point(e.X, e.Y));
                ControlPaint.DrawReversibleFrame(m_Rectangle, ControlPaint.ContrastControlDark, FrameStyle.Dashed);
                m_Rectangle = new Rectangle(endPoint.X - M_StartPoint.X, endPoint.Y - M_StartPoint.Y, this.Width, this.Height);
                ControlPaint.DrawReversibleFrame(m_Rectangle, ControlPaint.ContrastControlDark, FrameStyle.Dashed);
            }
        }
        private void iSpriteForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.FormBorderStyle == FormBorderStyle.None && m_IsDrag )
            {
                this.SuspendLayout();
                ControlPaint.DrawReversibleFrame(m_Rectangle, ControlPaint.ContrastControlDark, FrameStyle.Dashed);

                this.Location = new Point(m_Rectangle.X, this.m_Rectangle.Y);

                this.ResumeLayout(true);

                m_IsDrag = false;
                m_Rectangle = new Rectangle(0, 0, 0, 0);
                M_StartPoint = new Point(0, 0);

                ControlPaint.DrawBorder(this.CreateGraphics(), this.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
            }
        }
        #endregion
    }
}
