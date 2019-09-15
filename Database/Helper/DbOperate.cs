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
        Select,
        [Description("插入")]
        Insert,
        [Description("更新")]
        Update,
        [Description("删除")]
        Delete,
        [Description("单值查询")]
        ExecuteScalar,
        [Description("集合查询")]
        ExecuteReader,
        [Description("操作")]
        ExecuteNoQuery,
        [Description("存储过程调用")]
        ExecuteProcedure,
        [Description("事务提交")]
        Commit,
        [Description("事务回滚")]
        Rollback
    }
    /// <summary>
    /// 数据适配器操作类型枚举
    /// </summary>
    public enum AptOperate
    {
        [Description("查询")]
        Get,
        [Description("更改")]
        Set
    }
}
