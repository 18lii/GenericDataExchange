using Dapper;
using Database.Helper;
using Database.Interface;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Database.Accessor
{
    /// <summary>
    /// 数据客户端-数据库存取类
    /// </summary>
    internal class SqlCommandAccessor : ISqlCommandAccessor
    {
        #region 执行SQL， 返回DataTable
        public Tuple<bool, object> ExecuteReader(SqlConnection connection, string sqlText, DynamicParameters dyParam, IDbTransaction dbTransaction)
        {
            try
            {
                var tb = new DataTable();
                tb.Load(connection.ExecuteReader(sqlText, dyParam, dbTransaction));
                return new Tuple<bool, object>(true, tb);
            }
            catch (Exception e)
            {
                return new Tuple<bool, object>(false, new ExceptionMessage(e).ExMessage);
            }
        }
        #endregion
        #region 执行SQL， 返回object
        public Tuple<bool, object> ExecuteScalar(SqlConnection connection, string sqlText, DynamicParameters dyParam, IDbTransaction dbTransaction)
        {
            try
            {
                return new Tuple<bool, object>(true, connection.ExecuteScalar(sqlText, dyParam, dbTransaction));
            }
            catch (Exception ex)
            {
                return new Tuple<bool, object>(false, new ExceptionMessage(ex).ExMessage);
            }
        }
        #endregion
        #region 执行SQL， 返回int
        public Tuple<bool, object> ExecuteNoQuery(SqlConnection connection, string sqlText, DynamicParameters dyParam, IDbTransaction dbTransaction)
        {
            try
            {
                return new Tuple<bool, object>(true, connection.Execute(sqlText, dyParam, dbTransaction));
            }
            catch (Exception ex)
            {
                return new Tuple<bool, object>(false, new ExceptionMessage(ex).ExMessage);
            }
        }
        #endregion
        #region 执行SQL， 返回int
        public Tuple<bool, object> ExecuteProcedure(SqlConnection connection, string procedureName, DynamicParameters dyParam, IDbTransaction dbTransaction)
        {
            try
            {
                var tb = new DataTable();
                tb.Load(connection.ExecuteReader(procedureName, dyParam, dbTransaction, null, CommandType.StoredProcedure));
                return new Tuple<bool, object>(true, tb);
            }
            catch (Exception e)
            {
                return new Tuple<bool, object>(false, new ExceptionMessage(e).ExMessage);
            }
            
        }
        #endregion
    }
}
