using System;
using System.Threading.Tasks;
using Blob.Contracts.Commands;

namespace Blob.Managers.Command
{
    public interface ICommandQueueManager : IDisposable
    {
        Task<bool> QueueCommandAsync(Guid deviceId, IDeviceCommand command);
    }
}
