using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace iSprite
{
    public partial class ThemePreviewItem : UserControl
    {
        private ThemePreviewItem()
        { 
        }

        public bool IsSelected
        {
            get
            {
                return this.chlselect.Checked;
            }
        }

        public ThemePreviewItem(Image imgTheme, string themeName)
        {
            InitializeComponent();

            picbox.BackgroundImageLayout = ImageLayout.Stretch;
            picbox.BackgroundImage = imgTheme;
            picbox.Click += new EventHandler(picbox_Click);
            picbox.MouseEnter += new EventHandler(picbox_MouseEnter);
            picbox.MouseLeave += new EventHandler(picbox_MouseLeave);
            chlselect.Text = themeName;

            this.MouseEnter += new EventHandler(ThemePreviewItem_MouseEnter);
            this.MouseLeave += new EventHandler(ThemePreviewItem_MouseLeave);
        }

        void picbox_MouseLeave(object sender, EventArgs e)
        {
            this.OnMouseLeave(e);
        }

        void picbox_MouseEnter(object sender, EventArgs e)
        {
            this.OnMouseEnter(e);
        }

        void ThemePreviewItem_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
        }

        void ThemePreviewItem_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.DodgerBlue;
        }

        void picbox_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }
    }
}
