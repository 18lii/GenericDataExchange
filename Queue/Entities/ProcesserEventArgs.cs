using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Entities
{
    /// <summary>
    /// 消息处理EventArgs
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class ProcessorEventArgs<T> : EventArgs
    {
        public T Item { get; }
        public ProcessorEventArgs(T item)
        {
            Item = item;
        }
    }
}
