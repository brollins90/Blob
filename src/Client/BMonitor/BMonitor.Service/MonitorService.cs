using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BMonitor.Service
{
    public partial class MonitorService : ServiceBase
    {
        public const string SERVICE_NAME = "BMonitorService";

        private Thread _monitorServiceThread;
        private ManualResetEvent _serviceStopEvent;

        private const int TICK_INTERVAL = 1000 * 5; // 5 seconds

        private MonitorManager _manager;

        public MonitorService()
        {
            InitializeComponent();
            ServiceName = SERVICE_NAME;
        }

        public void Start(string[] args)
        {
            OnStart(args);
        }

        protected override void OnStart(string[] args)
        {
            Guid deviceId = Guid.Parse("43116fba-c370-4880-8a98-826176219ba6");
            string path = "";
            _manager = new MonitorManager(deviceId, path);

            _monitorServiceThread = new Thread(RunBMonitor);
            _monitorServiceThread.Start();
        }

        protected override void OnStop()
        {
            if (_serviceStopEvent != null)
            {
                _serviceStopEvent.Set();

                //Wait for the thread to terminate
                TimeSpan stopTimeout = new TimeSpan(0, 0, 10);
                _monitorServiceThread.Join(stopTimeout);
            };
        }


        private void RunBMonitor()
        {
            _serviceStopEvent = new ManualResetEvent(false);

            do
            {
                _manager.MonitorTick();
            } while (!_serviceStopEvent.WaitOne(TICK_INTERVAL));
        }
    }
}
