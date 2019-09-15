using Database.Helper;
using System;
using System.Collections;
using System.Collections.Concurrent;

namespace Database.Interface
{
    /// <summary>
    /// 数据库操作上下文接口
    /// </summary>
    public interface ICommandContext : IBaseContext
    {
        Tuple<bool, object> Activing(Tuple<CmdOperate, ConcurrentDictionary<string, Hashtable>> context);
    }
}
