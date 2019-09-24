using AdvancedDependencyContainer.Interface;
using AdvancedDependencyContainer.Event;
using System;

namespace AdvancedDependencyContainer.Dependency
{
    /// <summary>
    /// IoC内核类
    /// </summary>
    internal class IoCKernel : IIoCKernel
    {
        private Type _baseType;
        public IoCKernel()
        {
            IoCContext.Context.DIManager = new DIManager();
        }
        public IIoCKernel Bind<T>()
        {
            _baseType = typeof(T);
            return this;
        }
        public IIoCKernel Bind(Type type)
        {
            _baseType = type;
            return this;
        }
        public IIoCKernel To<U>() where U : class
        {
            var type = typeof(U);
            if(type.BaseType == _baseType || type.GetInterface(_baseType.Name) != null)
            {
                IoCContext.Context.DIManager.AddTypeInfo(_baseType, type);
            }
            return this;
        }
        public IIoCKernel To(Type type)
        {
            if(type.BaseType == _baseType || type.GetInterface(_baseType.Name) != null)
            {
                IoCContext.Context.DIManager.AddTypeInfo(_baseType, type);
            }
            return this;
        }
        public V Resolve<V>(object parameter = null) where V : class
        {
            return IoCContext.Context.DITypeAnalyticalProvider.CreateDITypeAnalaytical().GetValue<V>(parameter);
        }
        public object Resolve(Type type, object parameter = null)
        {
            return IoCContext.Context.DITypeAnalyticalProvider.CreateDITypeAnalaytical().GetValue(type, parameter);
        }
    }
}
