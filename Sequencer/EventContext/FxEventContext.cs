using Sequencer.Entities;
using Sequencer.Events;
using Sequencer.Interface;
using Sequencer.Peristaltic;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Sequencer.EventContext
{
    /**
     * Shine
     * 2019-9-6  
     * 队列对象处理方法，通过委托绑定，
     * 说明：
     * 根据QueueExecuter上文选择有返回值处理方法下文
     **/
    /// <summary>
    /// 消息处理类
    /// </summary>
    internal class FxEventContext<T, R>
    {
        private readonly IPeristalticEventProvider<T, R> _handler;
        private readonly QueueResulter _resulter;
        private readonly AsyncCallback _callback;

        public FxEventContext
            (IPeristalticEventProvider<T, R> handler, QueueResulter resulter)
        {
            _handler = handler;
            _resulter = resulter;
        }
        public FxEventContext
            (IPeristalticEventProvider<T, R> handler, QueueResulter resulter, AsyncCallback callback)
            : this(handler, resulter)
        {
            _callback = callback;
        }
        
        public void Active(ProcessorEventArgs<T> e)
        {
            try
            {
                if (_resulter.ContainsKey(e.Id))
                {
                    _resulter.GetValue(e.Id).Invoke(_handler.OnPeristalticEvent(e.Item));
                }
            }
            catch(Exception ex)
            {
                return;
            }
        }
        public void ActiveAsync(ProcessorEventArgs<T> e)
        {
            try
            {
                _handler.OnPeristalticEventAsync(e.Item, _callback);
            }
            catch
            {

            }
        }
    }
}
