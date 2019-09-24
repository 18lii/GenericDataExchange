using Dapper;
using Database.Helper;
using Database.Interface;
using System;
using System.Collections.Concurrent;
using System.Data;
using System.Data.SqlClient;

namespace Database.Infrastructure
{
    internal abstract class BaseContext : IBaseContext
    {
        private readonly ISqlCommandAccessor _comUtil;
        private readonly ISqlAdapterAccessor _aptUtil;
        protected BaseContext()
        {
            Connection = new ConcurrentDictionary<Guid, SqlConnection>();
            Transaction = new ConcurrentDictionary<Guid, SqlTransaction>();
        }
        protected BaseContext(ISqlCommandAccessor cmdUtil)
            : this()
        {
            _comUtil = cmdUtil;

            
        }
        protected BaseContext(ISqlAdapterAccessor aptUtil)
            : this()
        {
            _aptUtil = aptUtil;
        }
        public string ConnectionString { get; set; }
        protected void SetConnection(out Guid guid)
        {
            var id = Guid.NewGuid();
            Connection.TryAdd(id, new SqlConnection(ConnectionString));
            if (Connection[id].State == ConnectionState.Closed)
            {
                Connection[id].Open();
                Transaction.TryAdd(id, Connection[id].BeginTransaction());
            }
            guid = id;
        }
        public ConcurrentDictionary<Guid, SqlConnection> Connection { get; set; }
        public ConcurrentDictionary<Guid, SqlTransaction> Transaction { get; set; }
        
        /// <summary>
        /// 数据库提交方法
        /// </summary>
        /// <param name="command"></param>
        /// <param name="dbOperate"></param>
        /// <returns></returns>
        public Tuple<bool, object> Accept(Guid id, CmdOperate operate, string sqlText, DynamicParameters param)
        {
            switch (operate)
            {
                case CmdOperate.Select:
                    return _comUtil.ExecuteReader(Connection[id], sqlText, param, Transaction[id]);
                case CmdOperate.Insert:
                    return _comUtil.ExecuteNoQuery(Connection[id], sqlText, param, Transaction[id]); ;
                case CmdOperate.Update:
                    return _comUtil.ExecuteNoQuery(Connection[id], sqlText, param, Transaction[id]);
                case CmdOperate.Delete:
                    return _comUtil.ExecuteNoQuery(Connection[id], sqlText, param, Transaction[id]);
                case CmdOperate.ExecuteScalar:
                    return _comUtil.ExecuteScalar(Connection[id], sqlText, param, Transaction[id]);
                case CmdOperate.ExecuteReader:
                    return _comUtil.ExecuteReader(Connection[id], sqlText, param, Transaction[id]);
                case CmdOperate.ExecuteNoQuery:
                    return _comUtil.ExecuteNoQuery(Connection[id], sqlText, param, Transaction[id]);
                case CmdOperate.ExecuteProcedure:
                    return _comUtil.ExecuteProcedure(Connection[id], sqlText, param, Transaction[id]);
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
        public Tuple<bool, object> DbCommit(Guid id)
        {
            if (Transaction.TryRemove(id, out var tran))
            {
                try
                {
                    tran.Commit();
                    return new Tuple<bool, object>(true, "Commited");
                }
                catch (Exception e)
                {
                    tran.Rollback();
                    return new Tuple<bool, object>(false, new ExceptionMessage(e).ExMessage);
                }
                finally
                {
                    if(Connection.TryRemove(id, out var conn))
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
            else
            {
                return new Tuple<bool, object>(true, "提交了不存在的事务！");
            }
            
        }
        public Tuple<bool, object> DbRollback(Guid id)
        {
            if(Transaction.TryRemove(id, out var tran))
            {
                try
                {
                    tran.Rollback();
                    return new Tuple<bool, object>(true, "Rollbacked");
                }
                catch (Exception e)
                {
                    return new Tuple<bool, object>(false, new ExceptionMessage(e).ExMessage);
                }
                finally
                {
                    if(Connection.TryRemove(id, out var conn))
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
            else
            {
                return new Tuple<bool, object>(true, "回滚了不存在的事务！");
            }
        }
    }
}
