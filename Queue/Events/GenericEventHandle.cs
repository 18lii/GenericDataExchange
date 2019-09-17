using Queue.Interface;
using System;
using System.Threading.Tasks;

namespace Queue.Events
{
    /// <summary>
    /// 全局通用队列入列事件类
    /// </summary>
    public static class GenericEventHandle
    {
        private static event Action<string, Guid, object, bool> GenericEvent;
        private static event Func<Guid, object> ResultEvent;
        internal static void Register(Action<string, Guid, object, bool> m)
        {
            GenericEvent += m;
        }
        public static bool OnGenericEvent(string n, Guid i, object o, bool s)
        {
            try
            {
                GenericEvent.Invoke(n, i, o, s);
                return true;
            }
            catch
            {
                return false;
            }

        }
        internal static void Register(Func<Guid, object> m)
        {
            ResultEvent += m;
        }
        public static object OnResultEvent(Guid id)
        {
            return ResultEvent.Invoke(id);
        }
        public static void OnResultEventAsync(Guid id, AsyncCallback callback)
        {
            ResultEvent.BeginInvoke(id, callback, ResultEvent);
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
        public void OnGenericEventEvent(T t)
        {
            GenericEvent.Invoke(t);
        }
        public void OnGenericEventAsync(T t, AsyncCallback c)
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
        public R OnGenericEvent(T o)
        {
            return GenericEvent.Invoke(o);
        }
        public void OnGenericEventAsync(T t, AsyncCallback c)
        {
            GenericEvent.BeginInvoke(t, c, GenericEvent);
        }
    }
}
