using Core.Infrastructure;
using Core.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.ServiceModel;
using UnitTestProject.Infrastructure;
using WCFService;
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
                //初始化核心入列事件依赖
                //初始化核心无返回值消息处理事件依赖
                //初始化核心有返回值消息处理事件依赖
                //初始化数据库依赖
                kernel.Resolve<Database.Initialization>().BindToCore();
                //初始化队列器依赖，服务启动
                //获取数据工厂
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
            var strs = new string[2];
            strs[0] = ConfigurationManager.AppSettings["DESString"];
            strs[1] = ConfigurationManager.AppSettings["DESKey"];
            WCFServiceInitialization.Initialization(strs);
            host.Open();
            Console.ReadLine();
            host.Close();
        }
    }
}
