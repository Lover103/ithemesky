using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IThemeSky.Model;
using IThemeSky.DataAccess;

namespace IThemeSky.UI.Models
{
    public class SoftCommentListModel : ModelBase
    {
        public SoftCommentListModel(string softIdentify, int pageIndex, int pageSize)
        {
            int recordCount = 0;
            ISoftCommentRepository commentRespository = ThemeRepositoryFactory.Default.GetSoftCommentRepository();

            this.SoftIdentify = softIdentify;
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;            
            this.Comments = commentRespository.GetComments(SoftIdentify, pageIndex, pageSize, ref recordCount);
            this.RecordCount = recordCount;
        }

        public List<SoftComment> Comments { get; private set; }

        public string SoftIdentify { get; private set; }

        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }

        public int RecordCount { get; private set; }
    }
}
