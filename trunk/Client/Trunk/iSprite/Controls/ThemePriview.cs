using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace iSprite
{
    internal partial class ThemePriview : iUserControl
    {
        internal event ThemePriviewMessageHandler OnMessage;
        List<string> m_themeInfo;

        private Dictionary<string, PictureBox> m_ThemeIconDic = new Dictionary<string, PictureBox>();

        #region 消息通知
        private void RaiseMessageHandler(List<string> themeInfo, ThemePriviewMessageTypeOption messageType)
        {
            if (OnMessage != null)
            {
                OnMessage(themeInfo, messageType);
            }
        }
        #endregion

        public ThemePriview()
        {
            this.Visible = false;
            InitializeComponent();

            m_ThemeIconDic.Clear();

            m_ThemeIconDic.Add("Text", this.PictureBox1);
            m_ThemeIconDic.Add("Calendar", this.PictureBox2);
            m_ThemeIconDic.Add("Photos", this.PictureBox3);
            m_ThemeIconDic.Add("Camera", this.PictureBox4);
            m_ThemeIconDic.Add("YouTube", this.PictureBox5);
            m_ThemeIconDic.Add("Stocks", this.PictureBox6);
            m_ThemeIconDic.Add("Maps", this.PictureBox7);
            m_ThemeIconDic.Add("Weather", this.PictureBox8);
            m_ThemeIconDic.Add("Clock", this.PictureBox9);
            m_ThemeIconDic.Add("Calculator", this.PictureBox10);
            m_ThemeIconDic.Add("Notes", this.PictureBox11);
            m_ThemeIconDic.Add("Settings", this.PictureBox12);
            m_ThemeIconDic.Add("Phone", this.PictureBox13);
            m_ThemeIconDic.Add("Mail", this.PictureBox14);
            m_ThemeIconDic.Add("Safari", this.PictureBox15);
            m_ThemeIconDic.Add("iPod", this.PictureBox16);

            foreach (string icon in m_ThemeIconDic.Keys)
            {
                m_ThemeIconDic[icon].Tag = m_ThemeIconDic[icon].Image.Clone();
            }
        }

        internal void ShowPriview(List<string> themeInfo)
        {
            m_themeInfo = themeInfo;
            if (themeInfo.Count == 2)
            {
                string themePacket = themeInfo[1];
                if (Directory.Exists(themePacket))
                {
                    ShowIcons(themePacket);

                    this.Visible = true;
                }
            }
        }

        private void ShowIcons(string themePacket)
        {
            string themeIconPath = themePacket + "\\Icons\\";
            string themeWallpaper = themePacket + "\\Wallpaper.png";
            string themeDock = themePacket + "\\Dock.png";

            foreach (string icon in m_ThemeIconDic.Keys)
            {
                m_ThemeIconDic[icon].Image = (Image)m_ThemeIconDic[icon].Tag;
            }            

            this.pWallpaper.BackgroundImage = null;
            if (File.Exists(themeWallpaper))
            {
                try
                {
                    using (Image objImage = Image.FromFile(themeWallpaper))
                    {
                        this.pWallpaper.BackgroundImage = (Image)objImage.Clone();
                    }
                }
                catch
                {
                }
            }

            System.Drawing.Image imgBlank = null;
            System.Drawing.Graphics imgDraw = null;

            if (File.Exists(themeDock))
            {
                try
                {
                    using (Image objImage = Image.FromFile(themeDock))
                    {
                        imgBlank = this.pWallpaper.BackgroundImage;
                        imgDraw = System.Drawing.Graphics.FromImage(imgBlank);

                        imgDraw.DrawImage(objImage, new Rectangle(0, 392, imgBlank.Width, 88),
                            new Rectangle(0, 0, objImage.Width, objImage.Height), GraphicsUnit.Pixel);
                        imgDraw.Dispose();
                    }

                    this.pWallpaper.BackgroundImage = imgBlank;
                }
                catch
                {
                }
            }

            imgBlank = this.pWallpaper.BackgroundImage;
            if (imgBlank != null)
            {
                try
                {
                    imgDraw = System.Drawing.Graphics.FromImage(imgBlank);

                    imgDraw.DrawImage(global::iSprite.Resource.WB_Text, 0, 0, imgBlank.Width, imgBlank.Height);
                    imgDraw.Dispose();


                    this.pWallpaper.BackgroundImage = imgBlank;
                }
                catch
                {
                }
            }

            // 检查是否存在Icon库 
            if (Directory.Exists(themeIconPath))
            {
                string fullFileName;
                foreach (KeyValuePair<string, PictureBox> item in m_ThemeIconDic)
                {
                    fullFileName = themeIconPath + item.Key + ".png";
                    if (File.Exists(fullFileName))
                    {
                        try
                        {
                            using (Image objImage = Image.FromFile(fullFileName))
                            {
                                m_ThemeIconDic[item.Key].Image = (Image)objImage.Clone();
                                //m_ThemeIconDic[item.Key].BackColor = Color.Transparent;
                            }
                        }
                        catch
                        {
                        }
                    }
                }
                Bitmap mapWallpaper = (Bitmap)this.pWallpaper.BackgroundImage;
                foreach (KeyValuePair<string, PictureBox> item in m_ThemeIconDic)
                {
                    Rectangle r = new Rectangle(item.Value.Location.X - pWallpaper.Location.X, item.Value.Location.Y - pWallpaper.Location.Y, 59, 60);
                    item.Value.BackgroundImage = mapWallpaper.Clone(r, mapWallpaper.PixelFormat);
                }  
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            RaiseMessageHandler( null, ThemePriviewMessageTypeOption.Cancel);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            RaiseMessageHandler(m_themeInfo, ThemePriviewMessageTypeOption.Apply);
        }

    }
}
