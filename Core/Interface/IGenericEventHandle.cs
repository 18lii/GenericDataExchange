using System;

namespace Core.Interface
{
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
