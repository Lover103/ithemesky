using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IThemeSky.Library.Data;
using System.Data;
using System.Data.SqlClient;
using IThemeSky.Model;

namespace IThemeSky.DataAccess
{
    /// <summary>
    /// 评论数据访问类
    /// </summary>
    public class ThemeCommentRepository : IThemeCommentRepository
    {
        private IConnectionProvider _connectionProvider;
        /// <summary>
        /// 初始化评论数据访问类
        /// </summary>
        /// <param name="connectionProvider"></param>
        public ThemeCommentRepository(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        #region IThemeCommentRepository members

        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="comment">评论实体</param>
        /// <returns></returns>
        public bool AddComment(ThemeComment comment)
        {
            string cmdText = @"
			insert into ThemeComment
				(ThemeId,RateType,Title,Content,UserId,UserIp,AddTime,UpdateTime,DiggNumber,BuryNumber)
			values
				(@ThemeId,@RateType,@Title,@Content,@UserId,@UserIp,@AddTime,@UpdateTime,@DiggNumber,@BuryNumber)
			";
            SqlParameter[] parameters = new SqlParameter[]
			{
				SqlParameterHelper.BuildInputParameter("@ThemeId",SqlDbType.Int, 4, comment.ThemeId),
				SqlParameterHelper.BuildInputParameter("@RateType",SqlDbType.Int, 4, comment.RateType),
				SqlParameterHelper.BuildInputParameter("@Title",SqlDbType.VarChar, 300, comment.Title),
				SqlParameterHelper.BuildInputParameter("@Content",SqlDbType.VarChar, 16, comment.Content),
				SqlParameterHelper.BuildInputParameter("@UserId",SqlDbType.Int, 4, comment.UserId),
				SqlParameterHelper.BuildInputParameter("@UserIp",SqlDbType.VarChar, 40, comment.UserIp),
				SqlParameterHelper.BuildInputParameter("@AddTime",SqlDbType.DateTime, 8, comment.AddTime),
				SqlParameterHelper.BuildInputParameter("@UpdateTime",SqlDbType.DateTime, 8, comment.UpdateTime),
				SqlParameterHelper.BuildInputParameter("@DiggNumber",SqlDbType.Int, 4, comment.DiggNumber),
				SqlParameterHelper.BuildInputParameter("@BuryNumber",SqlDbType.Int, 4, comment.BuryNumber)
			};
            return SqlHelper.ExecuteNonQuery(_connectionProvider.GetWriteConnectionString(), CommandType.Text, cmdText, parameters) > 0;
        }

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="commentId">评论Id</param>
        /// <returns></returns>
        public bool DeleteComment(int commentId)
        {
            string cmdText = "delete from ThemeComment where CommentId=@CommentId ";
            SqlParameter[] parameters = new SqlParameter[]
			{
				SqlParameterHelper.BuildInputParameter("@CommentId",SqlDbType.BigInt, 8, commentId)
			};
            return SqlHelper.ExecuteNonQuery(_connectionProvider.GetWriteConnectionString(), CommandType.Text, cmdText, parameters) > 0;
        }

        /// <summary>
        /// 获取主题评论列表
        /// </summary>
        /// <param name="themeId">主题id</param>
        /// <param name="pageIndex">显示的页码，从1开始计数</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="recordCount">总记录数(ref)</param>
        /// <returns></returns>
        public List<ThemeComment> GetComments(int themeId, int pageIndex, int pageSize, ref int recordCount)
        {
            SqlParameter[] parameters = new SqlParameter[] 
			{
				SqlParameterHelper.BuildParameter("@RecordNum",SqlDbType.Int,4, ParameterDirection.InputOutput, recordCount),
				SqlParameterHelper.BuildInputParameter("@SelectList",SqlDbType.VarChar,2000,"CommentId,ThemeId,RateType,Title,Content,UserId,UserIp,AddTime,UpdateTime,DiggNumber,BuryNumber"),
				SqlParameterHelper.BuildInputParameter("@TableSource",SqlDbType.VarChar,100,"ThemeComment"),
				SqlParameterHelper.BuildInputParameter("@SearchCondition",SqlDbType.VarChar,2000, ""),
				SqlParameterHelper.BuildInputParameter("@OrderExpression",SqlDbType.VarChar,1000, "AddTime DESC"),
				SqlParameterHelper.BuildInputParameter("@PageSize",SqlDbType.Int,4,pageSize),
				SqlParameterHelper.BuildInputParameter("@PageIndex",SqlDbType.Int,4,pageIndex)
			};
            List<ThemeComment> list = new List<ThemeComment>();
            using (IDataReader dataReader = SqlHelper.ExecuteReader(_connectionProvider.GetReadConnectionString(), CommandType.StoredProcedure, "PR_GetDataByPageIndex", parameters))
            {
                while (dataReader.Read())
                {
                    list.Add(BindThemeComment(dataReader));
                }
            }
            recordCount = Convert.ToInt32(parameters[0]);
            return list;
        }        #endregion

        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public ThemeComment BindThemeComment(IDataReader dataReader)
        {
            return new ThemeComment()
            {
                CommentId = Convert.ToInt64(dataReader["CommentId"]),
                ThemeId = Convert.ToInt32(dataReader["ThemeId"]),
                RateType = Convert.ToInt32(dataReader["RateType"]),
                Title = dataReader["Title"].ToString(),
                Content = dataReader["Content"].ToString(),
                UserId = Convert.ToInt32(dataReader["UserId"]),
                UserIp = dataReader["UserIp"].ToString(),
                AddTime = Convert.ToDateTime(dataReader["AddTime"]),
                UpdateTime = Convert.ToDateTime(dataReader["UpdateTime"]),
                DiggNumber = Convert.ToInt32(dataReader["DiggNumber"]),
                BuryNumber = Convert.ToInt32(dataReader["BuryNumber"]),
            };
        }
    }
}
