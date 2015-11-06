using System;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using Blob.Contracts.Commands;
using log4net;

namespace BMonitor.Handlers
{
    public class WindowsServiceCommandHandler : IDeviceCommandHandler<WindowsServiceCommand>
    {
        private ILog _log;

        public WindowsServiceCommandHandler(ILog log)
        {
            _log = log;
        }

        public void Handle(WindowsServiceCommand command)
        {
            _log.Debug("Handling Windows Service Command");
            var ctl = ServiceController.GetServices().FirstOrDefault(s => s.ServiceName.Equals(command.ServiceName));
            if (ctl == null) throw new Exception(string.Format("Service with name [{0}] was not found", command.ServiceName));

            string lowerInvar = command.Action.ToLowerInvariant();

            if (lowerInvar.Equals("stop"))
            {
                ctl.Stop();
            }
            else if (lowerInvar.Equals("start"))
            {
                ctl.Stop();
            }
            else if (lowerInvar.Equals("restart"))
            {
                ctl.Stop();
                ctl.Start();
            }
            else
            {
                throw new Exception("invalid action");
            }
        }
    }
}
