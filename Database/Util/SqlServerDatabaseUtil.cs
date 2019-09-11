using Core.Entities;
using Core.Helper;
using Core.Interface;
using Dapper;
using Database.Entities;
using Database.Interface;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Database.Util
{
    internal class SqlServerDatabaseUtil : ISqlServerDatabaseUtil
    {
        #region 执行SQL， 返回DataTable
        public IGenericResult ExecuteReader(SqlConnection connection, string sqlText, DynamicParameters dyParam, IDbTransaction dbTransaction)
        {
            DataTable tb = new DataTable();
            try
            {
                tb.Load(connection.ExecuteReader(sqlText, dyParam, dbTransaction));
                return new GenericResultImpl(ResultType.Success, tb);
            }
            catch (Exception ex)
            {
                return new GenericResultImpl(ResultType.Error, new ExceptionMessage(ex).ExMessage, tb);
            }
        }
        #endregion
        #region 执行SQL， 返回object
        public IGenericResult ExecuteScalar(SqlConnection connection, string sqlText, DynamicParameters dyParam, IDbTransaction dbTransaction)
        {
            try
            {
                return new GenericResultImpl(ResultType.Success, connection.ExecuteScalar(sqlText, dyParam, dbTransaction));
            }
            catch (Exception ex)
            {
                return new GenericResultImpl(ResultType.Error, new ExceptionMessage(ex).ExMessage);
            }
        }
        #endregion
        #region 执行SQL， 返回int
        public IGenericResult ExecuteNoQuery(SqlConnection connection, string sqlText, DynamicParameters dyParam, IDbTransaction dbTransaction)
        {
            try
            {
                return new GenericResultImpl(ResultType.Success, connection.Execute(sqlText, dyParam, dbTransaction));
            }
            catch (Exception ex)
            {
                return new GenericResultImpl(ResultType.Error, new ExceptionMessage(ex).ExMessage);
            }
        }
        #endregion
        #region 执行SQL， 返回int
        public IGenericResult ExecuteProcedure(SqlConnection connection, string procedureName, DynamicParameters dyParam, IDbTransaction dbTransaction)
        {
            var tb = new DataTable();
            tb.Load(connection.ExecuteReader(procedureName, dyParam, dbTransaction, null, CommandType.StoredProcedure));
            return new GenericResultImpl(ResultType.Success, tb);
        }
        #endregion
    }
}
