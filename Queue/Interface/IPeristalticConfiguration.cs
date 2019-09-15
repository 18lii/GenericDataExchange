using System;

namespace Queue.Interface
{
    /// <summary>
    /// 线程配置接口，用于配置线程的对象类型及处理方法，在<see cref="Configuration"/>方法中配置以下项：
    /// <para><see cref="void"/> <see cref="IBindContext.Bind{T}(string, Action{T}, bool)"/>用于绑定无返回值处理方法；</para>
    /// <para><see cref="void"/> <see cref="IBindContext.BindAsync{T}(string, Action{T}, AsyncCallback, bool)"/>用于绑定带有异步回调函数，无返回值处理方法；</para>
    /// <para><see cref="void"/> <see cref="IBindContext.Bind{T, TResult}(string, Func{T, TResult}, bool)"/>
    /// 用于绑定有返回值的处理方法</para>
    /// <para><see cref="void"/> <see cref="IBindContext.BindAsync{T, TResult}(string, Func{T, TResult}, AsyncCallback, bool)"/>
    /// 用于绑定带有异步回调函数，有返回值的处理方法，返回值在异步回调中处理；</para>
    /// </summary>
    public interface IPeristalticConfiguration
    {
        /// <summary>
        /// 线程配置上下文
        /// </summary>
        IBindContext Context { get; set; }
        /// <summary>
        /// 线程配置方法
        /// 使用属性<see cref="IBindContext.Bind{T}()"/>方法或<see cref="IBindContext.BindAsync{T}()"/>进行线程绑定,
        /// 所有经绑定的线程均为驻留线程
        /// </summary>
        /// <returns></returns>
        IBindContext Configuration();
    }
}
