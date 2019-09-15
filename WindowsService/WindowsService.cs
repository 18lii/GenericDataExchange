using System.ServiceModel;
using System.ServiceProcess;
using WCFService.Service;
using WCFService;
using System.Configuration;

namespace WindowsService
{
    public partial class WindowsService : ServiceBase
    {
        public WindowsService()
        {
            InitializeComponent();
            Host = new ServiceHost(typeof(DataExchangeService));
        }

        protected override void OnStart(string[] args)
        {
            var strs = new string[2];
            strs[0] = ConfigurationManager.AppSettings["DESString"];
            strs[1] = ConfigurationManager.AppSettings["DESKey"];
            WCFServiceInitialization.Initialization(strs);
            Host.Open();
        }

        protected override void OnStop()
        {
            Host.Close();
        }
        private ServiceHost Host { get; set; }
    }
}
