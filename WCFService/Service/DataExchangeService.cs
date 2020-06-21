using System;
using System.ServiceModel;
using TransparentAgent.Contract;
using WCFService.Infrastructure;
using WCFService.Interface;

namespace WCFService.Service
{
    /// <summary>
    /// WCF服务类
    /// </summary>
    [IoCServiceBehavior]
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.PerSession)]
    public class DataExchangeService : IDataExchangeService
    {
        private readonly IDbUnitOfWork _dbUnitOfWork;
        private readonly IServiceCallback callback = OperationContext.Current.GetCallbackChannel<IServiceCallback>();
        public DataExchangeService(IDbUnitOfWork dbUnitOfWork)
        {
            dbUnitOfWork.ResultEvent += obj =>
            {
                var temp = callback;
                var result = (Tuple<bool, object>)obj;
                temp.ServiceCallback(new ServiceResult(result.Item1, result.Item2).Compression());
            };
            _dbUnitOfWork = dbUnitOfWork;
        }

        #region 常规数据服务部分
        public byte[] Select(byte[] value)
        {
            return _dbUnitOfWork.Select(value);
        }
        public void BeginSelect(byte[] value)
        {
            callback.ServiceCallback(_dbUnitOfWork.Select(value));
        }
        
        public byte[] Insert(byte[] value)
        {
            return _dbUnitOfWork.Insert(value);
        }
        public void BeginInsert(byte[] value)
        {
            callback.ServiceCallback(_dbUnitOfWork.Insert(value));
        }
        public byte[] Update(byte[] value)
        {
            return _dbUnitOfWork.Update(value);
        }
        public void BeginUpdate(byte[] value)
        {
           callback.ServiceCallback(_dbUnitOfWork.Update(value));
        }
        public byte[] Delete(byte[] value)
        {
            return _dbUnitOfWork.Delete(value);
        }
        public void BeginDelete(byte[] value)
        {
            callback.ServiceCallback(_dbUnitOfWork.Delete(value));
        }
        public byte[] ExecuteNoQuery(byte[] value)
        {
            return _dbUnitOfWork.ExecuteNoQuery(value);
        }
        public void BeginExecuteNoQuery(byte[] value)
        {
            callback.ServiceCallback(_dbUnitOfWork.ExecuteNoQuery(value));
        }
        public byte[] ExecuteProcedure(byte[] value)
        {
            return _dbUnitOfWork.ExecuteProcedure(value);
        }
        public void BeginExecuteProcedure(byte[] value)
        {
            callback.ServiceCallback(_dbUnitOfWork.ExecuteProcedure(value));
        }
        public byte[] ExecuteReader(byte[] value)
        {
            return _dbUnitOfWork.ExecuteReader(value);
        }
        public void BeginExecuteReader(byte[] value)
        {
           callback.ServiceCallback(_dbUnitOfWork.ExecuteReader(value));
        }
        public byte[] ExecuteScalar(byte[] value)
        {
            return _dbUnitOfWork.ExecuteScalar(value);
        }
        public void BeginExecuteScalar(byte[] value)
        {
            callback.ServiceCallback(_dbUnitOfWork.ExecuteScalar(value));
        }
        public byte[] AdapterGet(byte[] value)
        {
            return _dbUnitOfWork.Get(value);
        }
        public void BeginAdapterGet(byte[] value)
        {
            callback.ServiceCallback(_dbUnitOfWork.Get(value));
        }
        public byte[] AdapterSet(byte[] value)
        {
            return _dbUnitOfWork.Set(value);
        }
        public void BeginAdapterSet(byte[] value)
        {
            callback.ServiceCallback(_dbUnitOfWork.Set(value));
        }
        #endregion

        #region 定序器数据服务部分
        //public byte[] SequentialSelect(byte[] value)
        //{
        //    return _dbUnitOfWork.SequentialSelect(value);
        //}
        //public byte[] SequentialInsert(byte[] value)
        //{
        //    return _dbUnitOfWork.SequentialInsert(value);
        //}

        //public byte[] SequentialUpdate(byte[] value)
        //{
        //    return _dbUnitOfWork.SequentialUpdate(value);
        //}

        //public byte[] SequentialDelete(byte[] value)
        //{
        //    return _dbUnitOfWork.SequentialDelete(value);
        //}

        //public byte[] SequentialExecuteNoQuery(byte[] value)
        //{
        //    return _dbUnitOfWork.SequentialExecuteNoQuery(value);
        //}

        //public byte[] SequentialExecuteScalar(byte[] value)
        //{
        //    return _dbUnitOfWork.SequentialExecuteScalar(value);
        //}

        //public byte[] SequentialExecuteReader(byte[] value)
        //{
        //    return _dbUnitOfWork.SequentialExecuteReader(value);
        //}

        //public byte[] SequentialExecuteProcedure(byte[] value)
        //{
        //    return _dbUnitOfWork.SequentialExecuteProcedure(value);
        //}

        //public byte[] SequentialAdapterGet(byte[] value)
        //{
        //    return _dbUnitOfWork.SequentialGet(value);
        //}

        //public byte[] SequentialAdapterSet(byte[] value)
        //{
        //    return _dbUnitOfWork.SequentialSet(value);
        //}
        public void BeginSequentialSelect(byte[] value)
        {
            _dbUnitOfWork.SequentialSelectAsync(value);
        }
        public void BeginSequentialInsert(byte[] value)
        {
            _dbUnitOfWork.SequentialInsertAsync(value);
        }

        public void BeginSequentialUpdate(byte[] value)
        {
            _dbUnitOfWork.SequentialUpdateAsync(value);
        }

        public void BeginSequentialDelete(byte[] value)
        {
            _dbUnitOfWork.SequentialDeleteAsync(value);
        }

        public void BeginSequentialExecuteNoQuery(byte[] value)
        {
            _dbUnitOfWork.SequentialExecuteNoQueryAsync(value);
        }

        public void BeginSequentialExecuteScalar(byte[] value)
        {
            _dbUnitOfWork.SequentialExecuteScalarAsync(value);
        }

        public void BeginSequentialExecuteReader(byte[] value)
        {
            _dbUnitOfWork.SequentialExecuteReaderAsync(value);
        }

        public void BeginSequentialExecuteProcedure(byte[] value)
        {
            _dbUnitOfWork.SequentialExecuteProcedureAsync(value);
        }

        public void BeginSequentialAdapterGet(byte[] value)
        {
            _dbUnitOfWork.SequentialGetAsync(value);
        }

        public void BeginSequentialAdapterSet(byte[] value)
        {
            _dbUnitOfWork.SequentialSetAsync(value);
        }
        #endregion
        private void SequentialCallback(object obj)
        {
            var result = (Tuple<bool, object>)obj;
            OperationContext.Current.GetCallbackChannel<IServiceCallback>().ServiceCallback(new ServiceResult(result.Item1, result.Item2).Compression());
        }
    }
}
