//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using BMonitor.Common.Interfaces;
//using BMonitor.Service.Connection;

//namespace BMonitor.Service
//{
//    public class MonitorMain
//    {
//        private Thread _monitorServiceThread;
//        private Thread _monitorServiceThread;
//        private IConnectionThread _connectionThread;
//        private IMonitorScheduler _monitorScheduler;
//        private ManualResetEvent _serviceStopEvent;

//        public MonitorMain(IConnectionThread connectionThread, IMonitorScheduler monitorScheduler)
//        {
//            _connectionThread = connectionThread;
//            _monitorScheduler = monitorScheduler;
//        }

//        void Start(ManualResetEvent stopEvent)
//        {
//            _serviceStopEvent = stopEvent;
//            _connectionThread.Start();
//            _monitorScheduler.Start();
//        }

//        void Stop()
//        {
//            if (_serviceStopEvent != null)
//            {
//                _serviceStopEvent.Set();

//                //Wait for the thread to terminate
//                TimeSpan stopTimeout = new TimeSpan(0, 0, 10);
//                _monitorServiceThread.Join(stopTimeout);
//            }
//        }
//    }
//}
