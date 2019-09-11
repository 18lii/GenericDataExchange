using Core.Entities;
using Core.Events;
using Core.Interface;
using Database.Entities;
using Database.Interface;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Factories
{
    public abstract class DatabaseFactory
    {
        /// <summary>
        /// 数据库插入/修改事件，成功则触发
        /// </summary>
        public event Action<IGenericResult> DatabaseModifyEvent;
        private readonly string _peristalticName = "DatabaseService";
        private void OnDatabaseModify(IGenericResult result)
        {
            DatabaseModifyEvent?.Invoke(result);
        }
        /// <summary>
        /// 数据库操作返回，以GUID进行获取，需等待
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IGenericResult Result(Guid id)
        {
            return GenericEventHandle.OnResultEvent(id);
        }
        /// <summary>
        /// 数据库记录获取
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <param name="param"></param>
        public Guid Get(string userId, string sqlText, ConcurrentBag<ConcurrentDictionary<string, object>> param)
        {
            var context = new FactoryContext(DbOperate.Select, param, sqlText);
            var evg = new GenericEventArgs<IFactoryContext>(_peristalticName, userId, context);
            GenericEventHandle.OnQueueEvent(evg);
            return evg.Id;
        }

        ///// <summary>
        ///// 向数据库插入记录
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <param name="name"></param>
        ///// <param name="param"></param>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //public Guid Insert(string userId, IList<IGenericParameter> param, IList<Hashtable> data = null, Action<IGenericResult> callback = null)
        //{
        //    var context = new FactoryContext(DbOperate.Insert, param.Set(name, data));
        //    context.ModifyEvent += callback;
        //    var evg = new GenericEventArgs<IFactoryContext>(_peristalticName, userId, context);
        //    GenericEventHandle.OnQueueEvent(evg);
        //    return evg.Id;
        //}

        ///// <summary>
        ///// 向数据库更新记录， 参数param列表中必须包含<see cref="IGenericParameter.ParamType"/> = W的参数，否则更新失败；
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <param name="name"></param>
        ///// <param name="param"></param>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //public Guid Update(string userId, IList<IGenericParameter> param, IList<Hashtable> data = null, Action<IGenericResult> callback = null)
        //{
        //    var context = new FactoryContext( DbOperate.Update, param.Set(name, data));
        //    context.ModifyEvent += callback;
        //    var evg = new GenericEventArgs<IFactoryContext>(_peristalticName, userId, context);
        //    GenericEventHandle.OnQueueEvent(evg);
        //    return evg.Id;
        //}

        ///// <summary>
        ///// 删除数据库中的记录，参数param列表中必须包含<see cref="IGenericParameter.ParamType"/> = W的参数，否则更新失败；
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <param name="name"></param>
        ///// <param name="param"></param>
        ///// <returns></returns>
        //public Guid Delete(string userId, string name, IList<IGenericParameter> param,  Action<IGenericResult> callback = null)
        //{
        //    IFactoryContext context = new FactoryContext(DbOperate.Delete, new ConcurrentBag<ConcurrentBag<IGenericParameter>>
        //    {
        //        new ConcurrentBag<IGenericParameter>(param)
        //    });
        //    context.ModifyEvent += callback;
        //    var evg = new GenericEventArgs<IFactoryContext>(_peristalticName, userId, context);
        //    GenericEventHandle.OnQueueEvent(evg);
        //    return evg.Id;
        //}
        ///// <summary>
        ///// 数据库查询，返回值为查询结果第一行第一列的值
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <param name="name"></param>
        ///// <param name="sqlText"></param>
        ///// <returns></returns>
        //public Guid ExecuteScalar(string userId, string name, string sqlText, IList<IGenericParameter> param = null)
        //{
        //    var context = new FactoryContext(name, DbOperate.ExecuteScalar, param.Set(name, null), sqlText);
        //    var evg = new GenericEventArgs<IFactoryContext>(_peristalticName, userId, context);
        //    GenericEventHandle.OnQueueEvent(evg);
        //    return evg.Id;
        //}
        ///// <summary>
        ///// 数据库查询，返回值为结果集，类型为<see cref="Hashtable"/>[]
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <param name="name"></param>
        ///// <param name="sqlText"></param>
        ///// <returns></returns>
        //public Guid ExecuteReader(string userId,string name, string sqlText, IList<IGenericParameter> param = null)
        //{
        //    var context = new FactoryContext(name, DbOperate.ExecuteReader, param.Set(name, null), sqlText);
        //    var evg = new GenericEventArgs<IFactoryContext>(_peristalticName, userId, context);
        //    GenericEventHandle.OnQueueEvent(evg);
        //    return evg.Id;
        //}
        ///// <summary>
        ///// 数据库操作，返回值为受影响的记录数
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <param name="name"></param>
        ///// <param name="sqlText"></param>
        ///// <returns></returns>
        //public Guid ExecuteNoQuery(string userId,string name, string sqlText, IList<IGenericParameter> param = null)
        //{
        //    var context = new FactoryContext(name, DbOperate.ExecuteNoQuery, param.Set(name, null), sqlText);
        //    var evg = new GenericEventArgs<IFactoryContext>(_peristalticName, userId, context);
        //    GenericEventHandle.OnQueueEvent(evg);
        //    return evg.Id;
        //}
        ///// <summary>
        ///// 数据库存储过程调用
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <param name="procedureName"></param>
        ///// <returns></returns>
        //public Guid ExecuteProcedure(string procedureName, string userId, IList<IGenericParameter> param = null)
        //{
        //    var context = new FactoryContext(procedureName, DbOperate.ExecuteProcedure, param.Set(procedureName, null));
        //    var evg = new GenericEventArgs<IFactoryContext>(_peristalticName, userId, context);
        //    GenericEventHandle.OnQueueEvent(evg);
        //    return evg.Id;
        //}
    }
}
