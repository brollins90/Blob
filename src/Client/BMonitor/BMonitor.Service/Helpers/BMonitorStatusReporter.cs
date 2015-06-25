using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using BMonitor.Common.Extensions;
using BMonitor.Common.Models;
using BMonitor.Service.Extensions;
using log4net;

namespace BMonitor.Service.Helpers
{
    public class BMonitorStatusReporter
    {
        private readonly ILog _log;
        private readonly BlobClientFactory _factory;

        public BMonitorStatusReporter(ILog log, BlobClientFactory factory)
        {
            _log = log;
            _factory = factory;
        }

        public void SendResults(ResultData result, Guid deviceId, bool enableStatusMonitoring, bool enablePerformanceMonitoring)
        {
            if (result.MonitorId == null)
            {
                throw new Exception("MonitorId is required");
            }

            var statusData = result.ToAddStatusRecordDto(deviceId);
            _log.Debug(statusData);

            var statusClient = _factory.CreateStatusClient(deviceId);

            _log.Info("got DeviceStatusClient");
            statusClient.ClientErrorHandler += HandleException;

            // send
            if (enableStatusMonitoring)
            {
                if (!enablePerformanceMonitoring && statusData.PerformanceRecordDto != null)
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
