using Sequencer.Events;
using System;

namespace Sequencer.Interface
{
    /// <summary>
    /// 消息队列配置接口
    /// </summary>
    public interface IBindContext
    {
        /// <summary>
        /// 线程类型及处理方法绑定,绑定<see cref="T"/>类型队列，相同的队列标识只允许绑定一次，线程驻留；
        /// <para><see cref="string"/> name为队列标识；</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method"></param>
        /// <param name="def"></param>
        void Bind<T>(string name, Action<T> method, int def = 1);
        /// <summary>
        /// 线程类型及处理方法绑定,绑定<see cref="T"/>类型队列，相同的队列标识只允许绑定一次，线程驻留；
        /// <para><see cref="string"/> name为队列标识。</para>
        /// <para><see cref="AsyncCallback"/> callback为异步回调函数</para>
        /// <para><see cref="int"/> def表示此线程启动数量，大于1时处理模式为并行；</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="method"></param>
        /// <param name="callback"></param>
        /// <param name="def"></param>
        void BindAsync<T>(string name, Action<T> method, AsyncCallback callback, int def = 1);
        /// <summary>
        /// 线程类型及处理方法绑定,绑定<see cref="T"/>类型队列，相同的队列标识只允许绑定一次，线程驻留；
        /// <para><see cref="string"/> name为队列标识。</para>
        /// <para><see cref="int"/> def表示此线程启动数量，大于1时处理模式为并行；</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="name"></param>
        /// <param name="method"></param>
        /// <param name="def"></param>
        void Bind<T, TResult>(string n, Func<T, TResult> m, int d = 1);
        /// <summary>
        /// 线程类型及处理方法绑定,绑定<see cref="T"/>类型队列，相同的队列标识只允许绑定一次，线程驻留；
        /// <para><see cref="string"/> name为队列标识。</para>
        /// <para><see cref="AsyncCallback"/> callback为异步回调函数</para>
        /// <para><see cref="int"/> def表示此线程启动数量，大于1时处理模式为并行；</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="name"></param>
        /// <param name="method"></param>
        /// <param name="callback"></param>
        /// <param name="def"></param>
        void BindAsync<T, TResult>(string name, Func<T, TResult> method, AsyncCallback callback, int def = 1);
    }
}
