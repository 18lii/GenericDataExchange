using System.ServiceModel;
using System.Threading.Tasks;
using TransparentAgent.Interface;

namespace WCFService.Service
{
    /// <summary>
    /// 服务约定接口
    /// </summary>
    [ServiceContract(CallbackContract =typeof(IServiceCallback))]
    public interface IDataExchangeService
    {
        /// <summary>
        /// 查询方法，同步
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] Select(byte[] value);
        /// <summary>
        /// 查询方法，异步回调
        /// </summary>
        /// <param name="value"></param>
        [OperationContract]
        void BeginSelect(byte[] value);
        /// <summary>
        /// 插入方法，同步
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] Insert(byte[] value);
        /// <summary>
        /// 插入方法，异步回调
        /// </summary>
        /// <param name="value"></param>
        [OperationContract]
        void BeginInsert(byte[] value);
        /// <summary>
        /// 更新方法，同步
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] Update(byte[] value);
        /// <summary>
        /// 更新方法，异步回调
        /// </summary>
        /// <param name="value"></param>
        [OperationContract]
        void BeginUpdate(byte[] value);
        /// <summary>
        /// 删除方法，同步
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] Delete(byte[] value);
        /// <summary>
        /// 删除方法，异步回调
        /// </summary>
        /// <param name="value"></param>
        [OperationContract]
        void BeginDelete(byte[] value);
        /// <summary>
        /// 无结果集数据操作，同步
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] ExecuteNoQuery(byte[] value);
        /// <summary>
        /// 无结果集数据操作，异步回调
        /// </summary>
        /// <param name="value"></param>
        [OperationContract]
        void BeginExecuteNoQuery(byte[] value);
        /// <summary>
        /// 单结果查询，同步
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] ExecuteScalar(byte[] value);
        /// <summary>
        /// 单结果查询，异步回调
        /// </summary>
        /// <param name="value"></param>
        [OperationContract]
        void BeginExecuteScalar(byte[] value);
        /// <summary>
        /// 有结果集数据操作，同步
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] ExecuteReader(byte[] value);
        /// <summary>
        /// 有结果集数据操作，异步回调
        /// </summary>
        /// <param name="value"></param>
        [OperationContract]
        void BeginExecuteReader(byte[] value);
        /// <summary>
        /// 执行存储过程，同步
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] ExecuteProcedure(byte[] value);
        /// <summary>
        /// 执行存储过程，异步回调
        /// </summary>
        /// <param name="value"></param>
        [OperationContract]
        void BeginExecuteProcedure(byte[] value);
        /// <summary>
        /// 有结果集数据适配器查询，同步
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] AdapterGet(byte[] value);
        /// <summary>
        /// 有结果集数据适配器查询，异步回调
        /// </summary>
        /// <param name="value"></param>
        [OperationContract]
        void BeginAdapterGet(byte[] value);
        /// <summary>
        /// 无结果集数据适配器操作，同步
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] AdapterSet(byte[] value);
        /// <summary>
        /// 无结果集数据适配器操作，异步回调
        /// </summary>
        /// <param name="value"></param>
        [OperationContract]
        void BeginAdapterSet(byte[] value);
    }

    // 使用下面示例中说明的数据约定将复合类型添加到服务操作。
    // 可以将 XSD 文件添加到项目中。在生成项目后，可以通过命名空间“WCFService.ContractType”直接使用其中定义的数据类型。
}
