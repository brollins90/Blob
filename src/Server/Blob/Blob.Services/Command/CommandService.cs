using System;
using System.Security.Permissions;
using System.ServiceModel;
using Blob.Contracts.Command;
using Blob.Managers.Command;
using log4net;

namespace Blob.Services.Command
{
    [ServiceBehavior]
    //[PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
    public class CommandService : ICommandService
    {
        private readonly ILog _log;

        public CommandService(ILog log)
        {
            _log = log;
        }


        private ICommandServiceCallback Callback
        {
            get { return OperationContext.Current.GetCallbackChannel<ICommandServiceCallback>(); }
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Assert, Role = "Device")]
        public void Connect(Guid deviceId)
        {
            _log.Debug(string.Format("Got Connect: {0}", deviceId));
            CommandConnectionManager.Instance.AddCallback(deviceId, Callback);
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Demand, Role = "Device")]
        public void Disconnect(Guid deviceId)
        {
            _log.Debug(string.Format("Got Disconnect: {0}", deviceId));
            CommandConnectionManager.Instance.RemoveCallback(deviceId, Callback);
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
