using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Database.Interface
{
    public interface ISqlCommandAccessor
    {
        Tuple<bool, object> ExecuteReader(SqlConnection connection, string sqlText, DynamicParameters dyParam, IDbTransaction dbTransaction);
        Tuple<bool, object> ExecuteScalar(SqlConnection connection, string sqlText, DynamicParameters dyParam, IDbTransaction dbTransaction);
        Tuple<bool, object> ExecuteNoQuery(SqlConnection connection, string sqlText, DynamicParameters dyParam, IDbTransaction dbTransaction);
        Tuple<bool, object> ExecuteProcedure(SqlConnection connection, string procedureName, DynamicParameters dyParam, IDbTransaction dbTransaction);
    }
}
