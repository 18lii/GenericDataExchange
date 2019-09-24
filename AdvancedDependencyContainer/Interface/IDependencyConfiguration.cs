using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedDependencyContainer.Interface
{
    /// <summary>
    /// 依赖注入配置接口，实现接口并使用<see cref="IDependencyBindContext.IoCKernel.Bind{T}().To{U}()"/> 方法绑定依赖组件
    /// </summary>
    public interface IDependencyConfiguration
    {
        IDependencyBindContext DependencyBindContext { get; set; }
        IDependencyBindContext BindDependency();
    }
}
