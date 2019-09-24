using System.ComponentModel;

namespace WCFService.Helper
{
    /// <summary>
    /// 数据库操作类型枚举
    /// </summary>
    internal enum DbOperate
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
}
