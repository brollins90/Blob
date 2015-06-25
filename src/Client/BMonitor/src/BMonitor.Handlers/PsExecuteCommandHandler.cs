using System.Diagnostics;
using Blob.Contracts.Commands;

namespace BMonitor.Handlers
{
    public class PsExecuteCommandHandler : IDeviceCommandHandler<PsExecuteCommand>
    {
        public void Handle(PsExecuteCommand command)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo
                                         {
                                             WindowStyle = ProcessWindowStyle.Hidden,
                                             FileName = "PowerShell.exe",
                                             Arguments = string.Format("-ExecutionPolicy UnRestricted -File {0}", command.ScriptPath),
                                             UseShellExecute = false,
                                             RedirectStandardError = true,
                                             RedirectStandardOutput = true,
                                             CreateNoWindow = true,
                                         };
            process.StartInfo = startInfo;
            process.Start();

        }
    }
}
