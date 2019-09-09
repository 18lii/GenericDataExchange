using Core.Entities;
using Core.Helper;
using Core.Interface;
using Dapper;
using Database.Entities;
using Database.Interface;
using Database.Util;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Infrastructure
{
    public abstract class BaseContext : IBaseContext
    {
        protected BaseContext(string connectionString)
        {
            ConnectionString = connectionString;
            SqlConnection = new ConcurrentDictionary<string, SqlConnection>();
            SqlTransaction = new ConcurrentDictionary<Guid, SqlTransaction>();
            DbUtil = new SqlServerDatabaseUtil();
        }
        private string ConnectionString { get; set; }
        protected SqlConnection SetConnection(string userId)
        {
            SqlConnection.TryAdd(userId, new SqlConnection(ConnectionString));
            if (SqlConnection[userId].State == ConnectionState.Closed)
            {
                SqlConnection[userId].Open();
            }
            return SqlConnection[userId];
        }
        public ConcurrentDictionary<string, SqlConnection> SqlConnection { get; set; }
        public ConcurrentDictionary<Guid, SqlTransaction> SqlTransaction { get; set; }
        private SqlServerDatabaseUtil DbUtil { get; }
        /// <summary>
        /// 数据库提交方法
        /// </summary>
        /// <param name="command"></param>
        /// <param name="dbOperate"></param>
        /// <returns></returns>
        public IGenericResult Accept(SqlConnection connection, DbOperate operate, string sqlText, DynamicParameters param, SqlTransaction transaction)
        {
            switch (operate)
            {
                case DbOperate.Select:
                    return DbUtil.ExecuteReader(connection, sqlText, param, transaction);
                case DbOperate.Insert:
                    return DbUtil.ExecuteNoQuery(connection, sqlText, param, transaction); ;
                case DbOperate.Update:
                    return DbUtil.ExecuteNoQuery(connection, sqlText, param, transaction);
                case DbOperate.Delete:
                    return DbUtil.ExecuteNoQuery(connection, sqlText, param, transaction);
                case DbOperate.ExecuteScalar:
                    return DbUtil.ExecuteScalar(connection, sqlText, param, transaction);
                case DbOperate.ExecuteReader:
                    return DbUtil.ExecuteReader(connection, sqlText, param, transaction);
                case DbOperate.ExecuteNoQuery:
                    return DbUtil.ExecuteNoQuery(connection, sqlText, param, transaction);
                case DbOperate.ExecuteProcedure:
                    return DbUtil.ExecuteProcedure(connection, sqlText, param, transaction);
                default:
                    return new GenericResultImpl(ResultType.IllegalOperation, ResultType.IllegalOperation.ToDescription(), "错误的数据库操作类型。");
            }
        }
        /// <summary>
        /// 数据库事务提交
        /// </summary>
        /// <returns></returns>
        public IGenericResult DbCommit(string userId, Guid id)
        {
            var t = SqlTransaction.TryRemove(id, out var value);
            try
            {
                if (t)
                {
                    value.Commit();
                    return new GenericResultImpl(ResultType.Success, "Commited");
                }
                else
                {
                    return new GenericResultImpl(ResultType.IllegalOperation, "提交了不存在的数据库事务！！！");
                }
            }
            catch (Exception exp)
            {
                value.Rollback();
                var em = new ExceptionMessage(exp, "Rollback");
                return new GenericResultImpl(ResultType.NoChanged, em.ExMessage, em.UserMessage);
            }
            finally
            {
                SqlConnection[userId].Close();
            }
        }
        public IGenericResult DbRollback(string userId, Guid id)
        {
            try
            {
                if (SqlTransaction.TryRemove(id, out var value))
                {
                    value.Rollback();
                    return new GenericResultImpl(ResultType.Success, "Commited");
                }
                else
                {
                    return new GenericResultImpl(ResultType.IllegalOperation, "回滚了不存在的数据库事务！！！");
                }
            }
            catch (Exception exp)
            {
                var em = new ExceptionMessage(exp, "Rollback");
                return new GenericResultImpl(ResultType.NoChanged, em.ExMessage, em.UserMessage);
            }
            finally
            {
                SqlConnection[userId].Close();
            }
        }
    }
}
