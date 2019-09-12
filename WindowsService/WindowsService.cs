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
            var strs = new string[2]
            {
                "FF50E1525C650A586E0981980ED69935B92F4B36B964C87FB98189A605C704592ADF5312CB1EDED37336740A27F466D3C21E156E84275675F9CFBCF586AE0337A8C99EF1A72EA37228BB9F80D6D4F95A",
                "30E9AA65BA938F545A6CC1DB3027CED9"
            };
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
