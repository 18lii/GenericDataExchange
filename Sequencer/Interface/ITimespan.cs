using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequencer.Interface
{
    /// <summary>
    /// 入列计时器配置接口
    /// </summary>
    public interface ITimespan
    {
        long TimeTicks { get; set; }
        int TimeInterval { get; set; }
    }
}
