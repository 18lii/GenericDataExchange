using AdvancedDependencyContainer.Interface;
using System;

namespace AdvancedDependencyContainer.Dependency
{
    /// <summary>
    /// IoC内核类，依赖组件绑定与实例获取
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
        public IIoCKernel To<U>(object[] args = null) where U : class
        {
            var type = typeof(U);
            if(type.BaseType == _baseType || type.GetInterface(_baseType.Name) != null)
            {
                IoCContext.Context.DIManager.AddTypeInfo(_baseType, type, args);
            }
            return this;
        }
        public IIoCKernel To(Type type, object[] args = null)
        {
            if(type.BaseType == _baseType || type.GetInterface(_baseType.Name) != null)
            {
                IoCContext.Context.DIManager.AddTypeInfo(_baseType, type, args);
            }
            return this;
        }
        public V Resolve<V>() where V : class
        {
            return IoCContext.Context.DITypeAnalyticalProvider.CreateDITypeAnalaytical().GetValue<V>();
        }
        public object Resolve(Type type)
        {
            return IoCContext.Context.DITypeAnalyticalProvider.CreateDITypeAnalaytical().GetValue(type);
        }
    }
}
