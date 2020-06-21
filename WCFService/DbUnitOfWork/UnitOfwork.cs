using Sequencer.Interface;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Configuration;
using System.Data;
using TransparentAgent.Contract;
using TransparentAgent.Interface;
using WCFService.Events;
using WCFService.Helper;
using WCFService.Infrastructure;
using WCFService.Interface;

namespace WCFService.DbUnitOfWork
{
    internal class UnitOfwork : IDbUnitOfWork
    {
        public UnitOfwork(ISequencerEntry sequencerEntry)
        {
            SequencerEntry = sequencerEntry;
            _sqlEvent = new ServiceEventHandle<Tuple<int, ConcurrentDictionary<string, Hashtable>>, Tuple<bool, object>>();
            _adoEvent = new ServiceEventHandle<Tuple<int, string[], DataSet[]>, Tuple<bool, object>>();
        }

        #region 定序器使用的变量
        private readonly string _sqlClientName = ConfigurationManager.AppSettings["QueueExecute1"];
        private readonly string _adoClientName = ConfigurationManager.AppSettings["QueueExecute2"];
        private readonly IServiceEventHandle<Tuple<int, ConcurrentDictionary<string, Hashtable>>, Tuple<bool, object>> _sqlEvent;
        private readonly IServiceEventHandle<Tuple<int, string[], DataSet[]>, Tuple<bool, object>> _adoEvent;
        public ISequencerEntry SequencerEntry { get; set; }
        public event Action<object> ResultEvent
        {
            add
            {
                SequencerEntry.ResultCallbackEvent +=  obj => 
                {
                    var result = (Tuple<bool, object>)obj;
                    value.Invoke(new ServiceResult(result.Item1, result.Item2).Compression());
                };
            }
            remove { }
        }
        #endregion
        /// <summary>
        /// 数据库记录获取
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="param"></param>
        public byte[] Select(byte[] data)
        {
            var result = _sqlEvent.OnActiveEvent(data.ToContextParam(101, out var sequence));
            return new ServiceResult(result.Item1, result.Item2).Compression();
        }
        
        /// <summary>
        /// 向数据库插入记录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="param"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] Insert(byte[] data)
        {
            var result = _sqlEvent.OnActiveEvent(data.ToContextParam(102, out var sequence));
            return new ServiceResult(result.Item1, result.Item2).Compression().Compression();
        }
        
        /// <summary>
        /// 向数据库更新记录， 参数param列表中必须包含where参数，否则更新失败；
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <param name="param"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] Update(byte[] data)
        {
            var result = _sqlEvent.OnActiveEvent(data.ToContextParam(103, out var sequence));
            return new ServiceResult(result.Item1, result.Item2).Compression();
        }
        
        /// <summary>
        /// 删除数据库中的记录，参数param列表中必须包含where参数，否则操作失败；
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public byte[] Delete(byte[] data)
        {
            var result = _sqlEvent.OnActiveEvent(data.ToContextParam(104, out var sequence));
            return new ServiceResult(result.Item1, result.Item2).Compression();
        }
        
        /// <summary>
        /// 数据库查询，返回值为查询结果第一行第一列的值
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <param name="sqlText"></param>
        /// <returns></returns>
        public byte[] ExecuteScalar(byte[] data)
        {
            var result = _sqlEvent.OnActiveEvent(data.ToContextParam(201, out var sequence));
            return new ServiceResult(result.Item1, result.Item2).Compression();
        }
        
        /// <summary>
        /// 数据库查询，返回值为结果集，类型为<see cref="Hashtable"/>[]
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <param name="sqlText"></param>
        /// <returns></returns>
        public byte[] ExecuteReader(byte[] data)
        {
            var result = _sqlEvent.OnActiveEvent(data.ToContextParam(202, out var sequence));
            return new ServiceResult(result.Item1, result.Item2).Compression();
        }
        
        /// <summary>
        /// 数据库操作，返回值为受影响的记录数
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <param name="sqlText"></param>
        /// <returns></returns>
        public byte[] ExecuteNoQuery(byte[] data)
        {
            var result = _sqlEvent.OnActiveEvent(data.ToContextParam(203, out var sequence));
            return new ServiceResult(result.Item1, result.Item2).Compression();
        }
        
        ///// <summary>
        ///// 数据库存储过程调用
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <param name="procedureName"></param>
        ///// <returns></returns>
        public byte[] ExecuteProcedure(byte[] data)
        {
            var result = _sqlEvent.OnActiveEvent(data.ToContextParam(204, out var sequence));
            return new ServiceResult(result.Item1, result.Item2).Compression();
        }
        
        /// <summary>
        /// 数据适配器查询
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="dataSet"></param>
        /// <returns></returns>
        public byte[] Get(byte[] data)
        {
            var param = data.Decompress<IContractData>();
            var result = _adoEvent.OnActiveEvent(new Tuple<int, string[], DataSet[]>(301, param.SqlText, null));
            return new ServiceResult(result.Item1, result.Item2).Compression();
        }
        
        /// <summary>
        /// 数据适配器更改（插入，更新，删除）
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="dataSet"></param>
        /// <returns></returns>
        public byte[] Set(byte[] data)
        {
            var param = data.Decompress<IContractData>();
            var result = _adoEvent.OnActiveEvent(new Tuple<int, string[], DataSet[]>(302, param.SqlText, param.DataSet));
            return new ServiceResult(result.Item1, result.Item2).Compression();
        }

