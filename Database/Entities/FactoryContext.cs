using Core.Entities;
using Core.Interface;
using Database.Interface;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    /// <summary>
    /// 数据库操作类工厂上下文
    /// </summary>
    internal class FactoryContext : IFactoryContext
    {
        public FactoryContext(DbOperate operate, ConcurrentBag<ConcurrentDictionary<string, object>> param, string sqlText = null)
        {
            DbOperate = operate;
            Params = param;
            SqlText = sqlText;
        }
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
