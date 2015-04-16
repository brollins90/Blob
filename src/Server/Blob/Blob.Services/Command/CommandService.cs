using Blob.Contracts.Command;
using log4net;
using System;
using System.ServiceModel;

namespace Blob.Services.Command
{
    public class CommandService : ICommandService
    {
        private readonly ILog _log;
        //private readonly ICommandManager _commandManager;

        public CommandService(ILog log)
        {
            _log = log;
            //_registrationManager = registrationManager;
        }


        private ICommandServiceCallback Callback
        {
            get { return OperationContext.Current.GetCallbackChannel<ICommandServiceCallback>(); }
        }

        public void Connect(Guid deviceId)
        {
            _log.Debug(string.Format("Got Connect: {0}", deviceId));
            CommandManager.Instance.AddCallback(deviceId, Callback);
        }

        public void Disconnect(Guid deviceId)
        {
            _log.Debug(string.Format("Got Disconnect: {0}", deviceId));
            CommandManager.Instance.RemoveCallback(deviceId, Callback);
        }

        public void Ping(Guid deviceId)
        {
            _log.Debug(string.Format("Got Ping from: {0}", deviceId));
            Callback.OnReceivedPing("" + deviceId + " pinged successfully.");
        }
    }
}
