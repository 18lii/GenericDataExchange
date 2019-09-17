using Core.Infrastructure;
using DatabaseUnitOfWork;
using System;
using System.Diagnostics;
using System.ServiceModel;
using TransparentAgent.Contract;
using TransparentAgent.Interface;

namespace WCFService.Service
{
    /// <summary>
    /// WCF服务类
    /// </summary>
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.PerCall)]
    public class DataExchangeService : IService
    {
        private readonly IUnitOfWork _db = new UnitOfwork();
        private readonly ICallback callback = OperationContext.Current.GetCallbackChannel<ICallback>();
        public byte[] Select(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = (Tuple<bool, object>)_db.Result(_db.Get(receiveData.SqlText[0], receiveData.Param[0], receiveData.sequence));
            if (result.Item1)
            {
                return new ServiceResult(result.Item1, "操作成功", result.Item2).Compression();
            }
            else
            {
                return new ServiceResult(result.Item1, "操作失败", (string)result.Item2).Compression();
            }
        }
        public byte[] SelectAsync(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var id = _db.Get(receiveData.SqlText[0], receiveData.Param[0], receiveData.sequence);
            return id.Compression();
        }
        public byte[] Insert(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = (Tuple<bool, object>)_db.Result(_db.Insert(receiveData.SqlText, receiveData.Param, receiveData.sequence));
            if (result.Item1)
            {
                return new ServiceResult(result.Item1, "操作成功", result.Item2).Compression();
            }
            else
            {
                return new ServiceResult(result.Item1, "操作失败", (string)result.Item2).Compression();
            }
        }
        public byte[] InsertAsync(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var id = _db.Insert(receiveData.SqlText, receiveData.Param, receiveData.sequence);
            return id.Compression();
        }
        public byte[] Update(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = (Tuple<bool, object>)_db.Result(_db.Update(receiveData.SqlText, receiveData.Param, receiveData.sequence));
            if (result.Item1)
            {
                return new ServiceResult(result.Item1, "操作成功", result.Item2).Compression();
            }
            else
            {
                return new ServiceResult(result.Item1, "操作失败", (string)result.Item2).Compression();
            }
        }
        public byte[] UpdateAsync(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var id = _db.Update(receiveData.SqlText, receiveData.Param, receiveData.sequence);
            return id.Compression();
        }
        public byte[] Delete(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = (Tuple<bool, object>)_db.Result(_db.Delete(receiveData.SqlText, receiveData.Param, receiveData.sequence));
            if (result.Item1)
            {
                return new ServiceResult(result.Item1, "操作成功", result.Item2).Compression();
            }
            else
            {
                return new ServiceResult(result.Item1, "操作失败", (string)result.Item2).Compression();
            }
        }
        public byte[] DeleteAsync(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var id = _db.Delete(receiveData.SqlText, receiveData.Param, receiveData.sequence);
            return id.Compression();
        }
        public byte[] ExecuteNoQuery(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = (Tuple<bool, object>)_db.Result(_db.ExecuteNoQuery(receiveData.SqlText, receiveData.Param, receiveData.sequence));
            if (result.Item1)
            {
                return new ServiceResult(result.Item1, "操作成功", result.Item2).Compression();
            }
            else
            {
                return new ServiceResult(result.Item1, "操作失败", (string)result.Item2).Compression();
            }
        }
        public byte[] ExecuteNoQueryAsync(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var id = _db.ExecuteNoQuery(receiveData.SqlText, receiveData.Param, receiveData.sequence);
            return id.Compression();
        }
        public byte[] ExecuteProcedure(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = (Tuple<bool, object>)_db.Result(_db.ExecuteProcedure(receiveData.SqlText[0], receiveData.Param[0], receiveData.sequence));
            if (result.Item1)
            {
                return new ServiceResult(result.Item1, "操作成功", result.Item2).Compression();
            }
            else
            {
                return new ServiceResult(result.Item1, "操作失败", (string)result.Item2).Compression();
            }
        }
        public byte[] ExecuteProcedureAsync(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var id = _db.ExecuteProcedure(receiveData.SqlText[0], receiveData.Param[0], receiveData.sequence);
            return id.Compression();
        }
        public byte[] ExecuteReader(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = (Tuple<bool, object>)_db.Result(_db.ExecuteReader(receiveData.SqlText[0], receiveData.Param[0], receiveData.sequence));
            if (result.Item1)
            {
                return new ServiceResult(result.Item1, "操作成功", result.Item2).Compression();
            }
            else
            {
                return new ServiceResult(result.Item1, "操作失败", (string)result.Item2).Compression();
            }
        }
        public byte[] ExecuteReaderAsync(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var id = _db.ExecuteReader(receiveData.SqlText[0], receiveData.Param[0], receiveData.sequence);
            return id.Compression();
        }
        public byte[] ExecuteScalar(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = (Tuple<bool, object>)_db.Result(_db.ExecuteScalar(receiveData.SqlText[0], receiveData.Param[0], receiveData.sequence));
            if (result.Item1)
            {
                return new ServiceResult(result.Item1, "操作成功", result.Item2).Compression();
            }
            else
            {
                return new ServiceResult(result.Item1, "操作失败", (string)result.Item2).Compression();
            }
        }
        public byte[] ExecuteScalarAsync(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var id = _db.ExecuteScalar(receiveData.SqlText[0], receiveData.Param[0], receiveData.sequence);
            return id.Compression();
        }
        public byte[] AdapterGet(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = (Tuple<bool, object>)_db.Result(_db.Get(receiveData.SqlText[0], receiveData.Param[0], receiveData.sequence));
            if (result.Item1)
            {
                return new ServiceResult(result.Item1, "操作成功", result.Item2).Compression();
            }
            else
            {
                return new ServiceResult(result.Item1, "操作失败", (string)result.Item2).Compression();
            }
        }
        public byte[] AdapterGetAsync(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var id = _db.Get(receiveData.SqlText[0], receiveData.Param[0], receiveData.sequence);
            return id.Compression();
        }
        public byte[] AdapterSet(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = (Tuple<bool, object>)_db.Result(_db.Set(receiveData.SqlText, receiveData.DataSet, receiveData.sequence));
            if (result.Item1)
            {
                return new ServiceResult(result.Item1, "操作成功", result.Item2).Compression();
            }
            else
            {
                return new ServiceResult(result.Item1, "操作失败", (string)result.Item2).Compression();
            }
        }
        public byte[] AdapterSetAsync(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var id = _db.Set(receiveData.SqlText, receiveData.DataSet, receiveData.sequence);
            return id.Compression();
        }
        public void ResultAsync(byte[] id)
        {
            Debug.WriteLine("返回服务已被调用");
            _db.ResultAsync(id.Decompress<Guid>(), ac =>
            {
                Debug.WriteLine("回调服务已被调用");
                callback.Reuslt(((Tuple<bool, object>)(((Func<Guid, object>)ac.AsyncState).EndInvoke(ac))).Compression());
            });
        }
    }
}
