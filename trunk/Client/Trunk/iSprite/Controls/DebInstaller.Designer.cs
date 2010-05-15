namespace iSprite
{
    partial class DebInstaller
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
            this.btnSelect = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.lnkHelp = new System.Windows.Forms.LinkLabel();
            this.rb4 = new System.Windows.Forms.RadioButton();
            this.rb3 = new System.Windows.Forms.RadioButton();
            this.rb2 = new System.Windows.Forms.RadioButton();
            this.rb1 = new System.Windows.Forms.RadioButton();
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
            this.btnOK.Location = new System.Drawing.Point(267, 224);
            this.btnOK.Name = "btnOK";
            this.btnOK.NormalImage = global::iSprite.Resource.Img_button;
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 0;
            this.btnOK.TabStop = false;
            this.btnOK.Text = "Install";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnCancel.DownImage = null;
            this.btnCancel.HoverImage = null;
            this.btnCancel.Location = new System.Drawing.Point(152, 224);
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
            this.gb.Controls.Add(this.btnSelect);
            this.gb.Controls.Add(this.txtFileName);
            this.gb.Controls.Add(this.lnkHelp);
            this.gb.Controls.Add(this.rb4);
            this.gb.Controls.Add(this.rb3);
            this.gb.Controls.Add(this.rb2);
            this.gb.Controls.Add(this.rb1);
            this.gb.Location = new System.Drawing.Point(30, 39);
            this.gb.Name = "gb";
            this.gb.Size = new System.Drawing.Size(402, 171);
            this.gb.TabIndex = 5;
            this.gb.TabStop = false;
            this.gb.Text = "Choose Installing Method";
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(289, 114);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(89, 23);
            this.btnSelect.TabIndex = 6;
            this.btnSelect.Text = "Select File";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(23, 116);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(260, 21);
            this.txtFileName.TabIndex = 5;
            // 
            // lnkHelp
            // 
            this.lnkHelp.AutoSize = true;
            this.lnkHelp.Location = new System.Drawing.Point(24, 140);
            this.lnkHelp.Name = "lnkHelp";
            this.lnkHelp.Size = new System.Drawing.Size(173, 12);
            this.lnkHelp.TabIndex = 4;
            this.lnkHelp.TabStop = true;
            this.lnkHelp.Text = "Get help to install deb file";
            this.lnkHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkHelp_LinkClicked);
            // 
            // rb4
            // 
            this.rb4.AutoSize = true;
            this.rb4.Location = new System.Drawing.Point(23, 86);
            this.rb4.Name = "rb4";
            this.rb4.Size = new System.Drawing.Size(143, 16);
            this.rb4.TabIndex = 3;
            this.rb4.TabStop = true;
            this.rb4.Text = "Use Mobile Terminal ";
            this.rb4.UseVisualStyleBackColor = true;
            // 
            // rb3
            // 
            this.rb3.AutoSize = true;
            this.rb3.Location = new System.Drawing.Point(23, 64);
            this.rb3.Name = "rb3";
            this.rb3.Size = new System.Drawing.Size(107, 16);
            this.rb3.TabIndex = 2;
            this.rb3.TabStop = true;
            this.rb3.Text = "Use install0us";
            this.rb3.UseVisualStyleBackColor = true;
            // 
            // rb2
            // 
            this.rb2.AutoSize = true;
            this.rb2.Location = new System.Drawing.Point(23, 42);
            this.rb2.Name = "rb2";
            this.rb2.Size = new System.Drawing.Size(77, 16);
            this.rb2.TabIndex = 1;
            this.rb2.TabStop = true;
            this.rb2.Text = "Use iFile";
            this.rb2.UseVisualStyleBackColor = true;
            // 
            // rb1
            // 
            this.rb1.AutoSize = true;
            this.rb1.Location = new System.Drawing.Point(23, 20);
            this.rb1.Name = "rb1";
            this.rb1.Size = new System.Drawing.Size(203, 16);
            this.rb1.TabIndex = 0;
            this.rb1.TabStop = true;
            this.rb1.Text = "Use Cydia (must reboot iPhone)";
            this.rb1.UseVisualStyleBackColor = true;
            // 
            // DebInstaller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::iSprite.Resource.form_bg;
            this.ClientSize = new System.Drawing.Size(458, 261);
            this.Controls.Add(this.gb);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Name = "DebInstaller";
            this.Text = "Deb Installer";
            ((System.ComponentModel.ISupportInitialize)(this.btnOK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            this.gb.ResumeLayout(false);
            this.gb.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private iSpriteButton btnOK;
        private iSpriteButton btnCancel;
        private System.Windows.Forms.GroupBox gb;
        private System.Windows.Forms.RadioButton rb4;
        private System.Windows.Forms.RadioButton rb3;
        private System.Windows.Forms.RadioButton rb2;
        private System.Windows.Forms.RadioButton rb1;
        private System.Windows.Forms.LinkLabel lnkHelp;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.TextBox txtFileName;
    }
}
