using System.Collections.Generic;
using Blob.Contracts.Command;

namespace Blob.Contracts.Commands
{
    public class PsExecuteCommand : ICommand
    {
        public string PsScript { get; set; }
        private ICollection<string> Arguments { get; set; }
    }
}
