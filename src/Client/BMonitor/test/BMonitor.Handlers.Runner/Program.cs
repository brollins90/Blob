using Blob.Contracts.Commands;

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
