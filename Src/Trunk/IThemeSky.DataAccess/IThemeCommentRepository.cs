using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IThemeSky.Model;

namespace IThemeSky.DataAccess
{
    /// <summary>
    /// 评论数据访问接口
    /// </summary>
    public interface IThemeCommentRepository
    {
        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="comment">评论实体</param>
        /// <returns></returns>
        bool AddComment(ThemeComment comment);
        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="commentId">评论Id</param>
        /// <returns></returns>
        bool DeleteComment(int commentId);
        /// <summary>
        /// 获取主题评论列表
        /// </summary>
        /// <param name="themeId">主题id</param>
        /// <param name="pageIndex">显示的页码，从1开始计数</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="recordCount">总记录数(ref)</param>
        /// <returns></returns>
        List<ThemeComment> GetComments(int themeId, int pageIndex, int pageSize, ref int recordCount);
    }
}
