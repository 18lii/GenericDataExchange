using AdvancedDependencyContainer.ContainerUnity;
using Sequencer;
using WCFService.Infrastructure;

namespace WCFService
{
    public class WCFInitialization
    {
        public void Initialization()
        {
            //初始化依赖注入核心组件
            new DependencyComponentInitialization(new DependencyConfiguration()).Initialization();
            //初始化定序器，数据库组件
            DependencyKernel.Resolve<SequencerInitialization>().PeristalticStart();
        }
    }
}
