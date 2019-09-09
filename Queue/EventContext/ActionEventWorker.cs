using Core.Events;
using Core.Interface;
using Queue.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private GenericEventHandle<T> Handler { get; }
        private AsyncCallback Callback { get; }
        public ActionEventWorker(GenericEventHandle<T> handler)
        {
            Handler = handler;
        }
        public ActionEventWorker(GenericEventHandle<T> handler, AsyncCallback callback)
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
            Handler.OnQueueEvent(e.Item);
        }
        /// <summary>
        /// 触发Action并进行异步回调
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e"></param>
        public void ActionAsync(ProcessorEventArgs<T> e)
        {
            Handler.OnQueueEventAsync(e.Item, Callback);
        }
    }
}
