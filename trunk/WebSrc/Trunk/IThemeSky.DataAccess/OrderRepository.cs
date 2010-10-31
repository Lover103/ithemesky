using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IThemeSky.Library.Data;
using System.Data.SqlClient;
using System.Data;

namespace IThemeSky.DataAccess
{
    public class OrderRepository : IOrderRepository
    {
        private IConnectionProvider _connectionProvider;

        public OrderRepository(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        #region IOrderRepository Members

        public bool AddOrder(IThemeSky.Model.UserOrder order)
        {
            string cmdText = @"
            IF NOT EXISTS (SELECT 1 FROM UserOrder WHERE OrderNumber=@OrderNumber)
			    insert into UserOrder
				    (OrderNumber,UserMail,PayerMail,ThemeId,Price,Checksum,Status,Description,UserName)
			    values
				    (@OrderNumber,@UserMail,@PayerMail,@ThemeId,@Price,@Checksum,@Status,@Description,@UserName)
            ELSE
                update UserOrder set UserMail=@UserMail,UserName=@UserName WHERE OrderNumber=@OrderNumber
			";
            SqlParameter[] parameters = new SqlParameter[]
			{
                SqlParameterHelper.BuildInputParameter("@OrderNumber",SqlDbType.VarChar, 300, order.OrderNumber),
				SqlParameterHelper.BuildInputParameter("@UserMail",SqlDbType.VarChar, 300, order.UserMail),
                SqlParameterHelper.BuildInputParameter("@PayerMail",SqlDbType.VarChar, 300, order.PayerMail),
                SqlParameterHelper.BuildInputParameter("@ThemeId",SqlDbType.Int, 4, order.ThemeId),
                SqlParameterHelper.BuildInputParameter("@Price",SqlDbType.Decimal, 16, order.Price),
                SqlParameterHelper.BuildInputParameter("@Checksum",SqlDbType.VarChar, 32, order.Checksum),
				SqlParameterHelper.BuildInputParameter("@Status",SqlDbType.SmallInt, 2, order.Status),
				SqlParameterHelper.BuildInputParameter("@Description",SqlDbType.Text, 0, order.Description),
                SqlParameterHelper.BuildInputParameter("@UserName",SqlDbType.VarChar, 300, order.UserName),
			};
            return SqlHelper.ExecuteNonQuery(_connectionProvider.GetWriteConnectionString(), CommandType.Text, cmdText, parameters) > 0;
        }

        public bool UpdateOrderFromIPN(IThemeSky.Model.UserOrder order)
        {
            string cmdText = @"
            IF NOT EXISTS (SELECT 1 FROM UserOrder WHERE OrderNumber=@OrderNumber)
			    insert into UserOrder
				    (OrderNumber,UserMail,PayerMail,ThemeId,Price,Checksum,Status,Description,UserName)
			    values
				    (@OrderNumber,@UserMail,@PayerMail,@ThemeId,@Price,@Checksum,@Status,@Description,@UserName)
            ELSE
                update UserOrder set PayerMail=@PayerMail,Status=@Status, Description=@Description,UpdateTime=getdate() WHERE OrderNumber=@OrderNumber
			";
            SqlParameter[] parameters = new SqlParameter[]
			{
                SqlParameterHelper.BuildInputParameter("@OrderNumber",SqlDbType.VarChar, 300, order.OrderNumber),
				SqlParameterHelper.BuildInputParameter("@UserMail",SqlDbType.VarChar, 300, order.UserMail),
                SqlParameterHelper.BuildInputParameter("@PayerMail",SqlDbType.VarChar, 300, order.PayerMail),
                SqlParameterHelper.BuildInputParameter("@ThemeId",SqlDbType.Int, 4, order.ThemeId),
                SqlParameterHelper.BuildInputParameter("@Price",SqlDbType.Decimal, 16, order.Price),
                SqlParameterHelper.BuildInputParameter("@Checksum",SqlDbType.VarChar, 32, order.Checksum),
				SqlParameterHelper.BuildInputParameter("@Status",SqlDbType.SmallInt, 2, order.Status),
				SqlParameterHelper.BuildInputParameter("@Description",SqlDbType.Text, 0, order.Description),
                SqlParameterHelper.BuildInputParameter("@UserName",SqlDbType.VarChar, 300, order.UserName),
			};
            return SqlHelper.ExecuteNonQuery(_connectionProvider.GetWriteConnectionString(), CommandType.Text, cmdText, parameters) > 0;
        }

        public bool UpdateOrderStatus(string orderNumber, string payerMail, int status, string description)
        {
            string cmdText = "UPDATE UserOrder SET PayerMail=@PayerMail,Status=@Status,Description=@Description,UpdateTime=getdate() WHERE OrderNumber=@OrderNumber";
            SqlParameter[] parameters = new SqlParameter[]
			{
				SqlParameterHelper.BuildInputParameter("@OrderNumber",SqlDbType.VarChar, 300, orderNumber),
                SqlParameterHelper.BuildInputParameter("@PayerMail",SqlDbType.VarChar, 300, payerMail),
				SqlParameterHelper.BuildInputParameter("@Status",SqlDbType.SmallInt, 2, status),
				SqlParameterHelper.BuildInputParameter("@Description",SqlDbType.Text, 0, description),
			};
            return SqlHelper.ExecuteNonQuery(_connectionProvider.GetWriteConnectionString(), CommandType.Text, cmdText, parameters) > 0;
        }

        public IThemeSky.Model.UserOrder GetOrder(int themeId, string orderNumber)
        {
            string cmdText = "SELECT * FROM UserOrder WHERE ThemeId=@ThemeId AND OrderNumber=@OrderNumber";
            SqlParameter[] parameters = new SqlParameter[]
			{
                SqlParameterHelper.BuildInputParameter("@ThemeId",SqlDbType.Int, 4, themeId),
                SqlParameterHelper.BuildInputParameter("@OrderNumber",SqlDbType.VarChar, 300, orderNumber),
			};
            using (IDataReader reader = SqlHelper.ExecuteReader(_connectionProvider.GetReadConnectionString(), CommandType.Text, cmdText, parameters))
            {
                if (reader.Read())
                {
                    return new IThemeSky.Model.UserOrder() 
                    { 
                        OrderNumber = reader["OrderNumber"].ToString(),
                        AddTime = Convert.ToDateTime(reader["AddTime"]),
                        Checksum = reader["Checksum"].ToString(),
                        Description = reader["Description"].ToString(),
                        OrderId = Convert.ToInt32(reader["OrderId"]),
                        Price = Convert.ToDouble(reader["Price"]),
                        Status = Convert.ToInt32(reader["Status"]),
                        ThemeId = Convert.ToInt32(reader["ThemeId"]),
                        UserMail = reader["UserMail"].ToString(),
                        PayerMail = reader["PayerMail"].ToString(),
                        UpdateTime = Convert.ToDateTime(reader["UpdateTime"]),
                        UserName = reader["UserName"].ToString(),
                    };
                }
            }
            return null;
        }

        #endregion
    }
}
