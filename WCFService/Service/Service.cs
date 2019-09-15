using Core.Helper;
using Core.Infrastructure;
using System;
using System.Threading.Tasks;
using TransparentAgent.Contract;
using TransparentAgent.Interface;
using WCFService.Entity;

namespace WCFService.Service
{
    /// <summary>
    /// WCF服务类
    /// </summary>
    public class DataExchangeService : IService
    {
        private readonly DbUnitOfWork _dbFactory;
        
        public byte[] Select(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = (Tuple<bool, object>)_dbFactory.Result(_dbFactory.Get(receiveData.SqlText[0], receiveData.Param[0], receiveData.sequence)).Result;
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
            var id = _dbFactory.Get(receiveData.SqlText[0], receiveData.Param[0], receiveData.sequence);
            return id.Compression();
        }
        public byte[] Insert(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = (Tuple<bool, object>)_dbFactory.Result(_dbFactory.Insert(receiveData.SqlText, receiveData.Param, receiveData.sequence)).Result;
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
            var id = _dbFactory.Insert(receiveData.SqlText, receiveData.Param, receiveData.sequence);
            return id.Compression();
        }
        public byte[] Update(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = (Tuple<bool, object>)_dbFactory.Result(_dbFactory.Insert(receiveData.SqlText, receiveData.Param, receiveData.sequence)).Result;
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
            var id = _dbFactory.Update(receiveData.SqlText, receiveData.Param, receiveData.sequence);
            return id.Compression();
        }
        public byte[] Delete(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = (Tuple<bool, object>)_dbFactory.Result(_dbFactory.Update(receiveData.SqlText, receiveData.Param, receiveData.sequence)).Result;
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
            var id = _dbFactory.Delete(receiveData.SqlText, receiveData.Param, receiveData.sequence);
            return id.Compression();
        }
        public byte[] ExecuteNoQuery(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = (Tuple<bool, object>)_dbFactory.Result(_dbFactory.Delete(receiveData.SqlText, receiveData.Param, receiveData.sequence)).Result;
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
            var id = _dbFactory.ExecuteNoQuery(receiveData.SqlText, receiveData.Param, receiveData.sequence);
            return id.Compression();
        }
        public byte[] ExecuteProcedure(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = (Tuple<bool, object>)_dbFactory.Result(_dbFactory.ExecuteProcedure(receiveData.SqlText[0], receiveData.Param[0], receiveData.sequence)).Result;
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
            var id = _dbFactory.ExecuteProcedure(receiveData.SqlText[0], receiveData.Param[0], receiveData.sequence);
            return id.Compression();
        }
        public byte[] ExecuteReader(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = (Tuple<bool, object>)_dbFactory.Result(_dbFactory.ExecuteProcedure(receiveData.SqlText[0], receiveData.Param[0], receiveData.sequence)).Result;
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
            var id = _dbFactory.ExecuteReader(receiveData.SqlText[0], receiveData.Param[0], receiveData.sequence);
            return id.Compression();
        }
        public byte[] ExecuteScalar(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = (Tuple<bool, object>)_dbFactory.Result(_dbFactory.ExecuteScalar(receiveData.SqlText[0], receiveData.Param[0], receiveData.sequence)).Result;
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
            var id = _dbFactory.ExecuteScalar(receiveData.SqlText[0], receiveData.Param[0], receiveData.sequence);
            return id.Compression();
        }
        public byte[] AdapterGet(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = (Tuple<bool, object>)_dbFactory.Result(_dbFactory.Get(receiveData.SqlText[0], receiveData.Param[0], receiveData.sequence)).Result;
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
            var id = _dbFactory.Get(receiveData.SqlText[0], receiveData.Param[0], receiveData.sequence);
            return id.Compression();
        }
        public byte[] AdapterSet(byte[] value)
        {
            var receiveData = value.Decompress<IContractData>();
            var result = (Tuple<bool, object>)_dbFactory.Result(_dbFactory.Set(receiveData.SqlText, receiveData.DataSet, receiveData.sequence)).Result;
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
            var id = _dbFactory.Set(receiveData.SqlText, receiveData.DataSet, receiveData.sequence);
            return id.Compression();
        }
        public byte[] Result(byte[] id)
        {
            return _dbFactory.Result(id.Decompress<Guid>()).Compression();
        }
    }
}
