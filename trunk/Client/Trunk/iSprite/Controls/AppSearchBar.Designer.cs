namespace iSprite
{
    partial class AppSearchBar
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
            this.txtKey = new System.Windows.Forms.TextBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.chbCatalog = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(0, 3);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(471, 21);
            this.txtKey.TabIndex = 5;
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(697, 3);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 23);
            this.btnGo.TabIndex = 3;
            this.btnGo.Text = "Search";
            this.btnGo.UseVisualStyleBackColor = true;
            // 
            // chbCatalog
            // 
            this.chbCatalog.FormattingEnabled = true;
            this.chbCatalog.Items.AddRange(new object[] {
            "All Packeges"});
            this.chbCatalog.Location = new System.Drawing.Point(570, 4);
            this.chbCatalog.Name = "chbCatalog";
            this.chbCatalog.Size = new System.Drawing.Size(121, 20);
            this.chbCatalog.TabIndex = 4;
            // 
            // AppSearchBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.chbCatalog);
            this.Name = "AppSearchBar";
            this.Size = new System.Drawing.Size(775, 30);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtKey;
        internal System.Windows.Forms.Button btnGo;
        internal System.Windows.Forms.ComboBox chbCatalog;
    }
}
