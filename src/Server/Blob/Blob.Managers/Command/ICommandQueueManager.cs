using System;
using System.Threading.Tasks;
using Blob.Contracts.Command;

namespace Blob.Managers.Command
{
    public interface ICommandQueueManager : IDisposable
    {
        Task<bool> QueueCommandAsync(Guid deviceId, ICommand command);
    }
}
