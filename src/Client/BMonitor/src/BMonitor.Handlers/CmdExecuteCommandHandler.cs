using System.Diagnostics;
using Blob.Contracts.Commands;

namespace BMonitor.Handlers
{
    public class CmdExecuteCommandHandler : IDeviceCommandHandler<CmdExecuteCommand>
    {
        public void Handle(CmdExecuteCommand command)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = string.Format("/C {0}", command.CommandString);
            process.StartInfo = startInfo;
            process.Start();
        }
    }
}
