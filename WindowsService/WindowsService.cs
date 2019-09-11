using System.ServiceModel;
using System.ServiceProcess;
using WCFService.Service;
using WCFService;

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

            WCFServiceInitialization.Start(args);
            Host.Open();
        }

        protected override void OnStop()
        {
            Host.Close();
        }
        private ServiceHost Host { get; set; }
    }
}
