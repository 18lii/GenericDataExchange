using AdvancedDependencyContainer.Interface;
using Sequencer.Interface;
using WCFService.Entity;
using WCFService.Interface;

namespace WCFService
{
    /// <summary>
    /// WCF提供的全部服务所需组件在此装配
    /// </summary>
    public class DependencyConfiguration : IDependencyConfiguration
    {
        public IDependencyBindContext DependencyBindContext { get; set; }
        
        /// <summary>
        /// 服务组件配置方法
        /// </summary>
        /// <param name="codes"></param>
        public IDependencyBindContext BindDependency()
        {
            //绑定依赖组件
            DependencyBindContext.IoCKernel
                .Bind<IDbUnitOfWork>().To<UnitOfwork>()//绑定数据库工作单元
                .Bind<IPeristalticConfiguration>().To<PeristalticConfiguration>();//绑定定序器配置程序依赖
            return DependencyBindContext;
        }
    }
}
