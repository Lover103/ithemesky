using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;
using System.IO;

namespace iSprite
{
    internal partial class ThemePriview : iUserControl
    {
        internal event ThemePriviewMessageHandler OnMessage;
        ThemeInfo m_themeInfo;

        private Dictionary<string, PictureBox> m_ThemeIconDic = new Dictionary<string, PictureBox>();

        #region 消息通知
        private void RaiseMessageHandler(ThemeInfo themeInfo, ThemePriviewMessageTypeOption messageType)
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

        internal void ShowPreview(ThemeInfo themeInfo)
        {
            this.Visible = false;
            m_themeInfo = themeInfo;

            string themePacket = m_themeInfo.LocalPath;
            btnUpload.Visible = !m_themeInfo.IsExistsIniPhone;
            if (Directory.Exists(themePacket))
            {
                ShowIcons(themePacket, false);

                this.Visible = true;
            }
        }

        internal Image ShowPreview(string themePacket)
        {
            m_themeInfo = new ThemeInfo();
            this.Visible = false;
            if (Directory.Exists(themePacket))
            {
                m_themeInfo.LocalPath=themePacket;
                ShowIcons(themePacket,true);
            }
            return (Image)this.pWallpaper.BackgroundImage.Clone();
        }

        private void ShowIcons(string themePacket,bool forlistview)
        {
            string themeIconPath = themePacket + "\\Icons\\";
            string themeWallpaper = themePacket + "\\Wallpaper.png";
            string themeDock = themePacket + "\\Dock.png";
            string themePreview = themePacket + "\\previewImg.jpg";

            if (File.Exists(themeWallpaper) && Directory.Exists(themeIconPath))
            {
                //标准主题
                foreach (string icon in m_ThemeIconDic.Keys)
                {
                    m_ThemeIconDic[icon].Visible = true;
                    m_ThemeIconDic[icon].Image = (Image)m_ThemeIconDic[icon].Tag;
                }

                #region 加载背景
                //加载背景
                this.pWallpaper.BackgroundImage = null;
                if (File.Exists(themeWallpaper))
                {
                    try
                    {
                        using (Image objImage = Image.FromFile(themeWallpaper))
                        {
                            this.pWallpaper.BackgroundImage = (Image)objImage.Clone();
                            objImage.Dispose();
                        }
                    }
                    catch
                    {
                    }
                }
                #endregion

                #region 加载dock图标
                System.Drawing.Image imgBlank = null;
                System.Drawing.Graphics imgDraw = null;
                //加载dock图标
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
                #endregion

                #region 加载背景文字
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
                #endregion

                // 检查是否存在Icon库 
                if (Directory.Exists(themeIconPath))
                {
                    #region 加载图标
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
                                    if (objImage.Height > 60 || objImage.Width > 60)
                                    {
                                        m_ThemeIconDic[item.Key].Image = objImage.GetThumbnailImage(60, 60, null, IntPtr.Zero);
                                    }
                                    else
                                    {
                                        m_ThemeIconDic[item.Key].Image = (Image)objImage.Clone();
                                    }
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                    if (null != this.pWallpaper.BackgroundImage)
                    {
                        Bitmap mapWallpaper = (Bitmap)this.pWallpaper.BackgroundImage;

                        if (forlistview)
                        {
                            imgBlank = this.pWallpaper.BackgroundImage;
                            imgDraw = System.Drawing.Graphics.FromImage(imgBlank);
                        }

                        foreach (KeyValuePair<string, PictureBox> item in m_ThemeIconDic)
                        {
                            try
                            {
                                Rectangle r = new Rectangle(
                                item.Value.Location.X - pWallpaper.Location.X,
                                item.Value.Location.Y - pWallpaper.Location.Y,
                                item.Value.Image.Width,
                                item.Value.Image.Height
                                );

                                if (forlistview)
                                {
                                    Rectangle _r = new Rectangle(0, 0, item.Value.Image.Width, item.Value.Image.Height);
                                    item.Value.Visible = false;
                                    imgDraw.DrawImage(item.Value.Image, r, _r, GraphicsUnit.Pixel);
                                }
                                else
                                {
                                    item.Value.Visible = true;
                                    item.Value.BackgroundImage = mapWallpaper.Clone(r, mapWallpaper.PixelFormat);
                                }
                            }
                            catch
                            { 
                            }
                        }
                        if (forlistview)
                        {
                            this.pWallpaper.BackgroundImage = imgBlank;
                        }
                    }
                    #endregion
                }
            }
            else
            { 
                //动态主题
                if (File.Exists(themePreview))
                {
                    #region 加载背景
                    //加载背景
                    this.pWallpaper.BackgroundImage = null;
                    try
                    {
                        foreach (string icon in m_ThemeIconDic.Keys)
                        {
                            m_ThemeIconDic[icon].Visible = false;
                        }
                        using (Image objImage = Image.FromFile(themePreview))
                        {
                            this.pWallpaper.BackgroundImage = (Image)objImage.Clone();
                        }
                    }
                    catch
                    {
                    }

                    #endregion
                }
                else
                {
                    //其他情况，不显示预览图，直接安装
                    this.Visible = false;
                    if (!forlistview)
                    {
                        RaiseMessageHandler(m_themeInfo, ThemePriviewMessageTypeOption.Apply);
                    }
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

        private void btnUpload_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            RaiseMessageHandler(m_themeInfo, ThemePriviewMessageTypeOption.Upload);
        }

    }
}
