using Sequencer.Entities;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Sequencer.Peristaltic
{
    internal class QueueAttacher
    {
        private readonly ConcurrentQueue<QueueModel> _troops;
        private readonly WaitHandle[] _signal;
        private readonly ConcurrentDictionary<Guid, WaitHandle[]> _resultSignal;
        public QueueAttacher(ConcurrentQueue<QueueModel> queue, WaitHandle[] signals, ConcurrentDictionary<Guid, WaitHandle[]> resultSignal)
        {
            _troops = queue;
            _signal = signals;
            _resultSignal = resultSignal;
        }
        /// <summary>
        /// 消息入列方法
        /// </summary>
        /// <param name="t"></param>
        internal void Add(string n, Guid i, object c, bool s)
        {
            _troops.Enqueue(new QueueModel(n, i, c, s));
            //结果信号
            _resultSignal.TryAdd(i, new WaitHandle[2]
            {
                new AutoResetEvent(false),
                new ManualResetEvent(false)
            });
            //调度信号
            ((ManualResetEvent)_signal[1]).Set();
        }
    }
}
