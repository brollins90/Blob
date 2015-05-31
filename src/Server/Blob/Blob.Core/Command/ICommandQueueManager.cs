using System;
using System.Threading.Tasks;
using Blob.Contracts.Commands;

namespace Blob.Core.Command
{
    public interface ICommandQueueManager : IDisposable
    {
        Task<bool> QueueCommandAsync(Guid deviceId, Guid commandId, IDeviceCommand command);
    }
}
