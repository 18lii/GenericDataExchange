using System.Collections.Concurrent;
using System.Threading;

namespace Queue.Entities
{
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
