using AdvancedDependencyContainer.ContainerUnity;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace WCFService.Infrastructure
{
    /// <summary>
    /// WCF构造注入提供程序
    /// </summary>
    internal class IoCServiceProvider : IInstanceProvider
    {
        private readonly Type _serviceType;
        public IoCServiceProvider(Type serviceType)
        {
            _serviceType = serviceType;
        }
        public object GetInstance(InstanceContext instanceContext)
        {
            return GetInstance(instanceContext, null);
        }

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return DependencyKernel.Resolve(_serviceType);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            if(instance is IDisposable)
            {
                ((IDisposable)instance).Dispose();
            }
        }
    }
}
