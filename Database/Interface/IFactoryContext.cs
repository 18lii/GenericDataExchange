using Core.Entities;
using Core.Interface;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Interface
{
    /// <summary>
    /// 数据库工厂上下文接口
    /// </summary>
    public interface IFactoryContext
    {
        PolicyType PolicyType { get; set; }
        DbOperate DbOperate { get; set; }
        AdapterOperate AptOperate { get; set; }
        ConcurrentBag<ConcurrentDictionary<string, object>> Params { get; set; }
        string SqlText { get; set; }
        DataSet DataSet { get; set; }
        event Action<IGenericResult> ModifyEvent;
        void OnModifyEvent(IGenericResult e);
    }
}
