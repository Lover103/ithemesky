namespace iSprite
{
    partial class CydiaSourceList
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
            this.components = new System.ComponentModel.Container();
            this.toolapp = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolbtnAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolbtnUpdate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolbtnRemove = new System.Windows.Forms.ToolStripButton();
            this.app_Panelbuttom = new System.Windows.Forms.Panel();
            this.toolapp.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolapp
            // 
            this.toolapp.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.toolapp.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolapp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator7,
            this.toolbtnAdd,
            this.toolStripSeparator2,
            this.toolbtnUpdate,
            this.toolStripSeparator1,
            this.toolbtnRemove});
            this.toolapp.Location = new System.Drawing.Point(0, 0);
            this.toolapp.Name = "toolapp";
            this.toolapp.Size = new System.Drawing.Size(775, 25);
            this.toolapp.TabIndex = 17;
            this.toolapp.Text = "toolStrip3";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // toolbtnAdd
            // 
            this.toolbtnAdd.Image = global::iSprite.Resource.btn_add;
            this.toolbtnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbtnAdd.Name = "toolbtnAdd";
            this.toolbtnAdd.Size = new System.Drawing.Size(85, 22);
            this.toolbtnAdd.Text = "Add Source";
            this.toolbtnAdd.ToolTipText = "Add Source";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolbtnUpdate
            // 
            this.toolbtnUpdate.Image = global::iSprite.Resource.btn_refresh;
            this.toolbtnUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbtnUpdate.Name = "toolbtnUpdate";
            this.toolbtnUpdate.Size = new System.Drawing.Size(103, 22);
            this.toolbtnUpdate.Text = "Update Source";
            this.toolbtnUpdate.ToolTipText = "Update Source";
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
            this.toolbtnRemove.Size = new System.Drawing.Size(103, 22);
            this.toolbtnRemove.Text = "Remove Source";
            this.toolbtnRemove.ToolTipText = "Remove Source";
            // 
            // app_Panelbuttom
            // 
            this.app_Panelbuttom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.app_Panelbuttom.Location = new System.Drawing.Point(0, 520);
            this.app_Panelbuttom.Name = "app_Panelbuttom";
            this.app_Panelbuttom.Size = new System.Drawing.Size(775, 30);
            this.app_Panelbuttom.TabIndex = 18;
            // 
            // CydiaSourceList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.app_Panelbuttom);
            this.Controls.Add(this.toolapp);
            this.Name = "CydiaSourceList";
            this.Size = new System.Drawing.Size(775, 550);
            this.toolapp.ResumeLayout(false);
            this.toolapp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolapp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton toolbtnAdd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolbtnUpdate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolbtnRemove;
        internal System.Windows.Forms.Panel app_Panelbuttom;
    }
}
