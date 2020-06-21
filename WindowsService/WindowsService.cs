using System.ServiceModel;
using System.ServiceProcess;
using WCFService.Service;
using WCFService;
using System.Configuration;
using System;
using System.ServiceModel.Description;
using System.Reflection;
using WCFService.Interface;

namespace WindowsService
{
    public partial class WindowsService : ServiceBase
    {
        public WindowsService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //读取密文,初始化WCF服务组件
            new WCFInitialization().Initialization();
            
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
            bool httpGetEnabled = args.Length > 0 && bool.Parse(args[0]);
            
            //初始化宿主
            Host = new ServiceHost(typeof(DataExchangeService), new Uri(string.Format("net.tcp://{0}:{1}", baseAddr, basePort)));
            Host.Description.Name = "WCFService.Service.DataExchangeService";
            Host.AddServiceEndpoint(typeof(IDataExchangeService), tcpBinding, "/DataExchangeService");
            Host.Description.Behaviors.Add(new ServiceMetadataBehavior
            {
                HttpGetEnabled = httpGetEnabled,
                HttpGetUrl = new Uri(string.Format("http://{0}:{1}/Metadata", baseAddr, httpPort))
            });
            Host.Description.Behaviors.Add(new ServiceThrottlingBehavior
            {
                MaxConcurrentCalls = int.Parse(ConfigurationManager.AppSettings["maxConcurrentCalls"]),
                MaxConcurrentInstances = int.Parse(ConfigurationManager.AppSettings["maxConcurrentInstances"]),
                MaxConcurrentSessions = int.Parse(ConfigurationManager.AppSettings["maxConcurrentSessions"])
            });
            Host.Description.Behaviors.Add(Host.GetType().Assembly.CreateInstance
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
            Host.Open();
        }

        protected override void OnStop()
        {
            Host.Close();
        }
        private ServiceHost Host { get; set; }
    }
}
