using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IThemeSky.Model;
using IThemeSky.DataAccess;

namespace IThemeSky.UI.Models
{
    public class CommentListModel : ModelBase
    {
        public CommentListModel(int themeId, int pageIndex, int pageSize)
        {
            int recordCount = 0;
            IThemeCommentRepository commentRespository = ThemeRepositoryFactory.Default.GetThemeCommentRepository();

            this.ThemeId = themeId;
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;            
            this.Comments = commentRespository.GetComments(themeId, pageIndex, pageSize, ref recordCount);
            this.RecordCount = recordCount;
        }

        public List<ThemeComment> Comments { get; private set; }

        public int ThemeId { get; private set; }

        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }

        public int RecordCount { get; private set; }
    }
}
