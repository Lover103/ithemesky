using System;
using System.Collections.Generic;

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

        public bool EnableMaximize { get; set; }
        public bool EnableMinimize { get; set; }

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
               Area9Helper.GetNewImg(
                    e.Graphics,
                    new int[] { 2, 2, 30, 2 },
                    (Bitmap)this.BackgroundImage,
                    this.Width,
                    this.Height
                    );
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

            btn_close = new PictureBox();
            btn_close.Size = new System.Drawing.Size(22, 22);
            this.Controls.Add(btn_close);
            btn_close.Location = new Point(this.Width - btn_close.Width - 8, 1);
            btn_close.BackgroundImage = global::iSprite.Resource.close;
            btn_close.BackColor = Color.Transparent;
            btn_close.Click += new EventHandler(
                delegate(object sender, EventArgs e)
                {
                    this.Close();
                }
            );

            btn_maximize = new PictureBox();
            btn_maximize.Size = new System.Drawing.Size(22, 22);
            btn_maximize.BackgroundImage = global::iSprite.Resource.enlarge;
            btn_maximize.BackColor = Color.Transparent;
            this.Controls.Add(btn_maximize);
            btn_maximize.Location = new Point(btn_close.Location.X - btn_close.Width - 8, 5);

            btn_maximize.Click += new EventHandler(
                delegate(object sender, EventArgs e)
                {
                    if (this.WindowState == FormWindowState.Maximized)
                    {
                        this.WindowState = FormWindowState.Normal;
                    }
                    else
                    {
                        this.WindowState = FormWindowState.Maximized;
                    }
                }
            );
            btn_maximize.Visible = false;

            btn_minimize = new PictureBox();
            btn_minimize.Size = new System.Drawing.Size(22, 22);
            btn_minimize.BackgroundImage = global::iSprite.Resource.shrink;
            btn_minimize.BackColor = Color.Transparent;
            this.Controls.Add(btn_minimize);
            btn_minimize.Location = new Point(btn_maximize.Location.X - btn_maximize.Width - 8, 5);

            btn_minimize.Click += new EventHandler(
                delegate(object sender, EventArgs e)
                {
                    this.WindowState = FormWindowState.Minimized;
                }
            );
            btn_minimize.Visible = false;


            lblTitle = new Label();
            lblTitle.Location = new Point(5, 5);
            this.lblTitle.Font = new Font("Arial", 11F, 
                FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lblTitle.BackColor = Color.Transparent;
            this.Controls.Add(lblTitle);           


        }
        PictureBox btn_close, btn_maximize, btn_minimize;
        Label lblTitle;
        internal void ChangeControlsLocation()
        {
            lblTitle.Text = this.Text;
            btn_close.Location = new Point(this.Width - btn_close.Width - 8, 3);
            if (EnableMaximize)
            {
                btn_maximize.Visible = true;
                btn_maximize.Location = new Point(btn_close.Location.X - btn_close.Width - 6, 3);
            }
            else
            {
                btn_maximize.Visible = false;
            }
            if (EnableMinimize)
            {
                btn_minimize.Visible = true;
                btn_minimize.Location = new Point(btn_maximize.Location.X - btn_maximize.Width - 6, 3);
            }
            else
            {
                btn_minimize.Visible = false;
            }
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
