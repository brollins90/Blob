namespace Blob.Contracts.Commands
{
    using System.Collections.Generic;
    using Command;

    public class CmdExecuteCommand : IDeviceCommand
    {
        public string CommandString { get; set; }
        private ICollection<string> Arguments { get; set; }
    }
}