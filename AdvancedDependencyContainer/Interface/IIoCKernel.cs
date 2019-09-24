
using Database.Interface;
using System;

namespace AdvancedDependencyContainer.Interface
{
    /// <summary>
    /// IoC内核接口，含绑定方法与注入方法
    /// </summary>
    public interface IIoCKernel
    {
        IIoCKernel Bind<T>();
        IIoCKernel Bind(Type type);
        IIoCKernel To<U>() where U : class;
        IIoCKernel To(Type type);
        V Resolve<V>(object parameter = null) where V : class;
        object Resolve(Type type, object parameter = null);
    }
}
