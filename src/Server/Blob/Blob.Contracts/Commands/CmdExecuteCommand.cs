﻿using System.Collections.Generic;
using Blob.Contracts.Command;

namespace Blob.Contracts.Commands
{
    public class CmdExecuteCommand : ICommand
    {
        public string CommandString { get; set; }
        private ICollection<string> Arguments { get; set; }
    }
}