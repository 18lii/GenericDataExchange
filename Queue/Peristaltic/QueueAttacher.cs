using Queue.Entities;
using System.Collections.Concurrent;
using System.Threading;

namespace Queue.Peristaltic
{
    class QueueAttach
    {
        private ConcurrentQueue<QueueModel> Queue { get; }
        private WaitHandle[] Signal { get; }
        public QueueAttach(ConcurrentQueue<QueueModel> queue, WaitHandle[] signals)
        {
            Queue = queue;
            Signal = signals;
        }
        /// <summary>
        /// 消息入列方法
        /// </summary>
        /// <param name="t"></param>
        internal void Add(object c)
        {
            Queue.Enqueue(new QueueModel("DatabaseService", c));
            ((AutoResetEvent)Signal[0]).Set();
        }
    }
}
