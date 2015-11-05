using Blob.Contracts.ServiceContracts;
using Blob.Core.Command;
using log4net;
using System;
using System.ServiceModel;

namespace Blob.Services.Device
{
    public class DeviceConnectionService : IDeviceConnectionService
    {
        private readonly ILog _log;

        public DeviceConnectionService(ILog log)
        {
            _log = log;
        }

        private IDeviceConnectionServiceCallback Callback
        {
            get { return OperationContext.Current.GetCallbackChannel<IDeviceConnectionServiceCallback>(); }
        }
        private ICommandConnectionManager ConnectionManager
        {
            get { return CommandConnectionManager.Instance; }
        }

        public void Connect(Guid deviceId)
        {
            _log.Debug(string.Format("Got Connect: {0}", deviceId));
            ConnectionManager.AddCallback(deviceId, Callback);
        }

        public void Disconnect(Guid deviceId)
        {
            _log.Debug(string.Format("Got Disconnect: {0}", deviceId));
            ConnectionManager.RemoveCallback(deviceId);
        }

        public void Ping(Guid deviceId)
        {
            _log.Debug(string.Format("Got Ping from: {0}", deviceId));
            Callback.OnReceivedPing("" + deviceId + " pinged successfully.");
        }
    }
}
