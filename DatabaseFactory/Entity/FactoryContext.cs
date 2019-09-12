using Core.Entities;
using Core.Interface;
using DatabaseFactory.Interface;
using System;
using System.Collections.Concurrent;
using System.Data;

namespace DatabaseFactory.Entity
{
    /// <summary>
    /// 数据库操作类工厂上下文
    /// </summary>
    public class FactoryContext : IFactoryContext
    {
        public FactoryContext(PolicyType policyType)
        {
            PolicyType = PolicyType;
        }
        public FactoryContext(PolicyType policyType, DbOperate operate, string[] sqlText)
            : this(policyType)
        {
            DbOperate = operate;
            SqlText = sqlText;
        }
        public FactoryContext(PolicyType policyType, DbOperate operate, string[] sqlText, ConcurrentBag<ConcurrentDictionary<string, object>> param)
            : this(policyType, operate, sqlText)
        {
            Params = param;
        }
        public FactoryContext(PolicyType policyType, AdapterOperate operate, string[] sqlText)
            : this(policyType)
        {
            AptOperate = operate;
            SqlText = sqlText;
        }
        public FactoryContext(PolicyType policyType, AdapterOperate operate, string[] sqlText, DataSet[] dataSet)
            : this(policyType, operate, sqlText)
        {
            DataSet = dataSet;
        }

        public PolicyType PolicyType { get; set; }
        public DbOperate DbOperate { get; set; }
        public AdapterOperate AptOperate { get; set; }
        public ConcurrentBag<ConcurrentDictionary<string, object>> Params { get; set; }
        public string[] SqlText { get; set; }
        public DataSet[] DataSet { get; set; }

        public event Action<IGenericResult> ModifyEvent;
        public void OnModifyEvent(IGenericResult e)
        {
            ModifyEvent.Invoke(e);
        }
    }
}
