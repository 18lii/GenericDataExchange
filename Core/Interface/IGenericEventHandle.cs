using System;

namespace Core.Interface
{
    /// <summary>
    /// 全局事件接口
    /// </summary>
    public interface IGenericEventHandle
    {
        /// <summary>
        /// 入列事件注册
        /// </summary>
        /// <param name="m"></param>
        void Register(Action<object> m);
        /// <summary>
        /// 入列事件触发
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        bool OnQueueEvent(object o);
        /// <summary>
        /// 返回事件注册
        /// </summary>
        /// <param name="m"></param>
        void Register(Func<Guid, IGenericResult> m);
        /// <summary>
        /// 返回事件触发
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IGenericResult OnResultEvent(Guid id);
    }
    public interface IGenericEventHandle<T>
    {
        IGenericEventHandle<T> Register(Action<T> mehtod);
        void OnQueueEvent(T t);
        void OnQueueEventAsync(T t, AsyncCallback c);
    }
    public interface IGenericEventHandle<T, R>
    {
        IGenericEventHandle<T, R> Register(Func<T, R> m);
        R OnQueueEvent(T o);
        void OnQueueEventAsync(T t, AsyncCallback c);
    }
}
