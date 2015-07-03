namespace Blob.Contracts.Commands
{
    using System.Collections.Generic;
    using Command;

    public class PsExecuteCommand : IDeviceCommand
    {
        public string ScriptPath { get; set; }
        private ICollection<string> Arguments { get; set; }
    }
}