using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Helper
{
    /// <summary>
    /// 数据库命令操作类型枚举
    /// </summary>
    public enum CmdOperate
    {
        [Description("查询")]
        Select = 101,
        [Description("插入")]
        Insert = 102,
        [Description("更新")]
        Update = 103,
        [Description("删除")]
        Delete = 104,
        [Description("单值查询")]
        ExecuteScalar = 201,
        [Description("集合查询")]
        ExecuteReader = 202,
        [Description("操作")]
        ExecuteNoQuery = 203,
        [Description("存储过程调用")]
        ExecuteProcedure = 204,
        [Description("事务提交")]
        Commit = 0,
        [Description("事务回滚")]
        Rollback = -1
    }
    /// <summary>
    /// 数据适配器操作类型枚举
    /// </summary>
    public enum AptOperate
    {
        [Description("查询")]
        Get = 301,
        [Description("更改")]
        Set = 302
    }
}
