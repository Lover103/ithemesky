using System;
using System.Collections.Generic;

using System.Text;
using System.Drawing;

namespace iSprite
{
    internal class Area9Helper
    {

        /*
         用于 九宫格 切割
                   | Width 1 |       | Width 2 |
                   +---------------------------+ <-----------
                   |    R1   |   R2  |   R3    |    Height 1
                   |---------------------------| <-----------
                   |    R4   |   R5  |   R6    |
                   |---------------------------| <-----------
                   |    R7   |   R8  |   R9    |    Height 2
                   +---------------------------+ <-----------
         */

        internal static Image GetNewImg(int[] splitSetting, Bitmap Img, int destWidth, int destHeight)
        {
            if (splitSetting.Length == 4)
            {
                int m_width1, m_width2, m_height1, m_height2;

                m_width1 = splitSetting[0];
                m_width2 = splitSetting[1];
                m_height1 = splitSetting[2];
                m_height2 = splitSetting[3];

                Rectangle R1 = new Rectangle(new Point(0, 0), new Size(m_width1, m_height1));
                Rectangle _R1 = R1;

                Rectangle R2 = new Rectangle(new Point(R1.Width, 0), new Size(Img.Width - (m_width1 + m_width2), R1.Height));
                Rectangle _R2 = new Rectangle(new Point(_R1.Width, 0), new Size(destWidth - (m_width1 + m_width2), _R1.Height));

                Rectangle R3 = new Rectangle(new Point(R1.Width + R2.Width, 0), new Size(m_width2, m_height1));
                Rectangle _R3 = new Rectangle(new Point(_R1.Width + _R2.Width, 0), new Size(m_width2, m_height1));

                Rectangle R4 = new Rectangle(new Point(0, R1.Height), new Size(R1.Width, Img.Height - (m_height1 + m_height2)));
                Rectangle _R4 = new Rectangle(new Point(0, _R1.Height), new Size(_R1.Width, destHeight - (m_height1 + m_height2)));

                Rectangle R5 = new Rectangle(new Point(R4.Width, R1.Height), new Size(R2.Width, R4.Height));
                Rectangle _R5 = new Rectangle(new Point(_R4.Width, _R1.Height), new Size(_R2.Width, _R4.Height));

                Rectangle R6 = new Rectangle(new Point(R4.Width + R5.Width, R1.Height), new Size(R3.Width, R4.Height));
                Rectangle _R6 = new Rectangle(new Point(_R4.Width + _R5.Width, _R1.Height), new Size(_R3.Width, _R4.Height));

                Rectangle R7 = new Rectangle(new Point(0, R1.Height + R4.Height), new Size(R1.Width, m_height2));
                Rectangle _R7 = new Rectangle(new Point(0, _R1.Height + _R4.Height), new Size(_R1.Width, m_height2));

                Rectangle R8 = new Rectangle(new Point(R7.Width, R1.Height + R4.Height), new Size(R5.Width, R7.Height));
                Rectangle _R8 = new Rectangle(new Point(_R7.Width, _R1.Height + _R4.Height), new Size(_R5.Width, _R7.Height));

                Rectangle R9 = new Rectangle(new Point(R7.Width + R8.Width, R1.Height + R4.Height), new Size(R3.Width, R7.Height));
                Rectangle _R9 = new Rectangle(new Point(_R7.Width + _R8.Width, _R1.Height + _R4.Height), new Size(_R3.Width, _R7.Height));

                Image imgBlank = (Image)new Bitmap(destWidth, destHeight);
                Graphics imgDraw = Graphics.FromImage(imgBlank);

                imgDraw.DrawImage(Img, _R1, R1, GraphicsUnit.Pixel);
                imgDraw.DrawImage(Img, _R2, R2, GraphicsUnit.Pixel);
                imgDraw.DrawImage(Img, _R3, R3, GraphicsUnit.Pixel);
                imgDraw.DrawImage(Img, _R4, R4, GraphicsUnit.Pixel);
                imgDraw.DrawImage(Img, _R5, R5, GraphicsUnit.Pixel);
                imgDraw.DrawImage(Img, _R6, R6, GraphicsUnit.Pixel);
                imgDraw.DrawImage(Img, _R7, R7, GraphicsUnit.Pixel);
                imgDraw.DrawImage(Img, _R8, R8, GraphicsUnit.Pixel);
                imgDraw.DrawImage(Img, _R9, R9, GraphicsUnit.Pixel);

                return imgBlank;
            }
            else
            {
                return (Image)new Bitmap(destWidth, destHeight);
            }
        }

        internal static void GetNewImg(Graphics img2Draw,int[] splitSetting, Bitmap Img, int destWidth, int destHeight)
        {
            if (splitSetting.Length == 4)
            {
                int m_width1, m_width2, m_height1, m_height2;

                m_width1 = splitSetting[0];
                m_width2 = splitSetting[1];
                m_height1 = splitSetting[2];
                m_height2 = splitSetting[3];

                Rectangle R1 = new Rectangle(new Point(0, 0), new Size(m_width1, m_height1));
                Rectangle _R1 = R1;

                Rectangle R2 = new Rectangle(new Point(R1.Width, 0), new Size(Img.Width - (m_width1 + m_width2), R1.Height));
                Rectangle _R2 = new Rectangle(new Point(_R1.Width, 0), new Size(destWidth - (m_width1 + m_width2), _R1.Height));

                Rectangle R3 = new Rectangle(new Point(R1.Width + R2.Width, 0), new Size(m_width2, m_height1));
                Rectangle _R3 = new Rectangle(new Point(_R1.Width + _R2.Width, 0), new Size(m_width2, m_height1));

                Rectangle R4 = new Rectangle(new Point(0, R1.Height), new Size(R1.Width, Img.Height - (m_height1 + m_height2)));
                Rectangle _R4 = new Rectangle(new Point(0, _R1.Height), new Size(_R1.Width, destHeight - (m_height1 + m_height2)));

                Rectangle R5 = new Rectangle(new Point(R4.Width, R1.Height), new Size(R2.Width, R4.Height));
                Rectangle _R5 = new Rectangle(new Point(_R4.Width, _R1.Height), new Size(_R2.Width, _R4.Height));

                Rectangle R6 = new Rectangle(new Point(R4.Width + R5.Width, R1.Height), new Size(R3.Width, R4.Height));
                Rectangle _R6 = new Rectangle(new Point(_R4.Width + _R5.Width, _R1.Height), new Size(_R3.Width, _R4.Height));

                Rectangle R7 = new Rectangle(new Point(0, R1.Height + R4.Height), new Size(R1.Width, m_height2));
                Rectangle _R7 = new Rectangle(new Point(0, _R1.Height + _R4.Height), new Size(_R1.Width, m_height2));

                Rectangle R8 = new Rectangle(new Point(R7.Width, R1.Height + R4.Height), new Size(R5.Width, R7.Height));
                Rectangle _R8 = new Rectangle(new Point(_R7.Width, _R1.Height + _R4.Height), new Size(_R5.Width, _R7.Height));

                Rectangle R9 = new Rectangle(new Point(R7.Width + R8.Width, R1.Height + R4.Height), new Size(R3.Width, R7.Height));
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

    }
}
