using System.ServiceModel;

namespace WCFService.Interface
{
    public interface IServiceCallback
    {
        [OperationContract]
        void ServiceCallback(byte[] value);
    }
}
