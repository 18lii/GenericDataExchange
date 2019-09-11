using Core.Events;
using Core.Helper;
using Core.Interface;
using Queue.Entities;
using System;
using System.Collections.Concurrent;
using System.Threading;

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
    internal class FunctionEventWorker<T, R>
    {
        private GenericEventHandle<IGenericEventArg<T>, R> Handler { get; }
        private AsyncCallback Callback { get; }
        private WaitHandle[] LoaderSignal { get; }
        private ConcurrentQueue<QueueModel> Troops { get; }

        public FunctionEventWorker(GenericEventHandle<IGenericEventArg<T>, R> handler)
        {
            Handler = handler;
        }
        public FunctionEventWorker(GenericEventHandle<IGenericEventArg<T>, R> handler, AsyncCallback callback)
            : this(handler)
        {
            Callback = callback;
        }
        public FunctionEventWorker(GenericEventHandle<IGenericEventArg<T>, R> handler, WaitHandle[] loaderSignal, ConcurrentQueue<QueueModel> troops)
            : this(handler)
        {
            LoaderSignal = loaderSignal;
            Troops = troops;
        }
        public void Action(ProcessorEventArgs<IGenericEventArg<T>> e)
        {
            var result =  Handler.OnQueueEvent(e.Item);
            e.Item.AttachData = result;
            Troops.Enqueue(new QueueModel
            {
                Name = "Result",
                Item = e.Item,
            });
            ((AutoResetEvent)LoaderSignal[0]).Set();
        }
        public void ActionAsync(ProcessorEventArgs<IGenericEventArg<T>> e)
        {
            Handler.OnQueueEventAsync(e.Item, Callback);
        }
    }
}
