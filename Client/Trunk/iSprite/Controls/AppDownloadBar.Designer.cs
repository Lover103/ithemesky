namespace iSprite
{
    partial class AppDownloadBar
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
            this.toolbtnStart = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolbtnPause = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolbtnRemove = new System.Windows.Forms.ToolStripButton();
            this.toolapp.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolapp
            // 
            this.toolapp.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.toolapp.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolapp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator7,
            this.toolbtnStart,
            this.toolStripSeparator2,
            this.toolbtnPause,
            this.toolStripSeparator1,
            this.toolbtnRemove});
            this.toolapp.Location = new System.Drawing.Point(0, 0);
            this.toolapp.Name = "toolapp";
            this.toolapp.Size = new System.Drawing.Size(775, 25);
            this.toolapp.TabIndex = 15;
            this.toolapp.Text = "toolStrip3";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // toolbtnStart
            // 
            this.toolbtnStart.Image = global::iSprite.Resource.btn_start;
            this.toolbtnStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbtnStart.Name = "toolbtnStart";
            this.toolbtnStart.Size = new System.Drawing.Size(55, 22);
            this.toolbtnStart.Text = "Start";
            this.toolbtnStart.ToolTipText = "Start";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolbtnPause
            // 
            this.toolbtnPause.Image = global::iSprite.Resource.btn_pause;
            this.toolbtnPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbtnPause.Name = "toolbtnPause";
            this.toolbtnPause.Size = new System.Drawing.Size(55, 22);
            this.toolbtnPause.Text = "Pause";
            this.toolbtnPause.ToolTipText = "Pause";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolbtnRemove
            // 
            this.toolbtnRemove.Image = global::iSprite.Resource.Remove;
            this.toolbtnRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbtnRemove.Name = "toolbtnRemove";
            this.toolbtnRemove.Size = new System.Drawing.Size(61, 22);
            this.toolbtnRemove.Text = "Remove";
            this.toolbtnRemove.ToolTipText = "Remove";
            // 
            // AppDownloadBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolapp);
            this.Name = "AppDownloadBar";
            this.Size = new System.Drawing.Size(775, 30);
            this.toolapp.ResumeLayout(false);
            this.toolapp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolapp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton toolbtnStart;
        private System.Windows.Forms.ToolStripButton toolbtnPause;
        private System.Windows.Forms.ToolStripButton toolbtnRemove;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}
