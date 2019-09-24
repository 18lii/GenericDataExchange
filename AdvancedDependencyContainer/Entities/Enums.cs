using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedDependencyContainer.Entities
{
    public enum DataType
    {
        String,
        Number,
        DateTime,
        Boolean
    }
    [Description("操作结果枚举")]
    
    /// <summary>
    /// 数据连接策略类型枚举
    /// </summary>
    public enum PolicyType
    {
        [Description("ADO型")]
        Adapter,
        [Description("SQL型")]
        Command
    }
}
