using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequencer.Entities
{
    /// <summary>
    /// 消息处理EventArgs，内部
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class ProcessorEventArgs<T> : EventArgs
    {
        public Guid Id { get; }
        public T Item { get; }
        public ProcessorEventArgs(Guid id, T item)
        {
            Id = id;
            Item = item;
        }
    }
}
