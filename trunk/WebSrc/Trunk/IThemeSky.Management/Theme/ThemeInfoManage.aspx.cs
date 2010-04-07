using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IThemeSky.Library.Extensions;
using IThemeSky.Model;

namespace IThemeSky.Management.Theme
{
    public partial class ThemeInfoManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IThemeSky.Model.Theme theme = new IThemeSky.Model.Theme()
            {
                ThemeId = hidThemeId.Value.ToInt32(),
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

            };
        }
    }
}
