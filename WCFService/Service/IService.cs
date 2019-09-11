using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCFService.Service
{
    /// <summary>
    /// 服务约定接口
    /// </summary>
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        byte[] Select(byte[] value);
        [OperationContract]
        byte[] SelectAsync(byte[] value);
        [OperationContract]
        byte[] Insert(byte[] value);
        [OperationContract]
        byte[] InsertAsync(byte[] value);
        [OperationContract]
        byte[] Update(byte[] value);
        [OperationContract]
        byte[] UpdateAsync(byte[] value);
        [OperationContract]
        byte[] Delete(byte[] value);
        [OperationContract]
        byte[] DeleteAsync(byte[] value);
        [OperationContract]
        byte[] ExecuteNoQuery(byte[] value);
        [OperationContract]
        byte[] ExecuteNoQueryAsync(byte[] value);
        [OperationContract]
        byte[] ExecuteScalar(byte[] value);
        [OperationContract]
        byte[] ExecuteScalarAsync(byte[] value);
        [OperationContract]
        byte[] ExecuteReader(byte[] value);
        [OperationContract]
        byte[] ExecuteReaderAsync(byte[] value);
        [OperationContract]
        byte[] ExecuteProcedure(byte[] value);
        [OperationContract]
        byte[] ExecuteProcedureAsync(byte[] value);
        byte[] AdapterGet(byte[] value);
        byte[] AdapterGetAsync(byte[] value);
        byte[] AdapterSet(byte[] value);
        byte[] AdapterSetAsync(byte[] value);
        [OperationContract]
        byte[] Result(byte[] id);
    }

    // 使用下面示例中说明的数据约定将复合类型添加到服务操作。
    // 可以将 XSD 文件添加到项目中。在生成项目后，可以通过命名空间“WCFService.ContractType”直接使用其中定义的数据类型。
}
