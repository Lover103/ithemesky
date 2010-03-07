using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace IThemeSky.Library.Data
{
    public static class SqlParameterHelper
    {
        public static SqlParameter BuildInputParameter(string parameterName, SqlDbType dbType, int size, object value)
        {
            SqlParameter parameter = new SqlParameter(parameterName, dbType, size);
            parameter.Value = value;
            parameter.Direction = ParameterDirection.Input;
            return parameter;
        }

        public static SqlParameter BuildOutputParameter(string parameterName, SqlDbType dbType, int size, object value)
        {
            SqlParameter parameter = new SqlParameter(parameterName, dbType, size);
            parameter.Value = value;
            parameter.Direction = ParameterDirection.Output;
            return parameter;
        }

        public static SqlParameter BuildOutputParameter(string parameterName, SqlDbType dbType, int size, ParameterDirection direction, object value)
        {
            SqlParameter parameter = new SqlParameter(parameterName, dbType, size);
            parameter.Value = value;
            parameter.Direction = direction;
            return parameter;
        }
    }
}
