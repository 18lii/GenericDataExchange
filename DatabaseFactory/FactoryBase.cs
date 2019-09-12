﻿using Core.Entities;
using Core.Events;
using Core.Interface;
using DatabaseFactory.Entity;
using DatabaseFactory.Interface;
using System;
using System.Collections.Concurrent;
using System.Data;

namespace DatabaseFactory
{
    public abstract class FactoryBase
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
        /// <param name="param"></param>
        public Guid Get(string userId, string[] sqlText, ConcurrentBag<ConcurrentDictionary<string, object>> param)
        {
            var context = new FactoryContext(PolicyType.Command, DbOperate.Select, sqlText, param);
            var evg = new GenericEventArgs<IFactoryContext>(_peristalticName, userId, context);
            GenericEventHandle.OnQueueEvent(evg);
            return evg.Id;
        }
        /// <summary>
        /// 向数据库插入记录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="param"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Guid Insert(string userId, string[] sqlText, ConcurrentBag<ConcurrentDictionary<string, object>> param)
        {
            var context = new FactoryContext(PolicyType.Command, DbOperate.Insert, sqlText, param);
            //context.ModifyEvent += callback;
            var evg = new GenericEventArgs<IFactoryContext>(_peristalticName, userId, context);
            GenericEventHandle.OnQueueEvent(evg);
            return evg.Id;
        }

        /// <summary>
        /// 向数据库更新记录， 参数param列表中必须包含where参数，否则更新失败；
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <param name="param"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Guid Update(string userId, string[] sqlText, ConcurrentBag<ConcurrentDictionary<string, object>> param)
        {
            var context = new FactoryContext(PolicyType.Command, DbOperate.Update, sqlText, param);
            //context.ModifyEvent += callback;
            var evg = new GenericEventArgs<IFactoryContext>(_peristalticName, userId, context);
            GenericEventHandle.OnQueueEvent(evg);
            return evg.Id;
        }

        /// <summary>
        /// 删除数据库中的记录，参数param列表中必须包含where参数，否则操作失败；
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public Guid Delete(string userId, string[] sqlText, ConcurrentBag<ConcurrentDictionary<string, object>> param)
        {
            IFactoryContext context = new FactoryContext(PolicyType.Command, DbOperate.Delete, sqlText, param);
            //context.ModifyEvent += callback;
            var evg = new GenericEventArgs<IFactoryContext>(_peristalticName, userId, context);
            GenericEventHandle.OnQueueEvent(evg);
            return evg.Id;
        }
        /// <summary>
        /// 数据库查询，返回值为查询结果第一行第一列的值
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <param name="sqlText"></param>
        /// <returns></returns>
        public Guid ExecuteScalar(string userId, string[] sqlText)
        {
            var context = new FactoryContext(PolicyType.Command, DbOperate.ExecuteScalar, sqlText);
            var evg = new GenericEventArgs<IFactoryContext>(_peristalticName, userId, context);
            GenericEventHandle.OnQueueEvent(evg);
            return evg.Id;
        }
        /// <summary>
        /// 数据库查询，返回值为结果集，类型为<see cref="Hashtable"/>[]
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <param name="sqlText"></param>
        /// <returns></returns>
        public Guid ExecuteReader(string userId, string[] sqlText)
        {
            var context = new FactoryContext(PolicyType.Command, DbOperate.ExecuteReader, sqlText);
            var evg = new GenericEventArgs<IFactoryContext>(_peristalticName, userId, context);
            GenericEventHandle.OnQueueEvent(evg);
            return evg.Id;
        }
        /// <summary>
        /// 数据库操作，返回值为受影响的记录数
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <param name="sqlText"></param>
        /// <returns></returns>
        public Guid ExecuteNoQuery(string userId, string[] sqlText)
        {
            var context = new FactoryContext(PolicyType.Command, DbOperate.ExecuteNoQuery, sqlText);
            var evg = new GenericEventArgs<IFactoryContext>(_peristalticName, userId, context);
            GenericEventHandle.OnQueueEvent(evg);
            return evg.Id;
        }
        ///// <summary>
        ///// 数据库存储过程调用
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <param name="procedureName"></param>
        ///// <returns></returns>
        public Guid ExecuteProcedure(string userId, string[] sqlText, ConcurrentBag<ConcurrentDictionary<string, object>> param)
        {
            var context = new FactoryContext(PolicyType.Command, DbOperate.ExecuteProcedure, sqlText, param);
            var evg = new GenericEventArgs<IFactoryContext>(_peristalticName, userId, context);
            GenericEventHandle.OnQueueEvent(evg);
            return evg.Id;
        }
        /// <summary>
        /// 数据适配器查询
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sqlText"></param>
        /// <param name="dataSet"></param>
        /// <returns></returns>
        public Guid Get(string userId, string[] sqlText)
        {
            var context = new FactoryContext(PolicyType.Adapter, AdapterOperate.Get, sqlText);
            var evg = new GenericEventArgs<IFactoryContext>(_peristalticName, userId, context);
            GenericEventHandle.OnQueueEvent(evg);
            return evg.Id;
        }
        /// <summary>
        /// 数据适配器更改（插入，更新，删除）
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sqlText"></param>
        /// <returns></returns>
        public Guid Set(string userId, string[] sqlText, DataSet[] dataSet)
        {
            var context = new FactoryContext(PolicyType.Adapter, AdapterOperate.Get, sqlText, dataSet);
            var evg = new GenericEventArgs<IFactoryContext>(_peristalticName, userId, context);
            GenericEventHandle.OnQueueEvent(evg);
            return evg.Id;
        }
    }
}