namespace iSprite
{
    partial class FavoritesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.list = new System.Windows.Forms.ListView();
            this.btnDelete = new iSprite.iSpriteButton();
            this.btnSave = new iSprite.iSpriteButton();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            this.SuspendLayout();
            // 
            // list
            // 
            this.list.Location = new System.Drawing.Point(8, 31);
            this.list.Name = "list";
            this.list.Size = new System.Drawing.Size(382, 185);
            this.list.TabIndex = 4;
            this.list.UseCompatibleStateImageBehavior = false;
            // 
            // btnDelete
            // 
            this.btnDelete.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnDelete.DownImage = global::iSprite.Resource.Img_button_down;
            this.btnDelete.HoverImage = null;
            this.btnDelete.Location = new System.Drawing.Point(106, 220);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.NormalImage = global::iSprite.Resource.Img_button;
            this.btnDelete.Size = new System.Drawing.Size(75, 25);
            this.btnDelete.TabIndex = 94;
            this.btnDelete.TabStop = false;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSave.DownImage = global::iSprite.Resource.Img_button_down;
            this.btnSave.HoverImage = null;
            this.btnSave.Location = new System.Drawing.Point(217, 220);
            this.btnSave.Name = "btnSave";
            this.btnSave.NormalImage = global::iSprite.Resource.Img_button;
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.TabIndex = 93;
            this.btnSave.TabStop = false;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // FavoritesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::iSprite.Resource.form_bg;
            this.ClientSize = new System.Drawing.Size(398, 258);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.list);
            this.Name = "FavoritesForm";
            this.Text = "FavoritesForm";
            this.Controls.SetChildIndex(this.list, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.Controls.SetChildIndex(this.btnDelete, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView list;
        private iSpriteButton btnDelete;
        private iSpriteButton btnSave;
    }
}