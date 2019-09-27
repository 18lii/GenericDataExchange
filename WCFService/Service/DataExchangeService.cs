using System;
using System.ServiceModel;
using TransparentAgent.Contract;
using TransparentAgent.Interface;
using WCFService.Infrastructure;
using WCFService.Interface;

namespace WCFService.Service
{
    /// <summary>
    /// WCF服务类
    /// </summary>
    [IoCServiceBehavior]
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.PerCall)]
    public class DataExchangeService : IDataExchangeService
    {
        private readonly IDbUnitOfWork _dbUnitOfWork;
        public DataExchangeService(IDbUnitOfWork dbUnitOfWork)
        {
            _dbUnitOfWork = dbUnitOfWork;
        }
        private readonly IServiceCallback callback = OperationContext.Current.GetCallbackChannel<IServiceCallback>();
        public byte[] Select(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = _dbUnitOfWork.Select(receiveData.SqlText[0], receiveData.Param[0]);
            return new ServiceResult(result.Item1, result.Item2).Compression();
        }
        public void BeginSelect(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            _dbUnitOfWork.ResultAsync(_dbUnitOfWork.SequentialSelect(receiveData.SqlText[0], receiveData.Param[0], receiveData.sequence), ResultCallback);
        }

        public byte[] Insert(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = _dbUnitOfWork.Insert(receiveData.SqlText, receiveData.Param);
            return new ServiceResult(result.Item1, result.Item2).Compression();
        }
        public void BeginInsert(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            _dbUnitOfWork.ResultAsync(_dbUnitOfWork.SequentialInsert(receiveData.SqlText, receiveData.Param, receiveData.sequence), ResultCallback);
        }
        public byte[] Update(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = _dbUnitOfWork.Update(receiveData.SqlText, receiveData.Param);
            return new ServiceResult(result.Item1, result.Item2).Compression();
        }
        public void BeginUpdate(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            _dbUnitOfWork.ResultAsync(_dbUnitOfWork.SequentialUpdate(receiveData.SqlText, receiveData.Param, receiveData.sequence), ResultCallback);
        }
        public byte[] Delete(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = _dbUnitOfWork.Delete(receiveData.SqlText, receiveData.Param);
            return new ServiceResult(result.Item1, result.Item2).Compression();
        }
        public void BeginDelete(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            _dbUnitOfWork.ResultAsync(_dbUnitOfWork.SequentialDelete(receiveData.SqlText, receiveData.Param, receiveData.sequence), ResultCallback);
        }
        public byte[] ExecuteNoQuery(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = _dbUnitOfWork.ExecuteNoQuery(receiveData.SqlText, receiveData.Param);
            return new ServiceResult(result.Item1, result.Item2).Compression();
        }
        public void BeginExecuteNoQuery(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            _dbUnitOfWork.ResultAsync(_dbUnitOfWork.SequentialExecuteNoQuery(receiveData.SqlText, receiveData.Param, receiveData.sequence), ResultCallback);
        }
        public byte[] ExecuteProcedure(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = _dbUnitOfWork.ExecuteProcedure(receiveData.SqlText[0], receiveData.Param[0]);
            return new ServiceResult(result.Item1, result.Item2).Compression();
        }
        public void BeginExecuteProcedure(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            _dbUnitOfWork.ResultAsync(_dbUnitOfWork.SequentialExecuteProcedure(receiveData.SqlText[0], receiveData.Param[0], receiveData.sequence), ResultCallback);
        }
        public byte[] ExecuteReader(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = _dbUnitOfWork.ExecuteReader(receiveData.SqlText[0], receiveData.Param[0]);
            return new ServiceResult(result.Item1, result.Item2).Compression();
        }
        public void BeginExecuteReader(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            _dbUnitOfWork.ResultAsync(_dbUnitOfWork.SequentialExecuteReader(receiveData.SqlText[0], receiveData.Param[0], receiveData.sequence), ResultCallback);
        }
        public byte[] ExecuteScalar(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = _dbUnitOfWork.ExecuteScalar(receiveData.SqlText[0], receiveData.Param[0]);
            return new ServiceResult(result.Item1, result.Item2).Compression();
        }
        public void BeginExecuteScalar(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            _dbUnitOfWork.ResultAsync(_dbUnitOfWork.SequentialExecuteScalar(receiveData.SqlText[0], receiveData.Param[0], receiveData.sequence), ResultCallback);
        }
        public byte[] AdapterGet(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = _dbUnitOfWork.Get(receiveData.SqlText[0]);
            return new ServiceResult(result.Item1, result.Item2).Compression();
        }
        public void BeginAdapterGet(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            _dbUnitOfWork.ResultAsync(_dbUnitOfWork.SequentialGet(receiveData.SqlText[0], receiveData.sequence), ResultCallback);
        }
        public byte[] AdapterSet(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = _dbUnitOfWork.Set(receiveData.SqlText, receiveData.DataSet);
            return new ServiceResult(result.Item1, result.Item2).Compression();
        }
        public void BeginAdapterSet(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            _dbUnitOfWork.ResultAsync(_dbUnitOfWork.SequentialSet(receiveData.SqlText, receiveData.DataSet, receiveData.sequence), ResultCallback);
        }
        private void ResultCallback(IAsyncResult asyncResult)
        {
            var result = ((Func<Guid, Tuple<bool, object>>)asyncResult.AsyncState).EndInvoke(asyncResult);
            callback.ServiceCallback(new ServiceResult(result.Item1, result.Item2));
        }
    }
}
