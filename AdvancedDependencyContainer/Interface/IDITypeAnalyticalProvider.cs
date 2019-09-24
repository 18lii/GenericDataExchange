using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedDependencyContainer.Interface
{
    /// <summary>
    /// 分析上层类型的提供程序对象接口
    /// </summary>
    internal interface IDITypeAnalyticalProvider
    {
        IDITypeAnalytical CreateDITypeAnalaytical();
    }
}
