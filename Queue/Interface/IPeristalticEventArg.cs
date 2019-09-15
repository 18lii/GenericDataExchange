using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Interface
{
    /// <summary>
    /// 核心事件参数接口，线程绑定及入列事件触发只接受此类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPeristalticEventArg<T>
    {
        /// <summary>
        /// 全局唯一标识，用于获取某些返回值
        /// </summary>
        Guid Id { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        string PeristalticName { get; }
        /// <summary>
        /// 用户ID
        /// </summary>
        string UserId { get; }
        /// <summary>
        /// 事件消息说明
        /// </summary>
        string Message { get; set; }
        /// <summary>
        /// 可广播的数据
        /// </summary>
        T Item { get; set; }
        /// <summary>
        /// 附加字段，用于定位数据使用位置或附加其他数据
        /// </summary>
        object AttachData { get; set; }
    }
}
