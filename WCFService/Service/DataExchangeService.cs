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
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
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
            var result = (Tuple<bool, object>)_dbUnitOfWork.Result(_dbUnitOfWork.Get(receiveData.SqlText[0], receiveData.Param[0], receiveData.sequence));
            if (result.Item1)
            {
                return new ServiceResult(result.Item1, "操作成功", result.Item2).Compression();
            }
            else
            {
                return new ServiceResult(result.Item1, "操作失败", (string)result.Item2).Compression();
            }
        }
        public void BeginSelect(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = (Tuple<bool, object>)_dbUnitOfWork.Result(_dbUnitOfWork.Get(receiveData.SqlText[0], receiveData.Param[0], receiveData.sequence));
            callback.ServiceCallback(result.Compression());
        }
        public byte[] Insert(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = (Tuple<bool, object>)_dbUnitOfWork.Result(_dbUnitOfWork.Insert(receiveData.SqlText, receiveData.Param, receiveData.sequence));
            if (result.Item1)
            {
                return new ServiceResult(result.Item1, "操作成功", result.Item2).Compression();
            }
            else
            {
                return new ServiceResult(result.Item1, "操作失败", (string)result.Item2).Compression();
            }
        }
        public void BeginInsert(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = (Tuple<bool, object>)_dbUnitOfWork.Result(_dbUnitOfWork.Insert(receiveData.SqlText, receiveData.Param, receiveData.sequence));
            callback.ServiceCallback(result.Compression());
        }
        public byte[] Update(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = (Tuple<bool, object>)_dbUnitOfWork.Result(_dbUnitOfWork.Update(receiveData.SqlText, receiveData.Param, receiveData.sequence));
            if (result.Item1)
            {
                return new ServiceResult(result.Item1, "操作成功", result.Item2).Compression();
            }
            else
            {
                return new ServiceResult(result.Item1, "操作失败", (string)result.Item2).Compression();
            }
        }
        public void BeginUpdate(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = (Tuple<bool, object>)_dbUnitOfWork.Result(_dbUnitOfWork.Update(receiveData.SqlText, receiveData.Param, receiveData.sequence));
            callback.ServiceCallback(result.Compression());
        }
        public byte[] Delete(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = (Tuple<bool, object>)_dbUnitOfWork.Result(_dbUnitOfWork.Delete(receiveData.SqlText, receiveData.Param, receiveData.sequence));
            if (result.Item1)
            {
                return new ServiceResult(result.Item1, "操作成功", result.Item2).Compression();
            }
            else
            {
                return new ServiceResult(result.Item1, "操作失败", (string)result.Item2).Compression();
            }
        }
        public void BeginDelete(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = (Tuple<bool, object>)_dbUnitOfWork.Result(_dbUnitOfWork.Delete(receiveData.SqlText, receiveData.Param, receiveData.sequence));
            callback.ServiceCallback(result.Compression());
        }
        public byte[] ExecuteNoQuery(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = (Tuple<bool, object>)_dbUnitOfWork.Result(_dbUnitOfWork.ExecuteNoQuery(receiveData.SqlText, receiveData.Param, receiveData.sequence));
            if (result.Item1)
            {
                return new ServiceResult(result.Item1, "操作成功", result.Item2).Compression();
            }
            else
            {
                return new ServiceResult(result.Item1, "操作失败", (string)result.Item2).Compression();
            }
        }
        public void BeginExecuteNoQuery(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = (Tuple<bool, object>)_dbUnitOfWork.Result(_dbUnitOfWork.ExecuteNoQuery(receiveData.SqlText, receiveData.Param, receiveData.sequence));
            callback.ServiceCallback(result.Compression());
        }
        public byte[] ExecuteProcedure(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = (Tuple<bool, object>)_dbUnitOfWork.Result(_dbUnitOfWork.ExecuteProcedure(receiveData.SqlText[0], receiveData.Param[0], receiveData.sequence));
            if (result.Item1)
            {
                return new ServiceResult(result.Item1, "操作成功", result.Item2).Compression();
            }
            else
            {
                return new ServiceResult(result.Item1, "操作失败", (string)result.Item2).Compression();
            }
        }
        public void BeginExecuteProcedure(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = (Tuple<bool, object>)_dbUnitOfWork.Result(_dbUnitOfWork.ExecuteNoQuery(receiveData.SqlText, receiveData.Param, receiveData.sequence));
            callback.ServiceCallback(result.Compression());
        }
        public byte[] ExecuteReader(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = (Tuple<bool, object>)_dbUnitOfWork.Result(_dbUnitOfWork.ExecuteReader(receiveData.SqlText[0], receiveData.Param[0], receiveData.sequence));
            if (result.Item1)
            {
                return new ServiceResult(result.Item1, "操作成功", result.Item2).Compression();
            }
            else
            {
                return new ServiceResult(result.Item1, "操作失败", (string)result.Item2).Compression();
            }
        }
        public void BeginExecuteReader(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = (Tuple<bool, object>)_dbUnitOfWork.Result(_dbUnitOfWork.ExecuteReader(receiveData.SqlText[0], receiveData.Param[0], receiveData.sequence));
            callback.ServiceCallback(result.Compression());
        }
        public byte[] ExecuteScalar(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = (Tuple<bool, object>)_dbUnitOfWork.Result(_dbUnitOfWork.ExecuteScalar(receiveData.SqlText[0], receiveData.Param[0], receiveData.sequence));
            if (result.Item1)
            {
                return new ServiceResult(result.Item1, "操作成功", result.Item2).Compression();
            }
            else
            {
                return new ServiceResult(result.Item1, "操作失败", (string)result.Item2).Compression();
            }
        }
        public void BeginExecuteScalar(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = (Tuple<bool, object>)_dbUnitOfWork.Result(_dbUnitOfWork.ExecuteScalar(receiveData.SqlText[0], receiveData.Param[0], receiveData.sequence));
            callback.ServiceCallback(result.Compression());
        }
        public byte[] AdapterGet(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = (Tuple<bool, object>)_dbUnitOfWork.Result(_dbUnitOfWork.Get(receiveData.SqlText[0], receiveData.Param[0], receiveData.sequence));
            if (result.Item1)
            {
                return new ServiceResult(result.Item1, "操作成功", result.Item2).Compression();
            }
            else
            {
                return new ServiceResult(result.Item1, "操作失败", (string)result.Item2).Compression();
            }
        }
        public void BeginAdapterGet(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = (Tuple<bool, object>)_dbUnitOfWork.Result(_dbUnitOfWork.Get(receiveData.SqlText[0], receiveData.Param[0], receiveData.sequence));
            callback.ServiceCallback(result.Compression());
        }
        public byte[] AdapterSet(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = (Tuple<bool, object>)_dbUnitOfWork.Result(_dbUnitOfWork.Set(receiveData.SqlText, receiveData.DataSet, receiveData.sequence));
            if (result.Item1)
            {
                return new ServiceResult(result.Item1, "操作成功", result.Item2).Compression();
            }
            else
            {
                return new ServiceResult(result.Item1, "操作失败", (string)result.Item2).Compression();
            }
        }
        public void BeginAdapterSet(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = (Tuple<bool, object>)_dbUnitOfWork.Result(_dbUnitOfWork.Set(receiveData.SqlText, receiveData.DataSet, receiveData.sequence));
            callback.ServiceCallback(result.Compression());
        }
    }
}
