using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace iSprite
{
    public partial class AppSearchBar : UserControl
    {
        internal event SearchHandler OnSearch;
        internal void RaiseSearch(string key, string catalogName)
        {
            if (OnSearch != null)
            {
                OnSearch(key, catalogName);
            }
        }

        public AppSearchBar()
        {
            InitializeComponent();

            txtKey.KeyDown += new KeyEventHandler
                (
                    delegate(object sender, KeyEventArgs e)
                    {
                        if (e.KeyCode == Keys.Enter)
                        {
                            Search();
                        }
                    }
                );
            txtKey.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtKey.AutoCompleteSource = AutoCompleteSource.CustomSource;

            btnGo.Click += new EventHandler
                (
                    delegate(object sender, EventArgs e)
                    {
                        Search();
                    }
                );
        }

        public void LoadData(List<string> catalist,List<string> appNames)
        {
            chbCatalog.Items.Clear();
            chbCatalog.Items.Add("All Packages");

            foreach (string name in catalist)
            {
                this.chbCatalog.Items.Add(name);
                Application.DoEvents();
            }
            chbCatalog.SelectedText = "All Packages";

            txtKey.AutoCompleteCustomSource.AddRange(appNames.ToArray());  //搜索提示  
        }

        #region 搜索
        /// <summary>
        /// 搜索
        /// </summary>
        void Search()
        {
            string key = txtKey.Text.Trim();
            string catalog = string.Empty;
            if (null != chbCatalog.SelectedItem)
            {
                catalog = chbCatalog.SelectedItem.ToString();
            }
            else
            {
                catalog = chbCatalog.Text;
            }
            RaiseSearch(key, catalog);
        }
        #endregion
    }

    public delegate void SearchHandler(string key, string catalogName);
}
