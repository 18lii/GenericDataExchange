using AdvancedDependencyContainer.Dependency;
using AdvancedDependencyContainer.Event;
using AdvancedDependencyContainer.Interface;

namespace AdvancedDependencyContainer.ContainerUnity
{
    /// <summary>
    /// 依赖注入组件初始化类
    /// </summary>
    public class DependencyComponentInitialization
    {
        private readonly IDependencyConfiguration _dependencyConfiguration;
        /// <summary>
        /// 依赖组件初始化类构造函数，调用方需继承并实现<see cref="IDependencyConfiguration"/>
        /// 接口作为构造参数
        /// </summary>
        /// <param name="dependencyConfiguration"></param>
        public DependencyComponentInitialization(IDependencyConfiguration dependencyConfiguration)
        {
            _dependencyConfiguration = dependencyConfiguration;
            _dependencyConfiguration.DependencyBindContext = new DependencyBindContext();
        }
        
        /// <summary>
        /// IoC容器及依赖组件绑定初始化方法
        /// </summary>
        public void Initialization()
        {
            //绑定用户定义依赖
            var configuration = _dependencyConfiguration.BindDependency();
            //注册控制反转事件
            DependencyEventHandle.ResolveEvent += ((DependencyBindContext)configuration).IoCKernel.Resolve;
        }
    }
}
