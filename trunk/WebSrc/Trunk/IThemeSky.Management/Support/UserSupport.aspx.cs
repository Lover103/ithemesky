using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IThemeSky.Management.Base;
using IThemeSky.DataAccess;
using IThemeSky.Model;

namespace IThemeSky.Management.Support
{
    public partial class UserSupport : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            rptThemes.ItemDataBound += new RepeaterItemEventHandler(rptThemes_ItemDataBound);
            if (!IsPostBack)
            {
                BindSupportList();
            }
        }

        void rptThemes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Button control = e.Item.FindControl("btnReply") as Button;
            if ((e.Item.DataItem as ThemeSupport).IsReply)
            {
                control.Text = "已回复";
                control.Enabled = false;
            }
        }

        protected void pager_PageChanged(object sender, EventArgs e)
        {
            BindSupportList();
        }
        protected void btnReply_OnClick(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as Control).Parent as RepeaterItem;
            int supportId = Convert.ToInt32((item.FindControl("hidSupportId") as HiddenField).Value);
            IThemeSupportRepository repository = ThemeRepositoryFactory.Default.GetThemeSupportRepository();
            repository.ReplySupport(supportId, "");
            BindSupportList();
        }
        private void BindSupportList()
        {
            IThemeSupportRepository repository = ThemeRepositoryFactory.Default.GetThemeSupportRepository();
            int recordCount = 0;
            rptThemes.DataSource = repository.GetSupports(IThemeSky.Model.SupportTypeOption.All, pager.CurrentPageIndex, pager.PageSize, ref recordCount);
            rptThemes.DataBind();
            pager.RecordCount = recordCount;
        }
    }
}
