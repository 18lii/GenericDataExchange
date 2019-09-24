using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sequencer.Entities
{
    /// <summary>
    /// 控制信号类
    /// </summary>
    internal class QueueSignal
    {
        public QueueSignal()

        {
            AutoReset = new AutoResetEvent(false);
            ManualReset = new ManualResetEvent(false);
            Handles = new WaitHandle[2];
            Handles[0] = AutoReset;
            Handles[1] = ManualReset;

        }
        public EventWaitHandle AutoReset { get; }
        public EventWaitHandle ManualReset { get; }
        public WaitHandle[] Handles { get; }
    }
}
