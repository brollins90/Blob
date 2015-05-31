using System;
using System.Collections.Generic;
using Blob.Contracts.ServiceContracts;

namespace Blob.Core.Command
{
    public interface ICommandConnectionManager : IDisposable
    {
        void AddCallback(Guid deviceId, IDeviceConnectionServiceCallback callback);
        IEnumerable<Guid> GetActiveDeviceIds();
        IDeviceConnectionServiceCallback GetCallback(Guid deviceId);
        bool HasCallback(Guid deviceId);
        void RemoveCallback(Guid deviceId);
    }
}
