using Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dependency
{
    /// <summary>
    /// IoC内核类
    /// </summary>
    public abstract class IoCKernel : IIoCKernel
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

        public IIoCKernel To<U>() where U : class
        {
            var type = typeof(U);
            if(type.BaseType == _baseType || type.GetInterface(_baseType.Name) != null)
            {
                IoCContext.Context.DIManager.AddTypeInfo(_baseType, type);
            }
            return this;
        }
        public V Resolve<V>() where V : class
        {
            return IoCContext.Context.DITypeAnalyticalProvider.CreateDITypeAnalaytical().GetValue<V>();
        }
    }
}
