using Core.Interface;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Interface
{
    /// <summary>
    /// 数据库工厂类上下文接口，自动使用，不必理会
    /// </summary>
    public interface IBaseContext
    {
        ConcurrentDictionary<string, SqlConnection> SqlConnection { get; set; }
        ConcurrentDictionary<Guid, SqlTransaction> SqlTransaction { get; set; }
        IGenericResult DbCommit(string userId, Guid id);
        IGenericResult DbRollback(string userId, Guid id);
    }
}
