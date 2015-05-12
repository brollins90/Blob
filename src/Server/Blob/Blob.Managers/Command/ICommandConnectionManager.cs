using System;
using Blob.Contracts.Command;

namespace Blob.Managers.Command
{
    public interface ICommandConnectionManager : IDisposable
    {
        void AddCallback(Guid deviceId, IDeviceConnectionServiceCallback callback);
        IDeviceConnectionServiceCallback GetCallback(Guid deviceId);
        bool HasCallback(Guid deviceId);
        void RemoveCallback(Guid deviceId);
    }
}
