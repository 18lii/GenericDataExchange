using Core.Dependency;
using Core.Events;
using Core.Ignition;
using Core.Infrastructure;
using Core.Interface;
using Database.Interface;
using Queue;
using System;

namespace WCFService
{
    public class DataExchangeService : IService
    {
        public byte[] ExecuteNoQuery(byte[] stream)
        {
            var arg = new GenericEventArgs<IFactoryContext> { UserId = "1", Id = Guid.NewGuid(), Item = new FactoryContext { SqlText = null } };
            var esr = new EntryContext().Start(arg);
            return arg.Id.ToString().CompressionDataSet();
        }
        public byte[] Result(byte[] b)
        {
            return new byte[10];
            //return GenericEventHandle.OnResultEvent(id);
        }
        public byte[] GetData(int value)
        {
            return string.Format("连接成功: 您获取了{0}", value).CompressionDataSet();
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
    internal class IoCkernelImpl : IoCKernel { }
}
