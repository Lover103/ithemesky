using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IThemeSky.DataAccess;
using System.Data;
using IThemeSky.Library.Data;
using System.Text;

namespace IThemeSky.Management.Reports
{
    public class ReportData : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            RenderISpiritUserReport(context);            
        }

        public void RenderISpiritUserReport(HttpContext context)
        {
            string connectionString = ThemeRepositoryFactory.Default.ConnectionProvider.GetWriteConnectionString();
            int seed = context.Request.QueryString["seed"] != null ? Convert.ToInt32(context.Request.QueryString["seed"]) : 10;
            DateTime beginDate = DateTime.Now.AddDays(-1 * seed);
            StringBuilder sbCategories = new StringBuilder();
            StringBuilder sbAllUsers = new StringBuilder();
            StringBuilder sbOldUsers = new StringBuilder();

            string cmdText = "SELECT * FROM VisitorsDayReport WHERE ReportDate >= " + Convert.ToInt32(beginDate.ToString("yyyyMMdd"));
            using (IDataReader reader = SqlHelper.ExecuteReader(connectionString, CommandType.Text, cmdText))
            {
                sbCategories.Append("<categories>\r\n");
                sbAllUsers.Append("<dataset seriesName='总用户' color='AFD8F8' showValues='1'>\r\n");
                sbOldUsers.Append("<dataset seriesName='老用户' color='F6BD0F' showValues='1'>\r\n");
                while (reader.Read())
                {
                    sbCategories.AppendFormat("<category label='{0}' />\r\n", DateTime.ParseExact(reader["reportDate"].ToString(), "yyyyMMdd", null).ToString("yyyy-MM-dd"));
                    sbAllUsers.AppendFormat("<set value='{0}' />\r\n"
                        , reader["UsersNumber"]
                        );
                    sbOldUsers.AppendFormat("<set value='{0}' />\r\n"
                        , reader["OldUsersNumber"]
                        );
                }
                sbOldUsers.Append("</dataset>\r\n");
                sbAllUsers.Append("</dataset>\r\n");
                sbCategories.Append("</categories>\r\n");
            }

            System.IO.StreamWriter writter = new System.IO.StreamWriter(System.Web.HttpContext.Current.Response.OutputStream, System.Text.Encoding.UTF8);

            writter.Write("<?xml version=\"1.0\" encoding=\"utf-8\" ?><chart palette='1' caption='iSprit使用趋势图' showLabels='1' shownames='1' showvalues='1' decimals='0'  xAxisName='访问日期' yAxisName='用户数' useRoundEdges='1' legendBorderAlpha='0'>\r\n");
            writter.Write(sbCategories.ToString());
            writter.Write(sbAllUsers.ToString());
            writter.Write(sbOldUsers.ToString());
            writter.Write("</chart>");
            writter.Flush();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
