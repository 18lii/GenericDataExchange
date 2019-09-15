using Queue.Entities;
using Queue.Interface;
using System;

namespace Queue.EventContext
{
    /**
     * Shine
     * 2019-9-6  
     * 队列对象处理方法，通过委托绑定，
     * 包含：
     * Worker处理完成后，保持线程处于等待状态，
     **/
    /// <summary>
    /// 消息处理类
    /// </summary>
    internal class ActionEventWorker<T>
    {
        private IGenericEventHandle<T> Handler { get; }
        private AsyncCallback Callback { get; }
        public ActionEventWorker(IGenericEventHandle<T> handler)
        {
            Handler = handler;
        }
        public ActionEventWorker(IGenericEventHandle<T> handler, AsyncCallback callback)
            : this(handler)
        {
            Callback = callback;
        }
        /// <summary>
        /// 触发Action
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e"></param>
        public void Action(ProcessorEventArgs<T> e)
        {
            Handler.OnGenericEventEvent(e.Item);
        }
        /// <summary>
        /// 触发Action并进行异步回调
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e"></param>
        public void ActionAsync(ProcessorEventArgs<T> e)
        {
            Handler.OnGenericEventAsync(e.Item, Callback);
        }
    }
}
