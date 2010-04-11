namespace iSprite
{
    partial class ThemePreviewItem
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
            this.picbox = new System.Windows.Forms.PictureBox();
            this.chlselect = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.picbox)).BeginInit();
            this.SuspendLayout();
            // 
            // picbox
            // 
            this.picbox.Location = new System.Drawing.Point(10, 6);
            this.picbox.Name = "picbox";
            this.picbox.Size = new System.Drawing.Size(160, 240);
            this.picbox.TabIndex = 0;
            this.picbox.TabStop = false;
            // 
            // chlselect
            // 
            this.chlselect.AutoSize = true;
            this.chlselect.Location = new System.Drawing.Point(10, 252);
            this.chlselect.Name = "chlselect";
            this.chlselect.Size = new System.Drawing.Size(78, 16);
            this.chlselect.TabIndex = 1;
            this.chlselect.Text = "ThemeName";
            this.chlselect.UseVisualStyleBackColor = true;
            // 
            // ThemePreviewItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Controls.Add(this.chlselect);
            this.Controls.Add(this.picbox);
            this.Name = "ThemePreviewItem";
            this.Size = new System.Drawing.Size(180, 271);
            ((System.ComponentModel.ISupportInitialize)(this.picbox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picbox;
        private System.Windows.Forms.CheckBox chlselect;

    }
}
