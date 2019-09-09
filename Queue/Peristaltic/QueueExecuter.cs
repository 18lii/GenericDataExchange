using Queue.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Queue.Peristaltic
{
    internal delegate void ProcessorEventHandler<T>(ProcessorEventArgs<T> e);
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
                T item = default;
                if (Queue.TryDequeue(out var value))
                {
                    item = (T)value.Item;

                    ProcessorEvnet?.Invoke(new ProcessorEventArgs<T>(item));
                }
            }
        }
    }
    internal class Executer
    {
        //数组有效元素记录数
        public int Count { get; set; }
        public WaitHandle[,] Signals { get; }
        public ConcurrentQueue<QueueModel> Squadron { get; }
        public bool Sequential { get; }
        public bool Stay { get; }
        public Executer(WaitHandle[] signals, int max)
        {
            Signals = new WaitHandle[max, 2];
            Squadron = new ConcurrentQueue<QueueModel>();
            Signals[0, 0] = signals[0];
            Signals[0, 1] = signals[1];
            Count++;
        }
        public Executer(int max)
        {
            Signals = new WaitHandle[max, 2];
            Squadron = new ConcurrentQueue<QueueModel>();
        }
    }
}
