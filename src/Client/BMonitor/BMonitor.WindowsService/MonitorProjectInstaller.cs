using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace BMonitor.WindowsService
{
    [RunInstaller(true)]
    public partial class MonitorProjectInstaller : Installer
    {
        public MonitorProjectInstaller()
        {
            InitializeComponent();

            serviceProcessInstaller1.Account = ServiceAccount.LocalSystem;

            serviceInstaller1.ServiceName = MonitorService.SERVICE_NAME;
        }
    }
}
