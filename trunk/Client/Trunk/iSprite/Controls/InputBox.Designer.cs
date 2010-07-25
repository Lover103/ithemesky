namespace iSprite
{
    partial class InputBox
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
            ((System.ComponentModel.ISupportInitialize)(this.btnOK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOK.DownImage = null;
            this.btnOK.HoverImage = null;
            this.btnOK.Location = new System.Drawing.Point(211, 97);
            this.btnOK.Name = "btnOK";
            this.btnOK.NormalImage = global::iSprite.Resource.Img_button;
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 0;
            this.btnOK.TabStop = false;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnCancel.DownImage = null;
            this.btnCancel.HoverImage = null;
            this.btnCancel.Location = new System.Drawing.Point(99, 97);
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
            this.txtInput.Location = new System.Drawing.Point(35, 56);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(314, 21);
            this.txtInput.TabIndex = 5;
            // 
            // InputBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::iSprite.Resource.form_bg;
            this.ClientSize = new System.Drawing.Size(384, 146);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Name = "InputBox";
            this.Text = "Input";
            ((System.ComponentModel.ISupportInitialize)(this.btnOK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private iSpriteButton btnOK;
        private iSpriteButton btnCancel;
        private System.Windows.Forms.TextBox txtInput;
    }
}
