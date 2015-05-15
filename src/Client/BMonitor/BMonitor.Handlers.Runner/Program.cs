using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blob.Contracts.Commands;
using BMonitor.Handlers.Custom;

namespace BMonitor.Handlers.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            //WindowsUpdateCommandHandler handler = new WindowsUpdateCommandHandler();
            //WindowsUpdateCommand cmd = new WindowsUpdateCommand();

            var handler = new PsExecuteCommandHandler();
            PsExecuteCommand cmd = new PsExecuteCommand {ScriptPath = @"C:\_\psscript.ps1"};

            // C:\_\psscript.ps1
            handler.Handle(cmd);
        }
    }
}
