using Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Ignition
{
    /// <summary>
    /// 入列事件触发类
    /// </summary>
    internal class Timespan : ITimespan
    {
        public long TimeTicks { get; set; }
        public int TimeInterval { get; set; }
        public bool Pass
        {
            get
            {
                if (TimeInterval > 0)
                {
                    if (TimeTicks == 0)
                    {
                        TimeTicks = DateTime.Now.Ticks / 10000;
                        return true;
                    }
                    else
                    {
                        var b = ((DateTime.Now.Ticks / 10000) - TimeTicks) > TimeInterval;
                        if (b)
                        {
                            TimeTicks = DateTime.Now.Ticks / 10000;
                        }
                        return b;
                    }
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
