using Core.Interface;
using System;

namespace Core.Events
{
    /// <summary>
    /// 全局通用队列入列事件类
    /// </summary>
    public static class GenericEventHandle
    {
        private static event Action<object> GenericEvent;
        private static event Func<Guid, IGenericResult> GenericResultEvent;//绑定数据库提交事件
        public static void Register(Action<object> m)
        {
            GenericEvent += m;
        }
        public static bool OnQueueEvent(object o)
        {
            try
            {
                GenericEvent.Invoke(o);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public static void Register(Func<Guid, IGenericResult> m)
        {
            GenericResultEvent += m;
        }
        public static IGenericResult OnResultEvent(Guid id)
        {
            return GenericResultEvent.Invoke(id);
        }
    }
    /// <summary>
    /// 队列专用，事务处理事件类，无返回值事件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class GenericEventHandle<T> : IGenericEventHandle<T>
    {
        private event Action<T> GenericEvent;
        public IGenericEventHandle<T> Register(Action<T> m)
        {
            GenericEvent += m;
            return this;
        }
        public void OnQueueEvent(T t)
        {
            GenericEvent.Invoke(t);
        }
        public void OnQueueEventAsync(T t, AsyncCallback c)
        {
            GenericEvent.BeginInvoke(t, c, GenericEvent);
        }
    }
    /// <summary>
    /// 队列专用，事务处理事件类，有返回值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="R"></typeparam>
    internal class GenericEventHandle<T, R> : IGenericEventHandle<T, R>
    {
        private event Func<T, R> GenericEvent;
        public IGenericEventHandle<T, R> Register(Func<T, R> m)
        {
            GenericEvent += m;
            return this;
        }
        public R OnQueueEvent(T o)
        {
            return GenericEvent.Invoke(o);
        }
        public void OnQueueEventAsync(T t, AsyncCallback c)
        {
            GenericEvent.BeginInvoke(t, c, GenericEvent);
        }
    }
}
