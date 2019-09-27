using System.ServiceModel;
using TransparentAgent.Interface;

namespace WCFService.Interface
{
    public interface IServiceCallback
    {
        [OperationContract]
        void ServiceCallback(IServiceResult serviceResult);
    }
}
