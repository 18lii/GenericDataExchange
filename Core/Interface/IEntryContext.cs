using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface
{
    /// <summary>
    /// 入列事件上下文，设置触发间隔及线程可使用次数
    /// </summary>
    public interface IEntryContext
    {
        /// <summary>
        /// 计时器
        /// </summary>
        ITimespan Timespan { get; set; }
        /// <summary>
        /// 线程可使用次数,临时线程使用
        /// </summary>
        int Count { get; set; }
    }
}
