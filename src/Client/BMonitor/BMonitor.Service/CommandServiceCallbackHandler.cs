using Blob.Contracts.Command;
using log4net;
using System;

namespace BMonitor.Service
{
    public class CommandServiceCallbackHandler : ICommandServiceCallback
    {
        private readonly ILog _log;

        public CommandServiceCallbackHandler()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        public void OnConnect(string message)
        {
            _log.Debug(string.Format("Client received OnConnect callback: {0}", message));
        }

        public void OnDisconnect(string message)
        {
            _log.Debug(string.Format("Client received OnDisconnect callback: {0}", message));
        }

        public void OnReceivedPing(string message)
        {
            _log.Debug(string.Format("Client received OnReceivedPing callback: {0}", message));
        }
    }
}
