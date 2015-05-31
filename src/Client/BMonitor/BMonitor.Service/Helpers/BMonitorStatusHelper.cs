using System;
using System.Linq;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Proxies;
using BMonitor.Common.Models;
using log4net;
using Ninject;

namespace BMonitor.Service.Helpers
{
    public class BMonitorStatusHelper
    {
        private IKernel _kernel;
        private ILog _log;
        private Guid _deviceId;
        private bool _enablePerformanceMonitoring;
        private bool _enableStatusMonitoring;

        public BMonitorStatusHelper(IKernel kernel, Guid deviceId, bool enablePerformanceMonitoring, bool enableStatusMonitoring)
        {
            _kernel = kernel;
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _deviceId = deviceId;
            _enablePerformanceMonitoring = enablePerformanceMonitoring;
            _enableStatusMonitoring = enableStatusMonitoring;
        }

        public void SendResults(ResultData result)
        {
            if (result.MonitorId == null)
            {
                throw new Exception("MonitorId is required");
            }

            AddStatusRecordDto statusData = new AddStatusRecordDto()
            {
                AlertLevel = (int)result.AlertLevel,
                CurrentValue = result.Value,
                DeviceId = _deviceId,
                MonitorDescription = result.MonitorDescription,
                MonitorId = result.MonitorId,
                MonitorName = result.MonitorName,
                TimeGenerated = result.TimeGenerated,
                TimeSent = DateTime.Now
            };

            AddPerformanceRecordDto spd = null;
            if (_enablePerformanceMonitoring && result.Perf.Any())
            {
                spd = new AddPerformanceRecordDto
                {
                    DeviceId = _deviceId,
                    MonitorDescription = result.MonitorDescription,
                    MonitorId = result.MonitorId,
                    MonitorName = result.MonitorName,
                    TimeGenerated = result.TimeGenerated,
                    TimeSent = DateTime.Now
                };

                foreach (PerformanceData perf in result.Perf)
                {
                    spd.Data.Add(new PerformanceRecordValue
                    {
                        Critical = perf.Critical,
                        Label = perf.Label,
                        Max = perf.Max,
                        Min = perf.Min,
                        UnitOfMeasure = perf.UnitOfMeasure,
                        Value = perf.Value,
                        Warning = perf.Warning
                    });
                }

                _log.Debug(statusData.CurrentValue + "|" + result.Perf.FirstOrDefault().ToString());
            }
            _log.Debug(statusData);

            _log.Info("Creating new StatusClient");
            var u = new Ninject.Parameters.ConstructorArgument("username", _deviceId.ToString());
            var p = new Ninject.Parameters.ConstructorArgument("password", _deviceId.ToString());
            DeviceStatusClient statusClient = _kernel.Get<DeviceStatusClient>(u, p);
            statusClient.ClientErrorHandler += HandleException;

            // send
            if (_enableStatusMonitoring)
            {
                if (_enablePerformanceMonitoring && spd != null)
                {
                    statusData.PerformanceRecordDto = spd;
                }
                _log.Debug("Sending status message.");
                Task.Run(() => statusClient.AddStatusRecordAsync(statusData));
            }
            else
            {
                if (_enablePerformanceMonitoring && spd != null)
                {
                    _log.Debug("Sending performance message.");
                    Task.Run(() => statusClient.AddPerformanceRecordAsync(spd));
                }
            }
        }

        private void HandleException(Exception ex)
        {
            _log.Error(string.Format("Error from client proxy."), ex);
            ;
        }
    }
}
