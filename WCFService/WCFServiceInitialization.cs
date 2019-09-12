using Core;
using Core.Interface;
using Database;
using DatabaseFactory.Interface;
using Queue;
using WCFService.Entity;

namespace WCFService
{
    /// <summary>
    /// WCF提供的全部服务所需组件在此装配
    /// </summary>
    public static class WCFServiceInitialization
    {
        
        public static void Initialization(string[] codes)
        {
            var kernel = new IoCKernelImpl();
            kernel.Bind<IIoCKernel>().To<IoCKernelImpl>();
            var core = kernel.Resolve<CoreInitialization>();
            //初始化核心入列事件依赖
            //初始化核心无返回值消息处理事件依赖
            core.Initialization<IGenericEventArg<IFactoryContext>>();
            //初始化核心有返回值消息处理事件依赖
            core.Initialization<IGenericEventArg<IFactoryContext>, IGenericResult>();
            //初始化数据库依赖
            kernel.Resolve<DatabaseInitialization>().Initialization();
            //初始化队列器依赖，服务启动
            kernel.Resolve<Dipper>().DipBind();
            //启动队列器，启动数据库配件
            kernel.Resolve<QueueInitialization>().PeristalticStart(new PeristalticConfiguration(codes));
        }
    }
}
