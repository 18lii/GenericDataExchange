using AdvancedDependencyContainer.Event;
using AdvancedDependencyContainer.Helper;
using System;

namespace AdvancedDependencyContainer.ContainerUnity
{
    /// <summary>
    /// 依赖注入全局静态类，控制反转
    /// </summary>
    public static class DependencyKernel
    {
        /// <summary>
        /// 通过IoC容器获取<see cref="{T}"/> 类型实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static T Resolve<T>(object parameter = null) where T : class => typeof(T).OnResolveEvent(parameter).CastTo<T>();
        /// <summary>
        /// 通过IoC容器获取指定<see cref="Type"/>类型的实例
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static object Resolve(Type type, object parameter = null) => type.OnResolveEvent(parameter);
    }
}
