using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IThemeSky.Model;

namespace IThemeSky.DataAccess
{
    /// <summary>
    /// 主题反馈数据访问类
    /// </summary>
    public interface IThemeSupportRepository
    {
        /// <summary>
        /// 添加反馈问题
        /// </summary>
        /// <param name="support">反馈实体</param>
        /// <returns></returns>
        bool AddSupport(ThemeSupport support);
        /// <summary>
        /// 回复反馈问题
        /// </summary>
        /// <param name="supportId"></param>
        /// <param name="replyContent"></param>
        /// <returns></returns>
        bool ReplySupport(int supportId, string replyContent);
        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="supportId">反馈问题Id</param>
        /// <returns></returns>
        bool DeleteSupport(int supportId);
        /// <summary>
        /// 获取主题评论列表
        /// </summary>
        /// <param name="supportType">反馈类型</param>
        /// <param name="pageIndex">显示的页码，从1开始计数</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="recordCount">总记录数(ref)</param>
        /// <returns></returns>
        List<ThemeSupport> GetSupports(SupportTypeOption supportType, int pageIndex, int pageSize, ref int recordCount);
        /// <summary>
        /// 添加iSpirit用户信息
        /// </summary>
        /// <param name="spiritUser"></param>
        /// <returns></returns>
        bool AddISpiritUserInfo(ISpiritUserInfo spiritUser);
    }
}
