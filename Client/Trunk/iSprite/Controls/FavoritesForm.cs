using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace iSprite
{
    internal partial class FavoritesForm : iSpriteForm
    {
        private static FavoritesForm m_FavForm;
        private Favourites m_Favourites;

        public FavoritesForm()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
            this.ShowInTaskbar = false;
            this.FormClosed += new FormClosedEventHandler(FavoritesForm_FormClosed);

            this.list.LabelEdit = true;
            this.list.BeforeLabelEdit += new LabelEditEventHandler(list_BeforeLabelEdit);
            this.list.AfterLabelEdit += new LabelEditEventHandler(list_AfterLabelEdit);
        }

        void list_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Label != null && e.Label.Length > 0)
            {
                ListViewItem item = this.list.Items[e.Item];
                if (item.Text != e.Label)
                {
                    if (!IsExist(item.Index, e.Label))
                    {
                        item.Text = e.Label;
                    }
                    else
                    {
                        e.CancelEdit = true;
                        MessageHelper.ShowError("This name already exists !");
                    }
                }
            }
        }

        bool IsExist(int index,string newName)
        {
            foreach (ListViewItem item in this.list.Items)
            {
                if (item.Index != index && item.Text == newName)
                {
                    return true;
                }
            }
            return false;
        }

        void list_BeforeLabelEdit(object sender, LabelEditEventArgs e)
        {
            e.CancelEdit = false;
        }

        void FavoritesForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dictionary<string, string> dicPaths = new Dictionary<string, string>();

            foreach (ListViewItem item in this.list.Items)
            {
                dicPaths.Add(item.Text, item.Name);
            }

            m_Favourites.Save(dicPaths);
        }

        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialise(Favourites fav, Dictionary<string, string> dicPaths)
        {
            m_Favourites = fav;
            this.list.View = View.Details;
            this.list.Columns.Add("Name", "Name");
            this.list.Columns.Add("Path", "Path");
            this.list.Items.Clear();
            foreach (KeyValuePair<string, string> current in dicPaths)
            {
                this.list.Items.Add(CreateItem(current));
            }
            this.list.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            if (this.list.Columns[0].Width < 100)
            {
                this.list.Columns[0].Width = 100;
            }
            this.list.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent); 
            if (this.list.Columns[1].Width < 100)
            {
                this.list.Columns[1].Width = 100;
            }

        }

        #region 创建行对象
        /// <summary>
        /// 创建行对象
        /// </summary>
        /// <param name="name"></param>
        /// <param name="itemType"></param>
        /// <returns></returns>
        ListViewItem CreateItem(KeyValuePair<string, string> favitem)
        {
            ListViewItem item = new ListViewItem();
            item.Text = favitem.Key;
            item.Name = favitem.Value;
            item.SubItems.Add(new ListViewItem.ListViewSubItem().Text = favitem.Value);
            return item;
        }
        #endregion

        #endregion


        public static DialogResult Show(Favourites fav, Dictionary<string, string> dicPaths)
        {
            m_FavForm = new FavoritesForm();
            m_FavForm.Initialise(fav, dicPaths);
            return m_FavForm.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem item in this.list.SelectedItems)
            {
                this.list.Items.RemoveByKey(item.Text);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
