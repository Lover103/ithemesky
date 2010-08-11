﻿using System;
using System.Collections.Generic;

using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace iSprite
{
    internal class iUserControl : UserControl
    {
        Bitmap Img;

        protected override void OnPaint(PaintEventArgs e)
        {
            if (null != this.BackgroundImage)
            {
                Area9Helper.GetNewImg(
                        e.Graphics,
                        new int[] { 2, 2, 2, 2 },
                        (Bitmap)this.BackgroundImage,
                        this.Width,
                        this.Height
                        );
            }
        }
    }
}