using System;
using Blob.Proxies;
using log4net;
using Ninject;

namespace BMonitor.Service.Helpers
{
    public class BlobClientFactory
    {
        private readonly IKernel _kernel;
        private readonly ILog _log;

        public BlobClientFactory(IKernel kernel, ILog log)
        {
            _kernel = kernel;
            _log = log;
        }

        public DeviceConnectionClient CreateDeviceConnectionClient(Guid deviceId)
        {
            _log.Info("Creating new DeviceConnectionClient");
            var u = new Ninject.Parameters.ConstructorArgument("username", deviceId.ToString());
            var p = new Ninject.Parameters.ConstructorArgument("password", deviceId.ToString());
            _log.Info(string.Format("DeviceConnectionClient {0}, {1}", u, p));

            DeviceConnectionClient statusClient = _kernel.Get<DeviceConnectionClient>(u, p);
            return statusClient;
        }

        public DeviceStatusClient CreateStatusClient(Guid deviceId)
        {
            return CreateStatusClient(deviceId.ToString(), deviceId.ToString());
        }

        public DeviceStatusClient CreateStatusClient(string username, string password)
        {
            _log.Info("Creating new StatusClient");
            var u = new Ninject.Parameters.ConstructorArgument("username", username);
            var p = new Ninject.Parameters.ConstructorArgument("password", password);
            _log.Info(string.Format("StatusClient {0}, {1}", u, p));

            DeviceStatusClient statusClient = _kernel.Get<DeviceStatusClient>(u, p);
            return statusClient;
        }
    }
}