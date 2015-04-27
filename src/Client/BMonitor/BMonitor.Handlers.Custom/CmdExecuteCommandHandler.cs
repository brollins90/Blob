using System;
using System.Diagnostics;
using Blob.Contracts.Command;
using Blob.Contracts.Commands;

namespace BMonitor.Handlers.Custom
{
    public class CmdExecuteCommandHandler : ICommandHandler<CmdExecuteCommand>
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
