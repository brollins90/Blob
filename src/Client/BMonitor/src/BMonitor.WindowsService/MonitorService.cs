using System;
using System.ServiceProcess;
using System.Threading;
using BMonitor.Service;
using log4net;
using Ninject;

namespace BMonitor.WindowsService
{
    public partial class MonitorService : ServiceBase
    {
        private readonly IKernel _kernel;
        private readonly ILog _log;
        public const string SERVICE_NAME = "BMonitorService";

        private Thread _monitorServiceThread;
        private ManualResetEvent _serviceStopEvent;

        private const int TICK_INTERVAL = 1000 * 5; // 5 seconds

        private MonitorManager _manager;

        public MonitorService(IKernel kernel, ILog log)
        {
            _kernel = kernel;
            _log = log;
            InitializeComponent();
            ServiceName = SERVICE_NAME;
        }

        public void Start(string[] args)
        {
            _log.Debug("Starting MonitorService.");
            OnStart(args);
        }

        protected override void OnStart(string[] args)
        {
            _log.Debug("OnStart called for MonitorService.");
            _serviceStopEvent = new ManualResetEvent(false);

            //_managerConfig = (NameValueCollection)ConfigurationManager.GetSection("BMonitor");
            _manager = _kernel.Get<MonitorManager>();
            if (_manager == null)// || _managerConfig == null)
                throw new InvalidOperationException("A required component of the MonitorService failed to load.");

            _monitorServiceThread = new Thread(RunBMonitor);
            _monitorServiceThread.Start();
        }

        protected override void OnStop()
        {
            _log.Debug("OnStop called for MonitorService.");

            if (_serviceStopEvent != null)
            {
                _serviceStopEvent.Set();
                _log.Debug("Stop event set.");

                //Wait for the thread to terminate
                TimeSpan stopTimeout = new TimeSpan(0, 0, 10);
                _monitorServiceThread.Join(stopTimeout);
            }
        }


        // This is the manager thread method
        private void RunBMonitor()
        {
            _log.Debug("MonitorService thread start.");

            _manager.LoadConfig();
            _manager.Start();

            // add as a task when i have a scheduler
            // register first
            //_manager.RegisterDevice("customerUser1", "password");

            do
            {
                _manager.MonitorTick();
            } while (!_serviceStopEvent.WaitOne(TICK_INTERVAL));
        }
    }
}
