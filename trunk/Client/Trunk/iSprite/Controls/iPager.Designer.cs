namespace iSprite
{
    partial class iPager
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
            this.toolapp = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolbtnFirst = new System.Windows.Forms.ToolStripButton();
            this.toolbtnPrevious = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolpageIndex = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtPageCount = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolbtnNext = new System.Windows.Forms.ToolStripButton();
            this.toolbtnLast = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolapp.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolapp
            // 
            this.toolapp.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.toolapp.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolapp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator7,
            this.toolbtnFirst,
            this.toolbtnPrevious,
            this.toolStripSeparator1,
            this.toolpageIndex,
            this.toolStripLabel1,
            this.txtPageCount,
            this.toolStripSeparator8,
            this.toolbtnNext,
            this.toolbtnLast,
            this.toolStripSeparator2});
            this.toolapp.Location = new System.Drawing.Point(0, 0);
            this.toolapp.Name = "toolapp";
            this.toolapp.Size = new System.Drawing.Size(248, 25);
            this.toolapp.TabIndex = 14;
            this.toolapp.Text = "toolStrip3";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // toolbtnFirst
            // 
            this.toolbtnFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolbtnFirst.Image = global::iSprite.Resource.btnFirst;
            this.toolbtnFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbtnFirst.Name = "toolbtnFirst";
            this.toolbtnFirst.Size = new System.Drawing.Size(23, 22);
            this.toolbtnFirst.Text = "Go to first";
            this.toolbtnFirst.ToolTipText = "Go to first";
            // 
            // toolbtnPrevious
            // 
            this.toolbtnPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolbtnPrevious.Image = global::iSprite.Resource.btnPrevious;
            this.toolbtnPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbtnPrevious.Name = "toolbtnPrevious";
            this.toolbtnPrevious.Size = new System.Drawing.Size(23, 22);
            this.toolbtnPrevious.Text = "Go to Previous";
            this.toolbtnPrevious.ToolTipText = "Go to Previous";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolpageIndex
            // 
            this.toolpageIndex.AutoSize = false;
            this.toolpageIndex.DropDownWidth = 50;
            this.toolpageIndex.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.toolpageIndex.Name = "toolpageIndex";
            this.toolpageIndex.Size = new System.Drawing.Size(75, 20);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(11, 22);
            this.toolStripLabel1.Text = "/";
            // 
            // txtPageCount
            // 
            this.txtPageCount.Name = "txtPageCount";
            this.txtPageCount.ReadOnly = true;
            this.txtPageCount.Size = new System.Drawing.Size(36, 25);
            this.txtPageCount.Text = "1";
            this.txtPageCount.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // toolbtnNext
            // 
            this.toolbtnNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolbtnNext.Image = global::iSprite.Resource.btnNext;
            this.toolbtnNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbtnNext.Name = "toolbtnNext";
            this.toolbtnNext.Size = new System.Drawing.Size(23, 22);
            this.toolbtnNext.Text = "Go to Next";
            this.toolbtnNext.ToolTipText = "Go to Next";
            // 
            // toolbtnLast
            // 
            this.toolbtnLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolbtnLast.Image = global::iSprite.Resource.btnLast;
            this.toolbtnLast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbtnLast.Name = "toolbtnLast";
            this.toolbtnLast.Size = new System.Drawing.Size(23, 20);
            this.toolbtnLast.Text = "Go to Last";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // iPager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolapp);
            this.Name = "iPager";
            this.Size = new System.Drawing.Size(248, 30);
            this.toolapp.ResumeLayout(false);
            this.toolapp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolapp;
        private System.Windows.Forms.ToolStripButton toolbtnFirst;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton toolbtnPrevious;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton toolbtnNext;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripComboBox toolpageIndex;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox txtPageCount;
        private System.Windows.Forms.ToolStripButton toolbtnLast;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}
