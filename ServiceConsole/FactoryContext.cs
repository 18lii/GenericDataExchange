using Core.Entities;
using Core.Interface;
using Database.Interface;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceConsole
{
    class FactoryContext : IFactoryContext
    {
        public DbOperate DbOperate { get; set; }
        public ConcurrentBag<ConcurrentDictionary<string, object>> Params { get; set; }
        public string SqlText { get; set; }

        public event Action<IGenericResult> ModifyEvent;

        public void OnModifyEvent(IGenericResult e)
        {
            ModifyEvent.Invoke(e);
        }
    }
}
