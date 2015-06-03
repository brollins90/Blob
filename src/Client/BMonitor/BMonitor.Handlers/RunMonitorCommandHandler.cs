using Blob.Contracts.Commands;
using BMonitor.Common.Interfaces;
using log4net;

namespace BMonitor.Handlers
{
    public class RunMonitorCommandHandler : IDeviceCommandHandler<RunMonitorCommand>
    {
        private readonly ILog _log;
        private readonly IMonitorScheduler _scheduler;

        public RunMonitorCommandHandler(ILog log, IMonitorScheduler scheduler)
        {
            _log = log;
            _scheduler = scheduler;
        }

        public void Handle(RunMonitorCommand command)
        {
            _scheduler.RunJob(command.MonitorName);
        }
    }
}
