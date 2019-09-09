using Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Events
{
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
    public class GenericEventHandle<T>
    {
        private event Action<T> GenericEvent;
        public GenericEventHandle<T> Register(Action<T> m)
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
    public class GenericEventHandle<T, R>
    {
        private event Func<T, R> GenericEvent;
        public GenericEventHandle<T, R> Register(Func<T, R> m)
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
