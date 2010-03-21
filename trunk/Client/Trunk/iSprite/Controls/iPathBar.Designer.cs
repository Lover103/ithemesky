namespace iSprite
{
    partial class iPathBar
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(iPathBar));
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsbtnFavourites = new System.Windows.Forms.ToolStripSplitButton();
            this.ctxFavourites = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnUp = new System.Windows.Forms.ToolStripButton();
            this.btnBack = new System.Windows.Forms.ToolStripButton();
            this.tsbtnGo = new System.Windows.Forms.ToolStripButton();
            this.ctxPaths = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cbbpathlist = new System.Windows.Forms.ComboBox();
            this.tsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsMain
            // 
            this.tsMain.AllowMerge = false;
            this.tsMain.AutoSize = false;
            this.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnFavourites,
            this.btnUp,
            this.btnBack,
            this.tsbtnGo});
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(490, 23);
            this.tsMain.TabIndex = 2;
            // 
            // tsbtnFavourites
            // 
            this.tsbtnFavourites.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnFavourites.AutoSize = false;
            this.tsbtnFavourites.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnFavourites.DoubleClickEnabled = true;
            this.tsbtnFavourites.DropDown = this.ctxFavourites;
            this.tsbtnFavourites.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnFavourites.Image")));
            this.tsbtnFavourites.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnFavourites.Name = "tsbtnFavourites";
            this.tsbtnFavourites.Size = new System.Drawing.Size(32, 22);
            this.tsbtnFavourites.ToolTipText = "Add To Favourites";
            this.tsbtnFavourites.ButtonClick += new System.EventHandler(this.tsbtnFavourites_ButtonClick);
            // 
            // ctxFavourites
            // 
            this.ctxFavourites.Name = "ctxFavourites";
            this.ctxFavourites.OwnerItem = this.tsbtnFavourites;
            this.ctxFavourites.Size = new System.Drawing.Size(153, 26);
            // 
            // btnUp
            // 
            this.btnUp.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.Image")));
            this.btnUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(23, 20);
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnBack
            // 
            this.btnBack.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBack.Image = ((System.Drawing.Image)(resources.GetObject("btnBack.Image")));
            this.btnBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(23, 20);
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // tsbtnGo
            // 
            this.tsbtnGo.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnGo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnGo.Image = global::iSprite.Resource.btn_go;
            this.tsbtnGo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnGo.Name = "tsbtnGo";
            this.tsbtnGo.Size = new System.Drawing.Size(23, 20);
            this.tsbtnGo.Text = "Go";
            this.tsbtnGo.ToolTipText = "Go";
            this.tsbtnGo.Click += new System.EventHandler(this.tsbtnGo_Click);
            // 
            // ctxPaths
            // 
            this.ctxPaths.Name = "ctxPaths";
            this.ctxPaths.Size = new System.Drawing.Size(61, 4);
            // 
            // cbbpathlist
            // 
            this.cbbpathlist.FormattingEnabled = true;
            this.cbbpathlist.Location = new System.Drawing.Point(3, 0);
            this.cbbpathlist.Name = "cbbpathlist";
            this.cbbpathlist.Size = new System.Drawing.Size(115, 20);
            this.cbbpathlist.TabIndex = 3;
            this.cbbpathlist.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbbpathlist_KeyDown);
            // 
            // PathBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbbpathlist);
            this.Controls.Add(this.tsMain);
            this.Name = "PathBar";
            this.Size = new System.Drawing.Size(490, 25);
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripSplitButton tsbtnFavourites;
        private System.Windows.Forms.ContextMenuStrip ctxFavourites;
        private System.Windows.Forms.ToolStripButton btnUp;
        private System.Windows.Forms.ToolStripButton btnBack;
        private System.Windows.Forms.ContextMenuStrip ctxPaths;
        private System.Windows.Forms.ComboBox cbbpathlist;
        private System.Windows.Forms.ToolStripButton tsbtnGo;
    }
}
