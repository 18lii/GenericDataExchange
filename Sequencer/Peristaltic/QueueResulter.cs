using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequencer.Peristaltic
{
    internal class QueueResulter
    {
        public QueueResulter()
        {
            Contianer = new ConcurrentDictionary<Guid, Action<object>>();
        }
        public bool Add(Guid id, Action<object> method)
        {
            return Contianer.TryAdd(id, method);
        }
        public bool ContainsKey(Guid id)
        {
            return Contianer.ContainsKey(id);
        }
        public Action<object> GetValue(Guid id)
        {
            Contianer.TryGetValue(id, out var method);
            return method;
        }
        public void Remove(Guid id)
        {
            Contianer.TryRemove(id, out var temp);
        }
        private ConcurrentDictionary<Guid, Action<object>> Contianer { get; }
    }
}
