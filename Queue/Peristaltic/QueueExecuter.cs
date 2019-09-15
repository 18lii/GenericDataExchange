using Queue.Entities;
using System.Collections.Concurrent;
using System.Threading;

namespace Queue.Peristaltic
{
    internal delegate void ProcessorEventHandler<T>(ProcessorEventArgs<T> e);
    /// <summary>
    /// 队列处理类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class QueueExecuter<T>
    {
        private event ProcessorEventHandler<T> ProcessorEvnet;
        private ConcurrentQueue<QueueModel> Queue { get; }
        private WaitHandle[] Signals { get; }
        public QueueExecuter(WaitHandle[] signal, ConcurrentQueue<QueueModel> queue, ProcessorEventHandler<T> method)
        {
            Queue = queue;
            Signals = signal;
            ProcessorEvnet = null;
            ProcessorEvnet += method;
        }

        public void Execute()
        {
            while (WaitHandle.WaitAny(Signals) == 0)
            {
                T item = default(T);
                if (Queue.TryDequeue(out var value))
                {
                    item = (T)value.Item;
                    if (value.Sequence)//是否顺序处理
                    {
                        ProcessorEvnet.Invoke(new ProcessorEventArgs<T>(value.Id, item));
                    }
                    else
                    {
                        //放入线程池延迟处理
                        ThreadPool.QueueUserWorkItem(obj =>
                        {
                            ProcessorEvnet.Invoke(new ProcessorEventArgs<T>(value.Id, item));
                        }, item);
                    }
                }
            }
        }
    }
}
