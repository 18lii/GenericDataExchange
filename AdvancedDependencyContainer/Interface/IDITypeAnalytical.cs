using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedDependencyContainer.Interface
{
    /// <summary>
    /// 反射器接口
    /// </summary>
    internal interface IDITypeAnalytical
    {
        T GetValue<T>(object parameter = null);
        object GetValue(Type type, object parameter = null);
    }
}
