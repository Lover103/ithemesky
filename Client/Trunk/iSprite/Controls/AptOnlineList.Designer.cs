namespace iSprite
{
    partial class AptOnlineList
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
            this.app_Paneltop = new System.Windows.Forms.Panel();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.chbCatalog = new System.Windows.Forms.ComboBox();
            this.app_Panelbuttom = new System.Windows.Forms.Panel();
            this.appPager = new iSprite.iPager();
            this.appwb = new System.Windows.Forms.WebBrowser();
            this.app_Paneltop.SuspendLayout();
            this.app_Panelbuttom.SuspendLayout();
            this.SuspendLayout();
            // 
            // app_Paneltop
            // 
            this.app_Paneltop.Controls.Add(this.txtKey);
            this.app_Paneltop.Controls.Add(this.btnGo);
            this.app_Paneltop.Controls.Add(this.chbCatalog);
            this.app_Paneltop.Dock = System.Windows.Forms.DockStyle.Top;
            this.app_Paneltop.Location = new System.Drawing.Point(0, 0);
            this.app_Paneltop.Name = "app_Paneltop";
            this.app_Paneltop.Size = new System.Drawing.Size(775, 30);
            this.app_Paneltop.TabIndex = 1;
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(1, 4);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(471, 21);
            this.txtKey.TabIndex = 8;
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(698, 4);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 23);
            this.btnGo.TabIndex = 6;
            this.btnGo.Text = "Search";
            this.btnGo.UseVisualStyleBackColor = true;
            // 
            // chbCatalog
            // 
            this.chbCatalog.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.chbCatalog.FormattingEnabled = true;
            this.chbCatalog.Items.AddRange(new object[] {
            "All Packeges"});
            this.chbCatalog.Location = new System.Drawing.Point(571, 5);
            this.chbCatalog.Name = "chbCatalog";
            this.chbCatalog.Size = new System.Drawing.Size(121, 20);
            this.chbCatalog.TabIndex = 7;
            // 
            // app_Panelbuttom
            // 
            this.app_Panelbuttom.Controls.Add(this.appPager);
            this.app_Panelbuttom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.app_Panelbuttom.Location = new System.Drawing.Point(0, 520);
            this.app_Panelbuttom.Name = "app_Panelbuttom";
            this.app_Panelbuttom.Size = new System.Drawing.Size(775, 30);
            this.app_Panelbuttom.TabIndex = 2;
            // 
            // appPager
            // 
            this.appPager.CurrentPageIndex = 0;
            this.appPager.Enabled = false;
            this.appPager.Location = new System.Drawing.Point(530, 5);
            this.appPager.Name = "appPager";
            this.appPager.PageCount = 0;
            this.appPager.PageSize = 20;
            this.appPager.RecordCount = 0;
            this.appPager.Size = new System.Drawing.Size(248, 30);
            this.appPager.TabIndex = 0;
            // 
            // appwb
            // 
            this.appwb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.appwb.Location = new System.Drawing.Point(0, 30);
            this.appwb.MinimumSize = new System.Drawing.Size(20, 20);
            this.appwb.Name = "appwb";
            this.appwb.Size = new System.Drawing.Size(775, 490);
            this.appwb.TabIndex = 3;
            // 
            // AptOnlineList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.appwb);
            this.Controls.Add(this.app_Panelbuttom);
            this.Controls.Add(this.app_Paneltop);
            this.Name = "AptOnlineList";
            this.Size = new System.Drawing.Size(775, 550);
            this.app_Paneltop.ResumeLayout(false);
            this.app_Paneltop.PerformLayout();
            this.app_Panelbuttom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel app_Paneltop;
        internal System.Windows.Forms.Panel app_Panelbuttom;
        internal iPager appPager;
        internal System.Windows.Forms.WebBrowser appwb;
        internal System.Windows.Forms.TextBox txtKey;
        internal System.Windows.Forms.Button btnGo;
        internal System.Windows.Forms.ComboBox chbCatalog;
    }
}
