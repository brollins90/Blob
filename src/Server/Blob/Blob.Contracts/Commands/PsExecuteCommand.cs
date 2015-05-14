using System.Collections.Generic;

namespace Blob.Contracts.Commands
{
    public class PsExecuteCommand : IDeviceCommand
    {
        public string PsScript { get; set; }
        private ICollection<string> Arguments { get; set; }
    }
}
