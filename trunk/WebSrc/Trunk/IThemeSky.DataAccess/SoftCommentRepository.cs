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
    public class SoftCommentRepository : ISoftCommentRepository
    {
        private IConnectionProvider _connectionProvider;
        /// <summary>
        /// 初始化评论数据访问类
        /// </summary>
        /// <param name="connectionProvider"></param>
        public SoftCommentRepository(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        #region ISoftCommentRepository members

        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="comment">评论实体</param>
        /// <returns></returns>
        public bool AddComment(SoftComment comment)
        {
            string cmdText = @"
			insert into SoftComment
				(SoftIdentify,SoftTitle,SoftDescription,RateType,Title,Content,UserName,UserMail,UserId,UserIp,AddTime,UpdateTime,DiggNumber,BuryNumber)
			values
				(@SoftIdentify,@SoftTitle,@SoftDescription,@RateType,@Title,@Content,@UserName,@UserMail,@UserId,@UserIp,@AddTime,@UpdateTime,@DiggNumber,@BuryNumber)
			";
            SqlParameter[] parameters = new SqlParameter[]
			{
				SqlParameterHelper.BuildInputParameter("@SoftIdentify",SqlDbType.NVarChar, 300, comment.SoftIdentify),
                SqlParameterHelper.BuildInputParameter("@SoftTitle",SqlDbType.NVarChar, 300, comment.SoftTitle),
                SqlParameterHelper.BuildInputParameter("@SoftDescription",SqlDbType.NVarChar, 300, comment.SoftDescription),
				SqlParameterHelper.BuildInputParameter("@RateType",SqlDbType.Int, 4, comment.RateType),
				SqlParameterHelper.BuildInputParameter("@Title",SqlDbType.NVarChar, 300, comment.Title),
				SqlParameterHelper.BuildInputParameter("@Content",SqlDbType.NText, 0, comment.Content),
                SqlParameterHelper.BuildInputParameter("@UserName",SqlDbType.NVarChar, 100, comment.UserName),
                SqlParameterHelper.BuildInputParameter("@UserMail",SqlDbType.NVarChar, 300, comment.UserMail),
				SqlParameterHelper.BuildInputParameter("@UserId",SqlDbType.Int, 4, comment.UserId),
				SqlParameterHelper.BuildInputParameter("@UserIp",SqlDbType.NVarChar, 40, comment.UserIp),
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
            string cmdText = "delete from SoftComment where CommentId=@CommentId ";
            SqlParameter[] parameters = new SqlParameter[]
			{
				SqlParameterHelper.BuildInputParameter("@CommentId",SqlDbType.BigInt, 8, commentId)
			};
            return SqlHelper.ExecuteNonQuery(_connectionProvider.GetWriteConnectionString(), CommandType.Text, cmdText, parameters) > 0;
        }

        /// <summary>
        /// 获取软件评论列表
        /// </summary>
        /// <param name="softIdentify">软件标识符</param>
        /// <param name="pageIndex">显示的页码，从1开始计数</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="recordCount">总记录数(ref)</param>
        /// <returns></returns>
        public List<SoftComment> GetComments(string softIdentify, int pageIndex, int pageSize, ref int recordCount)
        {
            SqlParameter[] parameters = new SqlParameter[] 
			{
				SqlParameterHelper.BuildParameter("@RecordNum",SqlDbType.Int,4, ParameterDirection.InputOutput, recordCount),
				SqlParameterHelper.BuildInputParameter("@SelectList",SqlDbType.VarChar,2000,"*"),
				SqlParameterHelper.BuildInputParameter("@TableSource",SqlDbType.VarChar,100,"SoftComment"),
				SqlParameterHelper.BuildInputParameter("@SearchCondition",SqlDbType.VarChar,2000, "SoftIdentify='" + softIdentify.Replace("'", "''") + "'"),
				SqlParameterHelper.BuildInputParameter("@OrderExpression",SqlDbType.VarChar,1000, "AddTime DESC"),
				SqlParameterHelper.BuildInputParameter("@PageSize",SqlDbType.Int,4,pageSize),
				SqlParameterHelper.BuildInputParameter("@PageIndex",SqlDbType.Int,4,pageIndex)
			};
            List<SoftComment> list = new List<SoftComment>();
            using (IDataReader dataReader = SqlHelper.ExecuteReader(_connectionProvider.GetReadConnectionString(), CommandType.StoredProcedure, "PR_GetDataByPageIndex", parameters))
            {
                while (dataReader.Read())
                {
                    list.Add(BindSoftComment(dataReader));
                }
            }
            recordCount = Convert.ToInt32(parameters[0].Value);
            return list;
        }        #endregion

        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public SoftComment BindSoftComment(IDataReader dataReader)
        {
            return new SoftComment()
            {
                CommentId = Convert.ToInt64(dataReader["CommentId"]),
                SoftIdentify = dataReader["SoftIdentify"].ToString(),
                SoftTitle = dataReader["SoftTitle"].ToString(),
                SoftDescription = dataReader["SoftDescription"].ToString(),
                RateType = Convert.ToInt32(dataReader["RateType"]),
                Title = dataReader["Title"].ToString(),
                Content = dataReader["Content"].ToString(),
                UserName = dataReader["UserName"].ToString(),
                UserMail = dataReader["UserMail"].ToString(),
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
