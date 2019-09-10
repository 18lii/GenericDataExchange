using Queue;
using System.ServiceModel;
using System.ServiceProcess;
using WCFService.Service;
using Core.Infrastructure;
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
            Initialization.PeristalticStart(new PeristalticConfiguration(args[0].Decryptogram(args[1].Decryptogram(""))));
            Host.Open();
        }

        protected override void OnStop()
        {
            Host.Close();
        }
        private ServiceHost Host { get; set; }
    }
}
