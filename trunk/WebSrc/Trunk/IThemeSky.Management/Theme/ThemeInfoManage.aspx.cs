using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IThemeSky.Library.Extensions;
using IThemeSky.Model;
using IThemeSky.DataAccess;
using IThemeSky.Management.Base;

namespace IThemeSky.Management.Theme
{
    public partial class ThemeInfoManage : PageBase
    {
        private IThemeViewRepository _repositoryView = ThemeRepositoryFactory.Default.GetThemeViewRepository();
        private IThemeManageRepository _repositoryManage = ThemeRepositoryFactory.Default.GetThemeManageRepository();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlCategoryId.DataSource = _repositoryView.GetThemeCategories();
                ddlCategoryId.DataBind();
                if (Request.QueryString["themeId"] != null && Request.QueryString["themeId"].ToInt32() > 0)
                {
                    hidThemeId.Value = Request.QueryString["themeId"];
                    IThemeSky.Model.Theme theme = _repositoryView.GetTheme(Request.QueryString["themeId"].ToInt32());
                    txtThumbnailName.Text = theme.ThumbnailName;
                    txtTitle.Text = theme.Title;
                    txtAuthorMail.Text = theme.AuthorMail;
                    txtAuthorName.Text = theme.AuthorName;
                    ddlCategoryId.SelectedValue = theme.CategoryId.ToString();
                    ddlCheckState.SelectedValue = theme.CheckState.ToInt32().ToString();
                    ddlCommendIndex.SelectedValue = theme.CommendIndex.ToString();
                    txtDescription.Text = theme.Description;
                    txtDownloads.Text = theme.Downloads.ToString();
                    chkDisplayState.Checked = theme.DisplayState == DisplayStateOption.Display;
                    txtDownloadUrl.Text = theme.DownloadUrl;
                    txtLastMonthDownloads.Text = theme.LastMonthDownloads.ToString();
                    txtLastWeekDownloads.Text = theme.LastWeekDownloads.ToString();
                    txtRateNumbers.Text = theme.RateNumbers.ToString();
                    txtRateScore.Text = theme.RateScore.ToString();
                    txtFileSize.Text = theme.FileSize.ToString();
                    txtViews.Text = theme.Views.ToString();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (hidThemeId.Value.ToInt32() > 0)
            {
                IThemeSky.Model.Theme theme = _repositoryView.GetTheme(hidThemeId.Value.ToInt32());
                theme.ThemeId = hidThemeId.Value.ToInt32();
                theme.ThumbnailName = txtThumbnailName.Text;
                theme.Title = txtTitle.Text;
                theme.AuthorId = 0;
                theme.AuthorMail = txtAuthorMail.Text;
                theme.AuthorName = txtAuthorName.Text;
                theme.CategoryId = ddlCategoryId.SelectedValue.ToInt32();
                theme.CheckState = ddlCheckState.SelectedValue.ToEnum<CheckStateOption>(CheckStateOption.Waitting);
                theme.CommendIndex = ddlCommendIndex.SelectedValue.ToInt32();
                theme.Comments = 0;
                theme.Description = txtDescription.Text;
                theme.Downloads = txtDownloads.Text.ToInt32(0);
                theme.DisplayState = chkDisplayState.Checked ? DisplayStateOption.Display : DisplayStateOption.Hidden;
                theme.DownloadUrl = txtDownloadUrl.Text;
                theme.LastMonthDownloads = Convert.ToInt32(txtLastMonthDownloads.Text);
                theme.LastWeekDownloads = Convert.ToInt32(txtLastWeekDownloads.Text);
                theme.RateNumbers = Convert.ToInt32(txtRateNumbers.Text);
                theme.RateScore = Convert.ToInt32(txtRateScore.Text);
                theme.FileSize = txtFileSize.Text.ToInt64();
                theme.UpdateTime = DateTime.Now;
                theme.Views = Convert.ToInt32(txtViews.Text);
                _repositoryManage.UpdateTheme(theme);
                ltlMessage.Text = "修改主题成功";
            }
            else
            {
                IThemeSky.Model.Theme theme = new IThemeSky.Model.Theme()
                {
                    ThumbnailName = txtThumbnailName.Text,
                    Title = txtTitle.Text,
                    AddTime = DateTime.Now,
                    AuthorId = 0,
                    AuthorMail = txtAuthorMail.Text,
                    AuthorName = txtAuthorName.Text,
                    CategoryId = ddlCategoryId.SelectedValue.ToInt32(),
                    CheckState = ddlCheckState.SelectedValue.ToEnum<CheckStateOption>(CheckStateOption.Waitting),
                    CommendIndex = ddlCommendIndex.SelectedValue.ToInt32(),
                    Comments = 0,
                    Description = txtDescription.Text,
                    Downloads = txtDownloads.Text.ToInt32(0),
                    DisplayState = chkDisplayState.Checked ? DisplayStateOption.Display : DisplayStateOption.Hidden,
                    DownloadUrl = txtDownloadUrl.Text,
                    LastMonthDownloads = Convert.ToInt32(txtLastMonthDownloads.Text),
                    LastWeekDownloads = Convert.ToInt32(txtLastWeekDownloads.Text),
                    RateNumbers = Convert.ToInt32(txtRateNumbers.Text),
                    RateScore = Convert.ToInt32(txtRateScore.Text),
                    FileSize = txtFileSize.Text.ToInt64(),
                    UpdateTime = DateTime.Now,
                    Views = Convert.ToInt32(txtViews.Text),
                    Source = SourceOption.IThemeSky,
                };
                _repositoryManage.AddTheme(theme);
                ltlMessage.Text = "添加主题成功";
            }
        }
    }
}
