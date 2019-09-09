using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public enum DataType
    {
        String,
        Number,
        DateTime,
        Boolean
    }
    [Description("操作结果枚举")]
    public enum ResultType
    {
        /// <summary>
        ///     操作成功
        /// </summary>
        [Description("操作成功。")]
        Success,

        /// <summary>
        ///     操作取消或操作没引发任何变化
        /// </summary>
        [Description("操作没有引发任何变化，提交取消。")]
        NoChanged,

        /// <summary>
        ///     参数错误
        /// </summary>
        [Description("参数错误。")]
        ParamError,

        /// <summary>
        ///     指定参数的数据不存在
        /// </summary>
        [Description("指定参数的数据不存在。")]
        QueryNull,

        /// <summary>
        ///     权限不足
        /// </summary>
        [Description("当前用户权限不足，不能继续操作。")]
        PurviewLack,

        /// <summary>
        ///     非法操作
        /// </summary>
        [Description("非法操作。")]
        IllegalOperation,

        /// <summary>
        ///     警告
        /// </summary>
        [Description("警告")]
        Warning,

        /// <summary>
        ///     操作引发错误
        /// </summary>
        [Description("操作引发错误。")]
        Error
    }
    public enum StartState
    {
        Success,
        Ship,
        Faild
    }
    /// <summary>
    /// 参数类型枚举
    /// </summary>
    public enum ParamType
    {
        [Description("赋值参数")]
        V,
        [Description("条件参数")]
        W,
        [Description("排序参数")]
        O,
        [Description("匹配参数")]
        L,
        [Description("分页参数")]
        P,
        [Description("存储过程参数")]
        S,
        [Description("附加参数")]
        A
    }
    public enum ExecuteType
    {
        Count,
        Data,
        Value
    }
    /// <summary>
    /// 数据库操作类型枚举
    /// </summary>
    public enum DbOperate
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
