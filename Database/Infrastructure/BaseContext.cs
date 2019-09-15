using Dapper;
using Database.Helper;
using Database.Interface;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Database.Infrastructure
{
    internal abstract class BaseContext : IBaseContext
    {
        private readonly ISqlCommandDatabaseUtil _comUtil;
        private readonly ISqlAdapterDatabaseUtil _aptUtil;
        protected BaseContext(ISqlCommandDatabaseUtil cmdUtil)
        {
            _comUtil = cmdUtil;
            
        }
        protected BaseContext(ISqlAdapterDatabaseUtil aptUtil)
        {
            _aptUtil = aptUtil;
        }
        public string ConnectionString { get; set; }
        protected SqlConnection SetConnection()
        {
            Connection = new SqlConnection(ConnectionString);
            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
                Transaction = Connection.BeginTransaction();
            }
            return Connection;
        }
        public SqlConnection Connection { get; set; }
        public SqlTransaction Transaction { get; set; }
        
        /// <summary>
        /// 数据库提交方法
        /// </summary>
        /// <param name="command"></param>
        /// <param name="dbOperate"></param>
        /// <returns></returns>
        public Tuple<bool, object> Accept(SqlConnection connection, CmdOperate operate, string sqlText, DynamicParameters param, SqlTransaction transaction)
        {
            switch (operate)
            {
                case CmdOperate.Select:
                    return _comUtil.ExecuteReader(connection, sqlText, param, transaction);
                case CmdOperate.Insert:
                    return _comUtil.ExecuteNoQuery(connection, sqlText, param, transaction); ;
                case CmdOperate.Update:
                    return _comUtil.ExecuteNoQuery(connection, sqlText, param, transaction);
                case CmdOperate.Delete:
                    return _comUtil.ExecuteNoQuery(connection, sqlText, param, transaction);
                case CmdOperate.ExecuteScalar:
                    return _comUtil.ExecuteScalar(connection, sqlText, param, transaction);
                case CmdOperate.ExecuteReader:
                    return _comUtil.ExecuteReader(connection, sqlText, param, transaction);
                case CmdOperate.ExecuteNoQuery:
                    return _comUtil.ExecuteNoQuery(connection, sqlText, param, transaction);
                case CmdOperate.ExecuteProcedure:
                    return _comUtil.ExecuteProcedure(connection, sqlText, param, transaction);
                default:
                    return new Tuple<bool, object>(false, "错误的数据库操作类型。");
            }
        }
        public Tuple<bool, object> Accept(AptOperate operate, SqlCommand command, DataSet dataSet = null)
        {
            switch (operate)
            {
                case AptOperate.Get:
                    return _aptUtil.Get(command);
                case AptOperate.Set:
                    return _aptUtil.Set(command, dataSet);
                default:
                    return new Tuple<bool, object>(false, "错误的数据库操作类型");
            }
        }
        /// <summary>
        /// 数据库事务提交
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, object> DbCommit()
        {
            try
            {
                    Transaction.Commit();
                    return new Tuple<bool, object>(true, "Commited");
            }
            catch (Exception e)
            {
                Transaction.Rollback();
                return new Tuple<bool, object>(false, new ExceptionMessage(e).ExMessage);
            }
            finally
            {
                Connection.Close();
                Connection.Dispose();
            }
        }
        public Tuple<bool, object> DbRollback()
        {
            try
            {
                Transaction.Rollback();
                    return new Tuple<bool, object>(true, "Commited");
            }
            catch (Exception e)
            {
                return new Tuple<bool, object>(false, new ExceptionMessage(e).ExMessage);
            }
            finally
            {
                Connection.Close();
                Connection.Dispose();
            }
        }
    }
}
