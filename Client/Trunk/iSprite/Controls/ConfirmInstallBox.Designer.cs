namespace iSprite
{
    partial class ConfirmInstallBox
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
            this.txtInput = new System.Windows.Forms.TextBox();
            this.txtIntroduce = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chb = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnOK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOK.DownImage = null;
            this.btnOK.HoverImage = null;
            this.btnOK.Location = new System.Drawing.Point(211, 307);
            this.btnOK.Name = "btnOK";
            this.btnOK.NormalImage = global::iSprite.Resource.Img_button;
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 0;
            this.btnOK.TabStop = false;
            this.btnOK.Text = "Download";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnCancel.DownImage = null;
            this.btnCancel.HoverImage = null;
            this.btnCancel.Location = new System.Drawing.Point(99, 307);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.NormalImage = global::iSprite.Resource.Img_button;
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(35, 52);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(314, 21);
            this.txtInput.TabIndex = 5;
            // 
            // txtIntroduce
            // 
            this.txtIntroduce.Location = new System.Drawing.Point(35, 99);
            this.txtIntroduce.Multiline = true;
            this.txtIntroduce.Name = "txtIntroduce";
            this.txtIntroduce.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtIntroduce.Size = new System.Drawing.Size(314, 180);
            this.txtIntroduce.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(35, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "AppName:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(35, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "Detail:";
            // 
            // chb
            // 
            this.chb.AutoSize = true;
            this.chb.BackColor = System.Drawing.Color.Transparent;
            this.chb.Checked = true;
            this.chb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb.Location = new System.Drawing.Point(33, 284);
            this.chb.Name = "chb";
            this.chb.Size = new System.Drawing.Size(252, 16);
            this.chb.TabIndex = 9;
            this.chb.Text = "Auto install app after finish download";
            this.chb.UseVisualStyleBackColor = false;
            // 
            // ConfirmInstallBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::iSprite.Resource.form_bg;
            this.ClientSize = new System.Drawing.Size(384, 343);
            this.Controls.Add(this.chb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtIntroduce);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Name = "ConfirmInstallBox";
            this.Text = "Down Confirm";
            ((System.ComponentModel.ISupportInitialize)(this.btnOK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private iSpriteButton btnOK;
        private iSpriteButton btnCancel;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.TextBox txtIntroduce;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chb;
    }
}
