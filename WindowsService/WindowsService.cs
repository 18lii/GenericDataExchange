using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using WCFService;

namespace WindowsService
{
    public partial class WindowsService : ServiceBase
    {
        public WindowsService()
        {
            InitializeComponent();
            Host = new ServiceHost(typeof(WCFService.DataExchangeService));
        }

        protected override void OnStart(string[] args)
        {
            Host.Open();
        }

        protected override void OnStop()
        {
            Host.Close();
        }
        private ServiceHost Host { get; set; }
    }
}
