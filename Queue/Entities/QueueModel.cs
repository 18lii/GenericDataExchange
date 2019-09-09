using Queue.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Entities
{
    /// <summary>
    /// 队列元素模型
    /// </summary>
    public class QueueModel : IQueueModel
    {
        public QueueModel() { }
        public QueueModel(string name, object item)
        {
            Name = name;
            Item = item;
        }
        public string Name { get; set; }
        public object Item { get; set; }
    }
}
