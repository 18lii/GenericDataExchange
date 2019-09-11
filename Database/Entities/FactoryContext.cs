using Core.Entities;
using Core.Interface;
using Database.Interface;
using System;
using System.Collections.Concurrent;
using System.Data;

namespace Database.Entities
{
    /// <summary>
    /// 数据库操作类工厂上下文
    /// </summary>
    internal class FactoryContext : IFactoryContext
    {
        public FactoryContext(DbOperate operate, string sqlText)
        {
            DbOperate = operate;
            SqlText = sqlText;
        }
        public FactoryContext(DbOperate operate, string sqlText, ConcurrentBag<ConcurrentDictionary<string, object>> param)
            : this(operate, sqlText)
        {
            Params = param;
        }
        public FactoryContext(AdapterOperate operate, string sqlText)
        {
            AptOperate = operate;
            SqlText = sqlText;
        }
        public FactoryContext(AdapterOperate operate, string sqlText, DataSet dataSet)
            : this(operate, sqlText)
        {
            DataSet = dataSet;
        }
        
        public PolicyType PolicyType { get; set; }
        public DbOperate DbOperate { get; set; }
        public AdapterOperate AptOperate { get; set; }
        public ConcurrentBag<ConcurrentDictionary<string, object>> Params { get; set; }
        public string SqlText { get; set; }
        public DataSet DataSet { get; set; }

        public event Action<IGenericResult> ModifyEvent;
        public void OnModifyEvent(IGenericResult e)
        {
            ModifyEvent.Invoke(e);
        }
    }
}
