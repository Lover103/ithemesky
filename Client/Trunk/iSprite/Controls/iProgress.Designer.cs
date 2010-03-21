namespace iSprite
{
    partial class iProgress
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
            this.progressbar = new System.Windows.Forms.ProgressBar();
            this.lblpercent = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // progressbar
            // 
            this.progressbar.ForeColor = System.Drawing.SystemColors.Desktop;
            this.progressbar.Location = new System.Drawing.Point(3, 3);
            this.progressbar.Name = "progressbar";
            this.progressbar.Size = new System.Drawing.Size(425, 33);
            this.progressbar.TabIndex = 0;
            this.progressbar.Value = 50;
            // 
            // lblpercent
            // 
            this.lblpercent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblpercent.AutoSize = true;
            this.lblpercent.BackColor = System.Drawing.Color.Transparent;
            this.lblpercent.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblpercent.Location = new System.Drawing.Point(164, 8);
            this.lblpercent.Name = "lblpercent";
            this.lblpercent.Size = new System.Drawing.Size(46, 24);
            this.lblpercent.TabIndex = 1;
            this.lblpercent.Text = "50%";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.BackColor = System.Drawing.SystemColors.Control;
            this.lblStatus.Location = new System.Drawing.Point(3, 47);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(47, 12);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Message";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(353, 42);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // iProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkRed;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblpercent);
            this.Controls.Add(this.progressbar);
            this.Name = "iProgress";
            this.Size = new System.Drawing.Size(432, 72);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressbar;
        private System.Windows.Forms.Label lblpercent;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnCancel;
    }
}
