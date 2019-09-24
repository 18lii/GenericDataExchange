using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Description;
using UnitTestProject.Infrastructure;
using WCFService;
using WCFService.Infrastructure;
using WCFService.Service;
using AdvancedDependencyContainer.Configurations;

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
                //初始化核心入列事件依赖
                //初始化核心无返回值消息处理事件依赖
                //初始化核心有返回值消息处理事件依赖
                //初始化数据库依赖
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
            //读取密文,初始化WCF服务组件
            new WCFInitialization().Initialization(new string[]
            {
                ConfigurationManager.AppSettings["DESString"],
                ConfigurationManager.AppSettings["DESKey"]
            });

            //读取服务基地址
            var baseAddr = ConfigurationManager.AppSettings["baseAddress"];
            var basePort = int.Parse(ConfigurationManager.AppSettings["port"]);
            var httpPort = basePort + 1;
            var tcpBinding = new NetTcpBinding
            {
                Name = "NetTcpBinding_IDataExchangeService",
                MaxBufferPoolSize = int.Parse(ConfigurationManager.AppSettings["maxBufferPoolSize"]),
                MaxBufferSize = int.Parse(ConfigurationManager.AppSettings["maxBufferSize"]),
                MaxReceivedMessageSize = int.Parse(ConfigurationManager.AppSettings["maxReceivedMessageSize"]),
                MaxConnections = int.Parse(ConfigurationManager.AppSettings["maxConnections"]),
                ListenBacklog = int.Parse(ConfigurationManager.AppSettings["listenBacklog"]),
                OpenTimeout = TimeSpan.Parse(ConfigurationManager.AppSettings["openTimeout"]),
                CloseTimeout = TimeSpan.Parse(ConfigurationManager.AppSettings["closeTimeout"]),
                SendTimeout = TimeSpan.Parse(ConfigurationManager.AppSettings["sendTimeout"]),
                ReceiveTimeout = TimeSpan.Parse(ConfigurationManager.AppSettings["receiveTimeout"]),
            };

            //安全模式，重要
            tcpBinding.Security.Mode = (SecurityMode)int.Parse(ConfigurationManager.AppSettings["securityMode"]);
            tcpBinding.ReaderQuotas.MaxArrayLength = int.Parse(ConfigurationManager.AppSettings["maxArrayLength"]);
            tcpBinding.ReaderQuotas.MaxStringContentLength = int.Parse(ConfigurationManager.AppSettings["maxStringContentLength"]);
            tcpBinding.ReaderQuotas.MaxBytesPerRead = int.Parse(ConfigurationManager.AppSettings["maxBytesPerRead"]);
            bool httpGetEnabled = true;

            //初始化宿主
            var host = new ServiceHost(typeof(DataExchangeService), new Uri(string.Format("net.tcp://{0}:{1}", baseAddr, basePort)));
            host.Description.Name = "WCFService.Service.DataExchangeService";
            host.AddServiceEndpoint(typeof(IDataExchangeService), tcpBinding, "/DataExchangeService");
            host.Description.Behaviors.Add(new ServiceMetadataBehavior
            {
                HttpGetEnabled = httpGetEnabled,
                HttpGetUrl = new Uri(string.Format("http://{0}:{1}/Metadata", baseAddr, httpPort))
            });
            host.Description.Behaviors.Add(new ServiceThrottlingBehavior
            {
                MaxConcurrentCalls = int.Parse(ConfigurationManager.AppSettings["maxConcurrentCalls"]),
                MaxConcurrentInstances = int.Parse(ConfigurationManager.AppSettings["maxConcurrentInstances"]),
                MaxConcurrentSessions = int.Parse(ConfigurationManager.AppSettings["maxConcurrentSessions"])
            });
            host.Description.Behaviors.Add(host.GetType().Assembly.CreateInstance
                    (
                        "System.ServiceModel.Dispatcher.DataContractSerializerServiceBehavior",
                        true,
                        BindingFlags.CreateInstance |
                        BindingFlags.Instance |
                        BindingFlags.NonPublic,
                        null,
                        new object[] { false, int.Parse(ConfigurationManager.AppSettings["maxConcurrentSessions"]) },
                        null,
                        null
                    ) as IServiceBehavior);

            //服务打开
            host.Open();
            Console.ReadLine();
            //host.Close();
        }
    }
}
