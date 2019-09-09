using Core.Interface;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Interface
{
    public interface ISqlServerDatabaseUtil
    {
        IGenericResult ExecuteReader(SqlConnection connection, string sqlText, DynamicParameters dyParam, IDbTransaction dbTransaction);
        IGenericResult ExecuteScalar(SqlConnection connection, string sqlText, DynamicParameters dyParam, IDbTransaction dbTransaction);
        IGenericResult ExecuteNoQuery(SqlConnection connection, string sqlText, DynamicParameters dyParam, IDbTransaction dbTransaction);
        IGenericResult ExecuteProcedure(SqlConnection connection, string procedureName, DynamicParameters dyParam, IDbTransaction dbTransaction);
    }
}
