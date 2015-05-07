using System;
using System.Security.Permissions;
using System.ServiceModel;
using Blob.Contracts.Command;
using Blob.Managers.Command;
using log4net;

namespace Blob.Services.Device
{
    [ServiceBehavior]
    //[PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
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
        private CommandConnectionManager ConnectionManager
        {
            get { return CommandConnectionManager.Instance; }
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Assert, Role = "Device")]
        public void Connect(Guid deviceId)
        {
            _log.Debug(string.Format("Got Connect: {0}", deviceId));
            ConnectionManager.AddCallback(deviceId, Callback);
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Demand, Role = "Device")]
        public void Disconnect(Guid deviceId)
        {
            _log.Debug(string.Format("Got Disconnect: {0}", deviceId));
            ConnectionManager.RemoveCallback(deviceId, Callback);
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Demand, Role = "Device")]
        public void Ping(Guid deviceId)
        {
            _log.Debug(string.Format("Got Ping from: {0}", deviceId));
            Callback.OnReceivedPing("" + deviceId + " pinged successfully.");
        }
    }
}
