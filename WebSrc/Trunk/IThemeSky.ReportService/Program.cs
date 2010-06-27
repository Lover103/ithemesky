using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IThemeSky.DataAccess;
using IThemeSky.Library.Data;

namespace IThemeSky.ReportService
{
    class Program
    {
        /// <summary>
        /// 计算当天的所有用户访问数(注意非唯一用户)
        /// </summary>
        private const string CMD_ALL_USER_COUNT = "SELECT COUNT(0) FROM [ISpiritUsers] WHERE AddTime > '{0}' AND AddTime < '{1}'";
        /// <summary>
        /// 计算当天的老用户访问数(注意非唯一用户)
        /// </summary>
        private const string CMD_OLD_USER_COUNT = @"SELECT COUNT(0)
          FROM [ISpiritUsers] WHERE AddTime > '{0}' AND AddTime < '{1}' AND UserIp in (SELECT UserIp From [ISpiritUsers] WHERE AddTime < '{0}')";
        private const string CMD_INSERT_REPORT_DATA = "INSERT INTO VisitorsDayReport (UsersNumber,OldUsersNumber,ReportDate) VALUES ({0}, {1}, {2})";
        static void Main(string[] args)
        {
            string connectionString = ThemeRepositoryFactory.Default.ConnectionProvider.GetWriteConnectionString();
            //获取当前报表统计表中已经统计到的日期
            DateTime lastDate = new DateTime(2010, 5, 22);
            string cmdText = "SELECT MAX(ReportDate) FROM VisitorsDayReport";
            object result = SqlHelper.ExecuteScalar(connectionString, System.Data.CommandType.Text, cmdText);
            if (result != DBNull.Value && Convert.ToInt32(result) > 0)
            {
                lastDate = DateTime.ParseExact(result.ToString(), "yyyyMMdd", null).AddDays(1);
            }
            //开始统计从上个统计日到今天的所有数据
            while (lastDate < DateTime.Now.Date)
            { 
                cmdText = string.Format(CMD_ALL_USER_COUNT, lastDate.ToString("yyyyMMdd"), lastDate.AddDays(1).ToString("yyyyMMdd"));
                int allUserCount = Convert.ToInt32(SqlHelper.ExecuteScalar(connectionString, System.Data.CommandType.Text, cmdText));
                cmdText = string.Format(CMD_OLD_USER_COUNT, lastDate.ToString("yyyyMMdd"), lastDate.AddDays(1).ToString("yyyyMMdd"));
                int oldUserCount = Convert.ToInt32(SqlHelper.ExecuteScalar(connectionString, System.Data.CommandType.Text, cmdText));
                cmdText = string.Format(CMD_INSERT_REPORT_DATA
                    , allUserCount
                    , oldUserCount
                    , lastDate.ToString("yyyyMMdd")
                    );
                SqlHelper.ExecuteNonQuery(connectionString, System.Data.CommandType.Text, cmdText);
                Console.WriteLine(string.Format("计算完成{0}日的统计，所有用户：{1}，老用户：{2}", lastDate, allUserCount, oldUserCount));
                lastDate = lastDate.AddDays(1);
            }
            Console.WriteLine("end");
            //Console.Read();
        }
    }
}
