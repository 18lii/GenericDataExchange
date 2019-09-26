using Sequencer.Entities;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Sequencer.Peristaltic
{
    /// <summary>
    /// 调度器
    /// </summary>
    internal class QueueDispatch
    {
        private WaitHandle[] Signal { get; }
        private ConcurrentQueue<QueueModel> Troop { get; }
        private ConcurrentDictionary<string, Executer> Executers { get; }
        public QueueDispatch(WaitHandle[] signals, ConcurrentQueue<QueueModel> troop, ConcurrentDictionary<string, Action<WaitHandle[], ConcurrentQueue<QueueModel>>> actions, ConcurrentDictionary<string, int> executerDefault)
        {
            Signal = signals;
            Executers = new ConcurrentDictionary<string, Executer>();
            Troop = troop;
            foreach (var a in actions)
            {
                var executer = new Executer(executerDefault[a.Key]);
                for (var i = 0; i < executerDefault[a.Key]; i++)
                {
                    var signal = new WaitHandle[2] { new AutoResetEvent(false), new ManualResetEvent(false)};
                    new Thread(obj => { a.Value(signal, executer.Squadron); })
                    {
                        Name = "GSP-EXECUTE:" + a.Key + "#default" + "#" + i
                    }.Start();
                    executer.Signals[i, 0] = signal[0];
                    executer.Signals[i, 1] = signal[1];
                    executer.Count++;
                }
                Executers.TryAdd(a.Key, executer);
            }
        }
        /// <summary>
        /// 调度处理
        /// </summary>
        public void Dispatch()
        {
            var locker = new object();
            while (WaitHandle.WaitAny(Signal) == 1)
            {
                if (Troop.TryDequeue(out var tv))
                {
                    var name = tv.Name;
                    Executers.TryGetValue(name, out var ev);
                    ev.Squadron.Enqueue(tv);
                    Parallel.For(0, ev.Count, i =>
                    {
                        ((ManualResetEvent)ev.Signals[i, 1]).Set();
                    });
                }
                else
                {
                    ((ManualResetEvent)Signal[1]).Reset();
                }
            }
        }
    }
}
