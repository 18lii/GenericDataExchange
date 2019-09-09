using Core.Entities;
using Core.Interface;
using Database.Interface;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        byte[] ExecuteNoQuery(byte[] stream);
        [OperationContract]
        byte[] Result(byte[] stream);
        [OperationContract]
        byte[] GetData(int value);
        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: 在此添加您的服务操作
    }

    // 使用下面示例中说明的数据约定将复合类型添加到服务操作。
    // 可以将 XSD 文件添加到项目中。在生成项目后，可以通过命名空间“WCFService.ContractType”直接使用其中定义的数据类型。
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
    [DataContract]
    public class GenericEventArgs<T> : IGenericEventArg<T>
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string PeristalticName { get; set; }
        [DataMember]
        public string UserId { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public T Item { get; set; }
        [DataMember]
        public object AttachData { get; set; }
    }
    [DataContract]
    public class FactoryContext : IFactoryContext
    {
        [DataMember]
        public DbOperate DbOperate { get; set; }
        [DataMember]
        public ConcurrentBag<ConcurrentDictionary<string, object>> Params { get; set; }
        [DataMember]
        public string SqlText { get; set; }

        public event Action<IGenericResult> ModifyEvent;

        public void OnModifyEvent(IGenericResult e)
        {
            ModifyEvent.Invoke(e);
        }
    }
    [DataContract]
    public class EntryContext : IEntryContext
    {
        [DataMember]
        public ITimespan Timespan { get; set; }
        [DataMember]
        public int Count { get; set; }
    }
}
