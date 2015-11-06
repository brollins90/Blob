using System.Collections.Generic;

namespace Blob.Contracts.Commands
{
    public class PsExecuteCommand : IDeviceCommand
    {
        public string ScriptPath { get; set; }
        private ICollection<string> Arguments { get; set; }
    }
}
