using Sequencer.Entities;
using Sequencer.Interface;
using System;

namespace Sequencer.EventContext
{
    /**
     * Shine
     * 2019-9-6  
     * 队列对象处理方法，通过委托绑定，
     * 说明：
     * 根据QueueExecuter上文选择无返回值处理方法下文
     **/
    /// <summary>
    /// 消息处理类
    /// </summary>
    internal class AxEventContext<T>
    {
        private IPeristalticEventProvider<T> Handler { get; }
        private AsyncCallback Callback { get; }
        public AxEventContext(IPeristalticEventProvider<T> handler)
        {
            Handler = handler;
        }
        public AxEventContext(IPeristalticEventProvider<T> handler, AsyncCallback callback)
            : this(handler)
        {
            Callback = callback;
        }
        /// <summary>
        /// 触发Action
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e"></param>
        public void Active(ProcessorEventArgs<T> e)
        {
            Handler.OnPeristalticEvent(e.Item);
        }
        /// <summary>
        /// 触发Action并进行异步回调
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e"></param>
        public void ActiveAsync(ProcessorEventArgs<T> e)
        {
            Handler.OnPeristalticEventAsync(e.Item, Callback);
        }
    }
}
