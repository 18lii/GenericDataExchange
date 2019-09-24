using Sequencer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequencer.Entities
{
    /// <summary>
    /// 队列元素模型
    /// </summary>
    internal class QueueModel : IQueueModel
    {
        public QueueModel() { }
        public QueueModel(string name, Guid id, object item, bool sequence)
        {
            Id = id;
            Name = name;
            Item = item;
            Sequence = sequence;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public object Item { get; set; }
        public bool Sequence { get; set; }
    }
}
