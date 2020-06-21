
using AdvancedDependencyContainer.Dependency;
using AdvancedDependencyContainer.Entities;
using System;

namespace AdvancedDependencyContainer.Interface
{
    /*
     * 依赖组件绑定上下文接口，可编码绑定&配置文件绑定，
     * 配置文件绑定分为app.config文件自定义节配置与XML文件配置
     * 使用配置文件方式时，务必确保按指定结构进行编写，否则容易造成程序无法启动或崩溃
     * 
     * ******Ojbect Create By Shine Lee 2019-09-17******
     * 
     */
    /// <summary>
    /// 依赖绑定上下文接口
    /// </summary>
    public interface IDependencyBindContext
    {
        /// <summary>
        /// 依赖绑定初始化方法，参数<see cref="string"/> sectionName为app.config配置文件中的自定义section节
        /// <para>！！！若未正确配置，请勿使用，否则可能引起程序崩溃！！！</para>
        /// </summary>
        /// <param name="option"></param>
        /// <param name="keyWord"></param>
        void UseConfiguration(DependencyConfigurationOption option, string keyWord);
        /// <summary>
        /// 泛型绑定，以<see cref="{T}"/>类型作为依赖注入契约
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IDependencyBindContext Bind<T>();
        /// <summary>
        /// 类型绑定，以<see cref="Type"/>类型作为注入契约
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IDependencyBindContext Bind(Type type);
        /// <summary>
        /// 泛型绑定，以<see cref="{U}"/>类型作为依赖注入实现
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <returns></returns>
        IDependencyBindContext To<U>(object[] args = null) where U : class;
        /// <summary>
        /// 类型绑定，以<see cref="Type"/>类型作为依赖注入实现
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IDependencyBindContext To(Type type, object[] args = null);
    }
}
