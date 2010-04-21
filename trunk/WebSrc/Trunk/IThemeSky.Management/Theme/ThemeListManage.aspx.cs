using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IThemeSky.Library.Extensions;
using IThemeSky.DataAccess;
using IThemeSky.Model;
using IThemeSky.Management.Base;

namespace IThemeSky.Management.Theme
{
    public partial class ThemeListManage : PageBase
    {
        private IThemeViewRepository _repositoryView = ThemeRepositoryFactory.Default.GetThemeViewRepository();
        private IThemeManageRepository _repositoryManage = ThemeRepositoryFactory.Default.GetThemeManageRepository();
        private List<ThemeCategory> _categories;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindThemeList();
            }
        }

        protected void pager_PageChanged(object sender, EventArgs e)
        {
            BindThemeList();
        }

        protected void BindThemeList()
        {
            ltlMessage.Text = "";
            _categories = _repositoryView.GetThemeCategories();
            int recordCount = 0;
            rptThemes.DataSource = _repositoryView.GetFullThemesByFilter(GetFilter(), ThemeSortOption.Default, pager.CurrentPageIndex, pager.PageSize, ref recordCount);
            rptThemes.DataBind();
            pager.RecordCount = recordCount;
        }

        protected void rptThemes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            FullThemeView theme = e.Item.DataItem as FullThemeView;
            HiddenField hidThemeId = e.Item.FindControl("hidThemeId") as HiddenField;
            hidThemeId.Value = theme.ThemeId.ToString();

            DropDownList ddlCategoryId = e.Item.FindControl("ddlCategoryId") as DropDownList;
            ddlCategoryId.DataSource = _categories;
            ddlCategoryId.DataBind();
            ddlCategoryId.SelectedValue = theme.CategoryId.ToString();

            DropDownList ddlCommendIndex = e.Item.FindControl("ddlCommendIndex") as DropDownList;
            ddlCommendIndex.SelectedValue = theme.CommendIndex.ToString();

            DropDownList ddlCheckState = e.Item.FindControl("ddlCheckState") as DropDownList;
            ddlCheckState.SelectedValue = theme.CheckState.ToInt32().ToString();

            DropDownList ddlDisplayState = e.Item.FindControl("ddlDisplayState") as DropDownList;
            ddlDisplayState.SelectedValue = theme.DisplayState.ToInt32().ToString();
        }

        /// <summary>
        /// 主题分类修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void propertyList_SelectedIndexChanged(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as Control).Parent as RepeaterItem;
            int themeId = Convert.ToInt32((item.FindControl("hidThemeId") as HiddenField).Value);
            IThemeSky.Model.Theme theme = _repositoryManage.GetTheme(themeId);

            string message = "";

            if (sender is DropDownList)
            {
                DropDownList dropDownList = sender as DropDownList;
                if (dropDownList.ID.Equals("ddlCategoryId"))
                {
                    theme.CategoryId = Convert.ToInt32(dropDownList.SelectedValue);
                    message = string.Format("[{0}]分类修改成功", theme.Title);
                }
                if (dropDownList.ID.Equals("ddlCommendIndex"))
                {
                    theme.CommendIndex = Convert.ToInt32(dropDownList.SelectedValue);
                    message = string.Format("[{0}]推荐指数修改成功", theme.Title);
                }
                if (dropDownList.ID.Equals("ddlCheckState"))
                {
                    theme.CheckState = Convert.ToInt32(dropDownList.SelectedValue).ToEnum<CheckStateOption>(CheckStateOption.Waitting);
                    message = string.Format("[{0}]审核状态修改成功", theme.Title);
                }
                if (dropDownList.ID.Equals("ddlDisplayState"))
                {
                    theme.DisplayState = Convert.ToInt32(dropDownList.SelectedValue).ToEnum<DisplayStateOption>(DisplayStateOption.Hidden);
                    message = string.Format("[{0}]显示状态修改成功", theme.Title);
                }
                
            }
            else if (sender is TextBox)
            {
                TextBox txtBox = sender as TextBox;
                if (txtBox.ID.Equals("txtTitle"))
                {
                    theme.Title = txtBox.Text;
                }
                if (txtBox.ID.Equals("txtTags"))
                {
                    string[] tags = txtBox.Text.Split(',');
                    _repositoryManage.DeleteTagMaps(themeId);
                    foreach (string tag in tags)
                    {
                        _repositoryManage.MappingThemeTag(themeId, tag);
                    }
                }
            }
            theme.UpdateTime = DateTime.Now;
            theme.CheckerId = UserContext.UserId;
            _repositoryManage.UpdateTheme(theme);
            ltlMessage.Text = message;
        }

        protected void propertyList_OnClick(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as Control).Parent as RepeaterItem;
            int themeId = Convert.ToInt32((item.FindControl("hidThemeId") as HiddenField).Value);
            IThemeSky.Model.Theme theme = _repositoryManage.GetTheme(themeId);
            TextBox txtTitle = item.FindControl("txtTitle") as TextBox;
            TextBox txtTags = item.FindControl("txtTags") as TextBox;
            theme.Title = txtTitle.Text;
            theme.UpdateTime = DateTime.Now;
            if ((sender as Control).ID.Equals("btnSaveAndCheck"))
            {
                theme.CheckState = CheckStateOption.CheckSuccess;
            }
            theme.CheckerId = UserContext.UserId;
            _repositoryManage.UpdateTheme(theme);
            string[] tags = txtTags.Text.Split(',');
            _repositoryManage.DeleteTagMaps(themeId);
            foreach (string tag in tags)
            {
                _repositoryManage.MappingThemeTag(themeId, tag);
            }
            ltlMessage.Text = "修改主题基本信息成功";
        }
        protected void filter_Changed(object sender, EventArgs e)
        {
            BindThemeList();
        }
        private ThemesFilter GetFilter()
        {
            ThemesFilter filter = _repositoryView.Filter;
            filter.CheckState = ddlCheckStateFilter.SelectedValue.ToEnum<CheckStateOption>(CheckStateOption.All);
            filter.DisplayState = ddlDisplayStateFilter.SelectedValue.ToEnum<DisplayStateOption>(DisplayStateOption.All);
            if (txtTitleFilter.Text.Length > 0)
            {
                filter.SearchKeyword = txtTitleFilter.Text;
            }
            return filter;
        }

        protected string GetThemeTags(object themeId)
        {
            List<string> tags = _repositoryView.GetTagsByThemeId(themeId.ToString().ToInt32());
            if (tags.Count > 1)
            {
                return tags.Aggregate((s1, s2) => s1 + "," + s2);
            }
            else if (tags.Count > 0)
            {
                return tags[0];
            }
            return "";
        }
    }
}
