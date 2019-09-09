using Core.Entities;
using Core.Events;
using Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Ignition
{
    /// <summary>
    /// 任务入列静态类
    /// </summary>
    public static class Entry
    {

        /// <summary>
        /// 任务入列方法，触发消息处理，同步无返回
        /// <para><see cref="int"/> timeInterval为触发频率，单位为毫秒</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="eventArg"></param>
        /// <param name="timeInterval"></param>
        public static StartState Start<T>(this IEntryContext context, IGenericEventArg<T> eventArg, int timeInterval = 0)
        {
            if (context.Timespan == null)
            {
                context.Timespan = new Timespan
                {
                    TimeInterval = timeInterval
                };
            }
            if (((Timespan)context.Timespan).Pass)
            {
                if (GenericEventHandle.OnQueueEvent(eventArg))
                {
                    return StartState.Success;
                }
                else
                {
                    return StartState.Faild;
                }
            }
            else
            {
                return StartState.Ship;
            }
        }
    }
}
