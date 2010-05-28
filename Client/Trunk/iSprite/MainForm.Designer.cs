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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvCatalog = new System.Windows.Forms.TreeView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.listView2 = new System.Windows.Forms.ListView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
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
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
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
            this.tabTheme.IsDrawn = true;
            this.tabTheme.Name = "tabTheme";
            this.tabTheme.Size = new System.Drawing.Size(991, 619);
            this.tabTheme.TabIndex = 1;
            this.tabTheme.Title = "Theme Manage";
            // 
            // tabApp
            // 
            this.tabApp.Controls.Add(this.splitContainer1);
            this.tabApp.Controls.Add(this.toolStrip1);
            this.tabApp.IsDrawn = true;
            this.tabApp.Name = "tabApp";
            this.tabApp.Selected = true;
            this.tabApp.Size = new System.Drawing.Size(991, 619);
            this.tabApp.TabIndex = 2;
            this.tabApp.Title = "App Manage";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvCatalog);
            this.splitContainer1.Panel1MinSize = 0;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2MinSize = 0;
            this.splitContainer1.Size = new System.Drawing.Size(991, 594);
            this.splitContainer1.SplitterDistance = 209;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 14;
            // 
            // tvCatalog
            // 
            this.tvCatalog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvCatalog.Font = new System.Drawing.Font("Arial", 10F);
            this.tvCatalog.Indent = 15;
            this.tvCatalog.ItemHeight = 25;
            this.tvCatalog.Location = new System.Drawing.Point(0, 0);
            this.tvCatalog.Name = "tvCatalog";
            this.tvCatalog.ShowRootLines = false;
            this.tvCatalog.Size = new System.Drawing.Size(209, 594);
            this.tvCatalog.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.textBox1);
            this.splitContainer2.Panel1.Controls.Add(this.comboBox1);
            this.splitContainer2.Panel1.Controls.Add(this.button1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.listView2);
            this.splitContainer2.Size = new System.Drawing.Size(781, 594);
            this.splitContainer2.SplitterDistance = 35;
            this.splitContainer2.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(5, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(533, 21);
            this.textBox1.TabIndex = 2;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "All Packeges"});
            this.comboBox1.Location = new System.Drawing.Point(556, 6);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(703, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Search";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // listView2
            // 
            this.listView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView2.Location = new System.Drawing.Point(0, 0);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(781, 555);
            this.listView2.TabIndex = 0;
            this.listView2.UseCompatibleStateImageBehavior = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton5,
            this.toolStripSeparator7,
            this.toolStripButton6,
            this.toolStripSeparator8,
            this.toolStripButton7});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(991, 25);
            this.toolStrip1.TabIndex = 13;
            this.toolStrip1.Text = "toolStrip3";
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
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
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
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tvCatalog;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListView listView2;


    }
}

