using Core.Infrastructure;
using Core.Interface;
using DatabaseUnitOfWork;
using Queue.Interface;
using WCFService.Entity;

namespace WCFService
{
    /// <summary>
    /// WCF提供的全部服务所需组件在此装配
    /// </summary>
    public static class WCFServiceInitialization
    {
        private static readonly IIoCKernel _kernel = new IoCKernelImpl();
        /// <summary>
        /// 服务组件装配方法，参数<see cref="string[]"/>
        /// </summary>
        /// <param name="codes"></param>
        public static void Initialization(string[] codes)
        {
            //绑定核心IoC容器依赖
            _kernel.Bind<IIoCKernel>().To<IoCKernelImpl>();
            
            //绑定数据库工作单元依赖
            _kernel.Bind<IUnitOfWork>().To<UnitOfwork>();
            //绑定队列器配置程序依赖
            _kernel.Bind<IPeristalticConfiguration>().To<PeristalticConfiguration>();
            //绑定数据库连接层依赖
            _kernel.Resolve<Database.Initialization>().BindToCore();
            //启动队列器，启动数据库配件
            _kernel.Resolve<Queue.Initialization>(codes[0].Decryptogram(codes[1].Decryptogram())).PeristalticStart();
        }
    }
}
