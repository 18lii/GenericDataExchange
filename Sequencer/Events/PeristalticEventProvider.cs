using Sequencer.Interface;
using System;

namespace Sequencer.Events
{
    /// <summary>
    /// 队列专用，事务处理事件类，无返回值事件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class PeristalticEventProvider<T> : IPeristalticEventProvider<T>
    {
        public PeristalticEventProvider(Action<T> method)
        {
            this.PeristalticEvent += method;
        }
        private Action<T> _peristalticing;
        internal event Action<T> PeristalticEvent
        {
            add
            {
                if(_peristalticing == null)
                {
                    _peristalticing += value;
                }
            }
            remove { }
        }
        
        public void OnPeristalticEvent(T t)
        {
            _peristalticing.Invoke(t);
        }
        public void OnPeristalticEventAsync(T t, AsyncCallback c)
        {
            _peristalticing.BeginInvoke(t, c, _peristalticing);
        }
    }
    /// <summary>
    /// 队列专用，事务处理事件类，有返回值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="R"></typeparam>
    internal class PeristalticEventProvider<T, R> : IPeristalticEventProvider<T, R>
    {
        public PeristalticEventProvider(Func<T, R> method)
        {
            this.PeristalticEvent += method;
        }
        private Func<T, R> _peristalticing;
        internal event Func<T, R> PeristalticEvent
        {
            add
            {
                if(_peristalticing == null)
                {
                    _peristalticing += value;
                }
            }
            remove { }
        }
        public R OnPeristalticEvent(T o)
        {
            return _peristalticing.Invoke(o);
        }
        public void OnPeristalticEventAsync(T t, AsyncCallback c)
        {
            _peristalticing.BeginInvoke(t, c, _peristalticing);
        }
    }
}
