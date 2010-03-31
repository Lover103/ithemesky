using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace iSprite
{
    internal class iUserControl : UserControl
    {
        protected override void OnPaint(PaintEventArgs e)
        {
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
    }
}
