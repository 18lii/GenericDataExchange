using AdvancedDependencyContainer.ContainerUnity;
using Sequencer;
using WCFService.Infrastructure;

namespace WCFService
{
    public class WCFInitialization
    {
        public void Initialization(string[] codes)
        {
            //初始化依赖注入核心组件
            new DependencyComponentInitialization(new DependencyConfiguration()).Initialization(true);
            //启动定序器，数据库组件
            DependencyKernel.Resolve<Initialization>(codes[0].Decryptogram(codes[1].Decryptogram())).PeristalticStart();
        }
    }
}
