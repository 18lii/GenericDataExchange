
using System;

namespace AdvancedDependencyContainer.Interface
{
    /// <summary>
    /// IoC内核接口，含绑定方法与注入方法
    /// </summary>
    public interface IIoCKernel
    {
        /// <summary>
        /// 泛型绑定，以<see cref="{T}"/>类型作为依赖注入契约
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IIoCKernel Bind<T>();
        /// <summary>
        /// 类型绑定，以<see cref="Type"/>类型作为注入契约
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IIoCKernel Bind(Type type);
        /// <summary>
        /// 泛型绑定，以<see cref="{U}"/>类型作为依赖注入实现
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <returns></returns>
        IIoCKernel To<U>() where U : class;
        /// <summary>
        /// 类型绑定，以<see cref="Type"/>类型作为依赖注入实现
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IIoCKernel To(Type type);
    }
}
