using Queue.Entities;
using Queue.Interface;
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
        private readonly IGenericEventHandle<T, R> _handler;
        private readonly AsyncCallback _callback;
        private readonly ConcurrentDictionary<Guid, object> _result;
        private readonly ConcurrentDictionary<Guid, WaitHandle[]> _resultSignal;

        public FunctionEventWorker
            (IGenericEventHandle<T, R> handler, ConcurrentDictionary<Guid, object> result, ConcurrentDictionary<Guid, WaitHandle[]> resultSignal)
        {
            _handler = handler;
            _result = result;
            _resultSignal = resultSignal;
        }
        public FunctionEventWorker
            (IGenericEventHandle<T, R> handler, ConcurrentDictionary<Guid, object> result, ConcurrentDictionary<Guid, WaitHandle[]> resultSignal, AsyncCallback callback)
            : this(handler, result, resultSignal)
        {
            _callback = callback;
        }
        
        public void Action(ProcessorEventArgs<T> e)
        {
            var result =  _handler.OnGenericEvent(e.Item);
            _result.TryAdd(e.Id, result);
        }
        public void ActionAsync(ProcessorEventArgs<T> e)
        {
            _handler.OnGenericEventAsync(e.Item, _callback);
        }
    }
}
