using System;
using System.Collections.Concurrent;
using System.Data.SqlClient;

namespace Database.Interface
{
    /// <summary>
    /// 数据库工厂类上下文接口，自动使用，不必理会
    /// </summary>
    public interface IBaseContext
    {
        string ConnectionString { get; set; }
        ConcurrentDictionary<Guid, SqlConnection> Connection { get; set; }
        ConcurrentDictionary<Guid, SqlTransaction> Transaction { get; set; }
        Tuple<bool, object> DbCommit(Guid id);
        Tuple<bool, object> DbRollback(Guid id);
    }
}
