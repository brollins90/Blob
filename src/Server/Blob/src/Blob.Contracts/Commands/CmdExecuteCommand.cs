using System.Collections.Generic;

namespace Blob.Contracts.Commands
{
    public class CmdExecuteCommand : IDeviceCommand
    {
        public string CommandString { get; set; }
        private ICollection<string> Arguments { get; set; }
    }
}