        //public byte[] SequentialSelect(byte[] data)
        //{
        //    var context = data.ToContextParam(101, out var sequence);
        //    var result = SequencerEntry.Access(_sqlClientName, context, sequence) as Tuple<bool, object>;
        //    return new ServiceResult(result.Item1, result.Item2).Compression();
        //}
        //public byte[] SequentialInsert(byte[] data)
        //{
        //    var context = data.ToContextParam(102, out var sequence);
        //    var result = SequencerEntry.Access(_sqlClientName, context, sequence) as Tuple<bool, object>;
        //    return new ServiceResult(result.Item1, result.Item2).Compression();
        //}
        //public byte[] SequentialUpdate(byte[] data)
        //{
        //    var context = data.ToContextParam(103, out var sequence);
        //    var result = SequencerEntry.Access(_sqlClientName, context, sequence) as Tuple<bool, object>;
        //    return new ServiceResult(result.Item1, result.Item2).Compression();
        //}
        //public byte[] SequentialDelete(byte[] data)
        //{
        //    var context = data.ToContextParam(104, out var sequence);
        //    var result = SequencerEntry.Access(_sqlClientName, context, sequence) as Tuple<bool, object>;
        //    return new ServiceResult(result.Item1, result.Item2).Compression();
        //}
        //public byte[] SequentialExecuteScalar(byte[] data)
        //{
        //    var context = data.ToContextParam(201, out var sequence);
        //    var result = SequencerEntry.Access(_sqlClientName, context, sequence) as Tuple<bool, object>;
        //    return new ServiceResult(result.Item1, result.Item2).Compression();
        //}
        //public byte[] SequentialExecuteReader(byte[] data)
        //{
        //    var context = data.ToContextParam(202, out var sequence);
        //    var result = SequencerEntry.Access(_sqlClientName, context, sequence) as Tuple<bool, object>;
        //    return new ServiceResult(result.Item1, result.Item2).Compression();
        //}
        //public byte[] SequentialExecuteNoQuery(byte[] data)
        //{
        //    var context = data.ToContextParam(203, out var sequence);
        //    var result = SequencerEntry.Access(_sqlClientName, context, sequence) as Tuple<bool, object>;
        //    return new ServiceResult(result.Item1, result.Item2).Compression();
        //}
        //public byte[] SequentialExecuteProcedure(byte[] data)
        //{
        //    var context = data.ToContextParam(204, out var sequence);
        //    var result = SequencerEntry.Access(_sqlClientName, context, sequence) as Tuple<bool, object>;
        //    return new ServiceResult(result.Item1, result.Item2).Compression();
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="sqlText"></param>
        ///// <param name="sequence"></param>
        ///// <returns></returns>
        //public byte[] SequentialGet(byte[] data)
        //{
        //    var context = data.ToContextParam(301, out var sequence);
        //    var result = SequencerEntry.Access(_adoClientName, context, sequence) as Tuple<bool, object>;
        //    return new ServiceResult(result.Item1, result.Item2).Compression();
        //}
        ///// <summary>
        ///// 数据适配器更改（插入，更新，删除）
        ///// </summary>
        ///// <param name="sqlText"></param>
        ///// <param name="dataSet"></param>
        ///// <param name="sequence"></param>
        ///// <returns></returns>
        //public byte[] SequentialSet(byte[] data)
        //{
        //    var context = data.ToContextParam(301, out var sequence);
        //    var result = SequencerEntry.Access(_adoClientName, context, sequence) as Tuple<bool, object>;
        //    return new ServiceResult(result.Item1, result.Item2).Compression();
        //}
        public void SequentialSelectAsync(byte[] data)
        {
            var context = data.ToContextParam(101, out var sequence);
            SequencerEntry.AccessAsync(_sqlClientName, context, sequence);
        }
        public void SequentialInsertAsync(byte[] data)
        {
            var context = data.ToContextParam(102, out var sequence);
            SequencerEntry.AccessAsync(_sqlClientName, context, sequence);
        }
        public void SequentialUpdateAsync(byte[] data)
        {
            var context = data.ToContextParam(103, out var sequence);
            SequencerEntry.AccessAsync(_sqlClientName, context, sequence);
        }
        public void SequentialDeleteAsync(byte[] data)
        {
            var context = data.ToContextParam(104, out var sequence);
            SequencerEntry.AccessAsync(_sqlClientName, context, sequence);
        }
        public void SequentialExecuteScalarAsync(byte[] data)
        {
            var context = data.ToContextParam(201, out var sequence);
            SequencerEntry.AccessAsync(_sqlClientName, context, sequence);
        }
        public void SequentialExecuteReaderAsync(byte[] data)
        {
            var context = data.ToContextParam(202, out var sequence);
            SequencerEntry.AccessAsync(_sqlClientName, context, sequence);
        }
        public void SequentialExecuteNoQueryAsync(byte[] data)
        {
            var context = data.ToContextParam(203, out var sequence);
            SequencerEntry.AccessAsync(_sqlClientName, context, sequence);
        }
        public void SequentialExecuteProcedureAsync(byte[] data)
        {
            var context = data.ToContextParam(204, out var sequence);
            SequencerEntry.AccessAsync(_sqlClientName, context, sequence);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public void SequentialGetAsync(byte[] data)
        {
            var param = data.Decompress<IContractData>();
            var context = new Tuple<int, string[], DataSet[]>(301, param.SqlText, null);
            SequencerEntry.AccessAsync(_adoClientName, context, param.Sequence);
        }
        /// <summary>
        /// 数据适配器更改（插入，更新，删除）
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="dataSet"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public void SequentialSetAsync(byte[] data)
        {
            var param = data.Decompress<IContractData>();
            var context = new Tuple<int, string[], DataSet[]>(302, param.SqlText, param.DataSet);
            SequencerEntry.AccessAsync(_adoClientName, context, param.Sequence);
        }
    }
}
