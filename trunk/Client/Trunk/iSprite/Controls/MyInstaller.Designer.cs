namespace iSprite
{
    partial class MyInstaller
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
            this.btnOK = new iSprite.iSpriteButton();
            this.btnCancel = new iSprite.iSpriteButton();
            this.gb = new System.Windows.Forms.GroupBox();
            this.r1 = new System.Windows.Forms.RadioButton();
            this.r2 = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.btnOK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            this.gb.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOK.DownImage = null;
            this.btnOK.HoverImage = null;
            this.btnOK.Location = new System.Drawing.Point(275, 162);
            this.btnOK.Name = "btnOK";
            this.btnOK.NormalImage = global::iSprite.Resource.Img_button;
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 0;
            this.btnOK.TabStop = false;
            this.btnOK.Text = "Next";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnCancel.DownImage = null;
            this.btnCancel.HoverImage = null;
            this.btnCancel.Location = new System.Drawing.Point(109, 162);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.NormalImage = global::iSprite.Resource.Img_button;
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // gb
            // 
            this.gb.BackColor = System.Drawing.Color.Transparent;
            this.gb.Controls.Add(this.r1);
            this.gb.Controls.Add(this.r2);
            this.gb.Location = new System.Drawing.Point(12, 34);
            this.gb.Name = "gb";
            this.gb.Size = new System.Drawing.Size(434, 100);
            this.gb.TabIndex = 5;
            this.gb.TabStop = false;
            this.gb.Text = "Install";
            // 
            // r1
            // 
            this.r1.AutoSize = true;
            this.r1.Checked = true;
            this.r1.Location = new System.Drawing.Point(10, 23);
            this.r1.Name = "r1";
            this.r1.Size = new System.Drawing.Size(317, 16);
            this.r1.TabIndex = 0;
            this.r1.TabStop = true;
            this.r1.Text = "Download {0} and Copy to Cydia AutoInstall Folder";
            this.r1.UseVisualStyleBackColor = true;
            // 
            // r2
            // 
            this.r2.AutoSize = true;
            this.r2.Location = new System.Drawing.Point(10, 63);
            this.r2.Name = "r2";
            this.r2.Size = new System.Drawing.Size(323, 16);
            this.r2.TabIndex = 1;
            this.r2.Text = "Get help to install {0} via Cydia or other methods";
            this.r2.UseVisualStyleBackColor = true;
            // 
            // MyInstaller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::iSprite.Resource.form_bg;
            this.ClientSize = new System.Drawing.Size(458, 204);
            this.Controls.Add(this.gb);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Name = "MyInstaller";
            this.Text = "Deb Installer";
            ((System.ComponentModel.ISupportInitialize)(this.btnOK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            this.gb.ResumeLayout(false);
            this.gb.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private iSpriteButton btnOK;
        private iSpriteButton btnCancel;
        private System.Windows.Forms.GroupBox gb;
        private System.Windows.Forms.RadioButton r2;
        private System.Windows.Forms.RadioButton r1;
    }
}
