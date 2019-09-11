using Core.Helper;
using Core.Infrastructure;
using System;
using WCFService.Entity;

namespace WCFService.Service
{
    /// <summary>
    /// WCF服务类
    /// </summary>
    public class DataExchangeService : IService
    {
        private readonly DbFactoryImpl _dbFactory;
        public DataExchangeService()
        {
            var kernel = new IoCKernelImpl();
            _dbFactory = kernel.Resolve<DbFactoryImpl>();
        }
        
        public byte[] Select(byte[] value)
        {
            var receiveData = value.Decompress<ReceiveData>();
            var id = _dbFactory.Get(receiveData.UserId, receiveData.SqlText, receiveData.Param.ToContextParam());
            return _dbFactory.Result(id).Compression();
        }
        public byte[] SelectAsync(byte[] value)
        {
            var receiveData = value.Decompress<ReceiveData>();
            var id = _dbFactory.Get(receiveData.UserId, receiveData.SqlText, receiveData.Param.ToContextParam());
            return id.Compression();
        }
        public byte[] Insert(byte[] value)
        {
            var receiveData = value.Decompress<ReceiveData>();
            var id = _dbFactory.Insert(receiveData.UserId, receiveData.SqlText, receiveData.Param.ToContextParam());
            return _dbFactory.Result(id).Compression();
        }
        public byte[] InsertAsync(byte[] value)
        {
            var receiveData = value.Decompress<ReceiveData>();
            var id = _dbFactory.Insert(receiveData.UserId, receiveData.SqlText, receiveData.Param.ToContextParam());
            return id.Compression();
        }
        public byte[] Update(byte[] value)
        {
            var receiveData = value.Decompress<ReceiveData>();
            var id = _dbFactory.Update(receiveData.UserId, receiveData.SqlText, receiveData.Param.ToContextParam());
            return _dbFactory.Result(id).Compression();
        }
        public byte[] UpdateAsync(byte[] value)
        {
            var receiveData = value.Decompress<ReceiveData>();
            var id = _dbFactory.Update(receiveData.UserId, receiveData.SqlText, receiveData.Param.ToContextParam());
            return id.Compression();
        }
        public byte[] Delete(byte[] value)
        {
            var receiveData = value.Decompress<ReceiveData>();
            var id = _dbFactory.Delete(receiveData.UserId, receiveData.SqlText, receiveData.Param.ToContextParam());
            return _dbFactory.Result(id).Compression();
        }
        public byte[] DeleteAsync(byte[] value)
        {
            var receiveData = value.Decompress<ReceiveData>();
            var id = _dbFactory.Delete(receiveData.UserId, receiveData.SqlText, receiveData.Param.ToContextParam());
            return id.Compression();
        }
        public byte[] ExecuteNoQuery(byte[] value)
        {
            var receiveData = value.Decompress<ReceiveData>();
            var id = _dbFactory.ExecuteNoQuery(receiveData.UserId, receiveData.SqlText, receiveData.Param.ToContextParam());
            return _dbFactory.Result(id).Compression();
        }
        public byte[] ExecuteNoQueryAsync(byte[] value)
        {
            var receiveData = value.Decompress<ReceiveData>();
            var id = _dbFactory.ExecuteNoQuery(receiveData.UserId, receiveData.SqlText, receiveData.Param.ToContextParam());
            return id.Compression();
        }
        public byte[] ExecuteProcedure(byte[] value)
        {
            var receiveData = value.Decompress<ReceiveData>();
            var id = _dbFactory.ExecuteProcedure(receiveData.UserId, receiveData.SqlText, receiveData.Param.ToContextParam());
            return _dbFactory.Result(id).Compression();
        }
        public byte[] ExecuteProcedureAsync(byte[] value)
        {
            var receiveData = value.Decompress<ReceiveData>();
            var id = _dbFactory.ExecuteProcedure(receiveData.UserId, receiveData.SqlText, receiveData.Param.ToContextParam());
            return id.Compression();
        }
        public byte[] ExecuteReader(byte[] value)
        {
            var receiveData = value.Decompress<ReceiveData>();
            var id = _dbFactory.ExecuteReader(receiveData.UserId, receiveData.SqlText, receiveData.Param.ToContextParam());
            return _dbFactory.Result(id).Compression();
        }
        public byte[] ExecuteReaderAsync(byte[] value)
        {
            var receiveData = value.Decompress<ReceiveData>();
            var id = _dbFactory.ExecuteReader(receiveData.UserId, receiveData.SqlText, receiveData.Param.ToContextParam());
            return id.Compression();
        }
        public byte[] ExecuteScalar(byte[] value)
        {
            var receiveData = value.Decompress<ReceiveData>();
            var id = _dbFactory.ExecuteScalar(receiveData.UserId, receiveData.SqlText, receiveData.Param.ToContextParam());
            return _dbFactory.Result(id).Compression();
        }
        public byte[] ExecuteScalarAsync(byte[] value)
        {
            var receiveData = value.Decompress<ReceiveData>();
            var id = _dbFactory.ExecuteScalar(receiveData.UserId, receiveData.SqlText, receiveData.Param.ToContextParam());
            return id.Compression();
        }
        public byte[] AdapterGet(byte[] value)
        {
            var receiveData = value.Decompress<ReceiveData>();
            var id = _dbFactory.Get(receiveData.UserId, receiveData.SqlText);
            return _dbFactory.Result(id).Compression();
        }
        public byte[] AdapterGetAsync(byte[] value)
        {
            var receiveData = value.Decompress<ReceiveData>();
            var id = _dbFactory.Get(receiveData.UserId, receiveData.SqlText);
            return id.Compression();
        }
        public byte[] AdapterSet(byte[] value)
        {
            var receiveData = value.Decompress<ReceiveData>();
            var id = _dbFactory.Set(receiveData.UserId, receiveData.SqlText, receiveData.DataSet);
            return _dbFactory.Result(id).Compression();
        }
        public byte[] AdapterSetAsync(byte[] value)
        {
            var receiveData = value.Decompress<ReceiveData>();
            var id = _dbFactory.Set(receiveData.UserId, receiveData.SqlText, receiveData.DataSet);
            return id.Compression();
        }
        public byte[] Result(byte[] id)
        {
            return _dbFactory.Result(id.Decompress<Guid>()).Compression();
        }
    }
}
