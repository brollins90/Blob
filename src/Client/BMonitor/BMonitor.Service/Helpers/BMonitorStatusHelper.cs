using System;
using System.Linq;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Proxies;
using BMonitor.Common.Extensions;
using BMonitor.Common.Models;
using log4net;
using Ninject;

namespace BMonitor.Service.Helpers
{
    public class BMonitorStatusHelper
    {
        private readonly IKernel _kernel;
        private readonly ILog _log;
        private Guid _deviceId;
        private readonly bool _enablePerformanceMonitoring;
        private readonly bool _enableStatusMonitoring;

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

            var statusData = result.ToAddStatusRecordDto(_deviceId);
            _log.Debug(statusData);

            _log.Info("Creating new StatusClient");
            var u = new Ninject.Parameters.ConstructorArgument("username", _deviceId.ToString());
            var p = new Ninject.Parameters.ConstructorArgument("password", _deviceId.ToString());
            _log.Info(string.Format("StatusClient {0}, {1}", u, p));

            DeviceStatusClient statusClient = _kernel.Get<DeviceStatusClient>(u, p);
            _log.Info("got DeviceStatusClient");
            statusClient.ClientErrorHandler += HandleException;

            // send
            if (_enableStatusMonitoring)
            {
                if (_enablePerformanceMonitoring && statusData.PerformanceRecordDto != null)
                {
                    _log.Debug("removing perf data because it is disabled");
                    statusData.PerformanceRecordDto = null;
                }
                _log.Debug("Sending status message.");
                Task.Run(() => statusClient.AddStatusRecordAsync(statusData));
            }
        }

        private void HandleException(Exception ex)
        {
            _log.Error(string.Format("Error from client proxy."), ex);
            throw ex;
        }
    }
}
