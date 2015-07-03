namespace Blob.Core.Command
{
    using System;
    using System.Threading.Tasks;
    using Contracts.Commands;

    public interface ICommandQueueManager : IDisposable
    {
        Task<bool> QueueCommandAsync(Guid deviceId, Guid commandId, IDeviceCommand command);
    }
}