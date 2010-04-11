namespace iSprite
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
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
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
            this.tsbtnreSpring = new System.Windows.Forms.ToolStripButton();
            this.tabTheme = new iSprite.ThirdControl.FarsiLibrary.FATabStripItem();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.faTabStrip1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabs)).BeginInit();
            this.tabs.SuspendLayout();
            this.tabFile.SuspendLayout();
            this.mainsplitcontainer.SuspendLayout();
            this.Filetoolmenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label4.ForeColor = System.Drawing.Color.ForestGreen;
            this.label4.Location = new System.Drawing.Point(3, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(814, 81);
            this.label4.TabIndex = 0;
            this.label4.Text = resources.GetString("label4.Text");
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Millimeter, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(71, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(678, 106);
            this.label3.TabIndex = 0;
            this.label3.Text = "Drag && Drop files here from\r\nwindows explorer\r\nin order to have them automatical" +
                "ly opened in new tabs";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.SandyBrown;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 126);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(820, 182);
            this.panel2.TabIndex = 4;
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
            this.tabTheme});
            this.tabs.Location = new System.Drawing.Point(3, 30);
            this.tabs.Name = "tabs";
            this.tabs.SelectedItem = this.tabFile;
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
            this.tabFile.Selected = true;
            this.tabFile.Size = new System.Drawing.Size(991, 619);
            this.tabFile.TabIndex = 0;
            this.tabFile.Title = "File Manage";
            // 
            // mainsplitcontainer
            // 
            this.mainsplitcontainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainsplitcontainer.Location = new System.Drawing.Point(0, 25);
            this.mainsplitcontainer.Margin = new System.Windows.Forms.Padding(0);
            this.mainsplitcontainer.Name = "mainsplitcontainer";
            this.mainsplitcontainer.Panel1MinSize = 0;
            this.mainsplitcontainer.Panel2MinSize = 0;
            this.mainsplitcontainer.Size = new System.Drawing.Size(991, 594);
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
            this.tsbtnreSpring});
            this.Filetoolmenu.Location = new System.Drawing.Point(0, 0);
            this.Filetoolmenu.Name = "Filetoolmenu";
            this.Filetoolmenu.Size = new System.Drawing.Size(991, 25);
            this.Filetoolmenu.TabIndex = 12;
            this.Filetoolmenu.Text = "toolStrip3";
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
            // tsbtnreSpring
            // 
            this.tsbtnreSpring.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnreSpring.Image = global::iSprite.Resource.quick_restart;
            this.tsbtnreSpring.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnreSpring.Name = "tsbtnreSpring";
            this.tsbtnreSpring.Size = new System.Drawing.Size(23, 22);
            this.tsbtnreSpring.Text = "reSpring";
            this.tsbtnreSpring.ToolTipText = "Restart SpringBoard";
            // 
            // tabTheme
            // 
            this.tabTheme.IsDrawn = true;
            this.tabTheme.Name = "tabTheme";
            this.tabTheme.Size = new System.Drawing.Size(991, 619);
            this.tabTheme.TabIndex = 1;
            this.tabTheme.Title = "Theme Manage";
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
            this.Text = "iSprite";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Controls.SetChildIndex(this.faTabStrip1, 0);
            this.Controls.SetChildIndex(this.tabs, 0);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.faTabStrip1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabs)).EndInit();
            this.tabs.ResumeLayout(false);
            this.tabFile.ResumeLayout(false);
            this.tabFile.PerformLayout();
            this.mainsplitcontainer.ResumeLayout(false);
            this.Filetoolmenu.ResumeLayout(false);
            this.Filetoolmenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
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


    }
}

