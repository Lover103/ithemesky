﻿namespace iSprite
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.faTabStrip1 = new iSprite.ThirdControl.FarsiLibrary.FATabStrip();
            this.tabs = new iSprite.ThirdControl.FarsiLibrary.FATabStrip();
            this.tabFile = new iSprite.ThirdControl.FarsiLibrary.FATabStripItem();
            this.mainsplitcontainer = new System.Windows.Forms.SplitContainer();
            this.Filetoolmenu = new System.Windows.Forms.ToolStrip();
            this.tsbtnTileVertical = new System.Windows.Forms.ToolStripButton();
            this.tsbtnTileHorizontal = new System.Windows.Forms.ToolStripButton();
            this.tsbtnSinglePane = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnFlipPanes = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnDeb = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnreSpring = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnHelp = new System.Windows.Forms.ToolStripButton();
            this.tabTheme = new iSprite.ThirdControl.FarsiLibrary.FATabStripItem();
            this.tabApp = new iSprite.ThirdControl.FarsiLibrary.FATabStripItem();
            this.appContainer = new System.Windows.Forms.SplitContainer();
            this.tvCatalog = new System.Windows.Forms.TreeView();
            this.catalogimg = new System.Windows.Forms.ImageList(this.components);
            this.appwb = new System.Windows.Forms.WebBrowser();
            this.app_Panelbuttom = new System.Windows.Forms.Panel();
            this.appPager = new iSprite.iPager();
            this.app_Paneltop = new System.Windows.Forms.Panel();
            this.toolapp = new System.Windows.Forms.ToolStrip();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.faTabStrip1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabs)).BeginInit();
            this.tabs.SuspendLayout();
            this.tabFile.SuspendLayout();
            this.mainsplitcontainer.SuspendLayout();
            this.Filetoolmenu.SuspendLayout();
            this.tabApp.SuspendLayout();
            this.appContainer.Panel1.SuspendLayout();
            this.appContainer.Panel2.SuspendLayout();
            this.appContainer.SuspendLayout();
            this.app_Panelbuttom.SuspendLayout();
            this.toolapp.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 300;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // faTabStrip1
            // 
            this.faTabStrip1.AlwaysShowClose = false;
            this.faTabStrip1.AlwaysShowMenuGlyph = false;
            this.faTabStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.faTabStrip1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.faTabStrip1.Location = new System.Drawing.Point(3, 30);
            this.faTabStrip1.Name = "faTabStrip1";
            this.faTabStrip1.Size = new System.Drawing.Size(993, 640);
            this.faTabStrip1.TabIndex = 1;
            this.faTabStrip1.Text = "faTabStrip1";
            // 
            // tabs
            // 
            this.tabs.AlwaysShowClose = false;
            this.tabs.AlwaysShowMenuGlyph = false;
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.tabs.Items.AddRange(new iSprite.ThirdControl.FarsiLibrary.FATabStripItem[] {
            this.tabFile,
            this.tabTheme,
            this.tabApp});
            this.tabs.Location = new System.Drawing.Point(3, 30);
            this.tabs.Name = "tabs";
            this.tabs.SelectedItem = this.tabApp;
            this.tabs.Size = new System.Drawing.Size(993, 640);
            this.tabs.TabIndex = 2;
            this.tabs.Text = "faTabStrip1";
            this.tabs.TabStripItemSelectionChanged += new iSprite.ThirdControl.FarsiLibrary.FATabStrip.TabStripItemChangedHandler(this.TabStripItemSelectionChanged);
            // 
            // tabFile
            // 
            this.tabFile.CanClose = false;
            this.tabFile.Controls.Add(this.mainsplitcontainer);
            this.tabFile.Controls.Add(this.Filetoolmenu);
            this.tabFile.IsDrawn = true;
            this.tabFile.Name = "tabFile";
            this.tabFile.Size = new System.Drawing.Size(991, 619);
            this.tabFile.TabIndex = 0;
            this.tabFile.Title = "File Manage";
            // 
            // mainsplitcontainer
            // 
            this.mainsplitcontainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainsplitcontainer.Location = new System.Drawing.Point(0, 0);
            this.mainsplitcontainer.Margin = new System.Windows.Forms.Padding(0);
            this.mainsplitcontainer.Name = "mainsplitcontainer";
            this.mainsplitcontainer.Panel1MinSize = 0;
            this.mainsplitcontainer.Panel2MinSize = 0;
            this.mainsplitcontainer.Size = new System.Drawing.Size(991, 619);
            this.mainsplitcontainer.SplitterDistance = 516;
            this.mainsplitcontainer.SplitterWidth = 1;
            this.mainsplitcontainer.TabIndex = 13;
            // 
            // Filetoolmenu
            // 
            this.Filetoolmenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.Filetoolmenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnTileVertical,
            this.tsbtnTileHorizontal,
            this.tsbtnSinglePane,
            this.toolStripSeparator1,
            this.tsbtnFlipPanes,
            this.toolStripSeparator4,
            this.tsbtnDeb,
            this.toolStripSeparator3,
            this.tsbtnreSpring,
            this.toolStripSeparator2,
            this.tsbtnHelp});
            this.Filetoolmenu.Location = new System.Drawing.Point(0, 0);
            this.Filetoolmenu.Name = "Filetoolmenu";
            this.Filetoolmenu.Size = new System.Drawing.Size(991, 25);
            this.Filetoolmenu.TabIndex = 12;
            this.Filetoolmenu.Text = "toolStrip3";
            this.Filetoolmenu.Visible = false;
            // 
            // tsbtnTileVertical
            // 
            this.tsbtnTileVertical.Checked = true;
            this.tsbtnTileVertical.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbtnTileVertical.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnTileVertical.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnTileVertical.Image")));
            this.tsbtnTileVertical.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnTileVertical.Name = "tsbtnTileVertical";
            this.tsbtnTileVertical.Size = new System.Drawing.Size(23, 22);
            this.tsbtnTileVertical.ToolTipText = "Tile Vertical";
            // 
            // tsbtnTileHorizontal
            // 
            this.tsbtnTileHorizontal.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnTileHorizontal.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnTileHorizontal.Image")));
            this.tsbtnTileHorizontal.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnTileHorizontal.Name = "tsbtnTileHorizontal";
            this.tsbtnTileHorizontal.Size = new System.Drawing.Size(23, 22);
            this.tsbtnTileHorizontal.ToolTipText = "Tile Horizontal";
            // 
            // tsbtnSinglePane
            // 
            this.tsbtnSinglePane.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSinglePane.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSinglePane.Image")));
            this.tsbtnSinglePane.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSinglePane.Name = "tsbtnSinglePane";
            this.tsbtnSinglePane.Size = new System.Drawing.Size(23, 22);
            this.tsbtnSinglePane.ToolTipText = "Single Pane";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtnFlipPanes
            // 
            this.tsbtnFlipPanes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnFlipPanes.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnFlipPanes.Image")));
            this.tsbtnFlipPanes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnFlipPanes.Name = "tsbtnFlipPanes";
            this.tsbtnFlipPanes.Size = new System.Drawing.Size(23, 22);
            this.tsbtnFlipPanes.ToolTipText = "Swap Source and Destination";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtnDeb
            // 
            this.tsbtnDeb.Image = global::iSprite.Resource.file_deb;
            this.tsbtnDeb.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnDeb.Name = "tsbtnDeb";
            this.tsbtnDeb.Size = new System.Drawing.Size(91, 22);
            this.tsbtnDeb.Text = "Install Deb";
            this.tsbtnDeb.ToolTipText = "Install Deb File";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtnreSpring
            // 
            this.tsbtnreSpring.Image = global::iSprite.Resource.quick_restart;
            this.tsbtnreSpring.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnreSpring.Name = "tsbtnreSpring";
            this.tsbtnreSpring.Size = new System.Drawing.Size(73, 22);
            this.tsbtnreSpring.Text = "Respring";
            this.tsbtnreSpring.ToolTipText = "Restart SpringBoard";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtnHelp
            // 
            this.tsbtnHelp.Image = global::iSprite.Resource.help;
            this.tsbtnHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnHelp.Name = "tsbtnHelp";
            this.tsbtnHelp.Size = new System.Drawing.Size(49, 22);
            this.tsbtnHelp.Text = "Help";
            this.tsbtnHelp.ToolTipText = "Get Help";
            // 
            // tabTheme
            // 
            this.tabTheme.CanClose = false;
            this.tabTheme.IsDrawn = true;
            this.tabTheme.Name = "tabTheme";
            this.tabTheme.Size = new System.Drawing.Size(991, 619);
            this.tabTheme.TabIndex = 1;
            this.tabTheme.Title = "Theme Manage";
            // 
            // tabApp
            // 
            this.tabApp.CanClose = false;
            this.tabApp.Controls.Add(this.appContainer);
            this.tabApp.Controls.Add(this.toolapp);
            this.tabApp.IsDrawn = true;
            this.tabApp.Name = "tabApp";
            this.tabApp.Selected = true;
            this.tabApp.Size = new System.Drawing.Size(991, 619);
            this.tabApp.TabIndex = 2;
            this.tabApp.Title = "App Manage";
            // 
            // appContainer
            // 
            this.appContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.appContainer.Location = new System.Drawing.Point(0, 25);
            this.appContainer.Margin = new System.Windows.Forms.Padding(0);
            this.appContainer.Name = "appContainer";
            // 
            // appContainer.Panel1
            // 
            this.appContainer.Panel1.Controls.Add(this.tvCatalog);
            this.appContainer.Panel1MinSize = 0;
            // 
            // appContainer.Panel2
            // 
            this.appContainer.Panel2.Controls.Add(this.appwb);
            this.appContainer.Panel2.Controls.Add(this.app_Panelbuttom);
            this.appContainer.Panel2.Controls.Add(this.app_Paneltop);
            this.appContainer.Panel2MinSize = 0;
            this.appContainer.Size = new System.Drawing.Size(991, 594);
            this.appContainer.SplitterDistance = 209;
            this.appContainer.SplitterWidth = 1;
            this.appContainer.TabIndex = 14;
            // 
            // tvCatalog
            // 
            this.tvCatalog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvCatalog.Font = new System.Drawing.Font("Arial", 10F);
            this.tvCatalog.ImageIndex = 0;
            this.tvCatalog.ImageList = this.catalogimg;
            this.tvCatalog.Indent = 15;
            this.tvCatalog.ItemHeight = 25;
            this.tvCatalog.Location = new System.Drawing.Point(0, 0);
            this.tvCatalog.Name = "tvCatalog";
            this.tvCatalog.SelectedImageIndex = 0;
            this.tvCatalog.ShowRootLines = false;
            this.tvCatalog.Size = new System.Drawing.Size(209, 594);
            this.tvCatalog.TabIndex = 0;
            // 
            // catalogimg
            // 
            this.catalogimg.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("catalogimg.ImageStream")));
            this.catalogimg.TransparentColor = System.Drawing.Color.Transparent;
            this.catalogimg.Images.SetKeyName(0, "Administration.png");
            this.catalogimg.Images.SetKeyName(1, "All Packages.png");
            this.catalogimg.Images.SetKeyName(2, "App Addons.png");
            this.catalogimg.Images.SetKeyName(3, "Archiving.png");
            this.catalogimg.Images.SetKeyName(4, "Blanks.png");
            this.catalogimg.Images.SetKeyName(5, "Carrier Bundles.png");
            this.catalogimg.Images.SetKeyName(6, "Communication.png");
            this.catalogimg.Images.SetKeyName(7, "Data Storage.png");
            this.catalogimg.Images.SetKeyName(8, "Development.png");
            this.catalogimg.Images.SetKeyName(9, "Dictionaries.png");
            this.catalogimg.Images.SetKeyName(10, "Downloaded Packages.png");
            this.catalogimg.Images.SetKeyName(11, "eBooks.png");
            this.catalogimg.Images.SetKeyName(12, "Education.png");
            this.catalogimg.Images.SetKeyName(13, "Entertainment.png");
            this.catalogimg.Images.SetKeyName(14, "Favorites.png");
            this.catalogimg.Images.SetKeyName(15, "Games.png");
            this.catalogimg.Images.SetKeyName(16, "Health and Fitness.png");
            this.catalogimg.Images.SetKeyName(17, "Imaging.png");
            this.catalogimg.Images.SetKeyName(18, "Installed Packages.png");
            this.catalogimg.Images.SetKeyName(19, "Java.png");
            this.catalogimg.Images.SetKeyName(20, "Keyboards.png");
            this.catalogimg.Images.SetKeyName(21, "Localization.png");
            this.catalogimg.Images.SetKeyName(22, "Messaging.png");
            this.catalogimg.Images.SetKeyName(23, "Multimedia.png");
            this.catalogimg.Images.SetKeyName(24, "Navigation.png");
            this.catalogimg.Images.SetKeyName(25, "Networking.png");
            this.catalogimg.Images.SetKeyName(26, "Packaging.png");
            this.catalogimg.Images.SetKeyName(27, "Planet-iPhones Mods.png");
            this.catalogimg.Images.SetKeyName(28, "Productivity.png");
            this.catalogimg.Images.SetKeyName(29, "Repositories.png");
            this.catalogimg.Images.SetKeyName(30, "Ringtones.png");
            this.catalogimg.Images.SetKeyName(31, "SBSettings Addons.png");
            this.catalogimg.Images.SetKeyName(32, "Scripting.png");
            this.catalogimg.Images.SetKeyName(33, "Search Result.png");
            this.catalogimg.Images.SetKeyName(34, "Security.png");
            this.catalogimg.Images.SetKeyName(35, "Social.png");
            this.catalogimg.Images.SetKeyName(36, "System.png");
            this.catalogimg.Images.SetKeyName(37, "Terminal Support.png");
            this.catalogimg.Images.SetKeyName(38, "Text Editors.png");
            this.catalogimg.Images.SetKeyName(39, "Themes.png");
            this.catalogimg.Images.SetKeyName(40, "Toys.png");
            this.catalogimg.Images.SetKeyName(41, "Utilities.png");
            this.catalogimg.Images.SetKeyName(42, "Wallpaper.png");
            this.catalogimg.Images.SetKeyName(43, "WebClips.png");
            this.catalogimg.Images.SetKeyName(44, "Widgets.png");
            this.catalogimg.Images.SetKeyName(45, "X Window.png");
            this.catalogimg.Images.SetKeyName(46, "Others.png");
            // 
            // appwb
            // 
            this.appwb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.appwb.Location = new System.Drawing.Point(0, 30);
            this.appwb.MinimumSize = new System.Drawing.Size(20, 20);
            this.appwb.Name = "appwb";
            this.appwb.Size = new System.Drawing.Size(781, 534);
            this.appwb.TabIndex = 2;
            // 
            // app_Panelbuttom
            // 
            this.app_Panelbuttom.Controls.Add(this.appPager);
            this.app_Panelbuttom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.app_Panelbuttom.Location = new System.Drawing.Point(0, 564);
            this.app_Panelbuttom.Name = "app_Panelbuttom";
            this.app_Panelbuttom.Size = new System.Drawing.Size(781, 30);
            this.app_Panelbuttom.TabIndex = 1;
            // 
            // appPager
            // 
            this.appPager.CurrentPageIndex = 0;
            this.appPager.Enabled = false;
            this.appPager.Location = new System.Drawing.Point(530, 5);
            this.appPager.Name = "appPager";
            this.appPager.PageCount = 0;
            this.appPager.PageSize = 20;
            this.appPager.RecordCount = 0;
            this.appPager.Size = new System.Drawing.Size(248, 30);
            this.appPager.TabIndex = 0;
            // 
            // app_Paneltop
            // 
            this.app_Paneltop.Dock = System.Windows.Forms.DockStyle.Top;
            this.app_Paneltop.Location = new System.Drawing.Point(0, 0);
            this.app_Paneltop.Name = "app_Paneltop";
            this.app_Paneltop.Size = new System.Drawing.Size(781, 30);
            this.app_Paneltop.TabIndex = 0;
            // 
            // toolapp
            // 
            this.toolapp.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolapp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton5,
            this.toolStripSeparator7,
            this.toolStripButton6,
            this.toolStripSeparator8,
            this.toolStripButton7});
            this.toolapp.Location = new System.Drawing.Point(0, 0);
            this.toolapp.Name = "toolapp";
            this.toolapp.Size = new System.Drawing.Size(991, 25);
            this.toolapp.TabIndex = 13;
            this.toolapp.Text = "toolStrip3";
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.Image = global::iSprite.Resource.file_deb;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(91, 22);
            this.toolStripButton5.Text = "Install Deb";
            this.toolStripButton5.ToolTipText = "Install Deb File";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.Image = global::iSprite.Resource.ipa;
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(91, 22);
            this.toolStripButton6.Text = "Install IPA";
            this.toolStripButton6.ToolTipText = "Install IPA";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.Image = global::iSprite.Resource.pxl;
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(91, 22);
            this.toolStripButton7.Text = "Install PXL";
            this.toolStripButton7.ToolTipText = "Get Help";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::iSprite.Resource.form_bg;
            this.ClientSize = new System.Drawing.Size(999, 673);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.faTabStrip1);
            this.EnableMaximize = true;
            this.EnableMinimize = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(3, 30, 3, 3);
            this.Text = "iSpirit";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.faTabStrip1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabs)).EndInit();
            this.tabs.ResumeLayout(false);
            this.tabFile.ResumeLayout(false);
            this.tabFile.PerformLayout();
            this.mainsplitcontainer.ResumeLayout(false);
            this.Filetoolmenu.ResumeLayout(false);
            this.Filetoolmenu.PerformLayout();
            this.tabApp.ResumeLayout(false);
            this.tabApp.PerformLayout();
            this.appContainer.Panel1.ResumeLayout(false);
            this.appContainer.Panel2.ResumeLayout(false);
            this.appContainer.ResumeLayout(false);
            this.app_Panelbuttom.ResumeLayout(false);
            this.toolapp.ResumeLayout(false);
            this.toolapp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private iSprite.ThirdControl.FarsiLibrary.FATabStrip faTabStrip1;
        private System.Windows.Forms.Timer timer;
        private iSprite.ThirdControl.FarsiLibrary.FATabStrip tabs;
        private iSprite.ThirdControl.FarsiLibrary.FATabStripItem tabFile;
        private System.Windows.Forms.SplitContainer mainsplitcontainer;
        private System.Windows.Forms.ToolStrip Filetoolmenu;
        private System.Windows.Forms.ToolStripButton tsbtnTileVertical;
        private System.Windows.Forms.ToolStripButton tsbtnTileHorizontal;
        private System.Windows.Forms.ToolStripButton tsbtnSinglePane;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbtnFlipPanes;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private iSprite.ThirdControl.FarsiLibrary.FATabStripItem tabTheme;
        private System.Windows.Forms.ToolStripButton tsbtnreSpring;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbtnHelp;
        private System.Windows.Forms.ToolStripButton tsbtnDeb;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private iSprite.ThirdControl.FarsiLibrary.FATabStripItem tabApp;
        private System.Windows.Forms.ToolStrip toolapp;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private System.Windows.Forms.ImageList catalogimg;
        internal System.Windows.Forms.WebBrowser appwb;
        internal iSprite.iPager appPager;
        internal System.Windows.Forms.SplitContainer appContainer;
        internal System.Windows.Forms.TreeView tvCatalog;
        internal System.Windows.Forms.Panel app_Paneltop;
        internal System.Windows.Forms.Panel app_Panelbuttom;


    }
}

