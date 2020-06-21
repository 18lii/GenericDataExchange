using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequencer.Interface
{
    public interface IPeristalticEventProvider<T>
    {
        void OnPeristalticEvent(T t);
        void OnPeristalticEventAsync(T t, AsyncCallback c);
    }
    public interface IPeristalticEventProvider<T, R>
    {
        R OnPeristalticEvent(T o);
        void OnPeristalticEventAsync(T t, AsyncCallback c);
    }
}
