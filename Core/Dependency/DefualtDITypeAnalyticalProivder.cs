using Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dependency
{
    /// <summary>
    /// 分析类型，提供程序
    /// </summary>
    public class DefualtDITypeAnalyticalProivder : IDITypeAnalyticalProvider
    {
        public IDITypeAnalytical CreateDITypeAnalaytical()
        {
            return new DITypeAnalytical();
        }
    }
}
