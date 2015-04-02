using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace BMonitor.Service
{
    [RunInstaller(true)]
    public partial class MonitorProjectInstaller : System.Configuration.Install.Installer
    {
        public MonitorProjectInstaller()
        {
            InitializeComponent();

            serviceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem;

            serviceInstaller1.ServiceName = MonitorService.SERVICE_NAME;
        }
    }
}
