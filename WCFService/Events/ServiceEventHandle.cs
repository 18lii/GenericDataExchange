using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFService.Interface;

namespace WCFService.Events
{
    internal class ServiceEventHandle<T, R> : IServiceEventHandle<T, R>
    {
        private static Func<T, R> _activing;
        public static event Func<T, R> ActiveEvent
        {
            add
            {
                if(_activing == null)
                {
                    _activing += value;
                }
            }
            remove { }
        }
        public R OnActiveEvent(T t)
        {
            return _activing.Invoke(t);
        }
    }
}
