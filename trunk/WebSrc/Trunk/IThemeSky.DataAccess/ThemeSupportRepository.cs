using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IThemeSky.Library.Data;
using IThemeSky.Model;
using IThemeSky.Library.Extensions;
using System.Data.SqlClient;
using System.Data;

namespace IThemeSky.DataAccess
{
    public class ThemeSupportRepository : IThemeSupportRepository
    {

        private IConnectionProvider _connectionProvider;
        /// <summary>
        /// 初始化评论数据访问类
        /// </summary>
        /// <param name="connectionProvider"></param>
        public ThemeSupportRepository(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        #region IThemeSupportRepository Members

        public bool AddSupport(ThemeSupport support)
        {
            string cmdText = @"
			insert into ThemeSupport
				(Name,Mail,SupportType,ThemeId,Subject,Description,AddTime,UserIp)
			values
				(@Name,@Mail,@SupportType,@ThemeId,@Subject,@Description,@AddTime,@UserIp)
			";
            SqlParameter[] parameters = new SqlParameter[]
			{
				SqlParameterHelper.BuildInputParameter("@Name",SqlDbType.NVarChar, 300, support.Name),
				SqlParameterHelper.BuildInputParameter("@Mail",SqlDbType.NVarChar, 300, support.Mail),
				SqlParameterHelper.BuildInputParameter("@SupportType",SqlDbType.SmallInt, 2, support.SupportType.ToInt32()),
				SqlParameterHelper.BuildInputParameter("@ThemeId",SqlDbType.Int, 4, support.ThemeId),
                SqlParameterHelper.BuildInputParameter("@Subject",SqlDbType.NVarChar, 1000, support.Subject),
                SqlParameterHelper.BuildInputParameter("@Description",SqlDbType.NVarChar, 2000, support.Description),
				SqlParameterHelper.BuildInputParameter("@AddTime",SqlDbType.DateTime, 8, support.AddTime),
				SqlParameterHelper.BuildInputParameter("@UserIp",SqlDbType.VarChar, 50, support.UserIp),
			};
            return SqlHelper.ExecuteNonQuery(_connectionProvider.GetWriteConnectionString(), CommandType.Text, cmdText, parameters) > 0;
        }
        /// <summary>
        /// 回复反馈问题
        /// </summary>
        /// <param name="supportId"></param>
        /// <param name="replyContent"></param>
        /// <returns></returns>
        public bool ReplySupport(int supportId, string replyContent)
        {
            string cmdText = "UPDATE ThemeSupport SET IsReply=1,ReplyContent=@ReplyContent,ReplyTime=getdate() where SupportId=@SupportId ";
            SqlParameter[] parameters = new SqlParameter[]
			{
				SqlParameterHelper.BuildInputParameter("@SupportId",SqlDbType.Int, 4, supportId),
                SqlParameterHelper.BuildInputParameter("@ReplyContent",SqlDbType.Text, 4, replyContent),
			};
            return SqlHelper.ExecuteNonQuery(_connectionProvider.GetWriteConnectionString(), CommandType.Text, cmdText, parameters) > 0;
        }
        public bool DeleteSupport(int supportId)
        {
            string cmdText = "delete from ThemeSupport where SupportId=@SupportId ";
            SqlParameter[] parameters = new SqlParameter[]
			{
				SqlParameterHelper.BuildInputParameter("@SupportId",SqlDbType.Int, 4, supportId)
			};
            return SqlHelper.ExecuteNonQuery(_connectionProvider.GetWriteConnectionString(), CommandType.Text, cmdText, parameters) > 0;
        }

        public List<ThemeSupport> GetSupports(SupportTypeOption supportType, int pageIndex, int pageSize, ref int recordCount)
        {
            SqlParameter[] parameters = new SqlParameter[] 
			{
				SqlParameterHelper.BuildParameter("@RecordNum",SqlDbType.Int,4, ParameterDirection.InputOutput, recordCount),
				SqlParameterHelper.BuildInputParameter("@SelectList",SqlDbType.VarChar,2000,"*"),
				SqlParameterHelper.BuildInputParameter("@TableSource",SqlDbType.VarChar,100,"ThemeSupport"),
				SqlParameterHelper.BuildInputParameter("@SearchCondition",SqlDbType.VarChar,2000, supportType == SupportTypeOption.All ? "" : "SupportType=" + supportType.ToInt32()),
				SqlParameterHelper.BuildInputParameter("@OrderExpression",SqlDbType.VarChar,1000, "SupportId DESC"),
				SqlParameterHelper.BuildInputParameter("@PageSize",SqlDbType.Int,4,pageSize),
				SqlParameterHelper.BuildInputParameter("@PageIndex",SqlDbType.Int,4,pageIndex)
			};
            List<ThemeSupport> list = new List<ThemeSupport>();
            using (IDataReader dataReader = SqlHelper.ExecuteReader(_connectionProvider.GetReadConnectionString(), CommandType.StoredProcedure, "PR_GetDataByPageIndex", parameters))
            {
                while (dataReader.Read())
                {
                    list.Add(BindThemeSupport(dataReader));
                }
            }
            recordCount = Convert.ToInt32(parameters[0].Value);
            return list;
        }

        /// <summary>
        /// 添加iSpirit用户信息
        /// </summary>
        /// <param name="spiritUser"></param>
        /// <returns></returns>
        public bool AddISpiritUserInfo(ISpiritUserInfo spiritUser)
        {
            string cmdText = @"
			insert into ISpiritUsers
				(PhoneVersion,SoftVersion,ITunesVersion,DeviceId,DeviceType,UserAgent,AddTime,UserIp)
			values
				(@PhoneVersion,@SoftVersion,@ITunesVersion,@DeviceId,@DeviceType,@UserAgent,getdate(),@UserIp)
			";
            SqlParameter[] parameters = new SqlParameter[]
			{
				SqlParameterHelper.BuildInputParameter("@PhoneVersion",SqlDbType.NVarChar, 30, spiritUser.PhoneVersion),
				SqlParameterHelper.BuildInputParameter("@SoftVersion",SqlDbType.NVarChar, 30, spiritUser.SoftVersion),
                SqlParameterHelper.BuildInputParameter("@ITunesVersion",SqlDbType.NVarChar, 30, spiritUser.ITunesVersion),
                SqlParameterHelper.BuildInputParameter("@DeviceId",SqlDbType.NVarChar, 300, spiritUser.DeviceId),
                SqlParameterHelper.BuildInputParameter("@DeviceType",SqlDbType.NVarChar, 30, spiritUser.DeviceType),
                SqlParameterHelper.BuildInputParameter("@UserAgent",SqlDbType.NVarChar, 500, spiritUser.UserAgent),
                SqlParameterHelper.BuildInputParameter("@UserIP",SqlDbType.NVarChar, 50, spiritUser.UserIP),
			};
            return SqlHelper.ExecuteNonQuery(_connectionProvider.GetWriteConnectionString(), CommandType.Text, cmdText, parameters) > 0;
        }
        #endregion

        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public ThemeSupport BindThemeSupport(IDataReader dataReader)
        {
            return new ThemeSupport()
            {
                SupportId = Convert.ToInt32(dataReader["SupportId"]),
                ThemeId = Convert.ToInt32(dataReader["ThemeId"]),
                UserIp = dataReader["UserIp"].ToString(),
                AddTime = Convert.ToDateTime(dataReader["AddTime"]),
                Description = dataReader["Description"].ToString(),
                Mail = dataReader["Mail"].ToString(),
                Name = dataReader["Name"].ToString(),
                Subject = dataReader["Subject"].ToString(),
                SupportType = dataReader["SupportType"].ToString().ToEnum<SupportTypeOption>(SupportTypeOption.Theme),
                IsReply = dataReader["IsReply"] != DBNull.Value ? Convert.ToInt16(dataReader["IsReply"]) > 0 : false,
                ReplyContent = dataReader["ReplyContent"] != DBNull.Value ? dataReader["ReplyContent"].ToString() : "",
                ReplyTime = dataReader["ReplyTime"] != DBNull.Value ? Convert.ToDateTime(dataReader["ReplyTime"]) : DateTime.Now,
            };
        }
    }
}
