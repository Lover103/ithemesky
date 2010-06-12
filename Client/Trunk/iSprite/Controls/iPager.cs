using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace iSprite
{
    public partial class iPager : UserControl
    {
        public event PageChangedEventHandler OnPageChanged;
        public iPager()
        {
            InitializeComponent();

            toolpageIndex.Size = new Size(50, 25);
            toolpageIndex.SelectedIndexChanged += new EventHandler(toolpageIndex_SelectedIndexChanged);
            toolpageIndex.KeyDown += new KeyEventHandler(toolpageIndex_KeyDown);
            this.Enabled = false;

            #region 事件处理
            this.toolbtnFirst.Click += new EventHandler
                (
                    delegate(object sender, EventArgs e)
                    {
                        CurrentPageIndex = 1;
                        this.DataBind();
                        PageChanged(CurrentPageIndex);
                    }
                );

            this.toolbtnPrevious.Click += new EventHandler
                (
                    delegate(object sender, EventArgs e)
                    {
                        CurrentPageIndex -= 1;
                        if (CurrentPageIndex <= 0)
                        {
                            CurrentPageIndex = 1;
                        }
                        this.DataBind();
                        PageChanged(CurrentPageIndex);
                    }
                );

            this.toolbtnNext.Click += new EventHandler
                (
                    delegate(object sender, EventArgs e)
                    {
                        this.CurrentPageIndex += 1;
                        if (CurrentPageIndex > PageCount)
                        {
                            CurrentPageIndex = PageCount;
                        }
                        this.DataBind();
                        PageChanged(CurrentPageIndex);
                    }
                );

            this.toolbtnLast.Click += new EventHandler
                (
                    delegate(object sender, EventArgs e)
                    {
                        CurrentPageIndex = PageCount;
                        this.DataBind();
                        PageChanged(CurrentPageIndex);
                    }
                );
            #endregion
        }

        #region 下拉框事件
        void toolpageIndex_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int pageindex = 0;
                if (!int.TryParse(toolpageIndex.Text, out pageindex))
                {
                    pageindex = 1;
                }
                CurrentPageIndex = pageindex;
                this.DataBind();
                PageChanged(CurrentPageIndex);
            }
        }

        void toolpageIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            int pageindex = 0;
            if (!int.TryParse(toolpageIndex.SelectedItem.ToString(), out pageindex))
            {
                pageindex = 1;
            }
            CurrentPageIndex = pageindex;
            this.DataBind();
            PageChanged(CurrentPageIndex);
        }
        #endregion

        #region 属性
        /// <summary>
        /// 每页显示记录数
        /// </summary>
        private int _pageSize = 20;
        /// <summary>
        /// 每页显示记录数
        /// </summary>
        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                _pageSize = value;
                GetPageCount();
            }
        }

        private int _RecordCount = 0;
        /// <summary>
        /// 总记录数
        /// </summary>
        public int RecordCount
        {
            get { return _RecordCount; }
            set
            {
                if (_RecordCount != value)
                {
                    _RecordCount = value;
                    GetPageCount();
                    this.toolpageIndex.Items.Clear();
                    for (int i = 1; i <= PageCount; i++)
                    {
                        this.toolpageIndex.Items.Add(i);
                    }
                }
            }
        }

        private int _pageCount = 0;
        /// <summary>
        /// 页数=总记录数/每页显示记录数
        /// </summary>
        public int PageCount
        {
            get { return _pageCount; }
            set { _pageCount = value; }
        }

        private int _CurrentPageIndex = 1;
        /// <summary>
        /// 当前页号
        /// </summary>
        public int CurrentPageIndex
        {
            get { return _CurrentPageIndex; }
            set { _CurrentPageIndex = value; }
        }
        #endregion

        private void GetPageCount()
        {
            if (this.RecordCount > 0)
            {
                this.PageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(this.RecordCount) / Convert.ToDouble(this.PageSize)));
            }
            else
            {
                this.PageCount = 0;
            }
        }

        void PageChanged(int newPageIndex)
        {
            if (OnPageChanged != null)
            {
                this.OnPageChanged(new PageChangedEventArgs(newPageIndex));
            }
        }
        /// <summary>
        /// 翻页控件数据绑定的方法
        /// </summary>
        public void DataBind()
        {
            this.Enabled = false;
            if (this.CurrentPageIndex > this.PageCount)
            {
                this.CurrentPageIndex = this.PageCount;
            }
            if (this.CurrentPageIndex < 1)
            {
                this.CurrentPageIndex = 1;
            }
            txtPageCount.Text = this.PageCount.ToString();

            if (this.CurrentPageIndex == 1)
            {
                this.toolbtnPrevious.Enabled = false;
                this.toolbtnFirst.Enabled = false;
            }
            else
            {
                toolbtnPrevious.Enabled = true;
                toolbtnFirst.Enabled = true;
            }

            if (this.CurrentPageIndex == this.PageCount)
            {
                this.toolbtnLast.Enabled = false;
                this.toolbtnNext.Enabled = false;
            }
            else
            {
                toolbtnLast.Enabled = true;
                toolbtnNext.Enabled = true;
            }

            if (this.RecordCount == 0)
            {
                toolbtnNext.Enabled = false;
                toolbtnLast.Enabled = false;
                toolbtnFirst.Enabled = false;
                toolbtnPrevious.Enabled = false;
            }

            this.toolpageIndex.SelectedItem = this.CurrentPageIndex;
            this.Enabled = true;
        }
    }


    /// <summary>
    /// 申明委托
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    public delegate void PageChangedEventHandler(PageChangedEventArgs e);


    /// <summary>
    /// 自定义事件数据基类
    /// </summary>
    public class PageChangedEventArgs : EventArgs
    {
        private readonly int _newpageindex;
        public PageChangedEventArgs(int newPageIndex)
        {
            _newpageindex = newPageIndex;
        }

        public int NewPageIndex
        {
            get { return _newpageindex; }
        }
    }
}
