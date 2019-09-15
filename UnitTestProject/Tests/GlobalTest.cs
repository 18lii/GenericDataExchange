using Core;
using Core.Entities;
using Core.Infrastructure;
using Core.Interface;
using Database;
using DatabaseUtil.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Queue;
using System;
using System.ServiceModel;
using UnitTestProject.Infrastructure;
using WCFService;
using WCFService.Entity;
using WCFService.Service;

namespace UnitTestProject.Tests
{
    [TestClass]
    public class GlobalTest
    {
        /// <summary>
        /// 组件装配测试
        /// </summary>
        [TestMethod]
        public void InitializationTest()
        {
            try
            {
                var connStr = new string[2]
            {
                "FF50E1525C650A58AEAB16969808322F8A05F4F054F803F39A9EB1CA3EABBC36E55E402825C50E5BD28A79D5A17761F232CBE1657DA924823180E0634289856C59B54F3B24BEE5D2A58CEE37309BA49BD2509C331F7CC98E",
                "30E9AA65BA938F545A6CC1DB3027CED9"
            };
                var result = connStr[0].Decryptogram(connStr[1].Decryptogram());
                var kernel = new IoCKernelImpl();
                kernel.Bind<IIoCKernel>().To<IoCKernelImpl>();
                var core = kernel.Resolve<CoreInitialization>();
                //初始化核心入列事件依赖
                //初始化核心无返回值消息处理事件依赖
                core.Initialization<IGenericEventArg<IFactoryContext>>();
                //初始化核心有返回值消息处理事件依赖
                core.Initialization<IGenericEventArg<IFactoryContext>, IGenericResult>();
                //初始化数据库依赖
                kernel.Resolve<Database.Initialization>().BindToCore();
                //初始化队列器依赖，服务启动
                kernel.Resolve<Dipper>().DipBind();
                kernel.Resolve<Queue.Initialization>().PeristalticStart(new PeristalticConfiguration(connStr));
                //获取数据工厂
                var factory = kernel.Resolve<DbFactoryImpl>();
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.IsTrue(false);
            }
        }
        [TestMethod]
        public void WindowsServiceTest()
        {
            
            ServiceHost host = new ServiceHost(typeof(DataExchangeService));
            var strs = new string[2]
            {
                "FF50E1525C650A58AEAB16969808322F8A05F4F054F803F39A9EB1CA3EABBC36E55E402825C50E5BD28A79D5A17761F232CBE1657DA924823180E0634289856C59B54F3B24BEE5D2A58CEE37309BA49BD2509C331F7CC98E",
                "30E9AA65BA938F545A6CC1DB3027CED9"
            };
            WCFServiceInitialization.Initialization(strs);
            host.Open();
            Console.ReadLine();
            host.Close();
        }
    }
}
