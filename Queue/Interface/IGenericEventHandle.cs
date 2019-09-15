using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Interface
{
    public interface IGenericEventHandle<T>
    {
        IGenericEventHandle<T> Register(Action<T> mehtod);
        void OnGenericEventEvent(T t);
        void OnGenericEventAsync(T t, AsyncCallback c);
    }
    public interface IGenericEventHandle<T, R>
    {
        IGenericEventHandle<T, R> Register(Func<T, R> m);
        R OnGenericEvent(T o);
        void OnGenericEventAsync(T t, AsyncCallback c);
    }
}
