using Database.Helper;
using Sequencer.Events;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Configuration;
using System.Data;
using WCFService.Helper;
using WCFService.Interface;

namespace WCFService.UnitOfWork
{
    public class UnitOfwork : IDbUnitOfWork
    {
        /// <summary>
        /// 数据库插入/修改事件，成功则触发
        /// </summary>
        //public event Action<IGenericResult> DatabaseModifyEvent;
        private readonly string _sqlClientName = ConfigurationManager.AppSettings["QueueExecute1"];
        private readonly string _adoClientName = ConfigurationManager.AppSettings["QueueExecute2"];
        //private void OnDatabaseModify(IGenericResult result)
        //{
        //    DatabaseModifyEvent?.Invoke(result);
        //}
        public object Result(Guid id)
        {
            return GenericEventHandle.OnResultEvent(id);
        }
        /// <summary>
        /// 数据库操作返回，以GUID进行获取，需等待
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void ResultAsync(Guid id, AsyncCallback callback)
        {
            GenericEventHandle.OnResultEventAsync(id, callback);
        }
        /// <summary>
        /// 数据库记录获取
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="param"></param>
        public Guid Get(string sqlText, Hashtable param, bool sequence)
        {
            var id = Guid.NewGuid();
            var context = new Tuple<CmdOperate, ConcurrentDictionary<string, Hashtable>>(CmdOperate.Select, sqlText.ToContextParam(param));
            GenericEventHandle.OnGenericEvent(_sqlClientName, id, context, sequence);
            return id;
        }
        /// <summary>
        /// 向数据库插入记录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="param"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Guid Insert(string[] sqls, Hashtable[] param, bool sequence)
        {
            var id = Guid.NewGuid();
            var context = new Tuple<CmdOperate, ConcurrentDictionary<string, Hashtable>>(CmdOperate.Insert, sqls.ToContextParam(param));
            GenericEventHandle.OnGenericEvent(_sqlClientName, id, context, sequence);
            return id;
        }

        /// <summary>
        /// 向数据库更新记录， 参数param列表中必须包含where参数，否则更新失败；
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <param name="param"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Guid Update(string[] sqls, Hashtable[] param, bool sequence)
        {
            var id = Guid.NewGuid();
            var context = new Tuple<CmdOperate, ConcurrentDictionary<string, Hashtable>>(CmdOperate.Update, sqls.ToContextParam(param));
            GenericEventHandle.OnGenericEvent(_sqlClientName, id, context, sequence);
            return id;
        }

        /// <summary>
        /// 删除数据库中的记录，参数param列表中必须包含where参数，否则操作失败；
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public Guid Delete(string[] sqls, Hashtable[] param, bool sequence)
        {
            var id = Guid.NewGuid();
            var context = new Tuple<CmdOperate, ConcurrentDictionary<string, Hashtable>>(CmdOperate.Delete, sqls.ToContextParam(param));
            GenericEventHandle.OnGenericEvent(_sqlClientName, id, context, sequence);
            return id;
        }
        /// <summary>
        /// 数据库查询，返回值为查询结果第一行第一列的值
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <param name="sqlText"></param>
        /// <returns></returns>
        public Guid ExecuteScalar(string sqlText, Hashtable param, bool sequence)
        {
            var id = Guid.NewGuid();
            var context = new Tuple<CmdOperate, ConcurrentDictionary<string, Hashtable>>(CmdOperate.ExecuteScalar, sqlText.ToContextParam(param));

            GenericEventHandle.OnGenericEvent(_sqlClientName, id, context, sequence);
            return id;
        }
        /// <summary>
        /// 数据库查询，返回值为结果集，类型为<see cref="Hashtable"/>[]
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <param name="sqlText"></param>
        /// <returns></returns>
        public Guid ExecuteReader(string sqlText, Hashtable param, bool sequence)
        {
            var id = Guid.NewGuid();
            var context = new Tuple<CmdOperate, ConcurrentDictionary<string, Hashtable>>(CmdOperate.ExecuteReader, sqlText.ToContextParam(param));
            GenericEventHandle.OnGenericEvent(_sqlClientName, id, context, sequence);
            return id;
        }
        /// <summary>
        /// 数据库操作，返回值为受影响的记录数
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <param name="sqlText"></param>
        /// <returns></returns>
        public Guid ExecuteNoQuery(string[] sqls, Hashtable[] param, bool sequence)
        {
            var id = Guid.NewGuid();
            var context = new Tuple<CmdOperate, ConcurrentDictionary<string, Hashtable>>(CmdOperate.ExecuteNoQuery, sqls.ToContextParam(param));
            GenericEventHandle.OnGenericEvent(_sqlClientName, id, context, sequence);
            return id;
        }
        ///// <summary>
        ///// 数据库存储过程调用
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <param name="procedureName"></param>
        ///// <returns></returns>
        public Guid ExecuteProcedure(string procedureName, Hashtable param, bool sequence)
        {
            var id = Guid.NewGuid();
            var context = new Tuple<CmdOperate, ConcurrentDictionary<string, Hashtable>>(CmdOperate.ExecuteProcedure, procedureName.ToContextParam(param));
            GenericEventHandle.OnGenericEvent(_sqlClientName, id, context, sequence);
            return id;
        }
        /// <summary>
        /// 数据适配器查询
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sqlText"></param>
        /// <param name="dataSet"></param>
        /// <returns></returns>
        public Guid Get(string sqlText, bool sequence)
        {
            var id = Guid.NewGuid();
            var context = new Tuple<AptOperate, string[], DataSet[]>(AptOperate.Set, new string[1] { sqlText }, null);
            GenericEventHandle.OnGenericEvent(_adoClientName, id, context, sequence);
            return id;
        }
        /// <summary>
        /// 数据适配器更改（插入，更新，删除）
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sqlText"></param>
        /// <returns></returns>
        public Guid Set(string[] sqlText, DataSet[] dataSet, bool sequence)
        {
            var id = Guid.NewGuid();
            var context = new Tuple<AptOperate, string[], DataSet[]>(AptOperate.Set, sqlText, dataSet);
            GenericEventHandle.OnGenericEvent(_adoClientName, id, context, sequence);
            return id;
        }
    }
}
