using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IThemeSky.Management.Base;
using IThemeSky.DataAccess;

namespace IThemeSky.Management.Support
{
    public partial class UserSupport : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSupportList();
            }
        }

        protected void pager_PageChanged(object sender, EventArgs e)
        {
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
